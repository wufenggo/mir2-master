using Server.MirDatabase;
using Server.MirEnvir;
using System.Collections.Generic;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class HolyDragon1 : MonsterObject
    {
        public long FearTime;
        public byte AttackRange = 6;
        public bool Summoned;

        protected internal HolyDragon1(MonsterInfo info)
            : base(info)
        {
            Direction = MirDirection.DownLeft;
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
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });


            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(MinDC, MaxDC);  // into this atk
            if (damage == 0) return;
            int boost = 0;
            switch (PetLevel)
            {
                case 1:
                    boost = 5;
                    break;
                case 2:
                    boost = 10;
                    break;
                case 3:
                    boost = 15;
                    break;
                case 4:
                    boost = 20;
                    break;
                case 5:
                    boost = 25;
                    break;
                case 6:
                    boost = 30;
                    break;
                case 7:
                    boost = 35;
                    break;
            }
            if (boost > 0)
            {
                int tmp = damage * boost / 100;
                damage += tmp;
            }
            long delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
            
            List<MapObject> targets = FindAllTargets(1, Target.CurrentLocation, false);
            if (targets.Count > 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].IsAttackTarget(this))
                    {
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, targets[i], damage, DefenceType.MAC);
                        ActionList.Add(action);
                    }
                }
            }
            else if (CurrentMap.ValidPoint(Target.CurrentLocation))
            {
                Cell cell = CurrentMap.GetCell(Target.CurrentLocation);
                if (cell != null &&
                    cell.Objects != null &&
                    cell.Objects.Count > 0)
                {
                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        if (cell.Objects[i].Race != ObjectType.Player &&
                            cell.Objects[i].Race != ObjectType.Monster)
                            continue;
                        if (cell.Objects[i].IsAttackTarget(this))
                        {
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, cell.Objects[i], damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                    }
                }
            }
            else
            {
                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, Target, damage, DefenceType.MAC);
                ActionList.Add(action);
            }
            if (Target.Dead)
                FindTarget();

        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (Master != null)
                MoveTo(Master.CurrentLocation);

            if (InAttackRange() && (Master != null || Envir.Time < FearTime))
            {
                Attack();
                return;
            }

            FearTime = Envir.Time + 5000;

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist < AttackRange)
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2)) //No favour
                {
                    case 0:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.NextDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                    default:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.PreviousDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                }
            }
        }

        public override void Spawned()
        {
            base.Spawned();

            Summoned = true;
        }

        public override Packet GetInfo()
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = Monster.HolyDragon1,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
                Skeleton = Harvested,
                Poison = CurrentPoison,
                Hidden = Hidden,
                Extra = Summoned,
                GlowAura = Info.LightColar,
                LightEffect = Info.LightEffect,
                AC = Info.MinAC,
                MAC = Info.MinMAC,
                DC = Info.MinDC,
                MC = Info.MinMC,
                SC = Info.MinSC,
                MaxAC = Info.MaxAC,
                MaxMAC = Info.MaxMAC,
                MaxDC = Info.MaxDC,
                MaxMC = Info.MaxMC,
                MaxSC = Info.MaxSC,
                Acc = Info.Accuracy,
                Agil = Info.Agility,
                AttkSpeed = Info.AttackSpeed,
                Health = Info.HP,
                CurrentHP = HP,
                IsPet = Master == null ? false : true,
                Level = Info.Level,
                IsTamable = Info.CanTame,
                IsPushable = Info.CanPush,
                IsUndead = Info.Undead,
                MoveSpeed = Info.MoveSpeed,
                Experience = Info.Experience,
            };
        }
    }
}
