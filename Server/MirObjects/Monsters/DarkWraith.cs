using Server.MirDatabase;
using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirObjects.Monsters
{
    public  class DarkWraith : MonsterObject
    {

        public int DebuffDuration; //seconds
        public long debuffTime;
        public int[] DebuffAmount = new int[]//10
        {
            0,//hp0
            0,//mp1
            0,//dc2
            0,//mc3
            0,//sc4
            0,//ac5
            0,//mac6
            0,//crit dmg7
            0,//reflect8
            0,//hp drain9
        };//5 stats
        public int StealDuration;
        public int[] StolenStats = new int[]//9
        {
            0,//hp0
            0,//dc1
            0,//mc2
            0,//sc3
            0,//ac4
            0,//mac5
            0,//crit dmg6
            0,//reflect7
            0,//hp drain8
        };
        public DarkWraith(MonsterInfo info)
            : base(info)
        {
            DebuffDuration = Envir.Random.Next(1, 10);
            StealDuration = DebuffDuration;
            DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
            DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
            DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
            DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
            StolenStats[5] = DebuffAmount[5];
            DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
            StolenStats[6] = DebuffAmount[6];
        }

        public long AttackTime1;

        public int RangeOfAttack = 1;

        /// <summary>
        /// Noraml Attack
        /// </summary>
        public void PerformAttack1()
        {
            int damage = GetAttackPower(MinDC, MaxDC);
            Target.Attacked(this, damage, DefenceType.ACAgility);
            Broadcast(new ServerPackets.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Type = 0, Location = CurrentLocation });
        }


        /// <summary>
        /// Special Attack (Attacks Target in front and two steps beside that target (if Valid)
        /// </summary>
        public void PerformAttack2()
        {
            //  Create a List to populate with targets
            List<MapObject> targets = new List<MapObject>();
            //  Add the main Target
            targets.Add(Target);

            //  Create a cell variable
            Cell obj = null;
            //  Check the cell to the right is valid
            if (CurrentMap.ValidPoint(Functions.PointMove(CurrentLocation, Functions.NextDir(Direction), 1)))
            {
                //  Assign that cell the the Cell Variable we defined above.
                obj = CurrentMap.GetCell(Functions.PointMove(CurrentLocation, Functions.NextDir(Direction), 1));
                //  Ensure that cell is valid
                if (obj != null)
                {
                    //  Ensure that cell is objects
                    if (obj.Objects != null)
                    {
                        //  Cycle through the objects in that Cell
                        for (int i = 0; i < obj.Objects.Count; i++)
                        {
                            //  Ensure the Object is valid and is a player or monster
                            if (obj.Objects[i] != null &&
                                ( ( obj.Objects[i].Race == ObjectType.Player ) ||
                                ( obj.Objects[i].Race == ObjectType.Monster ) ))
                            {
                                //  Convert the cell Object to a Map object
                                MapObject ob = obj.Objects[i];
                                //  Ensure the MapObject is an attack target
                                if (ob.IsAttackTarget(this))
                                    //  Add the target to the List.
                                    targets.Add(ob);
                            }
                        }                        
                    }
                }
            }
            // Same as above but we're going to check to the left
            if (CurrentMap.ValidPoint(Functions.PointMove(CurrentLocation, Functions.PreviousDir(Direction), 1)))
            {
                obj = CurrentMap.GetCell(Functions.PointMove(CurrentLocation, Functions.PreviousDir(Direction), 1));
                if (obj != null)
                {
                    if (obj.Objects != null)
                    {
                        for (int i = 0; i < obj.Objects.Count; i++)
                            if (obj.Objects[i] != null &&
                                ( ( obj.Objects[i].Race == ObjectType.Player ) ||
                                ( obj.Objects[i].Race == ObjectType.Monster ) ))
                            {
                                MapObject ob = obj.Objects[i];
                                if (ob.IsAttackTarget(this))
                                    targets.Add(ob);
                            }
                    }
                }
            }
            //  Ensure we're not using an invalid list.
            if (targets != null)
            {
                //  Cycle through any targets added to the List
                for (int i = 0; i < targets.Count; i++)
                {
                    //  Get damage for each target
                    int damage = GetAttackPower(MinSC, MaxSC);
                    //  Attack the player and as long as damage is done, we will randomise a poison (Frozen)
                    if (targets[i].Attacked(this, damage, DefenceType.MACAgility) > 0)
                    {
                        if (Envir.Time > debuffTime)
                        {
                            targets[i].AddBuff(new Buff { Caster = this, ExpireTime = Envir.Time + (DebuffDuration * Settings.Minute), ObjectID = targets[i].ObjectID, Type = BuffType.MobDebuff, Values = DebuffAmount });
                            targets[i].ApplyPoison(new Poison { Duration = Envir.Random.Next(3, 5), Owner = this, PType = PoisonType.Slow, TickSpeed = 1000, Value = GetAttackPower(MinDC, MaxDC) });
                            AddBuff(new Buff { Caster = this, Values = StolenStats, ExpireTime = Envir.Time + (StealDuration * Settings.Minute), Type = BuffType.MobDebuff, ObjectID = ObjectID });
                            DebuffDuration = Envir.Random.Next(1, 10);
                            StealDuration = DebuffDuration;
                            DebuffAmount[0] = Envir.Random.Next(50, 200);//hp
                            StolenStats[0] = DebuffAmount[0];
                            DebuffAmount[1] = Envir.Random.Next(50, 200);//mp
                            DebuffAmount[5] = Envir.Random.Next(10, 40);//AC
                            StolenStats[5] = DebuffAmount[5];
                            DebuffAmount[6] = Envir.Random.Next(10, 40);//AMC
                            StolenStats[6] = DebuffAmount[6];
                            DebuffAmount[7] = Envir.Random.Next(10, 40);//crit dmg
                            DebuffAmount[8] = Envir.Random.Next(10, 40);//reflect
                            DebuffAmount[9] = Envir.Random.Next(10, 40);//HP drain
                            debuffTime = Envir.Time + (DebuffDuration * Settings.Minute);
                        }
                        //  5 in 50 chance
                        if (Envir.Random.Next(50) > 45)
                            //  Apply poison to the target of a random duration from 0 to 5
                            targets[i].ApplyPoison(new Poison { Owner = this, Duration = Envir.Random.Next(5), PType = PoisonType.Frozen, TickSpeed = 4000, Value = 5 });
                    }
                }
            }
            //  Broadcast the ObjectAttack so the Frames process on the Clients.
            Broadcast(new ServerPackets.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Type = 1, Location = CurrentLocation });
        }

        protected override void ProcessTarget()
        {
            if (Target == null || Target.Dead)
            {
                FindTarget();
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            if (InAttackRange())
            {
                if (Envir.Time > AttackTime)
                {
                    if (Envir.Time > AttackTime1)
                    {
                        PerformAttack2();
                        AttackTime1 = Envir.Time + Settings.Second * 20;
                    }
                    else
                        PerformAttack1();
                    AttackTime = Envir.Time + AttackSpeed;
                }
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            if (Target.Dead)
            {
                FindTarget();
                return;
            }
            MoveTo(Target.CurrentLocation);
        }
    }
}
