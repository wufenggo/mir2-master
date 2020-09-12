using System.Collections.Generic;
using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class MinotaurKing : RightGuard
    {
        protected override byte AttackRange
        {
            get
            {
                return 6;
            }
        }

        protected internal MinotaurKing(MonsterInfo info)
            : base(info)
        {
        }

        public void CompleteRangeAttack(MapObject _target)
        {
            MapObject target = _target;
            int damage = GetAttackPower(MinMC, MaxMC);

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            List<MapObject> targets = FindAllTargets(3, target.CurrentLocation);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Attacked(this, damage, DefenceType.MAC);
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange())
            {
                Attack();
                return;
            }
            else if (Functions.InRange(CurrentLocation, Target.CurrentLocation, 14))
            {
                CompleteRangeAttack(Target);
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
