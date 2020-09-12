using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirObjects.Monsters
{
    public class DarkSpirit : MonsterObject
    {

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

        public DarkSpirit(MonsterInfo info)
            : base(info)
        {
            DebuffDuration = Envir.Random.Next(1, 10);
            StealDuration = DebuffDuration;
            DebuffAmount[0] = Envir.Random.Next(50, 200);//hp
            StolenStats[0] = DebuffAmount[0];
            DebuffAmount[1] = Envir.Random.Next(50, 200);//mp
            DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
            StolenStats[5] = DebuffAmount[5];
            DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
            StolenStats[6] = DebuffAmount[6];
            DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
            DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
            DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
        }

        protected override void ProcessTarget()
        {
            if (Target == null || Target.Dead)
            {
                FindTarget();
                return;
            }
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            if (InAttackRange())
            {
                if (Envir.Time > AttackTime)
                {
                    Attack();
                    if (Envir.Time > debuffTime)
                    {
                        Target.AddBuff(new Buff { Caster = this, ExpireTime = Envir.Time + (DebuffDuration * Settings.Minute), ObjectID = Target.ObjectID, Type = BuffType.MobDebuff, Values = DebuffAmount });
                        Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(3, 5), Owner = this, PType = PoisonType.Slow, TickSpeed = 1000, Value = GetAttackPower(MinDC, MaxDC) });
                        AddBuff(new Buff { Caster = this, Values = StolenStats, ExpireTime = Envir.Time + (StealDuration * Settings.Minute), Type = BuffType.MobDebuff, ObjectID = ObjectID });
                        DebuffDuration = Envir.Random.Next(1, 10);
                        StealDuration = DebuffDuration;
                        DebuffAmount[0] = Envir.Random.Next(50, 200);//hp
                        StolenStats[0] = DebuffAmount[0];
                        DebuffAmount[1] = Envir.Random.Next(50, 200);//mp
                        DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                        StolenStats[5] = DebuffAmount[5];
                        DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                        StolenStats[6] = DebuffAmount[6];
                        DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                        DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                        DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                        debuffTime = Envir.Time + (DebuffDuration * Settings.Minute);
                    }
                    AttackTime = Envir.Time + AttackSpeed;
                }
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }
            if (Target.Dead)
            {
                FindTarget();
                return;
            }
            if (!InAttackRange())
                MoveTo(Target.CurrentLocation);
        }
    }
}
