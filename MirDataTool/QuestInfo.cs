using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MirDataTool
{
    public class QuestInfo
    {
        public int Index;

        public uint NpcIndex;
        public NPCInfo NpcInfo;

        public bool HeroQuest = false;

        private uint _finishNpcIndex;

        public uint FinishNpcIndex
        {
            get { return _finishNpcIndex == 0 ? NpcIndex : _finishNpcIndex; }
            set { _finishNpcIndex = value; }
        }

        public string
            Name = string.Empty,
            Group = string.Empty,
            FileName = string.Empty,
            GotoMessage = string.Empty,
            KillMessage = string.Empty,
            ItemMessage = string.Empty,
            FlagMessage = string.Empty;

        public List<string> Description = new List<string>();
        public List<string> TaskDescription = new List<string>();
        public List<string> CompletionDescription = new List<string>();

        public int RequiredMinLevel, RequiredMaxLevel, RequiredQuest;
        public RequiredClass RequiredClass = RequiredClass.None;
        public bool percentageExp;
        public bool autoComplete;

        public int Time;

        public QuestType Type;


        public uint GoldReward;
        public uint ExpReward;
        public uint CreditReward;
        public uint ItemExpReward;
        public List<QuestItemReward> FixedRewards = new List<QuestItemReward>();
        public List<QuestItemReward> SelectRewards = new List<QuestItemReward>();
        public List<BuffReward> BuffRewards = new List<BuffReward>();

        private Regex _regexMessage = new Regex("\"([^\"]*)\"");


        public QuestInfo(bool heroQuest)
        {

            HeroQuest = heroQuest;

        }

        public QuestInfo(BinaryReader reader)
        {
            Index = reader.ReadInt32();
            Name = reader.ReadString();
            Group = reader.ReadString();
            FileName = reader.ReadString();
            RequiredMinLevel = reader.ReadInt32();

            if (Settings.DatabaseVersion >= 38)
            {
                RequiredMaxLevel = reader.ReadInt32();
                if (RequiredMaxLevel == 0) RequiredMaxLevel = ushort.MaxValue;
            }

            RequiredQuest = reader.ReadInt32();
            RequiredClass = (RequiredClass)reader.ReadByte();
            Type = (QuestType)reader.ReadByte();
            GotoMessage = reader.ReadString();
            KillMessage = reader.ReadString();
            ItemMessage = reader.ReadString();
            if (Settings.DatabaseVersion >= 37) FlagMessage = reader.ReadString();

            if (Settings.DatabaseVersion > 72)
                percentageExp = reader.ReadBoolean();

            if (Settings.DatabaseVersion > 77)
                Time = reader.ReadInt32();

            if (Settings.DatabaseVersion > 103)
                HeroQuest = reader.ReadBoolean();

            if (Settings.DatabaseVersion > 111)
                autoComplete = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(Name);
            writer.Write(Group);
            writer.Write(FileName);
            writer.Write(RequiredMinLevel);
            writer.Write(RequiredMaxLevel);
            writer.Write(RequiredQuest);
            writer.Write((byte)RequiredClass);
            writer.Write((byte)Type);
            writer.Write(GotoMessage);
            writer.Write(KillMessage);
            writer.Write(ItemMessage);
            writer.Write(FlagMessage);
            writer.Write(percentageExp);
            writer.Write(Time);
            writer.Write(HeroQuest);
            writer.Write(autoComplete);
        }

        public override string ToString()
        {
            return string.Format("{0}:   {1}", Index, Name);
        }
    }
}
