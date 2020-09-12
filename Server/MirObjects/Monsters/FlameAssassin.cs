using Server.MirDatabase;
using System.Collections.Generic;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class FlameAssassin : RightGuard
    {
        public long FearTime;

        protected internal FlameAssassin(MonsterInfo info)
            : base(info)
        {
        }

        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            if(target.Attacked(this, damage, defence) > 0)
            {
                if (Target is PlayerObject tmpOb) // this function is single target ye
                {
                    //  Pick number between 0~10, check if it's more or equal to players poison resist
                    if (Envir.Random.Next(Settings.PoisonResistWeight) >= tmpOb.PoisonResist) // check pr/mr settings
                    {
                        if (Envir.Random.Next(30) == 0 && tmpOb.Class == MirClass.Warrior && tmpOb.Class == MirClass.Assassin)
                        {
                            tmpOb.ApplyPoison(new Poison { Owner = this, PType = PoisonType.Burning, Duration = GetAttackPower(MinDC, MaxDC), TickSpeed = 1000 }, this);
                        }

                        if (Envir.Random.Next(25) == 0 && tmpOb.Class == MirClass.Wizard)
                        {
                            tmpOb.ApplyPoison(new Poison { Owner = this, PType = PoisonType.Red, Duration = GetAttackPower(MinMC, MaxMC), TickSpeed = 1000 }, this);
                        }

                        if (Envir.Random.Next(24) == 0 && tmpOb.Class == MirClass.Taoist)
                        {
                            tmpOb.ApplyPoison(new Poison { Owner = this, PType = PoisonType.Green, Duration = GetAttackPower(MinSC, MaxSC), TickSpeed = 1000 }, this);
                        }
                    }
                }
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange() && Envir.Time < FearTime)
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

            if (Info.TeleportBack && Envir.Random.Next(7) == 0 && Target.CurrentMap == CurrentMap)
                Teleport(Target.CurrentMap, Target.Back);
            else
                MoveTo(Target.CurrentLocation);
        }
    }
}
