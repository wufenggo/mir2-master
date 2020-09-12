using Server.MirEnvir;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Server.MirForms.Systems
{
    public partial class HeroConfigForm : Form
    {
        public Envir Envir
        {
            get { return SMain.EditEnvir; }
        }
        public HeroConfig config;
        private ListViewItem selectedExp = null;
        public HeroConfigForm()
        {
            InitializeComponent();
            Text = "Horo Confiuration v0.01\t\tCreated by Edens-Elite[Pete107]";
            config = Envir.heroConfig;

            UpdateForm();
        }

        void UpdateForm()
        {

            textBox1.Text = config.RequiredGold.ToString();
            textBox2.Text = config.RequiredLevel.ToString();
            ExpShareBox.Text = config.ExpShare.ToString();

            MasterAttackRangeBox.Text = config.MasterAttackRange.ToString();

            BagLock1.Text = config.BagLock1.ToString();
            BagLock2.Text = config.BagLock2.ToString();
            BagLock3.Text = config.BagLock3.ToString();
            BagLock4.Text = config.BagLock4.ToString();

            AllowSinBox.Checked = config.allowSin;
            AllowArcBox.Checked = config.allowArc;

            NoWhiteBox.Checked = config.NoWhite;
            NoBrownBox.Checked = config.NoBrown;
            NoYellowBox.Checked = config.NoYellow;
            NoRedBox.Checked = config.NoRed;

            NoMasterBox.Checked = config.NoMaster;
            NoHeroBox.Checked = config.NoHero;
            NoUnderLevelBox.Checked = config.NoUnderLevel;

            AllowMasterSkeleton.Checked = config.allowMasterSkeleton;
            AllowMasterSinshu.Checked = config.allowMasterSinshu;
            AllowMasterDeva.Checked = config.AllowMasterDeva;

            AllowDeathDropBox.Checked = config.allowDeathDrop;
            AllowInventoryDeathDropBox.Checked = config.allowInvetoryDeathDrop;

            for (int i = 0; i < config.LevelCaps.Length; i++)
                comboBox1.Items.Add(i);

            for (int i = 0; i < config.HeroExpRequired.Length; i++)
            {
                var ListItem = new ListViewItem((i + 1).ToString()) { Tag = this };

                ListItem.SubItems.Add(config.HeroExpRequired[i].ToString());

                HeroExpList.Items.Add(ListItem);
            }

            comboBox1.SelectedIndex = 0;
            textBox3.Text = config.LevelCaps[comboBox1.SelectedIndex].ToString();

            MaxHpBox.Text = config.MaxHpCaps[comboBox1.SelectedIndex].ToString();
            MaxMpBox.Text = config.MaxMpCap[comboBox1.SelectedIndex].ToString();

            DropRateBox.Text = config.DropRateCaps[comboBox1.SelectedIndex].ToString();
            ExpRateBox.Text = config.ExpRateCaps[comboBox1.SelectedIndex].ToString();

            HpRecoverBox.Text = config.HpRegenCaps[comboBox1.SelectedIndex].ToString();
            MpRecoverBox.Text = config.MpRegenCaps[comboBox1.SelectedIndex].ToString();

            AgilityBox.Text = config.AgilityCaps[comboBox1.SelectedIndex].ToString();
            AccuracyBox.Text = config.AccuracyCaps[comboBox1.SelectedIndex].ToString();
        }

        private void UpdateExpList()
        {
            for (int i = 0; i < HeroExpList.Items.Count; i++)
            {
                HeroExpList.Items[i].SubItems[1].Text = config.HeroExpRequired[i].ToString();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!long.TryParse(textBox1.Text, out long temp))
            {
                textBox1.BackColor = Color.Red;
                return;
            }
            textBox3.BackColor = Color.White;
            Envir.heroConfig.RequiredGold = temp;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(textBox2.Text, out int temp))
            {
                textBox2.BackColor = Color.Red;
                return;
            }
            textBox3.BackColor = Color.White;
            Envir.heroConfig.RequiredLevel = temp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            textBox3.Text = config.LevelCaps[comboBox1.SelectedIndex].ToString();

            MaxHpBox.Text = config.MaxHpCaps[comboBox1.SelectedIndex].ToString();
            MaxMpBox.Text = config.MaxMpCap[comboBox1.SelectedIndex].ToString();

            DropRateBox.Text = config.DropRateCaps[comboBox1.SelectedIndex].ToString();
            ExpRateBox.Text = config.ExpRateCaps[comboBox1.SelectedIndex].ToString();

            HpRecoverBox.Text = config.HpRegenCaps[comboBox1.SelectedIndex].ToString();
            MpRecoverBox.Text = config.MpRegenCaps[comboBox1.SelectedIndex].ToString();

            AgilityBox.Text = config.AgilityCaps[comboBox1.SelectedIndex].ToString();
            AccuracyBox.Text = config.AccuracyCaps[comboBox1.SelectedIndex].ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int temp))
            {
                textBox3.BackColor = Color.Red;
                return;
            }
            textBox3.BackColor = Color.White;
            config.LevelCaps[comboBox1.SelectedIndex] = temp;
        }

        private void HeroConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.SaveHeroConfig(config);
        }

        private void HeroConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void MaxHpBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(MaxHpBox.Text, out int temp))
            {
                MaxHpBox.BackColor = Color.Red;
                return;
            }
            MaxHpBox.BackColor = Color.White;
            config.MaxHpCaps[comboBox1.SelectedIndex] = temp;
        }

        private void MaxMpBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(MaxMpBox.Text, out int temp))
            {
                MaxMpBox.BackColor = Color.Red;
                return;
            }
            MaxMpBox.BackColor = Color.White;
            config.MaxMpCap[comboBox1.SelectedIndex] = temp;
        }

        private void ExpRateBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(ExpRateBox.Text, out int temp))
            {
                ExpRateBox.BackColor = Color.Red;
                return;
            }
            ExpRateBox.BackColor = Color.White;
            config.ExpRateCaps[comboBox1.SelectedIndex] = temp;
        }

        private void DropRateBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(DropRateBox.Text, out int temp))
            {
                DropRateBox.BackColor = Color.Red;
                return;
            }
            DropRateBox.BackColor = Color.White;
            config.DropRateCaps[comboBox1.SelectedIndex] = temp;
        }

        private void HpRecoverBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(HpRecoverBox.Text, out int temp))
            {
                HpRecoverBox.BackColor = Color.Red;
                return;
            }
            HpRecoverBox.BackColor = Color.White;
            config.HpRegenCaps[comboBox1.SelectedIndex] = temp;
        }

        private void MpRecoverBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(MpRecoverBox.Text, out int temp))
            {
                MpRecoverBox.BackColor = Color.Red;
                return;
            }
            MpRecoverBox.BackColor = Color.White;
            config.MpRegenCaps[comboBox1.SelectedIndex] = temp;
        }

        private void AccuracyBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(AccuracyBox.Text, out int temp))
            {
                AccuracyBox.BackColor = Color.Red;
                return;
            }
            AccuracyBox.BackColor = Color.White;
            config.AccuracyCaps[comboBox1.SelectedIndex] = temp;
        }

        private void AgilityBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(AgilityBox.Text, out int temp))
            {
                AgilityBox.BackColor = Color.Red;
                return;
            }
            AgilityBox.BackColor = Color.White;
            config.AgilityCaps[comboBox1.SelectedIndex] = temp;
        }

        private void ExpShareBox_TextChanged(object sender, EventArgs e)
        {
            if (!byte.TryParse(ExpShareBox.Text, out byte temp))
            {
                ExpShareBox.BackColor = Color.Red;
                return;
            }
            ExpShareBox.BackColor = Color.White;
            Envir.heroConfig.ExpShare = temp;
        }

        private void AllowSinBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowSin = AllowSinBox.Checked;
        }

        private void AllowArcBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowArc = AllowArcBox.Checked;
        }

        private void AllowDeathDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowDeathDrop = AllowDeathDropBox.Checked;
        }

        private void AllowInventoryDeathDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowInvetoryDeathDrop = AllowInventoryDeathDropBox.Checked;
        }

        private void HeroExpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HeroExpList.SelectedItems != null && HeroExpList.SelectedItems.Count > 0)
            {
                selectedExp = HeroExpList.SelectedItems[0];
                ExpBox.Text = HeroExpList.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void ExpBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedExp == null) return;

            if (!uint.TryParse(ExpBox.Text, out uint temp))
            {
                ExpBox.BackColor = Color.Red;
                return;
            }
            ExpBox.BackColor = Color.White;
            Envir.heroConfig.HeroExpRequired[selectedExp.Index] = temp;

            UpdateExpList();
        }

        private void BagLock1_TextChanged(object sender, EventArgs e)
        {
            if (!byte.TryParse(BagLock1.Text, out byte temp))
            {
                BagLock1.BackColor = Color.Red;
                return;
            }
            BagLock1.BackColor = Color.White;
            Envir.heroConfig.BagLock1 = temp;
        }

        private void BagLock2_TextChanged(object sender, EventArgs e)
        {
            {
                if (!byte.TryParse(BagLock2.Text, out byte temp))
                {
                    BagLock2.BackColor = Color.Red;
                    return;
                }
                BagLock2.BackColor = Color.White;
                Envir.heroConfig.BagLock2 = temp;
            }
        }

        private void BagLock3_TextChanged(object sender, EventArgs e)
        {
            {
                if (!byte.TryParse(BagLock3.Text, out byte temp))
                {
                    BagLock3.BackColor = Color.Red;
                    return;
                }
                BagLock3.BackColor = Color.White;
                Envir.heroConfig.BagLock3 = temp;
            }
        }
        
        private void BagLock4_TextChanged(object sender, EventArgs e)
        {
            {
                if (!byte.TryParse(BagLock4.Text, out byte temp))
                {
                    BagLock4.BackColor = Color.Red;
                    return;
                }
                BagLock4.BackColor = Color.White;
                Envir.heroConfig.BagLock4 = temp;
            }
        }

        private void AllowMasterSkeleton_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowMasterSkeleton = AllowMasterSkeleton.Checked;
        }

        private void AllowMasterSinshu_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.allowMasterSinshu = AllowMasterSinshu.Checked;
        }

        private void AllowMasterDeva_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.AllowMasterDeva = AllowMasterDeva.Checked;
        }

        private void NoWhiteBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoWhite = NoWhiteBox.Checked;
        }

        private void NoBrownBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoBrown = NoBrownBox.Checked;
        }

        private void NoYellowBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoYellow = NoYellowBox.Checked;
        }

        private void NoRedBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoRed = NoRedBox.Checked;
        }

        private void NoMasterBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoMaster = NoMasterBox.Checked;
        }

        private void NoHeroBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoHero = NoHeroBox.Checked;
        }

        private void NoUnderLevelBox_CheckedChanged(object sender, EventArgs e)
        {
            Envir.heroConfig.NoUnderLevel = NoUnderLevelBox.Checked;
        }

        private void MasterAttackRangeBox_TextChanged(object sender, EventArgs e)
        {
            if (!byte.TryParse(MasterAttackRangeBox.Text, out byte temp))
            {
                MasterAttackRangeBox.BackColor = Color.Red;
                return;
            }
            MasterAttackRangeBox.BackColor = Color.White;
            Envir.heroConfig.MasterAttackRange = temp;
        }
    }

    public class HeroConfig
    {
        public int RequiredLevel = 22;
        public long RequiredGold = 2000000;
        public byte ExpShare = 20;
        public byte MasterAttackRange = 20;
        public bool allowSin, allowArc;
        public bool allowDeathDrop, allowInvetoryDeathDrop;
        public bool allowMasterSkeleton, allowMasterSinshu, AllowMasterDeva;

        public bool NoWhite, NoBrown, NoRed, NoYellow;
        public bool NoMaster, NoHero, NoUnderLevel;

        public byte BagLock1 = 10, BagLock2 = 20, BagLock3 = 30, BagLock4 = 40;

        public int[] LevelCaps = new int[10];
        public int[] MaxMpCap = new int[10];
        public int[] MaxHpCaps = new int[10];
        public int[] ExpRateCaps = new int[10];
        public int[] DropRateCaps = new int[10];
        public int[] HpRegenCaps = new int[10];
        public int[] MpRegenCaps = new int[10];
        public int[] AgilityCaps = new int[10];
        public int[] AccuracyCaps = new int[10];
        public uint[] HeroExpRequired = new uint[byte.MaxValue];

        public void SaveHeroConfig(HeroConfig info)
        {
            using (FileStream stream = new FileStream(@".\HeroConfig.info", FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(info.RequiredLevel);
                    writer.Write(info.RequiredGold);

                    for (int i = 0; i < LevelCaps.Length; i++)
                        writer.Write(info.LevelCaps[i]);

                    for (int i = 0; i < MaxMpCap.Length; i++)
                        writer.Write(info.MaxMpCap[i]);

                    for (int i = 0; i < MaxHpCaps.Length; i++)
                        writer.Write(info.MaxHpCaps[i]);

                    for (int i = 0; i < ExpRateCaps.Length; i++)
                        writer.Write(info.ExpRateCaps[i]);

                    for (int i = 0; i < DropRateCaps.Length; i++)
                        writer.Write(info.DropRateCaps[i]);

                    for (int i = 0; i < HpRegenCaps.Length; i++)
                        writer.Write(info.HpRegenCaps[i]);

                    for (int i = 0; i < MpRegenCaps.Length; i++)
                        writer.Write(info.MpRegenCaps[i]);

                    for (int i = 0; i < AgilityCaps.Length; i++)
                        writer.Write(info.AgilityCaps[i]);

                    for (int i = 0; i < AccuracyCaps.Length; i++)
                        writer.Write(info.AccuracyCaps[i]);

                    writer.Write(info.ExpShare);
                    writer.Write(info.allowSin);
                    writer.Write(info.allowArc);

                    writer.Write(info.allowDeathDrop);
                    writer.Write(info.allowInvetoryDeathDrop);

                    for(int i = 0; i < HeroExpRequired.Length; i++)
                        writer.Write(info.HeroExpRequired[i]);

                    writer.Write(info.BagLock1);
                    writer.Write(info.BagLock2);
                    writer.Write(info.BagLock3);
                    writer.Write(info.BagLock4);

                    writer.Write(info.allowMasterSkeleton);
                    writer.Write(info.allowMasterSinshu);
                    writer.Write(info.AllowMasterDeva);

                    writer.Write(info.NoWhite);
                    writer.Write(info.NoBrown);
                    writer.Write(info.NoYellow);
                    writer.Write(info.NoRed);

                    writer.Write(info.NoMaster);
                    writer.Write(info.NoHero);
                    writer.Write(info.NoUnderLevel);

                    writer.Write(info.MasterAttackRange);
                }
            }
        }

        public HeroConfig()
        {
            if (!File.Exists(@".\HeroConfig.info"))
            {
                SaveHeroConfig(this);
                return;
            }
            using (FileStream stream = new FileStream(@".\HeroConfig.info", FileMode.OpenOrCreate))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    RequiredLevel = reader.ReadInt32();
                    RequiredGold = reader.ReadInt64();

                    if (reader.PeekChar() == -1) return;

                    for (int i = 0; i < LevelCaps.Length; i++)
                        LevelCaps[i] = reader.ReadInt32();

                    if (reader.PeekChar() == -1) return;

                    for (int i = 0; i < MaxMpCap.Length; i++)
                        MaxMpCap[i] = reader.ReadInt32();

                    for (int i = 0; i < MaxHpCaps.Length; i++)
                        MaxHpCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < ExpRateCaps.Length; i++)
                        ExpRateCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < DropRateCaps.Length; i++)
                        DropRateCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < HpRegenCaps.Length; i++)
                        HpRegenCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < MpRegenCaps.Length; i++)
                        MpRegenCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < AgilityCaps.Length; i++)
                        AgilityCaps[i] = reader.ReadInt32();

                    for (int i = 0; i < AccuracyCaps.Length; i++)
                        AccuracyCaps[i] = reader.ReadInt32();


                    if (reader.PeekChar() == -1) return;
                    ExpShare = reader.ReadByte();

                    if (reader.PeekChar() == -1) return;
                    allowSin = reader.ReadBoolean();
                    allowArc = reader.ReadBoolean();

                    if (reader.PeekChar() == -1) return;
                    allowDeathDrop = reader.ReadBoolean();
                    allowInvetoryDeathDrop = reader.ReadBoolean();

                    if (reader.PeekChar() == -1) return;
                    for (int i = 0; i < HeroExpRequired.Length; i++)
                        HeroExpRequired[i] = reader.ReadUInt32();

                    if (reader.PeekChar() == -1) return;
                    BagLock1 = reader.ReadByte();
                    BagLock2 = reader.ReadByte();
                    BagLock3 = reader.ReadByte();
                    BagLock4 = reader.ReadByte();

                    if (reader.PeekChar() == -1) return;
                    allowMasterSkeleton = reader.ReadBoolean();
                    allowMasterSinshu = reader.ReadBoolean();
                    AllowMasterDeva = reader.ReadBoolean();

                    if (reader.PeekChar() == -1) return;
                    NoWhite = reader.ReadBoolean();
                    NoBrown = reader.ReadBoolean();
                    NoYellow = reader.ReadBoolean();
                    NoRed = reader.ReadBoolean();

                    NoMaster = reader.ReadBoolean();
                    NoHero = reader.ReadBoolean();
                    NoUnderLevel = reader.ReadBoolean();

                    if (reader.PeekChar() == -1) return;
                    MasterAttackRange = reader.ReadByte();

                }
            }
        }
    }
}
