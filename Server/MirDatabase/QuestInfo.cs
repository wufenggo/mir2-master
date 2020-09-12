using System.Drawing;
using Server.MirObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Server.MirEnvir;

namespace Server.MirDatabase
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

        public List<QuestItemTask> CarryItems = new List<QuestItemTask>(); 

        public List<QuestKillTask> KillTasks = new List<QuestKillTask>();
        public List<QuestItemTask> ItemTasks = new List<QuestItemTask>();
        public List<QuestFlagTask> FlagTasks = new List<QuestFlagTask>();

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

            if (Envir.LoadVersion >= 38)
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
            if(Envir.LoadVersion >= 37) FlagMessage = reader.ReadString();

            if (Envir.LoadVersion > 72)
                percentageExp = reader.ReadBoolean();

            if (Envir.LoadVersion > 77)
                Time = reader.ReadInt32();

            if (Envir.LoadVersion > 103)
                HeroQuest = reader.ReadBoolean();

            if (Envir.LoadVersion > 111)
                autoComplete = reader.ReadBoolean();

            LoadInfo();
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

        public void LoadInfo(bool clear = false)
        {
            if (clear) ClearInfo();

            if (!Directory.Exists(Settings.QuestPath)) return;

            string fileName = Path.Combine(Settings.QuestPath, FileName + ".txt");

            if (File.Exists(fileName))
            {
                List<string> lines = File.ReadAllLines(fileName).ToList();

                ParseFile(lines);
            }
            else
                SMain.Enqueue(string.Format(Index.ToString() +  " File Not Found: {0}, Quest: {1}", fileName, Name));
        }

        public void ClearInfo()
        {
            Description.Clear();
            KillTasks = new List<QuestKillTask>();
            ItemTasks = new List<QuestItemTask>();
            FlagTasks = new List<QuestFlagTask>();
            FixedRewards = new List<QuestItemReward>();
            BuffRewards = new List<BuffReward>();
            SelectRewards = new List<QuestItemReward>();
            ExpReward = 0;
            GoldReward = 0;
            CreditReward = 0;
            ItemExpReward = 0;
        }

        public void ParseFile(List<string> lines)
        {
            const string
                descriptionCollectKey = "[@DESCRIPTION]",
                descriptionTaskKey = "[@TASKDESCRIPTION]",
                descriptionCompletionKey = "[@COMPLETION]",
                carryItemsKey = "[@CARRYITEMS]",
                killTasksKey = "[@KILLTASKS]",
                itemTasksKey = "[@ITEMTASKS]",
                flagTasksKey = "[@FLAGTASKS]",
                fixedRewardsKey = "[@FIXEDREWARDS]",
                buffRewards = "[@BUFFREWARDS]",
                selectRewardsKey = "[@SELECTREWARDS]",
                expRewardKey = "[@EXPREWARD]",
                goldRewardKey = "[@GOLDREWARD]",
                creditRewardKey = "[@CREDITREWARD]",
                itemExpRewardKey = "[@ITEMEXPREWARD]";

            List<string> headers = new List<string> 
            { 
                descriptionCollectKey, descriptionTaskKey, descriptionCompletionKey,
                carryItemsKey, killTasksKey, itemTasksKey, flagTasksKey,
                fixedRewardsKey, selectRewardsKey, expRewardKey, goldRewardKey, creditRewardKey,buffRewards,itemExpRewardKey
            };

            int currentHeader = 0;

            while (currentHeader < headers.Count)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i].ToUpper();

                    if (line != headers[currentHeader].ToUpper()) continue;

                    for (int j = i + 1; j < lines.Count; j++)
                    {
                        string innerLine = lines[j];

                        if (innerLine.StartsWith("[")) break;
                        if (string.IsNullOrEmpty(lines[j])) continue;

                        switch (line)
                        {
                            case descriptionCollectKey:
                                Description.Add(innerLine);
                                break;
                            case descriptionTaskKey:
                                TaskDescription.Add(innerLine);
                                break;
                            case descriptionCompletionKey:
                                CompletionDescription.Add(innerLine);
                                break;
                            case carryItemsKey:
                                QuestItemTask t = ParseItem(innerLine);
                                if (t != null) CarryItems.Add(t);
                                break;
                            case killTasksKey:
                                QuestKillTask t1 = ParseKill(innerLine);
                                if(t1 != null) KillTasks.Add(t1);
                                break;
                            case itemTasksKey:
                                QuestItemTask t2 = ParseItem(innerLine);
                                if (t2 != null) ItemTasks.Add(t2);
                                break;
                            case flagTasksKey:
                                QuestFlagTask t3 = ParseFlag(innerLine);
                                if (t3 != null) FlagTasks.Add(t3);
                                break;
                            case fixedRewardsKey:
                                {
                                    ParseReward(FixedRewards, innerLine);
                                    break;
                                }
                            case buffRewards:
                                {
                                    ParseBuffReward(BuffRewards, innerLine);
                                    break;
                                }
                            case selectRewardsKey:
                                {
                                    ParseReward(SelectRewards, innerLine);
                                    break;
                                }
                            case expRewardKey:
                                uint.TryParse(innerLine, out ExpReward);
                                break;
                            case goldRewardKey:
                                uint.TryParse(innerLine, out GoldReward);
                                break;
                            case creditRewardKey:
                                uint.TryParse(innerLine, out CreditReward);
                                break;
                            case itemExpRewardKey:
                                uint.TryParse(innerLine, out ItemExpReward);
                                break;
                        }
                    }
                }

                currentHeader++;
            }
        }

        public void ParseBuffReward(List<BuffReward> list, string line)
        {
            if (line.Length < 3) return;

            string[] split = line.Split(' ');


            if (!Enum.TryParse(split[0], out BuffType buff))
            {
                SMain.Enqueue("wrong buff " + split[0]);
                return;
            }
            int.TryParse(split[1], out int count);
            long.TryParse(split[2], out long time);

            BuffReward br = new BuffReward()
            {
                buff = buff,
                amount = count,
                time = time,
            };

            list.Add(br);
        }

        public void ParseReward(List<QuestItemReward> list, string line)
        {
            if (line.Length < 1) return;

            string[] split = line.Split(' ');
            uint count = 1;

            if (split.Length > 1) uint.TryParse(split[1], out count);

            ItemInfo mInfo = SMain.Envir.GetItemInfo(split[0]);

            if (mInfo == null)
            {
                mInfo = SMain.Envir.GetItemInfo(split[0] + "(M)");
                if (mInfo != null) list.Add(new QuestItemReward() { Item = mInfo, Count = count });

                mInfo = SMain.Envir.GetItemInfo(split[0] + "(F)");
                if (mInfo != null) list.Add(new QuestItemReward() { Item = mInfo, Count = count });
            }
            else
            {
                list.Add(new QuestItemReward() { Item = mInfo, Count = count });
            }
        }

        public QuestKillTask ParseKill(string line)
        {
            if (line.Length < 1) return null;

            string[] split = line.Split(' ');
            int count = 1;
            string message = "";

            MonsterInfo mInfo = SMain.Envir.GetMonsterInfo(split[0]);
            if (split.Length > 1) int.TryParse(split[1], out count);

            var match = _regexMessage.Match(line);
            if (match.Success)
            {
                message = match.Groups[1].Captures[0].Value;
            }

            return mInfo == null ? null : new QuestKillTask() { Monster = mInfo, Count = count, Message = message };
        }

        public QuestItemTask ParseItem(string line)
        {
            if (line.Length < 1) return null;

            string[] split = line.Split(' ');
            uint count = 1;
            string message = "";

            ItemInfo mInfo = SMain.Envir.GetItemInfo(split[0]);
            if (split.Length > 1) uint.TryParse(split[1], out count);

            var match = _regexMessage.Match(line);
            if (match.Success)
            {
                message = match.Groups[1].Captures[0].Value;
            }
            //if (mInfo.StackSize <= 1)
            //{
            //    //recursively add item if cant stack???
            //}

            return mInfo == null ? null : new QuestItemTask { Item = mInfo, Count = count, Message = message };
        }

        public QuestFlagTask ParseFlag(string line)
        {
            if (line.Length < 1) return null;

            string[] split = line.Split(' ');

            int number = -1;
            string message = "";

            int.TryParse(split[0], out number);

            if (number < 0 || number > Globals.FlagIndexCount - 1000) return null;

            var match = _regexMessage.Match(line);
            if (match.Success)
            {
                message = match.Groups[1].Captures[0].Value;
            }

            return new QuestFlagTask { Number = number, Message = message };
        }

        public bool CanAccept(PlayerObject player)
        {
            if (RequiredMinLevel > player.Level || RequiredMaxLevel < player.Level)
                return false;

            if (RequiredQuest > 0 && !player.CompletedQuests.Contains(RequiredQuest))
                return false;

            if (HeroQuest && (!player.GainedHero || player.Hero == null || !player.Hero.isSpawn))
            {
                player.ReceiveChat("You need to have hero spawned!", ChatType.System);
                return false;
            }

            switch (player.Class)
            {
                case MirClass.Warrior:
                    if (!RequiredClass.HasFlag(RequiredClass.Warrior))
                        return false;
                    break;
                case MirClass.Wizard:
                    if (!RequiredClass.HasFlag(RequiredClass.Wizard))
                        return false;
                    break;
                case MirClass.Taoist:
                    if (!RequiredClass.HasFlag(RequiredClass.Taoist))
                        return false;
                    break;
                case MirClass.Assassin:
                    if (!RequiredClass.HasFlag(RequiredClass.Assassin))
                        return false;
                    break;
                case MirClass.Archer:
                    if (!RequiredClass.HasFlag(RequiredClass.Archer))
                        return false;
                    break;
            }

            return true;
        }

        public ClientQuestInfo CreateClientQuestInfo()
        {
            return new ClientQuestInfo
            {
                Index = Index,
                NPCIndex = NpcIndex,
                FinishNPCIndex = FinishNpcIndex,
                Name = Name,
                Group = Group,
                Description = Description,
                TaskDescription = TaskDescription,
                CompletionDescription = CompletionDescription,
                MinLevelNeeded = RequiredMinLevel,
                MaxLevelNeeded = RequiredMaxLevel,
                ClassNeeded = RequiredClass,
                QuestNeeded = RequiredQuest,
                Type = Type,
                RewardGold = GoldReward,
                RewardExp = ExpReward,
                RewardCredit = CreditReward,
                RewardsFixedItem = FixedRewards,
                BuffRewards = BuffRewards,
                RewardsSelectItem = SelectRewards,
                percentageExp = percentageExp,
                autoComplete = autoComplete,
                time = Time,
                Hero = HeroQuest,
                ItemEXPReward = ItemExpReward
            };
        }

        public ClientQuestInfo UpdateQuestInfo()
        {
            return new ClientQuestInfo
            {
                Index = Index,
                NPCIndex = NpcIndex,
                FinishNPCIndex = FinishNpcIndex,
                Name = Name,
                Group = Group,
                Description = Description,
                TaskDescription = TaskDescription,
                CompletionDescription = CompletionDescription,
                MinLevelNeeded = RequiredMinLevel,
                MaxLevelNeeded = RequiredMaxLevel,
                ClassNeeded = RequiredClass,
                QuestNeeded = RequiredQuest,
                Type = Type,
                RewardGold = GoldReward,
                RewardExp = ExpReward,
                RewardCredit = CreditReward,
                RewardsFixedItem = FixedRewards,
                BuffRewards = BuffRewards,
                RewardsSelectItem = SelectRewards,
                percentageExp = percentageExp,
                autoComplete = autoComplete,
                time = Time,
                Hero = HeroQuest,
                ItemEXPReward = ItemExpReward
            };
        }

        public QuestInfo UpdateQuestInfo(QuestInfo m)
        {
            CarryItems = m.CarryItems;
            CompletionDescription = m.CompletionDescription;
            CreditReward = m.CreditReward;
            Description = m.Description;
            ExpReward = m.ExpReward;
            FileName = m.FileName;
            FixedRewards = m.FixedRewards;
            BuffRewards = m.BuffRewards;
            FlagMessage = m.FlagMessage;
            FlagTasks = m.FlagTasks;
            GoldReward = m.GoldReward;
            GotoMessage = m.GotoMessage;
            Group = m.Group;
            Index = m.Index;
            ItemMessage = m.ItemMessage;
            ItemTasks = m.ItemTasks;
            KillMessage = m.KillMessage;
            KillTasks = m.KillTasks;
            Name = m.Name;
            NpcIndex = m.NpcIndex;
            NpcInfo = m.NpcInfo;
            percentageExp = m.percentageExp;
            autoComplete = m.autoComplete;
            RequiredClass = m.RequiredClass;
            RequiredMaxLevel = m.RequiredMaxLevel;
            RequiredMinLevel = m.RequiredMinLevel;
            RequiredQuest = m.RequiredQuest;
            SelectRewards = m.SelectRewards;
            TaskDescription = m.TaskDescription;
            Time = m.Time;
            Type = m.Type;
            _finishNpcIndex = m._finishNpcIndex;
            _regexMessage = m._regexMessage;
            ItemExpReward = m.ItemExpReward;
            return this;
        }

        public static QuestInfo CloneQuest(QuestInfo m)
        {
            QuestInfo newQuest = new QuestInfo(m.HeroQuest)
            {
                CarryItems = m.CarryItems,
                CompletionDescription = m.CompletionDescription,
                CreditReward = m.CreditReward,
                Description = m.Description,
                ExpReward = m.ExpReward,
                FileName = m.FileName,
                FixedRewards = m.FixedRewards,
                BuffRewards = m.BuffRewards,
                FlagMessage = m.FlagMessage,
                FlagTasks = m.FlagTasks,
                GoldReward = m.GoldReward,
                GotoMessage = m.GotoMessage,
                Group = m.Group,
                Index = m.Index,
                ItemMessage = m.ItemMessage,
                ItemTasks = m.ItemTasks,
                KillMessage = m.KillMessage,
                KillTasks = m.KillTasks,
                Name = m.Name,
                NpcIndex = m.NpcIndex,
                NpcInfo = m.NpcInfo,
                percentageExp = m.percentageExp,
                autoComplete = m.autoComplete,
                RequiredClass = m.RequiredClass,
                RequiredMaxLevel = m.RequiredMaxLevel,
                RequiredMinLevel = m.RequiredMinLevel,
                RequiredQuest = m.RequiredQuest,
                SelectRewards = m.SelectRewards,
                TaskDescription = m.TaskDescription,
                Time = m.Time,
                Type = m.Type,
                _finishNpcIndex = m._finishNpcIndex,
                _regexMessage = m._regexMessage,
                ItemExpReward = m.ItemExpReward
            };


            return newQuest;

        }

        public static void FromText(string text, bool hero = false)
        {
            string[] data = text.Split(new[] { ',' });

            if (data.Length < 13) return;

            QuestInfo info = new QuestInfo(false)
            {
                Name = data[0],
                Group = data[1]
            };


            byte.TryParse(data[2], out byte temp);

            info.Type = (QuestType)temp;

            info.FileName = data[3];
            info.GotoMessage = data[4];
            info.KillMessage = data[5];
            info.ItemMessage = data[6];
            info.FlagMessage = data[7];

            int.TryParse(data[8], out info.RequiredMinLevel);
            int.TryParse(data[9], out info.RequiredMaxLevel);
            int.TryParse(data[10], out info.RequiredQuest);


            byte.TryParse(data[11], out temp);

            bool.TryParse(data[12], out info.HeroQuest);

            bool.TryParse(data[13], out info.autoComplete);

            int.TryParse(data[14], out info.Time);


            info.RequiredClass = (RequiredClass)temp;

            info.HeroQuest = hero;

            info.Index = ++SMain.EditEnvir.QuestIndex;
            SMain.EditEnvir.QuestInfoList.Add(info);
        }

        public string ToText()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",
                Name, Group, (byte)Type, FileName, GotoMessage, KillMessage, ItemMessage, FlagMessage, RequiredMinLevel, RequiredMaxLevel, RequiredQuest, (byte)RequiredClass,HeroQuest,autoComplete,Time);
        }

        public override string ToString()
        {
            return string.Format("{0}:   {1}", Index, Name);
        }
    }

    public class QuestKillTask
    {
        public MonsterInfo Monster;
        public int Count;
        public string Message;
    }

    public class QuestItemTask
    {
        public ItemInfo Item;
        public uint Count;
        public string Message;
    }

    public class QuestFlagTask
    {
        public int Number;
        public string Message;
    }
}
