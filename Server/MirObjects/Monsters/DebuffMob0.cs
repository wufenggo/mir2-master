using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class DebuffMob0 : MonsterObject
    {
        public long FearTime;
        public byte AttackRange = 6;

        public int DebuffDuration; //seconds
        public long debuffTime;
        public int[] DebuffAmount = new int[]//10
        {
            0,//hp0
            0,//mp1
            0,//dc2
            0,//mc3
            0,//sc4
            0,//ac5
            0,//mac6
            0,//crit dmg7
            0,//reflect8
            0,//hp drain9
        };//5 stats
        public int StealDuration;
        public int[] StolenStats = new int[]//9
        {
            0,//hp0
            0,//dc1
            0,//mc2
            0,//sc3
            0,//ac4
            0,//mac5
            0,//crit dmg6
            0,//reflect7
            0,//hp drain8
        };

        public DebuffMob0(MonsterInfo info) : base(info)
        {
            if (info.Name == "SnowFlower")
            {
                DebuffDuration = Envir.Random.Next(1, 10);
                StealDuration = DebuffDuration;
                DebuffAmount[0] = Envir.Random.Next(50, 200);//hp
                StolenStats[0] = DebuffAmount[0];
                DebuffAmount[1] = Envir.Random.Next(50, 200);//mp
                DebuffAmount[2] = Envir.Random.Next(10, 40);//DC
                StolenStats[2] = DebuffAmount[2];
                DebuffAmount[3] = Envir.Random.Next(10, 40);//DC
                StolenStats[3] = DebuffAmount[3];
                DebuffAmount[4] = Envir.Random.Next(10, 40);//DC
                StolenStats[4] = DebuffAmount[4];
            }
            else if (info.Name == "SnowFlowerQueen")
            {
                DebuffDuration = Envir.Random.Next(1, 10);
                StealDuration = DebuffDuration;
                DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                StolenStats[5] = DebuffAmount[5];
                DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                StolenStats[6] = DebuffAmount[6];
            }
            else if (info.Name == "FrozenWarewolf")
            {
                DebuffDuration = Envir.Random.Next(1, 10);
                StealDuration = DebuffDuration;
                DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                StolenStats[5] = DebuffAmount[5];
                DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                StolenStats[6] = DebuffAmount[6];
            }
        }

        protected override bool InAttackRange()  //Ice
        {
            return CurrentMap == Target.CurrentMap && CanFly(Target.CurrentLocation) && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
            //ADDED CANFLY SO THAT ARCHER MOBS CANNOT SHOOT THROUGH WALLS
        }


        public void Attack2()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
            int damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;

            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility);
            ActionList.Add(action);
            if (Envir.Random.Next(2) == 0)
                Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(10, 30), Owner = this, PType = PoisonType.Green, TickSpeed = 1000, Value = GetAttackPower(MinMC, MaxMC) });
            if (Target.Dead)
                FindTarget();

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
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
            int damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;

            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.ACAgility);
            ActionList.Add(action);
            if (Envir.Time > debuffTime)
            {
                Target.AddBuff(new Buff { Caster = this, ExpireTime = Envir.Time + (DebuffDuration * Settings.Minute), ObjectID = Target.ObjectID, Type = BuffType.MobDebuff, Values = DebuffAmount });
                if (Info.Name == "SnowFlower")
                {
                    DebuffDuration = Envir.Random.Next(1, 10);
                    StealDuration = DebuffDuration;
                    DebuffAmount[0] = Envir.Random.Next(50, 200);//hp
                    StolenStats[0] = DebuffAmount[0];
                    DebuffAmount[1] = Envir.Random.Next(50, 200);//mp
                    DebuffAmount[2] = Envir.Random.Next(10, 40);//DC
                    StolenStats[2] = DebuffAmount[2];
                    DebuffAmount[3] = Envir.Random.Next(10, 40);//DC
                    StolenStats[3] = DebuffAmount[3];
                    DebuffAmount[4] = Envir.Random.Next(10, 40);//DC
                    StolenStats[4] = DebuffAmount[4];
                    Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(10, 30), Owner = this, PType = PoisonType.Red, TickSpeed = 1000, Value = GetAttackPower(MinDC, MaxDC) });
                    AddBuff(new Buff { Caster = this, Values = StolenStats, ExpireTime = Envir.Time + (StealDuration * Settings.Minute), Type = BuffType.MobDebuff, ObjectID = ObjectID });
                }
                else if (Info.Name == "SnowFlowerQueen")
                {
                    DebuffDuration = Envir.Random.Next(1, 10);
                    StealDuration = DebuffDuration;
                    DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                    DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                    DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                    DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                    StolenStats[5] = DebuffAmount[5];
                    DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                    StolenStats[6] = DebuffAmount[6];
                    Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(10, 30), Owner = this, PType = PoisonType.Red, TickSpeed = 1000, Value = GetAttackPower(MinDC, MaxDC) });
                    AddBuff(new Buff { Caster = this, Values = StolenStats, ExpireTime = Envir.Time + (StealDuration * Settings.Minute), Type = BuffType.MobDebuff, ObjectID = ObjectID });
                }
                else if (Info.Name == "FrozenWarewolf")
                {
                    DebuffDuration = Envir.Random.Next(1, 10);
                    StealDuration = DebuffDuration;
                    DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                    DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                    DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                    DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                    StolenStats[5] = DebuffAmount[5];
                    DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                    StolenStats[6] = DebuffAmount[6];
                    Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(3, 5), Owner = this, PType = PoisonType.Frozen, TickSpeed = 1000, Value = GetAttackPower(MinDC, MaxDC) });
                    AddBuff(new Buff { Caster = this, Values = StolenStats, ExpireTime = Envir.Time + (StealDuration * Settings.Minute), Type = BuffType.MobDebuff, ObjectID = ObjectID });
                }
                debuffTime = Envir.Time + (Envir.Random.Next(2, 6) * Settings.Minute);
            }
            if (Target.Dead)
                FindTarget();
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;


            if (InAttackRange() &&
                Envir.Time < FearTime)
            {
                if (Envir.Time > AttackTime)
                {
                    if (Envir.Random.Next(0, 10) <= 5)
                        Attack();
                    else
                        Attack2();

                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                }
                return;
            }
            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            FearTime = Envir.Time + 5000;
            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist >= AttackRange)
                MoveTo(Target.CurrentLocation);
            else
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
            return;
        }
    }
}
