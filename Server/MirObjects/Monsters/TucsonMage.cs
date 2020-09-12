using Server.MirDatabase;
using S = ServerPackets;
using System.Drawing;
using Server.MirEnvir;

namespace Server.MirObjects.Monsters
{
    public class TucsonMage : MonsterObject
    {
        protected internal TucsonMage(MonsterInfo info)
            : base(info)
        {
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
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;




            int col = 3;
            int row = 3;

            Point[] loc = new Point[col]; //0 = left 1 = center 2 = right
            loc[0] = Functions.PointMove(CurrentLocation, Functions.PreviousDir(Direction), 1);
            loc[1] = Functions.PointMove(CurrentLocation, Direction, 1);
            loc[2] = Functions.PointMove(CurrentLocation, Functions.NextDir(Direction), 1);

            for (int i = 0; i < col; i++)
            {
                Point startPoint = loc[i];
                for (int j = 0; j < row; j++)
                {
                    Point hitPoint = Functions.PointMove(startPoint, Direction, j);

                    if (!CurrentMap.ValidPoint(hitPoint)) continue;

                    Cell cell = CurrentMap.GetCell(hitPoint);

                    if (cell.Objects == null) continue;

                    for (int k = 0; k < cell.Objects.Count; k++)
                    {
                        MapObject target = cell.Objects[k];
                        switch (target.Race)
                        {
                            case ObjectType.Monster:
                            case ObjectType.Player:
                            case ObjectType.Hero:
                                //Only targets
                                if (target.IsAttackTarget(this))
                                {
                                    target.Attacked(this, damage, DefenceType.MAC);

                                    if (Envir.Random.Next(Settings.PoisonResistWeight) >= Target.PoisonResist)
                                    {
                                        if (Envir.Random.Next(25) == 0)
                                        {
                                            target.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = 3, TickSpeed = 1000 }, this);
                                        }
                                    }

                                }
                                break;
                        }
                    }
                }
            }



        }
    }
}
