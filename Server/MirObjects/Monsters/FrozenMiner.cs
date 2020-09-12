using System;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;
using System.Collections.Generic;

namespace Server.MirObjects.Monsters
{
    public class FrozenMiner : MonsterObject
    {
        private long whirlwindCooldown;

        protected internal FrozenMiner(MonsterInfo info)
            : base(info)
        {
            whirlwindCooldown = Envir.Time;
        }


        protected override void Attack()
        {
            if (whirlwindCooldown > Envir.Time - 10000)
            {
                base.Attack();
            }
            else
            {
                whirlwindCooldown = Envir.Time;

                if (!Target.IsAttackTarget(this))
                {
                    Target = null;
                    return;
                }

                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                Whirlwind();
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });


                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;
                ShockTime = 0;


            }
        }

        private void Whirlwind()
        {

            int damage = GetAttackPower(MinMC, MaxMC);

            List<MapObject> targets = FindAllTargets(1, CurrentLocation);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Attacked(this, damage, DefenceType.MAC);

            }

        }

    }
}

