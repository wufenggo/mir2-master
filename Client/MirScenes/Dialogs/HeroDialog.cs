using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public class HeroCreationDialog : MirImageControl
    {
        private static readonly Regex Reg = new Regex(@"^[A-Za-z0-9]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");
        public MirImageControl Background, Title;
        public MirImageControl TitleLabel;
        public MirAnimatedControl CharacterDisplay;

        public MirButton OKButton, CancelButton, WarriorButton, WizardButton, TaoistButton, AssassinButton, ArcherButton, MaleButton, FemaleButton;

        public MirTextBox NameTextBox;

        public MirLabel Description;

        private MirClass _class;
        private MirGender _gender;

        #region Descriptions
        public const string WarriorDescription =
                "Warriors are a class of great strength and vitality. They are not easily killed in battle and have the advantage of being able to use" +
                " a variety of heavy weapons and Armour. Therefore, Warriors favor attacks that are based on melee physical damage. They are weak in ranged" +
                " attacks, however the variety of equipment that are developed specifically for Warriors complement their weakness in ranged combat.";

        public const string WizardDescription =
                "Wizards are a class of low strength and stamina, but have the ability to use powerful spells. Their offensive spells are very effective, but" +
                " because it takes time to cast these spells, they're likely to leave themselves open for enemy's attacks. Therefore, the physically weak wizards" +
                " must aim to attack their enemies from a safe distance.";

        public const string TaoistDescription =
                "Taoists are well disciplined in the study of Astronomy, Medicine, and others aside from Mu-Gong. Rather then directly engaging the enemies, their" +
                " specialty lies in assisting their allies with support. Taoists can summon powerful creatures and have a high resistance to magic, and is a class" +
                " with well balanced offensive and defensive abilities.";

        public const string AssassinDescription =
                "Assassins are members of a secret organization and their history is relatively unknown. They're capable of hiding themselves and performing attacks" +
                " while being unseen by others, which naturally makes them excellent at making fast kills. It is necessary for them to avoid being in battles with" +
                " multiple enemies due to their weak vitality and strength.";

        public const string ArcherDescription =
                "Archers are a class of great accuracy and strength, using their powerful skills with bows to deal extraordinary damage from range. Much like" +
                " wizards, they rely on their keen instincts to dodge oncoming attacks as they tend to leave themselves open to frontal attacks. However, their" +
                " physical prowess and deadly aim allows them to instil fear into anyone they hit.";


        public void Show()
        {
            Reset();
            if (!Visible)
                Visible = true;

            BringToFront();
            NameTextBox.SetFocus();
        }


        public void Hide()
        {
            if (Visible)
                Visible = false;
        }
        #endregion

        #region HeroCreationDialog

        public HeroCreationDialog()
        {
            Index = 73;
            Library = Libraries.Prguse;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            Modal = true;

            TitleLabel = new MirImageControl
            {
                Index = 34,
                Library = Libraries.Title,
                Location = new Point(206, 11),
                Parent = this,
            };

            CancelButton = new MirButton
            {
                HoverIndex = 281,
                Index = 280,
                Library = Libraries.Title,
                Location = new Point(425, 425),
                Parent = this,
                PressedIndex = 282
            };
            CancelButton.Click += (o, e) => Hide();


            OKButton = new MirButton
            {
                Enabled = false,
                ForeColour = Color.Gray,
                HoverIndex = 361,
                Index = 360,
                Library = Libraries.Title,
                Location = new Point(160, 425),
                Parent = this,
                PressedIndex = 362,
            };
            OKButton.Click += (o, e) => CreateCharacter();

            NameTextBox = new MirTextBox
            {
                Location = new Point(325, 268),
                Parent = this,
                Size = new Size(240, 20),
                MaxLength = Globals.MaxCharacterNameLength
            };
            NameTextBox.TextBox.KeyPress += TextBox_KeyPress;
            NameTextBox.TextBox.TextChanged += CharacterNameTextBox_TextChanged;
            NameTextBox.SetFocus();

            CharacterDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 250,
                Index = 20,
                Library = Libraries.ChrSel,
                Location = new Point(120, 250),
                Parent = this,
                UseOffSet = true,
            };
            CharacterDisplay.AfterDraw += (o, e) =>
            {
                if (_class == MirClass.Wizard)
                    Libraries.ChrSel.DrawBlend(CharacterDisplay.Index + 560, CharacterDisplay.DisplayLocationWithoutOffSet, Color.White, true);
            };


            WarriorButton = new MirButton
            {
                HoverIndex = 2427,
                Index = 2427,
                Library = Libraries.Prguse,
                Location = new Point(323, 296),
                Parent = this,
                PressedIndex = 2428,
                Sound = SoundList.ButtonA,
            };
            WarriorButton.Click += (o, e) =>
            {
                _class = MirClass.Warrior;
                UpdateInterface();
            };


            WizardButton = new MirButton
            {
                HoverIndex = 2430,
                Index = 2429,
                Library = Libraries.Prguse,
                Location = new Point(373, 296),
                Parent = this,
                PressedIndex = 2431,
                Sound = SoundList.ButtonA,
            };
            WizardButton.Click += (o, e) =>
            {
                _class = MirClass.Wizard;
                UpdateInterface();
            };


            TaoistButton = new MirButton
            {
                HoverIndex = 2433,
                Index = 2432,
                Library = Libraries.Prguse,
                Location = new Point(423, 296),
                Parent = this,
                PressedIndex = 2434,
                Sound = SoundList.ButtonA,
            };
            TaoistButton.Click += (o, e) =>
            {
                _class = MirClass.Taoist;
                UpdateInterface();
            };

            AssassinButton = new MirButton
            {
                HoverIndex = 2436,
                Index = 2435,
                Library = Libraries.Prguse,
                Location = new Point(473, 296),
                Parent = this,
                PressedIndex = 2437,
                Sound = SoundList.ButtonA,
            };
            AssassinButton.Click += (o, e) =>
            {
                _class = MirClass.Assassin;
                UpdateInterface();
            };

            ArcherButton = new MirButton
            {
                HoverIndex = 2439,
                Index = 2438,
                Library = Libraries.Prguse,
                Location = new Point(523, 296),
                Parent = this,
                PressedIndex = 2440,
                Sound = SoundList.ButtonA,
            };
            ArcherButton.Click += (o, e) =>
            {
                _class = MirClass.Archer;
                UpdateInterface();
            };


            MaleButton = new MirButton
            {
                HoverIndex = 2421,
                Index = 2421,
                Library = Libraries.Prguse,
                Location = new Point(323, 343),
                Parent = this,
                PressedIndex = 2422,
                Sound = SoundList.ButtonA,
            };
            MaleButton.Click += (o, e) =>
            {
                _gender = MirGender.Male;
                UpdateInterface();
            };

            FemaleButton = new MirButton
            {
                HoverIndex = 2424,
                Index = 2423,
                Library = Libraries.Prguse,
                Location = new Point(373, 343),
                Parent = this,
                PressedIndex = 2425,
                Sound = SoundList.ButtonA,
            };
            FemaleButton.Click += (o, e) =>
            {
                _gender = MirGender.Female;
                UpdateInterface();
            };

            Description = new MirLabel
            {
                Border = true,
                Location = new Point(279, 70),
                Parent = this,
                Size = new Size(278, 170),
                Text = WarriorDescription,
            };
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null)
                return;
            if (e.KeyChar != (char)Keys.Enter)
                return;
            e.Handled = true;

            if (OKButton.Enabled)
                OKButton.InvokeMouseClick(null);
        }
        private void CharacterNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                OKButton.Enabled = false;
                OKButton.ForeColour = Color.Gray;
                NameTextBox.Border = false;
            }
            else if (!Reg.IsMatch(NameTextBox.Text))
            {
                OKButton.Enabled = false;
                NameTextBox.Border = true;
                OKButton.ForeColour = Color.Gray;
                NameTextBox.BorderColour = Color.Red;
            }
            else
            {
                OKButton.Enabled = true;
                NameTextBox.Border = true;
                OKButton.ForeColour = Color.White;
                NameTextBox.BorderColour = Color.Green;
            }
        }

        private void CreateCharacter()
        {

            OKButton.Enabled = false;

            Network.Enqueue(new C.HeroCreation
            {
                Name = NameTextBox.Text,
                Class = _class,
                Gender = _gender,
                Hair = (byte)CMain.Random.Next(0, 3),
            });

            Hide();
        }

        private void Reset()
        {
            NameTextBox.Text = string.Empty;
            _class = MirClass.Warrior;
            _gender = MirGender.Male;

            UpdateInterface();
        }



        private void UpdateInterface()
        {
            MaleButton.Index = 2420;
            FemaleButton.Index = 2423;

            WarriorButton.Index = 2426;
            WizardButton.Index = 2429;
            TaoistButton.Index = 2432;
            AssassinButton.Index = 2435;
            ArcherButton.Index = 2438;

            switch (_gender)
            {
                case MirGender.Male:
                    MaleButton.Index = 2421;
                    break;
                case MirGender.Female:
                    FemaleButton.Index = 2424;
                    break;
            }

            switch (_class)
            {
                case MirClass.Warrior:
                    WarriorButton.Index = 2427;
                    Description.Text = WarriorDescription;
                    CharacterDisplay.Index = (byte)_gender == 0 ? 20 : 300; //220 : 500;
                    break;
                case MirClass.Wizard:
                    WizardButton.Index = 2430;
                    Description.Text = WizardDescription;
                    CharacterDisplay.Index = (byte)_gender == 0 ? 40 : 320; //240 : 520;
                    break;
                case MirClass.Taoist:
                    TaoistButton.Index = 2433;
                    Description.Text = TaoistDescription;
                    CharacterDisplay.Index = (byte)_gender == 0 ? 60 : 340; //260 : 540;
                    break;
                case MirClass.Assassin:
                    AssassinButton.Index = 2436;
                    Description.Text = AssassinDescription;
                    CharacterDisplay.Index = (byte)_gender == 0 ? 80 : 360; //280 : 560;
                    break;
                case MirClass.Archer:
                    ArcherButton.Index = 2439;
                    Description.Text = ArcherDescription;
                    CharacterDisplay.Index = (byte)_gender == 0 ? 100 : 140; //160 : 180;
                    break;
            }

            //CharacterDisplay.Index = ((byte)_class + 1) * 20 + (byte)_gender * 280;
        }
    }
    public sealed class CharacterButton : MirImageControl
    {
        public MirLabel NameLabel, LevelLabel, ClassLabel;
        public bool Selected;

        public CharacterButton()
        {
            Index = 44;
            Library = Libraries.Prguse;
            Sound = SoundList.ButtonA;

            NameLabel = new MirLabel
            {
                Location = new Point(107, 9),
                Parent = this,
                NotControl = true,
                Size = new Size(170, 18)
            };

            LevelLabel = new MirLabel
            {
                Location = new Point(107, 28),
                Parent = this,
                NotControl = true,
                Size = new Size(30, 18)
            };

            ClassLabel = new MirLabel
            {
                Location = new Point(178, 28),
                Parent = this,
                NotControl = true,
                Size = new Size(100, 18)
            };
        }

        public void Update(SelectInfo info)
        {
            if (info == null)
            {
                Index = 44;
                Library = Libraries.Prguse;
                NameLabel.Text = string.Empty;
                LevelLabel.Text = string.Empty;
                ClassLabel.Text = string.Empty;

                NameLabel.Visible = false;
                LevelLabel.Visible = false;
                ClassLabel.Visible = false;

                return;
            }

            Library = Libraries.Title;

            Index = 660 + (byte)info.Class;

            if (Selected)
                Index += 5;


            NameLabel.Text = info.Name;
            LevelLabel.Text = info.Level.ToString();
            ClassLabel.Text = info.Class.ToString();

            NameLabel.Visible = true;
            LevelLabel.Visible = true;
            ClassLabel.Visible = true;
        }
    }
    #endregion

        #region HeroBar
    public class HeroDialog : MirImageControl
    {
        public MirButton HeroCharButton,
                         HeroSkillsButton,
                         HeroBagButton;


        public HeroDialog()
        {
            if (Settings.Resolution > 1024)
            {
                Index = 2179;
                Parent = GameScene.Scene.MainDialog;
                Library = Libraries.Prguse;
                Location = new Point(Parent.Size.Width - 701, Parent.Location.Y + 39);  //-12
                Sort = true;
                Visible = false;
                Movable = false; ;
            }
            else
            {
                Index = 2179;
                Parent = GameScene.Scene.MainDialog;
                Library = Libraries.Prguse;
                Location = new Point(Parent.Size.Width - 159, Parent.Location.Y + 12);
                Sort = true;
                Visible = false;
                Movable = false;
            }

            HeroCharButton = new MirButton
            {
                HoverIndex = 2177,
                Index = 2176,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 37),
                PressedIndex = 2178,
                Hint = "HeroCharacter (" + CMain.InputKeys.GetKey(KeybindOptions.HeroEquipment) + ")"
            };
            HeroCharButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HeroCharacterDialog.Visible)
                    GameScene.Scene.HeroCharacterDialog.Hide();
                else
                    GameScene.Scene.HeroCharacterDialog.Show();
            };

            HeroSkillsButton = new MirButton
            {
                HoverIndex = 2174,
                Index = 2173,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 3),
                PressedIndex = 2175,
                Hint = "HeroSkills  (" + CMain.InputKeys.GetKey(KeybindOptions.HeroSkills) + ")"
            };
            HeroSkillsButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HeroCharacterDialog.Visible)
                {
                    if (GameScene.Scene.HeroCharacterDialog.SkillPage.Visible)
                        GameScene.Scene.HeroCharacterDialog.Hide();
                    else
                        GameScene.Scene.HeroCharacterDialog.ShowSkillPage();
                }
                else
                {
                    GameScene.Scene.HeroCharacterDialog.Show();
                    GameScene.Scene.HeroCharacterDialog.ShowSkillPage();
                }
            };

            HeroBagButton = new MirButton
            {
                Index = 2170,
                HoverIndex = 2171,
                PressedIndex = 2172,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 20),
                Hint = "HeroBag  (" + CMain.InputKeys.GetKey(KeybindOptions.HeroInventory) + ")"
            };
            HeroBagButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HeroInventoryDialog.Visible)
                    GameScene.Scene.HeroInventoryDialog.Hide();
                else
                    GameScene.Scene.HeroInventoryDialog.Show();
            };
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }
    }
    #endregion

        #region AutoPot
    public sealed class AutoPotDialog : MirImageControl
    {
        MirButton HPButton, MPButton;
        MirTextBox HpPercent, MpPercent;
        public MirLabel MpCount, HpCount;

        public int HpIndex = -1, MpIndex = -1;


        public AutoPotDialog()
        {
            HPButton = new MirButton()
            {
                Index = -1,
                Library = Libraries.Items,
                Location = new Point(-25, 0),
                Parent = this,
                Size = new Size(35, 35),
                Sound = SoundList.ButtonA,
            };
            HPButton.Click += (e, o) =>
            {
                HpIndex = -1;
                GameScene.Hero.HpInfo = -1;

                RefreshInterface();
            };

            HpCount = new MirLabel()
            {
                AutoSize = true,
                Location = new Point(15, 20),
                Parent = HPButton,
                NotControl = true,
                Text = "00",
                Visible = true,
                ForeColour = Color.Yellow,
            };

            MPButton = new MirButton()
            {
                Index = -1,
                Library = Libraries.Items,
                Location = new Point(20, 0),
                Parent = this,
                Size = new Size(35, 35),
                Sound = SoundList.ButtonA,
            };
            MPButton.Click += (e, o) =>
            {
                MpIndex = -1;
                GameScene.Hero.MpInfo = -1;

                RefreshInterface();
            };

            MpCount = new MirLabel()
            {
                AutoSize = true,
                Location = new Point(15, 20),
                Parent = MPButton,
                NotControl = true,
                Text = "00",
                ForeColour = Color.Yellow,
            };

            HpPercent = new MirTextBox
            {
                Location = new Point(-70, 20),
                Parent = this,
                Size = new Size(40, 20),
                MaxLength = 2,
                CanLoseFocus = true,
            };
            HpPercent.TextBox.KeyPress += HpPercent_KeyPress;
            HpPercent.TextBox.TextChanged += HpPercent_TextChanged;
            HpPercent.TextBox.KeyUp += TextBox_KeyUp;
            HpPercent.TextBox.KeyDown += TextBox_KeyDown;

            MpPercent = new MirTextBox
            {
                Location = new Point(78, 20),
                Parent = this,
                Size = new Size(40, 20),
                MaxLength = 2,
                CanLoseFocus = true,
            };
            MpPercent.TextBox.KeyPress += MpPercent_KeyPress;
            MpPercent.TextBox.TextChanged += MpPercent_TextChanged;
            MpPercent.TextBox.KeyUp += TextBox_KeyUp1;
            MpPercent.TextBox.KeyDown += TextBox_KeyDown1;

        }

        private void TextBox_KeyDown1(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyDown(sender, e);
                    break;

            }
        }

        private void TextBox_KeyUp1(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyUp(sender, e);
                    break;

            }
        }

        private void HpPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null)
                return;
            if (e.KeyChar != (char)Keys.Enter)
                return;
            e.Handled = true;

        }
        private void HpPercent_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HpPercent.Text))
            {
                HpPercent.ForeColour = Color.Red;
                return;
            }


            if (!byte.TryParse(HpPercent.Text, out byte tempbyte))
            {
                HpPercent.ForeColour = Color.Red;
                return;
            }

            if (tempbyte < 10)
            {
                HpPercent.ForeColour = Color.Red;
                return;
            }

            GameScene.Hero.HpRate = tempbyte;
            HpPercent.ForeColour = Color.White;

            UpdateServer();
        }


        private void MpPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null)
                return;
            if (e.KeyChar != (char)Keys.Enter)
                return;
            e.Handled = true;

        }
        private void MpPercent_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MpPercent.Text))
            {
                MpPercent.ForeColour = Color.Red;
                return;
            }


            if (!byte.TryParse(MpPercent.Text, out byte tempbyte))
            {
                MpPercent.ForeColour = Color.Red;
                return;
            }

            if (tempbyte < 10)
            {
                MpPercent.ForeColour = Color.Red;
                return;
            }

            GameScene.Hero.MpRate = tempbyte;
            MpPercent.ForeColour = Color.White;

            UpdateServer();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyUp(sender, e);
                    break;

            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyDown(sender, e);
                    break;

            }
        }

        public void UpdateServer()
        {

            Network.Enqueue(new C.AutoPotUpdate { HpInfo = GameScene.Hero.HpInfo, MpInfo = GameScene.Hero.MpInfo, HpRate = GameScene.Hero.HpRate, MpRate = GameScene.Hero.MpRate });
        }


        public void RefreshInterface()
        {
            HPButton.Index = HpIndex;
            MPButton.Index = MpIndex;

            var hp = CountItem(GameScene.Hero.HpInfo);
            HpCount.Text = hp > 0 ? hp.ToString() : "";
            GameScene.Scene.HeroHeaderDialog.HeroPotLabel.Text = HpCount.Text;

            var mp = CountItem(GameScene.Hero.MpInfo);
            MpCount.Text = mp > 0 ? mp.ToString() : "";
            GameScene.Scene.HeroHeaderDialog.HeroPot2Label.Text = MpCount.Text;

        }

        public uint CountItem(int index)
        {
            uint count = 0;
            foreach (var i in GameScene.Hero.Inventory)
            {
                if (i == null) continue;

                if (i.Info.Index == index)
                    count += i.Count;
            }

            return count;
        }

        public void LoadInterface()
        {
            if (GameScene.Hero.HpInfo != -1)
            {
                var pot1 = GameScene.ItemInfoList.FirstOrDefault(x => x.Index == GameScene.Hero.HpInfo);
                if (pot1 != null)
                {
                    HPButton.Index = pot1.Image;
                    HpIndex = pot1.Image;
                }
            }

            if (GameScene.Hero.MpInfo != -1)
            {
                var pot2 = GameScene.ItemInfoList.FirstOrDefault(x => x.Index == GameScene.Hero.MpInfo);
                if (pot2 != null)
                {
                    MPButton.Index = pot2.Image;
                    MpIndex = pot2.Image;
                }
            }

            HpPercent.Text = GameScene.Hero.HpRate.ToString();
            MpPercent.Text = GameScene.Hero.MpRate.ToString();
        }

    }
    #endregion

        #region Bag
    public sealed class HeroInventoryDialog : MirImageControl
    {
        public MirItemCell[] Grid, QuestGrid;

        public MirButton CloseButton, HeroItemButton2, HeroItemButton, HeroQuestButton, AutoPot1, AutoPot2;
        public MirImageControl[] LockBar = new MirImageControl[4];
        public AutoPotDialog autoPotDialog;

        public HeroInventoryDialog()
        {
            Index = 1422;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;
            Visible = false;
            Parent = GameScene.Scene;
            Location = new Point(0, 230);

            autoPotDialog = new AutoPotDialog()
            {
                Index = -1,
                Library = Libraries.Items,
                Parent = this,
                Location = new Point(150, 213),
                Size = new Size(150, 70),
                Visible = false,
            };

            HeroItemButton = new MirButton
            {
                Index = 802,
                Library = Libraries.Title,
                Location = new Point(56, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            HeroItemButton.Click += (e, o) => ShowBag();

            HeroItemButton2 = new MirButton
            {
                Index = 801,
                Library = Libraries.Title,
                Location = new Point(126, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            //HeroItemButton2.Click += Button_Click;

            HeroQuestButton = new MirButton
            {
                Index = 803,
                Library = Libraries.Title,
                Location = new Point(196, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            HeroQuestButton.Click += (e, o) =>
            {
                ShowQuestBag();
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(299, 2),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            AutoPot1 = new MirButton
            {
                Index = 1428,
                Library = Libraries.Prguse,
                Location = new Point(56, 196),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,

            };

            AutoPot2 = new MirButton
            {
                Index = 1429,
                Library = Libraries.Prguse,
                Location = new Point(164, 196),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };

            Grid = new MirItemCell[4 * 10];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    int idx = 8 * y + x;
                    Grid[idx] = new MirItemCell
                    {
                        ItemSlot = 2 + idx,
                        GridType = MirGridType.HeroInventory,
                        Library = Libraries.Items,
                        Parent = this,
                        Location = new Point(x * 36 + 13 + x, y % 5 * 32 + 22 + y % 5),
                    };

                }
            }

            QuestGrid = new MirItemCell[8 * 5];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    QuestGrid[8 * y + x] = new MirItemCell
                    {
                        ItemSlot = 8 * y + x,
                        GridType = MirGridType.HeroQuestInventory,
                        Library = Libraries.Items,
                        Parent = this,
                        Location = new Point(x * 36 + 13 + x, y % 5 * 32 + 22 + y % 5),
                        Visible = false
                    };
                }
            }

            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i] = new MirImageControl
                {
                    Index = 1423,
                    Library = Libraries.Prguse,
                    Location = new Point(15, 57 + i * 33),
                    Parent = this,
                    DrawImage = true,
                    NotControl = true,
                    Visible = false,
                };
            }

        }


        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }

        public void AutoPotUpdate()
        {
            AutoPot1.Visible = GameScene.Hero.AllowAutoPot ? false : true;
            AutoPot2.Visible = GameScene.Hero.AllowAutoPot ? false : true;

            autoPotDialog.Visible = GameScene.Hero.AllowAutoPot ? true : false;
            autoPotDialog.Enabled = GameScene.Hero.AllowAutoPot ? true : false;

            autoPotDialog.RefreshInterface();
        }


        public void Show()
        {
            Visible = true;
            ShowBag();
            AutoPotUpdate();
        }

        public void ShowBag()
        {
            RefreshInventory();
            foreach (var v in Grid)
            {
                v.Visible = true;
            }
        }

        public void RefreshInventory()
        {
            Reset();

            for (int i = 0; i < Grid.Length; i++)
            {
                var grid = Grid[i];
                if (i < GameScene.Hero.Inventory.Length)
                    grid.Enabled = true;
                else
                    grid.Enabled = false;
            }

            int openLevel = (GameScene.Hero.Inventory.Length - 10) / 8;
            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i].Visible = (i < openLevel) ? false : true;
            }

        }

        public void ShowQuestBag()
        {
            Reset();
            foreach (var v in QuestGrid)
            {
                v.Visible = true;
            }
        }

        public void Reset()
        {
            foreach (var v in Grid)
            {
                v.Visible = false;
            }

            foreach (var v in QuestGrid)
            {
                v.Visible = false;
            }

            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i].Visible = false;
            }

        }

        public void Hide()
        {
            Visible = false;
        }
    }
    #endregion

       #region Character
    public sealed class HeroCharacterDialog : MirImageControl
    {
        public MirImageControl CharacterPage, StatusPage, StatePage, SkillPage, ClassImage;
        public MirButton NextButton, BackButton, CloseButton, CharacterButton, StatusButton, StateButton, SkillButton;
        public MirLabel HeroName, MasterName;

        public MapObject Hero;

        public MirLabel ACLabel, MACLabel, DCLabel, MCLabel, SCLabel, HealthLabel, ManaLabel;
        public MirLabel CritRLabel, CritDLabel, LuckLabel, AttkSpdLabel, AccLabel, AgilLabel;
        public MirLabel ExpPLabel, BagWLabel, WearWLabel, HandWLabel, MagicRLabel, PoisonRecLabel, HealthRLabel, ManaRLabel, PoisonResLabel, HolyTLabel, FreezeLabel, PoisonAtkLabel;

        public MirItemCell[] Grid;
        public HeroMagicButton[] Magics;

        public int StartIndex;

        public int weaponGlow = 0;
        public long glowCooldown = 0;

        public HeroCharacterDialog()
        {
            Index = 509;
            Library = Libraries.CustomTitle;
            Location = new Point(Settings.ScreenWidth - 526, 0);
            Movable = true;
            Sort = true;
            Parent = GameScene.Scene;
            BeforeDraw += (o, e) =>
            {

                HeroName = new MirLabel
                {
                    AutoSize = true,
                    Parent = this,
                    Location = new Point(114, 20),
                    NotControl = true,
                    Text = GameScene.Hero.Name,

                };
            };

            ClassImage = new MirImageControl
            {
                Index = 100,
                Library = Libraries.CustomPrguse,
                Location = new Point(27, 40),
                Parent = this,
                NotControl = true,
            };

            CharacterPage = new MirImageControl
            {
                Index = 340,
                Parent = this,
                Library = Libraries.CustomPrguse,
                Location = new Point(8, 90),
            };
            CharacterPage.AfterDraw += (o, e) =>
            {
                if (Libraries.StateItems == null) return;
                ItemInfo RealItem = null;

                if (Grid[(int)EquipmentSlot.Armour].Item != null)
                {
                    RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Armour].Item.Info, GameScene.Hero.Level, GameScene.Hero.Class, GameScene.ItemInfoList);
                    if (RealItem.Effect > 0)
                    {
                        int _startIndex = -1;
                        _startIndex = RealItem.Effect >= 1 && RealItem.Effect <= 2 && GameScene.Hero.Gender == MirGender.Male ? 0 : 1;
                        if (_startIndex == -1 && RealItem.Effect > 1)
                            _startIndex = GameScene.Hero.Gender == MirGender.Male ? 0 : 10;
                        Libraries.WingEffectLibrary[RealItem.Effect].DrawBlend(_startIndex, DisplayLocation, Color.White, true, 1F);
                    }
                    Libraries.StateItems.Draw(RealItem.Image, DisplayLocation, Color.White, true, 1F);



                }
                if (Grid[(int)EquipmentSlot.Weapon].Item != null)
                {
                    RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Weapon].Item.Info, GameScene.Hero.Level, GameScene.Hero.Class, GameScene.ItemInfoList);

                    Libraries.StateItems.Draw(RealItem.Image, DisplayLocation, Color.White, true, 1F);

                    //  More optimal now
                    if (RealItem.Effect > 0)
                    {
                        if (glowCooldown < CMain.Time - 300)
                        {
                            glowCooldown = CMain.Time;
                            weaponGlow++;
                            if (weaponGlow >= Libraries.WeaponEffectLibrary[RealItem.Effect].GetCount())
                                weaponGlow = 0;

                        }
                        Libraries.WeaponEffectLibrary[RealItem.Effect].DrawBlend(weaponGlow, DisplayLocation, Color.White, true, 1F);
                    }

                }

                if (Grid[(int)EquipmentSlot.Helmet].Item != null)
                    Libraries.StateItems.Draw(Grid[(int)EquipmentSlot.Helmet].Item.Info.Image, DisplayLocation, Color.White, true, 1F);
                else
                {
                    int hair = 441 + GameScene.Hero.Hair + (GameScene.Hero.Class == MirClass.Assassin ? 20 : 0) + (GameScene.Hero.Gender == MirGender.Male ? 0 : 40);

                    int offSetX = GameScene.Hero.Class == MirClass.Assassin ? (GameScene.Hero.Gender == MirGender.Male ? 6 : 4) : 0;
                    int offSetY = GameScene.Hero.Class == MirClass.Assassin ? (GameScene.Hero.Gender == MirGender.Male ? 25 : 18) : 0;

                    Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X + offSetX, DisplayLocation.Y + offSetY), Color.White, true, 1F);
                }


                if (Grid[(int)EquipmentSlot.Shield].Item != null)
                    Libraries.StateItems.Draw(Grid[(int)EquipmentSlot.Shield].Item.Info.Image, DisplayLocation, Color.White, true, 1F);


                if (Grid[(int)EquipmentSlot.Pads].Item != null)
                    Libraries.StateItems.Draw(Grid[(int)EquipmentSlot.Pads].Item.Info.Image, DisplayLocation, Color.White, true, 1F);

            };



            Grid = new MirItemCell[18];

            Grid[(int)EquipmentSlot.Weapon] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Weapon,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(126, 7),
            };


            Grid[(int)EquipmentSlot.Armour] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Armour,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 62),
            };


            Grid[(int)EquipmentSlot.Helmet] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Helmet,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 7),
            };



            Grid[(int)EquipmentSlot.Torch] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Torch,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(166, 242),
            };


            Grid[(int)EquipmentSlot.Necklace] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Necklace,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 98),
            };


            Grid[(int)EquipmentSlot.BraceletL] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletL,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(6, 170),
            };

            Grid[(int)EquipmentSlot.BraceletR] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletR,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 170),
            };

            Grid[(int)EquipmentSlot.RingL] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingL,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(6, 206),
            };

            Grid[(int)EquipmentSlot.RingR] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingR,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 206),
            };


            Grid[(int)EquipmentSlot.Amulet] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Amulet,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(6, 242),
            };


            Grid[(int)EquipmentSlot.Boots] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Boots,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(46, 242),
            };

            Grid[(int)EquipmentSlot.Belt] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Belt,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(86, 242),
            };


            Grid[(int)EquipmentSlot.Stone] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Stone,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(126, 242),
            };

            Grid[(int)EquipmentSlot.Mount] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Mount,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 134),
            };

            Grid[(int)EquipmentSlot.Poison] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Poison,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(86, 7),
            };

            Grid[(int)EquipmentSlot.Medals] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Medals,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(206, 242),
            };

            Grid[(int)EquipmentSlot.Shield] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Shield,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(166, 7),
            };

            Grid[(int)EquipmentSlot.Pads] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Pads,
                GridType = MirGridType.HeroEquipment,
                Parent = CharacterPage,
                Location = new Point(6, 98),
            };

            CharacterButton = new MirButton
            {
                Index = 500,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 70),
                Parent = this,
                PressedIndex = 500,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA,
            };
            CharacterButton.Click += (o, e) => ShowCharacterPage();

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(241, 3),
                Library = Libraries.CustomPrguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            CloseButton.Click += (o, e) => Hide();

            StateButton = new MirButton
            {
                Library = Libraries.CustomTitle,
                Location = new Point(132, 70),
                Parent = this,
                PressedIndex = 502,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            StateButton.Click += (o, e) => ShowStatePage();

            SkillPage = new MirImageControl
            {
                Index = 508,
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 90),
                Visible = false
            };

            Magics = new HeroMagicButton[7];

            for (int i = 0; i < Magics.Length; i++)
                Magics[i] = new HeroMagicButton { Parent = SkillPage, Visible = false, Location = new Point(8, 8 + i * 33) };

            SkillButton = new MirButton
            {
                Library = Libraries.CustomTitle,
                Location = new Point(194, 70),
                Parent = this,
                PressedIndex = 503,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            SkillButton.Click += (o, e) => ShowSkillPage();

            NextButton = new MirButton
            {
                Index = 396,
                Location = new Point(140, 250),
                Library = Libraries.Prguse,
                Parent = SkillPage,
                PressedIndex = 397,
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                if (StartIndex + 7 >= GameScene.Hero.Magics.Count) return;

                StartIndex += 7;
                RefreshInterface();

                ClearCoolDowns();
            };

            BackButton = new MirButton
            {
                Index = 398,
                Location = new Point(90, 250),
                Library = Libraries.Prguse,
                Parent = SkillPage,
                PressedIndex = 399,
                Sound = SoundList.ButtonA,
            };
            BackButton.Click += (o, e) =>
            {
                if (StartIndex - 7 < 0) return;

                StartIndex -= 7;
                RefreshInterface();

                ClearCoolDowns();
            };

            StatusButton = new MirButton
            {
                Library = Libraries.CustomTitle,
                Location = new Point(70, 70),
                Parent = this,
                PressedIndex = 501,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            StatusButton.Click += (o, e) => ShowStatusPage();

            StatusPage = new MirImageControl
            {
                Index = 506,
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 90),
                Visible = false
            };

            StatusPage.BeforeDraw += (o, e) =>
            {
                ACLabel.Text = string.Format("{0}-{1}", GameScene.Hero.MinAC, GameScene.Hero.MaxAC);
                MACLabel.Text = string.Format("{0}-{1}", GameScene.Hero.MinMAC, GameScene.Hero.MaxMAC);
                DCLabel.Text = string.Format("{0}-{1}", GameScene.Hero.MinDC, GameScene.Hero.MaxDC);
                MCLabel.Text = string.Format("{0}-{1}", GameScene.Hero.MinMC, GameScene.Hero.MaxMC);
                SCLabel.Text = string.Format("{0}-{1}", GameScene.Hero.MinSC, GameScene.Hero.MaxSC);
                HealthLabel.Text = string.Format("{0}/{1}", GameScene.Hero.HP, GameScene.Hero.MaxHP);
                ManaLabel.Text = string.Format("{0}/{1}", GameScene.Hero.MP, GameScene.Hero.MaxMP);
                CritRLabel.Text = string.Format("{0}%", GameScene.Hero.CriticalRate);
                CritDLabel.Text = string.Format("{0}", GameScene.Hero.CriticalDamage);
                AttkSpdLabel.Text = string.Format("{0}", GameScene.Hero.ASpeed);
                AccLabel.Text = string.Format("+{0}", GameScene.Hero.Accuracy);
                AgilLabel.Text = string.Format("+{0}", GameScene.Hero.Agility);
                LuckLabel.Text = string.Format("{0}", GameScene.Hero.Luck);
            };

            StatePage = new MirImageControl
            {
                Index = 507,
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 90),
                Visible = false
            };
            StatePage.BeforeDraw += (o, e) =>
            {
                ExpPLabel.Text = string.Format("{0:0.##%}", GameScene.Hero.Experience / (double)GameScene.Hero.MaxExperience);
                BagWLabel.Text = string.Format("{0}/{1}", GameScene.Hero.CurrentBagWeight, GameScene.Hero.MaxBagWeight);
                WearWLabel.Text = string.Format("{0}/{1}", GameScene.Hero.CurrentWearWeight, GameScene.Hero.MaxWearWeight);
                HandWLabel.Text = string.Format("{0}/{1}", GameScene.Hero.CurrentHandWeight, GameScene.Hero.MaxHandWeight);
                MagicRLabel.Text = string.Format("+{0}", GameScene.Hero.MagicResist);
                PoisonResLabel.Text = string.Format("+{0}", GameScene.Hero.PoisonResist);
                HealthRLabel.Text = string.Format("+{0}", GameScene.Hero.HealthRecovery);
                ManaRLabel.Text = string.Format("+{0}", GameScene.Hero.SpellRecovery);
                PoisonRecLabel.Text = string.Format("+{0}", GameScene.Hero.PoisonRecovery);
                HolyTLabel.Text = string.Format("+{0}", GameScene.Hero.Holy);
                FreezeLabel.Text = string.Format("+{0}", GameScene.Hero.Freezing);
                PoisonAtkLabel.Text = string.Format("+{0}", GameScene.Hero.PoisonAttack);
            };

            // STATS I
            HealthLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 8),
                NotControl = true,
                Text = "0-0",
            };

            ManaLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 26),
                NotControl = true,
                Text = "0-0",
            };

            ACLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 44),
                NotControl = true,
                Text = "0-0",
            };

            MACLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 62),
                NotControl = true,
                Text = "0-0",
            };
            DCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 80),
                NotControl = true,
                Text = "0-0"
            };
            MCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 98),
                NotControl = true,
                Text = "0/0"
            };
            SCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 116),
                NotControl = true,
                Text = "0/0"
            };

            CritRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 134),
                NotControl = true
            };
            CritDLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 152),
                NotControl = true
            };
            AttkSpdLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 170),
                NotControl = true
            };
            AccLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 188),
                NotControl = true
            };
            AgilLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 206),
                NotControl = true
            };
            LuckLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 224),
                NotControl = true
            };

            ExpPLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 20),
                NotControl = true,
                Text = "0-0",
            };

            BagWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 38),
                NotControl = true,
                Text = "0-0",
            };

            WearWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 56),
                NotControl = true,
                Text = "0-0",
            };

            HandWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 74),
                NotControl = true,
                Text = "0-0",
            };
            MagicRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 92),
                NotControl = true,
                Text = "0-0"
            };
            PoisonResLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 110),
                NotControl = true,
                Text = "0/0"
            };
            HealthRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 128),
                NotControl = true,
                Text = "0/0"
            };

            ManaRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 146),
                NotControl = true
            };
            PoisonRecLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 164),
                NotControl = true
            };
            HolyTLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 182),
                NotControl = true
            };
            FreezeLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 200),
                NotControl = true
            };
            PoisonAtkLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 218),
                NotControl = true
            };
        }

        private void ClearCoolDowns()
        {
            for (int i = 0; i < Magics.Length; i++)
            {
                Magics[i].CoolDown.Dispose();
            }
        }

        public void RefreshInterface()
        {
            int offSet = GameScene.Hero.Gender == MirGender.Male ? 0 : 1;

            Index = 509;// +offSet;
            CharacterPage.Index = 340 + offSet;
            ClassImage.Index = 105 + (int)GameScene.Hero.Class;


            switch (GameScene.Hero.Class)
            {
                case MirClass.Warrior:
                    ClassImage.Index = 100;// + offSet * 5;
                    Hint = "Warrior";
                    if (GameScene.Hero.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Wizard:
                    ClassImage.Index = 101;// + offSet * 5;
                    Hint = "Wizard";
                    if (GameScene.Hero.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Taoist:
                    ClassImage.Index = 102;// + offSet * 5;
                    Hint = "Taoist";
                    if (GameScene.Hero.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Assassin:
                    ClassImage.Index = 103;// + offSet * 5;
                    Hint = "Assassin";
                    if (GameScene.Hero.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Archer:
                    ClassImage.Index = 104;// + offSet * 5;
                    Hint = "Archer";
                    if (GameScene.Hero.HumUp)
                        CharacterPage.Index = 379;
                    break;
            }

            for (int i = 0; i < Magics.Length; i++)
            {
                if (i + StartIndex >= GameScene.Hero.Magics.Count)
                {
                    Magics[i].Visible = false;
                    continue;
                }

                Magics[i].Visible = true;
                Magics[i].Update(GameScene.Hero.Magics[i + StartIndex]);
            }
        }

        public void ShowCharacterPage()
        {
            CharacterPage.Visible = true;
            StatusPage.Visible = false;
            StatePage.Visible = false;
            SkillPage.Visible = false;
            CharacterButton.Index = 500;
            StatusButton.Index = -1;
            StateButton.Index = -1;
            SkillButton.Index = -1;
        }

        public bool HasClassWeapon
        {
            get
            {
                if (Grid[(int)EquipmentSlot.Weapon].Item == null)
                    return true;

                switch (Grid[(int)EquipmentSlot.Weapon].Item.Info.Shape / 100)
                {
                    default:
                        return GameScene.Hero.Class == MirClass.Wizard || GameScene.Hero.Class == MirClass.Warrior || GameScene.Hero.Class == MirClass.Taoist;
                    case 1:
                    case 6:
                        return GameScene.Hero.Class == MirClass.Assassin;
                    case 7:
                    case 2:
                        return GameScene.Hero.Class == MirClass.Archer;
                }
            }
        }

        private void ShowStatusPage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = true;
            StatePage.Visible = false;
            SkillPage.Visible = false;
            CharacterButton.Index = -1;
            StatusButton.Index = 501;
            StateButton.Index = -1;
            SkillButton.Index = -1;


        }

        public void Process()
        {
            BringToFront();
        }

        public void ShowSkillPage()
        {
            RefreshInterface();

            CharacterPage.Visible = false;
            StatusPage.Visible = false;
            StatePage.Visible = false;
            SkillPage.Visible = true;
            CharacterButton.Index = -1;
            StatusButton.Index = -1;
            StateButton.Index = -1;
            SkillButton.Index = 503;
            StartIndex = 0;
        }

        private void ShowStatePage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = false;
            StatePage.Visible = true;
            SkillPage.Visible = false;
            CharacterButton.Index = -1;
            StatusButton.Index = -1;
            StateButton.Index = 502;
            SkillButton.Index = -1;

        }

        public void Show()
        {
            RefreshInterface();
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }
    }
    public sealed class HeroMagicButton : MirControl
    {
        public MirImageControl LevelImage, ExpImage, UseSpell;
        public MirLabel Use_Skill;
        public MirButton SkillButton;
        public MirLabel LevelLabel, NameLabel, ExpLabel, KeyLabel;
        public ClientMagic Magic;
        public MirAnimatedControl CoolDown;

        public HeroMagicButton()
        {
            Size = new Size(231, 33);

            SkillButton = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.MagIcon2,
                Parent = this,
                Location = new Point(36, 0),
                Sound = SoundList.ButtonA,
            };
            SkillButton.Click += (o, e) =>
            {
                new HeroAssignKeyPanel(Magic);
            };

            LevelImage = new MirImageControl
            {
                Index = 516,
                Library = Libraries.Title,
                Location = new Point(73, 7),
                Parent = this,
                NotControl = true,
            };

            Use_Skill = new MirLabel
            {
                Location = new Point(5, 4),
                Parent = this,
                NotControl = true,
                AutoSize = true
            };
            ExpImage = new MirImageControl
            {
                Index = 517,
                Library = Libraries.Title,
                Location = new Point(73, 19),
                Parent = this,
                NotControl = true,
            };

            LevelLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(88, 2),
                NotControl = true,
            };

            NameLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(109, 2),
                NotControl = true,
            };

            ExpLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(109, 15),
                NotControl = true,
            };

            CoolDown = new MirAnimatedControl
            {
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(36, 0),
                NotControl = true,
                UseOffSet = true,
                Loop = false,
                Animated = false,
                Opacity = 0.6F
            };
        }

        public void Update(ClientMagic magic)
        {
            Magic = magic;

            NameLabel.Text = Magic.Spell.ToString();

            LevelLabel.Text = Magic.Level.ToString();
            switch (Magic.Level)
            {
                case 0:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need1);
                    break;
                case 1:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need2);
                    break;
                case 2:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need3);
                    break;
                case 3:
                    if ((GameScene.Hero.HumUp && magic.HumUpEnable) || magic.OverrideHumUp)
                        ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need4);
                    else
                        ExpLabel.Text = "-";
                    break;
                case 4:
                    if ((GameScene.Hero.HumUp && magic.HumUpEnable) || magic.OverrideHumUp)
                        ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need5);
                    else
                        ExpLabel.Text = "-";
                    break;
                case 5:
                    ExpLabel.Text = "-";
                    break;
            }

            if (Magic.Key > 0)
                Use_Skill.Text = string.Format("Shift" + Environment.NewLine + "F{0}", Magic.Key);
            else
                Use_Skill.Text = "None";


            switch (magic.Spell)
            {  //Warrior
                case Spell.Fencing:
                    SkillButton.Hint = string.Format("Fencing \n\nHitting accuracy will be increased in accordance\nwith practice level.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Slaying:
                    SkillButton.Hint = string.Format("Slaying \n\nHitting accuracy and destructive power will\nbe increased in accordance with practive level.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Thrusting:
                    SkillButton.Hint = string.Format("Thrusting \n\nIncreases the reach of your hits destructive power\nwill increase in accordance with practive level.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.HalfMoon:
                    SkillButton.Hint = string.Format("HalfMoon \n\nCause damage to mobs in a semi circle with\nthe shock waves from your fast moving weapon.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FlamingSword:
                    SkillButton.Hint = string.Format("FlamingSword \n\nCause additional damage by summoning the spirit\nof fire into weapon\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ShoulderDash:
                    SkillButton.Hint = string.Format("ShoulderDash \n\nA warrior can push away mobs by charging\nthem with his shoulder, inflicting damage\nif they hit any obstacle.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.CrossHalfMoon:
                    SkillButton.Hint = string.Format("CrossHalfMoon \n\nA warrior uses two powerfull waves of Half Moon\nto inflict damage on all mobs stood next to them.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.TwinDrakeBlade:
                    SkillButton.Hint = string.Format("TwinDrakeBlade \n\nThe art of making multiple power attacks. It has a\nlow chance of stunning the mob temporarly. Stunned\nmobs get 1.5 times more damage inflicted.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.LionRoar:
                    SkillButton.Hint = string.Format("LionRoar \n\nParalyses mobs , duration increases with skill level.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.BladeAvalanche:
                    SkillButton.Hint = string.Format("BladeAvalanche \n\n3-Way Thrusting.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Entrapment:
                    SkillButton.Hint = string.Format("Entrapment \n\nParalyses mobs and draws them to the caster.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Rage:
                    SkillButton.Hint = string.Format("Rage \n\nEnhances your inner force to increase its power\nfor a certain time. Attack power and duration time\nwill depend on the skill level. Once the skill has been used\n you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ProtectionField:
                    SkillButton.Hint = string.Format("ProtectionField \n\nConcentrates inner force and spreads it to all\n the parts of your body. This will enhance the\nprotection from enemies. Defense power and duration\nwill be depend on the skill level. Once the skill\n has been used, you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SlashingBurst:
                    SkillButton.Hint = string.Format("SlashingBurst \n\nAllows The Warrior to Jump 1 Space Over a Obejct or Monster.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.CounterAttack:
                    SkillButton.Hint = string.Format("CounterAttack \n\n Increases AC and AMC for a short period of time\nChance to defend an attack and counter.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ImmortalSkin:
                    SkillButton.Hint = string.Format("ImmortalSkin \n\n Increase defense to reduce attacks.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Fury:
                    SkillButton.Hint = string.Format("Fury \n\n Increases the warriors attack speed for a set period of time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SliceNDice:
                    SkillButton.Hint = string.Format("SliceNDice \n\n Slice though your attacker with three powerfull hits.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.BladeStorm:
                    SkillButton.Hint = string.Format("BladeStorm \n\n Slice though your attacker with powerfull wind blades.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.BlazingSword:
                    SkillButton.Hint = string.Format("BlazingSword\n\nCause additional damage by summoning the spirit\nof fire into weapon and strike in a line\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                //Wizard
                case Spell.FireBall:
                    SkillButton.Hint = string.Format("Fireball \n\nInstant Casting \n\nElements of fire are gathered to form\na fireball. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.DragonFlames:
                    SkillButton.Hint = string.Format("DragonFlames \nChanneling Casting \n\nElements of fire are gathered to form\na DragonFlame. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.BrokenSoulCut:
                    SkillButton.Hint = string.Format("BrokenSoulCut \nChanneling Casting \n\nElements of fire are gathered to form\na DragonFlame. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SoulReaper:
                    SkillButton.Hint = string.Format("SoulReaper \nChanneling Casting \n\nElements of dark are gathered to form\na Dark Void. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.LastJudgement:
                    SkillButton.Hint = string.Format("LastJudgement \nChanneling Casting \n\nElements of fire are gathered to form\na Judgement power. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ThunderClap:
                    SkillButton.Hint = string.Format("ThunderClap \nChanneling Casting \n\nElements of electricity are gathered to form\na Thunder. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ChopChopStar:
                    SkillButton.Hint = string.Format("ChopChopStar \nChanneling Casting \n\nElements of wind are gathered to form\na cutting blade. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SoulEaterSwamp:
                    SkillButton.Hint = string.Format("SoulEaterSwamp \nChanneling Casting \n\nElements of earth are gathered to form\na explodin blast. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.HandOfGod:
                    SkillButton.Hint = string.Format("HandOfGod \nChanneling Casting \n\nElements of fire are gathered to form\na explodin blast. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ThunderBolt:
                    SkillButton.Hint = string.Format("Thundebolt \n\nInstant Casting \n\nStrikes the foe with a lightning bolt \ninflicting high damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.GreatFireBall:
                    SkillButton.Hint = string.Format("GreatFireBall \n\nInstant Casting\n\nStronger then fire ball, Great Fire Ball\nwill fire up the mobs.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Repulsion:
                    SkillButton.Hint = string.Format("Repulsion \n\nInstant Casting\n\nPush away mobs useing the power of fire.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.HellFire:
                    SkillButton.Hint = string.Format("Hellfire \n\nInstant Casting\n\nShoots out a streak of fire attack\nthe monster in front.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Lightning:
                    SkillButton.Hint = string.Format("Lightning \n\nInstant Casting\n\nShoots out a steak of lightning to attack\nthe monster in front.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ElectricShock:
                    SkillButton.Hint = string.Format("ElectrickShock \n\nInstant Casting\n\nStrong shock wave hits the mob and the\nmob will not be able to move or the mob\nwill get confused and fight for you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Teleport:
                    SkillButton.Hint = string.Format("Teleport \n\nInstant Casting\n\nTeleport to a random spot.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FireWall:
                    SkillButton.Hint = string.Format("FireWall \n\nInstant Casting\n\nThis skill will build a fire wall at a designated\nspot to attack the monster passing the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FireBang:
                    SkillButton.Hint = string.Format("FireBang \n\nInstant Casting\n\nFirebang will burst out fire at a designated spot to\nburn all the monster within the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.ThunderStorm:
                    SkillButton.Hint = string.Format("Thunderstorm \n\nInstant Casting\n\nThis skill will make a thunder storm with in a designated area \nto attack the monster with in.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MagicShield:
                    SkillButton.Hint = string.Format("MagicShield \n\nInstant Casting\n\nThis skill will use Mp to create protective\nlayer around you\nAttack will be absorbed by the protective layer\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.TurnUndead:
                    SkillButton.Hint = string.Format("TurnUndead \n\nInstant Casting\n\nThis magic will bring birght light into \npower and attack undead monsters\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.IceStorm:
                    SkillButton.Hint = string.Format("IceStorm \n\nInstant Casting\n\nThis skill will make an ice storm with in a designated \narea to attack the monsters with in\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FlameDisruptor:
                    SkillButton.Hint = string.Format("FlameDisruptor \n\nInstant Casting\n\nFlame from the underground will be brought\ninto surface to attack the mobs.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FrostCrunch:
                    SkillButton.Hint = string.Format("FrostCrunch \n\nInstant Casting\n\nFreeze the elements in the air around the \nmonster to slow them down\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Mirroring:
                    SkillButton.Hint = string.Format("Mirroring \n\nInstant Casting\n\nCreate a mirror image of yourself to attack\nthe monsters together\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FlameField:
                    SkillButton.Hint = string.Format("FlameField \n\nInstant Casting\n\nA powerful spell of fire is used to \ndamage surrounding enemies.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Vampirism:
                    SkillButton.Hint = string.Format("Vampirism \n\nInstant Casting\n\nUsing Mp take away monsters Hp to\nincrease your Hp.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Blizzard:
                    SkillButton.Hint = string.Format("Blizzard \n\nConcentrate inner force and spreads it to all\nthe parts of your body.This will enhance the\nprotection from enemies. Defense power and duration\ntime will depend on the skill level. Once the skill\nhas been used, you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MeteorStrike:
                    SkillButton.Hint = string.Format("MeteorStrike \n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with lumps \nof fire falling from the sky.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.IceThrust:
                    SkillButton.Hint = string.Format("IceThrust \n\nInstant Casting\n\nAttack monsters by creating an ice pillar.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MagicBooster:
                    SkillButton.Hint = string.Format("MagicBooster \n\nLasting Effect\n\nIncrease magical damage, but comsume additional MP.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FastMove:
                    SkillButton.Hint = string.Format("FastMove \n\nLimited Effect\n\nIncrease movemoent with rooted skills.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.StormEscape:
                    SkillButton.Hint = string.Format("StormEscape \n\nLimited Effect\n\nParalyze nearby enemies and teleport to the designated location.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.LavaKing:
                    SkillButton.Hint = string.Format("LavaKing \n\nLimited Effect\n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with lumps lava.\nMay cause a burning poison.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FrozenRains:
                    SkillButton.Hint = string.Format("FrozenRains \n\nLimited Effect\n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with Rains.\nMay cause a Frozen poison.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;

                //Taoist
                case Spell.SpiritSword:
                    SkillButton.Hint = string.Format("SpiritSword \n\nIncreases the chance of hitting the target in\n melee combat.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Healing:
                    SkillButton.Hint = string.Format("Healing \n\nInstant Casting\n\n Heals a single target \nrecovering HP over time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Poisoning:
                    SkillButton.Hint = string.Format("Poisoning \n\nInstant Casting\nRequired Items: Poison Powder\n\nThrow poison at mobs to weaken them.\nUse green poison to weaken Hp.\nUse red poison to weaken defense.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SoulFireBall:
                    SkillButton.Hint = string.Format("SoulFireBall \n\nInstant Casting\nRequired Items: Amulet\n\nPut power into a scroll and throw it at \na mob. The scroll will burst into fire.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SoulShield:
                    SkillButton.Hint = string.Format("SoulShield \n\nInstant Casting\nRequired Items: Amulet\n\nBless the partymembers to strengthen there magic\ndefence.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.BlessedArmour:
                    SkillButton.Hint = string.Format("BlessedArmour \n\nInstant Casting\nRequired Items: Amulet\n\nBless the partymemebers to strenghten there defence.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.TrapHexagon:
                    SkillButton.Hint = string.Format("TrapHexagon \n\nInstant Casting\nRequired Items: Amulet\n\nTrap the monster with this magical power\n to stop them from moving. Any damages\nfrom outside source will allow the monsters\nto move again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SummonSkeleton:
                    SkillButton.Hint = string.Format("SummonSkeleton \n\nInstant Casting\nSummons a Powerful AOE Skeleton, Which will Fight Side By Side With You\nRequired Items: Amulet.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Hiding:
                    SkillButton.Hint = string.Format("Hiding \n\nInstant Casting\nRequired Items: Amulet\n\nMobs will not be able to spot you for a short\nmoment.Mobs will notice you if you start\nto move around.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MassHiding:
                    SkillButton.Hint = string.Format("MassHiding \n\nInstant Casting\nRequired Items: Amulet\n\nMobs will not be able to spot you or your \nparty members for a short moment. \nMobs will notice you and your party if \nyou start to move around.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Revelation:
                    SkillButton.Hint = string.Format("Revelation \n\nInstant Casting\n\nYou will be able to read Hp of others\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MassHealing:
                    SkillButton.Hint = string.Format("MassHealing \n\nInstant Casting\n\nHeal all injured players in the specified\narea by surrounding them with mana.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SummonShinsu:
                    SkillButton.Hint = string.Format("SummonShinsu \n\nInstant Casting\nSummons a Dog, That Will fight Side By Side with you.\nRequired Items: Amulet.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.UltimateEnhancer:
                    SkillButton.Hint = string.Format("UltimateEnhancer \n\nInstant Casting\nRequired Items: Amulet\n\nAbsorb the energy from the surroundings to increase the stats.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.EnergyRepulsor:
                    SkillButton.Hint = string.Format("EnergyRepulsor \n\nInstant Casting\n\nConcentrate your energy for one big blast to push away the monsters around you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Purification:
                    SkillButton.Hint = string.Format("Purification \n\nInstant Casting\n\nHelp others to recover from poisoning and\nparalysis useing this skill.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SummonHolyDeva:
                    SkillButton.Hint = string.Format("SummonHolyDeva \n\nInstant Casting\nRequired Items: Amulet\n\nSummon a holy spirit.This holy spirit will\nuse strong thunder to attack monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Curse:
                    SkillButton.Hint = string.Format("Curse \n\nInstant Casting\nRequired Items: Amulet\n\nReduces mob attacks (Attack Speed, DC ,MC ,SC)\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Hallucination:
                    SkillButton.Hint = string.Format("Hallucination \n\nInstant Casting\nRequired Items: Amulet\n\nThe monster will only see hallucination \nand attack anyone on the way\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Reincarnation:
                    SkillButton.Hint = string.Format("Reincarnation \n\nInstant Casting\nRequired Items: Amulet\n\nRevives a dead players\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.PoisonCloud:
                    SkillButton.Hint = string.Format("PoisonCloud \n\nInstant Casting\nRequired Items: GreenPoison\n\nThrow the amulet and a very strong\npoison cloud will appear in the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.EnergyShield:
                    SkillButton.Hint = string.Format("EnergyShield \n\nInstant Casting\nRequired Items: Amulet\n\nCreate an enegy shield to heal immediately when attacked by monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Plague:
                    SkillButton.Hint = string.Format("Plague \n\nInstant Casting\nRequired Items: Amulet\n\nDecreases targets MP and inflict target with various debuffs\nExample: Stun , Curse , Poison and Slow.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.HealingCircle:
                    SkillButton.Hint = string.Format("HealingCircle \n\nInstant Casting\nTreatment area friendly target, and the enemy caused spell damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.PetEnhancer:
                    SkillButton.Hint = string.Format("PetEnhancer \n\nInstant Casting\nStrengthening pets defense and power.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;

                //Assassin
                case Spell.FatalSword:
                    SkillButton.Hint = string.Format("FatalSword \n\nIncrease attack damage on the monsters.\nalso increases accuracy a little.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.DoubleSlash:
                    SkillButton.Hint = string.Format("DoubleSlash \n\nSlash the monster twice in a quick motion\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Haste:
                    SkillButton.Hint = string.Format("Haste \n\nIncrease the attack speed\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FlashDash:
                    SkillButton.Hint = string.Format("FlashDash \n\nAttack a monster with quick slash and\nparalize the monster\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.HeavenlySword:
                    SkillButton.Hint = string.Format("HeavenlySword \n\nAttack monsters with in 2 steps radius\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.FireBurst:
                    SkillButton.Hint = string.Format("FireBurst \n\nPush away mobs surrounding you\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Trap:
                    SkillButton.Hint = string.Format("Trap \nInstant casting CoolTime 60 secs\n\nTrap the monster for a short while.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MoonLight:
                    SkillButton.Hint = string.Format("Moonlight \n\nHide yourself from monster by turning invisible\nGreater damage is done when you attack monster using\nthis skill.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MPEater:
                    SkillButton.Hint = string.Format("MpEater \nPassive\nAbsord monsters MP to recharge your MP\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.SwiftFeet:
                    SkillButton.Hint = string.Format("SwiftFeet \n\nIncreased Runing Speed\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.LightBody:
                    SkillButton.Hint = string.Format("LightBody \n\nLighten your body using this skill and move faster\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.PoisonSword:
                    SkillButton.Hint = string.Format("PoisonSword \n\nPoison the monsters with a slash of you\nsword.Poison effect will damage the monster\nover time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.DarkBody:
                    SkillButton.Hint = string.Format("DarkBody \n\nCreate an illusion of yourself to attack\nthe monster while you become invisible.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.CrescentSlash:
                    SkillButton.Hint = string.Format("CrescentSlash \n\nBurst out of the power of your sword and attack all monsters around you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.Hemorrhage:
                    SkillButton.Hint = string.Format("Hemorrhage \nPassive\nChance to deal cristical damage and inflict bleeding damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;
                case Spell.MoonMist:
                    SkillButton.Hint = string.Format("Moon Mist\nActive\nAbility to hide your self from Monster\nYour first attack will be stronger than normal.");
                    break;
                case Spell.FuryWaves:
                    SkillButton.Hint = string.Format("FuryWaves \n\nBurst out of the power of your sword and attack all monsters around you.\nThis could push or poison. \nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? 0 : 0);
                    break;




                default:

                    break;

            }



            SkillButton.Index = Magic.Icon * 2;
            SkillButton.PressedIndex = Magic.Icon * 2;

            SetDelay();
        }

        public void SetDelay()
        {
            if (Magic == null) return;

            int totalFrames = 34;

            long timeLeft = Magic.CastTime + Magic.Delay - CMain.Time;

            if (timeLeft < 100 || (CoolDown != null && CoolDown.Animated)) return;

            int delayPerFrame = (int)(Magic.Delay / totalFrames);
            int startFrame = totalFrames - (int)(timeLeft / delayPerFrame);

            if ((CMain.Time <= Magic.CastTime + Magic.Delay) && Magic.CastTime > 0)
            {
                CoolDown.Dispose();

                CoolDown = new MirAnimatedControl
                {
                    Index = 1290 + startFrame,
                    AnimationCount = (totalFrames - startFrame),
                    AnimationDelay = delayPerFrame,
                    Library = Libraries.Prguse2,
                    Parent = this,
                    Location = new Point(36, 0),
                    NotControl = true,
                    UseOffSet = true,
                    Loop = false,
                    Animated = true,
                    Opacity = 0.6F
                };
            }
        }

        private Keys GetKey(int barindex, int i)
        {
            //KeybindOptions Type = KeybindOptions.Bar1Skill1;
            if ((barindex == 3) && (i == 1))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill1);
            if ((barindex == 3) && (i == 2))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill2);
            if ((barindex == 3) && (i == 3))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill3);
            if ((barindex == 3) && (i == 4))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill4);
            if ((barindex == 3) && (i == 5))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill5);
            if ((barindex == 3) && (i == 6))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill6);
            if ((barindex == 3) && (i == 7))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar3Skill7);
            return Keys.None;
        }
    }
    #endregion

       #region Belt
    public sealed class HeroBeltDialog : MirImageControl
    {
        public MirLabel[] Key = new MirLabel[2];
        public MirItemCell[] Grid;

        public HeroBeltDialog()
        {
            Index = 1921;
            Library = Libraries.CustomPrguse;
            Movable = false;
            Sort = true;
            Visible = true;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 520, Settings.ScreenHeight - 162);

            BeforeDraw += HeroBeltPanel_BeforeDraw;

            for (int i = 0; i < Key.Length; i++)
            {
                Key[i] = new MirLabel
                {
                    Parent = this,
                    Size = new Size(26, 14),
                    Location = new Point(48 + i * 35, 2),
                    Text = (i + 7).ToString()
                };
            }

            Grid = new MirItemCell[2];
            for (int x = 0; x < 2; x++)
            {
                Grid[x] = new MirItemCell
                {
                    ItemSlot = x,
                    Size = new Size(32, 32),
                    GridType = MirGridType.HeroInventory,
                    Library = Libraries.Items,
                    Parent = this,
                    Location = new Point(x * 36 + 50, 6), //12
                };
            }

        }

        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }

        private void HeroBeltPanel_BeforeDraw(object sender, EventArgs e)
        {
            //if Transparent return

            if (Libraries.CustomPrguse != null)
                Libraries.CustomPrguse.Draw(Index + 1, DisplayLocation, Color.White, false, 0.5F);
        }
        public void Hide()
        {
            Visible = false;
        }

        public void Show()
        {
            Visible = true;
        }

        public void setVisible(bool b)
        {
            Visible = b;
        }
    }
    #endregion

       #region Dura Panel
    public sealed class HeroCharacterDuraPanel : MirImageControl
    {
        public MirImageControl GrayBackground, Background, Helmet, Armour, Belt, Boots, Weapon, Necklace, RightBracelet, LeftBracelet, RightRing, LeftRing, Torch, Stone, Amulet, Mount, Item1, Item2;

        public HeroCharacterDuraPanel()
        {
            Index = 2106;
            Library = Libraries.Prguse;
            Movable = false;
            Location = new Point(Settings.ScreenWidth - 61, 330);

            GrayBackground = new MirImageControl()
            {
                Index = 2161,
                Library = Libraries.Prguse,
                Parent = this,
                Size = new Size(56, 80),
                Location = new Point(3, 3),
                Opacity = 0.4F
            };
            Background = new MirImageControl()
            {
                Index = 2118,
                Library = Libraries.Prguse,
                Parent = this,
                Size = new Size(56, 80),
                Location = new Point(3, 3),
            };

            #region Pieces

            Helmet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(24, 3) };
            Belt = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 7), Location = new Point(23, 23) };
            Armour = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(28, 32), Location = new Point(16, 11) };
            Boots = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(24, 9), Location = new Point(17, 43) };
            Weapon = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 33), Location = new Point(4, 5) };
            Necklace = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(3, 67) };
            LeftBracelet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 8), Location = new Point(3, 43) };
            RightBracelet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 8), Location = new Point(43, 43) };
            LeftRing = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(3, 54) };
            RightRing = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(43, 54) };
            Torch = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 32), Location = new Point(44, 5) };
            Stone = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(30, 54) };
            Amulet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(16, 54) };
            Mount = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(43, 68) };
            Item1 = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 12), Location = new Point(19, 67) };
            Item2 = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 12), Location = new Point(31, 67) };

            #endregion
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
        public void Show()
        {
            if (Visible) return;
            Visible = true;

            GetHeroDura();
        }

        public void GetHeroDura()
        {
            if (GameScene.Scene.HeroCharacterDialog.Grid[0].Item == null) { Weapon.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[1].Item == null) { Armour.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[2].Item == null) { Helmet.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[3].Item == null) { Torch.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[4].Item == null) { Necklace.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[5].Item == null) { LeftBracelet.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[6].Item == null) { RightBracelet.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[7].Item == null) { LeftRing.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[8].Item == null) { RightRing.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[9].Item == null) { Amulet.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[10].Item == null) { Belt.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[11].Item == null) { Boots.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[12].Item == null) { Stone.Index = -1; }
            if (GameScene.Scene.HeroCharacterDialog.Grid[13].Item == null) { Mount.Index = -1; }

            for (int i = 0; i < GameScene.Hero.Equipment.Length; i++)
            {
                if (GameScene.Hero.Equipment[i] == null) continue;
                UpdateHeroDura(GameScene.Hero.Equipment[i]);
            }
        }

        public void UpdateHeroDura(UserItem item)
        {
            int Warning = item.MaxDura / 2;
            int Danger = item.MaxDura / 5;
            uint AmuletWarning = item.Info.StackSize / 2;
            uint AmuletDanger = item.Info.StackSize / 5;

            switch (item.Info.Type)
            {
                case ItemType.Amulet:
                    if (item.Count > AmuletWarning)
                        Amulet.Index = 2134;
                    if (item.Count <= AmuletWarning)
                        Amulet.Index = 2135;
                    if (item.Count <= AmuletDanger)
                        Amulet.Index = 2136;
                    if (item.Count == 0)
                        Amulet.Index = -1;
                    break;
                case ItemType.Armour:
                    if (item.CurrentDura > Warning)
                        Armour.Index = 2149;
                    if (item.CurrentDura <= Warning)
                        Armour.Index = 2150;
                    if (item.CurrentDura <= Danger)
                        Armour.Index = 2151;
                    if (item.CurrentDura == 0)
                        Armour.Index = -1;
                    break;
                case ItemType.Belt:
                    if (item.CurrentDura > Warning)
                        Belt.Index = 2158;
                    if (item.CurrentDura <= Warning)
                        Belt.Index = 2159;
                    if (item.CurrentDura <= Danger)
                        Belt.Index = 2160;
                    if (item.CurrentDura == 0)
                        Belt.Index = -1;
                    break;
                case ItemType.Boots:
                    if (item.CurrentDura > Warning)
                        Boots.Index = 2152;
                    if (item.CurrentDura <= Warning)
                        Boots.Index = 2153;
                    if (item.CurrentDura <= Danger)
                        Boots.Index = 2154;
                    if (item.CurrentDura == 0)
                        Boots.Index = -1;
                    break;
                case ItemType.Bracelet:
                    if (GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.BraceletR].Item != null && item.UniqueID == GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.BraceletR].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            RightBracelet.Index = 2143;
                        if (item.CurrentDura <= Warning)
                            RightBracelet.Index = 2144;
                        if (item.CurrentDura <= Danger)
                            RightBracelet.Index = 2145;
                        if (item.CurrentDura == 0)
                            RightBracelet.Index = -1;
                    }
                    else if (GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.BraceletL].Item != null && item.UniqueID == GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.BraceletL].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            LeftBracelet.Index = 2143;
                        if (item.CurrentDura <= Warning)
                            LeftBracelet.Index = 2144;
                        if (item.CurrentDura <= Danger)
                            LeftBracelet.Index = 2145;
                        if (item.CurrentDura == 0)
                            LeftBracelet.Index = -1;
                    }
                    break;
                case ItemType.Helmet:
                    if (item.CurrentDura > Warning)
                        Helmet.Index = 2155;
                    if (item.CurrentDura <= Warning)
                        Helmet.Index = 2156;
                    if (item.CurrentDura <= Danger)
                        Helmet.Index = 2157;
                    if (item.CurrentDura == 0)
                        Helmet.Index = -1;
                    break;
                case ItemType.Necklace:
                    if (item.CurrentDura > Warning)
                        Necklace.Index = 2122;
                    if (item.CurrentDura <= Warning)
                        Necklace.Index = 2123;
                    if (item.CurrentDura <= Danger)
                        Necklace.Index = 2124;
                    if (item.CurrentDura == 0)
                        Necklace.Index = -1;
                    break;
                case ItemType.Ring:
                    if (GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.RingR].Item != null && item.UniqueID == GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.RingR].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            RightRing.Index = 2131;
                        if (item.CurrentDura <= Warning)
                            RightRing.Index = 2132;
                        if (item.CurrentDura <= Danger)
                            RightRing.Index = 2133;
                        if (item.CurrentDura == 0)
                            RightRing.Index = -1;
                    }
                    else if (GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.RingL].Item != null && item.UniqueID == GameScene.Scene.HeroCharacterDialog.Grid[(byte)EquipmentSlot.RingL].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            LeftRing.Index = 2131;
                        if (item.CurrentDura <= Warning)
                            LeftRing.Index = 2132;
                        if (item.CurrentDura <= Danger)
                            LeftRing.Index = 2133;
                        if (item.CurrentDura == 0)
                            LeftRing.Index = -1;
                    }
                    break;
                case ItemType.Stone:
                    if (item.CurrentDura == 0)
                        Stone.Index = 2137;
                    break;
                case ItemType.Mount:
                    if (item.CurrentDura > Warning)
                        Mount.Index = 2140;
                    if (item.CurrentDura <= Warning)
                        Mount.Index = 2141;
                    if (item.CurrentDura <= Danger)
                        Mount.Index = 2142;
                    if (item.CurrentDura == 0)
                        Mount.Index = -1;
                    break;
                case ItemType.Torch:
                    if (item.CurrentDura > Warning)
                        Torch.Index = 2146;
                    if (item.CurrentDura <= Warning)
                        Torch.Index = 2147;
                    if (item.CurrentDura <= Danger)
                        Torch.Index = 2148;
                    if (item.CurrentDura == 0)
                        Torch.Index = -1;
                    break;
                case ItemType.Weapon:
                    if (item.CurrentDura > Warning)
                        Weapon.Index = 2125;
                    if (item.CurrentDura <= Warning)
                        Weapon.Index = 2126;
                    if (item.CurrentDura <= Danger)
                        Weapon.Index = 2127;
                    if (item.CurrentDura == 0)
                        Weapon.Index = -1;
                    break;
            }
        }
    }
    #endregion

       #region Skill Key Panel
    public sealed class HeroAssignKeyPanel : MirImageControl
    {
        public MirButton SaveButton, NoneButton;

        public MirLabel TitleLabel;
        public MirImageControl MagicImage;
        public MirButton[] FKeys;

        public ClientMagic Magic;
        public byte Key;

        public HeroAssignKeyPanel(ClientMagic magic)
        {
            Magic = magic;
            Key = magic.Key;

            Modal = true;
            Index = 1640;
            Library = Libraries.Prguse;
            Location = Center;
            Parent = GameScene.Scene;
            Visible = true;

            MagicImage = new MirImageControl
            {
                Location = new Point(16, 16),
                Index = magic.Icon * 2,
                Library = Libraries.MagIcon2,
                Parent = this,
            };

            TitleLabel = new MirLabel
            {
                Location = new Point(49, 17),
                Parent = this,
                Size = new Size(230, 32),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak,
                Text = string.Format("Select the Key for: {0}", magic.Spell)
            };

            NoneButton = new MirButton
            {
                Index = 287, //154
                HoverIndex = 288,
                PressedIndex = 289,
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(284, 64),
            };
            NoneButton.Click += (o, e) => Key = 0;

            SaveButton = new MirButton
            {
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(284, 101),
                Index = 156,
                HoverIndex = 157,
                PressedIndex = 158,
            };
            SaveButton.Click += (o, e) =>
            {
                for (int i = 0; i < GameScene.Hero.Magics.Count; i++)
                {
                    if (GameScene.Hero.Magics[i].Key == Key)
                        GameScene.Hero.Magics[i].Key = 0;
                }

                Network.Enqueue(new C.HeroMagicKey { Spell = Magic.Spell, Key = Key });
                Magic.Key = Key;

                for (int i = 0; i < GameScene.Scene.HeroCharacterDialog.Magics.Length; i++)
                    if (GameScene.Scene.HeroCharacterDialog.Magics[i].Magic == Magic)
                        GameScene.Scene.HeroCharacterDialog.Magics[i].Update(magic);
                Dispose();
            };


            FKeys = new MirButton[7];

            FKeys[0] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(36, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F1"
            };
            FKeys[0].Click += (o, e) => Key = 1;

            FKeys[1] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(68, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F2"
            };
            FKeys[1].Click += (o, e) => Key = 2;

            FKeys[2] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(100, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F3"
            };
            FKeys[2].Click += (o, e) => Key = 3;

            FKeys[3] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(132, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F4"
            };
            FKeys[3].Click += (o, e) => Key = 4;

            FKeys[4] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(169, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F5"
            };
            FKeys[4].Click += (o, e) => Key = 5;

            FKeys[5] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(201, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F6",
            };
            FKeys[5].Click += (o, e) => Key = 6;

            FKeys[6] = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(233, 77),
                Sound = SoundList.ButtonA,
                Text = "Shift" + Environment.NewLine + "F7"
            };
            FKeys[6].Click += (o, e) => Key = 7;

            BeforeDraw += AssignKeyPanel_BeforeDraw;
        }

        private void AssignKeyPanel_BeforeDraw(object sender, EventArgs e)
        {
            for (int i = 0; i < FKeys.Length; i++)
            {
                FKeys[i].Index = 1656;
                FKeys[i].HoverIndex = 1657;
                FKeys[i].PressedIndex = 1658;
                FKeys[i].Visible = true;
            }

            if (Key == 0 || Key > FKeys.Length) return;

            FKeys[Key - 1].Index = 1658;
            FKeys[Key - 1].HoverIndex = 1658;
            FKeys[Key - 1].PressedIndex = 1658;
        }
    }
    #endregion
}