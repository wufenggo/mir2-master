﻿using System.Collections.Generic;
using System;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class TucsonWarrior : MonsterObject
    {
        protected internal TucsonWarrior(MonsterInfo info)
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
            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            int damage;

           if (Envir.Random.Next(5) != 1)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                damage = GetAttackPower(MinDC, MaxDC);
                if (damage == 0) return;

                HalfmoonAttack(damage);

            }           
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                damage = GetAttackPower(MinMC, MaxMC);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.AC);
                ActionList.Add(action);

                Target.Pushed(this, Direction, 1 + Envir.Random.Next(2));
                if ( Envir.Random.Next(4) == 1)
                {
                    Target.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = 3, TickSpeed = 1000 }, this);
                }
            }

            if (Target.Dead)
                FindTarget();

        }


        private void HalfmoonAttack(int damage)
        {
            MirDirection dir = Functions.PreviousDir(Direction);
            for (int i = 0; i < 4; i++)
            {
                Point location = Functions.PointMove(CurrentLocation, dir, 1);
                if (!CurrentMap.ValidPoint(location))
                    continue;
                Cell cell = CurrentMap.GetCell(location);
                if (cell != null &&
                    cell.Objects != null)
                {
                    for (int x = 0; x < cell.Objects.Count; x++)
                    {
                        if (cell.Objects[x].Race == ObjectType.Player ||
                            cell.Objects[x].Race == ObjectType.Monster ||
                            cell.Objects[x].Race == ObjectType.Hero)
                        {
                            if (cell.Objects[x].IsAttackTarget(this))
                            {
                                cell.Objects[x].Attacked(this, damage, DefenceType.MAC);
                            }
                        }
                    }
                }
                dir = Functions.NextDir(dir);
            }
        }

     
    }
}
