using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class NewIncarnatedZT : ZumaTaurus
    {
        /// <summary>
        /// Default 7
        /// </summary>
        private int _fireWallRange = 7;
        /// <summary>
        /// Default 4
        /// </summary>
        private int _fireWallChance = 4;
        public long fwDelay;
        protected internal NewIncarnatedZT(MonsterInfo info) : base(info)
        {
            AvoidFireWall = false;
            Stoned = false;
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (Envir.Time > fwDelay)
            {
                fwDelay = Envir.Time + Settings.Second * 5;
                if (Functions.InRange(CurrentLocation, Target.CurrentLocation, _fireWallRange) && 
                    Envir.Random.Next(_fireWallChance) == 0)
                {
                    int damage = GetAttackPower(MinMC, MaxMC);
                    if (damage > 0)
                    {
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                        //Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                        ActionTime = Envir.Time + 500;
                        AttackTime = Envir.Time + AttackSpeed;
                        SpawnFireWall(Target.CurrentLocation, damage);
                        return;
                    }
                }
            }
            if (InAttackRange())
            {
                Attack();
                if (Target.Dead)
                    FindTarget();
                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            MoveTo(Target.CurrentLocation);
        }


        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;


            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;

            if (Target.Attacked(this, damage, DefenceType.MACAgility) <= 0) return;

            if (Envir.Random.Next(Settings.PoisonResistWeight) >= Target.PoisonResist)
            {
                if (Envir.Random.Next(12) == 0)
                    Target.ApplyPoison(new Poison { PType = PoisonType.Paralysis, Duration = 5, TickSpeed = 1000 }, this);
            }
        }

        private void SpawnFireWall(Point location, int value)
        {
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.FireWall, Cast = true, Level = 3 });
            DelayedAction action = new DelayedAction(DelayedType.MonsterMagic, Envir.Time + 500, this, Spell.MobFireWall, value, location);
            //  Add the action to the current map
            CurrentMap.ActionList.Add(action);
        }
    }

}
