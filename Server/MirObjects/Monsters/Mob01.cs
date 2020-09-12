using Server.MirDatabase;
using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mob01 : MonsterObject
    {
        #region Stats
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MinDCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MaxDCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MinMCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MaxMCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MinSCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MaxSCBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MinACBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MaxACBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MinMACBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort MaxMACBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort AgilBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort AccBoost;
        /// <summary>
        /// The Current boost from stealing Stats from target(s)
        /// </summary>
        public ushort HealthBoost;

        #endregion

        #region Action Times & Ranges
        /// <summary>
        /// Check every 5 seconds if it's possible to move.
        /// </summary>
        long CheckBlockTime;
        long checkBlockTime;
        /// <summary>
        /// The Ultimate attack (strongest) | casts 15 - 45 seconds.
        /// </summary>
        long UltimateAttackTime;
        long ultimateAttackTime;
        /// <summary>
        /// The Attack Range of the Ultimate Attack.
        /// </summary>
        byte UltimateAttackRange = 14;
        /// <summary>
        /// The Attack that targets a Specific Class | casts 5 - 15 seconds.
        /// </summary>
        long ClassAttackTime;
        long classAttackTime;
        /// <summary>
        /// The Attack Range of the Class Attack.
        /// </summary>
        byte ClassAttackRange = 12;
        /// <summary>
        /// The Attack that targets a valid target with the lowest HP | casts 20 - 35 seconds.
        /// </summary>
        long LowestHPAttackTime;
        long lowestHPAttackTime;
        /// <summary>
        /// The Attack Range of the LowestHP Attack.
        /// </summary>
        byte LowestAttackRange = 4;
        /// <summary>
        /// Cast a random Debuff on the target(s) | casts 3 - 10 seconds.
        /// </summary>
        long DebuffTime;
        long debuffTime;
        /// <summary>
        /// The Buff that is 'cast' upon self has a lower duration than that of the debuff time | 5 - 8 seconds.
        /// </summary>
        long BuffDuration;
        long buffDuration;
        /// <summary>
        /// The Attack Range of the Debuff Attack.
        /// </summary>
        byte DebuffAttackRange = 8;
        /// <summary>
        /// The Time until the next Push | casts 30 - 60 seconds.
        /// </summary>
        long PushTime;
        long pushTime;
        /// <summary>
        /// If we've sent the message as an indication of Push being used soon.
        /// </summary>
        bool PushWarn = false;
        /// <summary>
        /// The Attack Range of the Push Attack.
        /// </summary>
        byte PushAttackRange = 1;
        /// <summary>
        /// Order all monsters on Map to approch the area | casts 15 - 50 seconds.
        /// </summary>
        long OrderMobTime;
        long orderMobTime;
        /// <summary>
        /// The Area range to affect other Monsters.
        /// </summary>
        byte OrderMobRange = 32;
        /// <summary>
        /// Spawn SpellObjects at random points on the map within range | casts 8 - 23 seconds.
        /// </summary>
        long RandomMapAttackTime;
        long randomMapAttackTime;
        /// <summary>
        /// 
        /// </summary>
        byte RandomMapAttackRange = 16;
        /// <summary>
        /// To try prevent being stuck due to being hit so much we'll do a check when we could hit last.
        /// </summary>
        long _lastHitTime;
        long _LastHitTime = Settings.Second * 5;
        #endregion

        /// <summary>
        /// This will switch to true if current health is below 25%
        /// </summary>
        bool WalkAway = false;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">The Monsters Information</param>
        public Mob01(MonsterInfo info) : base (info)
        {
            CheckBlockTime = Settings.Second * 5;
            checkBlockTime = Envir.Time;
            UltimateAttackTime = Settings.Second * Envir.Random.Next(15, 45);
            ultimateAttackTime = Envir.Time + UltimateAttackTime;
            ClassAttackTime = Settings.Second * Envir.Random.Next(5, 15);
            classAttackTime = Envir.Time + ClassAttackTime;
            LowestHPAttackTime = Settings.Second * Envir.Random.Next(20, 35);
            lowestHPAttackTime = Envir.Time + LowestHPAttackTime;
            DebuffTime = Settings.Second * Envir.Random.Next(3, 10);
            debuffTime = Envir.Time;
            PushTime = Settings.Second * Envir.Random.Next(15, 30);
            pushTime = Envir.Time + PushTime;
            OrderMobTime = Settings.Second * Envir.Random.Next(5, 15);
            orderMobTime = Envir.Time + OrderMobTime;
            //  Every third of a second
            AttackSpeed = 750;
            MoveSpeed = 750;
        }
        #endregion
        protected override void ProcessAI()
        {
            if (Dead) return;
            //  Get the Health percent
            if (PercentHealth <= 25)
                //  Walk away at 25% remaining
                WalkAway = true;
            else
                //  Set it to normal if above 25% remaining
                WalkAway = false;
            if (Envir.Time > buffDuration)
            {
                MaxHP -= HealthBoost;
                Accuracy -= (byte)AccBoost;
                Agility -= (byte)AgilBoost;
                MinAC -= MinACBoost;
                MaxAC -= MaxACBoost;
                MinMAC -= MinMACBoost;
                MaxMAC -= MaxMACBoost;
                MinACBoost = 0;
                MaxACBoost = 0;
                MinDCBoost = 0;
                MaxDCBoost = 0;
                MinMACBoost = 0;
                MaxMACBoost = 0;
                MinMCBoost = 0;
                MaxMCBoost = 0;
                MinSCBoost = 0;
                MaxSCBoost = 0;
                AccBoost = 0;
                AgilBoost = 0;
                HealthBoost = 0;
            }

            #region Blocking Check
            if (Envir.Time > checkBlockTime)
            {
                List<MapObject> list = FindAllNearby(1, CurrentLocation, false);
                int count = 0;
                MirDirection dir = Direction;

                for (int i = 0; i < 8; i++)
                {
                    Point location = Functions.PointMove(CurrentLocation, dir, 1);

                    if (CurrentMap.ValidPoint(location))
                    {
                        Cell cell = CurrentMap.GetCell(location);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            if (!cell.Objects[o].Blocking) continue;
                            count++;
                            break;
                        }
                    }
                    else count++;

                    dir = Functions.NextDir(dir);
                }
                //  Surrounded.
                if (count >= 8)
                    //  Attempt to teleport
                    if (TeleportRandom(10, 4, CurrentMap))
                        //  Next cast time
                        checkBlockTime = Envir.Time + CheckBlockTime;

            }
            #endregion
            ProcessSearch();
            ProcessRoam();
            ProcessTarget();
        }
        public override int Struck(int damage, DefenceType type = DefenceType.ACAgility)
        {
            return 0;
        }

        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            //  If we haven't hit anything for 5 seconds take 0 damage
            if (Envir.Time > _lastHitTime)
                damage = 0;
            return base.Attacked(attacker, damage, type);
        }

        public override int Attacked(PlayerObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            //  If we haven't hit anything for 5 seconds take 0 damage
            if (Envir.Time > _lastHitTime)
                damage = 0;
            return base.Attacked(attacker, damage, type, damageWeapon);
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;
            if (Target.Dead)
            {
                FindTarget();
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            #region Push Attack
            if (Envir.Time > pushTime - (PushTime / 2) &&
                !PushWarn)
            {
                Broadcast(new S.Chat { Type = ChatType.Shout2, Message = string.Format("Be gone already you pesky ants!") });
                PushWarn = true;
            }

            if (Envir.Time > pushTime)
            {
                List<MapObject> list = FindAllTargets(PushAttackRange, CurrentLocation, false);
                if (list != null &&
                    list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].IsAttackTarget(this))
                        {
                            list[i].Attacked(this, GetAttackPower(MinDC + MinDCBoost, MaxDC + MaxDCBoost), DefenceType.Repulsion);
                            MirDirection dir = Functions.DirectionFromPoint(CurrentLocation, list[i].CurrentLocation);
                            list[i].Pushed(this, dir, 5);
                        }
                    }
                }
                _lastHitTime = Envir.Time + _LastHitTime;
                PushTime = Settings.Second * Envir.Random.Next(30, 60);
                pushTime = Envir.Time + PushTime;
                PushWarn = false;
            }
            #endregion
            #region Lowest HP Attack
            else if (Envir.Time > lowestHPAttackTime)
            {
                byte lowestHP = 100;
                MapObject lowestObj = null;
                List<MapObject> list = FindAllTargets(LowestAttackRange, CurrentLocation, false);
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                        if (list[i].IsAttackTarget(this))
                            if (list[i].PercentHealth < lowestHP)
                            {
                                lowestHP = list[i].PercentHealth;
                                lowestObj = list[i];
                            }
                }
                if (lowestObj != null)
                {
                    if (lowestObj.Attacked(this, GetAttackPower(MinSC + MinSCBoost, MaxSC + MaxSCBoost), DefenceType.None) > 0)
                    {
                        if (lowestObj.Race == ObjectType.Player)
                            Broadcast(new S.Chat { Message = string.Format("{0} how does it feel to see true POWER!", lowestObj.Name), Type = ChatType.Shout2 });
                        Broadcast(new S.ObjectAttack { Direction = Direction, Location = CurrentLocation, ObjectID = ObjectID, Type = 4 });
                    }
                }
                LowestHPAttackTime = Settings.Second * Envir.Random.Next(20, 35);
                lowestHPAttackTime = Envir.Time + LowestHPAttackTime;
            }
            #endregion
            #region The Ultimate Attack
            else if (Envir.Time > ultimateAttackTime)
            {
                List<MapObject> list = FindAllTargets(UltimateAttackRange, CurrentLocation, false);
                int hitCount = 0;
                if (list != null && list.Count > 0)
                    for (int i = 0; i < list.Count; i++)
                        if (list[i].IsAttackTarget(this))
                        {
                            if (list[i].Attacked(this, GetAttackPower(MinDC + MinDCBoost, MaxDC + MaxDCBoost), DefenceType.ACAgility) > 0)
                                hitCount++;
                        }
                if (hitCount > 0)
                    Broadcast(new S.ObjectAttack { Direction = Direction, Type = 3, ObjectID = ObjectID, Location = CurrentLocation });
                UltimateAttackTime = Settings.Second * Envir.Random.Next(15, 45);
                ultimateAttackTime = Envir.Time + UltimateAttackTime;
                _lastHitTime = Envir.Time + _LastHitTime;
            }
            #endregion
            #region Order all mobs to current Location, might be funny lol
            else if (Envir.Time > orderMobTime)
            {
                for (int x = CurrentLocation.X - OrderMobRange; x < CurrentLocation.X + OrderMobRange / 2; x++)
                {
                    for (int y = CurrentLocation.Y - OrderMobRange; y < CurrentLocation.Y + OrderMobRange / 2; y++)
                    {
                        Point tmp = new Point(x, y);
                        if (!CurrentMap.ValidPoint(tmp))
                            continue;
                        Cell cell = CurrentMap.GetCell(tmp);
                        if (cell.Objects == null)
                            continue;
                        for (int i = 0; i < cell.Objects.Count; i++)
                            if (cell.Objects[i].Race == ObjectType.Monster)
                            {
                                MonsterObject tmo = (MonsterObject)cell.Objects[i];
                                if (tmo != null &&
                                    !tmo.Dead)
                                {
                                    bool routeExists = false;
                                    //  Check if the route list is valid before trying to add to a null object
                                    if (tmo.Route == null)
                                        //  It was null so we'll create one
                                        tmo.Route = new List<RouteInfo>();
                                    //  Now add the route, not sure what the delay is meant to be..
                                    else
                                    {
                                        if (tmo.Route.Count > 0)
                                        {
                                            for (int o = 0; o < tmo.Route.Count; o++)
                                            {
                                                if (tmo.Route[o].Location == CurrentLocation)
                                                    routeExists = true;
                                            }
                                        }
                                    }
                                    //  If Route already exists (I.E it was already set to the current location)
                                    if (routeExists)
                                        continue;
                                    //  Route wasn't for the Current location so Add a new route to our current location.
                                    tmo.Route.Add(new RouteInfo()
                                    {
                                        Delay = 0,
                                        Location = CurrentLocation
                                    });
                                }
                            }
                    }
                }
                //  Broadcast a message notifying players of any monsters in range coming to aid Mob01
                Broadcast(new S.Chat { Message = string.Format("{0}>Come forth my brothers in arms!", Name), Type = ChatType.Shout2 });
                //  Set the next Order time randomly
                OrderMobTime = Settings.Second * Envir.Random.Next(15, 50);
                //  Now set the order time with the random cool-down
                orderMobTime = Envir.Time + OrderMobTime;
            }
            #endregion
            #region The Class Attack
            else if (Envir.Time > classAttackTime)
            {
                MirClass lastClass = MirClass.Wizard;
                int repeatCount = 0;
                REPEAT:
                List<MapObject> list = FindAllTargets(ClassAttackRange, CurrentLocation, false);
                if (list != null &&
                    list.Count > 0)
                {
                    for (int i = list.Count - 1; i > 0; i--)
                    {
                        if (list[i].IsAttackTarget(this))
                        {
                            if (list[i].Race == ObjectType.Player)
                            {
                                PlayerObject temp = (PlayerObject)list[i];
                                if (temp.Class != lastClass)
                                    list.RemoveAt(i);
                            }
                        }
                    }
                }
                if (repeatCount < 5)
                {
                    repeatCount++;
                    if (list != null &&
                        list.Count > 0)
                    {
                        list[0].Attacked(this, GetAttackPower(MinMC + MinMCBoost, MaxMC + MaxMCBoost), DefenceType.MAC);
                        _lastHitTime = Envir.Time + _LastHitTime;
                    }
                    else
                    {
                        if (lastClass == MirClass.Wizard)
                            lastClass = MirClass.Taoist;
                        else if (lastClass == MirClass.Taoist)
                            lastClass = MirClass.Assassin;
                        else if (lastClass == MirClass.Assassin)
                            lastClass = MirClass.Warrior;
                        else if (lastClass == MirClass.Warrior)
                            lastClass = MirClass.Archer;
                        else if (lastClass == MirClass.Archer)
                            lastClass = MirClass.Wizard;
                        goto REPEAT;
                    }
                }
                ClassAttackTime = Settings.Second * Envir.Random.Next(5, 15);
                classAttackTime = Envir.Time + ClassAttackTime;
            }
            #endregion
            #region The Debuff Attack
            else if (Envir.Time > debuffTime)
            {
                List<MapObject> list = FindAllTargets(DebuffAttackRange, CurrentLocation, false);
                DebuffTime = Settings.Second * Envir.Random.Next(3, 10);
                BuffDuration = Settings.Second * Envir.Random.Next(5, 8);
                if (BuffDuration > DebuffTime)
                    DebuffTime = BuffDuration;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].IsAttackTarget(this))
                    {
                        if (list[i].Attacked(this, GetAttackPower(MinSC, MaxSC), DefenceType.ACAgility) > 0)
                        {
                            list[i].AddBuff(new Buff { Caster = this, Type = BuffType.WonderDrug, ExpireTime = Envir.Time + DebuffTime, ObjectID = list[i].ObjectID, Values = new int[] { 2, 3, 1, 5, 25 } });
                            MinDCBoost += 1;
                            MaxDCBoost += 5;
                            MinMCBoost += 1;
                            MaxMCBoost += 5;
                            MinSCBoost += 1;
                            MaxSCBoost += 5;
                            MinMACBoost += 1;
                            MaxMACBoost += 5;
                            MinACBoost += 1;
                            MaxACBoost += 5;
                            AgilBoost += 2;
                            AccBoost += 3;
                            HealthBoost += 25;
                        }
                    }
                }
                buffDuration = Envir.Time + BuffDuration;
                debuffTime = Envir.Time + DebuffTime;
                _lastHitTime = Envir.Time + _LastHitTime;
            }
            #endregion
            #region SpellObject Spawning
            else if (Envir.Time > randomMapAttackTime)
            {
                List<Point> locations = new List<Point>();
                RandomMapAttackTime = Settings.Second * Envir.Random.Next(8, 23);
                for (int i = 0; i < 32; i++)
                {
                    Point loc = GetRandomPoint(15, RandomMapAttackRange, CurrentMap);
                    if (CurrentMap.ValidPoint(loc))
                        locations.Add(loc);
                }
                for (int i = 0; i < locations.Count; i++)
                {
                    //                                                                                                          Damage                                                  Location        Duration
                    DelayedAction spell = new DelayedAction(DelayedType.MonsterMagic, Envir.Time + 800, this, Spell.SpecialMob, GetAttackPower(MinMC + MinMCBoost, MaxMC + MaxMCBoost), locations[i], RandomMapAttackTime);
                    CurrentMap.ActionList.Add(spell);
                }
                
                randomMapAttackTime = Envir.Time + RandomMapAttackTime;
            }
            #endregion
            #region Normal Attacks
            else if (InAttackRange() && !WalkAway)
            {
                Target.Attacked(this, GetAttackPower(MinDC, MaxDC), DefenceType.ACAgility);
                Broadcast(new S.ObjectAttack { Type = 0, Direction = Direction, Location = CurrentLocation, ObjectID = ObjectID });
                _lastHitTime = Envir.Time + _LastHitTime;
            }
            else if (InAttackRange() && WalkAway)
            {
                //  Range attack once < 25% health remaining
                Target.Attacked(this, GetAttackPower(MinMC, MaxMC), DefenceType.MACAgility);
                Broadcast(new S.ObjectAttack { Type = 1, Direction = Direction, Location = CurrentLocation, ObjectID = ObjectID });
                _lastHitTime = Envir.Time + _LastHitTime;
            }
            #endregion
            #region Check if Target died, find a new target
            if (Target.Dead)
            {
                FindTarget();
                return;
            }
            #endregion
            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }
            #region Walk away behaviour
            if (WalkAway)
            {
                int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);
                if (dist >= 13)
                    MoveTo(Target.CurrentLocation);
                else
                {
                    MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                    if (Walk(dir)) return;

                    switch (Envir.Random.Next(2)) //No favour
                    {
                        case 0:
                            for (int i = 0; i < 7; i++)
                            {
                                dir = Functions.NextDir(dir);

                                if (Walk(dir))
                                    return;
                            }
                            break;
                        default:
                            for (int i = 0; i < 7; i++)
                            {
                                dir = Functions.PreviousDir(dir);

                                if (Walk(dir))
                                    return;
                            }
                            break;
                    }

                }
            }
            else
                MoveTo(Target.CurrentLocation);
            #endregion
        }
        /// <summary>
        /// Override the Range check to allow the behaviour of walking away at 25% hp remaining
        /// </summary>
        /// <returns></returns>
        protected override bool InAttackRange()
        {
            int dist = WalkAway ? 14 : 1;
            if (Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) > dist)
                return false;
            else
                return true;
        }
    }
}
