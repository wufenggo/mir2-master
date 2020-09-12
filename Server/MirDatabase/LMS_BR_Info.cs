using Server.MirEnvir;
using Server.MirObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirDatabase
{
    public class LMS_BR_Info
    {

        public int Index;

        /// <summary>
        ///
        /// 0 Indicates Sunday.
        /// 1 Indicates Monday.
        /// 2 Indicates Tuesday.
        /// 3 Indicates Wednesday.
        /// 4 Indicates Thursday.
        /// 5 Indicates Friday.
        /// 6 Indicates Saturday.
        /// </summary>
        public byte StartDay;
        /// <summary>
        /// 0~23
        /// </summary>
        public byte StartHour;
        /// <summary>
        /// 0~60
        /// </summary>
        public byte StartMinute;
        /// <summary>
        /// In minutes
        /// </summary>
        public byte Duration;
        /// <summary>
        /// The Centre of the map ideally.
        /// </summary>
        public Point StartingLocation;
        /// <summary>
        /// The initial Size of the circle
        /// </summary>
        public ushort StartingSize;

        public ushort StartingRange = 200;

        public ushort MinLevel;
        public ushort MaxLevel;

        public List<LMS_RewardInfo> Rewards = new List<LMS_RewardInfo>();

        public LMS_BR_Info()
        {

        }

        public LMS_BR_Info(BinaryReader reader, int version = int.MaxValue)
        {
            Index = reader.ReadInt32();
            StartDay = reader.ReadByte();
            StartHour = reader.ReadByte();
            StartMinute = reader.ReadByte();
            Duration = reader.ReadByte();
            StartingLocation = new Point(reader.ReadInt32(), reader.ReadInt32());
            StartingSize = reader.ReadUInt16();
            StartingRange = reader.ReadUInt16();
            MinLevel = reader.ReadUInt16();
            MaxLevel = reader.ReadUInt16();
            if (Envir.LoadVersion > 146)
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                    Rewards.Add(new LMS_RewardInfo(reader));
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(StartDay);
            writer.Write(StartHour);
            writer.Write(StartMinute);
            writer.Write(Duration);
            writer.Write(StartingLocation.X);
            writer.Write(StartingLocation.Y);
            writer.Write(StartingSize);
            writer.Write(StartingRange);
            writer.Write(MinLevel);
            writer.Write(MaxLevel);
            writer.Write(Rewards.Count);
            for (int i = 0; i < Rewards.Count; i++)
                Rewards[i].Save(writer);
        }

        public override string ToString()
        {
            return string.Format("[{0}]Lv {1} - {2}", Index, MinLevel, MaxLevel);
        }
    }

    public class LMS_BR
    {

        private static Envir Envir
        {
            get { return SMain.Envir; }
        }

        /// <summary>
        /// The Database Values
        /// </summary>
        public LMS_BR_Info Info;
        /// <summary>
        /// The Map the match is run on.
        /// </summary>
        public Map CurrentMap;
        /// <summary>
        /// Indicates if it's started or not
        /// </summary>
        public bool Started = false;
        /// <summary>
        /// The current Size of the Circle.
        /// </summary>
        public ushort CurrentSize;
        /// <summary>
        /// Start Size = Start Size - 20%
        /// </summary>
        public ushort NextSize { get { return (byte)(CurrentSize - (Info.StartingSize * 20 / 100)); } }
        /// <summary>
        /// Finishing requires the Time to reach the limit or one player remaining
        /// </summary>
        public bool Finished = false;

        /// <summary>
        /// The original number of players within the match
        /// </summary>
        public byte OriginalPlayercount;
        /// <summary>
        /// The Current Player count
        /// </summary>
        public byte PlayerCount { get { return GetAlivePlayers(); } }
        /// <summary>
        /// The starting time of the match
        /// </summary>
        public long StartTime;

        /// <summary>
        /// The end time of the match, used if there are still more than 1 player on the map.
        /// </summary>
        public long EndTime;

        public long ShrinkTime;

        public long NextMessageTime;

        public bool InitialMessage;

        public long NextRemainingPlayerMsgTime;

        public List<LMS_Rank> PlayerRanks = new List<LMS_Rank>();

        public Point StartingLocation = new Point();
        public List<Point> CircleLocations = new List<Point>();

        public List<PlayerObject> SignedupPlayers = new List<PlayerObject>();

        public long NextDamageTime;

        public byte Stage = 0;
        public bool tempStarted = false;
        public LMS_BR(LMS_BR_Info info, Map map)
        {
            CurrentMap = map;
            Info = info;
        }


        public void Load_LMS_BR()
        {

        }


        public byte[] RankingPoints = new byte[]
        {
            50, //  Rank1
            30, //  Rank2
            15, //  Rank3
            10, //  Rank4
            10, //  Rank5
            10, //  Rank6
            10, //  Rank7
            10, //  Rank8
            10, //  Rank9
            5, //  Rank10
        };

        public void AnnounceCountDown(List<string> exemptPlayers, bool fivemin = false)
        {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Info.StartHour, Info.StartMinute, 0);
            for (int i = 0; i < Envir.Players.Count; i++)
            {
                PlayerObject player = Envir.Players[i];
                if (player.Connection == null ||
                    player.Connection.Stage != MirNetwork.GameStage.Game)
                    continue;
                bool sendMessage = true;
                if (exemptPlayers != null && exemptPlayers.Count > 0)
                    for (int x = 0; x < exemptPlayers.Count; x++)
                        if (exemptPlayers[x] == player.Name)
                            sendMessage = false;
                if (sendMessage)
                {
                    TimeSpan span = startTime.Subtract(DateTime.Now);
                    player.ReceiveChat(string.Format("{0} remaining to sign up to LMS!", span.Hours > 0 ? string.Format("{0:D1} hour{3} {1:D2} minutes", span.Hours, span.Minutes, span.Hours > 1 ? "s" : "") : string.Format("{0:D1} minutes", span.Minutes)), ChatType.Announcement);
                }
            }
        }

        public void Process()
        {
            if (Info.StartDay != 7 &&
                (DayOfWeek)Info.StartDay == DateTime.Now.DayOfWeek)
            {
                DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,  DateTime.Now.Day, Info.StartHour, Info.StartMinute, 0);
                DateTime hourTime = startTime.AddHours(-1);
                DateTime minuteTime = startTime.AddMinutes(-30);
                DateTime fifteen = startTime.AddMinutes(-15);
                DateTime five = startTime.AddMinutes(-5);
                if (((DateTime.Now >= hourTime && DateTime.Now <= hourTime.AddSeconds(30)) ||
                    (DateTime.Now >= minuteTime && DateTime.Now <= minuteTime.AddSeconds(30)) ||
                    (DateTime.Now >= fifteen && DateTime.Now <= fifteen.AddSeconds(30)) ||
                    (DateTime.Now >= five && DateTime.Now <= five.AddSeconds(30))) &&
                     Envir.Time > NextMessageTime)
                {
                    NextMessageTime = Envir.Time + Settings.Minute;
                    List<string> listedPlayers = new List<string>();
                    if (SignedupPlayers != null &&
                        SignedupPlayers.Count > 0)
                    {

                        for (int i = 0; i < SignedupPlayers.Count; i++)
                        {
                            if (SignedupPlayers[i].Connection == null ||
                                SignedupPlayers[i].Connection.Stage != MirNetwork.GameStage.Game)
                                continue;
                            listedPlayers.Add(SignedupPlayers[i].Name);
                            SignedupPlayers[i].ReceiveChat(string.Format("LMS will start in {0} minutes",
                                DateTime.Now == hourTime ? "60" :
                                DateTime.Now == minuteTime ? "30" :
                                DateTime.Now == fifteen ? "15" : "5"), ChatType.System);
                        }

                    }
                    AnnounceCountDown(listedPlayers, DateTime.Now >= five && DateTime.Now <= five.AddSeconds(30));
                }
            }
            else if (Info.StartDay == 7)
            {
                DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Info.StartHour, Info.StartMinute, 0);
                DateTime hourTime = startTime.AddHours(-1);
                DateTime minuteTime = startTime.AddMinutes(-30);
                DateTime fifteen = startTime.AddMinutes(-15);
                DateTime five = startTime.AddMinutes(-5);
                if (((DateTime.Now >= hourTime && DateTime.Now <= hourTime.AddSeconds(30)) ||
                    (DateTime.Now >= minuteTime && DateTime.Now <= minuteTime.AddSeconds(30)) ||
                    (DateTime.Now >= fifteen && DateTime.Now <= fifteen.AddSeconds(30)) ||
                    (DateTime.Now >= five && DateTime.Now <= five.AddSeconds(30))) &&
                     Envir.Time > NextMessageTime)
                {
                    NextMessageTime = Envir.Time + Settings.Minute;
                    List<string> listedPlayers = new List<string>();
                    if (SignedupPlayers != null &&
                        SignedupPlayers.Count > 0)
                    {

                        for (int i = 0; i < SignedupPlayers.Count; i++)
                        {
                            if (SignedupPlayers[i].Connection == null ||
                                SignedupPlayers[i].Connection.Stage != MirNetwork.GameStage.Game)
                                continue;
                            listedPlayers.Add(SignedupPlayers[i].Name);
                            SignedupPlayers[i].ReceiveChat(string.Format("LMS will start in {0} minutes",
                                DateTime.Now == hourTime ? "60" :
                                DateTime.Now == minuteTime ? "30" :
                                DateTime.Now == fifteen ? "15" : "5"), ChatType.System);
                        }

                    }
                    AnnounceCountDown(listedPlayers, DateTime.Now >= five && DateTime.Now <= five.AddSeconds(30));
                }
            }
            
            

            if (Info.StartDay == 7 &&
                DateTime.Now.Hour == Info.StartHour &&
                DateTime.Now.Minute == Info.StartMinute &&
                !Started && !tempStarted)
            {
                tempStarted = true;
                SMain.EnqueueDebugging(string.Format("[LMS BR] {0} Started at {1}:{2}", CurrentMap.Info.Title, Info.StartHour, Info.StartMinute));
                Start_Match();
                return;
            }
            else if ((byte)DateTime.Now.DayOfWeek == Info.StartDay &&
                DateTime.Now.Hour == Info.StartHour &&
                DateTime.Now.Minute == Info.StartMinute &&
                !Started && !tempStarted)
            {
                tempStarted = true;
                SMain.EnqueueDebugging(string.Format("[LMS BR] {0} Started at {1}:{2} on {3}", CurrentMap.Info.Title, Info.StartHour, Info.StartMinute, (DayOfWeek)Info.StartDay));
                Start_Match();
                return;
            }
            else if (DateTime.Now.Hour != Info.StartHour &&
                DateTime.Now.Minute != Info.StartMinute)
                tempStarted = false;
                
                

            if (Started && Stage < 4)
            {
                if (CurrentMap.Players.Count == 0)
                {
                    End_Match();
                    return;
                }
                if (GetAlivePlayers() == 1)
                {
                    End_Match(true);
                    return;
                }
                else
                {
                    if (Envir.Time >= NextRemainingPlayerMsgTime)
                    {
                        SMain.EnqueueDebugging(string.Format("[LMS BR] {0} {1}/{2} Players remaining", CurrentMap.Info.Title, PlayerCount, OriginalPlayercount));
                        NextRemainingPlayerMsgTime = Envir.Time + (Settings.Second * 90);
                        BroadcastMessage(string.Format("{0}/{1} players remaining", PlayerCount, OriginalPlayercount), ChatType.Hint, 2);
                    }
                    if (Envir.Time >= NextMessageTime)
                    {
                        if (ShrinkTime - Settings.Second * 60 == Envir.Time)
                        {
                            NextMessageTime = Settings.Second * 11;
                            long temp = ShrinkTime - Envir.Time;
                            string remainingTime = Functions.PrintTimeSpanFromSeconds(temp / 1000);
                            SMain.EnqueueDebugging(string.Format("[LMS BR] {0} Circle will Shrink in {1}", CurrentMap.Info.Title, remainingTime));
                            BroadcastMessage(string.Format("The circle will shrink in {0}", remainingTime), ChatType.Announcement, 2);
                        }
                        else if (ShrinkTime - Settings.Second * 30 == Envir.Time)
                        {
                            CreateInnerCircle();
                            NextMessageTime = Settings.Second * 11;
                            long temp = ShrinkTime - Envir.Time;
                            string remainingTime = Functions.PrintTimeSpanFromSeconds(temp / 1000);
                            SMain.EnqueueDebugging(string.Format("[LMS BR] {0} Circle will Shrink in {1}", CurrentMap.Info.Title, remainingTime));
                            BroadcastMessage(string.Format("The circle will shrink in {0}", remainingTime), ChatType.Announcement, 2);
                        }
                    }
                    if (Envir.Time >= ShrinkTime)
                    {
                        Stage++;
                        float tempDuration = Info.Duration;
                        switch (Stage)
                        {
                            case 1:
                                ShrinkTime = Envir.Time + (long)(tempDuration / 4 * Settings.Minute);
                                CurrentSize = (byte)(CurrentSize - (Info.StartingSize * 20 / 100));
                                break;
                            case 2:
                                CurrentSize = (byte)(CurrentSize - (Info.StartingSize * 20 / 100));
                                ShrinkTime = Envir.Time + (long)(tempDuration / 8 * Settings.Minute);
                                break;
                            case 3:
                                CurrentSize = (byte)(CurrentSize - (Info.StartingSize * 20 / 100));
                                ShrinkTime = Envir.Time + (long)(tempDuration / 8 * Settings.Minute);
                                break;
                        }
                        if (Stage < 4)
                        {
                            CreateCircle(CurrentSize);
                            CreateInnerCircle();
                            NextMessageTime = ShrinkTime - Settings.Second * 60;
                            long temp = ShrinkTime - Envir.Time;
                            string remainingTime = Functions.PrintTimeSpanFromSeconds(temp / 1000);
                            SMain.EnqueueDebugging(string.Format("[LMS BR] Circle will Shrink again in {0}", CurrentMap.Info.Title, remainingTime));
                            BroadcastMessage(string.Format("The circle will shrink again in {0}", remainingTime), ChatType.Announcement, 2);
                        }
                    }
                    else
                    {
                        if (Envir.Time > NextDamageTime)
                        {
                            for (int i = 0; i < CurrentMap.Players.Count; i++)
                            {
                                if (!InLMSRange(CurrentMap.Players[i].CurrentLocation))
                                {
                                    if (CurrentMap.Players[i].Dead)
                                        continue;
                                    CurrentMap.Players[i].LMS_DamageAccumulator = CurrentMap.Players[i].LMS_DamageAccumulator == 0 ? -5 : CurrentMap.Players[i].LMS_DamageAccumulator * 2;
                                    CurrentMap.Players[i].LMS_InflictDamage();
                                }
                                else if (CurrentMap.Players[i].LMS_DamageAccumulator != 0)
                                    CurrentMap.Players[i].LMS_DamageAccumulator = 0;
                            }
                            NextDamageTime = Envir.Time + Settings.Second;
                        }
                    }
                }
            }
            else if (Finished || Stage == 4)
            {
                End_Match();
            }
        }


        public bool InLMSRange(Point location)
        {
            bool hasPoint = false;
            hasPoint = Math.Sqrt(Math.Pow(location.X - StartingLocation.X, 2) + Math.Pow(StartingLocation.Y - location.Y, 2)) <= CurrentSize;
            if (!hasPoint)
            {
                for (int i = 0; i < CircleLocations.Count; i++)
                {
                    if (CircleLocations[i] == location)
                        hasPoint = true;
                }
            }
            return hasPoint;
        }

        public bool InLMSCircle(Point location)
        {
            bool hasPoint = false;
            double x = location.X;
            double y = location.Y;
            hasPoint = Math.Pow((x - StartingLocation.X), CurrentSize) + Math.Pow((y - StartingLocation.Y), CurrentSize) <= Math.Pow(CurrentSize, CurrentSize);
            if (!hasPoint)
            {
                for (int i = 0; i < CircleLocations.Count; i++)
                {
                    if (CircleLocations[i] == location)
                        hasPoint = true;
                }
            }
            return hasPoint;
        }

        public void SignUp(PlayerObject player, byte selection)
        {
            SMain.EnqueueDebugging(string.Format("[LMS BR] {0} Player {1} signing up to {2}", CurrentMap.Info.Title, player.Name, selection));
            if (Started || Finished)
                return;
            if (player.Level < Info.MinLevel ||
                player.Level > Info.MaxLevel)
                return;
            if (Info.Index != selection)
                return;
            if (SignedupPlayers == null)
                SignedupPlayers = new List<PlayerObject>();
            bool playerFound = false;
            if (SignedupPlayers.Count > 0)
            {
                for (int i = 0; i < SignedupPlayers.Count; i++)
                {
                    if (SignedupPlayers[i].Name == player.Name)
                        playerFound = true;
                }
                if (playerFound)
                {
                    SignedupPlayers.Remove(player);
                    return;
                }
                else
                {
                    SignedupPlayers.Add(player);
                    return;
                }
            }
            else
                SignedupPlayers.Add(player);
            return;
        }

        public Point GetRandomPointInCircle()
        {
            Point returnPoint = StartingLocation;
            for (int i = 0; i < 1;)
            {
                returnPoint = new Point(Envir.Random.Next(returnPoint.X - CurrentSize, returnPoint.X + CurrentSize), Envir.Random.Next(returnPoint.Y - CurrentSize, returnPoint.Y + CurrentSize));
                if (InLMSRange(returnPoint))
                    i++;
            }
            return returnPoint;
        }

        public void Start_Match()
        {
            if (CurrentMap == null)
                return;
            if (SignedupPlayers == null ||
                SignedupPlayers.Count == 0)
                return;
            StartingLocation = new Point(
                Envir.Random.Next(Info.StartingLocation.X - Info.StartingRange <= 0 ? 1 : Info.StartingLocation.X - Info.StartingRange, Info.StartingLocation.X + Info.StartingRange >= CurrentMap.Width ? CurrentMap.Width - 1 : Info.StartingLocation.X + Info.StartingRange),
                Envir.Random.Next(Info.StartingLocation.Y - Info.StartingRange <= 0 ? 1 : Info.StartingLocation.Y - Info.StartingRange, Info.StartingLocation.Y + Info.StartingRange >= CurrentMap.Height ? CurrentMap.Height - 1 : Info.StartingLocation.Y + Info.StartingRange));
            SMain.EnqueueDebugging(string.Format("[LMS BR] Map {0} Random Start Point X:{1} Y:{2}", CurrentMap.Info.Title, StartingLocation.X, StartingLocation.Y));

            ShrinkTime = Envir.Time + (Info.Duration / 4 * Settings.Minute);

            for (int i = 0; i < SignedupPlayers.Count; i++)
            {
                if (SignedupPlayers[i].Connection == null)
                    continue;
                if (SignedupPlayers[i].Connection.Stage != MirNetwork.GameStage.Game)
                    continue;
                if (SignedupPlayers[i].Dead)
                    continue;
                Point tempPoint = GetRandomPointInCircle();
                while (!CurrentMap.ValidPoint(tempPoint))
                    tempPoint = GetRandomPointInCircle();
                SignedupPlayers[i].Teleport(CurrentMap, tempPoint, true);
                SignedupPlayers[i].InLMSBR = true;
                SignedupPlayers[i].CurrentLMS = this;
            }
            SMain.EnqueueDebugging(string.Format("[LMS BR] Map {0} {1} Players signed up teleported in", CurrentMap.Info.Title, SignedupPlayers.Count));
            for (int i = 0; i < CurrentMap.Players.Count; i++)
            {
                PlayerObject player = CurrentMap.Players[i];
                if (player.IsGM ||
                    player.IsDev)
                    continue;
                if (player.Dead)
                    continue;
                PlayerRanks.Add(new LMS_Rank { Player = player, Kills = 0 });
            }
            Started = true;
            StartTime = Envir.Time;
            EndTime = Envir.Time + (Settings.Minute * Info.Duration);
            CurrentSize = Info.StartingSize;
            OriginalPlayercount = (byte)PlayerRanks.Count;
            CreateCircle(Info.StartingSize);
            CreateInnerCircle();
            BroadcastMessage(string.Format("Be advised you take damage while being OUTSIDE of the circle."), ChatType.Hint);
            long temp = ShrinkTime - Envir.Time;
            string remainingTime = Functions.PrintTimeSpanFromSeconds(temp / 1000);
            BroadcastMessage(string.Format("The Circle will shrink in {0} be ready!", remainingTime), ChatType.Announcement);
        }

        public void End_Match(bool foundWinner = false)
        {
            bool failed = false;
            string matchResult = "";
            if (foundWinner)
            {
                LMS_Rank lmsRank = null;
                PlayerObject winningPlayer = null;
                for (int i = 0; i < CurrentMap.Players.Count; i++)
                {
                    PlayerObject player = CurrentMap.Players[i];
                    if (player == null ||
                        player.Dead)
                        continue;
                    for (int x = 0; x < PlayerRanks.Count; x++)
                    {
                        if (player.Name == PlayerRanks[x].Player.Name &&
                            winningPlayer == null &&
                            lmsRank == null)
                        {
                            winningPlayer = player;
                            lmsRank = PlayerRanks[x];
                        }
                    }
                }
                if (winningPlayer != null &&
                    lmsRank != null)
                {
                    //  TODO Reward players (send by mail)
                    matchResult = string.Format("{0} is the Victor of the match!", winningPlayer.Name);
                    
                    winningPlayer.Teleport(Envir.GetMap(winningPlayer.BindMapIndex), winningPlayer.BindLocation, true, 0);
                    winningPlayer.InLMSBR = false;
                    List<LMS_RewardInfo> rewards = CreateRewards(1, winningPlayer);
                    if (rewards != null &&
                        rewards.Count > 0)
                    {
                        winningPlayer.ReceiveChat(string.Format("Victory is yours! Prize(s) will be sent via mail!"), ChatType.Hint);
                        List<UserItem> items = new List<UserItem>();
                        for (int i = 0; i < rewards.Count; i++)
                        {
                            if (rewards[i].ItemReward == null)
                            {
                                SMain.EnqueueDebugging(string.Format("Null reward"));
                                continue;
                            }
                            ItemInfo iInfo = Envir.GetItemInfo(rewards[i].ItemReward.Index);
                            if (iInfo == null)
                            {
                                SMain.EnqueueDebugging(string.Format("Null item info"));
                                continue;
                            }
                            UserItem item = Envir.CreateFreshItem(iInfo);
                            if (item != null)
                                items.Add(item);
                        }
                        MailInfo mail = new MailInfo(winningPlayer.Info.Index)
                        {
                            Items = items,
                            Message = string.Format("Congratulations on winning the match! here is your prize(s)"),
                            Sender = string.Format("LMS"),
                            MailID = ++Envir.NextMailID
                        };
                        mail.Send();
                    }
                    SMain.EnqueueDebugging(string.Format("[LMS BR] Map {0} {1} wins the Match, made {2} kills.", CurrentMap.Info.Title, winningPlayer.Name, lmsRank.Kills));
                }
                else
                {
                    failed = true;
                }
            }
            if ((failed && foundWinner) || !foundWinner)
            {
                if (CurrentMap.Players.Count > 0)
                {
                    string[] finalPlayers = new string[CurrentMap.Players.Count];
                    if (finalPlayers.Length >= 1)
                    {
                        int index = 0;
                        for (int i = 0; i < CurrentMap.Players.Count; i++)
                        {
                            PlayerObject player = CurrentMap.Players[i];
                            if (player.Dead ||
                                player.IsGM)
                                continue;
                            finalPlayers[index] = player.Name;
                            player.Teleport(Envir.GetMap(player.BindMapIndex), player.BindLocation);
                            player.InLMSBR = failed;
                            index++;
                        }
                        Array.Resize(ref finalPlayers, index);
                    }
                    matchResult = "Stalemate, Players ";
                    for (int i = 0; i < finalPlayers.Length; i++)
                    {
                        matchResult += string.Format("{0}{1} ", finalPlayers[i], i - 1 <= finalPlayers.Length ? "," : " have drawn the match!");
                    }
                }
            }

            if (matchResult.Length > 0)
                Envir.Broadcast(new ServerPackets.Chat { Message = matchResult, Type = ChatType.Announcement });
            StartTime = 0;
            EndTime = 0;
            SignedupPlayers = new List<PlayerObject>();
            CircleLocations = new List<Point>();
            PlayerRanks = new List<LMS_Rank>();
            StartingLocation = Info.StartingLocation;
            Stage = 0;
            Finished = false;
            Started = false;

        }

        public List<LMS_RewardInfo> CreateRewards(byte rank, PlayerObject player)
        {
            RequiredClass playersClass =
                player.Class == MirClass.Warrior ? RequiredClass.Warrior :
                player.Class == MirClass.Wizard ? RequiredClass.Wizard :
                player.Class == MirClass.Taoist ? RequiredClass.Taoist :
                player.Class == MirClass.Assassin ? RequiredClass.Assassin :
                RequiredClass.Archer;
            List<LMS_RewardInfo> rewards = new List<LMS_RewardInfo>();
            List<LMS_RewardInfo> group0 = new List<LMS_RewardInfo>();
            List<LMS_RewardInfo> group1 = new List<LMS_RewardInfo>();
            List<LMS_RewardInfo> group2 = new List<LMS_RewardInfo>();
            List<LMS_RewardInfo> group3 = new List<LMS_RewardInfo>();
            List<LMS_RewardInfo> group4 = new List<LMS_RewardInfo>();
            for (int i = 0; i < Info.Rewards.Count; i++)
            {
                if (Info.Rewards[i].Rank == 0)
                {

                }
                else if (Info.Rewards[i].Rank != rank)
                    continue;
                if (Info.Rewards[i].RequiredClass == RequiredClass.None)
                {

                }
                else if (Info.Rewards[i].RequiredClass != playersClass)
                    continue;
                if (Info.Rewards[i].Group == 0)
                    group0.Add(Info.Rewards[i]);
                else if (Info.Rewards[i].Group == 1)
                    group1.Add(Info.Rewards[i]);
                else if (Info.Rewards[i].Group == 2)
                    group2.Add(Info.Rewards[i]);
                else if (Info.Rewards[i].Group == 3)
                    group3.Add(Info.Rewards[i]);
                else if (Info.Rewards[i].Group == 4)
                    group4.Add(Info.Rewards[i]);
            }
            if (group0 != null &&
                group0.Count > 0)
            {
                int item1Index = group0.Count == 1 ? 0 : Envir.Random.Next(0, group0.Count - 1);
                if (group0[item1Index] != null)
                    if (Envir.Random.Next(0, 10000) <= group0[item1Index].Rate)
                        rewards.Add(group0[item1Index]);
            }
            if (group1 != null &&
                group1.Count > 0)
            {
                int item2Index = group1.Count == 1 ? 0 : Envir.Random.Next(0, group1.Count - 1);
                if (Envir.Random.Next(0, 10000) <= group1[item2Index].Rate)
                    rewards.Add(group1[item2Index]);
            }
            if (group2 != null &&
                group2.Count > 0)
            {
                int item3Index = group2.Count == 1 ? 0 : Envir.Random.Next(0, group2.Count - 1);
                if (Envir.Random.Next(0, 10000) <= group2[item3Index].Rate)
                    rewards.Add(group2[item3Index]);
            }
            if (group3 != null &&
                group3.Count > 0)
            {
                int item4Index = group3.Count == 1 ? 0 : Envir.Random.Next(0, group3.Count - 1);
                if (Envir.Random.Next(0, 10000) <= group3[item4Index].Rate)
                    rewards.Add(group3[item4Index]);
            }
            if (group4 != null &&
                group4.Count > 0)
            {
                int item5Index = group4.Count == 1 ? 0 : Envir.Random.Next(0, group4.Count - 1);
                if (Envir.Random.Next(0, 10000) <= group4[item5Index].Rate)
                    rewards.Add(group4[item5Index]);
            }
            return rewards;
        }

        public void CreateCircle(ushort circleSize)
        {
            CircleLocations = Functions.GetCircleLocations(StartingLocation, circleSize);

            for (int i = 0; i < CircleLocations.Count; i++)
            {
                if (CurrentMap.ValidPoint(CircleLocations[i]))
                {
                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.FireWall,
                        Value = 0,
                        ExpireTime = ShrinkTime,
                        //ExpireTime = Envir.Time + ( 10 + value / 2 ) * 1000,
                        TickSpeed = 1000,
                        CurrentLocation = CircleLocations[i],
                        CurrentMap = CurrentMap,
                        LMS_Circle = true
                    };

                    CurrentMap.Cells[CircleLocations[i].X, CircleLocations[i].Y].Add(ob);

                    ob.Spawned();
                }
            }
        }

        public void CreateInnerCircle()
        {
            List<Point> nextCircle = new List<Point>();
            nextCircle = Functions.GetCircleLocations(StartingLocation, NextSize);
            for (int i = 0; i < nextCircle.Count; i++)
            {
                if (CurrentMap.ValidPoint(nextCircle[i]))
                {
                    SpellObject spell = new SpellObject
                    {
                        Spell = Spell.DigOutZombie,
                        Value = 0,
                        ExpireTime = ShrinkTime,
                        //ExpireTime = Envir.Time + ( 10 + value / 2 ) * 1000,
                        TickSpeed = 1000,
                        CurrentLocation = nextCircle[i],
                        CurrentMap = CurrentMap,
                        LMS_Circle = true
                    };
                    CurrentMap.Cells[nextCircle[i].X, nextCircle[i].Y].Add(spell);

                    spell.Spawned();
                }
            }
        }

        public byte GetAlivePlayers()
        {
            if (CurrentMap == null) return 0;
            if (CurrentMap.Players.Count == 0) return 0;
            byte alivePlayers = 0;
            for (int i = 0; i < CurrentMap.Players.Count; i++)
            {
                PlayerObject player = CurrentMap.Players[i];
                if (player.IsGM ||
                    player.IsDev)
                    continue;
                if (!player.Dead)
                    alivePlayers++;
            }
            return alivePlayers;
        }

        public PlayerObject GetFromTopThree(byte rank = 1)
        {
            return null;
        }

        /// <summary>
        /// Broadcast messages to the players currently on the map.
        /// </summary>
        /// <param name="msg">The message being sent</param>
        /// <param name="type">The type of message</param>
        /// <param name="nextDelay">The next delay to add</param>
        /// <param name="targetType">The target players, 0 - All | 1 - Dead | 2 - Alive</param>
        public void BroadcastMessage(string msg, ChatType type, byte targetType = 0)
        {
            if (CurrentMap == null)
                return;
            if (StartTime == 0)
                return;
            if (!Started)
                return;
            if (Finished)
                return;
            if (CurrentMap.Players.Count <= 0)
                return;
            for (int i = 0; i < CurrentMap.Players.Count; i++)
            {
                PlayerObject player = CurrentMap.Players[i];
                if (targetType == 0)    //  All
                    player.ReceiveChat(msg, type);
                else if (targetType == 1 && player.Dead)    //  Dead
                    player.ReceiveChat(msg, type);
                else if (targetType == 2 && !player.Dead)   //  Alive
                    player.ReceiveChat(msg, type);
            }
        }
    }

    public class LMS_Rank
    {
        public PlayerObject Player;
        public byte Kills;
        public bool Alive = true;
    }

    public class LMS_Player_Rank
    {
        public int LMS_Index;
        public int PlayerAccIndex;
        public int PlayerIndex;
        public string PlayerName;
        public uint TotalMatches;
        public long Points;
        public uint Wins;   //  50 Points
        public uint Second; //  30 Points
        public uint Third;  //  20 Points
        public uint Fourth; //  10 Points
        public uint Fifth;  //  10 Points
        public uint Sixth;  //  5 Points
        public uint Seventh;//  5 Points
        public uint Eighth; //  5 Points
        public uint Ninth;  //  5 Points
        public uint Tenth;  //  5 Points
        public uint Other;  //  1 Point
        public uint Kills;  //  1 Point
        public uint Deaths; //  -1 Point
    }

    public class LMS_RewardInfo
    {
        public ItemInfo ItemReward;
        public byte Amount = 1;
        public RequiredClass RequiredClass = RequiredClass.None;
        public byte Group;
        public ushort Rate;
        public byte Rank;

        public LMS_RewardInfo() { }
        public LMS_RewardInfo(BinaryReader reader)
        {
            bool valid = reader.ReadBoolean();
            if (valid)
                ItemReward = new ItemInfo(reader, Envir.LoadVersion, Envir.LoadCustomVersion, false);
            RequiredClass = (RequiredClass)reader.ReadByte();
            Group = reader.ReadByte();
            Rate = reader.ReadUInt16();
            Rank = reader.ReadByte();
        }

        public void Save(BinaryWriter writer)
        {
            if (ItemReward == null)
                writer.Write(false);
            else
            {
                writer.Write(true);
                ItemReward.Save(writer);
            }
            writer.Write((byte)RequiredClass);
            writer.Write(Group);
            writer.Write(Rate);
            writer.Write(Rank);
        }

        public override string ToString()
        {
            return string.Format("[Item]{0}\t[Group]{1}\t[Rate]{2}\t[Class]{3}", ItemReward == null ? "NULL" : ItemReward.Name, Group, Rate, RequiredClass.ToString());
        }
    }
}
