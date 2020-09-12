using Server.MirDatabase;
using S = ServerPackets;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Drawing;

namespace Server.MirObjects.Monsters
{
    class FlameQueen : MonsterObject
    {
        public long AoeDmg;
        public static int AoeCooldown = 20000;
        protected virtual byte AttackRange
        {
            get
            {
                return 11;
            }
        }

        protected internal FlameQueen(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (AoeDmg > Envir.Time || HP == MaxHP)
            {
                if (!ranged && Envir.Random.Next(6) != 1)
                {
                    int damage;
                    if (Envir.Random.Next(7) != 1)
                    {
                        damage = GetAttackPower(MinDC, MaxDC);
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                        Target.Attacked(this, damage, DefenceType.ACAgility);
                    }
                    else
                    {
                        damage = GetAttackPower(MinSC, MaxSC);
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation ,Type = 1});
                        Target.Attacked(this, damage, DefenceType.AC);
                        Target.Pushed(this, Direction, 3 + Envir.Random.Next(2));
                    }
              
                }
                else
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                    AoeAttack(3, Target.CurrentLocation, false);

                }
            }
            else
            {
                AoeDmg = Envir.Time + AoeCooldown;
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = ObjectID , Type = 1});

                AoeAttack(6, CurrentLocation, true , 1.50F);
            }

            if (Target.Dead)
                FindTarget();

        }



        private void AoeAttack(int range , Point Loc , bool ign , float mul = 1F)
        {
            int damage;

            damage = (int)(mul * GetAttackPower(MinMC, MaxMC));

            List<MapObject> targets = FindAllTargets(range, Loc, false);

            if (targets.Count != 0)
            {

                for (int i = 0; i < targets.Count; i++)
                {

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.MACAgility);
                    ActionList.Add(action);

                    if (ign && Envir.Random.Next(8) == 1)
                    {
                        DelayedAction pa = new DelayedAction(DelayedType.Poison, Envir.Time + 200, targets[i], PoisonType.Stun, SpellEffect.None, 3, 2000, (int)(damage / 7));
                        ActionList.Add(pa);
                    }
                }
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null) return;

            if (InAttackRange() && CanAttack)
            {
                Attack();
                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, 1))
                MoveTo(Target.CurrentLocation);

        }
    }
}