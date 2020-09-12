using Server.MirDatabase;
using System.Collections.Generic;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class FlameMage : RightGuard
    {
        public long FearTime;

        protected internal FlameMage(MonsterInfo info)
            : base(info)
        {
        }

        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            List<MapObject> targets = FindAllTargets(2, target.CurrentLocation);

            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Attacked(this, damage, defence);
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
