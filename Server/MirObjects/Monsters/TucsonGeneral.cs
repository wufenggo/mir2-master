using Server.MirDatabase;
using S = ServerPackets;
using System.Drawing;
using Server.MirEnvir;
using System.Collections.Generic;

namespace Server.MirObjects.Monsters
{
    public class TucsonGeneral : MonsterObject
    {
        protected virtual byte AttackRange
        {
            get
            {
                return 10;
            }
        }

        public long NukeDelay = Envir.Time;

        protected internal TucsonGeneral(MonsterInfo info)
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

            if (HP  < MaxHP / 1.25F && NukeDelay < Envir.Time )
            {
                NukeDelay = Envir.Time + 20 * Settings.Second;
                NukeAoe();
            }

            if (ranged)
                AttackRanged();
            else
                AttackMeele();

            if (Target.Dead)
                FindTarget();

        }

        private void AttackRanged()
        {
            SlowRange();
        }

        private void AttackMeele()
        {
            if (Envir.Random.Next(10) == 1)
            {
                AoeAttack();
            }

            if (Envir.Random.Next(9) == 1)
            {
                SlowRange();
                return;
            }

            HalfmoonAttack();
        }

        private void SlowRange()
        {
            int damage = 0;
            damage = GetAttackPower(MinDC, MaxDC);
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
            AttackTime = Envir.Time + AttackSpeed + 500;

            if (Envir.Random.Next(8) == 0)
                Target.ApplyPoison(new Poison { Owner = this, Duration = 7, PType = PoisonType.Frozen, TickSpeed = 2000 }, this);
            else if (Envir.Random.Next(4) == 0)
                Target.ApplyPoison(new Poison { Owner = this, Duration = 5, PType = PoisonType.Slow, TickSpeed = 2000 }, this);


            if (damage == 0) return;
            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC);
            ActionList.Add(action);
        }

        private void AoeAttack()
        {
            int damage;

            damage =  GetAttackPower(MinMC, MaxMC);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation ,Type = 1});

            List<MapObject> targets = FindAllTargets(2, CurrentLocation, false);

            if (targets.Count != 0)
            {

                for (int i = 0; i < targets.Count; i++)
                {

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, targets[i], damage, DefenceType.MACAgility);
                    ActionList.Add(action);

                    if (Envir.Random.Next(3) == 1)
                    {
                        DelayedAction pa = new DelayedAction(DelayedType.Poison, Envir.Time + 200, targets[i], PoisonType.Stun, SpellEffect.None, 5, 2000, (int)(damage / 7));
                        ActionList.Add(pa);
                    }
                }
            }
        }

        private void NukeAoe()
        {
            int damage;

            damage = GetAttackPower(MinSC, MaxSC);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

            List<MapObject> targets = FindAllTargets(7, CurrentLocation, false);

            if (targets.Count != 0)
            {

                for (int i = 0; i < targets.Count; i++)
                {

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, targets[i], damage, DefenceType.MACAgility);
                    ActionList.Add(action);

                    targets[i].Pushed(this, (MirDirection)Envir.Random.Next(8), 1);

                    Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.TucsonGeneralEffect });

                }
            }
        }


        private void HalfmoonAttack()
        {
            int damage = 0;
            damage = GetAttackPower(MinDC, MaxDC);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            MirDirection dir = Functions.PreviousDir(Direction);
            for (int i = 0; i < 3; i++)
            {
                Point location = Functions.PointMove(CurrentLocation, dir, 1);
                if (!CurrentMap.ValidPoint(location))
                    continue;
                Cell cell = CurrentMap.GetCell(location);
                if (cell != null &&
                    cell.Objects != null)
                {
                    for (int x = 0; x < cell.Objects.Count; x++)
                    {
                        if (cell.Objects[x].Race == ObjectType.Player ||
                            cell.Objects[x].Race == ObjectType.Monster ||
                            cell.Objects[x].Race == ObjectType.Hero)
                        {
                            if (cell.Objects[x].IsAttackTarget(this))
                            {
                                cell.Objects[x].Attacked(this, damage, DefenceType.None);
                            }
                        }
                    }
                }
                dir = Functions.NextDir(dir);
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

            MoveTo(Target.CurrentLocation);
        }
    }
}
