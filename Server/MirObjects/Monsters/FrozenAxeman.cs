using System;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;
using System.Collections.Generic;

namespace Server.MirObjects.Monsters
{
    public class FrozenAxeman : MonsterObject
    {
        private long shieldBashCooldown;

        protected internal FrozenAxeman(MonsterInfo info)
            : base(info)
        {
            shieldBashCooldown = Envir.Time;
        }


        protected override void Attack()
        {
            if (shieldBashCooldown > Envir.Time - 8000 )
            {
                base.Attack();
            }
            else
            {
                shieldBashCooldown = Envir.Time + Envir.Random.Next(3000);

                if (!Target.IsAttackTarget(this))
                {
                    Target = null;
                    return;
                }

                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                Bash();
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });


                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;
                ShockTime = 0;


            }
        }

        private void Bash()
        {

            int damage = GetAttackPower(MinMC, MaxMC);

            Target.Attacked(this, damage, DefenceType.MAC);

            MirDirection dir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Target.Pushed(this, dir, 1);

        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange())
            {
                Attack();
                if (Target.Dead)
                    FindTarget();

                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            if (Info.TeleportBack && Envir.Random.Next(7) == 0 && Target.CurrentMap == CurrentMap)
                Teleport(Target.CurrentMap, Target.Back);
            else
                if (Envir.Random.Next(7) == 0)
                Taunt();
            else
                MoveTo(Target.CurrentLocation);


        }

        private void Taunt()
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
        }

    }
}

