using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class StrayCat : MonsterObject
    {
        public long ThrustAttackTime;
        public StrayCat(MonsterInfo info)
            : base(info)
        {

        }



        protected override void ProcessTarget()
        {
            if (Target == null)
                return;

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
