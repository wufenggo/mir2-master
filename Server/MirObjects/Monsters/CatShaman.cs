﻿using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirObjects.Monsters
{
    public class CatShaman : MonsterObject
    {
        public CatShaman(MonsterInfo info)
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
