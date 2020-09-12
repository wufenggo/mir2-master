using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Server.MirEnvir;
using System.Drawing;

namespace Server.MirDatabase
{
    public class MonsterInfo
    {
        public int Index;
        public string Name = string.Empty;

        public Monster Image;
        public byte AI, Effect, ViewRange = 7, CoolEye;
        public ushort Level;

        public uint HP;
        public byte Accuracy, Agility, Light;
        public string LightColar = Color.White.ToString();
        public ushort MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC;
        public ushort RandomQuestChance;
        public byte LightEffect = 0;

        public ushort AttackSpeed = 2500, MoveSpeed = 1800;
        public uint Experience;
        
        public List<DropInfo> Drops = new List<DropInfo>();
        public List<EliteDropInfo> EliteDrops = new List<EliteDropInfo>();
        public bool CanTame = true, CanPush = true, AutoRev = true, Undead = false , Ignore = false , TeleportBack = false;

        public List<int> RandomQuest = new List<int>();

        public bool HasSpawnScript;
        public bool HasDieScript;

        public bool IsBoss;
        public bool CanBeElite;
        public bool IsSub;
        public bool IsRetreatMob = false;
        public bool 
            AllowFreeze = true, 
            AllowSlow = true,
            AllowGreen = true,
            AllowRed = true,
            AllowPara = true,
            AllowBurning = true,
            AllowBleeding = true;
        public List<AIAttack> Attacks = new List<AIAttack>();
        public MonsterInfo()
        {
        }
        public MonsterInfo(BinaryReader reader)
        {
            Index = reader.ReadInt32();
            Name = reader.ReadString();

            Image = (Monster) reader.ReadUInt16();
            AI = reader.ReadByte();
            Effect = reader.ReadByte();
            if (Envir.LoadVersion < 62)
            {
                Level = (ushort)reader.ReadByte();
            }
            else
            {
                Level = reader.ReadUInt16();
            }

            ViewRange = reader.ReadByte();
            if (Envir.LoadVersion >= 3) CoolEye = reader.ReadByte();

            HP = reader.ReadUInt32();

            if (Envir.LoadVersion < 62)
            {
                MinAC = (ushort)reader.ReadByte();
                MaxAC = (ushort)reader.ReadByte();
                MinMAC = (ushort)reader.ReadByte();
                MaxMAC = (ushort)reader.ReadByte();
                MinDC = (ushort)reader.ReadByte();
                MaxDC = (ushort)reader.ReadByte();
                MinMC = (ushort)reader.ReadByte();
                MaxMC = (ushort)reader.ReadByte();
                MinSC = (ushort)reader.ReadByte();
                MaxSC = (ushort)reader.ReadByte();
            }
            else
            {
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

            Accuracy = reader.ReadByte();
            Agility = reader.ReadByte();
            Light = reader.ReadByte();

            AttackSpeed = reader.ReadUInt16();
            MoveSpeed = reader.ReadUInt16();
            Experience = reader.ReadUInt32();

            if (Envir.LoadVersion < 6)
            {
                reader.BaseStream.Seek(8, SeekOrigin.Current);

                int count = reader.ReadInt32();
                reader.BaseStream.Seek(count*12, SeekOrigin.Current);
            }

            CanPush = reader.ReadBoolean();
            CanTame = reader.ReadBoolean();

            if (Envir.LoadVersion < 18) return;
            AutoRev = reader.ReadBoolean();
            Undead = reader.ReadBoolean();

            if (Envir.LoadVersion < 74) return;
            Ignore = reader.ReadBoolean();
            TeleportBack = reader.ReadBoolean();
            if (Envir.LoadVersion > 84)
            {
                IsBoss = reader.ReadBoolean();
            }
            if (Envir.LoadVersion > 91)
                CanBeElite = reader.ReadBoolean();

            if (Envir.LoadVersion > 105)
                LightColar = reader.ReadString();


            if (Envir.LoadVersion > 107)
            {
                int count = 0;
                count = reader.ReadInt32();

                for(int i = 0; i < count; i++)
                {
                    RandomQuest.Add(reader.ReadInt32());
                }

            }

            if (Envir.LoadVersion > 108)
                RandomQuestChance = reader.ReadUInt16();

            if (Envir.LoadVersion > 114)
                LightEffect = reader.ReadByte();
            
            if (Envir.LoadVersion > 122)
                IsSub = reader.ReadBoolean();
            /*
            if (Envir.LoadVersion > 128)
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                    Attacks.Add(new AIAttack(reader, Envir.LoadVersion));
                AntiPoison = (AIAntiPoison)reader.ReadUInt32();
            }
            */
            IsRetreatMob = reader.ReadBoolean();
            if (Envir.LoadVersion > 137)
            {
                AllowBleeding = reader.ReadBoolean();
                AllowBurning = reader.ReadBoolean();
                AllowFreeze = reader.ReadBoolean();
                AllowGreen = reader.ReadBoolean();
                AllowPara = reader.ReadBoolean();
                AllowRed = reader.ReadBoolean();
                AllowSlow = reader.ReadBoolean();
            }
            
        }

        public string GameName
        {
            get { return Regex.Replace(Name, @"[\d-]", string.Empty); }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(Name);

            writer.Write((ushort) Image);
            writer.Write(AI);
            writer.Write(Effect);
            writer.Write(Level);
            writer.Write(ViewRange);
            writer.Write(CoolEye);

            writer.Write(HP);

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

            writer.Write(Accuracy);
            writer.Write(Agility);
            writer.Write(Light);

            writer.Write(AttackSpeed);
            writer.Write(MoveSpeed);
            writer.Write(Experience);

            writer.Write(CanPush);
            writer.Write(CanTame);
            writer.Write(AutoRev);
            writer.Write(Undead);

            writer.Write(Ignore);
            writer.Write(TeleportBack);
            writer.Write(IsBoss);
            writer.Write(CanBeElite);
            writer.Write(LightColar);
            writer.Write(RandomQuest.Count);

            for (int i = 0; i < RandomQuest.Count; i++)
            {
                writer.Write(RandomQuest[i]);
            }

            writer.Write(RandomQuestChance);
            writer.Write(LightEffect);
            writer.Write(IsSub);
            /*
            writer.Write(Attacks.Count);
            for (int i = 0; i < Attacks.Count; i++)
                Attacks[i].Save(writer);
            writer.Write((ushort)AntiPoison);
            */
            writer.Write(IsRetreatMob);
            writer.Write(AllowBleeding);
            writer.Write(AllowBurning);
            writer.Write(AllowFreeze);
            writer.Write(AllowGreen);
            writer.Write(AllowPara);
            writer.Write(AllowRed);
            writer.Write(AllowSlow);
        }



        public static MonsterInfo CloneMonster(MonsterInfo m)
        {
            MonsterInfo newmob = new MonsterInfo
            {
                Accuracy = m.Accuracy,
                Agility = m.Agility,
                AI = m.AI,
                AttackSpeed = m.AttackSpeed,
                AutoRev = m.AutoRev,
                CanPush = m.CanPush,
                CanTame = m.CanTame,
                CoolEye = m.CoolEye,
                Drops = m.Drops,
                Effect = m.Effect,
                Experience = m.Experience,
                HP = m.HP,
                Image = m.Image,
                Index = m.Index,
                Level = m.Level,
                Light = m.Light,
                MaxAC = m.MaxAC,
                MaxDC = m.MaxDC,
                MaxMAC = m.MaxMAC,
                MaxMC = m.MaxMC,
                MaxSC = m.MaxSC,
                MinAC = m.MinAC,
                MinDC = m.MinDC,
                MinMAC = m.MinMAC,
                MinMC = m.MinMC,
                MinSC = m.MinSC,
                MoveSpeed = m.MoveSpeed,
                Name = m.Name,
                Undead = m.Undead,
                ViewRange = m.ViewRange,

                Ignore = m.Ignore,
                TeleportBack = m.TeleportBack,
                IsBoss = m.IsBoss,
                LightColar = m.LightColar,

                RandomQuestChance = m.RandomQuestChance,
                LightEffect = m.LightEffect,
                IsSub = m.IsSub
            };
            return newmob;

        }

        public void UpdateMonster(MonsterInfo m)
        {

            Accuracy = m.Accuracy;
            Agility = m.Agility;
            AI = m.AI;
            AttackSpeed = m.AttackSpeed;
            AutoRev = m.AutoRev;
            CanPush = m.CanPush;
            CanTame = m.CanTame;
            CoolEye = m.CoolEye;
            Effect = m.Effect;
            Experience = m.Experience;
            HP = m.HP;
            Image = m.Image;
            Index = m.Index;
            Level = m.Level;
            Light = m.Light;
            MaxAC = m.MaxAC;
            MaxDC = m.MaxDC;
            MaxMAC = m.MaxMAC;
            MaxMC = m.MaxMC;
            MaxSC = m.MaxSC;
            MinAC = m.MinAC;
            MinDC = m.MinDC;
            MinMAC = m.MinMAC;
            MinMC = m.MinMC;
            MinSC = m.MinSC;
            MoveSpeed = m.MoveSpeed;
            Name = m.Name;
            Undead = m.Undead;
            ViewRange = m.ViewRange;

            Ignore = m.Ignore;
            TeleportBack = m.TeleportBack;
            IsBoss = m.IsBoss;
            LightColar = m.LightColar;

            RandomQuestChance = m.RandomQuestChance;
            LightEffect = m.LightEffect;
            IsSub = m.IsSub;
            LoadDrops();
            LoadEliteDrops();
        }

        public void LoadEliteDrops()
        {
            try
            {
                EliteDrops.Clear();
                var pathArray = Directory.GetFiles(Settings.DropPath + @".\Elites\", Name + ".txt", SearchOption.TopDirectoryOnly);

                string path = string.Empty;
                if (pathArray.Length > 0)
                    path = pathArray[0];

                if (!File.Exists(path))
                {
                    //  Create a dummy/empty

                    return;
                }

                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(";") || string.IsNullOrWhiteSpace(lines[i]))
                        continue;

                    EliteDropInfo drop = EliteDropInfo.FromLine(lines[i], Name);
                    if (drop == null)
                    {
                        SMain.Enqueue(string.Format("Could not load Drop: {0}, Line {1}", Name, lines[i]));
                        continue;
                    }

                    EliteDrops.Add(drop);
                }
            }
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
            }
        }


        public void LoadDrops()
        {
            try
            {
                Drops.Clear();
                var pathArray = Directory.GetFiles(Settings.DropPath, Name + ".txt", SearchOption.AllDirectories);

                string path = string.Empty;
                if (pathArray.Length > 0)
                    path = pathArray[0];

                if (!File.Exists(path))
                {
                    return;
                }

                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(";") || string.IsNullOrWhiteSpace(lines[i])) continue;

                    DropInfo drop = DropInfo.FromLine(lines[i], Name);
                    if (drop == null)
                    {
                        SMain.Enqueue(string.Format("Could not load Drop: {0}, Line {1}", Name, lines[i]));
                        continue;
                    }

                    Drops.Add(drop);
                }

                Drops.Sort((drop1, drop2) =>
                {
                    if (drop1.Gold > 0 && drop2.Gold == 0)
                        return 1;
                    if (drop1.Gold == 0 && drop2.Gold > 0)
                        return -1;
                    if (drop1.Gold > 0 && drop2.Gold > 0)
                        return drop1.Gold > drop2.Gold ? 1 : -1;

                    return drop1.Item.Type.CompareTo(drop2.Item.Type);
                });
            }
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
            }
        }

        public static void FromText(string text)
        {
            string[] data = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 25) return; //28

            MonsterInfo info = new MonsterInfo {Name = data[0]};
            if (!ushort.TryParse(data[1], out ushort image)) return;
            info.Image = (Monster) image;

            if (!byte.TryParse(data[2], out info.AI)) return;
            if (!byte.TryParse(data[3], out info.Effect)) return;
            if (!ushort.TryParse(data[4], out info.Level)) return;
            if (!byte.TryParse(data[5], out info.ViewRange)) return;

            if (!uint.TryParse(data[6], out info.HP)) return;

            if (!ushort.TryParse(data[7], out info.MinAC)) return;
            if (!ushort.TryParse(data[8], out info.MaxAC)) return;
            if (!ushort.TryParse(data[9], out info.MinMAC)) return;
            if (!ushort.TryParse(data[10], out info.MaxMAC)) return;
            if (!ushort.TryParse(data[11], out info.MinDC)) return;
            if (!ushort.TryParse(data[12], out info.MaxDC)) return;
            if (!ushort.TryParse(data[13], out info.MinMC)) return;
            if (!ushort.TryParse(data[14], out info.MaxMC)) return;
            if (!ushort.TryParse(data[15], out info.MinSC)) return;
            if (!ushort.TryParse(data[16], out info.MaxSC)) return;
            if (!byte.TryParse(data[17], out info.Accuracy)) return;
            if (!byte.TryParse(data[18], out info.Agility)) return;
            if (!byte.TryParse(data[19], out info.Light)) return;

            if (!ushort.TryParse(data[20], out info.AttackSpeed)) return;
            if (!ushort.TryParse(data[21], out info.MoveSpeed)) return;

            if (!uint.TryParse(data[22], out info.Experience)) return;
            
            if (!bool.TryParse(data[23], out info.CanTame)) return;
            if (!bool.TryParse(data[24], out info.CanPush)) return;
            if (!bool.TryParse(data[25], out info.IsBoss))
                return;
            if (!bool.TryParse(data[26], out info.CanBeElite))
                return;
            if (!bool.TryParse(data[27], out info.Undead)) return;
            if (!bool.TryParse(data[28], out info.Ignore)) return;
            if (!bool.TryParse(data[29], out info.TeleportBack)) return;
            info.LightColar = data[30];
            if (!byte.TryParse(data[31], out info.LightEffect)) return;
            if (!bool.TryParse(data[32], out info.IsSub)) return;

            info.Index = ++SMain.EditEnvir.MonsterIndex;
            SMain.EditEnvir.MonsterInfoList.Add(info);
        }
        public string ToText() // New Ice

        {

            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}",

            Name, (ushort)Image, AI, Effect, Level, ViewRange, HP, MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC, Accuracy, Agility, Light, AttackSpeed, MoveSpeed, Experience, CanTame, CanPush, IsBoss, CanBeElite, Undead, Ignore, TeleportBack,LightColar,LightEffect, IsSub);

        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Index, Name);
            //return string.Format("{0}", Name);
        }

    }

    public class EliteDropInfo
    {
        public int Chance;
        public ItemInfo Item;
        public uint Gold = 0;

        public byte Type;
        public bool QuestRequired;

        public byte EliteLevel;

        public static EliteDropInfo FromLine(string s, string mobsName = "")
        {
            string[] parts = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //for (int i = 0; i < parts.Length; i++)  // Mob Drop Debug Code
            //    SMain.Enqueue(parts[i] + " " + mobsName);
            EliteDropInfo info = new EliteDropInfo();

            if (!int.TryParse(parts[0].Substring(2), out info.Chance))
                return null;
            if (string.Compare(parts[1], "Gold", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (parts.Length < 4)
                    return null;
                if (!uint.TryParse(parts[2], out info.Gold) || info.Gold == 0)
                    return null;
                switch (parts[3])
                {
                    case "LV1":
                        info.EliteLevel = 1;
                        break;
                    case "LV2":
                        info.EliteLevel = 2;
                        break;
                    case "LV3":
                        info.EliteLevel = 3;
                        break;
                    case "LV4":
                        info.EliteLevel = 4;
                        break;
                    case "LV5":
                        info.EliteLevel = 5;
                        break;
                    case "LV6":
                        info.EliteLevel = 6;
                        break;
                    case "LV7":
                        info.EliteLevel = 7;
                        break;
                    case "LV8":
                        info.EliteLevel = 8;
                        break;
                }
            }
            else
            {
                info.Item = SMain.Envir.GetItemInfo(parts[1]);
                if (info.Item == null)
                    return null;

                if (parts.Length > 2)
                {
                    string dropRequirement = parts[2];
                    if (dropRequirement.ToUpper() == "Q")
                        info.QuestRequired = true;
                    else
                    {
                        switch (dropRequirement)
                        {
                            case "LV1":
                                info.EliteLevel = 1;
                                break;
                            case "LV2":
                                info.EliteLevel = 2;
                                break;
                            case "LV3":
                                info.EliteLevel = 3;
                                break;
                            case "LV4":
                                info.EliteLevel = 4;
                                break;
                            case "LV5":
                                info.EliteLevel = 5;
                                break;
                            case "LV6":
                                info.EliteLevel = 6;
                                break;
                            case "LV7":
                                info.EliteLevel = 7;
                                break;
                            case "LV8":
                                info.EliteLevel = 8;
                                break;
                        }
                    }
                        
                }
            }

            return info;
        }
    }

    public class DropInfo
    {
        public int Chance;
        public ItemInfo Item;
        public uint Gold;

        public byte Type;
        public bool QuestRequired;

        public static DropInfo FromLine(string s, string mobsName = "")
        {
            string[] parts = s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            DropInfo info = new DropInfo();
            //for (int i = 0; i < parts.Length; i++)  // Mob Drop Debug Code
            //    SMain.Enqueue(parts[i] + " " + mobsName);
            if (!int.TryParse(parts[0].Substring(2), out info.Chance)) return null;
            if (string.Compare(parts[1], "Gold", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (parts.Length < 3) return null;
                if (!uint.TryParse(parts[2], out info.Gold) || info.Gold == 0) return null;
            }
            else
            {
                info.Item = SMain.Envir.GetItemInfo(parts[1]);
                if (info.Item == null) return null;

                if (parts.Length > 2)
                {
                    string dropRequirement = parts[2];
                    if (dropRequirement.ToUpper() == "Q") info.QuestRequired = true;
                }
            }
            return info;
        }
    }
}
