using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom
{
    public class RaidMap_Info
    {

        public int Index;

        public string Title = string.Empty;
        public string Description = string.Empty;
        /// <summary>
        /// The waitin area (map)
        /// </summary>
        public int LobbyIndex;
        /// <summary>
        /// The Spawn locations of the Boss and Sub bosses.
        /// </summary>
        public Point[] BossAreas = new Point[3];
        /// <summary>
        /// The start area for players
        /// </summary>
        public Point StartLocation;
        /// <summary>
        /// The Bosses mob Index
        /// </summary>
        public int BossIndex;
        /// <summary>
        /// The Sub Bosses Index (0)
        /// </summary>
        public int Sub0Index;
        /// <summary>
        /// The Sub Bosses Index (1)
        /// </summary>
        public int Sub1Index;
        /// <summary>
        /// The Map which the Raid runs on.
        /// </summary>
        public int MapIndex;
        /// <summary>
        /// The rewards to distribute.
        /// </summary>
        public List<RaidItem_Info> ItemRewards = new List<RaidItem_Info>();

        /// <summary>
        /// The day the Raid starts on
        /// </summary>
        public byte StartDay;
        /// <summary>
        /// The time it starts at
        /// </summary>
        public byte StartHour, StartMinute;
        /// <summary>
        /// The duration of the Raid
        /// </summary>
        public byte Duration;
        /// <summary>
        /// If the player(s) have one life (they die, they cannot return)
        /// </summary>
        public bool OneLife;

        public RaidMap_Info()
        {

        }

        public RaidMap_Info(BinaryReader reader)
        {
            Index = reader.ReadInt32();
            Title = reader.ReadString();
            Description = reader.ReadString();
            LobbyIndex = reader.ReadInt32();
            BossAreas[0] = new Point(reader.ReadInt32(), reader.ReadInt32());
            BossAreas[1] = new Point(reader.ReadInt32(), reader.ReadInt32());
            BossAreas[2] = new Point(reader.ReadInt32(), reader.ReadInt32());
            StartLocation = new Point(reader.ReadInt32(), reader.ReadInt32());
            BossIndex = reader.ReadInt32();
            Sub0Index = reader.ReadInt32();
            Sub1Index = reader.ReadInt32();
            MapIndex = reader.ReadInt32();
            StartDay = reader.ReadByte();
            StartHour = reader.ReadByte();
            StartMinute = reader.ReadByte();
            Duration = reader.ReadByte();
            OneLife = reader.ReadBoolean();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                ItemRewards.Add(new RaidItem_Info(reader));
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(Title);
            writer.Write(Description);
            writer.Write(LobbyIndex);
            writer.Write(BossAreas[0].X);
            writer.Write(BossAreas[0].Y);
            writer.Write(BossAreas[1].X);
            writer.Write(BossAreas[1].Y);
            writer.Write(BossAreas[2].X);
            writer.Write(BossAreas[2].Y);
            writer.Write(StartLocation.X);
            writer.Write(StartLocation.Y);
            writer.Write(BossIndex);
            writer.Write(Sub0Index);
            writer.Write(Sub1Index);
            writer.Write(MapIndex);
            writer.Write(StartDay);
            writer.Write(StartHour);
            writer.Write(StartMinute);
            writer.Write(Duration);
            writer.Write(OneLife);
            writer.Write(ItemRewards.Count);
            for (int i = 0; i < ItemRewards.Count; i++)
                ItemRewards[i].Save(writer);
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}", Index, Title);
        }
    }

    public class RaidItem_Info
    {
        /// <summary>
        /// Original Item
        /// </summary>
        public ItemInfo Info;
        /// <summary>
        /// Overridden stats
        /// </summary>
        public ushort MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC;

        public RaidItem_Info()
        {

        }

        public RaidItem_Info(BinaryReader reader)
        {
            bool valid = reader.ReadBoolean();
            if (valid)
            {
                Info = new ItemInfo(reader);
            }
            MinAC = reader.ReadUInt16();
            MaxAC = reader.ReadUInt16();
            MinMAC = reader.ReadUInt16();
            MaxMAC = reader.ReadUInt16();
            MinDC = reader.ReadUInt16();
            MaxDC = reader.ReadUInt16();
            MinMC = reader.ReadUInt16();
            MaxMC = reader.ReadUInt16();
            MinSC = reader.ReadUInt16();
            MaxSC = reader.ReadUInt16();
        }

        public void Save(BinaryWriter writer)
        {
            if (Info != null)
            {
                writer.Write(true);
                Info.Save(writer);
            }
            else
                writer.Write(false);
            writer.Write(MinAC);
            writer.Write(MaxAC);
            writer.Write(MinMAC);
            writer.Write(MaxMAC);
            writer.Write(MinDC);
            writer.Write(MaxDC);
            writer.Write(MinMC);
            writer.Write(MaxMC);
            writer.Write(MinSC);
            writer.Write(MaxSC);
        }

        public override string ToString()
        {
            return Info.ToString();
        }
    }

    public class RaidMap
    {

        private static Envir Envir
        {
            get { return SMain.Envir; }
        }

        public RaidMap_Info Info;
        public Map CurrentMap;
        public Map LobbyMap;
        public bool Started = false;
        public List<RaidStats> PlayerStats = new List<RaidStats>();

        public RaidMap(Map map, Map lobby)
        {
            CurrentMap = map;
            LobbyMap = lobby;
        }

        public void Process()
        {
            if (!Started && CanStartRaid())
            {
                StartRaid();
            }
            if (Started &&
                (DateTime.Now > DateTime.Now.AddMinutes(Info.Duration) || (CurrentMap.Players.Count == 0 && Info.OneLife)))
            {
                EndRaid();
            }
        }
        
        public void EndRaid(bool finished = false)
        {
            //  End the raid
            Started = false;
            if (CurrentMap.Players.Count > 0)
            {
                int[] rewards = new int[CurrentMap.Players.Count];
                for (int i = 0; i < rewards.Length; i++)
                {
                    rewards[i] = Envir.Random.Next(0, Info.ItemRewards.Count);
                }
                PlayerStats = PlayerStats.OrderBy(o => o.DamageDealt).ToList();
                PlayerStats.Reverse();
                for (int i = CurrentMap.Players.Count - 1; i >= 0; i--)
                {
                    if (!CurrentMap.Players[i].Dead)
                    {
                        if (finished)
                        {
                            for (int x = 0; x < PlayerStats.Count; x++)
                            {
                                if (rewards[x] == -1)
                                    continue;
                                if (PlayerStats[x].PlayerName == CurrentMap.Players[i].Name)
                                {
                                    UserItem item = CreateRaidItem(rewards[x], CurrentMap.Players[i].Class);
                                    List<UserItem> items = new List<UserItem>
                                    {
                                        item
                                    };
                                    if (item != null)
                                    {
                                        rewards[x] = -1;
                                        MailInfo mail = new MailInfo(CurrentMap.Players[i].Info.Index)
                                        {
                                            Items = items,
                                            Message = string.Format("You were awarded {0} for your efforts in the raid", item.FriendlyName),
                                            MailID = ++Envir.NextMailID,
                                            Sender = string.Format("Raid{0}", Info.Index)
                                        };
                                        mail.Send();
                                    }
                                }
                            }
                            CurrentMap.Players[i].ReceiveChat(string.Format("All monsters have been banished!"), ChatType.System);
                        }
                        CurrentMap.Players[i].Teleport(Envir.GetMap(CurrentMap.Players[i].BindMapIndex), CurrentMap.Players[i].BindLocation);
                    }
                }
            }
            Envir.Broadcast(new ServerPackets.Chat { Message = string.Format("{0} raid has finished!", Info.Title) });
        }

        public void StartRaid()
        {
            if (CurrentMap == null ||
                LobbyMap == null)
                return;
            
            //  Get the Mob info for sub0
            MonsterInfo mInfo = Envir.GetMonsterInfo(Info.Sub0Index);
            //  Check it's not null
            if (mInfo != null)
            {
                //  Get a Monster object
                MonsterObject mob = MonsterObject.GetMonster(mInfo);
                //  Check it's not null
                if (mob != null)
                {
                    //  Set a default direction
                    mob.Direction = MirDirection.DownRight;
                    //  Set it's action time
                    mob.ActionTime = Envir.Time + 1000;
                    mob.Raid = this;
                    mob.IsRaidBoss = true;
                    //  Inital attempt to spawn
                    bool spawned = mob.Spawn(CurrentMap, Info.BossAreas[0]);
                    //  Check if it's not been spawned
                    if (!spawned)
                        //  Loop through locations in a 10x10 square area
                        for (int x = Info.BossAreas[0].X - 5; x < Info.BossAreas[0].X + 5; x++)
                        {
                            //  Stop if it's spawned already
                            if (spawned)
                                continue;
                            for (int y = Info.BossAreas[0].Y - 5; y < Info.BossAreas[0].Y; y++)
                            {
                                //  Attempt to spawn the sub0
                                if (mob.Spawn(CurrentMap, new Point(x, y)))
                                    //  It successfully spawned.
                                    spawned = true;
                            }
                        }
                    if (spawned)
                    {
                        //  Loop through the players on the lobby map
                        for (int i = LobbyMap.Players.Count - 1; i >= 0; i--)
                        {
                            //  Notify how logn they have.
                            LobbyMap.Players[i].ReceiveChat(string.Format("You have {0} minutes to complete the raid!", Info.Duration), ChatType.Announcement);
                            LobbyMap.Players[i].ReceiveChat(string.Format("You must defeat {0} located at X:{1} Y:{2} before proceeding to the next boss.", mob.Info.GameName, mob.CurrentLocation.X, mob.CurrentLocation.Y), ChatType.Hint);
                            //  Teleport them in within a distance of 12
                            LobbyMap.Players[i].TeleportRandom(100, 12, CurrentMap);
                        }
                        Envir.Broadcast(new ServerPackets.Chat { Message = string.Format("{0} raid has started!", Info.Title), Type = ChatType.Announcement });
                        Started = true;
                    }
                }
            }
        }

        public bool CanStartRaid()
        {
            if (Info.StartDay != 7)
            {
                if (DateTime.Now.DayOfWeek == (DayOfWeek)Info.StartDay)
                {
                    if (DateTime.Now.Hour == Info.StartHour &&
                        DateTime.Now.Minute == Info.StartMinute &&
                        !Started)
                    {
                        return true;
                    }
                }
            }
            else if (DateTime.Now.Hour == Info.StartHour &&
                    DateTime.Now.Minute == Info.StartMinute &&
                    !Started)
            {
                return true;
            }

            return false;
        }

        public UserItem CreateRaidItem(int index, MirClass mirClass)
        {
            UserItem item = Envir.CreateFreshItem(Info.ItemRewards[index].Info);
            if (item != null)
            {
                switch (mirClass)
                {
                    case MirClass.Taoist:
                        if (item.Info.Type == ItemType.Armour)
                        {
                            item.MinAC = Info.ItemRewards[index].MinAC;
                            item.MaxAC = Info.ItemRewards[index].MaxAC;
                            item.MinMAC = Info.ItemRewards[index].MinMAC;
                            item.MaxMAC = Info.ItemRewards[index].MaxMAC;
                        }
                        else if (item.Info.Type == ItemType.Weapon)
                        {
                            item.MinSC = Info.ItemRewards[index].MinSC;
                            item.MaxSC = Info.ItemRewards[index].MaxSC;
                        }
                        break;
                    case MirClass.Warrior:
                        if (item.Info.Type == ItemType.Armour)
                        {
                            item.MinAC = Info.ItemRewards[index].MinAC;
                            item.MaxAC = Info.ItemRewards[index].MaxAC;
                            item.MinMAC = Info.ItemRewards[index].MinMAC;
                            item.MaxMAC = Info.ItemRewards[index].MaxMAC;
                        }
                        else if (item.Info.Type == ItemType.Weapon)
                        {
                            item.MinDC = Info.ItemRewards[index].MinDC;
                            item.MaxDC = Info.ItemRewards[index].MaxDC;
                        }
                        break;
                    case MirClass.Wizard:
                        if (item.Info.Type == ItemType.Armour)
                        {
                            item.MinAC = Info.ItemRewards[index].MinAC;
                            item.MaxAC = Info.ItemRewards[index].MaxAC;
                            item.MinMAC = Info.ItemRewards[index].MinMAC;
                            item.MaxMAC = Info.ItemRewards[index].MaxMAC;
                        }
                        else if (item.Info.Type == ItemType.Weapon)
                        {
                            item.MinMC = Info.ItemRewards[index].MinMC;
                            item.MaxMC = Info.ItemRewards[index].MaxMC;
                        }
                        break;
                    case MirClass.Assassin:
                        if (item.Info.Type == ItemType.Armour)
                        {
                            item.MinAC = Info.ItemRewards[index].MinAC;
                            item.MaxAC = Info.ItemRewards[index].MaxAC;
                            item.MinMAC = Info.ItemRewards[index].MinMAC;
                            item.MaxMAC = Info.ItemRewards[index].MaxMAC;
                        }
                        else if (item.Info.Type == ItemType.Weapon)
                        {
                            item.MinDC = Info.ItemRewards[index].MinDC;
                            item.MaxDC = Info.ItemRewards[index].MaxDC;
                        }
                        break;
                    case MirClass.Archer:
                        if (item.Info.Type == ItemType.Armour)
                        {
                            item.MinAC = Info.ItemRewards[index].MinAC;
                            item.MaxAC = Info.ItemRewards[index].MaxAC;
                            item.MinMAC = Info.ItemRewards[index].MinMAC;
                            item.MaxMAC = Info.ItemRewards[index].MaxMAC;
                        }
                        else if (item.Info.Type == ItemType.Weapon)
                        {
                            item.MinMC = Info.ItemRewards[index].MinMC;
                            item.MaxMC = Info.ItemRewards[index].MaxMC;
                        }
                        break;
                }
            }
            return item;
        }
    }

    public class RaidStats
    {
        public string PlayerName;
        public long DamageDealt;
    }
}
