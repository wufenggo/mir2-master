using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirObjects.Monsters
{
    public class JinLord : MonsterObject
    {
        public byte MLMinRange = 1, MLMaxRange = 2;
        public byte RMinRange = 2, RMaxRange = 12;

        public byte MaxRangeTarget = 6;

        public long SpecialAttackTime;
        public long RangeAttackTime;

        public JinLord(MonsterInfo info) : base(info)
        {

        }


        public void DoRangeAttack()
        {

        }

        public void DoSpecialAttack()
        {

        }

        protected override void ProcessTarget()
        {
            base.ProcessTarget();
        }
    }
}
