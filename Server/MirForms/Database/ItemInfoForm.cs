using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server.MirEnvir;

namespace Server
{
    public partial class ItemInfoForm : Form
    {
        public string ItemListPath = Path.Combine(Settings.ExportPath, "ItemList.txt");

        public Envir Envir
        {
            get { return SMain.EditEnvir; }
        }
        private List<ItemInfo> _selectedItemInfos;

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public ItemInfoForm()
        {
            InitializeComponent();

            ITypeComboBox.Items.AddRange(Enum.GetValues(typeof (ItemType)).Cast<object>().ToArray());
            IGradeComboBox.Items.AddRange(Enum.GetValues(typeof(ItemGrade)).Cast<object>().ToArray());
            RTypeComboBox.Items.AddRange(Enum.GetValues(typeof (RequiredType)).Cast<object>().ToArray());
            RClassComboBox.Items.AddRange(Enum.GetValues(typeof (RequiredClass)).Cast<object>().ToArray());
            WearBox.Items.AddRange(Enum.GetValues(typeof(WearType)).Cast<object>().ToArray());
            LvlableBy.Items.AddRange(Enum.GetValues(typeof(WearType)).Cast<object>().ToArray());
            RGenderComboBox.Items.AddRange(Enum.GetValues(typeof (RequiredGender)).Cast<object>().ToArray());
            ISetComboBox.Items.AddRange(Enum.GetValues(typeof(ItemSet)).Cast<object>().ToArray());

            posElementsBox.Items.AddRange(Enum.GetValues(typeof(ElementPos)).Cast<object>().ToArray());
            negElementBox.Items.AddRange(Enum.GetValues(typeof(ElementNeg)).Cast<object>().ToArray());
            runeTypeBox.Items.AddRange(Enum.GetValues(typeof(SocketType)).Cast<object>().ToArray());

            ITypeFilterComboBox.Items.AddRange(Enum.GetValues(typeof(ItemType)).Cast<object>().ToArray());
            ITypeFilterComboBox.Items.Add(new ComboBoxItem { Text = "All" });
            ITypeFilterComboBox.SelectedIndex = ITypeFilterComboBox.Items.Count - 1;


            for (int i = 1; i <= 10; i++)
            {
                LvlSysComboBox.Items.Add(i);
            }

            UpdateInterface();
        }

        public void RefreshUniqueTab()
        {
            if ((ITypeComboBox.SelectedItem != null) && ((ItemType)ITypeComboBox.SelectedItem == ItemType.Gem))
            {
                tabControl1.TabPages[3].Text = "Usable on";
                ParalysischeckBox.Text = "Weapon";
                TeleportcheckBox.Text = "Armour";
                ClearcheckBox.Text = "Helmet";
                ProtectioncheckBox.Text = "Necklace";
                RevivalcheckBox.Text = "Bracelet";
                MusclecheckBox.Text = "Ring";
                FlamecheckBox.Text = "Amulet";
                HealingcheckBox.Text = "Belt";
                ProbecheckBox.Text = "Boots";
                SkillcheckBox.Text = "Stone";
                NoDuraLosscheckBox.Text = "Torch";
                PickaxecheckBox.Text = "Unused";
                //label50.Text = "Base rate%";
                //label52.Text = "Success drop";
                //label51.Text = "Max stats (all)";
                //label49.Text = "Max gem stat";
            }
            else
            {
                tabControl1.TabPages[3].Text = "Special Stats";
                ParalysischeckBox.Text = "Paralysis ring";
                TeleportcheckBox.Text = "Teleport ring";
                ClearcheckBox.Text = "Clear ring";
                ProtectioncheckBox.Text = "Protection ring";
                RevivalcheckBox.Text = "Revival ring";
                MusclecheckBox.Text = "Muscle ring";
                FlamecheckBox.Text = "Flame ring";
                HealingcheckBox.Text = "Healing ring";
                ProbecheckBox.Text = "Probe necklace";
                SkillcheckBox.Text = "Skill necklace";
                NoDuraLosscheckBox.Text = "No dura loss";
                PickaxecheckBox.Text = "Pickaxe";
                //label50.Text = "Critical rate:";
                //label52.Text = "Reflect:";
                //label51.Text = "Critical Dmg:";
                //label49.Text = "HP Drain:";
            }
        }
        public bool refresh = true;
        public void UpdateInterface(bool refreshList = false)
        {
            if (refreshList && refresh)
            {
                ItemInfoListBox.Items.Clear();

                for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                {
                    if (ITypeFilterComboBox.SelectedItem == null ||
                        ITypeFilterComboBox.SelectedIndex == ITypeFilterComboBox.Items.Count - 1 ||
                        Envir.ItemInfoList[i].Type == (ItemType)ITypeFilterComboBox.SelectedItem)
                        ItemInfoListBox.Items.Add(Envir.ItemInfoList[i]);
                }
            }

            _selectedItemInfos = ItemInfoListBox.SelectedItems.Cast<ItemInfo>().ToList();


            if (_selectedItemInfos.Count == 0)
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
                imageBox1.Text = string.Empty; //etc...
                imageBox2.Text = string.Empty;
                imageBox3.Text = string.Empty;
                imageBox4.Text = string.Empty;
                imageBox5.Text = string.Empty;
                imageBox6.Text = string.Empty;
                imageBox7.Text = string.Empty;
                imageBox8.Text = string.Empty;
                imageBox9.Text = string.Empty;
                imageBox10.Text = string.Empty;
                armimageBox1.Text = string.Empty;
                armimageBox2.Text = string.Empty;
                armimageBox3.Text = string.Empty;
                armimageBox4.Text = string.Empty;
                armimageBox5.Text = string.Empty;
                armimageBox6.Text = string.Empty;
                armimageBox7.Text = string.Empty;
                armimageBox8.Text = string.Empty;
                armimageBox9.Text = string.Empty;
                armimageBox10.Text = string.Empty;
                itemimageBox1.Text = string.Empty;
                itemimageBox2.Text = string.Empty;
                itemimageBox3.Text = string.Empty;
                itemimageBox4.Text = string.Empty;
                itemimageBox5.Text = string.Empty;
                itemimageBox6.Text = string.Empty;
                itemimageBox7.Text = string.Empty;
                itemimageBox8.Text = string.Empty;
                itemimageBox9.Text = string.Empty;
                itemimageBox10.Text = string.Empty;
                baseratetextBox4.Text = string.Empty;
                baseratedroptextBox2.Text = string.Empty;
                maxstatstextBox3.Text = string.Empty;
                maxgemstattextBox1.Text = string.Empty;
                socketAddBox1.Text = string.Empty;
                effectBox40.Text = string.Empty;
                effectBox41.Text = string.Empty;
                effectBox42.Text = string.Empty;
                effectBox43.Text = string.Empty;
                effectBox44.Text = string.Empty;
                effectBox45.Text = string.Empty;
                effectBox46.Text = string.Empty;
                effectBox47.Text = string.Empty;
                effectBox48.Text = string.Empty;
                effectBox49.Text = string.Empty;

                textBox50.Text = string.Empty;
                textBox51.Text = string.Empty;
                textBox52.Text = string.Empty;
                textBox53.Text = string.Empty;
                textBox54.Text = string.Empty;
                textBox55.Text = string.Empty;
                textBox56.Text = string.Empty;
                textBox57.Text = string.Empty;
                textBox58.Text = string.Empty;
                textBox59.Text = string.Empty;

                textBox40.Text = string.Empty;
                textBox41.Text = string.Empty;
                textBox42.Text = string.Empty;
                textBox43.Text = string.Empty;
                textBox44.Text = string.Empty;
                textBox45.Text = string.Empty;
                textBox46.Text = string.Empty;
                textBox47.Text = string.Empty;
                textBox48.Text = string.Empty;
                textBox49.Text = string.Empty;

                return;
            }

            ItemInfo info = _selectedItemInfos[0];

            ItemInfoPanel.Enabled = true;

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
            noDisassemble.Checked = info.Bind.HasFlag(BindMode.UnableToDisassemble);
            noMailBox.Checked = info.Bind.HasFlag(BindMode.NoMail);
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
            baseratetextBox4.Text = info.BaseRate.ToString();
            baseratedroptextBox2.Text = info.BaseRateDrop.ToString();
            maxstatstextBox3.Text = info.MaxStats.ToString();
            maxgemstattextBox1.Text = info.MaxGemStat.ToString();
            socketAddBox1.Text = info.SocketAdd.ToString();
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

            if (info.Type == ItemType.RuneStone)
            {
                runeTypeBox.SelectedItem = (SocketType)info.Shape;
                switch ((SocketType)info.Shape)
                {
                    #region Preset Stats
                    case SocketType.DamageReductionPvE:
                        raLbl.Text = "Damage Reduction vs Envir (MaxMAC)";
                        raBox.Text = info.MaxMAC.ToString();
                        break;
                    case SocketType.DamageReductionPvP:
                        raLbl.Text = "Damage Reduction vs Player (MaxAC)";
                        raBox.Text = info.MaxAC.ToString();
                        break;
                    case SocketType.DamageIncreasePvE:
                        raLbl.Text = "Damage Increase vs Envir (MinAC)";
                        raBox.Text = info.MinAC.ToString();
                        break;
                    case SocketType.DamageIncreasePvP:
                        raLbl.Text = "Damage Increase vs Player (MaxMAC)";
                        raBox.Text = info.MinMAC.ToString();
                        break;
                    case SocketType.MeleeDamageBonus:
                        raLbl.Text = "Melee Damage bonus % (MaxAcRate)";
                        raBox.Text = info.MaxAcRate.ToString();
                        break;
                    case SocketType.MagicDamageBonus:
                        raLbl.Text = "Magic Damage bonus % (MaxMacRate)";
                        raBox.Text = info.MaxMacRate.ToString();
                        break;
                    case SocketType.SpiritualBonus:
                        raLbl.Text = "Spiritual Damage bonus % (Holy)";
                        raBox.Text = info.Holy.ToString();
                        break;
                    case SocketType.HealthBonus:
                        raLbl.Text = "Health Bonus (HP)";
                        raBox.Text = info.HP.ToString();
                        break;
                    case SocketType.ManaBonus:
                        raLbl.Text = "Mana Bonus (MP)";
                        raBox.Text = info.MP.ToString();
                        break;
                    case SocketType.HealthRegenBonus:
                        raLbl.Text = "Health Recovery Bonus % (HealthRecovery)";
                        raBox.Text = info.HealthRecovery.ToString();
                        break;
                    case SocketType.ManaRegenBonus:
                        raLbl.Text = "Mana Recovery Bonus % (spellRecovery)";
                        raBox.Text = info.SpellRecovery.ToString();
                        break;
                    case SocketType.DestructionBonus:
                        raLbl.Text = "Destruction Bonus - Flat (Min & Max DC)";
                        rnMinDCBox.Text = info.MinDC.ToString();
                        rnMaxDCBox.Text = info.MaxDC.ToString();
                        break;
                    case SocketType.MagicBonus:
                        raLbl.Text = "Magic Bonus - Flat (Min & Max MC)";
                        rnMinMCBox.Text = info.MinMC.ToString();
                        rnMaxMCBox.Text = info.MaxMC.ToString();
                        break;
                    case SocketType.SpiritBonus:
                        raLbl.Text = "Spiritual Bonus - Flat (Min & Max SC)";
                        rnMinSCBox.Text = info.MinSC.ToString();
                        rnMaxSCBox.Text = info.MaxSC.ToString();
                        break;
                    #region Passive Buffs
                    case SocketType.PinPoint:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        /*
                        rStat0Lbl.Text = "Accuracy";
                        rStat1Lbl.Text = "Max DC";
                        rStat2Lbl.Text = "Crit Rate";
                        rStat3Lbl.Text = "Crit Dmg";
                         */
                        rStat0Box.Text = info.Accuracy.ToString();
                        rStat1Box.Text = info.MaxDC.ToString();
                        rStat2Box.Text = info.CriticalRate.ToString();
                        rStat3Box.Text = info.CriticalDamage.ToString();
                        break;
                    case SocketType.Evasive:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        /*
                        rStat0Lbl.Text = "Max AC";
                        rStat1Lbl.Text = "Max MAC";
                        rStat2Lbl.Text = "Agil";
                         */
                        rStat0Box.Text = info.MaxAC.ToString();
                        rStat1Box.Text = info.MaxMAC.ToString();
                        rStat2Box.Text = info.Agility.ToString();
                        break;
                    case SocketType.Enrage:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();
                        /*
                        rStat0Lbl.Text = "A.Speed";
                        rStat1Lbl.Text = "Max DC";
                        rStat2Lbl.Text = "Crit Rate";
                        rStat3Lbl.Text = "Crit Dmg";
                         */
                        rStat0Box.Text = info.AttackSpeed.ToString();
                        rStat1Box.Text = info.MaxDC.ToString();
                        rStat2Box.Text = info.CriticalRate.ToString();
                        rStat3Box.Text = info.CriticalDamage.ToString();
                        break;
                    case SocketType.IronWall:
                        runeCBox.Text = info.Weight.ToString();
                        runeDBox.Text = info.Durability.ToString();
                        runeCDBox.Text = info.RequiredAmount.ToString();

                        /*
                        rStat0Lbl.Text = "Min AC|MAC";
                        rStat1Lbl.Text = "Max AC|MAC";
                        rStat2Lbl.Text = "HP";
                        rStat3Lbl.Text = "Agil";
                         */
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

            for (int i = 1; i < _selectedItemInfos.Count; i++)
            {
                info = _selectedItemInfos[i];

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
                if (noDisassemble.Checked != info.Bind.HasFlag(BindMode.UnableToDisassemble)) noDisassemble.CheckState = CheckState.Indeterminate;
                if (noMailBox.Checked != info.Bind.HasFlag(BindMode.NoMail)) noMailBox.CheckState = CheckState.Indeterminate;
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
                if (imageBox1.Text != info.LevelWeapLooks[0].ToString())
                    imageBox1.Text = string.Empty;
                if (imageBox2.Text != info.LevelWeapLooks[1].ToString())
                    imageBox2.Text = string.Empty;
                if (imageBox3.Text != info.LevelWeapLooks[2].ToString())
                    imageBox3.Text = string.Empty;
                if (imageBox4.Text != info.LevelWeapLooks[3].ToString())
                    imageBox4.Text = string.Empty;
                if (imageBox5.Text != info.LevelWeapLooks[4].ToString())
                    imageBox5.Text = string.Empty;
                if (imageBox6.Text != info.LevelWeapLooks[5].ToString())
                    imageBox6.Text = string.Empty;
                if (imageBox7.Text != info.LevelWeapLooks[6].ToString())
                    imageBox7.Text = string.Empty;
                if (imageBox8.Text != info.LevelWeapLooks[7].ToString())
                    imageBox8.Text = string.Empty;
                if (imageBox9.Text != info.LevelWeapLooks[8].ToString())
                    imageBox9.Text = string.Empty;
                if (imageBox10.Text != info.LevelWeapLooks[9].ToString())
                    imageBox10.Text = string.Empty;
                if (armimageBox1.Text != info.LevelArmourLooks[0].ToString())
                    armimageBox1.Text = string.Empty;
                if (armimageBox2.Text != info.LevelArmourLooks[1].ToString())
                    armimageBox2.Text = string.Empty;
                if (armimageBox3.Text != info.LevelArmourLooks[2].ToString())
                    armimageBox3.Text = string.Empty;
                if (armimageBox4.Text != info.LevelArmourLooks[3].ToString())
                    armimageBox4.Text = string.Empty;
                if (armimageBox5.Text != info.LevelArmourLooks[4].ToString())
                    armimageBox5.Text = string.Empty;
                if (armimageBox6.Text != info.LevelArmourLooks[5].ToString())
                    armimageBox6.Text = string.Empty;
                if (armimageBox7.Text != info.LevelArmourLooks[6].ToString())
                    armimageBox7.Text = string.Empty;
                if (armimageBox8.Text != info.LevelArmourLooks[7].ToString())
                    armimageBox8.Text = string.Empty;
                if (armimageBox9.Text != info.LevelArmourLooks[8].ToString())
                    armimageBox9.Text = string.Empty;
                if (armimageBox10.Text != info.LevelArmourLooks[9].ToString())
                    armimageBox10.Text = string.Empty;
                if (itemimageBox1.Text != info.LevelItemLooks[0].ToString())
                    itemimageBox1.Text = string.Empty;
                if (itemimageBox2.Text != info.LevelItemLooks[1].ToString())
                    itemimageBox2.Text = string.Empty;
                if (itemimageBox3.Text != info.LevelItemLooks[2].ToString())
                    itemimageBox3.Text = string.Empty;
                if (itemimageBox4.Text != info.LevelItemLooks[3].ToString())
                    itemimageBox4.Text = string.Empty;
                if (itemimageBox5.Text != info.LevelItemLooks[4].ToString())
                    itemimageBox5.Text = string.Empty;
                if (itemimageBox6.Text != info.LevelItemLooks[5].ToString())
                    itemimageBox6.Text = string.Empty;
                if (itemimageBox7.Text != info.LevelItemLooks[6].ToString())
                    itemimageBox7.Text = string.Empty;
                if (itemimageBox8.Text != info.LevelItemLooks[7].ToString())
                    itemimageBox8.Text = string.Empty;
                if (itemimageBox9.Text != info.LevelItemLooks[8].ToString())
                    itemimageBox9.Text = string.Empty;
                if (itemimageBox10.Text != info.LevelItemLooks[9].ToString())
                    itemimageBox10.Text = string.Empty;
                if (baseratetextBox4.Text != info.BaseRate.ToString())
                    baseratetextBox4.Text = string.Empty;
                if (baseratedroptextBox2.Text != info.BaseRateDrop.ToString())
                    baseratedroptextBox2.Text = string.Empty;
                if (maxstatstextBox3.Text != info.MaxStats.ToString())
                    maxstatstextBox3.Text = string.Empty;
                if (maxgemstattextBox1.Text != info.MaxGemStat.ToString())
                    maxgemstattextBox1.Text = string.Empty;
                if (socketAddBox1.Text != info.SocketAdd.ToString())
                    socketAddBox1.Text = string.Empty;
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
            RefreshUniqueTab();
            refresh = true;
        }

        private void RefreshItemList()
        {
            ItemInfoListBox.SelectedIndexChanged -= ItemInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < ItemInfoListBox.Items.Count; i++) selected.Add(ItemInfoListBox.GetSelected(i));
            ItemInfoListBox.Items.Clear();
            for (int i = 0; i < Envir.ItemInfoList.Count; i++)
            {
                if (ITypeFilterComboBox.SelectedItem == null ||
                    ITypeFilterComboBox.SelectedIndex == ITypeFilterComboBox.Items.Count - 1 ||
                    Envir.ItemInfoList[i].Type == (ItemType)ITypeFilterComboBox.SelectedItem)
                    ItemInfoListBox.Items.Add(Envir.ItemInfoList[i]);
            };

            ItemInfoListBox.SelectedIndexChanged += ItemInfoListBox_SelectedIndexChanged;
        }

        private void UpdateLvlSys()
        {
            if (_selectedItemInfos.Count == 0) return;

            ItemInfo info = _selectedItemInfos[0];

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

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ITypeFilterComboBox.SelectedIndex == ITypeFilterComboBox.Items.Count - 1)
            {
                Envir.CreateItemInfo();
                ITypeFilterComboBox.SelectedIndex = ITypeFilterComboBox.Items.Count - 1;
            }
            else
            {
                Envir.CreateItemInfo((ItemType)ITypeFilterComboBox.SelectedItem);
            }

            UpdateInterface(true);
        }

        private void ItemInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void ITypeFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface(true);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_selectedItemInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Items?", "Remove Items?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++) Envir.Remove(_selectedItemInfos[i]);

            if (Envir.ItemInfoList.Count == 0) Envir.ItemIndex = 0;

            UpdateInterface(true);
        }
        private void ItemNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Name = ItemNameTextBox.Text;

            //UpdateInterface(true);
            refresh = false;
        }
        private void ITypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Type = (ItemType)ITypeComboBox.SelectedItem;
            refresh = false;
        }
        private void RTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].RequiredType = (RequiredType) RTypeComboBox.SelectedItem;
            refresh = false;
        }
        private void RGenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].RequiredGender = (RequiredGender)RGenderComboBox.SelectedItem;
            refresh = false;
        }
        private void RClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].RequiredClass = (RequiredClass)RClassComboBox.SelectedItem;
            refresh = false;
        }
        private void StartItemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].StartItem = StartItemCheckBox.Checked;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Weight = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Image = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Durability = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Shape = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].StackSize = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Price = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].UpdateTo = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].RequiredAmount = temp;
            refresh = false;
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
            
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Light = (byte)(temp + (_selectedItemInfos[i].Light / 15)*15);
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinAC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxAC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinMAC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxMAC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinDC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxDC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinMC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxMC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinSC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxSC = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HP = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MP = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Accuracy = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Agility = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].AttackSpeed = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Luck = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].BagWeight = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HandWeight = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].WearWeight = temp;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Effect = temp;
            refresh = false;
        }

        private void ItemInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Envir.SaveDB();
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText();

            if (!data.StartsWith("Item", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Cannot Paste, Copied data is not Item Information.");
                return;
            }


            string[] items = data.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 1; i < items.Length; i++)
            {
                ItemInfo info = ItemInfo.FromText(items[i]);

                if (info == null) continue;
                info.Index = ++Envir.ItemIndex;
                Envir.ItemInfoList.Add(info);

            }

            UpdateInterface();
        }

        private void CopyMButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            ExportItems(Envir.ItemInfoList);
        }

        private void ExportSelectedButton_Click(object sender, EventArgs e)
        {
            var list = ItemInfoListBox.SelectedItems.Cast<ItemInfo>().ToList();

            ExportItems(list);
        }

        private void ExportItems(IEnumerable<ItemInfo> items)
        {
            
            var itemInfos = items as ItemInfo[] ?? items.ToArray();
            /*
            var list = itemInfos.Select(item => item.ToText()).ToList();

            File.WriteAllLines(ItemListPath, list);
            */
            using (FileStream stream = File.Create(@"./Exports/ItemInfoExport.dat"))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(itemInfos.Length);
                    for (int i = 0; i < itemInfos.Length; i++)
                    {
                        itemInfos[i].Save(writer);

                    }
                }
            }
                MessageBox.Show(ItemInfoListBox.Items.Count + " Items have been exported");
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            string Path = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Binary File|*.dat"
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
            for(int i = 0; i < list.Count; i++)
            {
                //  Check if the item exists
                ItemInfo tmp = Envir.ItemInfoList.Where(o => o.Name == list[i].Name).FirstOrDefault();
                //  It exists
                if (tmp != null)
                {
                    int origIdx = tmp.Index;
                    for (int x = 0; x < Envir.ItemInfoList.Count; x++)
                    {
                        if (Envir.ItemInfoList[x] == tmp)
                        {
                            if (list[i].Index != origIdx)
                                list[i].Index = origIdx;
                            Envir.ItemInfoList[x] = list[i];
                            updated++;
                        }
                    }
                }
                //  The Item wasn't found so we'll add it
                else
                {
                    list[i].Index = ++Envir.ItemIndex;
                    Envir.ItemInfoList.Add(list[i]);
                    added++;
                }
            }
            MessageBox.Show(string.Format("{0} Items Updated\r\n{1} new Items Added", updated, added));
            UpdateInterface(true);
        }

        private void ISetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Set = (ItemSet)ISetComboBox.SelectedItem;
            refresh = false;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxAcRate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxMacRate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MagicResist = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].PoisonResist = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HealthRecovery = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].SpellRecovery = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].PoisonRecovery = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HPrate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Holy = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Freezing = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].PoisonAttack = temp;
        }

        private void ClassBasedcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].ClassBased = ClassBasedcheckbox.Checked;
        }

        private void LevelBasedcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelBased = LevelBasedcheckbox.Checked;
        }

        private void Bind_dontdropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontdropcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontDrop : _selectedItemInfos[i].Bind ^= BindMode.DontDrop);
        }

        private void Bind_dontdeathdropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontdeathdropcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontDeathdrop : _selectedItemInfos[i].Bind ^= BindMode.DontDeathdrop);
        }

        private void Bind_destroyondropcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_destroyondropcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DestroyOnDrop : _selectedItemInfos[i].Bind ^= BindMode.DestroyOnDrop);
        }

        private void Bind_dontsellcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontsellcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontSell : _selectedItemInfos[i].Bind ^= BindMode.DontSell);
        }

        private void Bind_donttradecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_donttradecheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontTrade : _selectedItemInfos[i].Bind ^= BindMode.DontTrade);
        }

        private void Bind_dontrepaircheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontrepaircheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontRepair : _selectedItemInfos[i].Bind ^= BindMode.DontRepair);
        }

        private void Bind_dontstorecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontstorecheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontStore : _selectedItemInfos[i].Bind ^= BindMode.DontStore);
        }

        private void Bind_dontupgradecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_dontupgradecheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.DontUpgrade : _selectedItemInfos[i].Bind ^= BindMode.DontUpgrade);
        }

        private void NeedIdentifycheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].NeedIdentify = NeedIdentifycheckbox.Checked;
        }

        private void ShowGroupPickupcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].ShowGroupPickup = ShowGroupPickupcheckbox.Checked;
        }

        private void BindOnEquipcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (BindOnEquipcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.BindOnEquip : _selectedItemInfos[i].Bind ^= BindMode.BindOnEquip);
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MPrate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HpDrainRate = temp;
        }


        private void ParalysischeckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (ParalysischeckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Paralize : _selectedItemInfos[i].Unique ^= SpecialItemMode.Paralize);
        }

        private void TeleportcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (TeleportcheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Teleport : _selectedItemInfos[i].Unique ^= SpecialItemMode.Teleport);
        }

        private void ClearcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (ClearcheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Clearring : _selectedItemInfos[i].Unique ^= SpecialItemMode.Clearring);
        }

        private void ProtectioncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (ProtectioncheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Protection : _selectedItemInfos[i].Unique ^= SpecialItemMode.Protection);
        }

        private void RevivalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (RevivalcheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Revival : _selectedItemInfos[i].Unique ^= SpecialItemMode.Revival);
        }

        private void MusclecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (MusclecheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Muscle : _selectedItemInfos[i].Unique ^= SpecialItemMode.Muscle);
        }

        private void FlamecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (FlamecheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Flame : _selectedItemInfos[i].Unique ^= SpecialItemMode.Flame);
        }

        private void HealingcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (HealingcheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Healing : _selectedItemInfos[i].Unique ^= SpecialItemMode.Healing);
        }

        private void ProbecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (ProbecheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Probe : _selectedItemInfos[i].Unique ^= SpecialItemMode.Probe);
        }

        private void SkillcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (SkillcheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.Skill : _selectedItemInfos[i].Unique ^= SpecialItemMode.Skill);
        }

        private void NoDuraLosscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Unique = (NoDuraLosscheckBox.Checked ? _selectedItemInfos[i].Unique |= SpecialItemMode.NoDuraLoss : _selectedItemInfos[i].Unique ^= SpecialItemMode.NoDuraLoss);
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Strong = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].CriticalRate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].CriticalDamage = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Reflect = temp;
        }

        private void Bind_DontSpecialRepaircheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = (Bind_DontSpecialRepaircheckBox.Checked ? _selectedItemInfos[i].Bind |= BindMode.NoSRepair : _selectedItemInfos[i].Bind ^= BindMode.NoSRepair);
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Light = (byte)((_selectedItemInfos[i].Light % 15) + (15 * temp));
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
            {
                _selectedItemInfos[i].RandomStatsId = temp;
                if (temp != 255)
                    _selectedItemInfos[i].RandomStats = Settings.RandomItemStatsList[temp];
                else
                    _selectedItemInfos[i].RandomStats = null;
            }
        }

        private void PickaxecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].CanMine = PickaxecheckBox.Checked;
        }

        private void IGradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Grade = (ItemGrade)IGradeComboBox.SelectedItem;
        }

        private void FastRunCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].CanFastRun = FastRunCheckBox.Checked;
        }

        private void TooltipTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].ToolTip = TooltipTextBox.Text;
        }

        private void CanAwakening_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].CanAwakening = CanAwaken.Checked;
        }

        private void BreakOnDeathcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = 
                    (BreakOnDeathcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.BreakOnDeath : _selectedItemInfos[i].Bind ^= BindMode.BreakOnDeath);
        }

        private void ItemInfoForm_Load(object sender, EventArgs e)
        {

        }

        private void Gameshop_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                Envir.AddToGameShop(_selectedItemInfos[i]);
            Envir.SaveDB();
        }

        private void NoWeddingRingcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind = 
                    (NoWeddingRingcheckbox.Checked ? _selectedItemInfos[i].Bind |= BindMode.NoWeddingRing : _selectedItemInfos[i].Bind ^= BindMode.NoWeddingRing);
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].WeaponEffects = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].ItemGlow = temp;
        }

        private void HumUpBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HumUpBased = HumUpBox.Checked;
        }

        private void HumUpResBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HumUpRestricted = HumUpResBox.Checked;
        }

        private void WearBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].WearType = (WearType)WearBox.SelectedItem;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysExp[0] = temp;
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[1] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[2] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[3] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[4] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[5] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[6] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[7] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[8] = temp;
            }
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


                for (int i = 0; i < _selectedItemInfos.Count; i++)
                    _selectedItemInfos[i].LvlSysExp[9] = temp;
            }
        }

        private void EnableLvlSysBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].AllowLvlSys = EnableLvlSysBox.Checked;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMinAC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMaxAC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMinDC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMaxDC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMinMAC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMaxMAC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMinMC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMaxMC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMinSC[LvlSysComboBox.SelectedIndex] = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlSysMaxSC[LvlSysComboBox.SelectedIndex] = temp;
        }

        private void LvlableBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LvlableBy = (WearType)WearBox.SelectedItem;
        }

        private void RandomStatsBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].AllowRandomStats = RandomStatsBox.Checked;
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
            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinSocket = temp;
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
            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxSocket = temp;
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
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 0;
                        break;
                    case SocketType.DamageReductionPvP:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Reduction(PvP) %";
                        ShapeTextBox.Text = "1";
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 1;
                        break;
                    case SocketType.DamageIncreasePvE:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Increase(PvE) %";
                        ShapeTextBox.Text = "2";
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 2;
                        break;
                    case SocketType.DamageIncreasePvP:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Damage Increase(PvP) %";
                        ShapeTextBox.Text = "3";
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 3;
                        break;
                    case SocketType.MeleeDamageBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Melee Damage Bonus %";
                        ShapeTextBox.Text = "4";
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 4;
                        break;
                    case SocketType.MagicDamageBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Magic Damage Bonus %";
                        ShapeTextBox.Text = "5";
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 5;
                        break;
                    case SocketType.SpiritualBonus:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = false;
                        rAltPanel.Visible = true;
                        raLbl.Text = "Spirit Damage Bonus %";
                        ShapeTextBox.Text = "6";
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
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
                        foreach (var itemInfo in _selectedItemInfos)
                            itemInfo.Shape = 70;
                        break;
                    #endregion
                    #endregion
                    case SocketType.NONE:
                        specialRunePanel.Visible = false;
                        normalRunePanel.Visible = true;
                        rAltPanel.Visible = false;
                        ShapeTextBox.Text = "255";
                        foreach (var itemInfo in _selectedItemInfos)
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MinAC = usTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MinMAC = usTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MaxMAC = usTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MaxAC = usTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MaxAcRate = bTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].MaxMacRate = bTemp;
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


                        for (int i = 0; i < _selectedItemInfos.Count; i++)
                            _selectedItemInfos[i].Holy = bTemp;
                        break;
                }
            }
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinDC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinDC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinMC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinMC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinSC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinSC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinAC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinAC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MinMAC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MinMAC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HP = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.HP = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].HPrate = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.HPrate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Accuracy = temp;
            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.Accuracy = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxDC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxDC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxMC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxMC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxSC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxSC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxAC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxAC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxMAC = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MaxMAC = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MP = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MP = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MPrate = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.MPrate = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Agility = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.Agility = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Weight = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.Weight = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Durability = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.Durability = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].RequiredAmount = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.RequiredAmount = temp;
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].Accuracy = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].MaxAC = usTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].AttackSpeed = sbTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                    {
                        _selectedItemInfos[i].MinAC = usTemp;
                        _selectedItemInfos[i].MinMAC = usTemp;
                    }
                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].MinMC = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
                        itemInfo.MinMC = bTemp;
                    break;
            }
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].MaxDC = usTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].MaxMAC = usTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].MaxDC = usTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                    {
                        _selectedItemInfos[i].MaxAC = usTemp;
                        _selectedItemInfos[i].MaxMAC = usTemp;
                    }
                    foreach (var itemInfo in _selectedItemInfos)
                    {
                        itemInfo.MaxAC = usTemp;
                        itemInfo.MaxMAC = usTemp;
                    }
                    break;
            }
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].CriticalRate = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].Agility = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].CriticalRate = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].HP = usTemp;
                    foreach (var itemInfo in _selectedItemInfos)
                        itemInfo.HP = usTemp;
                    break;
            }
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].CriticalDamage = bTemp;

                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                        _selectedItemInfos[i].CriticalDamage = bTemp;
                    foreach (var itemInfo in _selectedItemInfos)
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


                    for (int i = 0; i < _selectedItemInfos.Count; i++)
                    {
                        _selectedItemInfos[i].Agility = bTemp;
                    }
                    foreach (var itemInfo in _selectedItemInfos)
                        itemInfo.Agility = bTemp;
                    break;
            }
        }

        private void posElementsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].PositiveElement = (ElementPos)posElementsBox.SelectedItem;
        }

        private void negElementBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].NegativeElement = (ElementNeg)negElementBox.SelectedItem;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].PositiveElementAmount = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.PositiveElementAmount = temp;
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


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].NegativeElementAmount = temp;

            foreach (var itemInfo in _selectedItemInfos)
                itemInfo.NegativeElementAmount = temp;
        }

        private void ItemNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Tab)
                RefreshItemList();
        }

        #region Gem Stat Boxes
        private void baseratetextBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].BaseRate = temp;
        }

        private void baseratedroptextBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].BaseRateDrop = temp;
        }

        private void maxstatstextBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxStats = temp;
        }

        private void maxgemstattextBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].MaxGemStat = temp;
        }

        private void socketAddBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].SocketAdd = temp;
        }
        #endregion

        #region Weapon Shape Change
        private void imageBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[0] = temp;
        }

        private void imageBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[1] = temp;
        }

        private void imageBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[2] = temp;
        }

        private void imageBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[3] = temp;
        }

        private void imageBox5_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[4] = temp;
        }
        
        private void imageBox6_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[5] = temp;
        }
        private void imageBox7_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[6] = temp;
        }


        private void imageBox8_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[7] = temp;
        }
        private void imageBox9_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[8] = temp;
        }

        private void imageBox10_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapLooks[9] = temp;
        }
        #endregion

        #region Armour Shape Change
        private void armimageBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[0] = temp;
        }

        private void armimageBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[1] = temp;
        }

        private void armimageBox3_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[2] = temp;
        }

        private void armimageBox4_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[3] = temp;
        }

        private void armimageBox5_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[4] = temp;
        }

        private void armimageBox6_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[5] = temp;
        }

        private void armimageBox7_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[6] = temp;
        }

        private void armimageBox8_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[7] = temp;
        }

        private void armimageBox9_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[8] = temp;
        }

        private void armimageBox10_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourLooks[9] = temp;

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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[0] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[1] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[2] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[3] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[4] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[5] = temp;

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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[6] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[7] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[8] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemLooks[9] = temp;
        }
        #endregion

        #region Item Level Weapon Effects
        //  Weapon Effect Lv1
        private void effectBox40_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[0] = temp;
        }
        //  Weapon Effect Lv2
        private void effectBox41_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[1] = temp;
        }
        //  Weapon Effect Lv3
        private void effectBox42_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[2] = temp;
        }
        //  Weapon Effect Lv4
        private void effectBox43_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[3] = temp;
        }
        //  Weapon Effect Lv5
        private void effectBox44_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[4] = temp;
        }
        //  Weapon Effect Lv6
        private void effectBox45_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[5] = temp;
        }
        //  Weapon Effect Lv7
        private void effectBox46_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[6] = temp;
        }
        //  Weapon Effect Lv8
        private void effectBox47_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[7] = temp;
        }
        //  Weapon Effect Lv9
        private void effectBox48_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[8] = temp;
        }
        //  Weapon Effect Lv10
        private void effectBox49_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Weapon)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelWeapEffect[9] = temp;
        }
        #endregion

        #region Item Level Armour Effects
        //  Armour Effect Lv1
        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[0] = temp;
        }
        //  Armour Effect Lv2
        private void textBox51_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[1] = temp;
        }
        //  Armour Effect Lv3
        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[2] = temp;
        }
        //  Armour Effect Lv4
        private void textBox53_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[3] = temp;
        }
        //  Armour Effect Lv5
        private void textBox54_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[4] = temp;
        }
        //  Armour Effect Lv6
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[5] = temp;
        }
        //  Armour Effect Lv7
        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[6] = temp;
        }
        //  Armour Effect Lv8
        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[7] = temp;
        }
        //  Armour Effect Lv9
        private void textBox58_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[8] = temp;
        }
        //  Armour Effect Lv10
        private void textBox59_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                if (_selectedItemInfos[i].Type != ItemType.Armour)
                    return;
            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelArmourEffect[9] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[0] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[1] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[2] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[3] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[4] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[5] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[6] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[7] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[8] = temp;
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

            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].LevelItemGlow[9] = temp;
        }
        #endregion

        private void noDisassemble_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind =
                    (noDisassemble.Checked ? _selectedItemInfos[i].Bind |= BindMode.UnableToDisassemble : _selectedItemInfos[i].Bind ^= BindMode.UnableToDisassemble);
        }

        private void noMailBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedItemInfos.Count; i++)
                _selectedItemInfos[i].Bind =
                    (noMailBox.Checked ? _selectedItemInfos[i].Bind |= BindMode.NoMail : _selectedItemInfos[i].Bind ^= BindMode.NoMail);
        }
    }
}
