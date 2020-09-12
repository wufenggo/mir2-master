using System.Collections.Generic;
using System.IO;

namespace MirDataTool
{
    internal static class Settings
    {
        public static string EnvirPath = @".\Envir\",
                            MapPath = @".\Maps\",
                            ExportPath = @".\Exports\",
                            NPCPath = @".\Envir\NPCs\",
                            QuestPath = @".\Envir\Quests\",
                            DatabasePath = "",
                            DropPath = @".\Envir\Drops\";
        private static readonly InIReader Reader = new InIReader(@".\Config.ini");

        public static int DatabaseVersion = 136;
        public static int CustomDatabaseVersion = 0;
        public static double ToolVersion = 0.001;

        public static short CredxGold = 0;
        public static List<RandomItemStat> RandomItemStatsList = new List<RandomItemStat>();
        public static List<MineSet> MineSetList = new List<MineSet>();
        public static void Load()
        {
            EnvirPath = Reader.ReadString("Paths", "Envir", EnvirPath);
            MapPath = Reader.ReadString("Paths", "Maps", MapPath);
            ExportPath = Reader.ReadString("Paths", "Export", ExportPath);
            NPCPath = Reader.ReadString("Paths", "NPCs", NPCPath);
            QuestPath = Reader.ReadString("Paths", "Quests", QuestPath);
            DropPath = Reader.ReadString("Paths", "Drops", DropPath);
            DatabaseVersion = Reader.ReadInt32("Database", "Version", DatabaseVersion);
            CustomDatabaseVersion = Reader.ReadInt32("Database", "CustomVersion", CustomDatabaseVersion);
            ToolVersion = Reader.ReadDouble("Tool", "CurrentVersion", ToolVersion);
            DatabasePath = Reader.ReadString("Paths", "Database", DatabasePath);
            CredxGold = Reader.ReadInt16("Config", "CreditXGold", CredxGold);
            LoadRandomItemStats();
            LoadMines();
        }

        public static void Save()
        {
            Reader.Write("Paths", "Envir", EnvirPath);
            Reader.Write("Paths", "Maps", MapPath);
            Reader.Write("Paths", "Export", ExportPath);
            Reader.Write("Paths", "NPCs", NPCPath);
            Reader.Write("Paths", "Quests", QuestPath);
            Reader.Write("Paths", "Drops", DropPath);
            Reader.Write("Database", "Version", DatabaseVersion);
            Reader.Write("Database", "CustomVersion", CustomDatabaseVersion);
            Reader.Write("Tool", "CurrentVersion", ToolVersion);
            Reader.Write("Paths", "Database", DatabasePath);
            Reader.Write("Config", "CreditxGold", CredxGold);
            SaveRandomItemStats();
            SaveMines();
        }

        public static void LoadRandomItemStats()
        {
            if (!File.Exists(@".\RandomItemStats.ini"))
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
            InIReader reader = new InIReader(@".\RandomItemStats.ini");
            int i = 0;
            RandomItemStat stat;
            while (reader.ReadByte("Item" + i.ToString(), "MaxDuraChance", 255) != 255)
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
            File.Delete(@".\RandomItemStats.ini");
            InIReader reader = new InIReader(@".\RandomItemStats.ini");
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
                reader.Write("Item" + i.ToString(), "MaxMcStatChance", stat.MaxMcStatChance);
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
            if (!File.Exists(@".\Mines.ini"))
            {
                MineSetList.Add(new MineSet(1));
                MineSetList.Add(new MineSet(2));
                SaveMines();
                return;
            }
            InIReader reader = new InIReader(@".\Mines.ini");
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
            File.Delete(@".\Mines.ini");
            InIReader reader = new InIReader(@".\Mines.ini");
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
    }
}
