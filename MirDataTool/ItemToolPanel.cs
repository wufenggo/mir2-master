using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MirDataTool
{
    public partial class ItemToolPanel : UserControl
    {
        MirDataTool MotherParent;

        public List<ItemInfo> ItemInfoList = new List<ItemInfo>();
        public List<ItemInfo> _SelectedItems = new List<ItemInfo>();
        #region Constructor
        public ItemToolPanel()
        {
            InitializeComponent();
            searchItemTypeBox.Items.AddRange(Enum.GetValues(typeof(ItemType)).Cast<object>().ToArray());
            searchItemTypeBox.Items.Add("All");
            searchItemSetBox.Items.AddRange(Enum.GetValues(typeof(ItemSet)).Cast<object>().ToArray());
            searchItemSetBox.Items.Add("All");
            searchClass.Items.AddRange(Enum.GetValues(typeof(MirClass)).Cast<object>().ToArray());
            searchClass.Items.Add("All");
            searchGender.Items.AddRange(Enum.GetValues(typeof(RequiredGender)).Cast<object>().ToArray());
            searchGender.Items.Add("All");
            searchItemGrade.Items.AddRange(Enum.GetValues(typeof(ItemGrade)).Cast<object>().ToArray());
            searchItemGrade.Items.Add("All");
            searchReqType.Items.AddRange(Enum.GetValues(typeof(RequiredType)).Cast<object>().ToArray());
            searchReqType.Items.Add("All");
            searchUseable.Items.AddRange(Enum.GetValues(typeof(WearType)).Cast<object>().ToArray());
            searchUseable.Items.Add("All");
            
            ITypeComboBox.Items.AddRange(Enum.GetValues(typeof(ItemType)).Cast<object>().ToArray());
            IGradeComboBox.Items.AddRange(Enum.GetValues(typeof(ItemGrade)).Cast<object>().ToArray());
            RTypeComboBox.Items.AddRange(Enum.GetValues(typeof(RequiredType)).Cast<object>().ToArray());
            RClassComboBox.Items.AddRange(Enum.GetValues(typeof(RequiredClass)).Cast<object>().ToArray());
            WearBox.Items.AddRange(Enum.GetValues(typeof(WearType)).Cast<object>().ToArray());
            LvlableBy.Items.AddRange(Enum.GetValues(typeof(WearType)).Cast<object>().ToArray());
            RGenderComboBox.Items.AddRange(Enum.GetValues(typeof(RequiredGender)).Cast<object>().ToArray());
            ISetComboBox.Items.AddRange(Enum.GetValues(typeof(ItemSet)).Cast<object>().ToArray());

            posElementsBox.Items.AddRange(Enum.GetValues(typeof(ElementPos)).Cast<object>().ToArray());
            negElementBox.Items.AddRange(Enum.GetValues(typeof(ElementNeg)).Cast<object>().ToArray());
            runeTypeBox.Items.AddRange(Enum.GetValues(typeof(SocketType)).Cast<object>().ToArray());

            for (int i = 1; i <= 10; i++)
            {
                LvlSysComboBox.Items.Add(i);
            }
            doSearch_CheckedChanged(this, null);
        }
        #endregion

        #region Functions
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
        }

        public void UpdateList()
        {
            itemListBox.Items.Clear();
            #region Search Function
            if (doSearch.Checked)
            {
                #region Search on but nothing to search
                if (searchItemShapeBox.Text.Length == 0 &&
                searchItemNameBox.Text.Length == 0 &&
                (searchItemSetBox.SelectedItem == null ||
                searchItemSetBox.SelectedIndex == searchItemSetBox.Items.Count - 1) &&
                (searchItemTypeBox.SelectedItem == null ||
                searchItemTypeBox.SelectedIndex == searchItemTypeBox.Items.Count - 1) &&
                (searchUseable.SelectedItem == null ||
                searchUseable.SelectedIndex == searchUseable.Items.Count - 1) &&
                (searchReqType.SelectedItem == null ||
                searchReqType.SelectedIndex == searchReqType.Items.Count - 1) &&
                (searchItemGrade.SelectedItem == null ||
                searchItemGrade.SelectedIndex == searchItemGrade.Items.Count - 1) &&
                (searchGender.SelectedItem == null || searchGender.SelectedIndex == searchGender.Items.Count - 1) &&
                (searchClass.SelectedItem == null || searchClass.SelectedIndex == searchClass.Items.Count - 1)
                )
                {
                    for (int i = 0; i < ItemInfoList.Count; i++)
                        itemListBox.Items.Add(ItemInfoList[i]);
                }
                #endregion
                #region Search Filter
                else
                {                    
                    List<ItemInfo> temp = new List<ItemInfo>();
                    for (int i = 0; i < ItemInfoList.Count; i++)
                    {
                        if (searchItemNameBox.Text.Length > 0)
                            if (!ItemInfoList[i].Name.ToLower().Contains(searchItemNameBox.Text.ToLower()))
                                continue;
                        if (searchItemSetBox.SelectedIndex != -1 &&
                            searchItemSetBox.SelectedIndex != searchItemSetBox.Items.Count - 1)
                            if (ItemInfoList[i].Set != (ItemSet)searchItemSetBox.SelectedItem)
                                continue;
                        if (searchItemTypeBox.SelectedIndex != -1 &&
                            searchItemTypeBox.SelectedIndex != searchItemTypeBox.Items.Count - 1)
                            if (ItemInfoList[i].Type != (ItemType)searchItemTypeBox.SelectedItem)
                                continue;
                        if (searchItemShapeBox.Text.Length > 0)
                            if (ItemInfoList[i].Shape != Convert.ToInt16(searchItemShapeBox.Text))
                                continue;
                        if (searchUseable.SelectedIndex != -1)
                            if (ItemInfoList[i].WearType != (WearType)searchUseable.SelectedItem)
                                continue;
                        if (searchReqType.SelectedIndex != -1)
                            if (ItemInfoList[i].RequiredType != (RequiredType)searchReqType.SelectedItem)
                                continue;
                        if (searchItemGrade.SelectedIndex != -1)
                            if (ItemInfoList[i].Grade != (ItemGrade)searchItemGrade.SelectedItem)
                                continue;
                        if (searchGender.SelectedIndex != -1)
                            if (ItemInfoList[i].RequiredGender != (RequiredGender)searchGender.SelectedItem)
                                continue;
                        itemListBox.Items.Add(ItemInfoList[i]);
                    }                    
                }
                #endregion
            }
            #endregion
            #region Non search
            else
            {

                for (int i = 0; i < ItemInfoList.Count; i++)
                    itemListBox.Items.Add(ItemInfoList[i]);
            }
            #endregion
            UpdateInterface();
        }
        #region Update the User Interface
        public void UpdateInterface()
        {
            _SelectedItems = itemListBox.SelectedItems.Cast<ItemInfo>().ToList();
            #region Clear all data as no item is selected
            if (_SelectedItems.Count == 0)
            {
                ItemInfoPanel.Enabled = false;

                ItemIndexTextBox.Text = string.Empty;
                ItemNameTextBox.Text = string.Empty;
                WeightTextBox.Text = string.Empty;
                ImageTextBox.Text = string.Empty;
                DuraTextBox.Text = string.Empty;
                ITypeComboBox.SelectedItem = null;
                LvlSysComboBox.SelectedItem = null;
                IGradeComboBox.SelectedItem = null;
                ISetComboBox.SelectedItem = null;
                ShapeTextBox.Text = string.Empty;
                SSizeTextBox.Text = string.Empty;
                PriceTextBox.Text = string.Empty;
                RTypeComboBox.SelectedItem = null;
                RAmountTextBox.Text = string.Empty;
                RClassComboBox.SelectedItem = null;
                WearBox.SelectedItem = null;
                RGenderComboBox.SelectedItem = null;
                LightTextBox.Text = string.Empty;
                LightIntensitytextBox.Text = string.Empty;

                MinACTextBox.Text = string.Empty;
                MaxACTextBox.Text = string.Empty;
                MinMACTextBox.Text = string.Empty;
                MaxMACTextBox.Text = string.Empty;
                MinDCTextBox.Text = string.Empty;
                MaxDCTextBox.Text = string.Empty;
                MinMCTextBox.Text = string.Empty;
                MaxMCTextBox.Text = string.Empty;
                MinSCTextBox.Text = string.Empty;
                MaxSCTextBox.Text = string.Empty;
                HPTextBox.Text = string.Empty;
                MPTextBox.Text = string.Empty;
                AccuracyTextBox.Text = string.Empty;
                AgilityTextBox.Text = string.Empty;
                ASpeedTextBox.Text = string.Empty;
                LuckTextBox.Text = string.Empty;
                StartItemCheckBox.Checked = false;

                WWeightTextBox.Text = string.Empty;
                HWeightTextBox.Text = string.Empty;
                BWeightText.Text = string.Empty;
                EffectTextBox.Text = string.Empty;

                PoisonRecoverytextBox.Text = string.Empty;
                SpellRecoverytextBox.Text = string.Empty;
                MagicResisttextBox.Text = string.Empty;
                HealthRecoveryTextbox.Text = string.Empty;
                StrongTextbox.Text = string.Empty;
                MacRateTextbox.Text = string.Empty;
                ACRateTextbox.Text = string.Empty;
                PoisonResisttextBox.Text = string.Empty;
                PoisonAttacktextbox.Text = string.Empty;
                Freezingtextbox.Text = string.Empty;
                Holytextbox.Text = string.Empty;
                HPratetextbox.Text = string.Empty;
                MPratetextbox.Text = string.Empty;
                HpDrainRatetextBox.Text = string.Empty;
                CriticalDamagetextBox.Text = string.Empty;
                CriticalRatetextBox.Text = string.Empty;
                ReflecttextBox.Text = string.Empty;

                LevelBasedcheckbox.Checked = false;
                ClassBasedcheckbox.Checked = false;
                HumUpBox.Checked = false;

                Bind_dontstorecheckbox.Checked = false;
                Bind_dontupgradecheckbox.Checked = false;
                Bind_dontrepaircheckbox.Checked = false;
                Bind_donttradecheckbox.Checked = false;
                Bind_dontsellcheckbox.Checked = false;
                Bind_destroyondropcheckbox.Checked = false;
                Bind_dontdeathdropcheckbox.Checked = false;
                Bind_dontdropcheckbox.Checked = false;
                Bind_DontSpecialRepaircheckBox.Checked = false;

                NeedIdentifycheckbox.Checked = false;
                ShowGroupPickupcheckbox.Checked = false;
                BindOnEquipcheckbox.Checked = false;
                ParalysischeckBox.Checked = false;
                TeleportcheckBox.Checked = false;
                ClearcheckBox.Checked = false;
                ProtectioncheckBox.Checked = false;
                RevivalcheckBox.Checked = false;
                MusclecheckBox.Checked = false;
                FlamecheckBox.Checked = false;
                HealingcheckBox.Checked = false;
                ProbecheckBox.Checked = false;
                SkillcheckBox.Checked = false;
                NoDuraLosscheckBox.Checked = false;
                RandomStatstextBox.Text = string.Empty;
                PickaxecheckBox.Checked = false;
                FastRunCheckBox.Checked = false;
                CanAwaken.Checked = false;
                TooltipTextBox.Text = string.Empty;

                posElementsBox.SelectedItem = null;
                negElementBox.SelectedItem = null;
                eleposAmountBox.Text = string.Empty;
                elenegAmountBox.Text = string.Empty;
                minSocketBox.Text = "0";
                maxSocketBox.Text = "0";
                return;
            }
            #endregion
            ItemInfo info = _SelectedItems[0];

            ItemInfoPanel.Enabled = true;

            #region Set Values by the first item selected
            LvlSysComboBox.SelectedItem = LvlSysComboBox.Items[0];
            ItemIndexTextBox.Text = info.Index.ToString();
            ItemNameTextBox.Text = info.Name;
            WeightTextBox.Text = info.Weight.ToString();
            ImageTextBox.Text = info.Image.ToString();
            DuraTextBox.Text = info.Durability.ToString();
            ITypeComboBox.SelectedItem = info.Type;
            IGradeComboBox.SelectedItem = info.Grade;
            ISetComboBox.SelectedItem = info.Set;
            ShapeTextBox.Text = info.Shape.ToString();
            SSizeTextBox.Text = info.StackSize.ToString();
            PriceTextBox.Text = info.Price.ToString();
            UpdateBox.Text = info.UpdateTo.ToString();
            RTypeComboBox.SelectedItem = info.RequiredType;
            RAmountTextBox.Text = info.RequiredAmount.ToString();
            RClassComboBox.SelectedItem = info.RequiredClass;
            RGenderComboBox.SelectedItem = info.RequiredGender;
            LightTextBox.Text = (info.Light % 15).ToString();
            LightIntensitytextBox.Text = (info.Light / 15).ToString();

            MinACTextBox.Text = info.MinAC.ToString();
            MaxACTextBox.Text = info.MaxAC.ToString();
            MinMACTextBox.Text = info.MinMAC.ToString();
            MaxMACTextBox.Text = info.MaxMAC.ToString();
            MinDCTextBox.Text = info.MinDC.ToString();
            MaxDCTextBox.Text = info.MaxDC.ToString();
            MinMCTextBox.Text = info.MinMC.ToString();
            MaxMCTextBox.Text = info.MaxMC.ToString();
            MinSCTextBox.Text = info.MinSC.ToString();
            MaxSCTextBox.Text = info.MaxSC.ToString();
            HPTextBox.Text = info.HP.ToString();
            MPTextBox.Text = info.MP.ToString();
            AccuracyTextBox.Text = info.Accuracy.ToString();
            AgilityTextBox.Text = info.Agility.ToString();
            ASpeedTextBox.Text = info.AttackSpeed.ToString();
            LuckTextBox.Text = info.Luck.ToString();

            WWeightTextBox.Text = info.WearWeight.ToString();
            HWeightTextBox.Text = info.HandWeight.ToString();
            BWeightText.Text = info.BagWeight.ToString();
            WearBox.SelectedItem = info.WearType;
            LvlableBy.SelectedItem = info.LvlableBy;

            StartItemCheckBox.Checked = info.StartItem;
            EffectTextBox.Text = info.Effect.ToString();
            WeaponEffectTextBox.Text = info.WeaponEffects.ToString();
            ItemGlowBox.Text = info.ItemGlow.ToString();

            PoisonRecoverytextBox.Text = info.PoisonRecovery.ToString();
            SpellRecoverytextBox.Text = info.SpellRecovery.ToString();
            MagicResisttextBox.Text = info.MagicResist.ToString();
            HealthRecoveryTextbox.Text = info.HealthRecovery.ToString();
            StrongTextbox.Text = info.Strong.ToString();
            MacRateTextbox.Text = info.MaxMacRate.ToString();
            ACRateTextbox.Text = info.MaxAcRate.ToString();
            PoisonResisttextBox.Text = info.PoisonResist.ToString();
            PoisonAttacktextbox.Text = info.PoisonAttack.ToString();
            Freezingtextbox.Text = info.Freezing.ToString();
            Holytextbox.Text = info.Holy.ToString();
            HPratetextbox.Text = info.HPrate.ToString();
            MPratetextbox.Text = info.MPrate.ToString();
            HpDrainRatetextBox.Text = info.HpDrainRate.ToString();
            CriticalRatetextBox.Text = info.CriticalRate.ToString();
            CriticalDamagetextBox.Text = info.CriticalDamage.ToString();
            ReflecttextBox.Text = info.Reflect.ToString();

            LevelBasedcheckbox.Checked = info.LevelBased;
            ClassBasedcheckbox.Checked = info.ClassBased;
            HumUpBox.Checked = info.HumUpBased;
            EnableLvlSysBox.Checked = info.AllowLvlSys;
            RandomStatsBox.Checked = info.AllowRandomStats;
            HumUpResBox.Checked = info.HumUpRestricted;

            ExpBox1.Text = info.LvlSysExp[0].ToString();
            ExpBox2.Text = info.LvlSysExp[1].ToString();
            ExpBox3.Text = info.LvlSysExp[2].ToString();
            ExpBox4.Text = info.LvlSysExp[3].ToString();
            ExpBox5.Text = info.LvlSysExp[4].ToString();
            ExpBox6.Text = info.LvlSysExp[5].ToString();
            ExpBox7.Text = info.LvlSysExp[6].ToString();
            ExpBox8.Text = info.LvlSysExp[7].ToString();
            ExpBox9.Text = info.LvlSysExp[8].ToString();
            ExpBox10.Text = info.LvlSysExp[9].ToString();

            lvlsysMinAC.Text = info.LvlSysMinAC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxAC.Text = info.LvlSysMaxAC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinMAC.Text = info.LvlSysMinMAC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxMAC.Text = info.LvlSysMaxMAC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinSC.Text = info.LvlSysMinSC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxSC.Text = info.LvlSysMaxSC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinMC.Text = info.LvlSysMinMC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxMC.Text = info.LvlSysMaxMC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinDC.Text = info.LvlSysMinDC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxDC.Text = info.LvlSysMaxDC[LvlSysComboBox.SelectedIndex].ToString();


            Bind_dontstorecheckbox.Checked = info.Bind.HasFlag(BindMode.DontStore);
            Bind_dontupgradecheckbox.Checked = info.Bind.HasFlag(BindMode.DontUpgrade);
            Bind_dontrepaircheckbox.Checked = info.Bind.HasFlag(BindMode.DontRepair);
            Bind_donttradecheckbox.Checked = info.Bind.HasFlag(BindMode.DontTrade);
            Bind_dontsellcheckbox.Checked = info.Bind.HasFlag(BindMode.DontSell);
            Bind_destroyondropcheckbox.Checked = info.Bind.HasFlag(BindMode.DestroyOnDrop);
            Bind_dontdeathdropcheckbox.Checked = info.Bind.HasFlag(BindMode.DontDeathdrop);
            Bind_dontdropcheckbox.Checked = info.Bind.HasFlag(BindMode.DontDrop);
            Bind_DontSpecialRepaircheckBox.Checked = info.Bind.HasFlag(BindMode.NoSRepair);
            BindOnEquipcheckbox.Checked = info.Bind.HasFlag(BindMode.BindOnEquip);
            BreakOnDeathcheckbox.Checked = info.Bind.HasFlag(BindMode.BreakOnDeath);
            NoWeddingRingcheckbox.Checked = info.Bind.HasFlag(BindMode.NoWeddingRing);


            NeedIdentifycheckbox.Checked = info.NeedIdentify;
            ShowGroupPickupcheckbox.Checked = info.ShowGroupPickup;


            ParalysischeckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Paralize);
            TeleportcheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Teleport);
            ClearcheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Clearring);
            ProtectioncheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Protection);
            RevivalcheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Revival);
            MusclecheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Muscle);
            FlamecheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Flame);
            HealingcheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Healing);
            ProbecheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Probe);
            SkillcheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.Skill);
            NoDuraLosscheckBox.Checked = info.Unique.HasFlag(SpecialItemMode.NoDuraLoss);
            RandomStatstextBox.Text = info.RandomStatsId.ToString();
            PickaxecheckBox.Checked = info.CanMine;
            FastRunCheckBox.Checked = info.CanFastRun;
            CanAwaken.Checked = info.CanAwakening;
            TooltipTextBox.Text = info.ToolTip;

            eleposAmountBox.Text = info.PositiveElementAmount.ToString();
            elenegAmountBox.Text = info.NegativeElementAmount.ToString();
            posElementsBox.SelectedItem = info.PositiveElement;
            negElementBox.SelectedItem = info.NegativeElement;
            minSocketBox.Text = info.MinSocket.ToString();
            maxSocketBox.Text = info.MaxSocket.ToString();
            effectBox40.Text = info.LevelWeapEffect[0].ToString();
            effectBox41.Text = info.LevelWeapEffect[1].ToString();
            effectBox42.Text = info.LevelWeapEffect[2].ToString();
            effectBox43.Text = info.LevelWeapEffect[3].ToString();
            effectBox44.Text = info.LevelWeapEffect[4].ToString();
            effectBox45.Text = info.LevelWeapEffect[5].ToString();
            effectBox46.Text = info.LevelWeapEffect[6].ToString();
            effectBox47.Text = info.LevelWeapEffect[7].ToString();
            effectBox48.Text = info.LevelWeapEffect[8].ToString();
            effectBox49.Text = info.LevelWeapEffect[9].ToString();

            textBox50.Text = info.LevelArmourEffect[0].ToString();
            textBox51.Text = info.LevelArmourEffect[1].ToString();
            textBox52.Text = info.LevelArmourEffect[2].ToString();
            textBox53.Text = info.LevelArmourEffect[3].ToString();
            textBox54.Text = info.LevelArmourEffect[4].ToString();
            textBox55.Text = info.LevelArmourEffect[5].ToString();
            textBox56.Text = info.LevelArmourEffect[6].ToString();
            textBox57.Text = info.LevelArmourEffect[7].ToString();
            textBox58.Text = info.LevelArmourEffect[8].ToString();
            textBox59.Text = info.LevelArmourEffect[9].ToString();

            textBox40.Text = info.LevelItemGlow[0].ToString();
            textBox41.Text = info.LevelItemGlow[1].ToString();
            textBox42.Text = info.LevelItemGlow[2].ToString();
            textBox43.Text = info.LevelItemGlow[3].ToString();
            textBox44.Text = info.LevelItemGlow[4].ToString();
            textBox45.Text = info.LevelItemGlow[5].ToString();
            textBox46.Text = info.LevelItemGlow[6].ToString();
            textBox47.Text = info.LevelItemGlow[7].ToString();
            textBox48.Text = info.LevelItemGlow[8].ToString();
            textBox49.Text = info.LevelItemGlow[9].ToString();

            imageBox1.Text = info.LevelWeapLooks[0].ToString();
            imageBox2.Text = info.LevelWeapLooks[1].ToString();
            imageBox3.Text = info.LevelWeapLooks[2].ToString();
            imageBox4.Text = info.LevelWeapLooks[3].ToString();
            imageBox5.Text = info.LevelWeapLooks[4].ToString();
            imageBox6.Text = info.LevelWeapLooks[5].ToString();
            imageBox7.Text = info.LevelWeapLooks[6].ToString();
            imageBox8.Text = info.LevelWeapLooks[7].ToString();
            imageBox9.Text = info.LevelWeapLooks[8].ToString();
            imageBox10.Text = info.LevelWeapLooks[9].ToString();
            armimageBox1.Text = info.LevelArmourLooks[0].ToString();
            armimageBox2.Text = info.LevelArmourLooks[1].ToString();
            armimageBox3.Text = info.LevelArmourLooks[2].ToString();
            armimageBox4.Text = info.LevelArmourLooks[3].ToString();
            armimageBox5.Text = info.LevelArmourLooks[4].ToString();
            armimageBox6.Text = info.LevelArmourLooks[5].ToString();
            armimageBox7.Text = info.LevelArmourLooks[6].ToString();
            armimageBox8.Text = info.LevelArmourLooks[7].ToString();
            armimageBox9.Text = info.LevelArmourLooks[8].ToString();
            armimageBox10.Text = info.LevelArmourLooks[9].ToString();
            itemimageBox1.Text = info.LevelItemLooks[0].ToString();
            itemimageBox2.Text = info.LevelItemLooks[1].ToString();
            itemimageBox3.Text = info.LevelItemLooks[2].ToString();
            itemimageBox4.Text = info.LevelItemLooks[3].ToString();
            itemimageBox5.Text = info.LevelItemLooks[4].ToString();
            itemimageBox6.Text = info.LevelItemLooks[5].ToString();
            itemimageBox7.Text = info.LevelItemLooks[6].ToString();
            itemimageBox8.Text = info.LevelItemLooks[7].ToString();
            itemimageBox9.Text = info.LevelItemLooks[8].ToString();
            itemimageBox10.Text = info.LevelItemLooks[9].ToString();
            #endregion
            #region RuneStone values
            if (info.Type == ItemType.RuneStone)
            {
                runeTypeBox.SelectedItem = (SocketType)info.Shape;
                switch ((SocketType)info.Shape)
                {
                    #region Preset Stats
                    case SocketType.DamageReductionPvE:
                        raBox.Text = info.MinAC.ToString();
                        break;
                    case SocketType.DamageReductionPvP:
                        raBox.Text = info.MinMAC.ToString();
                        break;
                    case SocketType.DamageIncreasePvE:
                        raBox.Text = info.MaxAC.ToString();
                        break;
                    case SocketType.DamageIncreasePvP:
                        raBox.Text = info.MaxMAC.ToString();
                        break;
                    case SocketType.MeleeDamageBonus:
                        raBox.Text = info.MaxAcRate.ToString();
                        break;
                    case SocketType.MagicDamageBonus:
                        raBox.Text = info.MaxMacRate.ToString();
                        break;
                    case SocketType.SpiritualBonus:
                        raBox.Text = info.Holy.ToString();
                        break;
                    case SocketType.HealthBonus:
                        break;
                    case SocketType.ManaBonus:
                        break;
                    case SocketType.HealthRegenBonus:
                        break;
                    case SocketType.ManaRegenBonus:
                        break;
                    case SocketType.DestructionBonus:
                        break;
                    case SocketType.MagicBonus:
                        break;
                    case SocketType.SpiritBonus:
                        break;
                    #region Passive Buffs
                    case SocketType.PinPoint:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        rStat0Box.Text = info.Accuracy.ToString();
                        rStat1Box.Text = info.MaxDC.ToString();
                        rStat2Box.Text = info.CriticalRate.ToString();
                        rStat3Box.Text = info.CriticalDamage.ToString();
                        break;
                    case SocketType.Evasive:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        rStat0Box.Text = info.MaxAC.ToString();
                        rStat1Box.Text = info.MaxMAC.ToString();
                        rStat2Box.Text = info.Agility.ToString();
                        break;
                    case SocketType.Enrage:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        rStat0Box.Text = info.AttackSpeed.ToString();
                        rStat1Box.Text = info.MaxDC.ToString();
                        rStat2Box.Text = info.CriticalRate.ToString();
                        rStat3Box.Text = info.CriticalDamage.ToString();
                        break;
                    case SocketType.IronWall:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        rStat0Box.Text = info.MinAC.ToString();
                        rStat1Box.Text = info.MaxAC.ToString();
                        rStat2Box.Text = info.HP.ToString();
                        rStat3Box.Text = info.Agility.ToString();
                        break;
                    case SocketType.SpeedyMagician:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();

                        rStat0Box.Text = info.MinMC.ToString();
                        break;
                    #endregion
                    #endregion
                    case SocketType.NONE:
                        break;

                }

                rnMinACBox.Text = info.MinAC.ToString();
                rnMinMACBox.Text = info.MinMAC.ToString();

                rnMaxACBox.Text = info.MaxAC.ToString();
                rnMaxMACBox.Text = info.MaxMAC.ToString();

                rnMinDCBox.Text = info.MinDC.ToString();
                rnMinMCBox.Text = info.MinMC.ToString();
                rnMinSCBox.Text = info.MinSC.ToString();

                rnMaxDCBox.Text = info.MaxDC.ToString();
                rnMaxMCBox.Text = info.MaxMC.ToString();
                rnMaxSCBox.Text = info.MaxSC.ToString();

                rnHPBox.Text = info.HP.ToString();
                rnMPBox.Text = info.MP.ToString();

                rnHPRegenBox.Text = info.HPrate.ToString();
                rnMPRegenBox.Text = info.MPrate.ToString();

                rnAccuracyBox.Text = info.Accuracy.ToString();
                rnAgilBox.Text = info.Agility.ToString();
                minSocketBox.Text = info.MinSocket.ToString();
                maxSocketBox.Text = info.MaxSocket.ToString();
            }
            #endregion
            #region Fill values in by the Items selected if the values are the same if not, don't fill them
            for (int i = 1; i < _SelectedItems.Count; i++)
            {
                info = _SelectedItems[i];

                if (ItemIndexTextBox.Text != info.Index.ToString()) ItemIndexTextBox.Text = string.Empty;
                if (ItemNameTextBox.Text != info.Name) ItemNameTextBox.Text = string.Empty;

                if (WeightTextBox.Text != info.Weight.ToString()) WeightTextBox.Text = string.Empty;
                if (ImageTextBox.Text != info.Image.ToString()) ImageTextBox.Text = string.Empty;
                if (DuraTextBox.Text != info.Durability.ToString()) DuraTextBox.Text = string.Empty;
                if (ITypeComboBox.SelectedItem == null || (ItemType)ITypeComboBox.SelectedItem != info.Type) ITypeComboBox.SelectedItem = null;
                if (IGradeComboBox.SelectedItem == null || (ItemGrade)IGradeComboBox.SelectedItem != info.Grade) IGradeComboBox.SelectedItem = null;
                if (ISetComboBox.SelectedItem == null || (ItemSet)ISetComboBox.SelectedItem != info.Set) ISetComboBox.SelectedItem = null;
                if (ShapeTextBox.Text != info.Shape.ToString()) ShapeTextBox.Text = string.Empty;
                if (SSizeTextBox.Text != info.StackSize.ToString()) SSizeTextBox.Text = string.Empty;
                if (PriceTextBox.Text != info.Price.ToString()) PriceTextBox.Text = string.Empty;
                if (UpdateBox.Text != info.UpdateTo.ToString()) UpdateBox.Text = string.Empty;
                if (RTypeComboBox.SelectedItem == null || (RequiredType)RTypeComboBox.SelectedItem != info.RequiredType) RTypeComboBox.SelectedItem = null;
                if (RAmountTextBox.Text != info.RequiredAmount.ToString()) RAmountTextBox.Text = string.Empty;
                if (RClassComboBox.SelectedItem == null || (RequiredClass)RClassComboBox.SelectedItem != info.RequiredClass) RClassComboBox.SelectedItem = null;
                if (WearBox.SelectedItem == null || (WearType)WearBox.SelectedItem != info.WearType) WearBox.SelectedItem = null;
                if (LvlableBy.SelectedItem == null || (WearType)LvlableBy.SelectedItem != info.WearType) LvlableBy.SelectedItem = null;
                if (RGenderComboBox.SelectedItem == null || (RequiredGender)RGenderComboBox.SelectedItem != info.RequiredGender) RGenderComboBox.SelectedItem = null;
                if (LightTextBox.Text != (info.Light % 15).ToString()) LightTextBox.Text = string.Empty;
                if (LightIntensitytextBox.Text != (info.Light / 15).ToString()) LightIntensitytextBox.Text = string.Empty;

                if (ExpBox1.Text != info.LvlSysExp[0].ToString()) ExpBox1.Text = string.Empty;
                if (ExpBox2.Text != info.LvlSysExp[1].ToString()) ExpBox2.Text = string.Empty;
                if (ExpBox3.Text != info.LvlSysExp[2].ToString()) ExpBox3.Text = string.Empty;
                if (ExpBox4.Text != info.LvlSysExp[3].ToString()) ExpBox4.Text = string.Empty;
                if (ExpBox5.Text != info.LvlSysExp[4].ToString()) ExpBox5.Text = string.Empty;
                if (ExpBox6.Text != info.LvlSysExp[5].ToString()) ExpBox6.Text = string.Empty;
                if (ExpBox7.Text != info.LvlSysExp[6].ToString()) ExpBox7.Text = string.Empty;
                if (ExpBox8.Text != info.LvlSysExp[7].ToString()) ExpBox8.Text = string.Empty;
                if (ExpBox9.Text != info.LvlSysExp[8].ToString()) ExpBox9.Text = string.Empty;
                if (ExpBox10.Text != info.LvlSysExp[9].ToString()) ExpBox10.Text = string.Empty;

                if (MinACTextBox.Text != info.MinAC.ToString()) MinACTextBox.Text = string.Empty;
                if (MaxACTextBox.Text != info.MaxAC.ToString()) MaxACTextBox.Text = string.Empty;
                if (MinMACTextBox.Text != info.MinMAC.ToString()) MinMACTextBox.Text = string.Empty;
                if (MaxMACTextBox.Text != info.MaxMAC.ToString()) MaxMACTextBox.Text = string.Empty;
                if (MinDCTextBox.Text != info.MinDC.ToString()) MinDCTextBox.Text = string.Empty;
                if (MaxDCTextBox.Text != info.MaxDC.ToString()) MaxDCTextBox.Text = string.Empty;
                if (MinMCTextBox.Text != info.MinMC.ToString()) MinMCTextBox.Text = string.Empty;
                if (MaxMCTextBox.Text != info.MaxMC.ToString()) MaxMCTextBox.Text = string.Empty;
                if (MinSCTextBox.Text != info.MinSC.ToString()) MinSCTextBox.Text = string.Empty;
                if (MaxSCTextBox.Text != info.MaxSC.ToString()) MaxSCTextBox.Text = string.Empty;
                if (HPTextBox.Text != info.HP.ToString()) HPTextBox.Text = string.Empty;
                if (MPTextBox.Text != info.MP.ToString()) MPTextBox.Text = string.Empty;
                if (AccuracyTextBox.Text != info.Accuracy.ToString()) AccuracyTextBox.Text = string.Empty;
                if (AgilityTextBox.Text != info.Agility.ToString()) AgilityTextBox.Text = string.Empty;
                if (ASpeedTextBox.Text != info.AttackSpeed.ToString()) ASpeedTextBox.Text = string.Empty;
                if (LuckTextBox.Text != info.Luck.ToString()) LuckTextBox.Text = string.Empty;

                if (WWeightTextBox.Text != info.WearWeight.ToString()) WWeightTextBox.Text = string.Empty;
                if (HWeightTextBox.Text != info.HandWeight.ToString()) HWeightTextBox.Text = string.Empty;
                if (BWeightText.Text != info.BagWeight.ToString()) BWeightText.Text = string.Empty;

                if (StartItemCheckBox.Checked != info.StartItem) StartItemCheckBox.CheckState = CheckState.Indeterminate;
                if (EffectTextBox.Text != info.Effect.ToString()) EffectTextBox.Text = string.Empty;
                if (WeaponEffectTextBox.Text != info.WeaponEffects.ToString()) WeaponEffectTextBox.Text = string.Empty;
                if (ItemGlowBox.Text != info.ItemGlow.ToString()) ItemGlowBox.Text = string.Empty;

                if (PoisonRecoverytextBox.Text != info.PoisonRecovery.ToString()) PoisonRecoverytextBox.Text = string.Empty;
                if (SpellRecoverytextBox.Text != info.SpellRecovery.ToString()) SpellRecoverytextBox.Text = string.Empty;
                if (MagicResisttextBox.Text != info.MagicResist.ToString()) MagicResisttextBox.Text = string.Empty;
                if (HealthRecoveryTextbox.Text != info.HealthRecovery.ToString()) HealthRecoveryTextbox.Text = string.Empty;
                if (StrongTextbox.Text != info.Strong.ToString()) StrongTextbox.Text = string.Empty;
                if (MacRateTextbox.Text != info.MaxMacRate.ToString()) MacRateTextbox.Text = string.Empty;
                if (ACRateTextbox.Text != info.MaxAcRate.ToString()) ACRateTextbox.Text = string.Empty;
                if (PoisonResisttextBox.Text != info.PoisonResist.ToString()) PoisonResisttextBox.Text = string.Empty;
                if (PoisonAttacktextbox.Text != info.PoisonAttack.ToString()) PoisonAttacktextbox.Text = string.Empty;
                if (Freezingtextbox.Text != info.Freezing.ToString()) Freezingtextbox.Text = string.Empty;
                if (Holytextbox.Text != info.Holy.ToString()) Holytextbox.Text = string.Empty;
                if (HPratetextbox.Text != info.HPrate.ToString()) HPratetextbox.Text = string.Empty;
                if (MPratetextbox.Text != info.MPrate.ToString()) MPratetextbox.Text = string.Empty;
                if (HpDrainRatetextBox.Text != info.HpDrainRate.ToString()) HpDrainRatetextBox.Text = string.Empty;
                if (CriticalRatetextBox.Text != info.CriticalRate.ToString()) CriticalRatetextBox.Text = string.Empty;
                if (CriticalDamagetextBox.Text != info.CriticalDamage.ToString()) CriticalDamagetextBox.Text = string.Empty;
                if (ReflecttextBox.Text != info.Reflect.ToString()) ReflecttextBox.Text = string.Empty;
                if (LevelBasedcheckbox.Checked != info.LevelBased) LevelBasedcheckbox.CheckState = CheckState.Indeterminate;
                if (ClassBasedcheckbox.Checked != info.ClassBased) ClassBasedcheckbox.CheckState = CheckState.Indeterminate;
                if (HumUpBox.Checked != info.ClassBased) HumUpBox.CheckState = CheckState.Indeterminate;
                if (Bind_dontstorecheckbox.Checked != info.Bind.HasFlag(BindMode.DontStore)) Bind_dontstorecheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_dontupgradecheckbox.Checked != info.Bind.HasFlag(BindMode.DontUpgrade)) Bind_dontupgradecheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_dontrepaircheckbox.Checked != info.Bind.HasFlag(BindMode.DontRepair)) Bind_dontrepaircheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_donttradecheckbox.Checked != info.Bind.HasFlag(BindMode.DontTrade)) Bind_donttradecheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_dontsellcheckbox.Checked != info.Bind.HasFlag(BindMode.DontSell)) Bind_dontsellcheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_destroyondropcheckbox.Checked != info.Bind.HasFlag(BindMode.DestroyOnDrop)) Bind_destroyondropcheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_dontdeathdropcheckbox.Checked != info.Bind.HasFlag(BindMode.DontDeathdrop)) Bind_dontdeathdropcheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_dontdropcheckbox.Checked != info.Bind.HasFlag(BindMode.DontDrop)) Bind_dontdropcheckbox.CheckState = CheckState.Indeterminate;
                if (Bind_DontSpecialRepaircheckBox.Checked != info.Bind.HasFlag(BindMode.NoSRepair)) Bind_DontSpecialRepaircheckBox.CheckState = CheckState.Indeterminate;
                if (BindOnEquipcheckbox.Checked != info.Bind.HasFlag(BindMode.BindOnEquip)) BindOnEquipcheckbox.CheckState = CheckState.Indeterminate;
                if (BreakOnDeathcheckbox.Checked != info.Bind.HasFlag(BindMode.BreakOnDeath)) BreakOnDeathcheckbox.CheckState = CheckState.Indeterminate;
                if (NoWeddingRingcheckbox.Checked != info.Bind.HasFlag(BindMode.NoWeddingRing)) NoWeddingRingcheckbox.CheckState = CheckState.Indeterminate;

                if (NeedIdentifycheckbox.Checked != info.NeedIdentify) NeedIdentifycheckbox.CheckState = CheckState.Indeterminate;
                if (ShowGroupPickupcheckbox.Checked != info.ShowGroupPickup) ShowGroupPickupcheckbox.CheckState = CheckState.Indeterminate;

                if (ParalysischeckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Paralize)) ParalysischeckBox.CheckState = CheckState.Indeterminate;
                if (TeleportcheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Teleport)) TeleportcheckBox.CheckState = CheckState.Indeterminate;
                if (ClearcheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Clearring)) ClearcheckBox.CheckState = CheckState.Indeterminate;
                if (ProtectioncheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Protection)) ProtectioncheckBox.CheckState = CheckState.Indeterminate;
                if (RevivalcheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Revival)) RevivalcheckBox.CheckState = CheckState.Indeterminate;
                if (MusclecheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Muscle)) MusclecheckBox.CheckState = CheckState.Indeterminate;
                if (FlamecheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Flame)) FlamecheckBox.CheckState = CheckState.Indeterminate;
                if (HealingcheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Healing)) HealingcheckBox.CheckState = CheckState.Indeterminate;
                if (ProbecheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Probe)) ProbecheckBox.CheckState = CheckState.Indeterminate;
                if (SkillcheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.Skill)) SkillcheckBox.CheckState = CheckState.Indeterminate;
                if (NoDuraLosscheckBox.Checked != info.Unique.HasFlag(SpecialItemMode.NoDuraLoss)) NoDuraLosscheckBox.CheckState = CheckState.Indeterminate;
                if (RandomStatstextBox.Text != info.RandomStatsId.ToString()) RandomStatstextBox.Text = string.Empty;
                if (PickaxecheckBox.Checked != info.CanMine) PickaxecheckBox.CheckState = CheckState.Indeterminate;
                if (FastRunCheckBox.Checked != info.CanFastRun) FastRunCheckBox.CheckState = CheckState.Indeterminate;
                if (CanAwaken.Checked != info.CanAwakening) CanAwaken.CheckState = CheckState.Indeterminate;
                if (TooltipTextBox.Text != info.ToolTip) TooltipTextBox.Text = string.Empty;
                if (minSocketBox.Text != info.MinSocket.ToString())
                    minSocketBox.Text = "0";
                if (maxSocketBox.Text != info.MaxSocket.ToString())
                    maxSocketBox.Text = "0";
                if (effectBox40.Text != info.LevelWeapEffect[0].ToString())
                    effectBox40.Text = string.Empty;
                if (effectBox41.Text != info.LevelWeapEffect[1].ToString())
                    effectBox41.Text = string.Empty;
                if (effectBox42.Text != info.LevelWeapEffect[2].ToString())
                    effectBox42.Text = string.Empty;
                if (effectBox43.Text != info.LevelWeapEffect[3].ToString())
                    effectBox43.Text = string.Empty;
                if (effectBox44.Text != info.LevelWeapEffect[4].ToString())
                    effectBox44.Text = string.Empty;
                if (effectBox45.Text != info.LevelWeapEffect[5].ToString())
                    effectBox45.Text = string.Empty;
                if (effectBox46.Text != info.LevelWeapEffect[6].ToString())
                    effectBox46.Text = string.Empty;
                if (effectBox47.Text != info.LevelWeapEffect[7].ToString())
                    effectBox47.Text = string.Empty;
                if (effectBox48.Text != info.LevelWeapEffect[8].ToString())
                    effectBox48.Text = string.Empty;
                if (effectBox49.Text != info.LevelWeapEffect[9].ToString())
                    effectBox49.Text = string.Empty;
                if (textBox50.Text != info.LevelArmourEffect[0].ToString())
                    textBox50.Text = string.Empty;
                if (textBox51.Text != info.LevelArmourEffect[1].ToString())
                    textBox51.Text = string.Empty;
                if (textBox52.Text != info.LevelArmourEffect[2].ToString())
                    textBox52.Text = string.Empty;
                if (textBox53.Text != info.LevelArmourEffect[3].ToString())
                    textBox53.Text = string.Empty;
                if (textBox54.Text != info.LevelArmourEffect[4].ToString())
                    textBox54.Text = string.Empty;
                if (textBox55.Text != info.LevelArmourEffect[5].ToString())
                    textBox55.Text = string.Empty;
                if (textBox56.Text != info.LevelArmourEffect[6].ToString())
                    textBox56.Text = string.Empty;
                if (textBox57.Text != info.LevelArmourEffect[7].ToString())
                    textBox57.Text = string.Empty;
                if (textBox58.Text != info.LevelArmourEffect[8].ToString())
                    textBox58.Text = string.Empty;
                if (textBox59.Text != info.LevelArmourEffect[9].ToString())
                    textBox59.Text = string.Empty;

                if (textBox40.Text != info.LevelItemGlow[0].ToString())
                    textBox40.Text = string.Empty;
                if (textBox41.Text != info.LevelItemGlow[1].ToString())
                    textBox41.Text = string.Empty;
                if (textBox42.Text != info.LevelItemGlow[2].ToString())
                    textBox42.Text = string.Empty;
                if (textBox43.Text != info.LevelItemGlow[3].ToString())
                    textBox43.Text = string.Empty;
                if (textBox44.Text != info.LevelItemGlow[4].ToString())
                    textBox44.Text = string.Empty;
                if (textBox45.Text != info.LevelItemGlow[5].ToString())
                    textBox45.Text = string.Empty;
                if (textBox46.Text != info.LevelItemGlow[6].ToString())
                    textBox46.Text = string.Empty;
                if (textBox47.Text != info.LevelItemGlow[7].ToString())
                    textBox47.Text = string.Empty;
                if (textBox48.Text != info.LevelItemGlow[8].ToString())
                    textBox48.Text = string.Empty;
                if (textBox49.Text != info.LevelItemGlow[9].ToString())
                    textBox49.Text = string.Empty;
            }
            #endregion
        }
        #endregion

        private void UpdateLvlSys()
        {
            if (_SelectedItems.Count == 0) return;

            ItemInfo info = _SelectedItems[0];

            lvlsysMinAC.Text = info.LvlSysMinAC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxAC.Text = info.LvlSysMaxAC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinMAC.Text = info.LvlSysMinMAC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxMAC.Text = info.LvlSysMaxMAC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinSC.Text = info.LvlSysMinSC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxSC.Text = info.LvlSysMaxSC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinMC.Text = info.LvlSysMinMC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxMC.Text = info.LvlSysMaxMC[LvlSysComboBox.SelectedIndex].ToString();

            lvlsysMinDC.Text = info.LvlSysMinDC[LvlSysComboBox.SelectedIndex].ToString();
            lvlsysMaxDC.Text = info.LvlSysMaxDC[LvlSysComboBox.SelectedIndex].ToString();
        }
        #region Import Export Procedures
        public void Import()
        {
            string Path = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Binary File|*.dat",
                InitialDirectory = Settings.ExportPath
            };
            ofd.ShowDialog();

            if (ofd.FileName == string.Empty) return;

            Path = ofd.FileName;

            List<ItemInfo> list = new List<ItemInfo>();
            using (FileStream stream = File.OpenRead(Path))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int _count = reader.ReadInt32();
                    for (int i = 0; i < _count; i++)
                        list.Add(new ItemInfo(reader));
                }
            }
            int updated = 0;
            int added = 0;
            for (int i = 0; i < list.Count; i++)
            {
                //  Check if the item exists
                ItemInfo tmp = ItemInfoList.Where(o => o.Name == list[i].Name).FirstOrDefault();
                //  It exists
                if (tmp != null)
                {
                    int origIndex = tmp.Index;
                    //  Check if the item name, type and shape are the same
                    if (tmp.Name == list[i].Name)
                    {
                        for (int x = 0; x < ItemInfoList.Count; x++)
                        {
                            if (ItemInfoList[x] == tmp)
                            {
                                ItemInfoList[x] = list[i];
                                //  Keep the original index!
                                ItemInfoList[x].Index = origIndex;
                                updated++;
                            }
                        }
                    }
                }
                //  The Item wasn't found so we'll add it
                else
                {
                    list[i].Index = ++MotherParent.ItemIndex;
                    ItemInfoList.Add(list[i]);
                    added++;
                }
            }
            MessageBox.Show(string.Format("{0} Items Updated\r\n{1} new Items Added", updated, added));
        }

        public void Export(List<ItemInfo> list)
        {
            if (list != null &&
                list.Count > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog
                {
                    InitialDirectory = Settings.ExportPath,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = "dat",
                    Filter = "Binary File|*.dat",
                    FileName = string.Format("ItemInfo-{0:dd-MM-yyyy hh-mm-ss-tt}.dat", DateTime.Now),
                    RestoreDirectory = true
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = File.Create(fileDialog.FileName))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            writer.Write(list.Count);
                            for (int i = 0; i < list.Count; i++)
                                list[i].Save(writer);
                        }
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Controls

        private void itemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void searchItemNameBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (searchItemNameBox.Text.Length <= 0)
                return;

            UpdateList();
        }

        private void searchItemButton_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void searchItemTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoSearch)
                UpdateList();
        }

        private void searchItemSetBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoSearch)
                UpdateList();
        }

        private void searchItemShapeBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!short.TryParse(searchItemSetBox.Text, out short tmp))
            {
                searchItemSetBox.BackColor = Color.Red;
                return;
            }
            searchItemSetBox.BackColor = SystemColors.Window;
            UpdateList();
        }

        public bool DoSearch = false;

        private void doSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (doSearch.Checked)
            {
                searchItemSetBox.SelectedIndex = searchItemSetBox.Items.Count - 1;
                searchItemSetBox.Enabled = true;
                searchItemTypeBox.SelectedIndex = searchItemTypeBox.Items.Count - 1;
                searchItemTypeBox.Enabled = true;
                searchItemNameBox.Text = string.Empty;
                searchItemNameBox.Enabled = true;
                searchItemShapeBox.Text = string.Empty;
                searchItemShapeBox.Enabled = true;
                searchItemButton.Enabled = true;
                DoSearch = true;
                doSearch.Text = "Search On";
            }
            else
            {
                DoSearch = false;
                searchItemTypeBox.SelectedItem = null;
                searchItemSetBox.Enabled = false;
                searchItemSetBox.SelectedItem = null;
                searchItemTypeBox.Enabled = false;
                searchItemNameBox.Text = string.Empty;
                searchItemNameBox.Enabled = false;
                searchItemShapeBox.Text = string.Empty;
                searchItemShapeBox.Enabled = false;
                searchItemButton.Enabled = false;
                doSearch.Text = "Search Off";
            }
        }

        private void ItemNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Name = ItemNameTextBox.Text;
            MotherParent.NeedSave = true;
            //UpdateInterface(true);

        }
        private void ITypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Type = (ItemType)ITypeComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }
        private void RTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredType = (RequiredType)RTypeComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }
        private void RGenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredGender = (RequiredGender)RGenderComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }
        private void RClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredClass = (RequiredClass)RClassComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }
        private void StartItemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].StartItem = StartItemCheckBox.Checked;
            MotherParent.NeedSave = true;
        }
        private void WeightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Weight = temp;
            MotherParent.NeedSave = true;
        }
        private void ImageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Image = temp;
            MotherParent.NeedSave = true;
        }
        private void DuraTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Durability = temp;
            MotherParent.NeedSave = true;
        }
        private void ShapeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!short.TryParse(ActiveControl.Text, out short temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Shape = temp;
            MotherParent.NeedSave = true;
        }
        private void SSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].StackSize = temp;
            MotherParent.NeedSave = true;
        }
        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Price = temp;
            MotherParent.NeedSave = true;
        }

        private void UpdateTextBox_Changed(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].UpdateTo = temp;
            MotherParent.NeedSave = true;
        }

        private void RAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredAmount = temp;
            MotherParent.NeedSave = true;
        }
        private void LightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            if (temp > 14)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Light = (byte)(temp + (_SelectedItems[i].Light / 15) * 15);
            MotherParent.NeedSave = true;
        }
        private void MinACTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxACTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinMACTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxMACTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinDCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinDC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxDCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxDC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinMCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxMCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinSCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinSC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxSCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxSC = temp;
            MotherParent.NeedSave = true;
        }
        private void HPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HP = temp;
            MotherParent.NeedSave = true;
        }
        private void MPTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MP = temp;
            MotherParent.NeedSave = true;
        }
        private void AccuracyTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Accuracy = temp;
            MotherParent.NeedSave = true;
        }
        private void AgilityTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Agility = temp;
            MotherParent.NeedSave = true;
        }
        private void ASpeedTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!sbyte.TryParse(ActiveControl.Text, out sbyte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AttackSpeed = temp;
            MotherParent.NeedSave = true;
        }
        private void LuckTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!sbyte.TryParse(ActiveControl.Text, out sbyte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Luck = temp;
            MotherParent.NeedSave = true;
        }
        private void BWeightText_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].BagWeight = temp;
            MotherParent.NeedSave = true;

        }
        private void HWeightTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HandWeight = temp;
            MotherParent.NeedSave = true;

        }
        private void WWeightTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].WearWeight = temp;
            MotherParent.NeedSave = true;

        }
        private void EffectTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Effect = temp;
            MotherParent.NeedSave = true;
        }

        private void ISetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Set = (ItemSet)ISetComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void ACRateTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxAcRate = temp;
            MotherParent.NeedSave = true;
        }

        private void MacRateTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMacRate = temp;
            MotherParent.NeedSave = true;
        }

        private void MagicResisttextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MagicResist = temp;
            MotherParent.NeedSave = true;
        }

        private void PoisonResisttextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].PoisonResist = temp;
            MotherParent.NeedSave = true;
        }

        private void HealthRecoveryTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HealthRecovery = temp;
            MotherParent.NeedSave = true;
        }

        private void SpellRecoverytextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].SpellRecovery = temp;
            MotherParent.NeedSave = true;
        }

        private void PoisonRecoverytextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].PoisonRecovery = temp;
            MotherParent.NeedSave = true;
        }

        private void HporMpRatetextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HPrate = temp;
            MotherParent.NeedSave = true;
        }

        private void Holytextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Holy = temp;
            MotherParent.NeedSave = true;
        }

        private void Freezingtextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Freezing = temp;
            MotherParent.NeedSave = true;
        }

        private void PoisonAttacktextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].PoisonAttack = temp;
            MotherParent.NeedSave = true;
        }

        private void ClassBasedcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ClassBased = ClassBasedcheckbox.Checked;
            MotherParent.NeedSave = true;
        }

        private void LevelBasedcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelBased = LevelBasedcheckbox.Checked;
            MotherParent.NeedSave = true;
        }

        private void Bind_dontdropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontdropcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontDrop : _SelectedItems[i].Bind ^= BindMode.DontDrop);
            MotherParent.NeedSave = true;
        }

        private void Bind_dontdeathdropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontdeathdropcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontDeathdrop : _SelectedItems[i].Bind ^= BindMode.DontDeathdrop);
            MotherParent.NeedSave = true;
        }

        private void Bind_destroyondropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_destroyondropcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DestroyOnDrop : _SelectedItems[i].Bind ^= BindMode.DestroyOnDrop);
            MotherParent.NeedSave = true;
        }

        private void Bind_dontsellcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontsellcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontSell : _SelectedItems[i].Bind ^= BindMode.DontSell);
            MotherParent.NeedSave = true;
        }

        private void Bind_donttradecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_donttradecheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontTrade : _SelectedItems[i].Bind ^= BindMode.DontTrade);
            MotherParent.NeedSave = true;
        }

        private void Bind_dontrepaircheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontrepaircheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontRepair : _SelectedItems[i].Bind ^= BindMode.DontRepair);
            MotherParent.NeedSave = true;
        }

        private void Bind_dontstorecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontstorecheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontStore : _SelectedItems[i].Bind ^= BindMode.DontStore);
            MotherParent.NeedSave = true;
        }

        private void Bind_dontupgradecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_dontupgradecheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.DontUpgrade : _SelectedItems[i].Bind ^= BindMode.DontUpgrade);
            MotherParent.NeedSave = true;
        }

        private void NeedIdentifycheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].NeedIdentify = NeedIdentifycheckbox.Checked;
            MotherParent.NeedSave = true;
        }

        private void ShowGroupPickupcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ShowGroupPickup = ShowGroupPickupcheckbox.Checked;
            MotherParent.NeedSave = true;
        }

        private void BindOnEquipcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (BindOnEquipcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.BindOnEquip : _SelectedItems[i].Bind ^= BindMode.BindOnEquip);
            MotherParent.NeedSave = true;
        }

        private void MPratetextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MPrate = temp;
            MotherParent.NeedSave = true;
        }

        private void HpDrainRatetextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HpDrainRate = temp;
            MotherParent.NeedSave = true;
        }


        private void ParalysischeckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (ParalysischeckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Paralize : _SelectedItems[i].Unique ^= SpecialItemMode.Paralize);
            MotherParent.NeedSave = true;
        }

        private void TeleportcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (TeleportcheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Teleport : _SelectedItems[i].Unique ^= SpecialItemMode.Teleport);
            MotherParent.NeedSave = true;
        }

        private void ClearcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (ClearcheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Clearring : _SelectedItems[i].Unique ^= SpecialItemMode.Clearring);
            MotherParent.NeedSave = true;
        }

        private void ProtectioncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (ProtectioncheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Protection : _SelectedItems[i].Unique ^= SpecialItemMode.Protection);
            MotherParent.NeedSave = true;
        }

        private void RevivalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (RevivalcheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Revival : _SelectedItems[i].Unique ^= SpecialItemMode.Revival);
            MotherParent.NeedSave = true;
        }

        private void MusclecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (MusclecheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Muscle : _SelectedItems[i].Unique ^= SpecialItemMode.Muscle);
            MotherParent.NeedSave = true;
        }

        private void FlamecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (FlamecheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Flame : _SelectedItems[i].Unique ^= SpecialItemMode.Flame);
            MotherParent.NeedSave = true;
        }

        private void HealingcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (HealingcheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Healing : _SelectedItems[i].Unique ^= SpecialItemMode.Healing);
            MotherParent.NeedSave = true;
        }

        private void ProbecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (ProbecheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Probe : _SelectedItems[i].Unique ^= SpecialItemMode.Probe);
            MotherParent.NeedSave = true;
        }

        private void SkillcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (SkillcheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.Skill : _SelectedItems[i].Unique ^= SpecialItemMode.Skill);
            MotherParent.NeedSave = true;
        }

        private void NoDuraLosscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Unique = (NoDuraLosscheckBox.Checked ? _SelectedItems[i].Unique |= SpecialItemMode.NoDuraLoss : _SelectedItems[i].Unique ^= SpecialItemMode.NoDuraLoss);
            MotherParent.NeedSave = true;
        }

        private void StrongTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Strong = temp;
            MotherParent.NeedSave = true;
        }

        private void CriticalRatetextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CriticalRate = temp;
            MotherParent.NeedSave = true;
        }

        private void CriticalDamagetextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CriticalDamage = temp;
            MotherParent.NeedSave = true;
        }

        private void ReflecttextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Reflect = temp;
            MotherParent.NeedSave = true;
        }

        private void Bind_DontSpecialRepaircheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (Bind_DontSpecialRepaircheckBox.Checked ? _SelectedItems[i].Bind |= BindMode.NoSRepair : _SelectedItems[i].Bind ^= BindMode.NoSRepair);
            MotherParent.NeedSave = true;
        }

        private void LightIntensitytextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            if (temp > 4)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Light = (byte)((_SelectedItems[i].Light % 15) + (15 * temp));
            MotherParent.NeedSave = true;
        }

        private void RandomStatstextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            if ((temp >= Settings.RandomItemStatsList.Count) && (temp != 255))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
            {
                _SelectedItems[i].RandomStatsId = temp;
                if (temp != 255)
                    _SelectedItems[i].RandomStats = Settings.RandomItemStatsList[temp];
                else
                    _SelectedItems[i].RandomStats = null;
            }
            MotherParent.NeedSave = true;
        }

        private void PickaxecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanMine = PickaxecheckBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void IGradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Grade = (ItemGrade)IGradeComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void FastRunCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanFastRun = FastRunCheckBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void TooltipTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ToolTip = TooltipTextBox.Text;
            MotherParent.NeedSave = true;
        }

        private void CanAwakening_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanAwakening = CanAwaken.Checked;
            MotherParent.NeedSave = true;
        }

        private void BreakOnDeathcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (BreakOnDeathcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.BreakOnDeath : _SelectedItems[i].Bind ^= BindMode.BreakOnDeath);
            MotherParent.NeedSave = true;
        }

        private void Gameshop_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItems == null ||
                _SelectedItems.Count == 0)
                return;
            if (MotherParent != null)
            {
                if (_SelectedItems.Count == 1)
                    MotherParent.AddGameShopItem(_SelectedItems[0]);
                else if (_SelectedItems.Count > 1)
                    MotherParent.AddGameShopItem(_SelectedItems);
            }
            /*
            for (int i = 0; i < _SelectedItems.Count; i++)
                Envir.AddToGameShop(_SelectedItems[i]);
            Envir.SaveDB();
            */
        }

        private void NoWeddingRingcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Bind = (NoWeddingRingcheckbox.Checked ? _SelectedItems[i].Bind |= BindMode.NoWeddingRing : _SelectedItems[i].Bind ^= BindMode.NoWeddingRing);
            MotherParent.NeedSave = true;
        }

        private void WeaponEffectTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].WeaponEffects = temp;
            MotherParent.NeedSave = true;
        }

        private void ItemGlowBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ItemGlow = temp;
            MotherParent.NeedSave = true;
        }

        private void HumUpBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HumUpBased = HumUpBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void HumUpResBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HumUpRestricted = HumUpResBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void WearBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].WearType = (WearType)WearBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void ExpBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysExp[0] = temp;
            MotherParent.NeedSave = true;
        }

        private void ExpBox2_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[1] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox3_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[2] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox4_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[3] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox5_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[4] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox6_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[5] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox7_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[6] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox8_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[7] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox9_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[8] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void ExpBox10_TextChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;


                if (!int.TryParse(ActiveControl.Text, out int temp))
                {
                    ActiveControl.BackColor = Color.Red;
                    return;
                }
                ActiveControl.BackColor = SystemColors.Window;


                for (int i = 0; i < _SelectedItems.Count; i++)
                    _SelectedItems[i].LvlSysExp[9] = temp;
            }
            MotherParent.NeedSave = true;
        }

        private void EnableLvlSysBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AllowLvlSys = EnableLvlSysBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void LvlSysComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLvlSys();
        }

        private void lvlsysMinAC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMinAC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMaxAC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMaxAC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMinDC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMinDC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMaxDC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMaxDC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMinMAC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMinMAC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMaxMAC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMaxMAC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMinMC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMinMC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMaxMC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMaxMC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMinSC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMinSC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void lvlsysMaxSC_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlSysMaxSC[LvlSysComboBox.SelectedIndex] = temp;
            MotherParent.NeedSave = true;
        }

        private void LvlableBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LvlableBy = (WearType)WearBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void RandomStatsBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AllowRandomStats = RandomStatsBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void minSocketBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinSocket = temp;
            MotherParent.NeedSave = true;
        }

        private void maxSocketBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            if (temp > 3)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxSocket = temp;
            MotherParent.NeedSave = true;
        }

        private void runeTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (runeTypeBox.SelectedItem != null)
            {
                switch ((SocketType)runeTypeBox.SelectedItem)
                {
                    #region Preset Stats
                    case SocketType.DamageReductionPvE:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Reduction(PvE) %";
                        ShapeTextBox.Text = "0";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 0;
                        break;
                    case SocketType.DamageReductionPvP:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Reduction(PvP) %";
                        ShapeTextBox.Text = "1";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 1;
                        break;
                    case SocketType.DamageIncreasePvE:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Increase(PvE) %";
                        ShapeTextBox.Text = "2";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 2;
                        break;
                    case SocketType.DamageIncreasePvP:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Increase(PvP) %";
                        ShapeTextBox.Text = "3";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 3;
                        break;
                    case SocketType.MeleeDamageBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Melee Damage Bonus %";
                        ShapeTextBox.Text = "4";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 4;
                        break;
                    case SocketType.MagicDamageBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Magic Damage Bonus %";
                        ShapeTextBox.Text = "5";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 5;
                        break;
                    case SocketType.SpiritualBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Spirit Damage Bonus %";
                        ShapeTextBox.Text = "6";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 6;
                        break;
                    case SocketType.HealthBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = true;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = true;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = false;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        ShapeTextBox.Text = "7";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 7;
                        break;
                    case SocketType.ManaBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = true;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = false;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = true;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        ShapeTextBox.Text = "8";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 8;
                        break;
                    case SocketType.HealthRegenBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = true;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        ShapeTextBox.Text = "9";
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = true;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = false;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 9;
                        break;
                    case SocketType.ManaRegenBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = true;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        ShapeTextBox.Text = "10";
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = false;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = true;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 10;
                        break;
                    case SocketType.DestructionBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = true;
                        rnMaxDCBox.Visible = true;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        rnStat0Lbl.Visible = true;
                        rnStat1Lbl.Visible = true;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = false;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = false;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        ShapeTextBox.Text = "11";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 1;
                        break;
                    case SocketType.MagicBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = true;
                        rnMaxMCBox.Visible = true;
                        rnMinSCBox.Visible = false;
                        rnMaxSCBox.Visible = false;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = true;
                        rnStat3Lbl.Visible = true;
                        rnStat4Lbl.Visible = false;
                        rnStat5Lbl.Visible = false;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = false;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = false;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        ShapeTextBox.Text = "12";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 12;
                        break;
                    case SocketType.SpiritBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        rnMinDCBox.Visible = false;
                        rnMaxDCBox.Visible = false;
                        rnMinMCBox.Visible = false;
                        rnMaxMCBox.Visible = false;
                        rnMinSCBox.Visible = true;
                        rnMaxSCBox.Visible = true;
                        rnMinACBox.Visible = false;
                        rnMaxACBox.Visible = false;
                        rnMinMACBox.Visible = false;
                        rnMaxMACBox.Visible = false;
                        rnHPBox.Visible = false;
                        rnHPRegenBox.Visible = false;
                        rnMPBox.Visible = false;
                        rnMPRegenBox.Visible = false;
                        rnAccuracyBox.Visible = false;
                        rnAgilBox.Visible = false;
                        rnStat0Lbl.Visible = false;
                        rnStat1Lbl.Visible = false;
                        rnStat2Lbl.Visible = false;
                        rnStat3Lbl.Visible = false;
                        rnStat4Lbl.Visible = true;
                        rnStat5Lbl.Visible = true;
                        rnStat6Lbl.Visible = false;
                        rnStat7Lbl.Visible = false;
                        rnStat8Lbl.Visible = false;
                        rnStat9Lbl.Visible = false;
                        rnStat10Lbl.Visible = false;
                        rnStat11Lbl.Visible = false;
                        rnStat12Lbl.Visible = false;
                        rnStat13Lbl.Visible = false;
                        rnStat14Lbl.Visible = false;
                        rnStat15Lbl.Visible = false;
                        ShapeTextBox.Text = "13";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 13;
                        break;
                    #region Passive Buffs
                    case SocketType.PinPoint:
                        specialRunePanel.Visible = true;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = false;
                        rStat0Lbl.Text = "Accuracy";
                        rStat1Lbl.Text = "Max DC";
                        rStat2Lbl.Text = "Crit Rate";
                        rStat3Lbl.Text = "Crit Dmg";
                        rStat1Lbl.Visible = true;
                        rStat2Lbl.Visible = true;
                        rStat3Lbl.Visible = true;
                        ShapeTextBox.Text = "30";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 30;
                        break;
                    case SocketType.Evasive:
                        specialRunePanel.Visible = true;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = false;
                        rStat0Lbl.Text = "Max AC";
                        rStat1Lbl.Text = "Max MAC";
                        rStat2Lbl.Text = "Agil";
                        rStat1Lbl.Visible = true;
                        rStat2Lbl.Visible = true;
                        rStat3Lbl.Visible = false;
                        ShapeTextBox.Text = "40";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 40;
                        break;
                    case SocketType.Enrage:
                        specialRunePanel.Visible = true;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = false;
                        rStat0Lbl.Text = "A.Speed";
                        rStat1Lbl.Text = "Max DC";
                        rStat2Lbl.Text = "Crit Rate";
                        rStat3Lbl.Text = "Crit Dmg";
                        rStat1Lbl.Visible = true;
                        rStat2Lbl.Visible = true;
                        rStat3Lbl.Visible = true;
                        ShapeTextBox.Text = "50";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 50;
                        break;
                    case SocketType.IronWall:
                        specialRunePanel.Visible = true;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = false;
                        rStat0Lbl.Text = "Min AC|MAC";
                        rStat1Lbl.Text = "Max AC|MAC";
                        rStat2Lbl.Text = "HP";
                        rStat3Lbl.Text = "Agil";
                        rStat1Lbl.Visible = true;
                        rStat2Lbl.Visible = true;
                        rStat3Lbl.Visible = true;
                        ShapeTextBox.Text = "60";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 60;
                        break;
                    case SocketType.SpeedyMagician:
                        specialRunePanel.Visible = true;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = false;
                        rStat0Lbl.Text = "Skill Cool-Down\r\nReduction %";
                        rStat1Lbl.Visible = false;
                        rStat2Lbl.Visible = false;
                        rStat3Lbl.Visible = false;
                        ShapeTextBox.Text = "70";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 70;
                        break;
                    #endregion
                    #endregion
                    case SocketType.NONE:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        ShapeTextBox.Text = "255";
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Shape = 255;
                        rnMinDCBox.Visible = true;
                        rnMaxDCBox.Visible = true;
                        rnMinSCBox.Visible = true;
                        rnMaxSCBox.Visible = true;
                        rnMinMCBox.Visible = true;
                        rnMaxMCBox.Visible = true;
                        rnMinACBox.Visible = true;
                        rnMaxACBox.Visible = true;
                        rnMinMACBox.Visible = true;
                        rnMaxMACBox.Visible = true;
                        rnHPBox.Visible = true;
                        rnHPRegenBox.Visible = true;
                        rnMPBox.Visible = true;
                        rnMPRegenBox.Visible = true;
                        rnAccuracyBox.Visible = true;
                        rnAgilBox.Visible = true;
                        rnStat0Lbl.Visible = true;
                        rnStat1Lbl.Visible = true;
                        rnStat2Lbl.Visible = true;
                        rnStat3Lbl.Visible = true;
                        rnStat4Lbl.Visible = true;
                        rnStat5Lbl.Visible = true;
                        rnStat6Lbl.Visible = true;
                        rnStat7Lbl.Visible = true;
                        rnStat8Lbl.Visible = true;
                        rnStat9Lbl.Visible = true;
                        rnStat10Lbl.Visible = true;
                        rnStat11Lbl.Visible = true;
                        rnStat12Lbl.Visible = true;
                        rnStat13Lbl.Visible = true;
                        rnStat14Lbl.Visible = true;
                        rnStat15Lbl.Visible = true;
                        break;
                }
            }
            MotherParent.NeedSave = true;
        }

        private void raBox_TextChanged(object sender, EventArgs e)
        {
            ushort usTemp;
            byte bTemp;
            if (runeTypeBox.SelectedItem != null)
            {
                switch ((SocketType)runeTypeBox.SelectedItem)
                {
                    case SocketType.DamageIncreasePvE:
                        if (ActiveControl != sender)
                            return;

                        if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MinAC = usTemp;
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MinAC = usTemp;
                        break;
                    case SocketType.DamageIncreasePvP:
                        if (ActiveControl != sender)
                            return;

                        if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MinMAC = usTemp;

                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MinMAC = usTemp;
                        break;
                    case SocketType.DamageReductionPvE:
                        if (ActiveControl != sender)
                            return;

                        if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MaxAC = usTemp;
                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MaxAC = usTemp;
                        break;
                    case SocketType.DamageReductionPvP:
                        if (ActiveControl != sender)
                            return;

                        if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MaxMAC = usTemp;

                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MaxMAC = usTemp;
                        break;
                    case SocketType.MeleeDamageBonus:
                        if (ActiveControl != sender)
                            return;

                        if (!byte.TryParse(ActiveControl.Text, out bTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MaxAcRate = bTemp;

                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MaxAcRate = bTemp;
                        break;
                    case SocketType.MagicDamageBonus:
                        if (ActiveControl != sender)
                            return;

                        if (!byte.TryParse(ActiveControl.Text, out bTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].MaxMacRate = bTemp;

                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.MaxMacRate = bTemp;
                        break;
                    case SocketType.SpiritualBonus:
                        if (ActiveControl != sender)
                            return;

                        if (!byte.TryParse(ActiveControl.Text, out bTemp))
                        {
                            ActiveControl.BackColor = Color.Red;
                            return;
                        }
                        ActiveControl.BackColor = SystemColors.Window;


                        for (int i = 0; i < _SelectedItems.Count; i++)
                            _SelectedItems[i].Holy = bTemp;

                        foreach (var itemInfo in _SelectedItems)
                            itemInfo.Holy = bTemp;
                        break;
                }
            }
            MotherParent.NeedSave = true;
        }

        private void rnMinDCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinDC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinDC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMinMCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinMC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMinSCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinSC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinSC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMinACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinAC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinAC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMinMACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMAC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MinMAC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnHPBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HP = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.HP = temp;
            MotherParent.NeedSave = true;
        }

        private void rnHPRegenBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HPrate = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.HPrate = temp;
            MotherParent.NeedSave = true;
        }

        private void rnAccuracyBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Accuracy = temp;
            foreach (var itemInfo in _SelectedItems)
                itemInfo.Accuracy = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMaxDCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxDC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxDC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMaxMCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxMC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMaxSCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxSC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxSC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMaxACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxAC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxAC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMaxMACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMAC = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MaxMAC = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMPBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MP = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MP = temp;
            MotherParent.NeedSave = true;
        }

        private void rnMPRegenBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MPrate = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.MPrate = temp;
            MotherParent.NeedSave = true;
        }

        private void rnAgilBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Agility = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.Agility = temp;
            MotherParent.NeedSave = true;
        }

        //  Chance  -   Weight ( #/10000 )
        private void runeCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Weight = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.Weight = temp;
            MotherParent.NeedSave = true;
        }

        //  Duration    -   Durability ( Seconds )
        private void runeDBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Durability = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.Durability = temp;
            MotherParent.NeedSave = true;
        }

        //  Cool-Down   -   RequiredAmount ( Seconds )
        private void runeCDBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredAmount = temp;

            foreach (var itemInfo in _SelectedItems)
                itemInfo.RequiredAmount = temp;
            MotherParent.NeedSave = true;
        }

        private void rStat0Box_TextChanged(object sender, EventArgs e)
        {
            byte bTemp;
            sbyte sbTemp;
            ushort usTemp;
            switch ((SocketType)runeTypeBox.SelectedItem)
            {
                case SocketType.PinPoint:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].Accuracy = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.Accuracy = bTemp;
                    break;
                case SocketType.Evasive:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].MaxAC = usTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.MaxAC = usTemp;
                    break;
                case SocketType.Enrage:
                    if (ActiveControl != sender)
                        return;

                    if (!sbyte.TryParse(ActiveControl.Text, out sbTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].AttackSpeed = sbTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.AttackSpeed = sbTemp;
                    break;
                case SocketType.IronWall:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                    {
                        _SelectedItems[i].MinAC = usTemp;
                        _SelectedItems[i].MinMAC = usTemp;
                    }
                    foreach (var itemInfo in _SelectedItems)
                    {
                        itemInfo.MinAC = usTemp;
                        itemInfo.MinMAC = usTemp;
                    }
                    break;
                case SocketType.SpeedyMagician:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].MinMC = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.MinMC = bTemp;
                    break;
            }
            MotherParent.NeedSave = true;
        }

        private void rStat1Box_TextChanged(object sender, EventArgs e)
        {
            ushort usTemp;
            switch ((SocketType)runeTypeBox.SelectedItem)
            {
                case SocketType.PinPoint:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].MaxDC = usTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.MaxDC = usTemp;
                    break;
                case SocketType.Evasive:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].MaxMAC = usTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.MaxMAC = usTemp;
                    break;
                case SocketType.Enrage:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].MaxDC = usTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.MaxDC = usTemp;
                    break;
                case SocketType.IronWall:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                    {
                        _SelectedItems[i].MaxAC = usTemp;
                        _SelectedItems[i].MaxMAC = usTemp;
                    }
                    foreach (var itemInfo in _SelectedItems)
                    {
                        itemInfo.MaxAC = usTemp;
                        itemInfo.MaxMAC = usTemp;
                    }
                    break;
            }
            MotherParent.NeedSave = true;
        }

        private void rStat2Box_TextChanged(object sender, EventArgs e)
        {
            byte bTemp;
            ushort usTemp;
            switch ((SocketType)runeTypeBox.SelectedItem)
            {
                case SocketType.PinPoint:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].CriticalRate = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.CriticalRate = bTemp;
                    break;
                case SocketType.Evasive:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].Agility = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.Agility = bTemp;
                    break;
                case SocketType.Enrage:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].CriticalRate = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.CriticalRate = bTemp;
                    break;
                case SocketType.IronWall:
                    if (ActiveControl != sender)
                        return;

                    if (!ushort.TryParse(ActiveControl.Text, out usTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].HP = usTemp;
                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.HP = usTemp;
                    break;
            }
            MotherParent.NeedSave = true;
        }

        private void rStat3Box_TextChanged(object sender, EventArgs e)
        {
            byte bTemp;
            switch ((SocketType)runeTypeBox.SelectedItem)
            {
                case SocketType.PinPoint:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].CriticalDamage = bTemp;

                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.CriticalDamage = bTemp;
                    break;
                case SocketType.Enrage:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                        _SelectedItems[i].CriticalDamage = bTemp;
                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.CriticalDamage = bTemp;
                    break;
                case SocketType.IronWall:
                    if (ActiveControl != sender)
                        return;

                    if (!byte.TryParse(ActiveControl.Text, out bTemp))
                    {
                        ActiveControl.BackColor = Color.Red;
                        return;
                    }
                    ActiveControl.BackColor = SystemColors.Window;


                    for (int i = 0; i < _SelectedItems.Count; i++)
                    {
                        _SelectedItems[i].Agility = bTemp;
                    }
                    foreach (var itemInfo in _SelectedItems)
                        itemInfo.Agility = bTemp;
                    break;
            }
            MotherParent.NeedSave = true;
        }

        private void posElementsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].PositiveElement = (ElementPos)posElementsBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void negElementBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].NegativeElement = (ElementNeg)negElementBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void eleposAmountBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].PositiveElementAmount = temp;
            MotherParent.NeedSave = true;
        }

        private void elenegAmountBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].NegativeElementAmount = temp;
            MotherParent.NeedSave = true;
        }

        private void ItemNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Tab)
                UpdateList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (MotherParent != null)
            {
                ItemInfo newItem = MotherParent.AddItem();
                _SelectedItems.Clear();
                _SelectedItems.Add(newItem);
                UpdateList();
                MotherParent.NeedSave = true;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_SelectedItems == null ||
                _SelectedItems.Count == 0) return;
            DialogResult result = MessageBox.Show(string.Format("Are you sure you want to delete {0}?", _SelectedItems.Count == 1 ? _SelectedItems[0].Name : _SelectedItems.Count + " Items"));
        }
        private void ImportButton_Click(object sender, EventArgs e)
        {
            Import();
            UpdateList();
            MotherParent.NeedSave = true;
        }

        private void ExportSelectedButton_Click(object sender, EventArgs e)
        {
            Export(_SelectedItems);
        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            Export(ItemInfoList);
        }
        #endregion

        private void dropBuildBtn_Click(object sender, EventArgs e)
        {
            if (MotherParent != null)
            {

            }
        }

        #region Weapon Shape Change
        private void imageBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[0] = temp;
        }

        private void imageBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[1] = temp;
        }

        private void imageBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[2] = temp;
        }

        private void imageBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[3] = temp;
        }

        private void imageBox5_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[4] = temp;
        }

        private void imageBox6_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[5] = temp;
        }
        private void imageBox7_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[6] = temp;
        }


        private void imageBox8_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[7] = temp;
        }
        private void imageBox9_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[8] = temp;
        }

        private void imageBox10_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapLooks[9] = temp;
        }
        #endregion

        #region Armour Shape Change
        private void armimageBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[0] = temp;
        }

        private void armimageBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[1] = temp;
        }

        private void armimageBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[2] = temp;
        }

        private void armimageBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[3] = temp;
        }

        private void armimageBox5_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[4] = temp;
        }

        private void armimageBox6_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[5] = temp;
        }

        private void armimageBox7_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[6] = temp;
        }

        private void armimageBox8_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[7] = temp;
        }

        private void armimageBox9_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[8] = temp;
        }

        private void armimageBox10_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourLooks[9] = temp;

        }
        #endregion

        #region Item Looks Change
        private void itemimageBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[0] = temp;
        }

        private void itemimageBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[1] = temp;
        }

        private void itemimageBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[2] = temp;
        }

        private void itemimageBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[3] = temp;
        }

        private void itemimageBox5_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[4] = temp;
        }

        private void itemimageBox6_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[5] = temp;

        }

        private void itemimageBox7_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[6] = temp;
        }

        private void itemimageBox8_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[7] = temp;
        }

        private void itemimageBox9_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[8] = temp;
        }

        private void itemimageBox10_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemLooks[9] = temp;
        }
        #endregion

        #region Item Level Weapon Effects
        //  Weapon Effect Lv1
        private void effectBox40_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[0] = temp;
        }
        //  Weapon Effect Lv2
        private void effectBox41_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[1] = temp;
        }
        //  Weapon Effect Lv3
        private void effectBox42_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[2] = temp;
        }
        //  Weapon Effect Lv4
        private void effectBox43_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[3] = temp;
        }
        //  Weapon Effect Lv5
        private void effectBox44_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[4] = temp;
        }
        //  Weapon Effect Lv6
        private void effectBox45_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[5] = temp;
        }
        //  Weapon Effect Lv7
        private void effectBox46_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[6] = temp;
        }
        //  Weapon Effect Lv8
        private void effectBox47_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[7] = temp;
        }
        //  Weapon Effect Lv9
        private void effectBox48_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[8] = temp;
        }
        //  Weapon Effect Lv10
        private void effectBox49_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelWeapEffect[9] = temp;
        }
        #endregion

        #region Item Level Armour Effects
        //  Armour Effect Lv1
        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[0] = temp;
        }
        //  Armour Effect Lv2
        private void textBox51_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[1] = temp;
        }
        //  Armour Effect Lv3
        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[2] = temp;
        }
        //  Armour Effect Lv4
        private void textBox53_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[3] = temp;
        }
        //  Armour Effect Lv5
        private void textBox54_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[4] = temp;
        }
        //  Armour Effect Lv6
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[5] = temp;
        }
        //  Armour Effect Lv7
        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[6] = temp;
        }
        //  Armour Effect Lv8
        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[7] = temp;
        }
        //  Armour Effect Lv9
        private void textBox58_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[8] = temp;
        }
        //  Armour Effect Lv10
        private void textBox59_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _SelectedItems.Count; i++)
                if (_SelectedItems[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelArmourEffect[9] = temp;
        }
        #endregion

        #region Item Level Glow effects
        //  Item Glow Lv1
        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[0] = temp;
        }
        //  Item Glow Lv2
        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[1] = temp;
        }
        //  Item Glow Lv3
        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[2] = temp;
        }
        //  Item Glow Lv4
        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[3] = temp;
        }
        //  Item Glow Lv5
        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[4] = temp;
        }
        //  Item Glow Lv6
        private void textBox45_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[5] = temp;
        }
        //  Item Glow Lv7
        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[6] = temp;
        }
        //  Item Glow Lv8
        private void textBox47_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[7] = temp;
        }
        //  Item Glow Lv9
        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[8] = temp;
        }
        //  Item Glow Lv10
        private void textBox49_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LevelItemGlow[9] = temp;
        }
        #endregion
    }
}
