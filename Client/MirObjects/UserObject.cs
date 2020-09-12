using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Client.MirScenes;
using S = ServerPackets;

namespace Client.MirObjects
{
    public class UserObject : PlayerObject
    {
        public uint Id;

        public ushort HP, MaxHP, MP, MaxMP;

        public ushort MinAC, MaxAC,
                   MinMAC, MaxMAC,
                   MinDC, MaxDC,
                   MinMC, MaxMC,
                   MinSC, MaxSC;

        public byte Accuracy, Agility;
        public sbyte ASpeed, Luck;
        public int AttackSpeed;
        public sbyte OrigAttackSpeed;
        public ushort CurrentHandWeight, MaxHandWeight,
                      CurrentWearWeight, MaxWearWeight;
        public ushort CurrentBagWeight, MaxBagWeight;
        public long Experience, MaxExperience;
        public byte LifeOnHit;

        public bool TradeLocked;
        public bool AllowTrade;

        public bool HasTeleportRing, HasProtectionRing, HasRevivalRing, HasClearRing,
            HasMuscleRing, HasParalysisRing, HasFireRing, HasHealRing, HasProbeNecklace, HasSkillNecklace, NoDuraLoss;

        public byte MagicResist, PoisonResist, HealthRecovery, SpellRecovery, PoisonRecovery, CriticalRate, CriticalDamage, Holy, Freezing, PoisonAttack, HpDrainRate;
        public BaseStats CoreStats = new BaseStats(0);

        public List<PlayerRecipe> Recipes = new List<PlayerRecipe>();       // was 14
        public UserItem[] Inventory = new UserItem[46], Equipment = new UserItem[17], Trade = new UserItem[10], QuestInventory = new UserItem[40];
        public int BeltIdx = 6;
        public bool AddedStorage = false;
        public List<ClientMagic> Magics = new List<ClientMagic>();
        public List<ItemSets> ItemSets = new List<ItemSets>();
        public List<EquipmentSlot> MirSet = new List<EquipmentSlot>();

        public List<ClientIntelligentCreature> IntelligentCreatures = new List<ClientIntelligentCreature>();//IntelligentCreature
        public IntelligentCreatureType SummonedCreatureType = IntelligentCreatureType.None;//IntelligentCreature
        public bool CreatureSummoned;//IntelligentCreature
        public int PearlCount = 0;

        public List<ClientQuestProgress> CurrentQuests = new List<ClientQuestProgress>();
        public List<int> CompletedQuests = new List<int>();
        public List<ClientQuestCompleted> ClientCompletedQuests = new List<ClientQuestCompleted>();
        public List<ClientAvailableQuests> AvailableQuests = new List<ClientAvailableQuests>();
        public List<ClientMail> Mail = new List<ClientMail>();

        public ClientMagic NextMagic;
        public Point NextMagicLocation;
        public MapObject NextMagicObject;
        public MirDirection NextMagicDirection;
        public QueuedAction QueuedAction;

        public Point FRCastLocation;

        public ushort PvEDamageIncrease = 0,
                    PvPDamageIncrease = 0,
                    PvEDamageReduction = 0,
                    PvPDamageReduction = 0,
                    MeleeDamageIncrease = 0,
                    MagicDamageBonus = 0,
                    SpiritualBonus = 0;


        public sbyte HeroCap;
        public sbyte CurrentHeroCount;

        public UserObject(uint objectID) : base(objectID)
        {
        }

        public void Load(S.UserInformation info)
        {
            HeroCap = info.HeroCap;
            CurrentHeroCount = info.CurrentHeroCount;
            Id = info.RealId;
            Name = info.Name;
            Settings.LoadTrackedQuests(info.Name);
            NameColour = info.NameColour;
            GuildName = info.GuildName;
            GuildRankName = info.GuildRank;
            Class = info.Class;
            Gender = info.Gender;
            Level = info.Level;
            OrigLevel = Math.Min(ushort.MaxValue, (ushort)(info.Level * 30 / 100));
            CurrentLocation = info.Location;
            MapLocation = info.Location;
            GameScene.Scene.MapControl.AddObject(this);

            Direction = info.Direction;
            Hair = info.Hair;


            Helmet = info.Helmet;


            VIP = info.VIP;
            HumUp = info.HumUp;
            HasHero = info.HasHero;
            OutLine_Colour = info.OutLineColour;

            HP = info.HP;
            MP = info.MP;
            
            Experience = info.Experience;
            MaxExperience = info.MaxExperience;

            LevelEffects = info.LevelEffects;

            Inventory = info.Inventory;
            Equipment = info.Equipment;
            QuestInventory = info.QuestInventory;         

            AddedStorage = info.AddedStorage;

            Magics = info.Magics;
            for (int i = 0; i < Magics.Count; i++ )
            {
                if (Magics[i].CastTime > 0)
                    Magics[i].CastTime = CMain.Time - Magics[i].CastTime;
            }
            IsGM = info.IsGM;
            IsDev = info.IsDev;
            ShowGMEffect = info.ShowGMEffect;
            for (int i = 0; i < info.Recipes.Count; i++)
            {
                PlayerRecipe recipe = new PlayerRecipe
                {
                    Recipe = info.Recipes[i].Recipe,
                    Learnt = info.Recipes[i].Learnt,
                    CraftEnd = info.Recipes[i].Finishedcraft,
                    CraftEndTime = info.Recipes[i].CurrentCraftTime,
                    Collected = info.Recipes[i].Collected,
                    InPrcoess = info.Recipes[i].InProgress
                };
                Recipes.Add(recipe);
            }
            IntelligentCreatures = info.IntelligentCreatures;//IntelligentCreature
            SummonedCreatureType = info.SummonedCreatureType;//IntelligentCreature
            CreatureSummoned = info.CreatureSummoned;//IntelligentCreature

            BindAllItems();

            RefreshStats();

            SetAction();
        }

        public override void SetLibraries()
        {
            base.SetLibraries();
        }

        public override void SetEffects()
        {
            base.SetEffects();
        }

        public void RefreshStats()
        {
            PvEDamageIncrease = 0;
            PvPDamageIncrease = 0;
            PvEDamageReduction = 0;
            PvPDamageReduction = 0;
            MeleeDamageIncrease = 0;
            MagicDamageBonus = 0;
            SpiritualBonus = 0;

            RefreshLevelStats();
            RefreshBagWeight();
            RefreshEquipmentStats();
            RefreshItemSetStats();
            RefreshMirSetStats();
            RefreshSkills();
            RefreshBuffs();
            RefreshMountStats();
            RefreshGuildBuffs();
            //RefreshFishingStats();

            SetLibraries();
            SetEffects();
            
            if (this == User && Light < 3) Light = 3;
            AttackSpeed = 1400 - ((ASpeed * 40) + Math.Min(370, (Level * 14)));
            if (AttackSpeed < 550) AttackSpeed = 550;
            OrigAttackSpeed = Math.Min(sbyte.MaxValue, (sbyte)(ASpeed * 30 / 100));
            PercentHealth = (byte)(HP / (float)MaxHP * 100);
            if (Equipment[(int)EquipmentSlot.Weapon] != null)
            {
                ItemInfo weap = GameScene.GetInfo(Equipment[(int)EquipmentSlot.Weapon].ItemIndex);
                if (weap.AllowLvlSys)
                {
                    switch (Equipment[(int)EquipmentSlot.Weapon].LvlSystem)
                    {
                        case 1:
                            Weapon = weap.LevelWeapLooks[0] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[0];
                            WeaponEffect = weap.LevelWeapEffect[0] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[0];
                            break;
                        case 2:
                            Weapon = weap.LevelWeapLooks[1] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[1];
                            WeaponEffect = weap.LevelWeapEffect[1] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[1];
                            break;
                        case 3:
                            Weapon = weap.LevelWeapLooks[2] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[2];
                            WeaponEffect = weap.LevelWeapEffect[2] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[2];
                            break;
                        case 4:
                            Weapon = weap.LevelWeapLooks[3] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[3];
                            WeaponEffect = weap.LevelWeapEffect[3] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[3];
                            break;
                        case 5:
                            Weapon = weap.LevelWeapLooks[4] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[4];
                            WeaponEffect = weap.LevelWeapEffect[4] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[4];
                            break;
                        case 6:
                            Weapon = weap.LevelWeapLooks[5] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[5];
                            WeaponEffect = weap.LevelWeapEffect[5] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[5];
                            break;
                        case 7:
                            Weapon = weap.LevelWeapLooks[6] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[6];
                            WeaponEffect = weap.LevelWeapEffect[6] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[6];
                            break;
                        case 8:
                            Weapon = weap.LevelWeapLooks[7] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[7];
                            WeaponEffect = weap.LevelWeapEffect[7] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[7];
                            break;
                        case 9:
                            Weapon = weap.LevelWeapLooks[8] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[8];
                            WeaponEffect = weap.LevelWeapEffect[8] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[8];
                            break;
                        case 10:
                            Weapon = weap.LevelWeapLooks[9] == 0 ? weap.Shape : (short)weap.LevelWeapLooks[9];
                            WeaponEffect = weap.LevelWeapEffect[9] == 0 ? weap.Effect : (byte)weap.LevelWeapEffect[9];
                            break;
                        case 0:
                        default:
                            Weapon = weap.Shape;
                            WeaponEffect = weap.Effect;
                            break;
                    }
                }
            }
            if (Equipment[(int)EquipmentSlot.Armour] != null)
            {
                ItemInfo weap = GameScene.GetInfo(Equipment[(int)EquipmentSlot.Armour].ItemIndex);
                if (weap.AllowLvlSys)
                {
                    switch (Equipment[(int)EquipmentSlot.Armour].LvlSystem)
                    {
                        case 1:
                            Armour = weap.LevelArmourLooks[0] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[0];
                            WingEffect = weap.LevelArmourEffect[0] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[0];
                            break;
                        case 2:
                            Armour = weap.LevelArmourLooks[1] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[1];
                            WingEffect = weap.LevelArmourEffect[1] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[1];
                            break;
                        case 3:
                            Armour = weap.LevelArmourLooks[2] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[2];
                            WingEffect = weap.LevelArmourEffect[2] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[2];
                            break;
                        case 4:
                            Armour = weap.LevelArmourLooks[3] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[3];
                            WingEffect = weap.LevelArmourEffect[3] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[3];
                            break;
                        case 5:
                            Armour = weap.LevelArmourLooks[4] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[4];
                            WingEffect = weap.LevelArmourEffect[4] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[4];
                            break;
                        case 6:
                            Armour = weap.LevelArmourLooks[5] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[5];
                            WingEffect = weap.LevelArmourEffect[5] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[5];
                            break;
                        case 7:
                            Armour = weap.LevelArmourLooks[6] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[6];
                            WingEffect = weap.LevelArmourEffect[6] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[6];
                            break;
                        case 8:
                            Armour = weap.LevelArmourLooks[7] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[7];
                            WingEffect = weap.LevelArmourEffect[7] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[7];
                            break;
                        case 9:
                            Armour = weap.LevelArmourLooks[8] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[8];
                            WingEffect = weap.LevelArmourEffect[8] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[8];
                            break;
                        case 10:
                            Armour = weap.LevelArmourLooks[9] == 0 ? weap.Shape : (short)weap.LevelArmourLooks[9];
                            WingEffect = weap.LevelArmourEffect[9] == 0 ? weap.Effect : (byte)weap.LevelArmourEffect[9];
                            break;
                        case 0:
                        default:
                            Armour = weap.Shape;
                            WingEffect = weap.Effect;
                            break;
                    }
                }
            }
            GameScene.Scene.Redraw();
        }
        private void RefreshLevelStats()
        {
            MaxHP = 0; MaxMP = 0;
            MinAC = 0; MaxAC = 0;
            MinMAC = 0; MaxMAC = 0;
            MinDC = 0; MaxDC = 0;
            MinMC = 0; MaxMC = 0;
            MinSC = 0; MaxSC = 0;


            //Other Stats;
            MaxBagWeight = 0;
            MaxWearWeight = 0;
            MaxHandWeight = 0;
            ASpeed = 0;
            Luck = 0;
            Light = 0;
            LifeOnHit = 0;
            HpDrainRate = 0;
            MagicResist = 0;
            PoisonResist = 0;
            HealthRecovery = 0;
            SpellRecovery = 0;
            PoisonRecovery = 0;
            Holy = 0;
            Freezing = 0;
            PoisonAttack = 0;

            Accuracy = CoreStats.StartAccuracy;
            Agility = CoreStats.StartAgility;
            CriticalRate = CoreStats.StartCriticalRate;
            CriticalDamage = CoreStats.StartCriticalDamage;

            MaxHP = (ushort)Math.Min(ushort.MaxValue, 14 + (Level / CoreStats.HpGain + CoreStats.HpGainRate) * Level);

            MinAC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MinAc > 0 ? Level / CoreStats.MinAc : 0);
            MaxAC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MaxAc > 0 ? Level / CoreStats.MaxAc : 0);
            MinMAC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MinMac > 0 ? Level / CoreStats.MinMac : 0);
            MaxMAC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MaxMac > 0 ? Level / CoreStats.MaxMac : 0);
            MinDC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MinDc > 0 ? Level / CoreStats.MinDc : 0);
            MaxDC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MaxDc > 0 ? Level / CoreStats.MaxDc : 0);
            MinMC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MinMc > 0 ? Level / CoreStats.MinMc : 0);
            MaxMC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MaxMc > 0 ? Level / CoreStats.MaxMc : 0);
            MinSC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MinSc > 0 ? Level / CoreStats.MinSc : 0);
            MaxSC = (ushort)Math.Min(ushort.MaxValue, CoreStats.MaxSc > 0 ? Level / CoreStats.MaxSc : 0);
            CriticalRate = (byte)Math.Min(byte.MaxValue, CoreStats.CritialRateGain > 0 ? CriticalRate + (Level / CoreStats.CritialRateGain) : CriticalRate);
            CriticalDamage = (byte)Math.Min(byte.MaxValue, CoreStats.CriticalDamageGain > 0 ? CriticalDamage + (Level / CoreStats.CriticalDamageGain) : CriticalDamage);
            MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, 50 + Level / CoreStats.BagWeightGain * Level);
            MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, 15 + Level / CoreStats.WearWeightGain * Level);
            MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, 12 + Level / CoreStats.HandWeightGain * Level);


            switch (Class)
            {
                case MirClass.Warrior:
                    MaxHP = (ushort)Math.Min(ushort.MaxValue, 14 + (Level / CoreStats.HpGain + CoreStats.HpGainRate + Level / 20F) * Level);
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, 11 + (Level * 3.5F) + (Level * CoreStats.MpGainRate));
                    break;
                case MirClass.Wizard:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, 13 + ((Level / 5F + 2F) * 2.2F * Level) + (Level * CoreStats.MpGainRate));
                    break;
                case MirClass.Taoist:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (13 + Level / 8F * 2.2F * Level) + (Level * CoreStats.MpGainRate));
                    break;
                case MirClass.Assassin:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (11 + Level * 5F) + (Level * CoreStats.MpGainRate));
                    break;
                case MirClass.Archer:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (11 + Level * 4F) + (Level * CoreStats.MpGainRate));
                    break;
            }
        }
        private void RefreshBagWeight()
        {
            CurrentBagWeight = 0;

            for (int i = 0; i < Inventory.Length; i++)
            {
                UserItem item = Inventory[i];
                if (item != null
                    && item.Info != null && item.Info.Type != ItemType.RuneStone)
                    CurrentBagWeight = (ushort)Math.Min(ushort.MaxValue, CurrentBagWeight + item.Weight);
            }
        }
        private void RefreshEquipmentStats()
        {
            if (Equipment[(int)EquipmentSlot.Weapon] == null)
                Weapon = -1;
            if (Equipment[(int)EquipmentSlot.Armour] == null)
                Armour = 0;
            WingEffect = 0;
            MountType = -1;
            WeaponEffect = 0;

            CurrentWearWeight = 0;
            CurrentHandWeight = 0;

            HasTeleportRing = false;
            HasProtectionRing = false;
            HasMuscleRing = false;
            HasParalysisRing = false;
            HasProbeNecklace = false;
            HasSkillNecklace = false;
            NoDuraLoss = false;
            FastRun = false;//here
            short Macrate = 0, Acrate = 0, HPrate = 0, MPrate = 0;

            ItemSets.Clear();
            MirSet.Clear();

            for (int i = 0; i < Equipment.Length; i++)//loops through the array which would have stopped at 13 which was mount
            {
                UserItem temp = Equipment[i];
                if (temp == null) continue;

                ItemInfo RealItem = Functions.GetRealItem(temp.Info, Level, Class, GameScene.ItemInfoList);

                if (RealItem.Type == ItemType.Weapon || RealItem.Type == ItemType.Torch)
                    CurrentHandWeight = (ushort)Math.Min(ushort.MaxValue, CurrentHandWeight + temp.Weight);
                else
                    CurrentWearWeight = (ushort)Math.Min(ushort.MaxValue, CurrentWearWeight + temp.Weight);

                //if (RealItem.AllowLvlSys &&
                //    temp.LvlSystem >= 1 &&
                //    temp.LvlSystem <= RealItem.LvlSysMaxAC.Length - 1)
                //{
                //    if (RealItem.LvlSysMinAC[temp.LvlSystem - 1] > 0)
                //    {
                //        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + RealItem.LvlSysMinAC[temp.LvlSystem - 1]);
                //        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + RealItem.LvlSysMaxAC[temp.LvlSystem - 1]);
                //        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + RealItem.LvlSysMinMAC[temp.LvlSystem - 1]);
                //        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + RealItem.LvlSysMaxMAC[temp.LvlSystem - 1]);
                //        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + RealItem.LvlSysMinDC[temp.LvlSystem - 1]);
                //        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + RealItem.LvlSysMaxDC[temp.LvlSystem - 1]);
                //        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + RealItem.LvlSysMinMC[temp.LvlSystem - 1]);
                //        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + RealItem.LvlSysMaxMC[temp.LvlSystem - 1]);
                //        MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + RealItem.LvlSysMinSC[temp.LvlSystem - 1]);
                //        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + RealItem.LvlSysMaxSC[temp.LvlSystem - 1]);
                //    }
                //}

                if (temp.CurrentDura == 0 && RealItem.Durability > 0) continue;
                if (temp.SocketCount > 0 && temp.Sockets.Count > 0)
                {
                    for (int x = 0; x < temp.Sockets.Count; x++)
                    {
                        if (temp.Sockets[x].SocketItemType == SocketType.DamageIncreasePvE)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                PvEDamageIncrease += (ushort)(PvEDamageIncrease * tmp.MaxMAC / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.DamageIncreasePvP)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                PvPDamageIncrease += (ushort)(PvPDamageIncrease * tmp.MaxAC / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.DamageReductionPvE)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                PvEDamageReduction += (ushort)(PvEDamageReduction * tmp.MinMAC / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.DamageReductionPvP)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                PvPDamageReduction += (ushort)(PvPDamageReduction * tmp.MinAC / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.MeleeDamageBonus &&
                            (Class == MirClass.Warrior ||
                            Class == MirClass.Assassin ||
                            Class == MirClass.Archer)
                            )
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                MeleeDamageIncrease += (ushort)(MeleeDamageIncrease * tmp.MaxAcRate / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.MagicDamageBonus &&
                            Class == MirClass.Wizard)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                MagicDamageBonus += (ushort)(MagicDamageBonus * tmp.MaxMacRate / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.SpiritualBonus &&
                            Class == MirClass.Taoist)
                        {
                            ItemInfo tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            if (tmp != null)
                                SpiritualBonus += (ushort)(SpiritualBonus * tmp.Holy / 100);
                        }
                        else if (temp.Sockets[x].SocketItemType == SocketType.NONE)
                        {
                            ItemInfo _tmp = GameScene.GetInfo(temp.Sockets[x].SocketedItem.ItemIndex);
                            MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + _tmp.MinAC);
                            MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + _tmp.MaxAC);
                            MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + _tmp.MinMAC);
                            MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + _tmp.MaxMAC);

                            MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + _tmp.MinDC);
                            MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + _tmp.MaxDC);
                            MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + _tmp.MinMC);
                            MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + _tmp.MaxMC);
                            MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + _tmp.MinSC);
                            MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + _tmp.MaxSC);

                            Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + _tmp.Accuracy);
                            Agility = (byte)Math.Min(byte.MaxValue, Agility + _tmp.Agility);

                            MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + _tmp.HP);
                            MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + _tmp.MP);

                            HPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, HPrate + _tmp.HPrate));
                            MPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, MPrate + _tmp.MPrate));
                        }
                    }
                }

                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + RealItem.MinAC + temp.Awake.getAC());
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + RealItem.MaxAC + temp.AC + temp.Awake.getAC());
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + RealItem.MinMAC + temp.Awake.getMAC());
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + RealItem.MaxMAC + temp.MAC + temp.Awake.getMAC());

                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + RealItem.MinDC + temp.Awake.DC);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + RealItem.MaxDC + temp.DC + temp.Awake.DC);
                MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + RealItem.MinMC + temp.Awake.getMC());
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + RealItem.MaxMC + temp.MC + temp.Awake.getMC());
                MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + RealItem.MinSC + temp.Awake.getSC());
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + RealItem.MaxSC + temp.SC + temp.Awake.getSC());

                Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + RealItem.Accuracy + temp.Accuracy);
                Agility = (byte)Math.Min(byte.MaxValue, Agility + RealItem.Agility + temp.Agility);

                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + RealItem.HP + temp.HP + temp.Awake.getHPMP());
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + RealItem.MP + temp.MP + temp.Awake.getHPMP());

                ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + temp.AttackSpeed + RealItem.AttackSpeed)));
                Luck = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, Luck + temp.Luck + RealItem.Luck)));

                MaxBagWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(ushort.MaxValue, MaxBagWeight + RealItem.BagWeight)));
                MaxWearWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(byte.MaxValue, MaxWearWeight + RealItem.WearWeight)));
                MaxHandWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(byte.MaxValue, MaxHandWeight + RealItem.HandWeight)));
                HPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, HPrate + RealItem.HPrate));
                MPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, MPrate + RealItem.MPrate));
                Acrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, Acrate + RealItem.MaxAcRate));
                Macrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, Macrate + RealItem.MaxMacRate));
                MagicResist = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, MagicResist + temp.MagicResist + RealItem.MagicResist)));
                PoisonResist = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonResist + temp.PoisonResist + RealItem.PoisonResist)));
                HealthRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, HealthRecovery + temp.HealthRecovery + RealItem.HealthRecovery)));
                SpellRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, SpellRecovery + temp.ManaRecovery + RealItem.SpellRecovery)));
                PoisonRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonRecovery + temp.PoisonRecovery + RealItem.PoisonRecovery)));
                CriticalRate = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalRate + temp.CriticalRate + RealItem.CriticalRate)));
                CriticalDamage = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalDamage + temp.CriticalDamage + RealItem.CriticalDamage)));
                Holy = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, Holy + RealItem.Holy)));
                Freezing = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, Freezing + temp.Freezing + RealItem.Freezing)));
                PoisonAttack = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonAttack + temp.PoisonAttack + RealItem.PoisonAttack)));
                //Reflect = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, Refl + RealItem.Reflect)));
                HpDrainRate = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, HpDrainRate + RealItem.HpDrainRate)));




                if (RealItem.Light > Light) Light = RealItem.Light;
                if (RealItem.Unique != SpecialItemMode.None)
                {
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Paralize)) HasParalysisRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Teleport)) HasTeleportRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Clearring)) HasClearRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Protection)) HasProtectionRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Revival)) HasRevivalRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Muscle)) HasMuscleRing = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Probe)) HasProbeNecklace = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.Skill)) HasSkillNecklace = true;
                    if (RealItem.Unique.HasFlag(SpecialItemMode.NoDuraLoss)) NoDuraLoss = true;
                }

                if (RealItem.CanFastRun)
                {
                    FastRun = true;
                }

                if (RealItem.Type == ItemType.Armour &&
                    !RealItem.AllowLvlSys) 
                {
                    Armour = RealItem.Shape;
                    WingEffect = RealItem.Effect;
             
                }
                if (RealItem.Type == ItemType.Weapon &&
                    !RealItem.AllowLvlSys)
                {

                    Weapon = RealItem.Shape;
                    WeaponEffect = RealItem.Effect;
                }

                if (RealItem.Type == ItemType.Mount)
                    MountType = RealItem.Shape;

                if (RealItem.Set == ItemSet.None) continue;

                ItemSets itemSet = ItemSets.Where(set => set.Set == RealItem.Set && !set.Type.Contains(RealItem.Type) && !set.SetComplete).FirstOrDefault();

                if (itemSet != null)
                {
                    itemSet.Type.Add(RealItem.Type);
                    itemSet.Count++;
                }
                else
                {
                    ItemSets.Add(new ItemSets { Count = 1, Set = RealItem.Set, Type = new List<ItemType> { RealItem.Type } });
                }

                //Mir Set
                if (RealItem.Set == ItemSet.Mir)
                {
                    if (!MirSet.Contains((EquipmentSlot)i))
                        MirSet.Add((EquipmentSlot)i);
                }
            }

            MaxHP = (ushort)Math.Min(ushort.MaxValue, (((double)HPrate / 100) + 1) * MaxHP);
            MaxMP = (ushort)Math.Min(ushort.MaxValue, (((double)MPrate / 100) + 1) * MaxMP);
            MaxAC = (ushort)Math.Min(ushort.MaxValue, (((double)Acrate / 100) + 1) * MaxAC);
            MaxMAC = (ushort)Math.Min(ushort.MaxValue, (((double)Macrate / 100) + 1) * MaxMAC);

            if (HasMuscleRing)
            {
                MaxBagWeight = (ushort)(MaxBagWeight * 2);
                MaxWearWeight = Math.Min(ushort.MaxValue, (ushort)(MaxWearWeight * 2));
                MaxHandWeight = Math.Min(ushort.MaxValue, (ushort)(MaxHandWeight * 2));
            }

        }

        private void RefreshItemSetStats()
        {
            foreach (var s in ItemSets)
            {
                if ((s.Set == ItemSet.Smash) && (s.Type.Contains(ItemType.Ring)) && (s.Type.Contains(ItemType.Bracelet)))
                    ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                if ((s.Set == ItemSet.Purity) && (s.Type.Contains(ItemType.Ring)) && (s.Type.Contains(ItemType.Bracelet)))
                    Holy = Math.Min(byte.MaxValue, (byte)(Holy + 3));
                if ((s.Set == ItemSet.HwanDevil) && (s.Type.Contains(ItemType.Ring)) && (s.Type.Contains(ItemType.Bracelet)))
                {
                    MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 5);
                    MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                }

                if (!s.SetComplete) continue;
                switch (s.Set)
                {
                    case ItemSet.Mundane:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 50);
                        break;
                    case ItemSet.NokChi:
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 50);
                        break;
                    case ItemSet.TaoProtect:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 30);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 30);
                        break;
                    case ItemSet.RedOrchid:
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 2);
                        HpDrainRate = (byte)Math.Min(byte.MaxValue, HpDrainRate + 10);
                        break;
                    case ItemSet.RedFlower:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 50);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP - 25);
                        break;
                    case ItemSet.Smash:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 3);
                        //ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.HwanDevil:
                        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + 1);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                        MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 5);
                        break;
                    case ItemSet.Purity:
                        MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        Holy = (byte)Math.Min(ushort.MaxValue, Holy + 3);
                        break;
                    case ItemSet.FiveString:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + ((MaxHP / 100) * 30));
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        break;
                    case ItemSet.Spirit:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 2);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 5);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.Bone:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        break;
                    case ItemSet.Bug:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                        PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 1);
                        break;
                    case ItemSet.WhiteGold:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        break;
                    case ItemSet.WhiteGoldH:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 3);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 30);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.RedJade:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 2);
                        break;
                    case ItemSet.RedJadeH:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 40);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 2);
                        break;
                    case ItemSet.Nephrite:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                        break;
                    case ItemSet.NephriteH:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 15);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 20);
                        Holy = (byte)Math.Min(byte.MaxValue, Holy + 1);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        break;
                    case ItemSet.Whisker1:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 25);
                        break;
                    case ItemSet.Whisker2:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Whisker3:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Whisker4:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                        break;
                    case ItemSet.Whisker5:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Hyeolryong:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 15);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 20);
                        Holy = (byte)Math.Min(byte.MaxValue, Holy + 1);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        break;
                    case ItemSet.Monitor:
                        MagicResist = (byte)Math.Min(byte.MaxValue, MagicResist + 1);
                        PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 1);
                        break;
                    case ItemSet.Oppressive:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
                        break;
                    case ItemSet.BlueFrost:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 50);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
                        break;
                }
            }
        }

        private void RefreshMirSetStats()
        {
            if (MirSet.Count() == 10)
            {
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 70);
                Luck = (sbyte)Math.Min(sbyte.MaxValue, Luck + 2);
                ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 70);
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 80);
                MagicResist = (byte)Math.Min(byte.MaxValue, MagicResist + 6);
                PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 6);
            }

            if (MirSet.Contains(EquipmentSlot.RingL) && MirSet.Contains(EquipmentSlot.RingR))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
            }
            if (MirSet.Contains(EquipmentSlot.BraceletL) && MirSet.Contains(EquipmentSlot.BraceletR))
            {
                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + 1);
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + 1);
            }
            if ((MirSet.Contains(EquipmentSlot.RingL) | MirSet.Contains(EquipmentSlot.RingR)) && (MirSet.Contains(EquipmentSlot.BraceletL) | MirSet.Contains(EquipmentSlot.BraceletR)) && MirSet.Contains(EquipmentSlot.Necklace))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 30);
                MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 17);
            }
            if (MirSet.Contains(EquipmentSlot.RingL) && MirSet.Contains(EquipmentSlot.RingR) && MirSet.Contains(EquipmentSlot.BraceletL) && MirSet.Contains(EquipmentSlot.BraceletR) && MirSet.Contains(EquipmentSlot.Necklace))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 10);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Helmet) && MirSet.Contains(EquipmentSlot.Weapon))
            {
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Boots) && MirSet.Contains(EquipmentSlot.Belt))
            {
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, MaxHandWeight + 17);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Boots) && MirSet.Contains(EquipmentSlot.Belt) && MirSet.Contains(EquipmentSlot.Helmet) && MirSet.Contains(EquipmentSlot.Weapon))
            {
                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + 1);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, MaxHandWeight + 17);
            }
        }

        private void RefreshSkills()
        {
            for (int i = 0; i < Magics.Count; i++)
            {
                ClientMagic magic = Magics[i];
                switch (magic.Spell)
                {
                    case Spell.Fencing:
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + magic.Level * 3);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + (magic.Level + 1) * 3);
                        break;
                    case Spell.Slaying:
                        if (magic.Level > 0)
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 3 / 3 * magic.Level);
                        break;
                    case Spell.FatalSword:
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + magic.Level * 2);
                        break;
                    case Spell.SpiritSword:
                        if (magic.Level > 0)
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + magic.Level);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + MaxSC * (magic.Level + 1) * 0.1F);
                        break;
                }
            }
        }
        private void RefreshBuffs()
        {
            TransformType = -1;

            for (int i = 0; i < GameScene.Scene.Buffs.Count; i++)
            {
                Buff buff = GameScene.Scene.Buffs[i];

                if (buff.Hero) continue;

                switch (buff.Type)
                {
                    case BuffType.Haste:
                    case BuffType.Fury:
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[0])));
                        break;
                    case BuffType.ImmortalSkin:
                        MaxHP = (ushort)Math.Max(ushort.MinValue, MaxHP - buff.Values[5]);
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[1]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[2]);
                        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[3]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[4]);
                        break;
                    case BuffType.SwiftFeet:
                        Sprint = true;
                        break;
                    case BuffType.FastMove:
                        FastChannel = true;
                        break;
                    case BuffType.LightBody:
                        Agility = (byte)Math.Min(ushort.MaxValue, Agility + buff.Values[0]);
                        break;
                    case BuffType.SoulShield:
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[0]);
                        break;
                    case BuffType.BlessedArmour:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        break;
                    case BuffType.UltimateEnhancer:
                    case BuffType.UltimateEnhancerQuest:
                        if (Class == MirClass.Wizard || Class == MirClass.Archer)
                        {
                            MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[0]);
                        }
                        else if (Class == MirClass.Taoist)
                        {
                            MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[0]);
                        }
                        else
                        {
                            MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        }
                        break;
                    case BuffType.CombinedBuff:
                        {
                            if (buff.Values.Length < 1) return;
                            if (Class == MirClass.Wizard || Class == MirClass.Archer)
                            {
                                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[0]);
                            }
                            else if (Class == MirClass.Taoist)
                            {
                                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[0]);
                            }
                            else
                            {
                                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                            }
                            MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[1]);
                            MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[2]);
                        }
                        break;
                    case BuffType.ProtectionField:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        break;
                    case BuffType.Rage:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        break;
                    case BuffType.CounterAttack:
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[0]);
                        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[0]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[0]);
                        break;
                    case BuffType.Curse:
                        ushort rMaxDC = (ushort)(((int)MaxDC / 100) * buff.Values[0]);
                        ushort rMaxMC = (ushort)(((int)MaxMC / 100) * buff.Values[0]);
                        ushort rMaxSC = (ushort)(((int)MaxSC / 100) * buff.Values[0]);
                        byte rASpeed = (byte)(((int)ASpeed / 100) * buff.Values[0]);

                        MaxDC = (ushort)Math.Max(ushort.MinValue, MaxDC - rMaxDC);
                        MaxMC = (ushort)Math.Max(ushort.MinValue, MaxMC - rMaxMC);
                        MaxSC = (ushort)Math.Max(ushort.MinValue, MaxSC - rMaxSC);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, (Math.Max(sbyte.MinValue, ASpeed - rASpeed)));
                        break;
                    case BuffType.SwBuff:
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[2]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[3]);
                        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[4]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[5]);
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + buff.Values[6]);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[7]);
                        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + buff.Values[8]);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[9]);
                        MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + buff.Values[10]);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[11]);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + buff.Values[12]);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + buff.Values[13]);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + buff.Values[14]);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + buff.Values[15]);
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[16])));
                        //ExpRateOffset = (float)Math.Min(float.MaxValue, ExpRateOffset + buff.Values[0]);
                        //ItemDropRateOffset = (float)Math.Min(float.MaxValue, ItemDropRateOffset + buff.Values[1]);
                        break;
                    case BuffType.HumUp:
                        switch (Class)
                        {
                            case MirClass.Warrior:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + 220));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + 130));
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + 10));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + 10));
                                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxBagWeight + 80));
                                break;
                            case MirClass.Wizard:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + 140));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + 210));
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + 10));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + 10));
                                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxBagWeight + 80));
                                break;
                            case MirClass.Taoist:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + 170));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + 180));
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + 10));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + 10));
                                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxBagWeight + 80));
                                break;
                            case MirClass.Assassin:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + 195));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + 155));
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + 10));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + 10));
                                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxBagWeight + 80));
                                break;
                            case MirClass.Archer:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + 160));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + 200));
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + 10));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + 10));
                                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxBagWeight + 80));
                                break;
                            default:
                                break;
                        }
                        break;


                    case BuffType.HeroBuff:
                        switch (Class)
                        {
                            case MirClass.Warrior:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxHP + buff.Values[0]));
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, Math.Max(ushort.MinValue, MaxMP + buff.Values[1]));
                                break;
                            case MirClass.Wizard:
                                break;
                            case MirClass.Taoist:
                                HealthRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, HealthRecovery + buff.Values[4]));
                                SpellRecovery = (byte)Math.Min(byte.MaxValue, Math.Max(byte.MinValue, SpellRecovery + buff.Values[5]));
                                break;
                            case MirClass.Assassin:
                                Accuracy = (byte)Math.Min(ushort.MaxValue, Accuracy + buff.Values[6]);
                                Agility = (byte)Math.Min(ushort.MaxValue, Agility + buff.Values[7]);
                                break;
                            default:
                                break;
                        }
                        break;

                    case BuffType.MagicBooster:
                        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + buff.Values[0]);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[0]);
                        break;

                    case BuffType.Knapsack:
                    case BuffType.BagWeight:
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + buff.Values[0]);
                        break;
                    case BuffType.Transform:
                        TransformType = (short)buff.Values[0];
                        break;
                        
                    case BuffType.Impact:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        break;
                    case BuffType.Magic:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[0]);
                        break;
                    case BuffType.Taoist:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[0]);
                        break;
                    case BuffType.Storm:
                    case BuffType.StormQuest:
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[0])));
                        break;
                    case BuffType.Accuracy:
                    case BuffType.AccuracyQuest:
                        Accuracy = (byte)Math.Max(sbyte.MinValue, (Math.Min(byte.MaxValue, Accuracy + buff.Values[0])));
                        break;
                    case BuffType.Agility:
                        Agility = (byte)Math.Max(sbyte.MinValue, (Math.Min(byte.MaxValue, Agility + buff.Values[0])));
                        break;
                    case BuffType.HealthAid:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + buff.Values[0]);
                        break;
                    case BuffType.ManaAid:
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + buff.Values[0]);
                        break;
                    case BuffType.Defence:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        break;
                    case BuffType.MagicDefence:
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[0]);
                        break;
                    case BuffType.WonderDrug:
                        switch (buff.Values[0])
                        {
                            case 2:
                                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + buff.Values[1]);
                                break;
                            case 3:
                                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + buff.Values[1]);
                                break;
                            case 4:
                                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[1]);
                                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[1]);
                                break;
                            case 5:
                                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[1]);
                                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[1]);
                                break;
                            case 6:
                                ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[1])));
                                break;
                        }
                        break;
                    case BuffType.PinPoint:
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + buff.Values[0]);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[1]);
                        CriticalRate = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalRate + buff.Values[2])));
                        CriticalDamage = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalDamage + buff.Values[3])));
                        break;
                    case BuffType.Enrage:
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[0])));
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[1]);
                        CriticalRate = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalRate + buff.Values[2])));
                        CriticalDamage = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalDamage + buff.Values[3])));
                        break;
                    case BuffType.IronWall:
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[0]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[1]);
                        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[0]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[1]);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + buff.Values[2]);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + buff.Values[3]);
                        break;
                    case BuffType.Evasive:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[1]);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + buff.Values[2]);
                        break;
                    case BuffType.MobDebuff:
                        if (buff.Values[0] > 0)
                            MaxHP = (ushort)Math.Max(ushort.MinValue, MaxHP - buff.Values[0]);
                        if (buff.Values[1] > 0)
                            MaxMP = (ushort)Math.Max(ushort.MinValue, MaxMP - buff.Values[1]);
                        if (buff.Values[2] > 0 && (Class == MirClass.Warrior || Class == MirClass.Taoist))
                        {
                            MinDC = (ushort)Math.Max(ushort.MinValue, MinDC - buff.Values[2]);
                            MaxDC = (ushort)Math.Max(ushort.MinValue, MaxDC - buff.Values[2]);
                        }
                        if (buff.Values[3] > 0 && Class == MirClass.Wizard)
                        {
                            MinMC = (ushort)Math.Max(ushort.MinValue, MinMC - buff.Values[3]);
                            MaxMC = (ushort)Math.Max(ushort.MinValue, MaxMC - buff.Values[3]);
                        }
                        if (buff.Values[4] > 0 && Class == MirClass.Taoist)
                        {
                            MinSC = (ushort)Math.Max(ushort.MinValue, MinSC - buff.Values[4]);
                            MaxSC = (ushort)Math.Max(ushort.MinValue, MaxSC - buff.Values[4]);
                        }
                        if (buff.Values[5] > 0)
                        {
                            MinAC = (ushort)Math.Max(ushort.MinValue, MinAC - buff.Values[5]);
                            MaxAC = (ushort)Math.Max(ushort.MinValue, MaxAC - buff.Values[5]);
                        }
                        if (buff.Values[6] > 0)
                        {
                            MinMAC = (ushort)Math.Max(ushort.MinValue, MinMAC - buff.Values[6]);
                            MaxMAC = (ushort)Math.Max(ushort.MinValue, MaxMAC - buff.Values[6]);
                        }
                        if (buff.Values[7] > 0)
                            CriticalDamage = (byte)Math.Max(byte.MinValue, CriticalDamage - buff.Values[7]);


                        /*
                        if (buff.Values[8] > 0)
                            Reflect = (byte)Math.Min(byte.MinValue, Reflect - buff.Values[8]);
                        if (buff.Values[9] > 0)
                            HpDrain = Math.Min(0f, HpDrain - buff.Values[9]);
                            */
                        break;
                }

            }
        }

        public void RefreshMountStats()
        {
            UserItem MountItem = Equipment[(int)EquipmentSlot.Mount];

            if (!RidingMount || MountItem == null) return;

            UserItem[] Slots = MountItem.Slots;

            for (int i = 0; i < Slots.Length; i++)
            {
                UserItem temp = Slots[i];
                if (temp == null) continue;

                ItemInfo RealItem = Functions.GetRealItem(temp.Info, Level, Class, GameScene.ItemInfoList);

                CurrentWearWeight = (ushort)Math.Min(ushort.MaxValue, CurrentWearWeight + temp.Weight);

                if (temp.CurrentDura == 0 && temp.Info.Durability > 0) continue;

                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + RealItem.MinAC);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + RealItem.MaxAC + temp.AC);
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + RealItem.MinMAC);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + RealItem.MaxMAC + temp.MAC);

                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + RealItem.MinDC);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + RealItem.MaxDC + temp.DC);
                MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + RealItem.MinMC);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + RealItem.MaxMC + temp.MC);
                MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + RealItem.MinSC);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + RealItem.MaxSC + temp.SC);

                Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + RealItem.Accuracy + temp.Accuracy);
                Agility = (byte)Math.Min(byte.MaxValue, Agility + RealItem.Agility + temp.Agility);

                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + RealItem.HP + temp.HP);
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + RealItem.MP + temp.MP);

                ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + temp.AttackSpeed + RealItem.AttackSpeed)));
                Luck = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, Luck + temp.Luck + RealItem.Luck)));
            }
        }

        public void RefreshGuildBuffs()
        {
            if (User != this) return;
            if (GameScene.Scene.GuildDialog == null) return;
            for (int i = 0; i < GameScene.Scene.GuildDialog.EnabledBuffs.Count; i++)
            {
                GuildBuff Buff = GameScene.Scene.GuildDialog.EnabledBuffs[i];
                if (Buff == null) continue;
                if (!Buff.Active) continue;
                if (Buff.Info == null)
                Buff.Info = GameScene.Scene.GuildDialog.FindGuildBuffInfo(Buff.Id);
                if (Buff.Info == null) continue;
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + Buff.Info.BuffAc);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + Buff.Info.BuffMac);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + Buff.Info.BuffDc);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + Buff.Info.BuffMc);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + Buff.Info.BuffSc);
                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + Buff.Info.BuffMaxHp);
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + Buff.Info.BuffMaxMp);
                HealthRecovery = (byte)Math.Min(byte.MaxValue, HealthRecovery + Buff.Info.BuffHpRegen);
                SpellRecovery = (byte)Math.Min(byte.MaxValue, SpellRecovery + Buff.Info.BuffMPRegen);
            }
        }

        public void BindAllItems()
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null) continue;
                GameScene.Bind(Inventory[i]);
            }

            for (int i = 0; i < Equipment.Length; i++)
            {
                if (Equipment[i] == null) continue;
                GameScene.Bind(Equipment[i]);
            }

            for (int i = 0; i < QuestInventory.Length; i++)
            {
                if (QuestInventory[i] == null) continue;
                GameScene.Bind(QuestInventory[i]);
            }
        }


        public ClientMagic GetMagic(Spell spell)
        {
            for (int i = 0; i < Magics.Count; i++)
            {
                ClientMagic magic = Magics[i];
                if (magic.Spell != spell) continue;
                return magic;
            }

            return null;
        }


        public void GetMaxGain(UserItem item)
        {
            if (CurrentBagWeight + item.Weight <= MaxBagWeight && FreeSpace(Inventory) > 0) return;

            uint min = 0;
            uint max = item.Count;

            if (CurrentBagWeight >= MaxBagWeight)
            {

            }

            if (item.Info.Type == ItemType.Amulet)
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem bagItem = Inventory[i];

                    if (bagItem == null || bagItem.Info != item.Info) continue;

                    if (bagItem.Count + item.Count <= bagItem.Info.StackSize)
                    {
                        item.Count = max;
                        return;
                    }
                    item.Count = bagItem.Info.StackSize - bagItem.Count;
                    min += item.Count;
                    if (min >= max)
                    {
                        item.Count = max;
                        return;
                    }
                }

                if (min == 0)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat(FreeSpace(Inventory) == 0 ? "You do not have enough space." : "You do not have enough weight.", ChatType.System);

                    item.Count = 0;
                    return;
                }

                item.Count = min;
                return;
            }

            if (CurrentBagWeight + item.Weight > MaxBagWeight)
            {
                item.Count = (uint)(Math.Max((MaxBagWeight - CurrentBagWeight), uint.MinValue) / item.Info.Weight);
                max = item.Count;
                if (item.Count == 0)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat("You do not have enough weight.", ChatType.System);
                    return;
                }
            }

            if (item.Info.StackSize > 1)
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem bagItem = Inventory[i];

                    if (bagItem == null) return;
                    if (bagItem.Info != item.Info) continue;

                    if (bagItem.Count + item.Count <= bagItem.Info.StackSize)
                    {
                        item.Count = max;
                        return;
                    }

                    item.Count = bagItem.Info.StackSize - bagItem.Count;
                    min += item.Count;
                    if (min >= max)
                    {
                        item.Count = max;
                        return;
                    }
                }

                if (min == 0)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat("You do not have enough space.", ChatType.System);
                    item.Count = 0;
                }
            }
            else
            {
                GameScene.Scene.ChatDialog.ReceiveChat("You do not have enough space.", ChatType.System);
                item.Count = 0;
            }

        }
        private int FreeSpace(UserItem[] array)
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
                count++;
            return count;
        }

        public int GetFreeInventorySpace()
        {
            int count = 0;
            for (int i = 0; i < Inventory.Length; i++)
                if (Inventory[i] == null)
                    count++;
            return count;
        }

        public override void SetAction()
        {
            if (QueuedAction != null )
            {
                if ((ActionFeed.Count == 0) || (ActionFeed.Count == 1 && NextAction.Action == MirAction.Stance))
                {
                    ActionFeed.Clear();
                    ActionFeed.Add(QueuedAction);
                    QueuedAction = null;
                }
            }

            base.SetAction();
        }
        public override void ProcessFrames()
        {
            bool clear = CMain.Time >= NextMotion;

            base.ProcessFrames();

            if (clear) QueuedAction = null;
            if ((CurrentAction == MirAction.Standing || CurrentAction == MirAction.MountStanding || CurrentAction == MirAction.Stance || CurrentAction == MirAction.Stance2 || CurrentAction == MirAction.DashFail) && (QueuedAction != null || NextAction != null))
                SetAction();
        }

        public void ClearMagic()
        {
            NextMagic = null;
            NextMagicDirection = 0;
            NextMagicLocation = Point.Empty;
            NextMagicObject = null;
        } 
    }

    public class PlayerRecipe
    {
        public byte Recipe;
        public bool Learnt;
        public long CraftEndTime;
        public bool CraftEnd;
        public bool Collected;
        public bool InPrcoess;
        public PlayerRecipe() { }
    }
}

