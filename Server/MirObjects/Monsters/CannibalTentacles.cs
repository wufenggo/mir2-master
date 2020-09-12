using Server.MirDatabase;
using S = ServerPackets;
using System.Drawing;

namespace Server.MirObjects.Monsters
{
    public class CannibalTentacles : MonsterObject
    {
        protected virtual byte AttackRange
        {
            get
            {
                return 8;
            }
        }

        protected internal CannibalTentacles(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void Attack()
        {

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(MinDC, MaxDC);
            if (!ranged && Envir.Random.Next(100) > 20)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                if (damage == 0) return;

                Target.Attacked(this, (int)(damage * 1.5f), DefenceType.MACAgility);
            }
            else
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                AttackTime = Envir.Time + AttackSpeed + 500;
                if (damage == 0) return;

                if (Envir.Random.Next(7) == 0)
                    Target.ApplyPoison(new Poison { Owner = this, Duration = 10, PType = PoisonType.Red, TickSpeed = 2000 }, this);


                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC);
                ActionList.Add(action);
            }


            if (Target.Dead)
                FindTarget();

        }

        protected override void ProcessTarget()
        {
            if (Target == null) return;

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

            MirDirection d = (MirDirection)Envir.Random.Next(7);
            Point p = Functions.PointMove(CurrentLocation, d, 1);

            MoveTo(p);
        }
    }
}
