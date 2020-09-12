using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MirDataTool
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

        //public List<DropInfo> Drops = new List<DropInfo>();
        //public List<EliteDropInfo> EliteDrops = new List<EliteDropInfo>();
        public bool CanTame = true, CanPush = true, AutoRev = true, Undead = false, Ignore = false, TeleportBack = false;

        public List<int> RandomQuest = new List<int>();

        public bool HasSpawnScript;
        public bool HasDieScript;

        public bool IsBoss;
        public bool CanBeElite;
        public bool IsSub;

        public MonsterInfo()
        {
        }
        public MonsterInfo(BinaryReader reader)
        {
            Index = reader.ReadInt32();
            Name = reader.ReadString();

            Image = (Monster)reader.ReadUInt16();
            AI = reader.ReadByte();
            Effect = reader.ReadByte();
            if (Settings.DatabaseVersion < 62)
            {
                Level = reader.ReadByte();
            }
            else
            {
                Level = reader.ReadUInt16();
            }

            ViewRange = reader.ReadByte();
            if (Settings.DatabaseVersion >= 3) CoolEye = reader.ReadByte();

            HP = reader.ReadUInt32();

            if (Settings.DatabaseVersion < 62)
            {
                MinAC = reader.ReadByte();
                MaxAC = reader.ReadByte();
                MinMAC = reader.ReadByte();
                MaxMAC = reader.ReadByte();
                MinDC = reader.ReadByte();
                MaxDC = reader.ReadByte();
                MinMC = reader.ReadByte();
                MaxMC = reader.ReadByte();
                MinSC = reader.ReadByte();
                MaxSC = reader.ReadByte();
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

            if (Settings.DatabaseVersion < 6)
            {
                reader.BaseStream.Seek(8, SeekOrigin.Current);

                int count = reader.ReadInt32();
                reader.BaseStream.Seek(count * 12, SeekOrigin.Current);
            }

            CanPush = reader.ReadBoolean();
            CanTame = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 18) return;
            AutoRev = reader.ReadBoolean();
            Undead = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 74) return;
            Ignore = reader.ReadBoolean();
            TeleportBack = reader.ReadBoolean();
            if (Settings.DatabaseVersion > 84)
            {
                IsBoss = reader.ReadBoolean();
            }
            if (Settings.DatabaseVersion > 91)
                CanBeElite = reader.ReadBoolean();

            if (Settings.DatabaseVersion > 105)
                LightColar = reader.ReadString();


            if (Settings.DatabaseVersion > 107)
            {
                int count = 0;
                count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    RandomQuest.Add(reader.ReadInt32());
                }

            }

            if (Settings.DatabaseVersion > 108)
                RandomQuestChance = reader.ReadUInt16();

            if (Settings.DatabaseVersion > 114)
                LightEffect = reader.ReadByte();

            if (Settings.DatabaseVersion > 122)
                IsSub = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(Name);

            writer.Write((ushort)Image);
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
        }


        public override string ToString()
        {
            return string.Format("{0}: {1}", Index, Name);
            //return string.Format("{0}", Name);
        }
    }
}
