using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Server.MirDatabase;

namespace Server
{
    internal static class Settings
    {
        public const int Day = 24 * Hour, Hour = 60 * Minute, Minute = 60 * Second, Second = 1000;
        public const string EnvirPath = @".\Envir\",
                            ConfigPath = @".\Configs\",
                            MapPath = @".\Maps\",
                            ExportPath = @".\Exports\",
                            GuildPath = @".\Guilds\",
                            ConquestsPath = @".\Conquests\",
                            NPCPath = EnvirPath + @".\NPCs\",
                            GoodsPath = EnvirPath + @".\Goods\",
                            QuestPath = EnvirPath + @".\Quests\",
                            DropPath = EnvirPath + @".\Drops\",
                            RoutePath = EnvirPath + @".\Routes\",
                            NameListPath = EnvirPath + @".\NameLists\",
                            ValuePath = EnvirPath + @".\Values\",
                            ReportPath = @".\Reports\",
                            RobotPath = EnvirPath + @".\Robot\",
                            LogPath = @".\Logs\",
                            ItemUpgrades = @".\ItemUpgrades\",
                            HumMobsPath = @".\HumanMobs\",
                            EventsPath = EnvirPath + @".\Events\",
                            DBPath = @".\Databases\";



        private static readonly InIReader Reader = new InIReader(ConfigPath + @".\Setup.ini");


        public static List<long> CommonShieldEXP = new List<long>();
        public static List<long> RareShieldEXP = new List<long>();
        public static List<long> LegendaryShieldEXP = new List<long>();
        public static List<long> MythicalShieldEXP = new List<long>();
        public static List<long> QuestShieldEXP = new List<long>();

        public static sbyte[] HeroStoreCaps = new sbyte[]
        {
            0,
            7,
            22,
            36,
            41,
            45,
            50,
            60,
            80
        };

        public static List<HumansMonsterSettings> HumMobs = new List<HumansMonsterSettings>();

        //General
        public static string VersionPath = @".\Mir2.Exe";
        public static bool CheckVersion = true;
        public static byte[] VersionHash;
        public static bool Multithreaded = true;
        public static int ThreadLimit = 2;
        public static bool TestServer = false;
        public static bool EnforceDBChecks = true;

        public static uint[] NormalBeneChances = new uint[MaxLuck];
        public static uint[] NormalBeneCurseChances = new uint[MaxLuck];
        public static uint[] SuperBeneChances = new uint[MaxLuck];
        public static uint[] SuperBeneCurseChances = new uint[MaxLuck];

        public static string DefaultNPCFilename = "00Default";
        public static string FishingDropFilename = "00Fishing";
	    public static string AwakeningDropFilename = "00Awakening";
        public static string StrongboxDropFilename = "00Strongbox";
        public static string BlackstoneDropFilename = "00Blackstone";
        public static string MonsterNPCFilename = "00Monster";
        public static string RobotFileName = "00Robot";
        public static string RandomQuestName = "00RandomQuest";
        public static string RuneDropFilename = "00Rune";
        public static string FortuneBoxWeaponFilename = "00FortuneBoxWeapon";
        public static string FortuneBoxArmourFilename = "00FortuneBoxArmour";
        public static string FortuneBoxAccessoryFilename = "00FortuneBoxAccessory";
        public static string FortuneBoxOrbFilename = "00FortuneBoxOrb";

        //Network
        public static string IPAddress = "127.0.0.1";

        public static ushort Port = 7000,
                             TimeOut = 10000,
                             MaxUser = 50,
                             RelogDelay = 50,
                             MaxIP = 5,
                             StatusConPort = 3000;
                             
        #region External Tool Settings & Users
        /// <summary>
        /// The Port the External Tool Connects to. [DEFAULT]4000
        /// </summary>
        public static ushort ServerManagerPort = 4000;
        /// <summary>
        /// Used to Toggle the Listener for the External tool to connecto to. [DEFAULT]False
        /// </summary>
        public static bool UseServerManager = false;
        /// <summary>
        /// Used to Toggle the debugging of Packets sent & received.
        /// </summary>
        public static bool DebugServerManager = true;
        /// <summary>
        /// By default there should be 1 user
        /// </summary>
        public static int ManagerUserCount = 1;

        /// <summary>
        /// The users setup for the Manager.
        /// </summary>
        public static List<MonitorToolUsers> ManagerUsers = new List<MonitorToolUsers>();
        #endregion
        //Permission
        public static bool AllowNewAccount = true,
                           AllowChangePassword = true,
                           AllowLogin = true,
                           AllowNewCharacter = true,
                           AllowDeleteCharacter = true,
                           AllowStartGame = false,
                           AllowCreateAssassin = true,
                           AllowCreateArcher = true;
        public static int DeadTimeDelay = 180000;
        public static int AllowedResolution = 1024;

        //Optional
        public static bool SafeZoneBorder = false,
                           SafeZoneHealing = false,
                           GatherOrbsPerLevel = true,
                           ExpMobLevelDifference = true,
                           ShowGMEffect = true;

        public static byte PercentPotionDelay = 60;

        public static int StartLevel = 1 , StartGold = 0;

        public static int BuyGTGold = 10000000, ExtendGT = 1000000, GTDays = 30;

        public static int VIPExp = 0;


        /// <summary>
        /// Default 5
        /// </summary>
        public static int SaveDelay = 5;
        public static short CredxGold = 30;
        //SQL Database
        public static bool UseSQL = false;
        public static string SQL_UName = "miruser";
        public static string SQL_Password = "sa";
        public static string SQL_IP = "127.0.0.1";
        public static int SQL_PORT = 3306;
        public static string SQL_DBName = "mir_db";


        //Game
        public static List<long> ExperienceList = new List<long>();
        public static List<long> OrbsExpList = new List<long>();
        public static List<long> OrbsDefList = new List<long>();
        public static List<long> OrbsDmgList = new List<long>();

        public static float DropRate = 1F, ExpRate = 1F, QuestDropRate = 1F , QuestExpRate = 1F, QuestGoldRate = 1F;

        public static int ItemTimeOut = 30,
                          PlayerDiedItemTimeOut = 120,
                          DropRange = 4,
                          DropStackSize = 5,
                          PKDelay = 12,
                          NonRedEquipDropLimit = 1,
                          NonRedInvDropLimit = 5,
                          RedEquipDropLimit = 2,
                          RedInvDropLimit = -1;

        public static long PetTimeOut = 5;
        public static bool PetSave = false;

        public static int RestedPeriod = 60,
                          RestedBuffLength = 10,
                          RestedExpBonus = 5,
                          RestedMaxBonus = 24;

        public static string SkeletonName = "BoneFamiliar",
                             SuperSkeletonName1 = "SuperBoneFamiliar1",
                             SuperSkeletonName2 = "SuperBoneFamiliar2",
                             SuperSkeletonName3 = "SuperBoneFamiliar3",
                             SuperShinsuName = "SuperShinsu",
                             SuperShinsuName2 = "SuperShinsu2",
                             SuperShinsuName4 = "SuperShinsu4",
                             ShinsuName = "Shinsu",
                             BugBatName = "BugBat",
                             Zuma1 = "ZumaStatue",
                             Zuma2 = "ZumaGuardian",
                             Zuma3 = "ZumaArcher",
                             Zuma4 = "WedgeMoth",
                             Zuma5 = "ZumaArcher3",
                             Zuma6 = "ZumaStatue3",
                             Zuma7 = "ZumaGuardian3",
                             Jar1Mob = "Hen",
                             Jar2Mob = "Hen",
                             Jar3Mob = "Hen",
                             Turtle1 = "RedTurtle",
                             Turtle2 = "GreenTurtle",
                             Turtle3 = "BlueTurtle",
                             Turtle4 = "TowerTurtle",
                             Turtle5 = "FinialTurtle",
                             BoneMonster1 = "BoneSpearman",
                             BoneMonster2 = "BoneBlademan",
                             BoneMonster3 = "BoneArcher",
                             BoneMonster4 = "BoneCaptain",
                             BehemothMonster1 = "Hugger",
                             BehemothMonster2 = "PoisonHugger",
                             BehemothMonster3 = "MutatedHugger",
                             HellKnight1 = "HellKnight1",
                             HellKnight2 = "HellKnight2",
                             HellKnight3 = "HellKnight3",
                             HellKnight4 = "HellKnight4",
                             HellBomb1 = "HellBomb1",
                             HellBomb2 = "HellBomb2",
                             HellBomb3 = "HellBomb3",
                             WhiteSnake = "WhiteSerpent",
                             AngelName = "HolyDeva",
                             AngelName1 = "HolyDeva1",
                             BombSpiderName = "BombSpider",
                             CloneName = "Clone",
                             AssassinCloneName = "AssassinClone",
                             VampireName = "VampireSpider",
                             ToadName = "SpittingToad",
                             SnakeTotemName = "SnakeTotem",
                             SnakesName = "CharmedSnake",
                             CrystalBeastSlave = "DarkSpirit",
                             DragonName = "HolyDragon",
                             DragonName1 = "HolyDragon1",
                             LordMonster1 = "LordsSlave1",
                             LordMonster2 = "LordsSlave2",
                             LordMonster3 = "LordsSlave3",
                             LordMonster4 = "LordsSlave4",
                             AncZuma1 = "ZumaStatue",
                             AncZuma2 = "ZumaGuardian",
                             AncZuma3 = "ZumaArcher",
                             AncZuma4 = "WedgeMoth",
                             AncZuma5 = "ZumaArcher3",
                             AncZuma6 = "ZumaStatue3",
                             AncZuma7 = "ZumaGuardian3",
                             AncBoneMonster1 = "BoneSpearman",
                             AncBoneMonster2 = "BoneBlademan",
                             AncBoneMonster3 = "BoneArcher",
                             AncBoneMonster4 = "BoneCaptain";

        public static string HealRing = "Healing",
                             FireRing = "FireBall",
                             ParalysisRing = "Paralysis";

        public static string PKTownMapName = "3";
        public static int PKTownPositionX = 848,
                          PKTownPositionY = 677;

        public static uint MaxDropGold = 2000;
        public static bool DropGold = true;

        public static SWBuffInfo SWBuffInfo;

        public static byte[] EliteDropRate = new byte[]
        {
            0,//1
            5,//2
            10,//3
            15,//4
            25,//5
            40,//6
            50,//7
            80//8
        };

        public static ushort[] EliteChances = new ushort[]
        {
            7000, //1
            6000, //2
            5000, //3
            3200, //4
            1900, //5
            1200, //6
            500, //7
            100 //8
        };

        public static byte[] EliteExpBoost = new byte[]
        {
            10,
            20,
            30,
            40,
            50,
            60,
            70,
            80
        };

        //IntelligentCreature
        public static string[] IntelligentCreatureNameList = { "BabyPig", "Chick", "Kitten", "BabySkeleton", "Baekdon", "Wimaen", "BlackKitten", "BabyDragon", "OlympicFlame", "BabySnowMan", "Frog", "BabyMonkey", "AngryBird", "Foxey" };
        public static string CreatureBlackStoneName = "BlackCreatureStone";

        //Fishing Settings
        public static int FishingAttempts = 30;
        public static int FishingSuccessStart = 10;
        public static int FishingSuccessMultiplier = 10;
        public static long FishingDelay = 0;
        public static int FishingMobSpawnChance = 5;
        public static string FishingMonster = "GiantKeratoid";

        //Mail Settings
        public static bool MailAutoSendGold = false;
        public static bool MailAutoSendItems = false;
        public static bool MailFreeWithStamp = true;
        public static uint MailCostPer1KGold = 100;
        public static uint MailItemInsurancePercentage = 5;
        public static uint MailCapacity = 100;

        //Refine Settings
        public static bool OnlyRefineWeapon = true;
        public static byte RefineBaseChance = 20;
        public static int RefineTime = 20;
        public static byte RefineIncrease = 1;
        public static byte RefineCritChance = 10;
        public static byte RefineCritIncrease = 2;
        public static byte RefineWepStatReduce = 6;
        public static byte RefineItemStatReduce = 15;
        public static int RefineCost = 125;

        public static string RefineOreName = "BlackIronOre";

        //Marriage Settings
        public static int LoverEXPBonus = 5;
        public static int MarriageCooldown = 7;
        public static bool WeddingRingRecall = true;
        public static int MarriageLevelRequired = 10;
        public static int ReplaceWedRingCost = 25; //125

        //NewbieGuild Settings
        public static string NewbieName = "Newbie";
        public static int NewbieMaxLevel = 33;
        public static int NewbieExpBuff = 20;

        //Mentor Settings
        public static byte MentorLevelGap = 10;
        public static bool MentorSkillBoost = true;
        public static byte MentorLength = 7;
        public static byte MentorDamageBoost = 10;
        public static byte MentorExpBoost = 10;
        public static byte MenteeExpBank = 1;

        //Group Settings
        public static bool GrpSin = true, GrpWar = true, GrpTao = true, GrpWiz = true, GrpArc = true, GrpNone = false;
        public static byte ExpStageIncrease = 0;

        //Gem Settings
        public static bool GemStatIndependent = true;

        public static byte FatalSwordChance = 10;
        public static byte MaxShieldLevel = 10;
        public static ItemGrade[] ShieldGrades = { ItemGrade.Common, ItemGrade.Rare, ItemGrade.Legendary, ItemGrade.Mythical, ItemGrade.Quest };
        public static ShieldUpgradeConfig[] ShieldConfig = new ShieldUpgradeConfig[5] {new ShieldUpgradeConfig(ItemGrade.Common), new ShieldUpgradeConfig(ItemGrade.Rare), new ShieldUpgradeConfig(ItemGrade.Legendary), new ShieldUpgradeConfig(ItemGrade.Mythical), new ShieldUpgradeConfig(ItemGrade.Quest) };
        public static byte ShieldEXPDivision = 10;
        public static byte EliteLevel1Bonus = 10;
        public static byte EliteLevel2Bonus = 20;
        public static byte EliteLevel3Bonus = 30;
        public static byte EliteLevel4Bonus = 40;
        public static byte EliteLevel5Bonus = 66;
        public static byte EliteLevel6Bonus = 80;
        public static byte EliteLevel7Bonus = 100;
        public static byte EliteLevel8Bonus = 100;
        public static byte ChanceToBeElite = 100;
        public static byte TDBPvPChance = 40;
        public static byte TDBPvEChance = 20;
        public static byte FlashDashChance = 30;
        public static byte TDBPoisonLevelRange = 10;



        //Goods Settings
        public static bool GoodsOn = true;
        public static uint GoodsMaxStored = 50;
        public static uint GoodsBuyBackTime = 60;
        public static uint GoodsBuyBackMaxStored = 20;


        //character settings
        private static String[] BaseStatClassNames = { "Warrior", "Wizard", "Taoist", "Assassin", "Archer" };
        public static BaseStats[] ClassBaseStats = new BaseStats[5] { new BaseStats(MirClass.Warrior), new BaseStats(MirClass.Wizard), new BaseStats(MirClass.Taoist), new BaseStats(MirClass.Assassin), new BaseStats(MirClass.Archer) };
        public static List<RandomItemStat> RandomItemStatsList = new List<RandomItemStat>();
        public static List<MineSet> MineSetList = new List<MineSet>();

        //item related settings
        public static byte MaxMagicResist = 6,
                    MagicResistWeight = 10,
                    MaxPoisonResist = 6,
                    PoisonResistWeight = 10,
                    MaxCriticalRate = 18,
                    CriticalRateWeight = 5,
                    MaxCriticalDamage = 10,
                    CriticalDamageWeight = 50,
                    MaxFreezing = 6,
                    FreezingAttackWeight = 10,
                    MaxPoisonAttack = 6,
                    PoisonAttackWeight = 10,
                    MaxHealthRegen = 8,
                    HealthRegenWeight = 10,
                    MaxManaRegen = 8,
                    ManaRegenWeight = 10,
                    MaxPoisonRecovery = 6,
                    MaxLuck = 10,
                    PerTickRegen = 5;

        public static Boolean PvpCanResistMagic = false,
                              PvpCanResistPoison = false,
                              PvpCanFreeze = false;
        /// <summary>
        /// MinMC, MaxMC + MagicShieldBaseDuration (seconds) [DEFAULT] 30
        /// </summary>
        public static long MagicShieldBaseDuration = 30;
        /// <summary>
        /// damage  *  (MagicShieldLevel + MagicShieldBaseReduction) / 10 [DEFAULT] 2
        /// </summary>
        public static long MagicShieldBaseReduction = 2;

        /// <summary>
        /// MagicShieldTime = MagicShieldStruckTimeReduction + (MS Lv 0 + 500, MS Lv 1 + 400, MS Lv 2 + 300, MS Lv 3 + 200) | [Lv 0 : 1sec] - [Lv 1 : .9sec] - [Lv 2 : .8sec] - [Lv 3 : .7sec] [DEFAULT] 500
        /// </summary>
        public static long MagicShieldStruckTimeReduction = 500;

        //Guild related settings
        public static byte Guild_RequiredLevel = 22, Guild_PointPerLevel = 0;
        public static float Guild_ExpRate = 0.01f;
        public static uint Guild_WarCost = 3000;
        public static long Guild_WarTime = 180;

        public static List<ItemVolume> Guild_CreationCostList = new List<ItemVolume>();
        public static List<long> Guild_ExperienceList = new List<long>();
        public static List<int> Guild_MembercapList = new List<int>();
        public static List<GuildBuffInfo> Guild_BuffList = new List<GuildBuffInfo>();

        public static void LoadVersion()
        {
            try
            {
                if (File.Exists(VersionPath))
                    using (FileStream stream = new FileStream(VersionPath, FileMode.Open, FileAccess.Read))
                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                        VersionHash = md5.ComputeHash(stream);
            }
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
            }
        }

        public static void Load()
        {
            //General Settings
            VersionPath = Reader.ReadString("General", "VersionPath", VersionPath);
            CheckVersion = Reader.ReadBoolean("General", "CheckVersion", CheckVersion);
            RelogDelay = Reader.ReadUInt16("General", "RelogDelay", RelogDelay);
            Multithreaded = Reader.ReadBoolean("General", "Multithreaded", Multithreaded);
            ThreadLimit = Reader.ReadInt32("General", "ThreadLimit", ThreadLimit);
            TestServer = Reader.ReadBoolean("General", "TestServer", TestServer);
            EnforceDBChecks = Reader.ReadBoolean("General", "EnforceDBChecks", EnforceDBChecks);

            //Paths Settings
            IPAddress = Reader.ReadString("Network", "IPAddress", IPAddress);
            Port = Reader.ReadUInt16("Network", "Port", Port);
            TimeOut = Reader.ReadUInt16("Network", "TimeOut", TimeOut);
            MaxUser = Reader.ReadUInt16("Network", "MaxUser", MaxUser);
            MaxIP = Reader.ReadUInt16("Network", "MaxIP", MaxIP);
            StatusConPort = Reader.ReadUInt16("Network", "StatusConPort", StatusConPort);
            
            //Permission Settings
            AllowNewAccount = Reader.ReadBoolean("Permission", "AllowNewAccount", AllowNewAccount);
            AllowChangePassword = Reader.ReadBoolean("Permission", "AllowChangePassword", AllowChangePassword);
            AllowLogin = Reader.ReadBoolean("Permission", "AllowLogin", AllowLogin);
            AllowNewCharacter = Reader.ReadBoolean("Permission", "AllowNewCharacter", AllowNewCharacter);
            AllowDeleteCharacter = Reader.ReadBoolean("Permission", "AllowDeleteCharacter", AllowDeleteCharacter);
            AllowStartGame = Reader.ReadBoolean("Permission", "AllowStartGame", AllowStartGame);
            AllowCreateAssassin = Reader.ReadBoolean("Permission", "AllowCreateAssassin", AllowCreateAssassin);
            AllowCreateArcher = Reader.ReadBoolean("Permission", "AllowCreateArcher", AllowCreateArcher);
            AllowedResolution = Reader.ReadInt32("Permission", "MaxResolution", AllowedResolution);

            //Optional Settings
            SafeZoneBorder = Reader.ReadBoolean("Optional", "SafeZoneBorder", SafeZoneBorder);
            SafeZoneHealing = Reader.ReadBoolean("Optional", "SafeZoneHealing", SafeZoneHealing);
            GatherOrbsPerLevel = Reader.ReadBoolean("Optional", "GatherOrbsPerLevel", GatherOrbsPerLevel);
            ExpMobLevelDifference = Reader.ReadBoolean("Optional", "ExpMobLevelDifference", ExpMobLevelDifference);
            ShowGMEffect = Reader.ReadBoolean("Optional", "ShowGMEffect", ShowGMEffect);

            //Database Settings
            SaveDelay = Reader.ReadInt32("Database", "SaveDelay", SaveDelay);
            CredxGold = Reader.ReadInt16("Database", "CredxGold", CredxGold);
            //MySQL Database
            UseSQL = Reader.ReadBoolean("MySQLDataBase", "UseSQL", UseSQL);
            SQL_UName = Reader.ReadString("MySQLDataBase", "SQL_UName", SQL_UName);
            SQL_Password = Reader.ReadString("MySQLDataBase", "SQL_Password", SQL_Password);
            SQL_IP = Reader.ReadString("MySQLDataBase", "SQL_IP", SQL_IP);
            SQL_PORT = Reader.ReadInt32("MySQLDataBase", "SQL_PORT", SQL_PORT);
            SQL_DBName = Reader.ReadString("MySQLDataBase", "SQL_DBName", SQL_DBName);
            //Game Settings
            StartLevel = Reader.ReadInt32("GameSettings", "StartLevel", StartLevel);
            StartGold = Reader.ReadInt32("GameSettings", "StartGold", StartGold);
            GTDays = Reader.ReadInt32("GameSettings", "GTDays", GTDays);
            BuyGTGold = Reader.ReadInt32("GameSettings", "BuyGTGold", BuyGTGold);
            ExtendGT = Reader.ReadInt32("GameSettings", "ExtendGT", ExtendGT);
            DropRate = Reader.ReadSingle("GameSettings", "DropRate", DropRate);
            ExpRate = Reader.ReadSingle("GameSettings", "ExpRate", ExpRate);
            QuestDropRate = Reader.ReadSingle("GameSettings", "QuestDropRate", QuestDropRate);
            QuestGoldRate = Reader.ReadSingle("GameSettings", "QuestGoldRate", QuestGoldRate);
            QuestExpRate = Reader.ReadSingle("GameSettings", "QuestExpRate", QuestExpRate);
            NewbieName = Reader.ReadString("GameSettings", "NewbieName", NewbieName);
            NewbieMaxLevel = Reader.ReadInt32("GameSettings", "NewbieMaxLevel", NewbieMaxLevel);
            NewbieExpBuff = Reader.ReadInt32("GameSettings", "NewbieExpBuff", NewbieExpBuff);
            VIPExp = Reader.ReadInt32("GameSettings", "VIPExp", VIPExp);
            ItemTimeOut = Reader.ReadInt32("GameSettings", "ItemTimeOut", ItemTimeOut);
            PlayerDiedItemTimeOut = Reader.ReadInt32("GameSettings", "PlayerDiedItemTimeOut", PlayerDiedItemTimeOut);
            PetTimeOut = Reader.ReadInt64("GameSettings", "PetTimeOut", PetTimeOut);
            PetSave = Reader.ReadBoolean("GameSettings", "PetSave", PetSave);
            PKDelay = Reader.ReadInt32("GameSettings", "PKDelay", PKDelay);
            TDBPvEChance = Reader.ReadByte("GameSettings", "TDBStunChancePvE", TDBPvEChance);
            TDBPvPChance = Reader.ReadByte("GameSettings", "TDBStunChancePvP", TDBPvPChance);
            TDBPoisonLevelRange = Reader.ReadByte("GameSettings", "TDBPoisonLevelRange", TDBPoisonLevelRange);
            FlashDashChance = Reader.ReadByte("GameSettings", "FlashDashStunChance", FlashDashChance);
            FatalSwordChance = Reader.ReadByte("GameSettings", "FatalSwordChance", FatalSwordChance);
            MagicShieldBaseDuration = Reader.ReadInt64("GameSettings", "MagicShieldBaseDuration", MagicShieldBaseDuration);
            MagicShieldBaseReduction = Reader.ReadInt64("GameSetting", "MagicShieldBaseReduction", MagicShieldBaseReduction);
            MagicShieldStruckTimeReduction = Reader.ReadInt64("GameSettings", "MagicShieldStruckTimeReduction", MagicShieldStruckTimeReduction);
            DeadTimeDelay = Reader.ReadInt32("GameSettings", "DeadTimeDelay", DeadTimeDelay);
            NonRedEquipDropLimit = Reader.ReadInt32("GameSettings", "NonRedEquipDropLimit", NonRedEquipDropLimit);
            NonRedInvDropLimit = Reader.ReadInt32("GameSettings", "NonRedInvDropLimit", NonRedInvDropLimit);
            RedEquipDropLimit = Reader.ReadInt32("GameSettings", "RedEquipDropLimit", RedEquipDropLimit);
            RedInvDropLimit = Reader.ReadInt32("GameSettings", "RedInvDropLimit", RedInvDropLimit);
            //Taoist Pets
            SkeletonName = Reader.ReadString("TaoistPets", "SkeletonName", SkeletonName);
            SuperSkeletonName1 = Reader.ReadString("TaoistPets", "SuperSkeletonName1", SuperSkeletonName1);
            SuperSkeletonName2 = Reader.ReadString("TaoistPets", "SuperSkeletonName2", SuperSkeletonName2);
            SuperSkeletonName3 = Reader.ReadString("TaoistPets", "SuperSkeletonName3", SuperSkeletonName3);
            ShinsuName = Reader.ReadString("TaoistPets", "ShinsuName", ShinsuName);
            SuperShinsuName = Reader.ReadString("TaoistPets", "SuperShinsuName", SuperShinsuName);
            SuperShinsuName2 = Reader.ReadString("TaoistPets", "SuperShinsuName2", SuperShinsuName2);
            SuperShinsuName4 = Reader.ReadString("TaoistPets", "SuperShinsuName4", SuperShinsuName4);
            AngelName = Reader.ReadString("TaoistPets", "AngelName", AngelName);
            AngelName1 = Reader.ReadString("TaoistPets", "AngelName1", AngelName1);
            DragonName = Reader.ReadString("TaoistPets", "DragonName", DragonName);
            DragonName1 = Reader.ReadString("TaoistPets", "DragonName1", DragonName1);

            //Elite System
            ChanceToBeElite = Reader.ReadByte("EliteSystem", "ChanceToBeElite", ChanceToBeElite);
            EliteLevel1Bonus = Reader.ReadByte("EliteSystem", "EliteLevel1Bonus", EliteLevel1Bonus);
            EliteLevel2Bonus = Reader.ReadByte("EliteSystem", "EliteLevel2Bonus", EliteLevel2Bonus);
            EliteLevel3Bonus = Reader.ReadByte("EliteSystem", "EliteLevel3Bonus", EliteLevel3Bonus);
            EliteLevel4Bonus = Reader.ReadByte("EliteSystem", "EliteLevel4Bonus", EliteLevel4Bonus);
            EliteLevel5Bonus = Reader.ReadByte("EliteSystem", "EliteLevel5Bonus", EliteLevel5Bonus);
            EliteLevel6Bonus = Reader.ReadByte("EliteSystem", "EliteLevel6Bonus", EliteLevel6Bonus);
            EliteLevel7Bonus = Reader.ReadByte("EliteSystem", "EliteLevel7Bonus", EliteLevel7Bonus);
            EliteLevel8Bonus = Reader.ReadByte("EliteSystem", "EliteLevel8Bonus", EliteLevel8Bonus);

            EliteDropRate[0] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel1Rate"), EliteDropRate[0]);
            EliteDropRate[1] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel2Rate"), EliteDropRate[1]);
            EliteDropRate[2] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel3Rate"), EliteDropRate[2]);
            EliteDropRate[3] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel4Rate"), EliteDropRate[3]);
            EliteDropRate[4] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel5Rate"), EliteDropRate[4]);
            EliteDropRate[5] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel6Rate"), EliteDropRate[5]);
            EliteDropRate[6] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel7Rate"), EliteDropRate[6]);
            EliteDropRate[7] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel8Rate"), EliteDropRate[7]);
            EliteChances[0] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel2Chance"), EliteChances[0]);
            EliteChances[1] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel3Chance"), EliteChances[1]);
            EliteChances[2] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel4Chance"), EliteChances[2]);
            EliteChances[3] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel5Chance"), EliteChances[3]);
            EliteChances[4] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel6Chance"), EliteChances[4]);
            EliteChances[5] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel7Chance"), EliteChances[5]);
            EliteChances[6] = Reader.ReadUInt16("EliteSystem", string.Format("EliteLevel8Chance"), EliteChances[6]);
            EliteExpBoost[0] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel1XPBoost"), EliteExpBoost[0]);
            EliteExpBoost[1] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel2XPBoost"), EliteExpBoost[1]);
            EliteExpBoost[2] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel3XPBoost"), EliteExpBoost[2]);
            EliteExpBoost[3] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel4XPBoost"), EliteExpBoost[3]);
            EliteExpBoost[4] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel5XPBoost"), EliteExpBoost[4]);
            EliteExpBoost[5] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel6XPBoost"), EliteExpBoost[5]);
            EliteExpBoost[6] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel7XPBoost"), EliteExpBoost[6]);
            EliteExpBoost[7] = Reader.ReadByte("EliteSystem", string.Format("EliteLevel8XPBoost"), EliteExpBoost[7]);
            //  Hero Storage caps
            HeroStoreCaps[0] = Reader.ReadSByte("HeroStore", "Cap0", HeroStoreCaps[0]);
            HeroStoreCaps[1] = Reader.ReadSByte("HeroStore", "Cap1", HeroStoreCaps[1]);
            HeroStoreCaps[2] = Reader.ReadSByte("HeroStore", "Cap2", HeroStoreCaps[2]);
            HeroStoreCaps[3] = Reader.ReadSByte("HeroStore", "Cap3", HeroStoreCaps[3]);
            HeroStoreCaps[4] = Reader.ReadSByte("HeroStore", "Cap4", HeroStoreCaps[4]);
            HeroStoreCaps[5] = Reader.ReadSByte("HeroStore", "Cap5", HeroStoreCaps[5]);
            HeroStoreCaps[6] = Reader.ReadSByte("HeroStore", "Cap6", HeroStoreCaps[6]);
            HeroStoreCaps[7] = Reader.ReadSByte("HeroStore", "Cap7", HeroStoreCaps[7]);
            //Monster Settings
            BugBatName = Reader.ReadString("Game", "BugBatName", BugBatName);
            Zuma1 = Reader.ReadString("Game", "Zuma1", Zuma1);
            Zuma2 = Reader.ReadString("Game", "Zuma2", Zuma2);
            Zuma3 = Reader.ReadString("Game", "Zuma3", Zuma3);
            Zuma4 = Reader.ReadString("Game", "Zuma4", Zuma4);
            Zuma5 = Reader.ReadString("Game", "Zuma5", Zuma5);
            Zuma6 = Reader.ReadString("Game", "Zuma6", Zuma6);
            Zuma7 = Reader.ReadString("Game", "Zuma7", Zuma7);
            Jar1Mob = Reader.ReadString("Game", "Jar1Mob", Jar1Mob);
            Jar2Mob = Reader.ReadString("Game", "Jar2Mob", Jar2Mob);
            Jar3Mob = Reader.ReadString("Game", "Jar3Mob", Jar3Mob);
            Turtle1 = Reader.ReadString("Game", "Turtle1", Turtle1);
            Turtle2 = Reader.ReadString("Game", "Turtle2", Turtle2);
            Turtle3 = Reader.ReadString("Game", "Turtle3", Turtle3);
            Turtle4 = Reader.ReadString("Game", "Turtle4", Turtle4);
            Turtle5 = Reader.ReadString("Game", "Turtle5", Turtle5);
            BoneMonster1 = Reader.ReadString("Game", "BoneMonster1", BoneMonster1);
            BoneMonster2 = Reader.ReadString("Game", "BoneMonster2", BoneMonster2);
            BoneMonster3 = Reader.ReadString("Game", "BoneMonster3", BoneMonster3);
            BoneMonster4 = Reader.ReadString("Game", "BoneMonster4", BoneMonster4);
            BehemothMonster1 = Reader.ReadString("Game", "BehemothMonster1", BehemothMonster1);
            BehemothMonster2 = Reader.ReadString("Game", "BehemothMonster2", BehemothMonster2);
            BehemothMonster3 = Reader.ReadString("Game", "BehemothMonster3", BehemothMonster3);
            HellKnight1 = Reader.ReadString("Game", "HellKnight1", HellKnight1);
            HellKnight2 = Reader.ReadString("Game", "HellKnight2", HellKnight2);
            HellKnight3 = Reader.ReadString("Game", "HellKnight3", HellKnight3);
            HellKnight4 = Reader.ReadString("Game", "HellKnight4", HellKnight4);
            HellBomb1 = Reader.ReadString("Game", "HellBomb1", HellBomb1);
            HellBomb2 = Reader.ReadString("Game", "HellBomb2", HellBomb2);
            HellBomb3 = Reader.ReadString("Game", "HellBomb3", HellBomb3);
            WhiteSnake = Reader.ReadString("Game", "WhiteSnake", WhiteSnake);
            BombSpiderName = Reader.ReadString("Game", "BombSpiderName", BombSpiderName);
            CloneName = Reader.ReadString("Game", "CloneName", CloneName);
            FishingMonster = Reader.ReadString("Game", "FishMonster", FishingMonster);
            AssassinCloneName = Reader.ReadString("Game", "AssassinCloneName", AssassinCloneName);
            VampireName = Reader.ReadString("Game", "VampireName", VampireName);
            ToadName = Reader.ReadString("Game", "ToadName", ToadName);
            SnakeTotemName = Reader.ReadString("Game", "SnakeTotemName", SnakeTotemName);
            SnakesName = Reader.ReadString("Game", "SnakesName", SnakesName);
            LordMonster1 = Reader.ReadString("Game", "LordMonster1", LordMonster1);
            LordMonster2 = Reader.ReadString("Game", "LordMonster2", LordMonster2);
            LordMonster3 = Reader.ReadString("Game", "LordMonster3", LordMonster3);
            LordMonster4 = Reader.ReadString("Game", "LordMonster4", LordMonster4);
            AncZuma1 = Reader.ReadString("Game", "AncZuma1", AncZuma1);
            AncZuma2 = Reader.ReadString("Game", "AncZuma2", AncZuma2);
            AncZuma3 = Reader.ReadString("Game", "AncZuma3", AncZuma3);
            AncZuma4 = Reader.ReadString("Game", "AncZuma4", AncZuma4);
            AncZuma5 = Reader.ReadString("Game", "AncZuma5", AncZuma5);
            AncZuma6 = Reader.ReadString("Game", "AncZuma6", AncZuma6);
            AncZuma7 = Reader.ReadString("Game", "AncZuma7", AncZuma7);
            AncBoneMonster1 = Reader.ReadString("Game", "AncBoneMonster1", AncBoneMonster1);
            AncBoneMonster2 = Reader.ReadString("Game", "AncBoneMonster2", AncBoneMonster2);
            AncBoneMonster3 = Reader.ReadString("Game", "AncBoneMonster3", AncBoneMonster3);
            AncBoneMonster4 = Reader.ReadString("Game", "AncBoneMonster4", AncBoneMonster4);

            //Rested
            RestedPeriod = Reader.ReadInt32("Rested", "Period", RestedPeriod);
            RestedBuffLength = Reader.ReadInt32("Rested", "BuffLength", RestedBuffLength);
            RestedExpBonus = Reader.ReadInt32("Rested", "ExpBonus", RestedExpBonus);
            RestedMaxBonus = Reader.ReadInt32("Rested", "MaxBonus", RestedMaxBonus);

            //Items
            HealRing = Reader.ReadString("Items", "HealRing", HealRing);
            FireRing = Reader.ReadString("Items", "FireRing", FireRing);

            //Group
            GrpWar = Reader.ReadBoolean("Group", "GrpWar", GrpWar);
            GrpWiz = Reader.ReadBoolean("Group", "GrpWiz", GrpWiz);
            GrpTao = Reader.ReadBoolean("Group", "GrpTao", GrpTao);
            GrpSin = Reader.ReadBoolean("Group", "GrpSin", GrpSin);
            GrpArc = Reader.ReadBoolean("Group", "GrpArc", GrpArc);
            GrpNone = Reader.ReadBoolean("Group", "GrpNone", GrpNone);
            ExpStageIncrease = Reader.ReadByte("Group", "ExpStageIncrease", ExpStageIncrease);

            //PKTown
            PKTownMapName = Reader.ReadString("PKTown", "PKTownMapName", PKTownMapName);
            PKTownPositionX = Reader.ReadInt32("PKTown", "PKTownPositionX", PKTownPositionX);
            PKTownPositionY = Reader.ReadInt32("PKTown", "PKTownPositionY", PKTownPositionY);

            //Gold Drop
            DropGold = Reader.ReadBoolean("DropGold", "DropGold", DropGold);
            MaxDropGold = Reader.ReadUInt32("DropGold", "MaxDropGold", MaxDropGold);

            //Item
            MaxMagicResist = Reader.ReadByte("Items","MaxMagicResist",MaxMagicResist);
            MagicResistWeight = Reader.ReadByte("Items","MagicResistWeight",MagicResistWeight);
            MaxPoisonResist = Reader.ReadByte("Items","MaxPoisonResist",MaxPoisonResist);
            PoisonResistWeight = Reader.ReadByte("Items","PoisonResistWeight",PoisonResistWeight);
            MaxCriticalRate = Reader.ReadByte("Items","MaxCriticalRate",MaxCriticalRate);
            CriticalRateWeight = Reader.ReadByte("Items","CriticalRateWeight",CriticalRateWeight);
            MaxCriticalDamage = Reader.ReadByte("Items","MaxCriticalDamage",MaxCriticalDamage);
            CriticalDamageWeight = Math.Max((byte)1,Reader.ReadByte("Items","CriticalDamageWeight",CriticalDamageWeight));
            MaxFreezing = Reader.ReadByte("Items","MaxFreezing",MaxFreezing);
            FreezingAttackWeight = Reader.ReadByte("Items","FreezingAttackWeight",FreezingAttackWeight);
            MaxPoisonAttack = Reader.ReadByte("Items","MaxPoisonAttack",MaxPoisonAttack);
            PoisonAttackWeight = Reader.ReadByte("Items","PoisonAttackWeight",PoisonAttackWeight);
            MaxHealthRegen = Reader.ReadByte("Items", "MaxHealthRegen", MaxHealthRegen);
            HealthRegenWeight = Reader.ReadByte("Items", "HealthRegenWeight", HealthRegenWeight);
            MaxManaRegen = Reader.ReadByte("Items", "MaxManaRegen", MaxManaRegen);
            ManaRegenWeight = Reader.ReadByte("Items", "ManaRegenWeight", ManaRegenWeight);
            MaxPoisonRecovery = Reader.ReadByte("Items", "MaxPoisonRecovery", MaxPoisonRecovery);
            MaxLuck = Reader.ReadByte("Items", "MaxLuck", MaxLuck);
            PvpCanResistMagic = Reader.ReadBoolean("Items","PvpCanResistMagic",PvpCanResistMagic);
            PvpCanResistPoison = Reader.ReadBoolean("Items", "PvpCanResistPoison", PvpCanResistPoison);
            PvpCanFreeze = Reader.ReadBoolean("Items", "PvpCanFreeze", PvpCanFreeze);
            PerTickRegen = Reader.ReadByte("Items", "PerTickRegen", PerTickRegen);
            //Custom
            CrystalBeastSlave = Reader.ReadString("Custom", "CrystalBeastSlave", CrystalBeastSlave);
            PercentPotionDelay = Reader.ReadByte("Custom", "PercentPotionDelay", PercentPotionDelay);
            MaxShieldLevel = Reader.ReadByte("Custom", "MaxShieldLevel", MaxShieldLevel);
            ShieldEXPDivision = Reader.ReadByte("Custom", "ShieldEXPDivision", ShieldEXPDivision);

            //IntelligentCreature
            for (int i = 0; i < IntelligentCreatureNameList.Length; i++)
                IntelligentCreatureNameList[i] = Reader.ReadString("IntelligentCreatures", "Creature" + i.ToString() + "Name", IntelligentCreatureNameList[i]);
            CreatureBlackStoneName = Reader.ReadString("IntelligentCreatures", "CreatureBlackStoneName", CreatureBlackStoneName);

            if (!Directory.Exists(EnvirPath))
                Directory.CreateDirectory(EnvirPath);
            if (!Directory.Exists(ConfigPath))
                Directory.CreateDirectory(ConfigPath);

            if (!Directory.Exists(MapPath))
                Directory.CreateDirectory(MapPath);
            if (!Directory.Exists(NPCPath))
                Directory.CreateDirectory(NPCPath);
            if (!Directory.Exists(GoodsPath))
                Directory.CreateDirectory(GoodsPath);
            if (!Directory.Exists(QuestPath))
                Directory.CreateDirectory(QuestPath);
            if (!Directory.Exists(DropPath))
                Directory.CreateDirectory(DropPath);
            if (!Directory.Exists(ExportPath))
                Directory.CreateDirectory(ExportPath);
            if (!Directory.Exists(RoutePath))
                Directory.CreateDirectory(RoutePath);
            if (!Directory.Exists(RobotPath))
                Directory.CreateDirectory(RobotPath);

            if (!Directory.Exists(NameListPath))
                Directory.CreateDirectory(NameListPath);
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
            if (!Directory.Exists(ItemUpgrades))
                Directory.CreateDirectory(ItemUpgrades);

            string fileName = Path.Combine(Settings.NPCPath, DefaultNPCFilename + ".txt");

            if (!File.Exists(fileName))
            {
                FileStream NewFile = File.Create(fileName);
                NewFile.Close();
            }

            fileName = Path.Combine(Settings.NPCPath, MonsterNPCFilename + ".txt");

            if (!File.Exists(fileName))
            {
                FileStream NewFile = File.Create(fileName);
                NewFile.Close();
            }

            LoadBeneConfigs();
            LoadVersion();
            LoadEXP();
            LoadBaseStats();
            LoadRandomItemStats();
            LoadMines();
            LoadGuildSettings();
			LoadAwakeAttribute();
            LoadFishing();
            LoadMail();
            LoadRefine();
            LoadMarriage();
            LoadMentor();
            LoadGoods();
            LoadGem();
            LoadShieldUpgrades();
            LoadHumMobs();
            LoadManagerSettings();
            LoadSWBuffInfo();
        }

        public static void LoadSWBuffInfo()
        {
            if (!File.Exists(ConfigPath + @".\SWBuffConfig.ini"))
            {
                SaveSWBuffInfo();
                return;
            }
            SWBuffInfo = new SWBuffInfo();
            InIReader reader = new InIReader(ConfigPath + @".\SWBuffConfig.ini");
            SWBuffInfo.Type = reader.ReadByte("General", "Type", 0);
            SWBuffInfo.EXPBoost = reader.ReadByte("General", "EXPBoost", 0);
            SWBuffInfo.DropBoost = reader.ReadByte("General", "DropBoost", 0);
            for (int i = (byte)MirClass.Warrior; i <= (byte)MirClass.Archer; i++)
            {
                ClassBuffInfo bInfo = new ClassBuffInfo
                {
                    ClassInfo = (MirClass)i,
                    MinAC = reader.ReadByte(((MirClass)i).ToString(), "MinAC", 0),
                    MaxAC = reader.ReadByte(((MirClass)i).ToString(), "MaxAC", 0),
                    MinMAC = reader.ReadByte(((MirClass)i).ToString(), "MinMAC", 0),
                    MaxMAC = reader.ReadByte(((MirClass)i).ToString(), "MaxMAC", 0),
                    MinDC = reader.ReadByte(((MirClass)i).ToString(), "MinDC", 0),
                    MaxDC = reader.ReadByte(((MirClass)i).ToString(), "MaxDC", 0),
                    MinMC = reader.ReadByte(((MirClass)i).ToString(), "MinMC", 0),
                    MaxMC = reader.ReadByte(((MirClass)i).ToString(), "MaxMC", 0),
                    MinSC = reader.ReadByte(((MirClass)i).ToString(), "MinSC", 0),
                    MaxSC = reader.ReadByte(((MirClass)i).ToString(), "MaxSC", 0),
                    Agility = reader.ReadByte(((MirClass)i).ToString(), "Agility", 0),
                    Accuracy = reader.ReadByte(((MirClass)i).ToString(), "Accuracy", 0),
                    ASpeed = reader.ReadByte(((MirClass)i).ToString(), "ASpeed", 0),
                    HP = reader.ReadUInt16(((MirClass)i).ToString(), "HP", 0),
                    MP = reader.ReadUInt16(((MirClass)i).ToString(), "MP", 0),
                };
                SWBuffInfo.BuffInfo.Add(bInfo);
            }
        }

        public static void SaveSWBuffInfo()
        {
            if (SWBuffInfo == null)
            {
                SWBuffInfo = new SWBuffInfo()
                {
                    Type = 0,
                    BuffInfo = new List<ClassBuffInfo>(),
                    EXPBoost = 0,
                    DropBoost = 0
                };
                for (int i = (byte)MirClass.Warrior; i <= (byte)MirClass.Archer; i++)
                {
                    SWBuffInfo.BuffInfo.Add(new ClassBuffInfo
                    {
                        ClassInfo = (MirClass)i,
                        MinAC = 0,
                        MaxAC = 0,
                        MinMAC = 0,
                        MaxMAC = 0,
                        MinDC = 0,
                        MaxDC = 0,
                        MinMC = 0,
                        MaxMC = 0,
                        MinSC = 0,
                        MaxSC = 0,
                        Accuracy = 0,
                        ASpeed = 0,
                        Agility = 0,
                        HP = 0,
                        MP = 0
                    });
                }
            }
            InIReader writer = new InIReader(ConfigPath + @".\SWBuffConfig.ini");
            writer.Write("General", "Type", SWBuffInfo.Type);
            writer.Write("General", "EXPBoost", SWBuffInfo.EXPBoost);
            writer.Write("General", "DropBoost", SWBuffInfo.DropBoost);
            for (int i = 0; i < SWBuffInfo.BuffInfo.Count; i++)
            {
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MinAC", SWBuffInfo.BuffInfo[i].MinAC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MaxAC", SWBuffInfo.BuffInfo[i].MaxAC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MinMAC", SWBuffInfo.BuffInfo[i].MinMAC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MaxMAC", SWBuffInfo.BuffInfo[i].MaxMAC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MinDC", SWBuffInfo.BuffInfo[i].MinDC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MaxDC", SWBuffInfo.BuffInfo[i].MaxDC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MinMC", SWBuffInfo.BuffInfo[i].MinMC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MaxMC", SWBuffInfo.BuffInfo[i].MaxMC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MinSC", SWBuffInfo.BuffInfo[i].MinSC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MaxSC", SWBuffInfo.BuffInfo[i].MaxSC);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "Accuracy", SWBuffInfo.BuffInfo[i].Accuracy);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "Agility", SWBuffInfo.BuffInfo[i].Agility);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "ASpeed", SWBuffInfo.BuffInfo[i].ASpeed);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "HP", SWBuffInfo.BuffInfo[i].HP);
                writer.Write(SWBuffInfo.BuffInfo[i].ClassInfo.ToString(), "MP", SWBuffInfo.BuffInfo[i].MP);
            }
        }

        public static void LoadManagerSettings()
        {
            if (!File.Exists(ConfigPath + @".\ManagerTool.ini"))
            {
                SaveManagerSettings();
                return;
            }
            else
            {
                InIReader reader = new InIReader(ConfigPath + @".\ManagerTool.ini");
                UseServerManager = reader.ReadBoolean("Settings", "UseManager", UseServerManager);
                DebugServerManager = reader.ReadBoolean("Settings", "DebugManager", DebugServerManager);
                ServerManagerPort = reader.ReadUInt16("Settings", "Port", ServerManagerPort);

                ManagerUserCount = reader.ReadInt32("Users", "Count", ManagerUserCount);
                for (int i = 0; i < ManagerUserCount; i++)
                {
                    ManagerUsers.Add(new MonitorToolUsers
                    {
                        UserName = reader.ReadString(string.Format("User{0}", i), "UserName", "miradmin"),
                        Password = reader.ReadString(string.Format("User{0}", i), "Password", "lomcn-edens-elite"),
                        Permissions = reader.ReadByte(string.Format("User{0}", i), "Permissions", 10)
                    });
                }
            }
        }

        public static void SaveManagerSettings()
        {
            InIReader reader = new InIReader(ConfigPath + @".\ManagerTool.ini");
            reader.Write("Settings", "UseManager", UseServerManager);
            reader.Write("Settings", "DebugManager", DebugServerManager);
            reader.Write("Settings", "Port", ServerManagerPort);
            ManagerUsers.Add(new MonitorToolUsers
            {
                UserName = "miradmin",
                Password = "lomcn-edens-elite",
                Permissions = 10
            });
            reader.Write("User0", "UserName", ManagerUsers[0].UserName);
            reader.Write("User0", "Password", ManagerUsers[0].Password);
            reader.Write("User0", "Permissions", ManagerUsers[0].Permissions);
        }

        public static void LoadBeneConfigs()
        {
            if (!File.Exists(ConfigPath + @".\BeneConfig.ini"))
            {
                SaveBeneConfig();
                return;
            }
            else
            {
                NormalBeneChances = new uint[MaxLuck];
                NormalBeneCurseChances = new uint[MaxLuck];
                SuperBeneChances = new uint[MaxLuck];
                SuperBeneCurseChances = new uint[MaxLuck];
                InIReader reader = new InIReader(ConfigPath + @".\BeneConfig.ini");
                for (int i = 0; i < MaxLuck; i++)
                {
                    NormalBeneChances[i] = reader.ReadUInt16("NormalBene", "Luck" + i, 0);
                    NormalBeneCurseChances[i] = reader.ReadUInt16("NormalBene", "Curse" + i, 0);
                    SuperBeneChances[i] = reader.ReadUInt16("SuperBene", "Luck" + i, 0);
                    SuperBeneCurseChances[i] = reader.ReadUInt16("SuperBene", "Curse" + i, 0);
                }
            }
        }

        public static void SaveBeneConfig()
        {
            InIReader reader = new InIReader(ConfigPath + @".\BeneConfig.ini");
            for (int i = 0; i < MaxLuck; i++)
            {
                reader.Write("NormalBene", "Luck" + i, 1 + (i * 3));
                reader.Write("NormalBene", "Curse" + i, 1 + (i * 4));
                reader.Write("SuperBene", "Luck" + i, 1 + (i * 2));
                reader.Write("SuperBene", "Curse" + i, 1 + (i * 3));
            }
        }

        public static void SaveHumMobs()
        {
            InIReader reader = new InIReader(HumMobsPath + @".\Humans.ini");
            reader.Write("Count", "HumanMobCount", 0);
        }

        public static void LoadHumMobs()
        {
            if (!Directory.Exists(HumMobsPath))
                Directory.CreateDirectory(HumMobsPath);
            if (!File.Exists(HumMobsPath + @".\Humans.ini"))
            {
                SaveHumMobs();
                return;
            }

            InIReader reader = new InIReader(HumMobsPath + @".\Humans.ini");
            int count = reader.ReadInt32("Count", "HumanMobCount", 0);
            for (int i = 0; i < count; i++)
            {
                HumansMonsterSettings hum = new HumansMonsterSettings
                {
                    HumansName = reader.ReadString("Human" + i.ToString(), "Name", string.Empty),
                    Weapon = reader.ReadInt16("Human" + i.ToString(), "Weapon", 0),
                    Armour = reader.ReadInt16("Human" + i.ToString(), "Armour", 0),
                    MobsClass = (MirClass)reader.ReadByte("Human" + i.ToString(), "Class", 0),
                    MobsGender = (MirGender)reader.ReadByte("Human" + i.ToString(), "Gender", 0),
                    Hair = reader.ReadByte("Human" + i.ToString(), "Hair", 0),
                    Wing = reader.ReadByte("Human" + i.ToString(), "Wing", 0),
                    Light = reader.ReadByte("Human" + i.ToString(), "Light", 0)
                };
                HumMobs.Add(hum);
            }
        }
        
        public static void SaveShieldUpgrades()
        {
            File.Delete(ItemUpgrades + @".\Shield.ini");
            InIReader reader = new InIReader(ItemUpgrades + @".\Shield.ini");
            if (MaxShieldLevel > 0)
            {
                for (int i = 0; i < CommonShieldEXP.Count; i++)
                    reader.Write(ShieldGrades[0].ToString(), "Level" + i, CommonShieldEXP[i]);
                for (int i = 0; i < CommonShieldEXP.Count; i++)
                    reader.Write(ShieldGrades[1].ToString(), "Level" + i, RareShieldEXP[i]);
                for (int i = 0; i < CommonShieldEXP.Count; i++)
                    reader.Write(ShieldGrades[2].ToString(), "Level" + i, LegendaryShieldEXP[i]);
                for (int i = 0; i < CommonShieldEXP.Count; i++)
                    reader.Write(ShieldGrades[3].ToString(), "Level" + i, MythicalShieldEXP[i]);
                for (int i = 0; i < CommonShieldEXP.Count; i++)
                    reader.Write(ShieldGrades[4].ToString(), "Level" + i, QuestShieldEXP[i]);
            }
        }

        public static void SaveShieldConfig()
        {
            File.Delete(ItemUpgrades + @".\ShieldConfig.ini");
            InIReader reader = new InIReader(ItemUpgrades + @".\ShieldConfig.ini");
            for (int i = 0; i < ShieldConfig.Length; i++)
            {
                reader.Write(ShieldGrades[i].ToString(), "MinDC", ShieldConfig[i].MinDC);
                reader.Write(ShieldGrades[i].ToString(), "MedDC", ShieldConfig[i].MedDC);
                reader.Write(ShieldGrades[i].ToString(), "MaxDC", ShieldConfig[i].MaxDC);
                reader.Write(ShieldGrades[i].ToString(), "MinDCRate", ShieldConfig[i].MinDCRate);
                reader.Write(ShieldGrades[i].ToString(), "MedDCRate", ShieldConfig[i].MedDCRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxDCRate", ShieldConfig[i].MaxDCRate);
                reader.Write(ShieldGrades[i].ToString(), "MinMC", ShieldConfig[i].MinMC);
                reader.Write(ShieldGrades[i].ToString(), "MedMC", ShieldConfig[i].MedMC);
                reader.Write(ShieldGrades[i].ToString(), "MaxMC", ShieldConfig[i].MaxMC);
                reader.Write(ShieldGrades[i].ToString(), "MinMCRate", ShieldConfig[i].MinMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MedMCRate", ShieldConfig[i].MedMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxMCRate", ShieldConfig[i].MaxMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MinSC", ShieldConfig[i].MinSC);
                reader.Write(ShieldGrades[i].ToString(), "MedSC", ShieldConfig[i].MedSC);
                reader.Write(ShieldGrades[i].ToString(), "MaxSC", ShieldConfig[i].MaxSC);
                reader.Write(ShieldGrades[i].ToString(), "MinSCRate", ShieldConfig[i].MinSCRate);
                reader.Write(ShieldGrades[i].ToString(), "MedSCRate", ShieldConfig[i].MedSCRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxSCRate", ShieldConfig[i].MaxSCRate);
                reader.Write(ShieldGrades[i].ToString(), "MinAC", ShieldConfig[i].MinAC);
                reader.Write(ShieldGrades[i].ToString(), "MedAC", ShieldConfig[i].MedAC);
                reader.Write(ShieldGrades[i].ToString(), "MaxAC", ShieldConfig[i].MaxAC);
                reader.Write(ShieldGrades[i].ToString(), "MinACRate", ShieldConfig[i].MinACRate);
                reader.Write(ShieldGrades[i].ToString(), "MedACRate", ShieldConfig[i].MedACRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxACRate", ShieldConfig[i].MaxACRate);
                reader.Write(ShieldGrades[i].ToString(), "MinAMC", ShieldConfig[i].MinAMC);
                reader.Write(ShieldGrades[i].ToString(), "MedAMC", ShieldConfig[i].MedAMC);
                reader.Write(ShieldGrades[i].ToString(), "MaxAMC", ShieldConfig[i].MaxAMC);
                reader.Write(ShieldGrades[i].ToString(), "MinAMCRate", ShieldConfig[i].MinAMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MedAMCRate", ShieldConfig[i].MedAMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxAMCRate", ShieldConfig[i].MaxAMCRate);
                reader.Write(ShieldGrades[i].ToString(), "MinHP", ShieldConfig[i].MinHP);
                reader.Write(ShieldGrades[i].ToString(), "MedHP", ShieldConfig[i].MedHP);
                reader.Write(ShieldGrades[i].ToString(), "MaxHP", ShieldConfig[i].MaxHP);
                reader.Write(ShieldGrades[i].ToString(), "MinHPRate", ShieldConfig[i].MinHPRate);
                reader.Write(ShieldGrades[i].ToString(), "MedHPRate", ShieldConfig[i].MedHPRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxHPRate", ShieldConfig[i].MaxHPRate);
                reader.Write(ShieldGrades[i].ToString(), "MinMP", ShieldConfig[i].MinMP);
                reader.Write(ShieldGrades[i].ToString(), "MedMP", ShieldConfig[i].MedMP);
                reader.Write(ShieldGrades[i].ToString(), "MaxMP", ShieldConfig[i].MaxMP);
                reader.Write(ShieldGrades[i].ToString(), "MinMPRate", ShieldConfig[i].MinMPRate);
                reader.Write(ShieldGrades[i].ToString(), "MedMPRate", ShieldConfig[i].MedMPRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxMPRate", ShieldConfig[i].MaxMPRate);
                reader.Write(ShieldGrades[i].ToString(), "MinAcc", ShieldConfig[i].MinAcc);
                reader.Write(ShieldGrades[i].ToString(), "MedAcc", ShieldConfig[i].MedAcc);
                reader.Write(ShieldGrades[i].ToString(), "MaxAcc", ShieldConfig[i].MaxAcc);
                reader.Write(ShieldGrades[i].ToString(), "MinAccRate", ShieldConfig[i].MinAccRate);
                reader.Write(ShieldGrades[i].ToString(), "MedAccRate", ShieldConfig[i].MedAccRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxAccRate", ShieldConfig[i].MaxAccRate);
                reader.Write(ShieldGrades[i].ToString(), "MinAgil", ShieldConfig[i].MinAgil);
                reader.Write(ShieldGrades[i].ToString(), "MedAgil", ShieldConfig[i].MedAgil);
                reader.Write(ShieldGrades[i].ToString(), "MaxAgil", ShieldConfig[i].MaxAgil);
                reader.Write(ShieldGrades[i].ToString(), "MinAgilRate", ShieldConfig[i].MinAgilRate);
                reader.Write(ShieldGrades[i].ToString(), "MedAgilRate", ShieldConfig[i].MedAgilRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxAgilRate", ShieldConfig[i].MaxAgilRate);
                reader.Write(ShieldGrades[i].ToString(), "MinCrit", ShieldConfig[i].MinCrit);
                reader.Write(ShieldGrades[i].ToString(), "MedCrit", ShieldConfig[i].MedCrit);
                reader.Write(ShieldGrades[i].ToString(), "MaxCrit", ShieldConfig[i].MaxCrit);
                reader.Write(ShieldGrades[i].ToString(), "MinCritRate", ShieldConfig[i].MinCritRate);
                reader.Write(ShieldGrades[i].ToString(), "MedCritRate", ShieldConfig[i].MedCritRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxCritRate", ShieldConfig[i].MaxCritRate);
                reader.Write(ShieldGrades[i].ToString(), "MinCritDmg", ShieldConfig[i].MinCritDmg);
                reader.Write(ShieldGrades[i].ToString(), "MedCritDmg", ShieldConfig[i].MedCritDmg);
                reader.Write(ShieldGrades[i].ToString(), "MaxCritDmg", ShieldConfig[i].MaxCritDmg);
                reader.Write(ShieldGrades[i].ToString(), "MinCritDmgRate", ShieldConfig[i].MinCritDmgRate);
                reader.Write(ShieldGrades[i].ToString(), "MedCritDmgRate", ShieldConfig[i].MedCritDmgRate);
                reader.Write(ShieldGrades[i].ToString(), "MaxCritDmgRate", ShieldConfig[i].MaxCritDmgRate);
            }
        }

        public static void ReloadShieldUpgrades()
        {
            CommonShieldEXP.Clear();
            RareShieldEXP.Clear();
            LegendaryShieldEXP.Clear();
            MythicalShieldEXP.Clear();
            QuestShieldEXP.Clear();

            if (MaxShieldLevel > 0)
            {
                InIReader reader = new InIReader(ItemUpgrades + @".\Shield.ini");
                for (int i = 0; i < MaxShieldLevel; i++)
                    CommonShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    RareShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    LegendaryShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    MythicalShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    QuestShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[0].ToString(), "Level" + i.ToString(), CommonShieldEXP[i]);
                    CommonShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[1].ToString(), "Level" + i.ToString(), RareShieldEXP[i]);
                    RareShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[2].ToString(), "Level" + i.ToString(), LegendaryShieldEXP[i]);
                    LegendaryShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[3].ToString(), "Level" + i.ToString(), MythicalShieldEXP[i]);
                    MythicalShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[4].ToString(), "Level" + i.ToString(), QuestShieldEXP[i]);
                    QuestShieldEXP[i] = tmp;
                }
            }
            {
                InIReader reader = new InIReader(ItemUpgrades + @".\ShieldConfig.ini");
                for (int i = 0; i < ShieldConfig.Length; i++)
                {
                    byte tmp = 0;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinDC", ShieldConfig[i].MinDC);
                    ShieldConfig[i].MinDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedDC", ShieldConfig[i].MedDC);
                    ShieldConfig[i].MedDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxDC", ShieldConfig[i].MaxDC);
                    ShieldConfig[i].MaxDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinDCRate", ShieldConfig[i].MinDCRate);
                    ShieldConfig[i].MinDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedDCRate", ShieldConfig[i].MedDCRate);
                    ShieldConfig[i].MedDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxDCRate", ShieldConfig[i].MaxDCRate);
                    ShieldConfig[i].MaxDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMC", ShieldConfig[i].MinMC);
                    ShieldConfig[i].MinMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMC", ShieldConfig[i].MedMC);
                    ShieldConfig[i].MedMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMC", ShieldConfig[i].MaxMC);
                    ShieldConfig[i].MaxMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMCRate", ShieldConfig[i].MinMCRate);
                    ShieldConfig[i].MinMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMCRate", ShieldConfig[i].MedMCRate);
                    ShieldConfig[i].MedMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMCRate", ShieldConfig[i].MaxMCRate);
                    ShieldConfig[i].MaxMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinSC", ShieldConfig[i].MinSC);
                    ShieldConfig[i].MinSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedSC", ShieldConfig[i].MedSC);
                    ShieldConfig[i].MedSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxSC", ShieldConfig[i].MaxSC);
                    ShieldConfig[i].MaxSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinSCRate", ShieldConfig[i].MinSCRate);
                    ShieldConfig[i].MinSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedSCRate", ShieldConfig[i].MedSCRate);
                    ShieldConfig[i].MedSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxSCRate", ShieldConfig[i].MaxSCRate);
                    ShieldConfig[i].MaxSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAC", ShieldConfig[i].MinAC);
                    ShieldConfig[i].MinAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAC", ShieldConfig[i].MedAC);
                    ShieldConfig[i].MedAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAC", ShieldConfig[i].MaxAC);
                    ShieldConfig[i].MaxAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinACRate", ShieldConfig[i].MinACRate);
                    ShieldConfig[i].MinACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedACRate", ShieldConfig[i].MedACRate);
                    ShieldConfig[i].MedACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxACRate", ShieldConfig[i].MaxACRate);
                    ShieldConfig[i].MaxACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAMC", ShieldConfig[i].MinAMC);
                    ShieldConfig[i].MinAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAMC", ShieldConfig[i].MedAMC);
                    ShieldConfig[i].MedAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAMC", ShieldConfig[i].MaxAMC);
                    ShieldConfig[i].MaxAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAMCRate", ShieldConfig[i].MinAMCRate);
                    ShieldConfig[i].MinAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAMCRate", ShieldConfig[i].MedAMCRate);
                    ShieldConfig[i].MedAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAMCRate", ShieldConfig[i].MaxAMCRate);
                    ShieldConfig[i].MaxAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinHP", ShieldConfig[i].MinHP);
                    ShieldConfig[i].MinHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedHP", ShieldConfig[i].MedHP);
                    ShieldConfig[i].MedHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxHP", ShieldConfig[i].MaxHP);
                    ShieldConfig[i].MaxHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinHPRate", ShieldConfig[i].MinHPRate);
                    ShieldConfig[i].MinHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedHPRate", ShieldConfig[i].MedHPRate);
                    ShieldConfig[i].MedHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxHPRate", ShieldConfig[i].MaxHPRate);
                    ShieldConfig[i].MaxHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMP", ShieldConfig[i].MinMP);
                    ShieldConfig[i].MinMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMP", ShieldConfig[i].MedMP);
                    ShieldConfig[i].MedMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMP", ShieldConfig[i].MaxMP);
                    ShieldConfig[i].MaxMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMPRate", ShieldConfig[i].MinMPRate);
                    ShieldConfig[i].MinMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMPRate", ShieldConfig[i].MedMPRate);
                    ShieldConfig[i].MedMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMPRate", ShieldConfig[i].MaxMPRate);
                    ShieldConfig[i].MaxMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAcc", ShieldConfig[i].MinAcc);
                    ShieldConfig[i].MinAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAcc", ShieldConfig[i].MedAcc);
                    ShieldConfig[i].MedAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAcc", ShieldConfig[i].MaxAcc);
                    ShieldConfig[i].MaxAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAccRate", ShieldConfig[i].MinAccRate);
                    ShieldConfig[i].MinAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAccRate", ShieldConfig[i].MedAccRate);
                    ShieldConfig[i].MedAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAccRate", ShieldConfig[i].MaxAccRate);
                    ShieldConfig[i].MaxAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAgil", ShieldConfig[i].MinAgil);
                    ShieldConfig[i].MinAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAgil", ShieldConfig[i].MedAgil);
                    ShieldConfig[i].MedAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAgil", ShieldConfig[i].MaxAgil);
                    ShieldConfig[i].MaxAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAgilRate", ShieldConfig[i].MinAgilRate);
                    ShieldConfig[i].MinAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAgilRate", ShieldConfig[i].MedAgilRate);
                    ShieldConfig[i].MedAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAgilRate", ShieldConfig[i].MaxAgilRate);
                    ShieldConfig[i].MaxAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCrit", ShieldConfig[i].MinCrit);
                    ShieldConfig[i].MinCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCrit", ShieldConfig[i].MedCrit);
                    ShieldConfig[i].MedCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCrit", ShieldConfig[i].MaxCrit);
                    ShieldConfig[i].MaxCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritRate", ShieldConfig[i].MinCritRate);
                    ShieldConfig[i].MinCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritRate", ShieldConfig[i].MedCritRate);
                    ShieldConfig[i].MedCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritRate", ShieldConfig[i].MaxCritRate);
                    ShieldConfig[i].MaxCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritDmg", ShieldConfig[i].MinCritDmg);
                    ShieldConfig[i].MinCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritDmg", ShieldConfig[i].MedCritDmg);
                    ShieldConfig[i].MedCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritDmg", ShieldConfig[i].MaxCritDmg);
                    ShieldConfig[i].MaxCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritDmgRate", ShieldConfig[i].MinCritDmgRate);
                    ShieldConfig[i].MinCritDmgRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritDmgRate", ShieldConfig[i].MedCritDmgRate);
                    ShieldConfig[i].MedCritDmgRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritDmgRate", ShieldConfig[i].MaxCritDmgRate);
                    ShieldConfig[i].MaxCritDmgRate = tmp;
                }
            }
        }

        public static void LoadShieldUpgrades()
        {
            if (!File.Exists(ItemUpgrades + @".\Shield.ini"))
            {
                SaveShieldUpgrades();
                return;
            }
            if (MaxShieldLevel > 0)
            {
                InIReader reader = new InIReader(ItemUpgrades + @".\Shield.ini");
                for (int i = 0; i < MaxShieldLevel; i++)
                    CommonShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    RareShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    LegendaryShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    MythicalShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                    QuestShieldEXP.Add(1000 * i);
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[0].ToString(), "Level" + i.ToString(), CommonShieldEXP[i]);
                    CommonShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[1].ToString(), "Level" + i.ToString(), RareShieldEXP[i]);
                    RareShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[2].ToString(), "Level" + i.ToString(), LegendaryShieldEXP[i]);
                    LegendaryShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[3].ToString(), "Level" + i.ToString(), MythicalShieldEXP[i]);
                    MythicalShieldEXP[i] = tmp;
                }
                for (int i = 0; i < MaxShieldLevel; i++)
                {
                    long tmp = reader.ReadInt64(ShieldGrades[4].ToString(), "Level" + i.ToString(), QuestShieldEXP[i]);
                    QuestShieldEXP[i] = tmp;
                }
            }
            if (!File.Exists(ItemUpgrades + @".\ShieldConfig.ini"))
            {
                SaveShieldConfig();
                return;
            }
            else
            {
                InIReader reader = new InIReader(ItemUpgrades + @".\ShieldConfig.ini");
                for (int i = 0; i < ShieldConfig.Length; i++)
                {
                    byte tmp = 0;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinDC", ShieldConfig[i].MinDC);
                    ShieldConfig[i].MinDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedDC", ShieldConfig[i].MedDC);
                    ShieldConfig[i].MedDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxDC", ShieldConfig[i].MaxDC);
                    ShieldConfig[i].MaxDC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinDCRate", ShieldConfig[i].MinDCRate);
                    ShieldConfig[i].MinDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedDCRate", ShieldConfig[i].MedDCRate);
                    ShieldConfig[i].MedDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxDCRate", ShieldConfig[i].MaxDCRate);
                    ShieldConfig[i].MaxDCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMC", ShieldConfig[i].MinMC);
                    ShieldConfig[i].MinMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMC", ShieldConfig[i].MedMC);
                    ShieldConfig[i].MedMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMC", ShieldConfig[i].MaxMC);
                    ShieldConfig[i].MaxMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMCRate", ShieldConfig[i].MinMCRate);
                    ShieldConfig[i].MinMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMCRate", ShieldConfig[i].MedMCRate);
                    ShieldConfig[i].MedMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMCRate", ShieldConfig[i].MaxMCRate);
                    ShieldConfig[i].MaxMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinSC", ShieldConfig[i].MinSC);
                    ShieldConfig[i].MinSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedSC", ShieldConfig[i].MedSC);
                    ShieldConfig[i].MedSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxSC", ShieldConfig[i].MaxSC);
                    ShieldConfig[i].MaxSC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinSCRate", ShieldConfig[i].MinSCRate);
                    ShieldConfig[i].MinSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedSCRate", ShieldConfig[i].MedSCRate);
                    ShieldConfig[i].MedSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxSCRate", ShieldConfig[i].MaxSCRate);
                    ShieldConfig[i].MaxSCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAC", ShieldConfig[i].MinAC);
                    ShieldConfig[i].MinAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAC", ShieldConfig[i].MedAC);
                    ShieldConfig[i].MedAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAC", ShieldConfig[i].MaxAC);
                    ShieldConfig[i].MaxAC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinACRate", ShieldConfig[i].MinACRate);
                    ShieldConfig[i].MinACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedACRate", ShieldConfig[i].MedACRate);
                    ShieldConfig[i].MedACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxACRate", ShieldConfig[i].MaxACRate);
                    ShieldConfig[i].MaxACRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAMC", ShieldConfig[i].MinAMC);
                    ShieldConfig[i].MinAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAMC", ShieldConfig[i].MedAMC);
                    ShieldConfig[i].MedAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAMC", ShieldConfig[i].MaxAMC);
                    ShieldConfig[i].MaxAMC = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAMCRate", ShieldConfig[i].MinAMCRate);
                    ShieldConfig[i].MinAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAMCRate", ShieldConfig[i].MedAMCRate);
                    ShieldConfig[i].MedAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAMCRate", ShieldConfig[i].MaxAMCRate);
                    ShieldConfig[i].MaxAMCRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinHP", ShieldConfig[i].MinHP);
                    ShieldConfig[i].MinHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedHP", ShieldConfig[i].MedHP);
                    ShieldConfig[i].MedHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxHP", ShieldConfig[i].MaxHP);
                    ShieldConfig[i].MaxHP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinHPRate", ShieldConfig[i].MinHPRate);
                    ShieldConfig[i].MinHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedHPRate", ShieldConfig[i].MedHPRate);
                    ShieldConfig[i].MedHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxHPRate", ShieldConfig[i].MaxHPRate);
                    ShieldConfig[i].MaxHPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMP", ShieldConfig[i].MinMP);
                    ShieldConfig[i].MinMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMP", ShieldConfig[i].MedMP);
                    ShieldConfig[i].MedMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMP", ShieldConfig[i].MaxMP);
                    ShieldConfig[i].MaxMP = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinMPRate", ShieldConfig[i].MinMPRate);
                    ShieldConfig[i].MinMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedMPRate", ShieldConfig[i].MedMPRate);
                    ShieldConfig[i].MedMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxMPRate", ShieldConfig[i].MaxMPRate);
                    ShieldConfig[i].MaxMPRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAcc", ShieldConfig[i].MinAcc);
                    ShieldConfig[i].MinAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAcc", ShieldConfig[i].MedAcc);
                    ShieldConfig[i].MedAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAcc", ShieldConfig[i].MaxAcc);
                    ShieldConfig[i].MaxAcc = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAccRate", ShieldConfig[i].MinAccRate);
                    ShieldConfig[i].MinAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAccRate", ShieldConfig[i].MedAccRate);
                    ShieldConfig[i].MedAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAccRate", ShieldConfig[i].MaxAccRate);
                    ShieldConfig[i].MaxAccRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAgil", ShieldConfig[i].MinAgil);
                    ShieldConfig[i].MinAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAgil", ShieldConfig[i].MedAgil);
                    ShieldConfig[i].MedAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAgil", ShieldConfig[i].MaxAgil);
                    ShieldConfig[i].MaxAgil = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinAgilRate", ShieldConfig[i].MinAgilRate);
                    ShieldConfig[i].MinAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedAgilRate", ShieldConfig[i].MedAgilRate);
                    ShieldConfig[i].MedAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxAgilRate", ShieldConfig[i].MaxAgilRate);
                    ShieldConfig[i].MaxAgilRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCrit", ShieldConfig[i].MinCrit);
                    ShieldConfig[i].MinCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCrit", ShieldConfig[i].MedCrit);
                    ShieldConfig[i].MedCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCrit", ShieldConfig[i].MaxCrit);
                    ShieldConfig[i].MaxCrit = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritRate", ShieldConfig[i].MinCritRate);
                    ShieldConfig[i].MinCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritRate", ShieldConfig[i].MedCritRate);
                    ShieldConfig[i].MedCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritRate", ShieldConfig[i].MaxCritRate);
                    ShieldConfig[i].MaxCritRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritDmg", ShieldConfig[i].MinCritDmg);
                    ShieldConfig[i].MinCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritDmg", ShieldConfig[i].MedCritDmg);
                    ShieldConfig[i].MedCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritDmg", ShieldConfig[i].MaxCritDmg);
                    ShieldConfig[i].MaxCritDmg = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MinCritDmgRate", ShieldConfig[i].MinCritDmgRate);
                    ShieldConfig[i].MinCritDmgRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MedCritDmgRate", ShieldConfig[i].MedCritDmgRate);
                    ShieldConfig[i].MedCritDmgRate = tmp;
                    tmp = reader.ReadByte(ShieldGrades[i].ToString(), "MaxCritDmgRate", ShieldConfig[i].MaxCritDmgRate);
                    ShieldConfig[i].MaxCritDmgRate = tmp;
                }
            }
        }

        public static void Save()
        {
            //General
            Reader.Write("General", "VersionPath", VersionPath);
            Reader.Write("General", "CheckVersion", CheckVersion);
            Reader.Write("General", "RelogDelay", RelogDelay);
            Reader.Write("General", "Multithreaded", Multithreaded);
            Reader.Write("General", "ThreadLimit", ThreadLimit);
            Reader.Write("General", "TestServer", TestServer);
            Reader.Write("General", "EnforceDBChecks", EnforceDBChecks);
            
            //Paths
            Reader.Write("Network", "IPAddress", IPAddress);
            Reader.Write("Network", "Port", Port);
            Reader.Write("Network", "TimeOut", TimeOut);
            Reader.Write("Network", "MaxUser", MaxUser);
            Reader.Write("Network", "MaxIP", MaxIP);
            Reader.Write("Network", "StatusConPort", StatusConPort);
            //Permission
            Reader.Write("Permission", "AllowNewAccount", AllowNewAccount);
            Reader.Write("Permission", "AllowChangePassword", AllowChangePassword);
            Reader.Write("Permission", "AllowLogin", AllowLogin);
            Reader.Write("Permission", "AllowNewCharacter", AllowNewCharacter);
            Reader.Write("Permission", "AllowDeleteCharacter", AllowDeleteCharacter);
            Reader.Write("Permission", "AllowStartGame", AllowStartGame);
            Reader.Write("Permission", "AllowCreateAssassin", AllowCreateAssassin);
            Reader.Write("Permission", "AllowCreateArcher", AllowCreateArcher);
            Reader.Write("Permission", "MaxResolution", AllowedResolution);

            //Optional
            Reader.Write("Optional", "SafeZoneBorder", SafeZoneBorder);
            Reader.Write("Optional", "SafeZoneHealing", SafeZoneHealing);
            Reader.Write("Optional", "GatherOrbsPerLevel", GatherOrbsPerLevel);
            Reader.Write("Optional", "ExpMobLevelDifference", ExpMobLevelDifference);
            Reader.Write("Optional", "ShowGMEffect", ShowGMEffect);

            //Database
            Reader.Write("Database", "SaveDelay", SaveDelay);
            Reader.Write("Database", "CredxGold", CredxGold);
            //MySQL Database
            Reader.Write("MySQLDataBase", "UseSQL", UseSQL);
            Reader.Write("MySQLDataBase", "SQL_UName", SQL_UName);
            Reader.Write("MySQLDataBase", "SQL_Password", SQL_Password);
            Reader.Write("MySQLDataBase", "SQL_IP", SQL_IP);
            Reader.Write("MySQLDataBase", "SQL_PORT", SQL_PORT);
            Reader.Write("MySQLDataBase", "SQL_DBName", SQL_DBName);
            //Game Settings
            Reader.Write("GameSettings", "StartLevel", StartLevel);
            Reader.Write("GameSettings", "StartGold", StartGold);
            Reader.Write("GameSettings", "BuyGTGold", BuyGTGold);
            Reader.Write("GameSettings", "GTDays", GTDays);
            Reader.Write("GameSettings", "ExtendGT", ExtendGT);
            Reader.Write("GameSettings", "DropRate", DropRate);
            Reader.Write("GameSettings", "ExpRate", ExpRate);
            Reader.Write("GameSettings", "QuestDropRate", QuestDropRate);
            Reader.Write("GameSettings", "QuestGoldRate", QuestGoldRate);
            Reader.Write("GameSettings", "QuestExpRate", QuestExpRate);
            Reader.Write("GameSettings", "NewbieName", NewbieName);
            Reader.Write("GameSettings", "NewbieExpBuff", NewbieExpBuff);
            Reader.Write("GameSettings", "NewbieMaxLevel", NewbieMaxLevel);
            Reader.Write("GameSettings", "VIPExp", VIPExp);
            Reader.Write("GameSettings", "ItemTimeOut", ItemTimeOut);
            Reader.Write("GameSettings", "PlayerDiedItemTimeOut", PlayerDiedItemTimeOut);
            Reader.Write("GameSettings", "PetTimeOut", PetTimeOut);
            Reader.Write("GameSettings", "PetSave", PetSave);
            Reader.Write("GameSettings", "PKDelay", PKDelay);
            Reader.Write("GameSettings", "TDBStunChancePvE", TDBPvEChance);
            Reader.Write("GameSettings", "TDBStunChancePvP", TDBPvPChance);
            Reader.Write("GameSettings", "TDBPoisonLevelRange", TDBPoisonLevelRange);
            Reader.Write("GameSettings", "FlashDashStunChance", FlashDashChance);
            Reader.Write("GameSettings", "FatalSwordChance", FatalSwordChance);
            Reader.Write("GameSettings", "MagicShieldBaseDuration", MagicShieldBaseDuration);
            Reader.Write("GameSetting", "MagicShieldBaseReduction", MagicShieldBaseReduction);
            Reader.Write("GameSettings", "MagicShieldStruckTimeReduction", MagicShieldStruckTimeReduction);
            Reader.Write("GameSettings", "DeadTimeDelay", DeadTimeDelay);
            Reader.Write("GameSettings", "NonRedEquipDropLimit", NonRedEquipDropLimit);
            Reader.Write("GameSettings", "NonRedInvDropLimit", NonRedInvDropLimit);
            Reader.Write("GameSettings", "RedEquipDropLimit", RedEquipDropLimit);
            Reader.Write("GameSettings", "RedInvDropLimit", RedInvDropLimit);
            //Taoist Pets
            Reader.Write("TaoistPets", "SkeletonName", SkeletonName);
            Reader.Write("TaoistPets", "SuperSkeletonName1", SuperSkeletonName1);
            Reader.Write("TaoistPets", "SuperSkeletonName2", SuperSkeletonName2);
            Reader.Write("TaoistPets", "SuperSkeletonName3", SuperSkeletonName3);
            Reader.Write("TaoistPets", "ShinsuName", ShinsuName);
            Reader.Write("TaoistPets", "SuperShinsuName", SuperShinsuName);
            Reader.Write("TaoistPets", "SuperShinsuName2", SuperShinsuName2);
            Reader.Write("TaoistPets", "SuperShinsuName4", SuperShinsuName4);
            Reader.Write("TaoistPets", "AngelName", AngelName);
            Reader.Write("TaoistPets", "AngelName1", AngelName1);
            Reader.Write("TaoistPets", "DragonName", DragonName);
            Reader.Write("TaoistPets", "DragonName1", DragonName1);

            //Elite System
            Reader.Write("EliteSystem", "ChanceToBeElite", ChanceToBeElite);
            Reader.Write("EliteSystem", "EliteLevel1Bonus", EliteLevel1Bonus);
            Reader.Write("EliteSystem", "EliteLevel2Bonus", EliteLevel2Bonus);
            Reader.Write("EliteSystem", "EliteLevel3Bonus", EliteLevel3Bonus);
            Reader.Write("EliteSystem", "EliteLevel4Bonus", EliteLevel4Bonus);
            Reader.Write("EliteSystem", "EliteLevel5Bonus", EliteLevel5Bonus);
            Reader.Write("EliteSystem", "EliteLevel6Bonus", EliteLevel6Bonus);
            Reader.Write("EliteSystem", "EliteLevel7Bonus", EliteLevel7Bonus);
            Reader.Write("EliteSystem", "EliteLevel8Bonus", EliteLevel8Bonus);
            
            Reader.Write("EliteSystem", string.Format("EliteLevel1Rate"), EliteDropRate[0]);
            Reader.Write("EliteSystem", string.Format("EliteLevel2Rate"), EliteDropRate[1]);
            Reader.Write("EliteSystem", string.Format("EliteLevel3Rate"), EliteDropRate[2]);
            Reader.Write("EliteSystem", string.Format("EliteLevel4Rate"), EliteDropRate[3]);
            Reader.Write("EliteSystem", string.Format("EliteLevel5Rate"), EliteDropRate[4]);
            Reader.Write("EliteSystem", string.Format("EliteLevel6Rate"), EliteDropRate[5]);
            Reader.Write("EliteSystem", string.Format("EliteLevel7Rate"), EliteDropRate[6]);
            Reader.Write("EliteSystem", string.Format("EliteLevel8Rate"), EliteDropRate[7]);
            Reader.Write("EliteSystem", string.Format("EliteLevel2Chance"), EliteChances[0]);
            Reader.Write("EliteSystem", string.Format("EliteLevel3Chance"), EliteChances[1]);
            Reader.Write("EliteSystem", string.Format("EliteLevel4Chance"), EliteChances[2]);
            Reader.Write("EliteSystem", string.Format("EliteLevel5Chance"), EliteChances[3]);
            Reader.Write("EliteSystem", string.Format("EliteLevel6Chance"), EliteChances[4]);
            Reader.Write("EliteSystem", string.Format("EliteLevel7Chance"), EliteChances[5]);
            Reader.Write("EliteSystem", string.Format("EliteLevel8Chance"), EliteChances[6]);
            Reader.Write("EliteSystem", string.Format("EliteLevel1XPBoost"), EliteExpBoost[0]);
            Reader.Write("EliteSystem", string.Format("EliteLevel2XPBoost"), EliteExpBoost[1]);
            Reader.Write("EliteSystem", string.Format("EliteLevel3XPBoost"), EliteExpBoost[2]);
            Reader.Write("EliteSystem", string.Format("EliteLevel4XPBoost"), EliteExpBoost[3]);
            Reader.Write("EliteSystem", string.Format("EliteLevel5XPBoost"), EliteExpBoost[4]);
            Reader.Write("EliteSystem", string.Format("EliteLevel6XPBoost"), EliteExpBoost[5]);
            Reader.Write("EliteSystem", string.Format("EliteLevel7XPBoost"), EliteExpBoost[6]);
            Reader.Write("EliteSystem", string.Format("EliteLevel8XPBoost"), EliteExpBoost[7]);
            Reader.Write("HeroStore", "Cap0", HeroStoreCaps[0]);
            Reader.Write("HeroStore", "Cap1", HeroStoreCaps[1]);
            Reader.Write("HeroStore", "Cap2", HeroStoreCaps[2]);
            Reader.Write("HeroStore", "Cap3", HeroStoreCaps[3]);
            Reader.Write("HeroStore", "Cap4", HeroStoreCaps[4]);
            Reader.Write("HeroStore", "Cap5", HeroStoreCaps[5]);
            Reader.Write("HeroStore", "Cap6", HeroStoreCaps[6]);
            Reader.Write("HeroStore", "Cap7", HeroStoreCaps[7]);
            //Monster Settings
            Reader.Write("Game", "BugBatName", BugBatName);
            Reader.Write("Game", "Zuma1", Zuma1);
            Reader.Write("Game", "Zuma2", Zuma2);
            Reader.Write("Game", "Zuma3", Zuma3);
            Reader.Write("Game", "Zuma4", Zuma4);
            Reader.Write("Game", "Zuma5", Zuma5);
            Reader.Write("Game", "Zuma6", Zuma6);
            Reader.Write("Game", "Zuma7", Zuma7);
            Reader.Write("Game", "Jar1Mob", Jar1Mob);
            Reader.Write("Game", "Jar2Mob", Jar2Mob);
            Reader.Write("Game", "Jar3Mob", Jar3Mob);
            Reader.Write("Game", "Turtle1", Turtle1);
            Reader.Write("Game", "Turtle2", Turtle2);
            Reader.Write("Game", "Turtle3", Turtle3);
            Reader.Write("Game", "Turtle4", Turtle4);
            Reader.Write("Game", "Turtle5", Turtle5);
            Reader.Write("Game", "BoneMonster1", BoneMonster1);
            Reader.Write("Game", "BoneMonster2", BoneMonster2);
            Reader.Write("Game", "BoneMonster3", BoneMonster3);
            Reader.Write("Game", "BoneMonster4", BoneMonster4);
            Reader.Write("Game", "BehemothMonster1", BehemothMonster1);
            Reader.Write("Game", "BehemothMonster2", BehemothMonster2);
            Reader.Write("Game", "BehemothMonster3", BehemothMonster3);
            Reader.Write("Game", "HellKnight1", HellKnight1);
            Reader.Write("Game", "HellKnight2", HellKnight2);
            Reader.Write("Game", "HellKnight3", HellKnight3);
            Reader.Write("Game", "HellKnight4", HellKnight4);
            Reader.Write("Game", "HellBomb1", HellBomb1);
            Reader.Write("Game", "HellBomb2", HellBomb2);
            Reader.Write("Game", "HellBomb3", HellBomb3);
            Reader.Write("Game", "WhiteSnake", WhiteSnake);
            Reader.Write("Game", "BombSpiderName", BombSpiderName);
            Reader.Write("Game", "CloneName", CloneName);
            Reader.Write("Game", "AssassinCloneName", AssassinCloneName);
            Reader.Write("Game", "VampireName", VampireName);
            Reader.Write("Game", "ToadName", ToadName);
            Reader.Write("Game", "SnakeTotemName", SnakeTotemName);
            Reader.Write("Game", "SnakesName", SnakesName);
            Reader.Write("Game", "LordMonster1", LordMonster1);
            Reader.Write("Game", "LordMonster2", LordMonster2);
            Reader.Write("Game", "LordMonster3", LordMonster3);
            Reader.Write("Game", "LordMonster4", LordMonster4);
            Reader.Write("Game", "AncZuma1", AncZuma1);
            Reader.Write("Game", "AncZuma2", AncZuma2);
            Reader.Write("Game", "AncZuma3", AncZuma3);
            Reader.Write("Game", "AncZuma4", AncZuma4);
            Reader.Write("Game", "AncZuma5", AncZuma5);
            Reader.Write("Game", "AncZuma6", AncZuma6);
            Reader.Write("Game", "AncZuma7", AncZuma7);
            Reader.Write("Game", "AncBoneMonster1", AncBoneMonster1);
            Reader.Write("Game", "AncBoneMonster2", AncBoneMonster2);
            Reader.Write("Game", "AncBoneMonster3", AncBoneMonster3);
            Reader.Write("Game", "AncBoneMonster4", AncBoneMonster4);


            Reader.Write("Rested", "Period", RestedPeriod);
            Reader.Write("Rested", "BuffLength", RestedBuffLength);
            Reader.Write("Rested", "ExpBonus", RestedExpBonus);
            Reader.Write("Rested", "MaxBonus", RestedMaxBonus);

            Reader.Write("Items", "HealRing", HealRing);
            Reader.Write("Items", "FireRing", FireRing);

            Reader.Write("Group", "GrpWar", GrpWar);
            Reader.Write("Group", "GrpWiz", GrpWiz);
            Reader.Write("Group", "GrpTao", GrpTao);
            Reader.Write("Group", "GrpSin", GrpSin);
            Reader.Write("Group", "GrpArc", GrpArc);
            Reader.Write("Group", "GrpNone", GrpNone);
            Reader.Write("Group", "ExpStageIncrease", ExpStageIncrease);

            Reader.Write("PKTown", "PKTownMapName", PKTownMapName);
            Reader.Write("PKTown", "PKTownPositionX", PKTownPositionX);
            Reader.Write("PKTown", "PKTownPositionY", PKTownPositionY);

            Reader.Write("DropGold", "DropGold", DropGold);
            Reader.Write("DropGold", "MaxDropGold", MaxDropGold);

            Reader.Write("Items", "MaxMagicResist", MaxMagicResist);
            Reader.Write("Items", "MagicResistWeight", MagicResistWeight);
            Reader.Write("Items", "MaxPoisonResist", MaxPoisonResist);
            Reader.Write("Items", "PoisonResistWeight", PoisonResistWeight);
            Reader.Write("Items", "MaxCriticalRate", MaxCriticalRate);
            Reader.Write("Items", "CriticalRateWeight", CriticalRateWeight);
            Reader.Write("Items", "MaxCriticalDamage", MaxCriticalDamage);
            Reader.Write("Items", "CriticalDamageWeight", CriticalDamageWeight);
            Reader.Write("Items", "MaxFreezing", MaxFreezing);
            Reader.Write("Items", "FreezingAttackWeight", FreezingAttackWeight);
            Reader.Write("Items", "MaxPoisonAttack", MaxPoisonAttack);
            Reader.Write("Items", "PoisonAttackWeight", PoisonAttackWeight);
            Reader.Write("Items", "MaxHealthRegen", MaxHealthRegen);
            Reader.Write("Items", "HealthRegenWeight", HealthRegenWeight);
            Reader.Write("Items", "MaxManaRegen", MaxManaRegen);
            Reader.Write("Items", "ManaRegenWeight", ManaRegenWeight);
            Reader.Write("Items", "MaxPoisonRecovery", MaxPoisonRecovery);
            Reader.Write("Items", "MaxLuck", MaxLuck);
            Reader.Write("Items", "PvpCanResistMagic", PvpCanResistMagic);
            Reader.Write("Items", "PvpCanResistPoison", PvpCanResistPoison);
            Reader.Write("Items", "PvpCanFreeze", PvpCanFreeze);
            Reader.Write("Items", "PerTickRegen", PerTickRegen);

            Reader.Write("Custom", "CrystalBeastSlave", CrystalBeastSlave);
            Reader.Write("Custom", "PercentPotionDelay", PercentPotionDelay);
            Reader.Write("Custom", "MaxShieldLevel", MaxShieldLevel);
            Reader.Write("Custom", "ShieldEXPDivision", ShieldEXPDivision);

            //IntelligentCreature
            for (int i = 0; i < IntelligentCreatureNameList.Length; i++)
                Reader.Write("IntelligentCreatures", "Creature" + i.ToString() + "Name", IntelligentCreatureNameList[i]);
            Reader.Write("IntelligentCreatures", "CreatureBlackStoneName", CreatureBlackStoneName);
            SaveAwakeAttribute();
            SaveSWBuffInfo();
        }

        public static void LoadEXP()
        {
            long exp = 100;
            InIReader reader = new InIReader(ConfigPath + @".\ExpList.ini");

            for (int i = 1; i <= 500; i++)
            {
                exp = reader.ReadInt64("Exp", "Level" + i, exp);
                ExperienceList.Add(exp);
            }

            //ArcherSpells - Elemental system
            reader = new InIReader(ConfigPath + @".\OrbsExpList.ini");
            for (int i = 1; i <= 4; i++)
            {
                exp = i * 50;//default exp value
                exp = reader.ReadInt64("Exp", "Orb" + i, exp);
                OrbsExpList.Add(exp);
                exp = i * 2;//default defense value
                exp = reader.ReadInt64("Def", "Orb" + i, exp);
                OrbsDefList.Add(exp);
                exp = i * 4;//default power value
                exp = reader.ReadInt64("Att", "Orb" + i, exp);
                OrbsDmgList.Add(exp);
            }
        }

        public static void LoadBaseStats()
        {
            if (!File.Exists(ConfigPath + @".\BaseStats.ini"))
            {
                SaveBaseStats();
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\BaseStats.ini");

            for (int i = 0; i < ClassBaseStats.Length; i++)
            {
                ClassBaseStats[i].HpGain = reader.ReadFloat(BaseStatClassNames[i], "HpGain", ClassBaseStats[i].HpGain);
                ClassBaseStats[i].HpGainRate = reader.ReadFloat(BaseStatClassNames[i], "HpGainRate", ClassBaseStats[i].HpGainRate);
                ClassBaseStats[i].MpGainRate = reader.ReadFloat(BaseStatClassNames[i], "MpGainRate", ClassBaseStats[i].MpGainRate);
                ClassBaseStats[i].BagWeightGain = reader.ReadFloat(BaseStatClassNames[i], "BagWeightGain", ClassBaseStats[i].BagWeightGain);
                ClassBaseStats[i].WearWeightGain = reader.ReadFloat(BaseStatClassNames[i], "WearWeightGain", ClassBaseStats[i].WearWeightGain);
                ClassBaseStats[i].HandWeightGain = reader.ReadFloat(BaseStatClassNames[i], "HandWeightGain", ClassBaseStats[i].HandWeightGain);
                ClassBaseStats[i].MinAc = reader.ReadByte(BaseStatClassNames[i], "MinAc", ClassBaseStats[i].MinAc);
                ClassBaseStats[i].MaxAc = reader.ReadByte(BaseStatClassNames[i], "MaxAc", ClassBaseStats[i].MaxAc);
                ClassBaseStats[i].MinMac = reader.ReadByte(BaseStatClassNames[i], "MinMac", ClassBaseStats[i].MinMac);
                ClassBaseStats[i].MaxMac = reader.ReadByte(BaseStatClassNames[i], "MaxMac", ClassBaseStats[i].MaxMac);
                ClassBaseStats[i].MinDc = reader.ReadByte(BaseStatClassNames[i], "MinDc", ClassBaseStats[i].MinDc);
                ClassBaseStats[i].MaxDc = reader.ReadByte(BaseStatClassNames[i], "MaxDc", ClassBaseStats[i].MaxDc);
                ClassBaseStats[i].MinMc = reader.ReadByte(BaseStatClassNames[i], "MinMc", ClassBaseStats[i].MinMc);
                ClassBaseStats[i].MaxMc = reader.ReadByte(BaseStatClassNames[i], "MaxMc", ClassBaseStats[i].MaxMc);
                ClassBaseStats[i].MinSc = reader.ReadByte(BaseStatClassNames[i], "MinSc", ClassBaseStats[i].MinSc);
                ClassBaseStats[i].MaxSc = reader.ReadByte(BaseStatClassNames[i], "MaxSc", ClassBaseStats[i].MaxSc);
                ClassBaseStats[i].StartAgility = reader.ReadByte(BaseStatClassNames[i], "StartAgility", ClassBaseStats[i].StartAgility);
                ClassBaseStats[i].StartAccuracy = reader.ReadByte(BaseStatClassNames[i], "StartAccuracy", ClassBaseStats[i].StartAccuracy);
                ClassBaseStats[i].StartCriticalRate = reader.ReadByte(BaseStatClassNames[i], "StartCriticalRate", ClassBaseStats[i].StartCriticalRate);
                ClassBaseStats[i].StartCriticalDamage = reader.ReadByte(BaseStatClassNames[i], "StartCriticalDamage", ClassBaseStats[i].StartCriticalDamage);
                ClassBaseStats[i].CritialRateGain = reader.ReadByte(BaseStatClassNames[i], "CritialRateGain", ClassBaseStats[i].CritialRateGain);
                ClassBaseStats[i].CriticalDamageGain = reader.ReadByte(BaseStatClassNames[i], "CriticalDamageGain", ClassBaseStats[i].CriticalDamageGain);
            }
        }
        public static void SaveBaseStats()
        {
            File.Delete(ConfigPath + @".\BaseStats.ini");
            InIReader reader = new InIReader(ConfigPath + @".\BaseStats.ini");

            for (int i = 0; i < ClassBaseStats.Length; i++)
            {
                reader.Write(BaseStatClassNames[i], "HpGain", ClassBaseStats[i].HpGain);
                reader.Write(BaseStatClassNames[i], "HpGainRate", ClassBaseStats[i].HpGainRate);
                reader.Write(BaseStatClassNames[i], "MpGainRate", ClassBaseStats[i].MpGainRate);
                reader.Write(BaseStatClassNames[i], "BagWeightGain", ClassBaseStats[i].BagWeightGain);
                reader.Write(BaseStatClassNames[i], "WearWeightGain", ClassBaseStats[i].WearWeightGain);
                reader.Write(BaseStatClassNames[i], "HandWeightGain", ClassBaseStats[i].HandWeightGain);
                reader.Write(BaseStatClassNames[i], "MinAc", ClassBaseStats[i].MinAc);
                reader.Write(BaseStatClassNames[i], "MaxAc", ClassBaseStats[i].MaxAc);
                reader.Write(BaseStatClassNames[i], "MinMac", ClassBaseStats[i].MinMac);
                reader.Write(BaseStatClassNames[i], "MaxMac", ClassBaseStats[i].MaxMac);
                reader.Write(BaseStatClassNames[i], "MinDc", ClassBaseStats[i].MinDc);
                reader.Write(BaseStatClassNames[i], "MaxDc", ClassBaseStats[i].MaxDc);
                reader.Write(BaseStatClassNames[i], "MinMc", ClassBaseStats[i].MinMc);
                reader.Write(BaseStatClassNames[i], "MaxMc", ClassBaseStats[i].MaxMc);
                reader.Write(BaseStatClassNames[i], "MinSc", ClassBaseStats[i].MinSc);
                reader.Write(BaseStatClassNames[i], "MaxSc", ClassBaseStats[i].MaxSc);
                reader.Write(BaseStatClassNames[i], "StartAgility", ClassBaseStats[i].StartAgility);
                reader.Write(BaseStatClassNames[i], "StartAccuracy", ClassBaseStats[i].StartAccuracy);
                reader.Write(BaseStatClassNames[i], "StartCriticalRate", ClassBaseStats[i].StartCriticalRate);
                reader.Write(BaseStatClassNames[i], "StartCriticalDamage", ClassBaseStats[i].StartCriticalDamage);
                reader.Write(BaseStatClassNames[i], "CritialRateGain", ClassBaseStats[i].CritialRateGain);
                reader.Write(BaseStatClassNames[i], "CriticalDamageGain", ClassBaseStats[i].CriticalDamageGain);
            }
        }
        public static void LoadRandomItemStats()
        {
            if (!File.Exists(ConfigPath + @".\RandomItemStats.ini"))
            {
                RandomItemStatsList.Add(new RandomItemStat());
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Weapon));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Armour));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Helmet));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Necklace));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Bracelet));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Ring));
                RandomItemStatsList.Add(new RandomItemStat(ItemType.Belt));
                SaveRandomItemStats();
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\RandomItemStats.ini");
            int i = 0;
            RandomItemStat stat;
            while (reader.ReadByte("Item" + i.ToString(),"MaxDuraChance",255) != 255)
            {
                stat = new RandomItemStat
                {
                    MaxDuraChance = reader.ReadByte("Item" + i.ToString(), "MaxDuraChance", 0),
                    MaxDuraStatChance = reader.ReadByte("Item" + i.ToString(), "MaxDuraStatChance", 1),
                    MaxDuraMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxDuraMaxStat", 1),
                    MaxAcChance = reader.ReadByte("Item" + i.ToString(), "MaxAcChance", 0),
                    MaxAcStatChance = reader.ReadByte("Item" + i.ToString(), "MaxAcStatChance", 1),
                    MaxAcMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxAcMaxStat", 1),
                    MaxMacChance = reader.ReadByte("Item" + i.ToString(), "MaxMacChance", 0),
                    MaxMacStatChance = reader.ReadByte("Item" + i.ToString(), "MaxMacStatChance", 1),
                    MaxMacMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxMACMaxStat", 1),
                    MaxDcChance = reader.ReadByte("Item" + i.ToString(), "MaxDcChance", 0),
                    MaxDcStatChance = reader.ReadByte("Item" + i.ToString(), "MaxDcStatChance", 1),
                    MaxDcMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxDcMaxStat", 1),
                    MaxMcChance = reader.ReadByte("Item" + i.ToString(), "MaxMcChance", 0),
                    MaxMcStatChance = reader.ReadByte("Item" + i.ToString(), "MaxMcStatChance", 1),
                    MaxMcMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxMcMaxStat", 1),
                    MaxScChance = reader.ReadByte("Item" + i.ToString(), "MaxScChance", 0),
                    MaxScStatChance = reader.ReadByte("Item" + i.ToString(), "MaxScStatChance", 1),
                    MaxScMaxStat = reader.ReadByte("Item" + i.ToString(), "MaxScMaxStat", 1),
                    AccuracyChance = reader.ReadByte("Item" + i.ToString(), "AccuracyChance", 0),
                    AccuracyStatChance = reader.ReadByte("Item" + i.ToString(), "AccuracyStatChance", 1),
                    AccuracyMaxStat = reader.ReadByte("Item" + i.ToString(), "AccuracyMaxStat", 1),
                    AgilityChance = reader.ReadByte("Item" + i.ToString(), "AgilityChance", 0),
                    AgilityStatChance = reader.ReadByte("Item" + i.ToString(), "AgilityStatChance", 1),
                    AgilityMaxStat = reader.ReadByte("Item" + i.ToString(), "AgilityMaxStat", 1),
                    HpChance = reader.ReadByte("Item" + i.ToString(), "HpChance", 0),
                    HpStatChance = reader.ReadByte("Item" + i.ToString(), "HpStatChance", 1),
                    HpMaxStat = reader.ReadByte("Item" + i.ToString(), "HpMaxStat", 1),
                    MpChance = reader.ReadByte("Item" + i.ToString(), "MpChance", 0),
                    MpStatChance = reader.ReadByte("Item" + i.ToString(), "MpStatChance", 1),
                    MpMaxStat = reader.ReadByte("Item" + i.ToString(), "MpMaxStat", 1),
                    StrongChance = reader.ReadByte("Item" + i.ToString(), "StrongChance", 0),
                    StrongStatChance = reader.ReadByte("Item" + i.ToString(), "StrongStatChance", 1),
                    StrongMaxStat = reader.ReadByte("Item" + i.ToString(), "StrongMaxStat", 1),
                    MagicResistChance = reader.ReadByte("Item" + i.ToString(), "MagicResistChance", 0),
                    MagicResistStatChance = reader.ReadByte("Item" + i.ToString(), "MagicResistStatChance", 1),
                    MagicResistMaxStat = reader.ReadByte("Item" + i.ToString(), "MagicResistMaxStat", 1),
                    PoisonResistChance = reader.ReadByte("Item" + i.ToString(), "PoisonResistChance", 0),
                    PoisonResistStatChance = reader.ReadByte("Item" + i.ToString(), "PoisonResistStatChance", 1),
                    PoisonResistMaxStat = reader.ReadByte("Item" + i.ToString(), "PoisonResistMaxStat", 1),
                    HpRecovChance = reader.ReadByte("Item" + i.ToString(), "HpRecovChance", 0),
                    HpRecovStatChance = reader.ReadByte("Item" + i.ToString(), "HpRecovStatChance", 1),
                    HpRecovMaxStat = reader.ReadByte("Item" + i.ToString(), "HpRecovMaxStat", 1),
                    MpRecovChance = reader.ReadByte("Item" + i.ToString(), "MpRecovChance", 0),
                    MpRecovStatChance = reader.ReadByte("Item" + i.ToString(), "MpRecovStatChance", 1),
                    MpRecovMaxStat = reader.ReadByte("Item" + i.ToString(), "MpRecovMaxStat", 1),
                    PoisonRecovChance = reader.ReadByte("Item" + i.ToString(), "PoisonRecovChance", 0),
                    PoisonRecovStatChance = reader.ReadByte("Item" + i.ToString(), "PoisonRecovStatChance", 1),
                    PoisonRecovMaxStat = reader.ReadByte("Item" + i.ToString(), "PoisonRecovMaxStat", 1),
                    CriticalRateChance = reader.ReadByte("Item" + i.ToString(), "CriticalRateChance", 0),
                    CriticalRateStatChance = reader.ReadByte("Item" + i.ToString(), "CriticalRateStatChance", 1),
                    CriticalRateMaxStat = reader.ReadByte("Item" + i.ToString(), "CriticalRateMaxStat", 1),
                    CriticalDamageChance = reader.ReadByte("Item" + i.ToString(), "CriticalDamageChance", 0),
                    CriticalDamageStatChance = reader.ReadByte("Item" + i.ToString(), "CriticalDamageStatChance", 1),
                    CriticalDamageMaxStat = reader.ReadByte("Item" + i.ToString(), "CriticalDamageMaxStat", 1),
                    FreezeChance = reader.ReadByte("Item" + i.ToString(), "FreezeChance", 0),
                    FreezeStatChance = reader.ReadByte("Item" + i.ToString(), "FreezeStatChance", 1),
                    FreezeMaxStat = reader.ReadByte("Item" + i.ToString(), "FreezeMaxStat", 1),
                    PoisonAttackChance = reader.ReadByte("Item" + i.ToString(), "PoisonAttackChance", 0),
                    PoisonAttackStatChance = reader.ReadByte("Item" + i.ToString(), "PoisonAttackStatChance", 1),
                    PoisonAttackMaxStat = reader.ReadByte("Item" + i.ToString(), "PoisonAttackMaxStat", 1),
                    AttackSpeedChance = reader.ReadByte("Item" + i.ToString(), "AttackSpeedChance", 0),
                    AttackSpeedStatChance = reader.ReadByte("Item" + i.ToString(), "AttackSpeedStatChance", 1),
                    AttackSpeedMaxStat = reader.ReadByte("Item" + i.ToString(), "AttackSpeedMaxStat", 1),
                    LuckChance = reader.ReadByte("Item" + i.ToString(), "LuckChance", 0),
                    LuckStatChance = reader.ReadByte("Item" + i.ToString(), "LuckStatChance", 1),
                    LuckMaxStat = reader.ReadByte("Item" + i.ToString(), "LuckMaxStat", 1),
                    CurseChance = reader.ReadByte("Item" + i.ToString(), "CurseChance", 0)
                };
                RandomItemStatsList.Add(stat);
                i++;
            }
        }
        public static void SaveRandomItemStats()
        {
            File.Delete(ConfigPath + @".\RandomItemStats.ini");
            InIReader reader = new InIReader(ConfigPath + @".\RandomItemStats.ini");
            RandomItemStat stat;
            for (int i = 0; i < RandomItemStatsList.Count; i++)
            {
                stat = RandomItemStatsList[i];
                reader.Write("Item" + i.ToString(), "MaxDuraChance", stat.MaxDuraChance);
                reader.Write("Item" + i.ToString(), "MaxDuraStatChance", stat.MaxDuraStatChance);
                reader.Write("Item" + i.ToString(), "MaxDuraMaxStat", stat.MaxDuraMaxStat);
                reader.Write("Item" + i.ToString(), "MaxAcChance", stat.MaxAcChance);
                reader.Write("Item" + i.ToString(), "MaxAcStatChance", stat.MaxAcStatChance);
                reader.Write("Item" + i.ToString(), "MaxAcMaxStat", stat.MaxAcMaxStat);
                reader.Write("Item" + i.ToString(), "MaxMacChance", stat.MaxMacChance);
                reader.Write("Item" + i.ToString(), "MaxMacStatChance", stat.MaxMacStatChance);
                reader.Write("Item" + i.ToString(), "MaxMACMaxStat", stat.MaxMacMaxStat);
                reader.Write("Item" + i.ToString(), "MaxDcChance", stat.MaxDcChance);
                reader.Write("Item" + i.ToString(), "MaxDcStatChance", stat.MaxDcStatChance);
                reader.Write("Item" + i.ToString(), "MaxDcMaxStat", stat.MaxDcMaxStat);
                reader.Write("Item" + i.ToString(), "MaxMcChance", stat.MaxMcChance);
                reader.Write("Item" + i.ToString(), "MaxMcStatChance",  stat.MaxMcStatChance);
                reader.Write("Item" + i.ToString(), "MaxMcMaxStat", stat.MaxMcMaxStat);
                reader.Write("Item" + i.ToString(), "MaxScChance", stat.MaxScChance);
                reader.Write("Item" + i.ToString(), "MaxScStatChance", stat.MaxScStatChance);
                reader.Write("Item" + i.ToString(), "MaxScMaxStat", stat.MaxScMaxStat);
                reader.Write("Item" + i.ToString(), "AccuracyChance", stat.AccuracyChance);
                reader.Write("Item" + i.ToString(), "AccuracyStatChance", stat.AccuracyStatChance);
                reader.Write("Item" + i.ToString(), "AccuracyMaxStat", stat.AccuracyMaxStat);
                reader.Write("Item" + i.ToString(), "AgilityChance", stat.AgilityChance);
                reader.Write("Item" + i.ToString(), "AgilityStatChance", stat.AgilityStatChance);
                reader.Write("Item" + i.ToString(), "AgilityMaxStat", stat.AgilityMaxStat);
                reader.Write("Item" + i.ToString(), "HpChance", stat.HpChance);
                reader.Write("Item" + i.ToString(), "HpStatChance", stat.HpStatChance);
                reader.Write("Item" + i.ToString(), "HpMaxStat", stat.HpMaxStat);
                reader.Write("Item" + i.ToString(), "MpChance", stat.MpChance);
                reader.Write("Item" + i.ToString(), "MpStatChance", stat.MpStatChance);
                reader.Write("Item" + i.ToString(), "MpMaxStat", stat.MpMaxStat);
                reader.Write("Item" + i.ToString(), "StrongChance", stat.StrongChance);
                reader.Write("Item" + i.ToString(), "StrongStatChance", stat.StrongStatChance);
                reader.Write("Item" + i.ToString(), "StrongMaxStat", stat.StrongMaxStat);
                reader.Write("Item" + i.ToString(), "MagicResistChance", stat.MagicResistChance);
                reader.Write("Item" + i.ToString(), "MagicResistStatChance", stat.MagicResistStatChance);
                reader.Write("Item" + i.ToString(), "MagicResistMaxStat", stat.MagicResistMaxStat);
                reader.Write("Item" + i.ToString(), "PoisonResistChance", stat.PoisonResistChance);
                reader.Write("Item" + i.ToString(), "PoisonResistStatChance", stat.PoisonResistStatChance);
                reader.Write("Item" + i.ToString(), "PoisonResistMaxStat", stat.PoisonResistMaxStat);
                reader.Write("Item" + i.ToString(), "HpRecovChance", stat.HpRecovChance);
                reader.Write("Item" + i.ToString(), "HpRecovStatChance", stat.HpRecovStatChance);
                reader.Write("Item" + i.ToString(), "HpRecovMaxStat", stat.HpRecovMaxStat);
                reader.Write("Item" + i.ToString(), "MpRecovChance", stat.MpRecovChance);
                reader.Write("Item" + i.ToString(), "MpRecovStatChance", stat.MpRecovStatChance);
                reader.Write("Item" + i.ToString(), "MpRecovMaxStat", stat.MpRecovMaxStat);
                reader.Write("Item" + i.ToString(), "PoisonRecovChance", stat.PoisonRecovChance);
                reader.Write("Item" + i.ToString(), "PoisonRecovStatChance", stat.PoisonRecovStatChance);
                reader.Write("Item" + i.ToString(), "PoisonRecovMaxStat", stat.PoisonRecovMaxStat);
                reader.Write("Item" + i.ToString(), "CriticalRateChance", stat.CriticalRateChance);
                reader.Write("Item" + i.ToString(), "CriticalRateStatChance", stat.CriticalRateStatChance);
                reader.Write("Item" + i.ToString(), "CriticalRateMaxStat", stat.CriticalRateMaxStat);
                reader.Write("Item" + i.ToString(), "CriticalDamageChance", stat.CriticalDamageChance);
                reader.Write("Item" + i.ToString(), "CriticalDamageStatChance", stat.CriticalDamageStatChance);
                reader.Write("Item" + i.ToString(), "CriticalDamageMaxStat", stat.CriticalDamageMaxStat);
                reader.Write("Item" + i.ToString(), "FreezeChance", stat.FreezeChance);
                reader.Write("Item" + i.ToString(), "FreezeStatChance", stat.FreezeStatChance);
                reader.Write("Item" + i.ToString(), "FreezeMaxStat", stat.FreezeMaxStat);
                reader.Write("Item" + i.ToString(), "PoisonAttackChance", stat.PoisonAttackChance);
                reader.Write("Item" + i.ToString(), "PoisonAttackStatChance", stat.PoisonAttackStatChance);
                reader.Write("Item" + i.ToString(), "PoisonAttackMaxStat", stat.PoisonAttackMaxStat);
                reader.Write("Item" + i.ToString(), "AttackSpeedChance", stat.AttackSpeedChance);
                reader.Write("Item" + i.ToString(), "AttackSpeedStatChance", stat.AttackSpeedStatChance);
                reader.Write("Item" + i.ToString(), "AttackSpeedMaxStat", stat.AttackSpeedMaxStat);
                reader.Write("Item" + i.ToString(), "LuckChance", stat.LuckChance);
                reader.Write("Item" + i.ToString(), "LuckStatChance", stat.LuckStatChance);
                reader.Write("Item" + i.ToString(), "LuckMaxStat", stat.LuckMaxStat);
                reader.Write("Item" + i.ToString(), "CurseChance", stat.CurseChance);
            }
        }

        public static void LoadMines()
        {
            if (!File.Exists(ConfigPath + @".\Mines.ini"))
            {
                MineSetList.Add(new MineSet(1));
                MineSetList.Add(new MineSet(2));
                SaveMines();
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\Mines.ini");
            int i = 0;
            MineSet Mine;
            while (reader.ReadByte("Mine" + i.ToString(), "SpotRegenRate", 255) != 255)
            {
                Mine = new MineSet();
                Mine.Name = reader.ReadString("Mine" + i.ToString(), "Name", Mine.Name);
                Mine.SpotRegenRate = reader.ReadByte("Mine" + i.ToString(), "SpotRegenRate", Mine.SpotRegenRate);
                Mine.MaxStones = reader.ReadByte("Mine" + i.ToString(), "MaxStones", Mine.MaxStones);
                Mine.HitRate = reader.ReadByte("Mine" + i.ToString(), "HitRate", Mine.HitRate);
                Mine.DropRate = reader.ReadByte("Mine" + i.ToString(), "DropRate", Mine.DropRate);
                Mine.TotalSlots = reader.ReadByte("Mine" + i.ToString(), "TotalSlots", Mine.TotalSlots);
                int j = 0;
                while (reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MinSlot", 255) != 255)
                {
                    Mine.Drops.Add(new MineDrop()
                        {
                            ItemName = reader.ReadString("Mine" + i.ToString(), "D" + j.ToString() + "-ItemName", ""),
                            MinSlot = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MinSlot", 255),
                            MaxSlot = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MaxSlot", 255),
                            MinDura = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MinDura", 255),
                            MaxDura = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MaxDura", 255),
                            BonusChance = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-BonusChance", 255),
                            MaxBonusDura = reader.ReadByte("Mine" + i.ToString(), "D" + j.ToString() + "-MaxBonusDura", 255)
                        });
                    j++;
                }
                MineSetList.Add(Mine);
                i++;
            }

        }
        public static void SaveMines()
        {
            File.Delete(ConfigPath + @".\Mines.ini");
            InIReader reader = new InIReader(ConfigPath + @".\Mines.ini");
            MineSet Mine;
            for (int i = 0; i < MineSetList.Count; i++)
            {
                Mine = MineSetList[i];
                reader.Write("Mine" + i.ToString(), "Name", Mine.Name);
                reader.Write("Mine" + i.ToString(), "SpotRegenRate", Mine.SpotRegenRate);
                reader.Write("Mine" + i.ToString(), "MaxStones", Mine.MaxStones);
                reader.Write("Mine" + i.ToString(), "HitRate", Mine.HitRate);
                reader.Write("Mine" + i.ToString(), "DropRate", Mine.DropRate);
                reader.Write("Mine" + i.ToString(), "TotalSlots", Mine.TotalSlots);
                
                for (int j = 0; j < Mine.Drops.Count; j++)
                {
                    MineDrop Drop = Mine.Drops[j];
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-ItemName", Drop.ItemName);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-MinSlot", Drop.MinSlot);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-MaxSlot", Drop.MaxSlot);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-MinDura", Drop.MinDura);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-MaxDura", Drop.MaxDura);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-BonusChance", Drop.BonusChance);
                    reader.Write("Mine" + i.ToString(), "D" + j.ToString() + "-MaxBonusDura", Drop.MaxBonusDura);
                }
            }
        }

        public static void LoadGuildSettings()
        {
            if (!File.Exists(ConfigPath + @".\GuildSettings.ini"))
            {
                Guild_CreationCostList.Add(new ItemVolume(){Amount = 1000000});
                Guild_CreationCostList.Add(new ItemVolume(){ItemName = "WoomaHorn",Amount = 1});
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\GuildSettings.ini");
            Guild_RequiredLevel = reader.ReadByte("Guilds", "MinimumLevel", Guild_RequiredLevel);
            Guild_ExpRate = reader.ReadFloat("Guilds", "ExpRate", Guild_ExpRate);
            Guild_PointPerLevel = reader.ReadByte("Guilds", "PointPerLevel", Guild_PointPerLevel);
            Guild_WarTime = reader.ReadInt64("Guilds", "WarTime", Guild_WarTime);
            Guild_WarCost = reader.ReadUInt32("Guilds", "WarCost", Guild_WarCost);

            int i = 0;
            while (reader.ReadUInt32("Required-" + i.ToString(),"Amount",0) != 0)
            {
                Guild_CreationCostList.Add(new ItemVolume()
                {
                    ItemName = reader.ReadString("Required-" + i.ToString(), "ItemName", ""),
                    Amount = reader.ReadUInt32("Required-" + i.ToString(), "Amount", 0)
                }
                );
                i++;
            }
            i = 0;
            while (reader.ReadInt64("Exp", "Level-" + i.ToString(), -1) != -1)
            {
                Guild_ExperienceList.Add(reader.ReadInt64("Exp", "Level-" + i.ToString(), 0));
                i++;
            }
            i = 0;
            while (reader.ReadInt32("Cap", "Level-" + i.ToString(), -1) != -1)
            {
                Guild_MembercapList.Add(reader.ReadInt32("Cap", "Level-" + i.ToString(), 0));
                i++;
            }
            byte TotalBuffs = reader.ReadByte("Guilds", "TotalBuffs", 0);
            for (i = 0; i < TotalBuffs; i++)
            {
                Guild_BuffList.Add(new GuildBuffInfo(reader, i));
            }



        }
        public static void SaveGuildSettings()
        {
            File.Delete(ConfigPath + @".\GuildSettings.ini");
            InIReader reader = new InIReader(ConfigPath + @".\GuildSettings.ini");
            reader.Write("Guilds", "MinimumLevel", Guild_RequiredLevel);
            reader.Write("Guilds", "ExpRate", Guild_ExpRate);
            reader.Write("Guilds", "PointPerLevel", Guild_PointPerLevel);
            reader.Write("Guilds", "TotalBuffs", Guild_BuffList.Count);
            reader.Write("Guilds", "WarTime", Guild_WarTime);
            reader.Write("Guilds", "WarCost", Guild_WarCost);

            int i = 0;
            for (i = 0; i < Guild_ExperienceList.Count; i++)
            {
                reader.Write("Exp", "Level-" + i.ToString(), Guild_ExperienceList[i]);
            }
            for (i = 0; i < Guild_MembercapList.Count; i++)
            {
                reader.Write("Cap", "Level-" + i.ToString(), Guild_MembercapList[i]);
            }
            for (i = 0; i < Guild_CreationCostList.Count; i++)
            {
                reader.Write("Required-" + i.ToString(), "ItemName", Guild_CreationCostList[i].ItemName);
                reader.Write("Required-" + i.ToString(), "Amount", Guild_CreationCostList[i].Amount);
            }
            for (i = 0; i < Guild_BuffList.Count; i++)
            {
                Guild_BuffList[i].Save(reader, i);
            }
        }
        public static void LinkGuildCreationItems(List<ItemInfo> ItemList)
        {
            for (int i = 0; i < Guild_CreationCostList.Count; i++)
            {
                if (Guild_CreationCostList[i].ItemName != "")
                    for (int j = 0; j < ItemList.Count; j++)
                    {
                        if (String.Compare(ItemList[j].Name.Replace(" ", ""), Guild_CreationCostList[i].ItemName, StringComparison.OrdinalIgnoreCase) != 0) continue;
                        Guild_CreationCostList[i].Item = ItemList[j];
                        break;
                    }
                  
            }
        }

		public static void LoadAwakeAttribute()
        {
            if (!File.Exists(ConfigPath + @".\AwakeningSystem.ini"))
            {
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\AwakeningSystem.ini");
            Awake.AwakeSuccessRate = reader.ReadByte("Attribute", "SuccessRate", Awake.AwakeSuccessRate);
            Awake.AwakeHitRate = reader.ReadByte("Attribute", "HitRate", Awake.AwakeHitRate);
            Awake.MaxAwakeLevel = reader.ReadInt32("Attribute", "MaxUpgradeLevel", Awake.MaxAwakeLevel);
            Awake.Awake_WeaponRate = reader.ReadByte("IncreaseValue", "WeaponValue", Awake.Awake_WeaponRate);
            Awake.Awake_HelmetRate = reader.ReadByte("IncreaseValue", "HelmetValue", Awake.Awake_HelmetRate);
            Awake.Awake_ArmorRate = reader.ReadByte("IncreaseValue", "ArmorValue", Awake.Awake_ArmorRate);

            for (int i = 0; i < 4; i++)
            {
                Awake.AwakeChanceMax[i] = reader.ReadByte("Value", "ChanceMax_" + ((ItemGrade)(i + 1)).ToString(), Awake.AwakeChanceMax[i]);
            }

            for (int i = 0; i < (int)AwakeType.HPMP; i++)
            {
                List<byte>[] value = new List<byte>[2];

                for (int k = 0; k < 2; k++)
                {
                    value[k] = new List<byte>();
                }

                for (int j = 0; j < 4; j++)
                {
                    byte material1 = 1;
                    material1 = reader.ReadByte("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material1", material1);
                    byte material2 = 1;
                    material2 = reader.ReadByte("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material2", material2);
                    value[0].Add(material1);
                    value[1].Add(material2);
                }

                Awake.AwakeMaterials.Add(value);
            }

            for (int c = 0; c < 4; c++)
            {
                Awake.AwakeMaterialRate[c] = reader.ReadFloat("Materials_IncreaseValue", "Materials_" + ((ItemGrade)(c + 1)).ToString(), Awake.AwakeMaterialRate[c]);
            }

        }
        public static void SaveAwakeAttribute()
        {
            File.Delete(ConfigPath + @".\AwakeningSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\AwakeningSystem.ini");
            reader.Write("Attribute", "SuccessRate", Awake.AwakeSuccessRate);
            reader.Write("Attribute", "HitRate", Awake.AwakeHitRate);
            reader.Write("Attribute", "MaxUpgradeLevel", Awake.MaxAwakeLevel);

            reader.Write("IncreaseValue", "WeaponValue", Awake.Awake_WeaponRate);
            reader.Write("IncreaseValue", "HelmetValue", Awake.Awake_HelmetRate);
            reader.Write("IncreaseValue", "ArmorValue", Awake.Awake_ArmorRate);

            for (int i = 0; i < 4; i++)
            {
                reader.Write("Value", "ChanceMax_" + ((ItemGrade)(i + 1)).ToString(), Awake.AwakeChanceMax[i]);
            }

            if (Awake.AwakeMaterials.Count == 0)
            {
                for (int i = 0; i < (int)AwakeType.HPMP; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        reader.Write("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material1", 1);
                        reader.Write("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material2", 1);
                    }
                }
            }
            else
            {
                for (int i = 0; i < (int)AwakeType.HPMP; i++)
                {
                    List<byte>[] value = Awake.AwakeMaterials[i];

                    for (int j = 0; j < value[0].Count; j++)
                    {
                        reader.Write("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material1", value[0][j]);
                        reader.Write("Materials_BaseValue", ((AwakeType)(i + 1)).ToString() + "_" + ((ItemGrade)(j + 1)).ToString() + "_Material2", value[1][j]);
                    }

                    Awake.AwakeMaterials.Add(value);
                }
            }

            for (int c = 0; c < 4; c++)
            {
                reader.Write("Materials_IncreaseValue", "Materials_" + ((ItemGrade)(c + 1)).ToString(), Awake.AwakeMaterialRate[c]);
            }
        }

        public static void LoadFishing()
        {
            if (!File.Exists(ConfigPath + @".\FishingSystem.ini"))
            {
                SaveFishing();
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\FishingSystem.ini");
            FishingAttempts = reader.ReadInt32("Rates", "Attempts", FishingAttempts);
            FishingSuccessStart = reader.ReadInt32("Rates", "SuccessStart", FishingSuccessStart);
            FishingSuccessMultiplier = reader.ReadInt32("Rates", "SuccessMultiplier", FishingSuccessMultiplier);
            FishingDelay = reader.ReadInt64("Rates", "Delay", FishingDelay);
            FishingMobSpawnChance = reader.ReadInt32("Rates", "MonsterSpawnChance", FishingMobSpawnChance);
            FishingMonster = reader.ReadString("Game", "Monster", FishingMonster);
        }
        public static void SaveFishing()
        {
            File.Delete(ConfigPath + @".\FishingSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\FishingSystem.ini");
            reader.Write("Rates", "Attempts", FishingAttempts);
            reader.Write("Rates", "SuccessStart", FishingSuccessStart);
            reader.Write("Rates", "SuccessMultiplier", FishingSuccessMultiplier);
            reader.Write("Rates", "Delay", FishingDelay);
            reader.Write("Rates", "MonsterSpawnChance", FishingMobSpawnChance);
            reader.Write("Game", "Monster", FishingMonster);
        }

        public static void LoadMail()
        {
            if (!File.Exists(ConfigPath + @".\MailSystem.ini"))
            {
                SaveMail();
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\MailSystem.ini");
            MailAutoSendGold = reader.ReadBoolean("AutoSend", "Gold", MailAutoSendGold);
            MailAutoSendItems = reader.ReadBoolean("AutoSend", "Items", MailAutoSendItems);
            MailFreeWithStamp = reader.ReadBoolean("Rates", "FreeWithStamp", MailFreeWithStamp);
            MailCostPer1KGold = reader.ReadUInt32("Rates", "CostPer1k", MailCostPer1KGold);
            MailItemInsurancePercentage = reader.ReadUInt32("Rates", "InsurancePerItem", MailItemInsurancePercentage);
            MailCapacity = reader.ReadUInt32("General", "MailCapacity", MailCapacity);
        }
        public static void SaveMail()
        {
            File.Delete(ConfigPath + @".\MailSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\MailSystem.ini");
            reader.Write("AutoSend", "Gold", MailAutoSendGold);
            reader.Write("AutoSend", "Items", MailAutoSendItems);
            reader.Write("Rates", "FreeWithStamp", MailFreeWithStamp);
            reader.Write("Rates", "CostPer1k", MailCostPer1KGold);
            reader.Write("Rates", "InsurancePerItem", MailItemInsurancePercentage);
            reader.Write("General", "MailCapacity", MailCapacity);
        }

        public static void LoadRefine()
        {
            if (!File.Exists(ConfigPath + @".\RefineSystem.ini"))
            {
                SaveRefine();
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\RefineSystem.ini");
            OnlyRefineWeapon = reader.ReadBoolean("Config", "OnlyRefineWeapon", OnlyRefineWeapon);
            RefineBaseChance = reader.ReadByte("Config", "BaseChance", RefineBaseChance);
            RefineTime = reader.ReadInt32("Config", "Time", RefineTime);
            RefineIncrease = reader.ReadByte("Config", "StatIncrease", RefineIncrease);
            RefineCritChance = reader.ReadByte("Config", "CritChance", RefineCritChance);
            RefineCritIncrease = reader.ReadByte("Config", "CritIncrease", RefineCritIncrease);
            RefineWepStatReduce = reader.ReadByte("Config", "WepStatReducedChance", RefineWepStatReduce);
            RefineItemStatReduce = reader.ReadByte("Config", "ItemStatReducedChance", RefineItemStatReduce);
            RefineCost = reader.ReadInt32("Config", "RefineCost", RefineCost);

            RefineOreName = reader.ReadString("Ore", "OreName", RefineOreName);
        }
        public static void SaveRefine()
        {
            File.Delete(ConfigPath + @".\RefineSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\RefineSystem.ini");
            reader.Write("Config", "OnlyRefineWeapon", OnlyRefineWeapon);
            reader.Write("Config", "BaseChance", RefineBaseChance);
            reader.Write("Config", "Time", RefineTime);
            reader.Write("Config", "StatIncrease", RefineIncrease);
            reader.Write("Config", "CritChance", RefineCritChance);
            reader.Write("Config", "CritIncrease", RefineCritIncrease);
            reader.Write("Config", "WepStatReducedChance", RefineWepStatReduce);
            reader.Write("Config", "ItemStatReducedChance", RefineItemStatReduce);
            reader.Write("Config", "RefineCost", RefineCost);

            reader.Write("Ore", "OreName", RefineOreName);

        }

        public static void LoadMarriage()
        {
            if (!File.Exists(ConfigPath + @".\MarriageSystem.ini"))
            {
                SaveMarriage();
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\MarriageSystem.ini");
            LoverEXPBonus = reader.ReadInt32("Config", "EXPBonus", LoverEXPBonus);
            MarriageCooldown = reader.ReadInt32("Config", "MarriageCooldown", MarriageCooldown);
            WeddingRingRecall = reader.ReadBoolean("Config", "AllowLoverRecall", WeddingRingRecall);
            MarriageLevelRequired = reader.ReadInt32("Config", "MinimumLevel", MarriageLevelRequired);
            ReplaceWedRingCost = reader.ReadInt32("Config", "ReplaceRingCost", ReplaceWedRingCost);
        }
        public static void SaveMarriage()
        {
            File.Delete(ConfigPath + @".\MarriageSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\MarriageSystem.ini");
            reader.Write("Config", "EXPBonus", LoverEXPBonus);
            reader.Write("Config", "MarriageCooldown", MarriageCooldown);
            reader.Write("Config", "AllowLoverRecall", WeddingRingRecall);
            reader.Write("Config", "MinimumLevel", MarriageLevelRequired);
            reader.Write("Config", "ReplaceRingCost", ReplaceWedRingCost); 
        }

        public static void LoadMentor()
        {
            if (!File.Exists(ConfigPath + @".\MentorSystem.ini"))
            {
                SaveMentor();
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\MentorSystem.ini");
            MentorLevelGap = reader.ReadByte("Config", "LevelGap", MentorLevelGap);
            MentorSkillBoost = reader.ReadBoolean("Config", "MenteeSkillBoost", MentorSkillBoost);
            MentorLength = reader.ReadByte("Config", "MentorshipLength", MentorLength);
            MentorDamageBoost = reader.ReadByte("Config", "MentorDamageBoost", MentorDamageBoost);
            MentorExpBoost = reader.ReadByte("Config", "MenteeExpBoost", MentorExpBoost);
            MenteeExpBank = reader.ReadByte("Config", "PercentXPtoMentor", MenteeExpBank);
        }
        public static void SaveMentor()
        {
            File.Delete(ConfigPath + @".\MentorSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\MentorSystem.ini");
            reader.Write("Config", "LevelGap", MentorLevelGap);
            reader.Write("Config", "MenteeSkillBoost", MentorSkillBoost);
            reader.Write("Config", "MentorshipLength", MentorLength);
            reader.Write("Config", "MentorDamageBoost", MentorDamageBoost);
            reader.Write("Config", "MenteeExpBoost", MentorExpBoost);
            reader.Write("Config", "PercentXPtoMentor", MenteeExpBank);
        }
        public static void LoadGem()
        {
            if (!File.Exists(ConfigPath + @".\GemSystem.ini"))
            {
                SaveGem();
                return;
            }
            InIReader reader = new InIReader(ConfigPath + @".\GemSystem.ini");
            GemStatIndependent = reader.ReadBoolean("Config", "GemStatIndependent", GemStatIndependent);


        }
        public static void SaveGem()
        {
            File.Delete(ConfigPath + @".\GemSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\GemSystem.ini");
            reader.Write("Config", "GemStatIndependent", GemStatIndependent);
        }

        public static void LoadGoods()
        {
            if (!File.Exists(ConfigPath + @".\GoodsSystem.ini"))
            {
                SaveGoods();
                return;
            }

            InIReader reader = new InIReader(ConfigPath + @".\GoodsSystem.ini");
            GoodsOn = reader.ReadBoolean("Goods", "On", GoodsOn);
            GoodsMaxStored = reader.ReadUInt32("Goods", "MaxStored", GoodsMaxStored);
            GoodsBuyBackTime = reader.ReadUInt32("Goods", "BuyBackTime", GoodsBuyBackTime);
            GoodsBuyBackMaxStored = reader.ReadUInt32("Goods", "BuyBackMaxStored", GoodsBuyBackMaxStored);
        }
        public static void SaveGoods()
        {
            File.Delete(ConfigPath + @".\GoodsSystem.ini");
            InIReader reader = new InIReader(ConfigPath + @".\GoodsSystem.ini");
            reader.Write("Goods", "On", GoodsOn);
            reader.Write("Goods", "MaxStored", GoodsMaxStored);
            reader.Write("Goods", "BuyBackTime", GoodsBuyBackTime);
            reader.Write("Goods", "BuyBackMaxStored", GoodsBuyBackMaxStored);
        }

    }
}
