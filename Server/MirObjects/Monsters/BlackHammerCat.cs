using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class BlackHammerCat : MonsterObject
    {
        public BlackHammerCat(MonsterInfo info)
            : base(info)
        {

        }

        public void PerformSmashAttack()
        {
            List<MapObject> targets = FindAllTargets(1, Target.CurrentLocation, false);
            if (targets.Count > 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    int damage = GetAttackPower(MinDC, MaxDC);
                    targets[i].Attacked(this, damage, DefenceType.ACAgility);
                }
            }
            Broadcast(new S.ObjectEffect { Direction = Direction, Effect = SpellEffect.BlackHammerCatSmash, ObjectID = Target.ObjectID });
            Broadcast(new S.ObjectAttack { Direction = Direction, ObjectID = ObjectID, Location = CurrentLocation });
            AttackTime = Envir.Time + AttackSpeed;
            ActionTime = Envir.Time + 300;
        }

        protected override void ProcessTarget()
        {
            if (Target == null)
                return;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            if (InAttackRange() && CanAttack)
            {

                PerformSmashAttack();
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
