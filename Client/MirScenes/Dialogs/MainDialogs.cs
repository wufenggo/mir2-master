using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using Microsoft.DirectX.Direct3D;
using Font = System.Drawing.Font;
using S = ServerPackets;
using C = ClientPackets;
using Effect = Client.MirObjects.Effect;
using Client.MirScenes.Dialogs;
using System.Drawing.Imaging;

namespace Client.MirScenes.Dialogs
{
    public sealed class MainDialogLeft : MirControl
    {
        public MirButton KeyBoardButton, HeroInfoButton;

        public MainDialogLeft()
        {
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 146, GameScene.Scene.MainDialog.Location.Y + 90);
            Size = new Size(36, 94);

            KeyBoardButton = new MirButton
            {
                Index = 285,
                HoverIndex = 286,
                PressedIndex = 287,
                Parent = this,
                Location = new Point(0, 32),
                Library = Libraries.CustomButtons,
                Sound = SoundList.ButtonA,
                Hint = "Key Binds (" + CMain.InputKeys.GetKey(KeybindOptions.KeySettings) + ")"
            };
            KeyBoardButton.Click += (o, e) =>
            {
                if (GameScene.Scene.KeyboardLayoutDialog.Visible)
                    GameScene.Scene.KeyboardLayoutDialog.Hide();
                else
                    GameScene.Scene.KeyboardLayoutDialog.Show();
            };

            HeroInfoButton = new MirButton
            {
                Index = 285,
                HoverIndex = 286,
                PressedIndex = 287,
                Parent = this,
                Location = new Point(0, 0),
                Library = Libraries.CustomButtons,
                Sound = SoundList.ButtonA,
                Hint = "HeroTab"
            };
            HeroInfoButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.HeroDialog.Visible)
                    GameScene.Scene.HeroDialog.Show();
                else
                    GameScene.Scene.HeroDialog.Hide();
            };
        }

        public void Show()
        {
            if (!Visible)
                Visible = true;
            BringToFront();
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }
    }

    public sealed class MainDialogRight : MirControl
    {
        public MirButton
            GameShopButton,
            StashButton,
            MenuButton,
            InventoryButton,
            CharacterButton,
            SkillButton,
            QuestButton,
            OptionButton,
            HeroBarButton,
            GroupButton;

        public MainDialogRight()
        {
            Location = new Point(GameScene.Scene.MainDialog.Location.X + GameScene.Scene.MainDialog.Size.Width - 170, GameScene.Scene.MainDialog.Location.Y + 30);
            Size = new Size(190, GameScene.Scene.MainDialog.Size.Height - 30);

            InventoryButton = new MirButton
            {
                HoverIndex = 1904,
                Index = 1903,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 151, 13),// 43
                Parent = this,
                PressedIndex = 1905,
                Sound = SoundList.ButtonA,
                Hint = "Inventory (" + CMain.InputKeys.GetKey(KeybindOptions.Inventory) + ")"
            };
            InventoryButton.Click += (o, e) =>
            {
                if (GameScene.Scene.InventoryDialog.Visible)
                    GameScene.Scene.InventoryDialog.Hide();
                else
                    GameScene.Scene.InventoryDialog.Show();
            };

            CharacterButton = new MirButton
            {
                HoverIndex = 1901,
                Index = 1900,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 194, 17), //47
                Parent = this,
                PressedIndex = 1902,
                Sound = SoundList.ButtonA,
                Hint = "Character (" + CMain.InputKeys.GetKey(KeybindOptions.Equipment) + ")"
            };
            CharacterButton.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDialog.Visible && GameScene.Scene.CharacterDialog.CharacterPage.Visible)
                    GameScene.Scene.CharacterDialog.Hide();
                else
                {
                    GameScene.Scene.CharacterDialog.Show();
                    GameScene.Scene.CharacterDialog.ShowCharacterPage();
                }
            };

            SkillButton = new MirButton
            {
                HoverIndex = 1907,
                Index = 1906,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 108, 3),
                Parent = this,
                PressedIndex = 1908,
                Sound = SoundList.ButtonA,
                Hint = "Skills (" + CMain.InputKeys.GetKey(KeybindOptions.Skills) + ")"
            };
            SkillButton.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDialog.Visible && GameScene.Scene.CharacterDialog.SkillPage.Visible)
                    GameScene.Scene.CharacterDialog.Hide();
                else
                {
                    GameScene.Scene.CharacterDialog.Show();
                    GameScene.Scene.CharacterDialog.ShowSkillPage();
                }
            };

            QuestButton = new MirButton
            {
                HoverIndex = 1910,
                Index = 1909,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 106, 51),
                Parent = this,
                PressedIndex = 1911,
                Sound = SoundList.ButtonA,
                Hint = "Quests (" + CMain.InputKeys.GetKey(KeybindOptions.Quests) + ")"
            };
            QuestButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.QuestLogDialog.Visible)
                    GameScene.Scene.QuestLogDialog.Show();
                else GameScene.Scene.QuestLogDialog.Hide();
            };

            OptionButton = new MirButton
            {
                HoverIndex = 1913,
                Index = 1912,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 70, 43),
                Parent = this,
                PressedIndex = 1914,
                Sound = SoundList.ButtonA,
                Hint = "Options (" + CMain.InputKeys.GetKey(KeybindOptions.Options) + ")"
            };
            OptionButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.OptionDialog.Visible)
                    GameScene.Scene.OptionDialog.Show();
                else GameScene.Scene.OptionDialog.Hide();
            };

            MenuButton = new MirButton
            {
                HoverIndex = 1961,
                Index = 1960,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 65, -4),
                Parent = this,
                PressedIndex = 1962,
                Sound = SoundList.ButtonC,
                Hint = "Menu"
            };
            MenuButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.MenuDialog.Visible) GameScene.Scene.MenuDialog.Show();
                else GameScene.Scene.MenuDialog.Hide();
            };

            GameShopButton = new MirButton
            {
                HoverIndex = 827,
                Index = 826,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 78, 115),
                Parent = this,
                PressedIndex = 828,
                Sound = SoundList.ButtonC,
                Hint = "Game Shop (" + CMain.InputKeys.GetKey(KeybindOptions.GameShop) + ")"
            };
            GameShopButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.GameShopDialog.Visible) GameScene.Scene.GameShopDialog.Show();
                else GameScene.Scene.GameShopDialog.Hide();
            };

            GroupButton = new MirButton
            {
                HoverIndex = 2448,
                Index = 2447,
                Library = Libraries.CustomPrguse,
                Location = new Point(Size.Width - 143, 57),
                Parent = this,
                PressedIndex = 2449,
                Sound = SoundList.ButtonA,
                Hint = "Groups (" + CMain.InputKeys.GetKey(KeybindOptions.Group) + ")"
            };
            GroupButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupDialog.Visible) GameScene.Scene.GroupDialog.Hide();
                else GameScene.Scene.GroupDialog.Show();
            };
        }


        public void Show()
        {
            if (!Visible)
                Visible = true;
            BringToFront();
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }
    }


    public sealed class HeroHeaderDialog : MirImageControl
    {
        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }
        public MirImageControl HeroHPBar, HeroMPBar, HeroExpBarHeroLock, HeroLock, HeroLock1, HeroExpBar, HeroPot1, HeroPot2;
        public MirLabel HeroLevelLabel, NameHeroLabel, HeroExp, HealthHeroLabel, ManaHeroLabel, HeroPotLabel, HeroPot2Label;

        public MirButton HeroPicButton, SummonHeroButton, HeroStashButton;
        public long delaySpawn = CMain.Time;

        public HeroHeaderDialog()
        {
            Index = 10;
            Library = Libraries.CustomPrguse;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, 0);

            HeroLevelLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(157, 7),
                Parent = this,
                Font = new Font(Settings.FontName, 8F),
                ForeColour = Color.Goldenrod,
            };

            NameHeroLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(77, 7),
                Parent = this,
                Font = new Font(Settings.FontName, 8F),
                ForeColour = Color.Goldenrod,
            };

            HeroPicButton = new MirButton
            {
                Index = 18,
                Library = Libraries.CustomPrguse,
                Location = new Point(17, 17),
                Parent = this,
                Sound = SoundList.ButtonA,
                Hint = "Hero Ai Control"
            };
            HeroPicButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.HeroAIDialog.Visible) GameScene.Scene.HeroAIDialog.Show();
                else GameScene.Scene.HeroAIDialog.Hide();
            };

            HeroPot1 = new MirImageControl
            {
                Index = 1392,
                Library = Libraries.Prguse,
                Location = new Point(174, 0),
                Parent = this,
            };

            HeroPotLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Red,
                OutLineColour = Color.Black,
                Parent = HeroPot1,
                Location = new Point(0, 2),
                Visible = true,
            };

            HeroPot2 = new MirImageControl
            {
                Index = 1392,
                Library = Libraries.Prguse,
                Location = new Point(174, 20),
                Parent = this,
            };

            HeroPot2Label = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Blue,
                OutLineColour = Color.Black,
                Parent = HeroPot2,
                Location = new Point(0, 2),
                Visible = true,
            };

            HeroLock = new MirImageControl
            {
                Index = 18,
                Library = Libraries.CustomPrguse,
                Location = new Point(17, 17),
                Parent = this,
                Visible = false,
            };

            HeroLock1 = new MirImageControl
            {
                Index = 1358,
                Library = Libraries.Prguse,
                Location = new Point(4, 70),
                Parent = this,
                Visible = false,
            };
            HeroHPBar = new MirImageControl
            {
                Index = 14,
                Library = Libraries.CustomPrguse,
                Location = new Point(77, 25),
                Parent = this,
                DrawImage = false,
                NotControl = false,
            };
            HeroHPBar.BeforeDraw += HeroHPBar_BeforeDraw;

            HeroMPBar = new MirImageControl
            {
                Index = 15,
                Library = Libraries.CustomPrguse,
                Location = new Point(82, 38),
                Parent = this,
                DrawImage = false,
                NotControl = false,
            };
            HeroMPBar.BeforeDraw += HeroMPBar_BeforeDraw;

            HeroExpBar = new MirImageControl
            {
                Index = 16,
                Library = Libraries.CustomPrguse,
                Location = new Point(82, 50),
                Parent = this,
                DrawImage = false,
                NotControl = false,
            };
            HeroExpBar.BeforeDraw += HeroExpBar_BeforeDraw;

            HeroExp = new MirLabel
            {
                AutoSize = true,
                Location = new Point(10, 0),
                Parent = HeroExpBar,
            };
            HealthHeroLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(10, 0),
                Parent = HeroHPBar,
            };

            ManaHeroLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(10, 0),
                Parent = HeroMPBar,
            };

            SummonHeroButton = new MirButton
            {
                HoverIndex = 2056,
                Index = 2055,
                Library = Libraries.CustomPrguse,
                Location = new Point(130, 61),
                Parent = this,
                PressedIndex = 2054,
                Sound = SoundList.ButtonA,
                Hint = "Summon/UnSummon (" + CMain.InputKeys.GetKey(KeybindOptions.SummonHero) + ")",
            };
            SummonHeroButton.Click += (o, e) =>
            {
                if (delaySpawn > CMain.Time) return;
                delaySpawn = CMain.Time + 1000;

                if (User.HeroState == HeroState.Spawned)
                {
                    Network.Enqueue(new C.Chat { Message = "@DESPAWNHERO" });
                }
                else if (User.HeroState == HeroState.Unspawned)
                {
                    Network.Enqueue(new C.Chat { Message = "@SPAWNHERO" });
                }
            };

            HeroStashButton = new MirButton
            {
                HoverIndex = 2044,
                Index = 2043,
                Library = Libraries.CustomPrguse,
                Location = new Point(155, 61),
                Parent = this,
                PressedIndex = 2042,
                Sound = SoundList.ButtonC,
                Hint = "Hero Stash "
            };
            HeroStashButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.HeroStashDialog.Visible) GameScene.Scene.HeroStashDialog.Show();
                else GameScene.Scene.HeroStashDialog.Hide();
            };
        }

        private void HeroHPBar_BeforeDraw(object sender, EventArgs e)
        {
            if (HeroHPBar.Library == null || GameScene.Hero == null) return;

            double percent = GameScene.Hero.HP / (double)GameScene.Hero.MaxHP;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((HeroHPBar.Size.Width - 3) * percent), HeroHPBar.Size.Height)
            };

            HeroHPBar.Library.Draw(HeroHPBar.Index, section, HeroHPBar.DisplayLocation, Color.White, false);
        }

        private void HeroMPBar_BeforeDraw(object sender, EventArgs e)
        {
            if (HeroMPBar.Library == null || GameScene.Hero == null) return;

            double percent = GameScene.Hero.MP / (double)GameScene.Hero.MaxMP;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((HeroMPBar.Size.Width - 3) * percent), HeroMPBar.Size.Height)
            };

            HeroMPBar.Library.Draw(HeroMPBar.Index, section, HeroMPBar.DisplayLocation, Color.White, false);
        }

        private void HeroExpBar_BeforeDraw(object sender, EventArgs e)
        {
            if (HeroExpBar.Library == null || GameScene.Hero == null) return;

            double percent = GameScene.Hero.Experience / (double)GameScene.Hero.MaxExperience;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((HeroExpBar.Size.Width - 3) * percent), HeroExpBar.Size.Height)
            };

            HeroExpBar.Library.Draw(HeroExpBar.Index, section, HeroExpBar.DisplayLocation, Color.White, false);
        }

        public void Process()
        {

            if (User.HasHero && GameScene.Hero != null)
            {
                HealthHeroLabel.Text = string.Format("{0}/{1}", GameScene.Hero.HP, GameScene.Hero.MaxHP);
                HealthHeroLabel.AutoSize = true;
                ManaHeroLabel.Text = string.Format("{0}/{1} ", GameScene.Hero.MP, GameScene.Hero.MaxMP);
                ManaHeroLabel.AutoSize = true;
                HeroExp.Text = string.Format("{0:0.##%}", GameScene.Hero.Experience / (double)GameScene.Hero.MaxExperience);
                NameHeroLabel.Text = GameScene.Hero.Name;
                NameHeroLabel.AutoSize = true;
                HeroLevelLabel.Text = GameScene.Hero.Level.ToString();
                HeroLevelLabel.AutoSize = true;
                HeroPicButton.Index = User.HeroState == HeroState.Dead ? 21 : 18 + (int)GameScene.Hero.Class + (GameScene.Hero.Gender == MirGender.Female ? 3 : 0);
                HeroLock.Index = User.HeroState == HeroState.Dead ? 21 : 18 + (int)GameScene.Hero.Class + (GameScene.Hero.Gender == MirGender.Female ? 3 : 0);
                HeroLock1.Index = 1358 + (GameScene.Hero.Gender == MirGender.Female ? 1 : 0);
            }

            switch (User.HeroState)
            {
                case HeroState.Spawned:
                    Visible = true;
                    GameScene.Scene.MainDialogLeft.HeroInfoButton.Visible = true;
                    HeroPicButton.Visible = true;
                    HeroPot1.Visible = true;
                    HeroPot2.Visible = true;
                    GameScene.Scene.ChatControl.HeroDuraButton.Visible = true;
                    break;

                case HeroState.Unspawned:
                    Visible = false;
                    GameScene.Scene.MainDialogLeft.HeroInfoButton.Visible = false;
                    HeroPicButton.Visible = false;
                    HeroPot1.Visible = false;
                    HeroPot2.Visible = false;
                    GameScene.Scene.HeroDialog.Visible = false;
                    GameScene.Scene.ChatControl.HeroDuraButton.Visible = false;
                    break;

                case HeroState.Dead:
                    Visible = false;
                    GameScene.Scene.MainDialogLeft.HeroInfoButton.Visible = false;
                    HeroPicButton.Visible = true;
                    HeroPot1.Visible = false;
                    HeroPot2.Visible = false;
                    GameScene.Scene.HeroDialog.Visible = false;
                    GameScene.Scene.ChatControl.HeroDuraButton.Visible = false;
                    break;
            }

            if (GameScene.User.HasHero && GameScene.Hero != null && GameScene.Hero.isLocked && User.HeroState != HeroState.Unspawned)
            {
                HeroLock.Visible = true;
                HeroLock1.Visible = true;
            }
            else
            {
                HeroLock.Visible = false;
                HeroLock1.Visible = false;
            }


            if (User.HasHero)
            {
                HeroStashButton.Visible = true;
                SummonHeroButton.Visible = true;
            }
            else
            {
                HeroStashButton.Visible = false;
                SummonHeroButton.Visible = false;
            }

        }

        private void Label_SizeChanged(object sender, EventArgs e)
        {

            if (!(sender is MirLabel l)) return;

            l.Location = new Point(50 - (l.Size.Width / 2), l.Location.Y);
        }
    }

    public sealed class MainDialog : MirImageControl
    {
        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }

        public MirImageControl ExperienceBar, LeftCap, RightCap;
        public MirControl HealthOrb;
        public MirLabel
            TopLabel,
            HealthLabel, 
            ManaLabel,
            BottomLabel,
            LevelLabel,
            CharacterName,
            ExperienceLabel,
            GoldLabel,
            WeightLabel,
            SpaceLabel,
            AModeLabel,
            PModeLabel,
            SModeLabel,
            PingLabel,
            TimeLabel,
            WeightBar;


        public bool HPOnly
        {
            get { return User != null && User.Class == MirClass.Warrior && User.Level < 22; }
        }

        public MainDialog()
        {
            Index = Settings.Resolution == 800 ? 0 : Settings.Resolution == 1024 ? 1 : 1;
            Library = Libraries.CustomPrguse;
            Location = new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), Settings.ScreenHeight - Size.Height);
            PixelDetect = true;
            NotControl = true;

            LeftCap = new MirImageControl
            {
                Index = 12,
                Library = Libraries.CustomPrguse,
                Location = new Point(-69, Size.Height - 122),
                Parent = this,
                Visible = false
            };
            RightCap = new MirImageControl
            {
                Index = 13,
                Library = Libraries.CustomPrguse,
                Location = new Point(1024, Size.Height - 154),
                Parent = this,
                Visible = false
            };

            if (Settings.Resolution > 1024)
            {
                LeftCap.Visible = true;
                RightCap.Visible = true;
            }

            HealthOrb = new MirControl
            {
                Parent = this,
                Location = new Point(45, 65),
                NotControl = true,
            };

            HealthOrb.BeforeDraw += HealthOrb_BeforeDraw;

            #region Main InterFace Labals
            HealthLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(0, 27),
                Parent = HealthOrb
            };
            HealthLabel.SizeChanged += Label_SizeChanged;

            ManaLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(0, 42),
                Parent = HealthOrb
            };
            ManaLabel.SizeChanged += Label_SizeChanged;

            TopLabel = new MirLabel
            {
                Size = new Size(85, 30),
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Location = new Point(9, 20),
                Parent = HealthOrb,
            };

            BottomLabel = new MirLabel
            {
                Size = new Size(85, 30),
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Location = new Point(9, 50),
                Parent = HealthOrb,
            };

            LevelLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(Size.Width - 143, 122),
            };

            CharacterName = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(Size.Width - 96, 126),
                AutoSize = true,
            };

            ExperienceBar = new MirImageControl
            {
                Index = ((Settings.Resolution != 800) ? 8 : 7),
                Library = Libraries.Prguse,
                Location = new Point(11, 184),
                Parent = this,
                DrawImage = false,
                NotControl = false,
            };
            ExperienceBar.BeforeDraw += ExperienceBar_BeforeDraw;

            ExperienceLabel = new MirLabel
            {
                AutoSize = true,
                Parent = ExperienceBar,
                NotControl = true,
            };

            GoldLabel = new MirLabel
            {
                DrawFormat = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter),
                Font = new Font(Settings.FontName, 8f),
                Location = new Point(this.Size.Width - 150, 148),
                Parent = this,
                Size = new Size(89, 13),
                Sound = SoundList.Gold,
                NotControl = false,
                Hint = "Gold"
            };
            GoldLabel.Click += (o, e) =>
            {
                if (GameScene.SelectedCell == null)
                    GameScene.PickedUpGold = !GameScene.PickedUpGold && GameScene.Gold > 0;
            };

            WeightLabel = new MirLabel
            {
                Parent = this,
                Location = new Point(Size.Width - 150, 165),
                AutoSize = true,
                Hint = "Weight"
            };

            SpaceLabel = new MirLabel
            {
                DrawFormat = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter),
                Parent = this,
                Location = new Point(Size.Width - 105, 165),
                Size = new Size(25, 14),
                NotControl = false,
                Hint = "Used Space"
            };

            AModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                //Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -448 : -280),
                Location = new Point(Size.Width - 1200, 124),
            };

            PModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Orange,
                OutLineColour = Color.Black,
                Parent = this,
                //Location = new Point(230, 125),
                Location = new Point(Size.Width - 1200, 136),
                Visible = false
            };

            SModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.LimeGreen,
                OutLineColour = Color.Black,
                Parent = this,
                //Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -463 : -295),
                Location = new Point(Size.Width - 1200, 148),
            };

            PingLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                //Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -443 : -265),
                Location = new Point(Size.Width - 1200, 160),
                Visible = false
            };

            TimeLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                //Location = new Point(Settings.Resolution != 800 ? 894 : 601, Settings.Resolution != 800 ? -407 : -229),
                Location = new Point(this.Size.Width - 1200, 172),
                ForeColour = Color.Lime,
                Font = new Font(Settings.FontName, 8F, FontStyle.Bold),
                Size = new Size(90, 16)
            };
            #endregion
        }
        

        public void SetBaseImage()
        {
            if (User != null && User.HasHero)
            {
                Index = Settings.Resolution == 800 ? 0 : Settings.Resolution == 1024 ? 1 : 1;
                Library = Libraries.CustomPrguse;
                Location = new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), Settings.ScreenHeight - Size.Height);
                PixelDetect = true;
                NotControl = true;
            }
            else
            {
                Index = Settings.Resolution == 800 ? 0 : Settings.Resolution == 1024 ? 1 : 1;
                Library = Libraries.CustomPrguse;
                Location = new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), Settings.ScreenHeight - Size.Height);
                PixelDetect = true;
            }
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Process()
        {
            switch (GameScene.Scene.AMode)
            {
                case AttackMode.Peace:
                    AModeLabel.Text = "[Mode: Peaceful]";
                    break;
                case AttackMode.Group:
                    AModeLabel.Text = "[Mode: Group]";
                    break;
                case AttackMode.Guild:
                    AModeLabel.Text = "[Mode: Guild]";
                    break;
                case AttackMode.EnemyGuild:
                    AModeLabel.Text = "[Mode: Enemy Guild]";
                    break;
                case AttackMode.RedBrown:
                    AModeLabel.Text = "[Mode: Red/Brown]";
                    break;
                case AttackMode.All:
                    AModeLabel.Text = "[Mode: Attack All]";
                    break;
            }

            switch (GameScene.Scene.PMode)
            {
                case PetMode.Both:
                    PModeLabel.Text = "[Pet: Attack and Move]";
                    break;
                case PetMode.MoveOnly:
                    PModeLabel.Text = "[Pet: Do Not Attack]";
                    break;
                case PetMode.AttackOnly:
                    PModeLabel.Text = "[Pet: Do Not Move]";
                    break;
                case PetMode.None:
                    PModeLabel.Text = "[Pet: Do Not Attack or Move]";
                    break;
            }

            if ((GameScene.Scene.PingTime) > 0)
            {
                PingLabel.Text = string.Format("Ping: {0}", GameScene.Scene.PingTime);
                PingLabel.Visible = true;
            }
            else
            {
                PingLabel.Visible = false;
            }

            switch (Settings.SkillMode)
            {
                case true:
                    SModeLabel.Text = "[Skill Mode: ~]";
                    break;
                case false:
                    SModeLabel.Text = "[Skill Mode: Ctrl]";
                    break;
            }

            if (Settings.HPView)
            {
                HealthLabel.Text = string.Format("HP {0}/{1}", User.HP, User.MaxHP);
                ManaLabel.Text = HPOnly ? "" : string.Format("MP {0}/{1} ", User.MP, User.MaxMP);
                TopLabel.Text = string.Empty;
                BottomLabel.Text = string.Empty;
            }
            else
            {
                if (HPOnly)
                {
                    TopLabel.Text = string.Format("{0}\n" + "--", User.HP);
                    BottomLabel.Text = string.Format("{0}", User.MaxHP);
                }
                else
                {
                    TopLabel.Text = string.Format(" {0}    {1} \n" + "---------------", User.HP, User.MP);
                    BottomLabel.Text = string.Format(" {0}    {1} ", User.MaxHP, User.MaxMP);
                }
                HealthLabel.Text = string.Empty;
                ManaLabel.Text = string.Empty;
            }

            LevelLabel.Text = User.Level.ToString();
            ExperienceLabel.Text = string.Format("{0:#0.##%}", User.Experience / (double)User.MaxExperience);
            ExperienceLabel.Location = new Point((ExperienceBar.Size.Width / 2) - -510, -10);
            GoldLabel.Text = GameScene.Gold.ToString("###,###,##0");
            CharacterName.Text = User.Name;
            SpaceLabel.Text = User.Inventory.Count(t => t == null).ToString();
            WeightLabel.Text = (MapObject.User.MaxBagWeight - MapObject.User.CurrentBagWeight).ToString();
            DateTime timeNow = DateTime.UtcNow;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(Settings.ServerTimeZone);
            timeNow = TimeZoneInfo.ConvertTimeFromUtc(timeNow, zone);
            TimeLabel.Text = string.Format("{0:dd/MM/yy HH:mm:ss}", timeNow);
            TimeLabel.AutoSize = true;
        }

        private void Label_SizeChanged(object sender, EventArgs e)
        {

            if (!(sender is MirLabel l)) return;

            l.Location = new Point(50 - (l.Size.Width / 2), l.Location.Y);
        }

        public int orbEffect0Index = 2493;
        public int orbEffectHPIndex = 2517;
        public int orbEffectMPIndex = 2537;
        public long nextIndexChange;
        private void HealthOrb_BeforeDraw(object sender, EventArgs e)
        {
            if (Libraries.CustomPrguse == null) return;
            if (User == null) return;
            if (CMain.Time > nextIndexChange)
            {
                orbEffect0Index++;
                if (orbEffect0Index == 2512)
                    orbEffect0Index = 2493;
                orbEffectHPIndex++;
                if (orbEffectHPIndex == 2531)
                    orbEffectHPIndex = 2517;
                orbEffectMPIndex++;
                if (orbEffectMPIndex == 2551)
                    orbEffectMPIndex = 2537;

                nextIndexChange = CMain.Time + 400;
            }


            int height;
            if (User != null && User.HP != User.MaxHP)
                height = (int)(94 * User.HP / (float)User.MaxHP);
            else
                height = 94;

            if (height < 0) height = 0;
            if (height > 94) height = 94;

            int orbImage = 4;

            bool hpOnly = false;

            if (HPOnly)
            {
                hpOnly = true;
                orbImage = 6;
            }
            #region HP Drawing
            Rectangle r = new Rectangle(0, 94 - height, hpOnly ? 94 : 50, height);
            Libraries.CustomPrguse.Draw(orbImage, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 46, HealthOrb.DisplayLocation.Y + 94 - height), Color.White, false);
            Libraries.CustomPrguse.DrawBlend(orbEffectHPIndex, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 46, HealthOrb.DisplayLocation.Y + 94 - height), Color.White, false, 0.9f);


            #endregion
            if (hpOnly) return;

            if (User.MP != User.MaxMP)
                height = (int)(94 * User.MP / (float)User.MaxMP);
            else
                height = 94;

            if (height < 0) height = 0;
            if (height > 94) height = 94;
            r = new Rectangle(51, 94 - height, 50, height);

            Libraries.CustomPrguse.Draw(4, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 96, HealthOrb.DisplayLocation.Y + 94 - height), Color.White, false);
            Libraries.CustomPrguse.DrawBlend(orbEffectMPIndex, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 96, HealthOrb.DisplayLocation.Y + 94 - height), Color.White, false, 0.9f);
        }

        private void ExperienceBar_BeforeDraw(object sender, EventArgs e)
        {
            if (ExperienceBar.Library == null) return;

            double percent = MapObject.User.Experience / (double)MapObject.User.MaxExperience;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((ExperienceBar.Size.Width - 3) * percent), ExperienceBar.Size.Height)
            };

            ExperienceBar.Library.Draw(ExperienceBar.Index, section, ExperienceBar.DisplayLocation, Color.White, false);
        }

    }

    public sealed class ChatDialog : MirImageControl
    {
        public List<ChatHistory> FullHistory = new List<ChatHistory>();
        public List<ChatHistory> History = new List<ChatHistory>();
        public List<MirLabel> ChatLines = new List<MirLabel>();

        public MirButton HomeButton, UpButton, EndButton, DownButton, PositionBar;
        public MirImageControl CountBar;
        public MirTextBox ChatTextBox;
        public Font ChatFont = new Font(Settings.FontName, 8F);
        public string LastPM = string.Empty;
        public List<UserItem> ChatItems = new List<UserItem>();
        public int StartIndex, LineCount = 6, WindowSize;
        public string ChatPrefix = "";

        public bool Transparent;

        public ChatDialog()
        {
            Index = Settings.Resolution != 800 ? 2221 : 2201;
            Library = Libraries.CustomPrguse;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 202, Settings.ScreenHeight - 103);
            PixelDetect = true;

            KeyPress += ChatPanel_KeyPress;
            KeyDown += ChatPanel_KeyDown;
            MouseWheel += ChatPanel_MouseWheel;

            ChatTextBox = new MirTextBox
            {
                BackColour = Color.DarkGray,
                ForeColour = Color.Black,
                Parent = this,
                Size = new Size((Settings.Resolution != 800) ? 613 : 403, 13),
                Location = new Point(1, 79),
                MaxLength =Globals.MaxChatLength,
                Visible = false,
                Font = ChatFont,
            };
            ChatTextBox.TextBox.KeyPress += ChatTextBox_KeyPress;
            ChatTextBox.TextBox.KeyDown += ChatTextBox_KeyDown;
            ChatTextBox.TextBox.KeyUp += ChatTextBox_KeyUp;

            HomeButton = new MirButton
            {
                Index = 2018,
                HoverIndex = 2019,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 620 : 394, 9),
                Parent = this,
                PressedIndex = 2020,
                Sound = SoundList.ButtonA,
            };
            HomeButton.Click += (o, e) =>
            {
                if (StartIndex == 0) return;
                StartIndex = 0;
                Update();
            };


            UpButton = new MirButton
            {
                Index = 2021,
                HoverIndex = 2022,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 620 : 394, 17),
                Parent = this,
                PressedIndex = 2023,
                Sound = SoundList.ButtonA,
            };
            UpButton.Click += (o, e) =>
            {
                if (StartIndex == 0) return;
                StartIndex--;
                Update();
            };


            EndButton = new MirButton
            {
                Index = 2027,
                HoverIndex = 2028,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 620 : 394, 77),
                Parent = this,
                PressedIndex = 2029,
                Sound = SoundList.ButtonA,
            };
            EndButton.Click += (o, e) =>
            {
                if (StartIndex == History.Count - 1) return;
                StartIndex = History.Count - 1;
                Update();
            };

            DownButton = new MirButton
            {
                Index = 2024,
                HoverIndex = 2025,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 620 : 394, 71),
                Parent = this,
                PressedIndex = 2026,
                Sound = SoundList.ButtonA,
            };
            DownButton.Click += (o, e) =>
            {
                if (StartIndex == History.Count - 1) return;
                StartIndex++;
                Update();
            };



            CountBar = new MirImageControl
            {
                Index = 2012,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 624 : 398, 23),
                Parent = this,
            };

            PositionBar = new MirButton
            {
                Index = 2015,
                HoverIndex = 2016,
                Library = Libraries.CustomPrguse,
                Location = new Point(Settings.Resolution != 800 ? 621 : 395, 23),
                Parent = this,
                PressedIndex = 2017,
                Movable = true,
                Sound = SoundList.None,
            };
            PositionBar.OnMoving += PositionBar_OnMoving;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        private void ChatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    e.Handled = true;
                    if (!string.IsNullOrEmpty(ChatTextBox.Text))
                    {
                        string msg = ChatTextBox.Text;

                        if (msg.ToUpper() == "@LEVELEFFECT")
                        {
                            Settings.LevelEffect = !Settings.LevelEffect;
                        }

                        if (msg.ToUpper() == "@TARGETDEAD")
                        {
                            Settings.TargetDead = !Settings.TargetDead;
                        }

                        Network.Enqueue(new C.Chat
                        {
                            Message = msg
                        });

                        if (ChatTextBox.Text[0] == '/')
                        {
                            string[] parts = ChatTextBox.Text.Split(' ');
                            if (parts.Length > 0)
                                LastPM = parts[0];
                        }
                    }
                    ChatTextBox.Visible = false;
                    ChatTextBox.Text = string.Empty;
                    break;
                case (char)Keys.Escape:
                    e.Handled = true;
                    ChatTextBox.Visible = false;
                    ChatTextBox.Text = string.Empty;
                    break;
            }
        }

        void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int x = (Settings.Resolution != 800) ? 621 : 395;
            int y = PositionBar.Location.Y;
            if (y >= 23 + CountBar.Size.Height - PositionBar.Size.Height) y = 16 + CountBar.Size.Height - PositionBar.Size.Height;
            if (y < 23) y = 23;

            int h = CountBar.Size.Height - PositionBar.Size.Height;
            h = (int)((y - 23) / (h / (float)(History.Count - 1)));

            if (h != StartIndex)
            {
                StartIndex = h;
                Update();
            }

            PositionBar.Location = new Point(x, y);
        }

        public void ReceiveChat(string text, ChatType type, List<UserItem> chatItemList = null)
        {
            Color foreColour, backColour;

            switch (type)
            {
                case ChatType.Hint:
                    backColour = Color.White;
                    foreColour = Color.DarkGreen;
                    break;
                case ChatType.Announcement:
                    backColour = Color.Blue;
                    foreColour = Color.White;
                    GameScene.Scene.ChatNoticeDialog.ShowNotice(text);
                    break;
                case ChatType.Event:
                    backColour = Color.Blue;
                    foreColour = Color.White;
                    GameScene.Scene.TimerDialog.ShowNotice(text, 0, 10);
                    return;
                case ChatType.Shout:
                    backColour = Color.Yellow;
                    foreColour = Color.Black;
                    break;
                case ChatType.Shout2:
                    backColour = Color.Green;
                    foreColour = Color.White;
                    break;
                case ChatType.Shout3:
                    backColour = Color.Purple;
                    foreColour = Color.White;
                    break;
                case ChatType.System:
                    backColour = Color.Red;
                    foreColour = Color.White;
                    break;
                case ChatType.System2:
                    backColour = Color.DarkRed;
                    foreColour = Color.White;
                    break;
                case ChatType.Group:
                    backColour = Color.White;
                    foreColour = Color.Brown;
                    break;
                case ChatType.WhisperOut:
                    foreColour = Color.CornflowerBlue;
                    backColour = Color.White;
                    break;
                case ChatType.WhisperIn:
                    foreColour = Color.DarkBlue;
                    backColour = Color.White;
                    break;
                case ChatType.Guild:
                    backColour = Color.White;
                    foreColour = Color.Green;
                    break;
                case ChatType.LevelUp:
                    backColour = Color.FromArgb(255, 225, 185, 250);
                    foreColour = Color.Blue;
                    break;
                case ChatType.Relationship:
                    backColour = Color.Transparent;
                    foreColour = Color.HotPink;
                    break;
                case ChatType.Mentor:
                    backColour = Color.White;
                    foreColour = Color.Purple;
                    break;
                default:
                    backColour = Color.White;
                    foreColour = Color.Black;
                    break;
            }

            int chatWidth = Settings.Resolution > 800 ? 598 : 390;
            List<string> chat = new List<string>();

            string[] output = text.Split('(', ')').Distinct().ToArray();
            if (ChatItems != null)
            {
                foreach (var o in output)
                {
                    if (ulong.TryParse(o, out ulong id))
                    {

                        UserItem tmp = null;
                        for (int i = 0; i < ChatItems.Count; i++)
                            if (ChatItems[i].UniqueID == id)
                                tmp = ChatItems[i];
                        if (tmp != null && tmp.Info == null)
                        {
                            var item = GameScene.ItemInfoList.FirstOrDefault(x => x.Index == tmp.ItemIndex);
                            tmp.Info = item;
                        }
                        if (tmp != null)
                        {
                            var indexof = text.IndexOf("(" + o + ")");
                            if (indexof < 0) continue;

                            var spaceSize = TextRenderer.MeasureText(CMain.Graphics, " ", ChatFont);
                            var specialItemSize = TextRenderer.MeasureText(CMain.Graphics, tmp.UniqueID.ToString(), new Font(Settings.FontName, 8F, FontStyle.Bold));
                            var normalItemSize = TextRenderer.MeasureText(CMain.Graphics, tmp.UniqueID.ToString(), ChatFont);

                            float result = (specialItemSize.Width - normalItemSize.Width) / (float)spaceSize.Width;
                            var space = new String(' ', 6 + (int)Math.Ceiling(result));

                            text = text.Replace("(" + o + ")", "[" + tmp.UniqueID + "]" + space);

                        }
                    }
                }
            }

            int index = 0;
            byte opened = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == '[') opened++;
                if (text[i] == ']') opened--;

                if (TextRenderer.MeasureText(CMain.Graphics, text.Substring(index, i - index), ChatFont).Width > chatWidth && opened == 0)
                {
                    chat.Add(text.Substring(index, i - index + 1));
                    index = i + 1;
                }
            }
            chat.Add(text.Substring(index, text.Length - index));

            if (StartIndex == History.Count - LineCount)
                StartIndex += chat.Count;

            for (int i = 0; i < chat.Count; i++)
                FullHistory.Add(new ChatHistory { Text = chat[i], BackColour = backColour, ForeColour = foreColour, Type = type });

            Update();
        }

        public void Update()
        {
            History = new List<ChatHistory>();

            for (int i = 0; i < FullHistory.Count; i++)
            {
                switch (FullHistory[i].Type)
                {
                    case ChatType.Normal:
                        if (Settings.FilterNormalChat) continue;
                        break;
                    case ChatType.WhisperIn:
                    case ChatType.WhisperOut:
                        if (Settings.FilterWhisperChat) continue;
                        break;
                    case ChatType.Shout:
                        if (Settings.FilterShoutChat) continue;
                        break;
                    case ChatType.System:
                    case ChatType.System2:
                        if (Settings.FilterSystemChat) continue;
                        break;
                    case ChatType.Group:
                        if (Settings.FilterGroupChat) continue;
                        break;
                    case ChatType.Guild:
                        if (Settings.FilterGuildChat) continue;
                        break;
                }

                History.Add(FullHistory[i]);
            }

            for (int i = 0; i < ChatLines.Count; i++)
                ChatLines[i].Dispose();

            ChatLines.Clear();

            if (StartIndex >= History.Count) StartIndex = History.Count - 1;
            if (StartIndex < 0) StartIndex = 0;

            if (History.Count > 1)
            {
                int h = CountBar.Size.Height - PositionBar.Size.Height;
                h = (int)((h / (float)(History.Count - 1)) * StartIndex);
                PositionBar.Location = new Point(Settings.Resolution != 800 ? 621 : 395, 23 + h);
            }

            int y = 1;

            for (int i = StartIndex; i < History.Count; i++)
            {
                var tempHistory = History[i].Text;


                MirLabel temp = new MirLabel
                {
                    AutoSize = true,                    
                    BackColour = History[i].BackColour,
                    ForeColour = History[i].ForeColour,
                    Location = new Point(1, y),
                    OutLine = false,
                    Parent = this,
                    Text = History[i].Text,
                    Font = ChatFont,
                };
                temp.MouseWheel += ChatPanel_MouseWheel;
                ChatLines.Add(temp);

                temp.Click += (o, e) =>
                {
                    MirLabel l = o as MirLabel;
                    if (l == null) return;

                    string[] parts = l.Text.Split(':', ' ');
                    if (parts.Length == 0) return;

                    string name = Regex.Replace(parts[0], "[^A-Za-z0-9]", "");

                    ChatTextBox.SetFocus();
                    ChatTextBox.Text = string.Format("/{0} ", name);
                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                };



                string[] output = History[i].Text.Split('[', ']');

                if (ChatItems != null)
                {
                    foreach (var o in output)
                    {
                        UserItem tmp = null;
                        for (int x = 0; x < ChatItems.Count; x++)
                        {
                            if (ChatItems[x].UniqueID.ToString() == o)
                            {
                                tmp = ChatItems[x];
                            }
                        }
                        if (tmp != null)
                        {
                            ItemInfo item = GameScene.ItemInfoList.FirstOrDefault(x => x.Index == tmp.ItemIndex);
                            tmp.Info = item;
                            if (tmp != null)
                            {
                                var indexof = tempHistory.IndexOf("[" + o + "]");
                                if (indexof < 0) continue;
                                var realSize = TextRenderer.MeasureText(CMain.Graphics, tempHistory.Substring(0, indexof), ChatFont);
                                MirLabelItem SpecialTemp = new MirLabelItem
                                {
                                    AutoSize = true,
                                    BackColour = History[i].BackColour,
                                    ForeColour = Color.Red,
                                    Location = new Point(realSize.Width - 2, y),
                                    Parent = this,
                                    OutLine = false,
                                    Text = tmp.FriendlyName,
                                    Font = new Font(Settings.FontName, 8F, FontStyle.Bold),
                                    Item = tmp
                                };
                                SpecialTemp.MouseWheel += SpecialChatPanel_MouseWheel;
                                ChatLines.Add(SpecialTemp);

                                tempHistory = Functions.ReplaceFirst(tempHistory, "[" + o + "]", o);

                                temp.Text = tempHistory;

                            }
                        }


                    }
                }
                y += 13;
                if (i - StartIndex == LineCount - 1) break;
            }

            GameScene.Scene.DisposeItemLabel();
            GameScene.HoverItem = null;

        }

        private void ChatPanel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (StartIndex == 0) return;
                    StartIndex--;
                    break;
                case Keys.Home:
                    if (StartIndex == 0) return;
                    StartIndex = 0;
                    break;
                case Keys.Down:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex++;
                    break;
                case Keys.End:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex = History.Count - 1;
                    break;
                case Keys.PageUp:
                    if (StartIndex == 0) return;
                    StartIndex -= LineCount;
                    break;
                case Keys.PageDown:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex += LineCount;
                    break;
                default:
                    return;
            }

            Update();
            e.Handled = true;
        }
        private void ChatPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '@':
                case '!':
                case ' ':
                case (char)Keys.Enter:
                    ChatTextBox.SetFocus();
                    if (e.KeyChar == '!') ChatTextBox.Text = "!";
                    if (e.KeyChar == '@') ChatTextBox.Text = "@";
                    if (ChatPrefix != "") ChatTextBox.Text = ChatPrefix;

                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                    e.Handled = true;
                    break;
                case '/':
                    ChatTextBox.SetFocus();
                    ChatTextBox.Text = LastPM + " ";
                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                    e.Handled = true;
                    break;
            }
        }
        private void ChatPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (StartIndex == 0 && count >= 0) return;
            if (StartIndex == History.Count - 1 && count <= 0) return;

            StartIndex -= count;
            Update();
        }

        private void SpecialChatPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (StartIndex == 0 && count >= 0) return;
            if (StartIndex == History.Count - 1 && count <= 0) return;

            StartIndex -= count;
            Update();

            GameScene.Scene.DisposeItemLabel();
            GameScene.HoverItem = null;

        }


        private void ChatTextBox_KeyUp(object sender, KeyEventArgs e)
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
                    CMain.CMain_KeyUp(sender, e);
                    break;

            }
        }
        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
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
                    CMain.CMain_KeyDown(sender, e);
                    break;

            }
        }


        public void ChangeSize()
        {
            if (++WindowSize >= 3) WindowSize = 0;

            int y = DisplayRectangle.Bottom;
            switch (WindowSize)
            {
                case 0:
                    LineCount = 6;
                    Index = Settings.Resolution != 800 ? 2221 : 2201;
                    CountBar.Index = 2012;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45);
                    //ChatTextBox.Location = new Point(1, 54);
                    break;
                case 1:
                    LineCount = 7;
                    Index = Settings.Resolution != 800 ? 2224 : 2204;
                    CountBar.Index = 2013;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39 + 87);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45 + 93);
                    ChatTextBox.Location = new Point(1, 102);
                    break;
                case 2:
                    LineCount = 11;
                    Index = Settings.Resolution != 800 ? 2227 : 2207;
                    CountBar.Index = 2014;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 135);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 141);
                    ChatTextBox.Location = new Point(1, 150);
                    break;
            }

            Location = new Point(Location.X, y - Size.Height);

            UpdateBackground();

            Update();
        }

        public void UpdateBackground()
        {
            int offset = Transparent ? 1 : 0;

            switch (WindowSize)
            {
                case 0:
                    Index = Settings.Resolution != 800 ? 2221 : 2201;
                    break;
                case 1:
                    Index = Settings.Resolution != 800 ? 2224 : 2204;
                    break;
                case 2:
                    Index = Settings.Resolution != 800 ? 2227 : 2207;
                    break;
            }

            Index -= offset;
        }

        public class ChatHistory
        {
            public string Text;
            public Color ForeColour, BackColour;
            public ChatType Type;
        }
    }

    public sealed class ChatControlBar : MirImageControl
    {
        public MirButton DuraButton, HeroDuraButton, SettingsButton, NormalButton, ShoutButton, WhisperButton, LoverButton, MentorButton, GroupButton, GuildButton, ReportButton;

        public MirButton WebButton;

        public ChatControlBar()
        {
            Index = Settings.Resolution != 800 ? 2034 : 2035;
            Library = Libraries.CustomPrguse;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 205, Settings.ScreenHeight - 119);

            SettingsButton = new MirButton
            {
                Index = 2060,
                HoverIndex = 2061,
                PressedIndex = 2062,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 596 : 372, 1),
                Sound = SoundList.ButtonA,
                Hint = "Chat Settings"
            };
            SettingsButton.Click += (o, e) =>
            {
                if (GameScene.Scene.ChatOptionDialog.Visible)
                    GameScene.Scene.ChatOptionDialog.Hide();
                else
                    GameScene.Scene.ChatOptionDialog.Show();
            };

            NormalButton = new MirButton
            {
                Index = 2036,
                HoverIndex = 2037,
                PressedIndex = 2038,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(12, 1),
                Sound = SoundList.ButtonA,
                Hint = "All"
            };
            NormalButton.Click += (o, e) =>
            {
                ToggleChatFilter("All");
            };

            ShoutButton = new MirButton
            {
                Index = 2039,
                HoverIndex = 2040,
                PressedIndex = 2041,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(34, 1),
                Sound = SoundList.ButtonA,
                Hint = "Shout"
            };
            ShoutButton.Click += (o, e) =>
            {
                ToggleChatFilter("Shout");
            };

            WhisperButton = new MirButton
            {
                Index = 2042,
                HoverIndex = 2043,
                PressedIndex = 2044,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(56, 1),
                Sound = SoundList.ButtonA,
                Hint = "Whisper"
            };
            WhisperButton.Click += (o, e) =>
            {
                ToggleChatFilter("Whisper");
            };

            LoverButton = new MirButton
            {
                Index = 2045,
                HoverIndex = 2046,
                PressedIndex = 2047,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(78, 1),
                Sound = SoundList.ButtonA,
                Hint = "Lover"
            };
            LoverButton.Click += (o, e) =>
            {
                ToggleChatFilter("Lover");
            };

            MentorButton = new MirButton
            {
                Index = 2048,
                HoverIndex = 2049,
                PressedIndex = 2050,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(100, 1),
                Sound = SoundList.ButtonA,
                Hint = "Mentor"
            };
            MentorButton.Click += (o, e) =>
            {
                ToggleChatFilter("Mentor");
            };

            GroupButton = new MirButton
            {
                Index = 2051,
                HoverIndex = 2052,
                PressedIndex = 2053,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(122, 1),
                Sound = SoundList.ButtonA,
                Hint = "Group"
            };
            GroupButton.Click += (o, e) =>
            {
                ToggleChatFilter("Group");
            };

            GuildButton = new MirButton
            {
                Index = 2054,
                HoverIndex = 2055,
                PressedIndex = 2056,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(144, 1),
                Sound = SoundList.ButtonA,
                Hint = "Guild"
            };
            GuildButton.Click += (o, e) =>
            {
                Settings.ShowGuildChat = !Settings.ShowGuildChat;
                ToggleChatFilter("Guild");
            };

            WebButton = new MirButton
            {
                Index = 2070,
                PressedIndex = 2071,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(166, 1),
                Sound = SoundList.ButtonA,
                Hint = "Web Link"
            };
            WebButton.Click += (o, e) =>
            {
                System.Diagnostics.Process.Start(Settings.C_WebLink);
            };

            ReportButton = new MirButton
            {
                Index = 2084,
                PressedIndex = 2085,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 580 : 356, 1),
                Sound = SoundList.ButtonA,
                Hint = "Report Bug",
                Visible = true
            };
            ReportButton.Click += (o, e) => GameScene.Scene.MailComposeLetterDialog.ComposeMail("StormHero,EdensDev");

            DuraButton = new MirButton
            {
                Index = 2055,
                HoverIndex = 2056,
                PressedIndex = 2054,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 558 : 334, 1),
                Sound = SoundList.ButtonA,
                Hint = "Player Dura Panel",
                Visible = true,
            };
            DuraButton.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDuraPanel.Visible == true)
                {
                    GameScene.Scene.CharacterDuraPanel.Hide();
                    Settings.DuraView = false;
                }
                else
                {
                    GameScene.Scene.CharacterDuraPanel.Show();
                    Settings.DuraView = true;
                }
            };

            HeroDuraButton = new MirButton
            {
                Index = 2055,
                HoverIndex = 2056,
                PressedIndex = 2054,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 536 : 284, 1),
                Sound = SoundList.ButtonA,
                Hint = "Hero Dura Panel",
                Visible = false,
            };
            HeroDuraButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HeroCharacterDuraPanel.Visible)
                    GameScene.Scene.HeroCharacterDuraPanel.Hide();
                else
                    GameScene.Scene.HeroCharacterDuraPanel.Show();
            };
        }

        public void ToggleChatFilter(string chatFilter)
        {
            NormalButton.Index = 2036;
            NormalButton.HoverIndex = 2037;
            ShoutButton.Index = 2039;
            ShoutButton.HoverIndex = 2040;
            WhisperButton.Index = 2042;
            WhisperButton.HoverIndex = 2043;
            LoverButton.Index = 2045;
            LoverButton.HoverIndex = 2046;
            MentorButton.Index = 2048;
            MentorButton.HoverIndex = 2049;
            GroupButton.Index = 2051;
            GroupButton.HoverIndex = 2052;
            GuildButton.Index = 2054;
            GuildButton.HoverIndex = 2055;

            GameScene.Scene.ChatDialog.ChatPrefix = "";

            switch (chatFilter)
            {
                case "All":
                    NormalButton.Index = 2038;
                    NormalButton.HoverIndex = 2038;
                    GameScene.Scene.ChatDialog.ChatPrefix = "";
                    break;
                case "Shout":
                    ShoutButton.Index = 2041;
                    ShoutButton.HoverIndex = 2041;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!";
                    break;
                case "Whisper":
                    WhisperButton.Index = 2044;
                    WhisperButton.HoverIndex = 2044;
                    GameScene.Scene.ChatDialog.ChatPrefix = "/";
                    break;
                case "Group":
                    GroupButton.Index = 2053;
                    GroupButton.HoverIndex = 2053;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!!";
                    break;
                case "Guild":
                    GuildButton.Index = 2056;
                    GuildButton.HoverIndex = 2056;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!~";
                    break;
                case "Lover":
                    LoverButton.Index = 2047;
                    LoverButton.HoverIndex = 2047;
                    GameScene.Scene.ChatDialog.ChatPrefix = ":)";
                    break;
                case "Mentor":
                    MentorButton.Index = 2050;
                    MentorButton.HoverIndex = 2050;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!#";
                    break;
            }
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

    public sealed class InventoryDialog : MirImageControl
    {
        public MirImageControl WeightBar;
        public MirImageControl[] LockBar = new MirImageControl[10];
        public MirItemCell[] Grid;
        public MirItemCell[] QuestGrid;

        public MirButton CloseButton, ItemButton, ItemButton2, QuestButton, AddButton;
        public MirLabel GoldLabel, WeightLabel;

        public InventoryDialog()
        {
            Index = 196;
            Library = Libraries.CustomTitle;
            Movable = true;
            Sort = true;
            Visible = false;

            WeightBar = new MirImageControl
            {
                Index = 24,
                Library = Libraries.CustomPrguse,
                Location = new Point(182, 217),
                Parent = this,
                DrawImage = false,
                NotControl = true,
            };

            ItemButton = new MirButton
            {
                Index = 197,
                Library = Libraries.CustomTitle,
                Location = new Point(6, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            ItemButton.Click += Button_Click;

            ItemButton2 = new MirButton
            {
                Index = 738,
                Library = Libraries.CustomTitle,
                Location = new Point(76, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            ItemButton2.Click += Button_Click;

            QuestButton = new MirButton
            {
                Index = 739,
                Library = Libraries.CustomTitle,
                Location = new Point(146, 7),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
            };
            QuestButton.Click += Button_Click;

            AddButton = new MirButton
            {
                Index = 483,
                HoverIndex = 484,
                PressedIndex = 485,
                Library = Libraries.CustomTitle,
                Location = new Point(235, 5),
                Parent = this,
                Size = new Size(72, 23),
                Sound = SoundList.ButtonA,
                Visible = false,
            };
            AddButton.Click += (o1, e) =>
            {
                int openLevel = (GameScene.User.Inventory.Length - 46) / 4;
                int openGold = (1000000 + openLevel * 1000000);
                MirMessageBox messageBox = new MirMessageBox(string.Format("Are you sure you would like to unlock 4 extra slots for {0:###,###} gold ?\n" +
                                                    "This will take your inventory space up to {1} slots in total.", openGold, GameScene.User.Inventory.Length + 4), MirMessageBoxButtons.OKCancel);

                messageBox.OKButton.Click += (o, a) =>
                {
                    Network.Enqueue(new C.Chat { Message = "@ADDINVENTORY" });
                };
                messageBox.Show();
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(289, 3),
                Library = Libraries.CustomPrguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            GoldLabel = new MirLabel
            {
                Parent = this,
                Location = new Point(40, 212),
                Size = new Size(111, 14),
                Sound = SoundList.Gold,
            };
            GoldLabel.Click += (o, e) =>
            {
                if (GameScene.SelectedCell == null)
                    GameScene.PickedUpGold = !GameScene.PickedUpGold && GameScene.Gold > 0;
            };


            Grid = new MirItemCell[8 * 10];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    int idx = 8 * y + x;
                    Grid[idx] = new MirItemCell
                    {
                        ItemSlot = 6 + idx,
                        GridType = MirGridType.Inventory,
                        Library = Libraries.Items,
                        Parent = this,
                        Location = new Point(x * 36 + 9 + x, y % 5 * 32 + 37 + y % 5),
                    };

                    if (idx >= 40)
                        Grid[idx].Visible = false;
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
                        GridType = MirGridType.QuestInventory,
                        Library = Libraries.Items,
                        Parent = this,
                        Location = new Point(x * 36 + 9 + x, y * 32 + 37 + y),
                        Visible = false
                    };
                }
            }

            WeightLabel = new MirLabel
            {
                Parent = this,
                Location = new Point(268, 212),
                AutoSize = true
            };
            WeightBar.BeforeDraw += WeightBar_BeforeDraw;

            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i] = new MirImageControl
                {
                    Index = 307,
                    Library = Libraries.CustomPrguse2,
                    Location = new Point(9 + i % 2 * 148, 37 + i / 2 * 33),
                    Parent = this,
                    DrawImage = true,
                    NotControl = true,
                    Visible = false,
                };
            }

        }

        void Button_Click(object sender, EventArgs e)
        {
            if (GameScene.User.Inventory.Length == 46 && sender == ItemButton2)
            {
                MirMessageBox messageBox = new MirMessageBox("Are you sure you would like to buy 8 extra slots for 1,000,000 gold?\n" +
                    "Next purchase you can unlock 4 extra slots up to a maximum of 40 slots.", MirMessageBoxButtons.OKCancel);

                messageBox.OKButton.Click += (o, a) =>
                {
                    Network.Enqueue(new C.Chat { Message = "@ADDINVENTORY" });
                };
                messageBox.Show();
            }
            else
            {
                if (sender == ItemButton)
                {
                    RefreshInventory();
                }
                else if (sender == ItemButton2)
                {
                    RefreshInventory2();
                }
                else if (sender == QuestButton)
                {
                    Reset();

                    ItemButton.Index = 737;
                    ItemButton2.Index = 738;
                    QuestButton.Index = 198;

                    if (GameScene.User.Inventory.Length == 46)
                    {
                        ItemButton2.Index = 169;
                    }

                    foreach (var grid in QuestGrid)
                    {
                        grid.Visible = true;
                    }
                }
            }
        }

        void Reset()
        {
            foreach (MirItemCell grid in QuestGrid)
            {
                grid.Visible = false;
            }

            foreach (MirItemCell grid in Grid)
            {
                grid.Visible = false;
            }

            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i].Visible = false;
            }

            AddButton.Visible = false;
        }



        public void RefreshInventory()
        {
            Reset();

            ItemButton.Index = 197;
            ItemButton2.Index = 738;
            QuestButton.Index = 739;

            if (GameScene.User.Inventory.Length == 46)
            {
                ItemButton2.Index = 169;
            }

            foreach (var grid in Grid)
            {
                if (grid.ItemSlot < 46)
                    grid.Visible = true;
                else
                    grid.Visible = false;
            }
        }

        public void RefreshInventory2()
        {
            Reset();

            ItemButton.Index = 737;
            ItemButton2.Index = 168;
            QuestButton.Index = 739;

            foreach (var grid in Grid)
            {
                if (grid.ItemSlot < 46 || grid.ItemSlot >= GameScene.User.Inventory.Length)
                    grid.Visible = false;
                else
                    grid.Visible = true;
            }

            int openLevel = (GameScene.User.Inventory.Length - 46) / 4;
            for (int i = 0; i < LockBar.Length; i++)
            {
                LockBar[i].Visible = (i < openLevel) ? false : true;
            }

            AddButton.Visible = openLevel >= 10 ? false : true;
        }

        public void Process()
        {
            WeightLabel.Text = GameScene.User.Inventory.Count(t => t == null).ToString();
            //WeightLabel.Text = (MapObject.User.MaxBagWeight - MapObject.User.CurrentBagWeight).ToString();
            GoldLabel.Text = GameScene.Gold.ToString("###,###,##0");
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Show()
        {
            Visible = true;

            //RefreshInventory();
        }

        private void WeightBar_BeforeDraw(object sender, EventArgs e)
        {
            if (WeightBar.Library == null) return;

            double percent = MapObject.User.CurrentBagWeight / (double)MapObject.User.MaxBagWeight;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((WeightBar.Size.Width - 3) * percent), WeightBar.Size.Height)
            };

            WeightBar.Library.Draw(WeightBar.Index, section, WeightBar.DisplayLocation, Color.White, false);
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

        public MirItemCell GetQuestCell(ulong id)
        {
            return QuestGrid.FirstOrDefault(t => t.Item != null && t.Item.UniqueID == id);
        }

        public void DisplayItemGridEffect(ulong id, int type = 0)
        {
            MirItemCell cell = GetCell(id);

            if (cell.Item == null) return;

            MirAnimatedControl animEffect = null;

            switch (type)
            {
                case 0:
                    animEffect = new MirAnimatedControl
                    {
                        Animated = true,
                        AnimationCount = 9,
                        AnimationDelay = 150,
                        Index = 410,
                        Library = Libraries.Prguse,
                        Location = cell.Location,
                        Parent = this,
                        Loop = false,
                        NotControl = true,
                        UseOffSet = true,
                        Blending = true,
                    };
                    animEffect.AfterAnimation += (o, e) => animEffect.Dispose();
                    SoundManager.PlaySound(20000 + (ushort)Spell.MagicShield * 10);
                    break;
            }

        }
    }

    public sealed class HeroAIDialog : MirImageControl
    {
        public MirButton HeroHelpButton, HeroGuardButton, HeroOffensiveButton, HeroDefensiveButton, HeroDontAttackButton, HeroDontMoveButton;

        public HeroAIDialog()
        {
            Index = 140;
            Library = Libraries.Prguse2;
            Movable = true;
            Sort = true;
            Visible = false;
            Location = new Point(670, 77);

            HeroHelpButton = new MirButton
            {
                HoverIndex = 142,
                Index = 144,
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(13, 4),
                PressedIndex = 143,
                Hint = "Hero Help"
            };
            HeroHelpButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HeroHelpDialog.Visible)
                    GameScene.Scene.HeroHelpDialog.Hide();
                else
                    GameScene.Scene.HeroHelpDialog.Show();
            };

            HeroGuardButton = new MirButton
            {
                Index = 1840,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(26, 3),
                PressedIndex = 1844,
                Hint = "Hero Guard"
            };
            HeroGuardButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ChangeHMode { Mode = HeroMode.Guard });
            };

            HeroOffensiveButton = new MirButton
            {
                Index = 1842,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(43, 3),
                PressedIndex = 1846,
                Hint = "Hero Offensive"
            };
            HeroOffensiveButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ChangeHMode { Mode = HeroMode.Offensive });
            };

            HeroDefensiveButton = new MirButton
            {
                Index = 1843,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(60, 3),
                PressedIndex = 1847,
                Hint = "Hero Defensive"
            };
            HeroDefensiveButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ChangeHMode { Mode = HeroMode.Defensive });
            };

            HeroDontAttackButton = new MirButton
            {
                Index = 145,
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(78, 3),
                PressedIndex = 147,
                Hint = "Hero Dont Attack",
            };
            HeroDontAttackButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ChangeHMode { Mode = HeroMode.DontAttack });
            };

            HeroDontMoveButton = new MirButton
            {
                Index = 153,
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(95, 3),
                PressedIndex = 155,
                Hint = "Hero Dont Move",
            };
            HeroDontMoveButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ChangeHMode { Mode = HeroMode.DontMove });
            };
        

        }
        public void Hide()
        {
            Visible = false;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Update(HeroMode h)
        {
            switch(h)
            {
                case HeroMode.Defensive:
                    HeroGuardButton.Index = 1840;
                    HeroDontAttackButton.Index = 145;
                    HeroDontMoveButton.Index = 153;
                    HeroDefensiveButton.Index = 1847;
                    HeroOffensiveButton.Index = 1842;
                    break;

                case HeroMode.Offensive:
                    HeroGuardButton.Index = 1840;
                    HeroDontAttackButton.Index = 145;
                    HeroDontMoveButton.Index = 153;
                    HeroDefensiveButton.Index = 1843;
                    HeroOffensiveButton.Index = 1846;
                    break;

                case HeroMode.Guard:
                    HeroGuardButton.Index = 1844;
                    HeroDontAttackButton.Index = 145;
                    HeroDontMoveButton.Index = 153;
                    HeroDefensiveButton.Index = 1843;
                    HeroOffensiveButton.Index = 1842;
                    break;

                case HeroMode.DontMove:
                    HeroGuardButton.Index = 1840;
                    HeroDontAttackButton.Index = 145;
                    HeroDontMoveButton.Index = 155;
                    HeroDefensiveButton.Index = 1843;
                    HeroOffensiveButton.Index = 1842;
                    break;

                case HeroMode.DontAttack:
                    HeroGuardButton.Index = 1840;
                    HeroDontAttackButton.Index = 147;
                    HeroDontMoveButton.Index = 153;
                    HeroDefensiveButton.Index = 1843;
                    HeroOffensiveButton.Index = 1842;
                    break;
            }
        }
    }

    public sealed class BeltDialog : MirImageControl
    {
        public MirLabel[] Key = new MirLabel[6];
        public MirItemCell[] Grid;

        public BeltDialog()
        {
            Index = 1932;
            Library = Libraries.CustomPrguse;
            Movable = true;
            Sort = true;
            Visible = true;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 203, Settings.ScreenHeight - 162);

            BeforeDraw += BeltPanel_BeforeDraw;

            for (int i = 0; i < Key.Length; i++)
            {
                Key[i] = new MirLabel
                {
                    Parent = this,
                    Size = new Size(26, 14),
                    Location = new Point(50 + i * 35, 5),
                    Text = (i + 1).ToString()
                };
            }

            Grid = new MirItemCell[6];

            for (int x = 0; x < 6; x++)
            {
                Grid[x] = new MirItemCell
                {
                    ItemSlot = x,
                    Size = new Size(32, 32),
                    GridType = MirGridType.Inventory,
                    Library = Libraries.Items,
                    Parent = this,
                    Location = new Point(x * 35 + 50, 6)
                };
            }

        }

        private void BeltPanel_BeforeDraw(object sender, EventArgs e)
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


        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }
    }
    public sealed class SkillBarDialog : MirImageControl
    {
        private readonly MirButton _switchBindsButton;

        public bool AltBind;
        public bool HasSkill = false;
        public byte BarIndex;

        //public bool TopBind = !Settings.SkillMode;
        public MirImageControl[] Cells = new MirImageControl[8];
        public MirLabel[] KeyNameLabels = new MirLabel[8];
        public MirLabel BindNumberLabel = new MirLabel();

        public MirAnimatedControl[] CoolDowns = new MirAnimatedControl[8];

        public SkillBarDialog()
        {
            Index = 2190;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;
            Location = new Point(0, BarIndex * 20);
            Visible = true;

            BeforeDraw += MagicKeyDialog_BeforeDraw;

            _switchBindsButton = new MirButton
            {
                Index = 2247,
                Library = Libraries.Prguse,
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(16, 28),
                Location = new Point(0, 0)
            };
            _switchBindsButton.Click += (o, e) =>
            {
                //Settings.SkillSet = !Settings.SkillSet;

                Update();
            };

            for (var i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new MirImageControl
                {
                    Index = -1,
                    Library = Libraries.MagIcon,
                    Parent = this,
                    Location = new Point(i * 25 + 15, 3),
                };
                int j = i + 1;
                Cells[i].Click += (o, e) =>
                    {
                        GameScene.Scene.UseSpell(j + (8 * BarIndex));
                    };

                CoolDowns[i] = new MirAnimatedControl
                {
                    Library = Libraries.Prguse2,
                    Parent = this,
                    Location = new Point(i * 25 + 15, 3),
                    NotControl = true,
                    UseOffSet = true,
                    Loop = false,
                    Animated = false,
                    Opacity = 0.6F
                };
            }

            BindNumberLabel = new MirLabel
            {
                Text = "1",
                Font = new Font(Settings.FontName, 8F),
                ForeColour = Color.White,
                Parent = this,
                Location = new Point(0, 1),
                Size = new Size(10, 25),
                NotControl = true
            };

            for (var i = 0; i < KeyNameLabels.Length; i++)
            {
                KeyNameLabels[i] = new MirLabel
                {
                    Text = "F" + (i + 1),
                    Font = new Font(Settings.FontName, 8F),
                    ForeColour = Color.White,
                    Parent = this,
                    Location = new Point(i * 25 + 13, 0),
                    Size = new Size(25, 25),
                    NotControl = true
                };
            }
            OnMoving += SkillBar_OnMoving;
        }

        private void SkillBar_OnMoving(object sender, MouseEventArgs e)
        {
            if (BarIndex * 2 >= Settings.SkillbarLocation.Length) return;
            Settings.SkillbarLocation[BarIndex, 0] = Location.X;
            Settings.SkillbarLocation[BarIndex, 1] = Location.Y;
        }

        private Keys GetKey(int barindex, int i)
        {
            //KeybindOptions Type = KeybindOptions.Bar1Skill1;
            if ((barindex == 0) && (i == 1))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1);
            if ((barindex == 0) && (i == 2))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill2);
            if ((barindex == 0) && (i == 3))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill3);
            if ((barindex == 0) && (i == 4))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill4);
            if ((barindex == 0) && (i == 5))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill5);
            if ((barindex == 0) && (i == 6))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill6);
            if ((barindex == 0) && (i == 7))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill7);
            if ((barindex == 0) && (i == 8))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8);
            if ((barindex == 1) && (i == 1))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill1);
            if ((barindex == 1) && (i == 2))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill2);
            if ((barindex == 1) && (i == 3))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill3);
            if ((barindex == 1) && (i == 4))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill4);
            if ((barindex == 1) && (i == 5))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill5);
            if ((barindex == 1) && (i == 6))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill6);
            if ((barindex == 1) && (i == 7))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill7);
            if ((barindex == 1) && (i == 8))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill8);
            return Keys.None;
        }


        void MagicKeyDialog_BeforeDraw(object sender, EventArgs e)
        {
            Libraries.Prguse.Draw(2193, new Point(DisplayLocation.X + 12, DisplayLocation.Y), Color.White, true, 0.5F);
        }

        public void Update()
        {
            HasSkill = false;
            foreach (var m in GameScene.User.Magics)
            {
                if ((m.Key < (BarIndex * 8) + 1) || (m.Key > ((BarIndex + 1) * 8) + 1)) continue;
                HasSkill = true;
            }
            if (!Visible) return;
            Index = 2190;
            _switchBindsButton.Index = 2247;
            BindNumberLabel.Text = (BarIndex + 1).ToString();
            BindNumberLabel.Location = new Point(0, 1);

            for (var i = 1; i <= 8; i++)
            {
                Cells[i - 1].Index = -1;

                int offset = BarIndex * 8;
                string key = GetKey(BarIndex, i).ToString();
                KeyNameLabels[i - 1].Text = key;

                foreach (var m in GameScene.User.Magics)
                {
                    if (m.Key != i + offset) continue;
                    HasSkill = true;
                    ClientMagic magic = MapObject.User.GetMagic(m.Spell);
                    if (magic == null) continue;

                    //string key = m.Key > 8 ? string.Format("CTRL F{0}", i) : string.Format("F{0}", m.Key);

                    Cells[i - 1].Index = magic.Icon * 2;
                    string cdStr = "0";
                    if (magic.Delay > 0)
                        cdStr = Functions.PrintTimeSpanFromMilliSeconds(magic.Delay);
                    Cells[i - 1].Hint = string.Format("{0}\nMP: {1}\nCooldown: {2}\nKey: {3}", magic.Spell,
                        (magic.BaseCost + (magic.LevelCost * magic.Level)), cdStr, key);

                    KeyNameLabels[i - 1].Text = "";
                }

                CoolDowns[i - 1].Dispose();
            }
        }


        public void Process()
        {
            ProcessSkillDelay();
        }

        private void ProcessSkillDelay()
        {
            if (!Visible) return;

            int offset = BarIndex * 8;

            for (int i = 0; i < Cells.Length; i++)
            {
                foreach (var magic in GameScene.User.Magics)
                {
                    if (magic.Key != i + offset + 1) continue;

                    int totalFrames = 22;
                    long timeLeft = magic.CastTime + magic.Delay - CMain.Time;

                    if (timeLeft < 100 || (CoolDowns[i] != null && CoolDowns[i].Animated))
                    {
                        if (timeLeft > 0)
                            CoolDowns[i].Dispose();
                        else
                            continue;
                    }

                    int delayPerFrame = (int)(magic.Delay / totalFrames);
                    int startFrame = totalFrames - (int)(timeLeft / delayPerFrame);

                    if ((CMain.Time <= magic.CastTime + magic.Delay) && magic.CastTime > 0)
                    {
                        CoolDowns[i].Dispose();

                        CoolDowns[i] = new MirAnimatedControl
                        {
                            Index = 1260 + startFrame,
                            AnimationCount = (totalFrames - startFrame),
                            AnimationDelay = delayPerFrame,
                            Library = Libraries.Prguse2,
                            Parent = this,
                            Location = new Point(i * 25 + 15, 3),
                            NotControl = true,
                            UseOffSet = true,
                            Loop = false,
                            Animated = true,
                            Opacity = 0.6F
                        };
                    }
                }
            }
        }

        public void Show()
        {
            if (Visible) return;
            if (!HasSkill) return;
            Settings.SkillBar = true;
            Visible = true;
            Update();
        }

        public void Hide()
        {
            if (!Visible) return;
            Settings.SkillBar = false;
            Visible = false;
        }
    }
    public sealed class CharacterDialog : MirImageControl
    {
        public MirButton CloseButton, CharacterButton, StatusButton, StateButton, SkillButton;
        public MirImageControl CharacterPage, StatusPage, StatePage, SkillPage, ClassImage;

        public MirLabel NameLabel, GuildLabel, LoverLabel;
        public MirLabel ACLabel, MACLabel, DCLabel, MCLabel, SCLabel, HealthLabel, ManaLabel;
        public MirLabel CritRLabel, CritDLabel, LuckLabel, AttkSpdLabel, AccLabel, AgilLabel;
        public MirLabel ExpPLabel, BagWLabel, WearWLabel, HandWLabel, MagicRLabel, PoisonRecLabel, HealthRLabel, ManaRLabel, PoisonResLabel, HolyTLabel, FreezeLabel, PoisonAtkLabel;
        public MirLabel HeadingLabel, StatLabel;
        public MirButton NextButton, BackButton;

        public long glowCooldown = 0;

        public MirItemCell[] Grid;
        public MagicButton[] Magics;

        public int weaponGlow = 0;

        public int StartIndex;
        
        public CharacterDialog()
        {
            Index = 504;
            Library = Libraries.CustomTitle;
            Location = new Point(Settings.ScreenWidth - 264, 0);
            Movable = true;
            Sort = true;

            BeforeDraw += (o, e) => RefreshInterface();

            CharacterPage = new MirImageControl
            {
                Index = 340,
                Parent = this,
                Library = Libraries.CustomPrguse,
                Location = new Point(8, 90),
            };
            CharacterPage.AfterDraw += (o, e) =>
            {

                if (GameScene.User.HumUp)
                {
                    MLibrary JobStaetItems = null;

                    if (GameScene.User.HasClassWeapon)
                    {
                        switch (GameScene.User.Class)
                        {
                            case MirClass.Warrior:
                                JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsWarM : Libraries.StateItemsWarW;
                                Libraries.Prguse.Draw(330 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Wizard:
                                JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsWizM : Libraries.StateItemsWizW;
                                Libraries.Prguse.Draw(332 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Taoist:
                                JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsTaoM : Libraries.StateItemsTaoW;
                                Libraries.Prguse.Draw(334 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Assassin:
                                JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsAssM : Libraries.StateItemsAssW;
                                Libraries.Prguse.Draw(336 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Archer:
                                JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsArcM : Libraries.StateItemsArcW;
                                Libraries.Prguse.Draw(320 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                        }
                    }
                    else
                    {
                        JobStaetItems = MapObject.User.Gender == MirGender.Male ? Libraries.StateItemsComM : Libraries.StateItemsComW;
                        Libraries.Prguse.Draw(320 + (MapObject.User.Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);

                    }

                    if (JobStaetItems == null) return;

                    ItemInfo RealItem = null;
                    if (Grid[(int)EquipmentSlot.Armour].Item != null)
                    {
                        if (GameScene.User.WingEffect == 1 || GameScene.User.WingEffect == 2)
                        {
                            int wingOffset = 0;

                            if (GameScene.User.HasClassWeapon)
                            {
                                switch (GameScene.User.Class)
                                {
                                    case MirClass.Warrior:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 10 : 20;
                                        break;
                                    case MirClass.Wizard:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 12 : 22;
                                        break;
                                    case MirClass.Taoist:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 14 : 24;
                                        break;
                                    case MirClass.Assassin:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 16 : 26;
                                        break;
                                    case MirClass.Archer:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 18 : 28;
                                        break;
                                    default:
                                        wingOffset = GameScene.User.WingEffect == 1 ? 2 : 4;
                                        break;
                                }
                            }
                            else
                            {
                                wingOffset = GameScene.User.WingEffect == 1 ? 18 : 28;

                            }

                            //stupple hum stop
                            int genderOffset = MapObject.User.Gender == MirGender.Male ? 0 : 1;

                            Libraries.Prguse2.DrawBlend(1200 + wingOffset + genderOffset, DisplayLocation, Color.White, true, 1F);
                        }

                        RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Armour].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                        JobStaetItems.Draw(Grid[(int)EquipmentSlot.Armour].Item.Image, DisplayLocation, Color.White, true, 1F);

                    }
                    if (Grid[(int)EquipmentSlot.Weapon].Item != null)
                    {
                        RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Weapon].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                        JobStaetItems.Draw(Grid[(int)EquipmentSlot.Weapon].Item.Image, DisplayLocation, Color.White, true, 1F);

                        if (Grid[(int)EquipmentSlot.Weapon].Item.Info.WeaponEffects > 0)
                        {
                            ushort weapEffect = Grid[(int)EquipmentSlot.Weapon].Item.Info.WeaponEffects;

                            //HumUp code judas
                            int female = MapObject.User.Gender == MirGender.Female ? 4 : 0;
                            //Libraries.StateItemEffects.DrawBlend(weapEffect * 8 + weaponGlow + female, DisplayLocation, Color.White, true, 1F);

                            if (MapObject.User.Class == MirClass.Assassin && MapObject.User.HasClassWeapon)
                            {
                                RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Weapon].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                                JobStaetItems.Draw(Grid[(int)EquipmentSlot.Weapon].Item.Image, DisplayLocation, Color.White, true, 1F);
                            }

                        }
                    }

                    if (Grid[(int)EquipmentSlot.Helmet].Item != null)
                        JobStaetItems.Draw(Grid[(int)EquipmentSlot.Helmet].Item.Info.Image, DisplayLocation, Color.White, true, 1F);
                    else
                    {
                        int hair = 461 + MapObject.User.Hair + (MapObject.User.Gender == MirGender.Male ? 0 : 40);

                        Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                    }
                }
                else
                {
                    if (Libraries.StateItems == null) return;
                    ItemInfo RealItem = null;

                    #region Armour
                    if (Grid[(int)EquipmentSlot.Armour].Item != null)
                    {
                        int ArmourEffectN = GameScene.User.WingEffect;
                        //  Level system overrides wingeffect value
                        RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Armour].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);

                        #region Armour Skin Changing
                        int img = Grid[(int)EquipmentSlot.Armour].Item.Image;
                        if (RealItem.AllowLvlSys)
                        {
                            switch (Grid[(int)EquipmentSlot.Armour].Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (RealItem.LevelItemLooks[0] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[0];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[0] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[0];
                                    break;
                                case 2:
                                    if (RealItem.LevelItemLooks[1] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[1];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[1] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[1];
                                    break;
                                case 3:
                                    if (RealItem.LevelItemLooks[2] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[2];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[2] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[2];
                                    break;
                                case 4:
                                    if (RealItem.LevelItemLooks[3] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[3];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[3] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[3];
                                    break;
                                case 5:
                                    if (RealItem.LevelItemLooks[4] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[4];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[4] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[4];
                                    break;
                                case 6:
                                    if (RealItem.LevelItemLooks[5] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[5];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[5] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[5];
                                    break;
                                case 7:
                                    if (RealItem.LevelItemLooks[6] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[6];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[6] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[6];
                                    break;
                                case 8:
                                    if (RealItem.LevelItemLooks[7] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[7];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[7] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[7];
                                    break;
                                case 9:
                                    if (RealItem.LevelItemLooks[8] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[8];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[8] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[8];
                                    break;
                                case 10:
                                    if (RealItem.LevelItemLooks[9] > 0)
                                    {
                                        img = RealItem.LevelItemLooks[9];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[9] == 0 ? GameScene.User.WingEffect : RealItem.LevelItemGlow[9];
                                    break;
                                case 0:
                                default:
                                    img = RealItem.Image;
                                    ArmourEffectN = GameScene.User.WingEffect;
                                    break;
                            }
                        }
                        Libraries.StateItems.Draw(img, DisplayLocation, Color.White, true, 1F);

                        
                        int startIndex = GameScene.User.WingEffect >= 1 && GameScene.User.WingEffect <= 2 && GameScene.User.Gender == MirGender.Male ? 0 : 1;
                        if (startIndex == -1 && GameScene.User.WingEffect > 1)
                            startIndex = GameScene.User.Gender == MirGender.Male ? 0 : 10;
                        Libraries.WingEffectLibrary[ArmourEffectN].DrawBlend(startIndex, DisplayLocation, Color.White, true, 1F);
                        #endregion
                    }
                    #endregion

                    #region Weapon
                    if (Grid[(int)EquipmentSlot.Weapon].Item != null)
                    {
                        RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Weapon].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                        #region Weapon Skin Changing
                        int img = Grid[(int)EquipmentSlot.Weapon].Item.Image;
                        int WeaponEffectN = RealItem.Effect;
                        if (RealItem.AllowLvlSys)
                        {
                            switch (Grid[(int)EquipmentSlot.Weapon].Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (RealItem.LevelItemLooks[0] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[0];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[0] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[0];
                                    break;
                                case 2:
                                    if (RealItem.LevelItemLooks[1] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[1];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[1] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[1];
                                    break;
                                case 3:
                                    if (RealItem.LevelItemLooks[2] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[2];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[2] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[2];
                                    break;
                                case 4:
                                    if (RealItem.LevelItemLooks[3] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[3];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[3] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[3];
                                    break;
                                case 5:
                                    if (RealItem.LevelItemLooks[4] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[4];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[4] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[4];
                                    break;
                                case 6:
                                    if (RealItem.LevelItemLooks[5] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[5];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[5] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[5];
                                    break;
                                case 7:
                                    if (RealItem.LevelItemLooks[6] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[6];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[6] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[6];
                                    break;
                                case 8:
                                    if (RealItem.LevelItemLooks[7] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[7];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[7] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[7];
                                    break;
                                case 9:
                                    if (RealItem.LevelItemLooks[8] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[8];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[8] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[8];
                                    break;
                                case 10:
                                    if (RealItem.LevelItemLooks[9] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[9];
                                    }
                                    WeaponEffectN = RealItem.LevelItemGlow[9] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[9];
                                    break;
                                case 0:
                                default:
                                    img = RealItem.Image;
                                    WeaponEffectN = RealItem.Effect;
                                    break;
                            }
                        }
                        //RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Weapon].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                        
                        Libraries.StateItems.Draw(img, DisplayLocation, Color.White, true, 1F);
                        
                        #endregion

                        #region Effect Changing
                        //  Level system overrides Weapon value
                        
 
                        if (WeaponEffectN > 0)
                        {
                            if (glowCooldown < CMain.Time - 300)
                            {
                                glowCooldown = CMain.Time;
                                weaponGlow++;
                                if (weaponGlow >= Libraries.WeaponEffectLibrary[WeaponEffectN].GetCount())
                                    weaponGlow = 0;
                            }
                            Libraries.WeaponEffectLibrary[WeaponEffectN].DrawBlend(weaponGlow, DisplayLocation, Color.White, true, 1F);
                        }
                           

                        
                        #endregion
                    }
                    #endregion

                    #region Helmet
                    if (Grid[(int)EquipmentSlot.Helmet].Item != null)
                    {
                        RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.Helmet].Item.Info, MapObject.User.Level, MapObject.User.Class, GameScene.ItemInfoList);
                        int img = Grid[(int)EquipmentSlot.Helmet].Item.Image;
                        if (Grid[(int)EquipmentSlot.Helmet].Item.Info.AllowLvlSys)
                        {
                            switch (Grid[(int)EquipmentSlot.Helmet].Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (RealItem.LevelItemLooks[0] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[0];
                                    }
                                    break;
                                case 2:
                                    if (RealItem.LevelItemLooks[1] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[1];
                                    }
                                    break;
                                case 3:
                                    if (RealItem.LevelItemLooks[2] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[2];
                                    }
                                    break;
                                case 4:
                                    if (RealItem.LevelItemLooks[3] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[3];
                                    }
                                    break;
                                case 5:
                                    if (RealItem.LevelItemLooks[4] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[4];
                                    }
                                    break;
                                case 6:
                                    if (RealItem.LevelItemLooks[5] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[5];
                                    }
                                    break;
                                case 7:
                                    if (RealItem.LevelItemLooks[6] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[6];
                                    }
                                    break;
                                case 8:
                                    if (RealItem.LevelItemLooks[7] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[7];
                                    }
                                    break;
                                case 9:
                                    if (RealItem.LevelItemLooks[8] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[8];
                                    }
                                    break;
                                case 10:
                                    if (RealItem.LevelItemLooks[9] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[9];
                                    }
                                    break;
                                case 0:
                                default:
                                    img = RealItem.Image;
                                    break;
                            }
                        }
                        
                        Libraries.StateItems.Draw(img, DisplayLocation, Color.White, true, 1F);
                    }
                    else
                    {
                        int hair = 441 + MapObject.User.Hair + (MapObject.User.Class == MirClass.Assassin ? 20 : 0) + (MapObject.User.Gender == MirGender.Male ? 0 : 40);

                        int offSetX = MapObject.User.Class == MirClass.Assassin ? (MapObject.User.Gender == MirGender.Male ? 6 : 4) : 0;
                        int offSetY = MapObject.User.Class == MirClass.Assassin ? (MapObject.User.Gender == MirGender.Male ? 25 : 18) : 0;

                        Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X + offSetX, DisplayLocation.Y + offSetY), Color.White, true, 1F);
                    }
                }
                #endregion

                    #region Shield
                if (Grid[(int)EquipmentSlot.Shield].Item != null)
                {
                    
                    int img = Grid[(int)EquipmentSlot.Shield].Item.Image;
                    if (Grid[(int)EquipmentSlot.Shield].Item.Info.AllowLvlSys)
                    {
                        switch (Grid[(int)EquipmentSlot.Shield].Item.LvlSystem)
                        {
                            case 1:
                                //  Stop all items being scanned
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[0] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[0];
                                }
                                break;
                            case 2:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[1] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[1];
                                }
                                break;
                            case 3:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[2] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[2];
                                }
                                break;
                            case 4:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[3] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[3];
                                }
                                break;
                            case 5:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[4] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[4];
                                }
                                break;
                            case 6:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[5] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[5];
                                }
                                break;
                            case 7:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[6] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[6];
                                }
                                break;
                            case 8:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[7] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[7];
                                }
                                break;
                            case 9:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[8] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[8];
                                }
                                break;
                            case 10:
                                if (Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[9] > 0)
                                {
                                    img = (short)Grid[(int)EquipmentSlot.Shield].Item.Info.LevelItemLooks[9];
                                }
                                break;
                            case 0:
                            default:
                                img = Grid[(int)EquipmentSlot.Shield].Item.Info.Image;
                                break;
                        }
                    }
                    
                    Libraries.StateItems.Draw(img, DisplayLocation, Color.White, true, 1F);
                }
                #endregion

                    #region Pads

                if (Grid[(int)EquipmentSlot.Pads].Item != null)
                    Libraries.StateItems.Draw(Grid[(int)EquipmentSlot.Pads].Item.Info.Image, DisplayLocation, Color.White, true, 1F);

                #endregion
            };

            StatusPage = new MirImageControl
            {
                Index = 506,
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 90),
                Visible = false,
            };
            StatusPage.BeforeDraw += (o, e) =>
            {
                ACLabel.Text = string.Format("{0}-{1}", MapObject.User.MinAC, MapObject.User.MaxAC);
                MACLabel.Text = string.Format("{0}-{1}", MapObject.User.MinMAC, MapObject.User.MaxMAC);
                DCLabel.Text = string.Format("{0}-{1}", MapObject.User.MinDC, MapObject.User.MaxDC);
                MCLabel.Text = string.Format("{0}-{1}", MapObject.User.MinMC, MapObject.User.MaxMC);
                SCLabel.Text = string.Format("{0}-{1}", MapObject.User.MinSC, MapObject.User.MaxSC);
                HealthLabel.Text = string.Format("{0}/{1}", MapObject.User.HP, MapObject.User.MaxHP);
                ManaLabel.Text = string.Format("{0}/{1}", MapObject.User.MP, MapObject.User.MaxMP);
                CritRLabel.Text = string.Format("{0}%", MapObject.User.CriticalRate);
                CritDLabel.Text = string.Format("{0}", MapObject.User.CriticalDamage);
                AttkSpdLabel.Text = string.Format("{0}", MapObject.User.ASpeed);
                AccLabel.Text = string.Format("+{0}", MapObject.User.Accuracy);
                AgilLabel.Text = string.Format("+{0}", MapObject.User.Agility);
                LuckLabel.Text = string.Format("{0}", GameScene.User.Luck);
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
                ExpPLabel.Text = string.Format("{0:0.##%}", MapObject.User.Experience / (double)MapObject.User.MaxExperience);
                BagWLabel.Text = string.Format("{0}/{1}", MapObject.User.CurrentBagWeight, MapObject.User.MaxBagWeight);
                WearWLabel.Text = string.Format("{0}/{1}", MapObject.User.CurrentWearWeight, MapObject.User.MaxWearWeight);
                HandWLabel.Text = string.Format("{0}/{1}", MapObject.User.CurrentHandWeight, MapObject.User.MaxHandWeight);
                MagicRLabel.Text = string.Format("+{0}", MapObject.User.MagicResist);
                PoisonResLabel.Text = string.Format("+{0}", MapObject.User.PoisonResist);
                HealthRLabel.Text = string.Format("+{0}", MapObject.User.HealthRecovery);
                ManaRLabel.Text = string.Format("+{0}", MapObject.User.SpellRecovery);
                PoisonRecLabel.Text = string.Format("+{0}", MapObject.User.PoisonRecovery);
                HolyTLabel.Text = string.Format("+{0}", MapObject.User.Holy);
                FreezeLabel.Text = string.Format("+{0}", MapObject.User.Freezing);
                PoisonAtkLabel.Text = string.Format("+{0}", MapObject.User.PoisonAttack);
            };


            SkillPage = new MirImageControl
            {
                Index = 508,
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(8, 90),
                Visible = false
            };

            #region Bottons
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

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(241, 3),
                Library = Libraries.CustomPrguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
                Hint ="Exit"
            };
            CloseButton.Click += (o, e) => Hide();
            #endregion

            #region Labels
            NameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(0, 12),
                Size = new Size(264, 20),
                NotControl = true,
            };
            GuildLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(0, 33),
                Size = new Size(264, 30),
                NotControl = true,
            };
            ClassImage = new MirImageControl
            {
                Index = 100,
                Library = Libraries.CustomPrguse,
                Location = new Point(27, 40),
                Parent = this,
                NotControl = true,
            };
            #endregion

            #region Item Cells
            Grid = new MirItemCell[Enum.GetNames(typeof(EquipmentSlot)).Length];

            Grid[(int)EquipmentSlot.Weapon] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Weapon,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(126, 7),
            };


            Grid[(int)EquipmentSlot.Armour] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Armour,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 62),
            };


            Grid[(int)EquipmentSlot.Helmet] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Helmet,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 7),
            };



            Grid[(int)EquipmentSlot.Torch] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Torch,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(166, 242),
            };


            Grid[(int)EquipmentSlot.Necklace] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Necklace,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 98),
            };


            Grid[(int)EquipmentSlot.BraceletL] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletL,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(6, 170),
            };

            Grid[(int)EquipmentSlot.BraceletR] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletR,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 170),
            };

            Grid[(int)EquipmentSlot.RingL] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingL,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(6, 206),
            };

            Grid[(int)EquipmentSlot.RingR] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingR,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 206),
            };


            Grid[(int)EquipmentSlot.Amulet] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Amulet,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(6, 242),
            };


            Grid[(int)EquipmentSlot.Boots] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Boots,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(46, 242),
            };

            Grid[(int)EquipmentSlot.Belt] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Belt,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(86, 242),
            };


            Grid[(int)EquipmentSlot.Stone] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Stone,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(126, 242),
            };

            Grid[(int)EquipmentSlot.Mount] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Mount,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 134),
            };

            Grid[(int)EquipmentSlot.Poison] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Poison,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(86, 7),
            };

            Grid[(int)EquipmentSlot.Medals] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Medals,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(206, 242),
            };

            Grid[(int)EquipmentSlot.Shield] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Shield,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(166, 7),
            };

            Grid[(int)EquipmentSlot.Pads] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Pads,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(6, 98),
            };

            Grid[(int)EquipmentSlot.Trophy] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Trophy,
                GridType = MirGridType.Equipment,
                Parent = CharacterPage,
                Location = new Point(6, 134),
            };
            #endregion

            #region Stat Labels
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
            // STATS II 
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
                NotControl = true,
                Text = "0/0"
            };
            PoisonRecLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 164),
                NotControl = true,
                Text = "0/0"
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
            #endregion

            Magics = new MagicButton[7];

            for (int i = 0; i < Magics.Length; i++)
                Magics[i] = new MagicButton { Parent = SkillPage, Visible = false, Location = new Point(8, 8 + i * 33) };

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
                if (StartIndex + 7 >= MapObject.User.Magics.Count) return;

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

            ClearCoolDowns();
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

        public void Process()
        {
            BringToFront();
        }

        public void ShowSkillPage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = false;
            StatePage.Visible = false;
            SkillPage.Visible = true;
            CharacterButton.Index = -1;
            StatusButton.Index = -1;
            StateButton.Index = -1;
            SkillButton.Index = 503;
            StartIndex = 0;

            ClearCoolDowns();
        }

        private void ClearCoolDowns()
        {
            for (int i = 0; i < Magics.Length; i++)
            {
                Magics[i].CoolDown.Dispose();
            }
        }

        private void RefreshInterface()
        {
            int offSet = MapObject.User.Gender == MirGender.Male ? 0 : 1;

            Index = 504;// +offSet;
            CharacterPage.Index = 340 + offSet;

            switch (MapObject.User.Class)
            {
                case MirClass.Warrior:
                    ClassImage.Index = 100;// + offSet * 5;
                    Hint = "Warrior";
                    if (MapObject.User.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Wizard:
                    ClassImage.Index = 101;// + offSet * 5;
                    Hint = "Wizard";
                    if (MapObject.User.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Taoist:
                    ClassImage.Index = 102;// + offSet * 5;
                    Hint = "Taoist";
                    if (MapObject.User.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Assassin:
                    ClassImage.Index = 103;// + offSet * 5;
                    Hint = "Assassin";
                    if (MapObject.User.HumUp)
                        CharacterPage.Index = 379;
                    break;
                case MirClass.Archer:
                    ClassImage.Index = 104;// + offSet * 5;
                    Hint = "Archer";
                    if (MapObject.User.HumUp)
                        CharacterPage.Index = 379;
                    break;
            }

            NameLabel.Text = MapObject.User.Name;
            GuildLabel.Text = MapObject.User.GuildName + " " + MapObject.User.GuildRankName;

            for (int i = 0; i < Magics.Length; i++)
            {
                if (i + StartIndex >= MapObject.User.Magics.Count)
                {
                    Magics[i].Visible = false;
                    continue;
                }

                Magics[i].Visible = true;
                Magics[i].Update(MapObject.User.Magics[i + StartIndex]);
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

    }

    public sealed class ComboBarDialog : MirImageControl
    {

        public double PlayerPercent , HeroPercent;
        public long HeroDuration, HeroTime;

        public MirImageControl ComboPlayerBar, ComboHeroBar;

        public ComboBarDialog()
        {
            Index = 970;
            Library = Libraries.Prguse2;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, 0);


            ComboPlayerBar = new MirImageControl
            {
                Index = 971,
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(10, 12),
                DrawImage = false,
            };
            ComboPlayerBar.BeforeDraw += ComboPlayerBar_BeforeDraw;

            ComboHeroBar = new MirImageControl
            {
                Index = 972,
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(96, 12),
                DrawImage = false,
            };
            ComboHeroBar.BeforeDraw += ComboHeroBar_BeforeDraw;

        }

        public void Process()
        {
            if (GameScene.Hero == null || GameScene.User.HeroState != HeroState.Spawned)
            {
                Visible = false;
                return;
            }

            if (PlayerPercent > .0 || HeroPercent > .0)
                Visible = true;
            else
                Visible = false;


            if (GameScene.User.comboSpell.Delay > CMain.Time)
            {
                PlayerPercent =1 - (double)(GameScene.User.comboSpell.Delay - CMain.Time) / GameScene.User.comboSpell.Duration;
            }
            else if (GameScene.User.comboSpell.Spell != Spell.None)
            {

                Network.Enqueue(new C.ComboSpell { spell = GameScene.User.comboSpell.Spell });
                GameScene.User.comboSpell.Spell = Spell.None;
            }

            if (HeroTime > CMain.Time)
            {
                HeroPercent = 1 - (double)(HeroTime - CMain.Time) / HeroDuration;
            }

            if (PlayerPercent > .9 && HeroPercent > .9 && HeroTime < CMain.Time)
            {
                PlayerPercent = 0;
                HeroPercent = 0;
            }
        }


        private void ComboPlayerBar_BeforeDraw(object sender, EventArgs e)
        {
            double percent = PlayerPercent;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((ComboPlayerBar.Size.Width - 3) * percent), ComboPlayerBar.Size.Height)
            };

            Libraries.Prguse2.Draw(ComboPlayerBar.Index, section, ComboPlayerBar.DisplayLocation, Color.White, false);

        }



        private void ComboHeroBar_BeforeDraw(object sender, EventArgs e)
        {
            double percent = HeroPercent;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((ComboHeroBar.Size.Width - 3) * percent), ComboHeroBar.Size.Height)
            };

            Libraries.Prguse2.Draw(ComboHeroBar.Index, section, ComboHeroBar.DisplayLocation, Color.White, false);

        }
    }




    public sealed class MiniMapDialog : MirImageControl
    {
        public MirImageControl LightSetting, NewMail;
        public MirButton ToggleButton, BigMapButton, MailButton;
        public MirLabel LocationLabel, MapNameLabel;
        private float _fade = 1F;
        private bool _bigMode = true, _realBigMode = true;

        public MirLabel AModeLabel, PModeLabel;

        public List<MirLabel> QuestIcons = new List<MirLabel>();

        public MiniMapDialog()
        {
            Index = 2090;
            Library = Libraries.CustomPrguse;
            Location = new Point(Settings.ScreenWidth - 126, 0);
            PixelDetect = true;

            BeforeDraw += MiniMap_BeforeDraw;
            AfterDraw += MiniMapDialog_AfterDraw;

            MapNameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new Size(120, 18),
                Location = new Point(2, 2),
                NotControl = true,
            };

            LocationLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new Size(56, 18),
                Location = new Point(46, 131),
                NotControl = true,
            };

            MailButton = new MirButton
            {
                Index = 2099,
                HoverIndex = 2100,
                PressedIndex = 2101,
                Parent = this,
                Location = new Point(4, 131),
                Library = Libraries.Prguse,
                Sound = SoundList.ButtonA,
                Hint = "Mail"
            };
            MailButton.Click += (o, e) => GameScene.Scene.MailListDialog.Toggle();

            NewMail = new MirImageControl
            {
                Index = 544,
                Location = new Point(5, 132),
                Parent = this,
                Library = Libraries.Prguse,
                Visible = false,
                NotControl = true
            };

            BigMapButton = new MirButton
            {
                Index = 2096,
                HoverIndex = 2097,
                PressedIndex = 2098,
                Parent = this,
                Location = new Point(25, 131),
                Library = Libraries.CustomPrguse,
                Sound = SoundList.ButtonA,
                Hint = "BigMap (" + CMain.InputKeys.GetKey(KeybindOptions.Bigmap) + ")"
            };
            BigMapButton.Click += (o, e) => GameScene.Scene.BigMapDialog.Toggle();

            ToggleButton = new MirButton
            {
                Index = 2102,
                HoverIndex = 2103,
                PressedIndex = 2104,
                Parent = this,
                Location = new Point(109, 3),
                Library = Libraries.CustomPrguse,
                Sound = SoundList.ButtonA,
                Hint = "MiniMap (" + CMain.InputKeys.GetKey(KeybindOptions.Minimap) + ")"
            };
            ToggleButton.Click += (o, e) => Toggle();

            LightSetting = new MirImageControl
            {
                Index = 2093,
                Library = Libraries.CustomPrguse,
                Parent = this,
                Location = new Point(102, 131),
            };


            AModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(115, 125)
            };

            PModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(230, 125),
                Visible = false
            };
        }

        private void MiniMapDialog_AfterDraw(object sender, EventArgs e)
        {

        }

        private void MiniMap_BeforeDraw(object sender, EventArgs e)
        {

            foreach (var icon in QuestIcons)
                icon.Dispose();

            QuestIcons.Clear();

            MapControl map = GameScene.Scene.MapControl;
            if (map == null) return;

            if (map.MiniMap == 0 && Index != 2091)
            {
                SetSmallMode();
            }
            else if (map.MiniMap > 0 && _bigMode && Index == 2091)
            {
                SetBigMode();
            }

            if (map.MiniMap <= 0 || Index != 2090 || Libraries.MiniMap == null)
            {
                return;
            }

            Rectangle viewRect = new Rectangle(0, 0, 120, 108);
            Point drawLocation = Location;
            drawLocation.Offset(3, 22);

            Size miniMapSize = Libraries.MiniMap.GetSize(map.MiniMap);
            float scaleX = miniMapSize.Width / (float)map.Width;
            float scaleY = miniMapSize.Height / (float)map.Height;

            viewRect.Location = new Point(
                (int)(scaleX * MapObject.User.CurrentLocation.X) - viewRect.Width / 2,
                (int)(scaleY * MapObject.User.CurrentLocation.Y) - viewRect.Height / 2);

            if (viewRect.Right >= miniMapSize.Width)
                viewRect.X = miniMapSize.Width - viewRect.Width;
            if (viewRect.Bottom >= miniMapSize.Height)
                viewRect.Y = miniMapSize.Height - viewRect.Height;

            if (viewRect.X < 0) viewRect.X = 0;
            if (viewRect.Y < 0) viewRect.Y = 0;

            Libraries.MiniMap.Draw(map.MiniMap, viewRect, drawLocation, Color.FromArgb(255, 255, 255), _fade);


            int startPointX = (int)(viewRect.X / scaleX);
            int startPointY = (int)(viewRect.Y / scaleY);

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];

                if (ob.Race == ObjectType.Item || ob.Dead || ob.Race == ObjectType.Spell || ob.Sneaking) continue;
                float x = ((ob.CurrentLocation.X - startPointX) * scaleX) + drawLocation.X;
                float y = ((ob.CurrentLocation.Y - startPointY) * scaleY) + drawLocation.Y;

                Color colour;
                //Self
                if ((GroupDialog.GroupList.Contains(ob.Name) && MapObject.User != ob) || ob.Name.EndsWith(string.Format("({0})", MapObject.User.Name)))
                    colour = Color.FromArgb(0, 0, 255);
                else if (ob is PlayerObject)
                {//Others
                    colour = Color.FromArgb(255, 255, 255);
                }
                else if (ob is NPCObject || ob.AI == 6)
                {
                    //NPC or w/e AI 6 is
                    colour = Color.FromArgb(0, 255, 50);
                }
                else if (ob is MonsterObject tmp)
                {
                    if (tmp.QuestMob)
                        colour = Color.Goldenrod;
                    else if (tmp.EventMob)
                        colour = Color.Plum;
                    //  Normal Mob
                    else
                        colour = Color.FromArgb(255, 0, 0);
                }
                else
                    colour = Color.FromArgb(255, 0, 0);
                //Hero
                if (ob.IsHero)
                {
                    colour = Color.Purple;
                }

                DXManager.Sprite.Draw2D(ob is PlayerObject ? DXManager.PlayerRadarTexture : DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(x - 0.5F), (int)(y - 0.5F)), colour);

                #region NPC Quest Icons

                NPCObject npc = ob as NPCObject;
                if (npc != null && npc.GetAvailableQuests(true).Any())
                {
                    string text = "";
                    Color color = Color.Empty;

                    switch (npc.QuestIcon)
                    {
                        case QuestIcon.ExclamationBlue:
                            color = Color.DodgerBlue;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationYellow:
                            color = Color.Yellow;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationGreen:
                            color = Color.Green;
                            text = "!";
                            break;
                        case QuestIcon.QuestionBlue:
                            color = Color.DodgerBlue;
                            text = "?";
                            break;
                        case QuestIcon.QuestionWhite:
                            color = Color.White;
                            text = "?";
                            break;
                        case QuestIcon.QuestionYellow:
                            color = Color.Yellow;
                            text = "?";
                            break;
                        case QuestIcon.QuestionGreen:
                            color = Color.Green;
                            text = "?";
                            break;
                        case QuestIcon.QuestionRed:
                            color = Color.Red;
                            text = "?";
                            break;
                        case QuestIcon.ExclamationRed:
                            color = Color.Red;
                            text = "!";
                            break;
                        case QuestIcon.QuestionOrange:
                            color = Color.Orange;
                            text = "?";
                            break;
                        case QuestIcon.ExclamationOrange:
                            color = Color.Orange;
                            text = "!";
                            break;

                        case QuestIcon.ExclamationDarkBlue:
                            color = Color.DarkBlue;
                            text = "!";
                            break;

                        case QuestIcon.QuestionDarkBlue:
                            color = Color.DarkBlue;
                            text = "?";
                            break;

                    }

                    QuestIcons.Add(new MirLabel
                    {
                        AutoSize = true,
                        Parent = GameScene.Scene.MiniMapDialog,
                        Font = new Font(Settings.FontName, 9f, FontStyle.Bold),
                        DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                        Text = text,
                        ForeColour = color,
                        Location = new Point((int)(x - Settings.ScreenWidth + GameScene.Scene.MiniMapDialog.Size.Width) - 6, (int)(y) - 10),
                        NotControl = true,
                        Visible = true,
                        Modal = true
                    });
                }

                #endregion

            }
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Toggle()
        {
            if (_fade == 0F)
            {
                _bigMode = true;
                SetBigMode();
                _fade = 1F;
            }
            else
            {
                _bigMode = false;
                SetSmallMode();
                _fade = 0;
            }

            Redraw();
        }

        private void SetSmallMode()
        {
            Index = 2091;
            int y = Size.Height - 23;
            MailButton.Location = new Point(4, y);
            NewMail.Location = new Point(5, y + 1);
            BigMapButton.Location = new Point(25, y);
            LocationLabel.Location = new Point(46, y);
            LightSetting.Location = new Point(102, y);

            _realBigMode = false;

        }

        private void SetBigMode()
        {
            Index = 2090;
            int y = Size.Height - 23;
            MailButton.Location = new Point(4, y);
            NewMail.Location = new Point(5, y + 1);
            BigMapButton.Location = new Point(25, y);
            LocationLabel.Location = new Point(46, y);
            LightSetting.Location = new Point(102, y);

            _realBigMode = true;

        }

        public void Process()
        {
            MapControl map = GameScene.Scene.MapControl;
            if (map == null) return;
            MapNameLabel.Text = map.Title;
            LocationLabel.Text = Functions.PointToString(MapObject.User.CurrentLocation);

            int offset = _realBigMode ? 0 : 108;

            //GameScene.Scene.MainDialog.SModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            //(GameScene.Scene.MiniMapDialog.Size.Height + 150) - Settings.ScreenHeight);
            //GameScene.Scene.MainDialog.AModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            //(GameScene.Scene.MiniMapDialog.Size.Height + 165) - Settings.ScreenHeight);
            //GameScene.Scene.MainDialog.PModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            //(GameScene.Scene.MiniMapDialog.Size.Height + 180) - Settings.ScreenHeight);
            //GameScene.Scene.MainDialog.PingLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            //(GameScene.Scene.MiniMapDialog.Size.Height + 195) - Settings.ScreenHeight);
            //GameScene.Scene.MainDialog.TimeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            //(GameScene.Scene.MiniMapDialog.Size.Height + 210) - Settings.ScreenHeight);

            if (GameScene.Scene.NewMail)
            {
                double time = (CMain.Time) / 100D;

                if (Math.Round(time) % 10 < 5 || GameScene.Scene.NewMailCounter >= 10)
                {
                    NewMail.Visible = true;
                }
                else
                {
                    if (NewMail.Visible)
                    {
                        GameScene.Scene.NewMailCounter++;
                    }

                    NewMail.Visible = false;
                }
            }
            else
            {
                NewMail.Visible = false;
            }
        }
    }
    public sealed class InspectDialog : MirImageControl
    {
        public static UserItem[] Items = new UserItem[14];
        public static uint InspectID;

        public string Name;
        public string GuildName;
        public string GuildRank;
        public MirClass Class;
        public MirGender Gender;
        public byte Hair;
        public ushort Level;
        public string LoverName;
        public bool humUp;
        public bool hero;

        public MirButton CloseButton, GroupButton, FriendButton, MailButton, TradeButton, LoverButton;
        public MirImageControl CharacterPage, ClassImage;
        public MirLabel NameLabel;
        public MirLabel GuildLabel, LoverLabel;

        public long glowCooldown;
        public int weaponGlow;

        public bool HasClassWeapon
        {
            get
            {
                if (WeaponCell == null || WeaponCell.Item == null)
                    return true;

                switch (WeaponCell.Item.Info.Shape / 100)
                {
                    default:
                        return Class == MirClass.Wizard || Class == MirClass.Warrior || Class == MirClass.Taoist;
                    case 1:
                        return Class == MirClass.Assassin;
                    case 2:
                        return Class == MirClass.Archer;
                }
            }
        }

        public MirItemCell
            WeaponCell,
            ArmorCell,
            HelmetCell,
            TorchCell,
            NecklaceCell,
            BraceletLCell,
            BraceletRCell,
            RingLCell,
            RingRCell,
            AmuletCell,
            BeltCell,
            BootsCell,
            StoneCell,
            MountCell,
            PoisonCell,
            MedalsCell,
            ShieldCell,
            PadsCell,
            TrophyCell;

        public InspectDialog()
        {
            Index = 430;
            Library = Libraries.CustomPrguse;
            Location = new Point(536, 0);
            Movable = true;
            Sort = true;

            CharacterPage = new MirImageControl
            {
                Index = 340,
                Parent = this,
                Library = Libraries.CustomPrguse,
                Location = new Point(8, 70),
            };
            CharacterPage.AfterDraw += (o, e) =>
            {
                   #region Humup
                if (humUp)
                {
                    MLibrary JobStaetItems = null;
                    CharacterPage.Index = 379;

                    if (HasClassWeapon)
                    {
                        switch (Class)
                        {
                            case MirClass.Warrior:
                                JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsWarM : Libraries.StateItemsWarW;
                                Libraries.Prguse.Draw(330 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Wizard:
                                JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsWizM : Libraries.StateItemsWizW;
                                Libraries.Prguse.Draw(332 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Taoist:
                                JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsTaoM : Libraries.StateItemsTaoW;
                                Libraries.Prguse.Draw(334 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Assassin:
                                JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsAssM : Libraries.StateItemsAssW;
                                Libraries.Prguse.Draw(336 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            case MirClass.Archer:
                                JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsArcM : Libraries.StateItemsArcW;
                                Libraries.Prguse.Draw(320 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                                break;
                            default:
                                JobStaetItems = Libraries.StateItems;
                                CharacterPage.Index = Gender == MirGender.Male ? 345 : 346;
                                break;
                        }
                    }
                    else
                    {
                        JobStaetItems = Gender == MirGender.Male ? Libraries.StateItemsComM : Libraries.StateItemsComW;
                        Libraries.Prguse.Draw(320 + (Gender == MirGender.Male ? 0 : 1), new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                    }

                    if (JobStaetItems == null) return;

                    ItemInfo RealItem = null;
                    if (ArmorCell.Item != null)
                    {
                        JobStaetItems.Draw(ArmorCell.Item.Image, DisplayLocation, Color.White, true, 1F);
                        RealItem = Functions.GetRealItem(ArmorCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                        //here
                        Libraries.StateItems.Draw(RealItem.Image, new Point(DisplayLocation.X, DisplayLocation.Y - 20),
                        Color.White, true, 1F);
                        {
                            int WingEffect = ArmorCell.Item.Info.Effect;

                            if (ArmorCell.Item.Info.Effect == 1 || WingEffect == 2)
                            {
                                int wingOffset = 0;

                                if (HasClassWeapon)
                                {
                                    switch (Class)
                                    {
                                        case MirClass.Warrior:
                                            wingOffset = WingEffect == 1 ? 10 : 20;
                                            break;
                                        case MirClass.Wizard:
                                            wingOffset = WingEffect == 1 ? 12 : 22;
                                            break;
                                        case MirClass.Taoist:
                                            wingOffset = WingEffect == 1 ? 14 : 24;
                                            break;
                                        case MirClass.Assassin:
                                            wingOffset = WingEffect == 1 ? 16 : 26;
                                            break;
                                        case MirClass.Archer:
                                            wingOffset = WingEffect == 1 ? 18 : 28;
                                            break;
                                        default:
                                            wingOffset = WingEffect == 1 ? 2 : 4;
                                            break;
                                    }
                                }
                                else
                                {
                                    wingOffset = WingEffect == 1 ? 18 : 28;
                                }

                                int genderOffset = Gender == MirGender.Male ? 0 : 1;


                                Libraries.Prguse2.DrawBlend(1200 + wingOffset + genderOffset, DisplayLocation, Color.White, true, 1F);
                            }
                        }
                    }


                    if (WeaponCell.Item != null)
                    {
                        JobStaetItems.Draw(WeaponCell.Item.Image, DisplayLocation, Color.White, true, 1F);
                        RealItem = Functions.GetRealItem(WeaponCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                        //here
                        Libraries.StateItems.Draw(RealItem.Image, new Point(DisplayLocation.X, DisplayLocation.Y - 20),
                        Color.White, true, 1F);

                        if (WeaponCell.Item.Info.WeaponEffects > 0)
                        {
                            ushort weapEffect = WeaponCell.Item.Info.WeaponEffects;

                            //HumUp code judas
                            int female = Gender == MirGender.Female ? 4 : 0;
                            //Libraries.StateItemEffects.DrawBlend(weapEffect * 8 + weaponGlow + female, DisplayLocation, Color.White, true, 1F);

                            if (Class == MirClass.Assassin && ClassWeapon(WeaponCell.Item.Info.Shape))
                            {//here
                                JobStaetItems.Draw(WeaponCell.Item.Image, DisplayLocation, Color.White, true, 1F);
                                RealItem = Functions.GetRealItem(WeaponCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                                //here
                                Libraries.StateItems.Draw(RealItem.Image, new Point(DisplayLocation.X, DisplayLocation.Y - 20),
                                Color.White, true, 1F);
                            }
                        }

                        if (HelmetCell.Item != null)
                        {//here
                            JobStaetItems.Draw(HelmetCell.Item.Image, DisplayLocation, Color.White, true, 1F);
                        }
                        else
                        {
                            int hair = 461 + Hair + (Gender == MirGender.Male ? 0 : 40);

                            Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X, DisplayLocation.Y), Color.White, true, 1F);
                        }
                    }
                }
                #endregion
                else
                {
                    if (Libraries.StateItems == null) return;

                    ItemInfo RealItem = null;

                    #region Armour
                    if (ArmorCell.Item != null)
                    {
                        RealItem = Functions.GetRealItem(ArmorCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                        int img = ArmorCell.Item.Image;
                        int ArmourEffectN = RealItem.Effect;
                        if (RealItem.AllowLvlSys)
                        {
                            switch (ArmorCell.Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (RealItem.LevelItemLooks[0] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[0];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[0] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[0];
                                    break;
                                case 2:
                                    if (RealItem.LevelItemLooks[1] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[1];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[1] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[1];
                                    break;
                                case 3:
                                    if (RealItem.LevelItemLooks[2] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[2];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[2] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[2];
                                    break;
                                case 4:
                                    if (RealItem.LevelItemLooks[3] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[3];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[3] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[3];
                                    break;
                                case 5:
                                    if (RealItem.LevelItemLooks[4] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[4];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[4] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[4];
                                    break;
                                case 6:
                                    if (RealItem.LevelItemLooks[5] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[5];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[5] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[5];
                                    break;
                                case 7:
                                    if (RealItem.LevelItemLooks[6] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[6];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[6] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[6];
                                    break;
                                case 8:
                                    if (RealItem.LevelItemLooks[7] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[7];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[7] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[7];
                                    break;
                                case 9:
                                    if (RealItem.LevelItemLooks[8] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[8];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[8] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[8];
                                    break;
                                case 10:
                                    if (RealItem.LevelItemLooks[9] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[9];
                                    }
                                    ArmourEffectN = RealItem.LevelItemGlow[9] == 0 ? RealItem.Effect : RealItem.LevelItemGlow[9];
                                    break;
                                case 0:
                                default:
                                    img = RealItem.Image;
                                    ArmourEffectN = RealItem.Effect;
                                    break;
                            }
                        }

                        if (ArmourEffectN > 0)
                        {
                            int _startIndex = -1;
                            _startIndex = ArmourEffectN >= 1 && ArmourEffectN <= 2 && MapObject.User.Gender == MirGender.Male ? 0 : 1;
                            if (_startIndex == -1 && ArmourEffectN > 0)
                                _startIndex = MapObject.User.Gender == MirGender.Male ? 0 : 10;
                            Libraries.WingEffectLibrary[ArmourEffectN].DrawBlend(_startIndex, new Point(DisplayLocation.X, DisplayLocation.Y + (hero ? 0 : -20)), Color.White, true, 1F);
                        }
                        //Armour
                        Libraries.StateItems.Draw(img, new Point(DisplayLocation.X + 0, DisplayLocation.Y + (hero ? 0 : -20 )), Color.White, true, 1F);
                    }
                    #endregion

                    #region Weapon
                    if (WeaponCell.Item != null)
                    {
                        RealItem = Functions.GetRealItem(WeaponCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                        int img = WeaponCell.Item.Image;
                        //  Use effect by default
                        int WeaponEffectN = WeaponCell.Item.Info.Effect;
                        if (WeaponCell.Item.Info.AllowLvlSys)
                        {
                            switch (WeaponCell.Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (WeaponCell.Item.Info.LevelItemLooks[0] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[0];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[0] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[0];
                                    break;
                                case 2:
                                    if (WeaponCell.Item.Info.LevelItemLooks[1] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[1];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[1] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[1];
                                    break;
                                case 3:
                                    if (WeaponCell.Item.Info.LevelItemLooks[2] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[2];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[2] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[2];
                                    break;
                                case 4:
                                    if (WeaponCell.Item.Info.LevelItemLooks[3] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[3];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[3] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[3];
                                    break;
                                case 5:
                                    if (WeaponCell.Item.Info.LevelItemLooks[4] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[4];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[4] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[4];
                                    break;
                                case 6:
                                    if (WeaponCell.Item.Info.LevelItemLooks[5] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[5];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[5] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[5];
                                    break;
                                case 7:
                                    if (WeaponCell.Item.Info.LevelItemLooks[6] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[6];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[6] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[6];
                                    break;
                                case 8:
                                    if (WeaponCell.Item.Info.LevelItemLooks[7] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[7];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[7] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[7];
                                    break;
                                case 9:
                                    if (WeaponCell.Item.Info.LevelItemLooks[8] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[8];
                                        
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[8] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[8];
                                    break;
                                case 10:
                                    if (WeaponCell.Item.Info.LevelItemLooks[9] > 0)
                                    {
                                        img = (short)WeaponCell.Item.Info.LevelItemLooks[9];
                                    }
                                    WeaponEffectN = WeaponCell.Item.Info.LevelItemGlow[9] == 0 ? WeaponCell.Item.Info.Effect : WeaponCell.Item.Info.LevelItemGlow[9];
                                    break;
                                case 0:
                                default:
                                    img = WeaponCell.Item.Info.Image;
                                    //  Again use Effect by default
                                    WeaponEffectN = WeaponCell.Item.Info.Effect;
                                    break;
                            }
                        }

                        Libraries.StateItems.Draw(img, new Point(DisplayLocation.X, DisplayLocation.Y + (hero ? 0 : -20)),
                        Color.White, true, 1F);

                        if (WeaponEffectN > 0)
                        {
                            if (CMain.Time > glowCooldown)
                            {
                                glowCooldown = CMain.Time + 300;
                                weaponGlow++;
                                if (weaponGlow >= Libraries.WeaponEffectLibrary[WeaponEffectN].GetCount())
                                    weaponGlow = 0;

                            }
                            Libraries.WeaponEffectLibrary[WeaponEffectN].DrawBlend(weaponGlow, new Point(DisplayLocation.X, DisplayLocation.Y + (hero ? 0 : -20)), Color.White, true, 1F);
                        }
                    }
                    #endregion

                    #region Helmet
                    if (HelmetCell.Item != null)
                    {
                        RealItem = Functions.GetRealItem(HelmetCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                        int img = HelmetCell.Item.Image;
                        if (HelmetCell.Item.Info.AllowLvlSys)
                        {
                            switch (HelmetCell.Item.LvlSystem)
                            {
                                case 1:
                                    //  Stop all items being scanned
                                    if (RealItem.LevelItemLooks[0] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[0];
                                    }
                                    break;
                                case 2:
                                    if (RealItem.LevelItemLooks[1] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[1];
                                    }
                                    break;
                                case 3:
                                    if (RealItem.LevelItemLooks[2] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[2];
                                    }
                                    break;
                                case 4:
                                    if (RealItem.LevelItemLooks[3] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[3];
                                    }
                                    break;
                                case 5:
                                    if (RealItem.LevelItemLooks[4] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[4];
                                    }
                                    break;
                                case 6:
                                    if (RealItem.LevelItemLooks[5] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[5];
                                    }
                                    break;
                                case 7:
                                    if (RealItem.LevelItemLooks[6] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[6];
                                    }
                                    break;
                                case 8:
                                    if (RealItem.LevelItemLooks[7] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[7];
                                    }
                                    break;
                                case 9:
                                    if (RealItem.LevelItemLooks[8] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[8];
                                    }
                                    break;
                                case 10:
                                    if (RealItem.LevelItemLooks[9] > 0)
                                    {
                                        img = (short)RealItem.LevelItemLooks[9];
                                    }
                                    break;
                                case 0:
                                default:
                                    img = HelmetCell.Item.Info.Image;
                                    break;
                            }
                        }
                        Libraries.StateItems.Draw(img, new Point(DisplayLocation.X, DisplayLocation.Y + (hero ? 0 : -20)), Color.White, true, 1F);
                    }
                    else
                    {
                        int hair = 441 + Hair + (Class == MirClass.Assassin ? 20 : 0) + (Gender == MirGender.Male ? 0 : 40);

                        int offSetX = Class == MirClass.Assassin ? (Gender == MirGender.Male ? 6 : 4) : 0;
                        int offSetY = Class == MirClass.Assassin ? (Gender == MirGender.Male ? 25 : 18) : 0;

                        Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X + offSetX, DisplayLocation.Y + offSetY + (hero ? 0 : -20)), Color.White, true, 1F);
                    }
                }
                #endregion

                    #region Shield
                if (ShieldCell.Item != null)
                {
                    int img = ShieldCell.Item.Image;
                    if (ShieldCell.Item.Info.AllowLvlSys)
                    {
                        switch (ShieldCell.Item.LvlSystem)
                        {
                            case 1:
                                //  Stop all items being scanned
                                if (ShieldCell.Item.Info.LevelItemLooks[0] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[0];
                                }
                                break;
                            case 2:
                                if (ShieldCell.Item.Info.LevelItemLooks[1] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[1];
                                }
                                break;
                            case 3:
                                if (ShieldCell.Item.Info.LevelItemLooks[2] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[2];
                                }
                                break;
                            case 4:
                                if (ShieldCell.Item.Info.LevelItemLooks[3] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[3];
                                }
                                break;
                            case 5:
                                if (ShieldCell.Item.Info.LevelItemLooks[4] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[4];
                                }
                                break;
                            case 6:
                                if (ShieldCell.Item.Info.LevelItemLooks[5] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[5];
                                }
                                break;
                            case 7:
                                if (ShieldCell.Item.Info.LevelItemLooks[6] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[6];
                                }
                                break;
                            case 8:
                                if (ShieldCell.Item.Info.LevelItemLooks[7] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[7];
                                }
                                break;
                            case 9:
                                if (ShieldCell.Item.Info.LevelItemLooks[8] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[8];
                                }
                                break;
                            case 10:
                                if (ShieldCell.Item.Info.LevelItemLooks[9] > 0)
                                {
                                    img = (short)ShieldCell.Item.Info.LevelItemLooks[9];
                                }
                                break;
                            case 0:
                            default:
                                img = ShieldCell.Item.Info.Image;
                                break;
                        }
                    }
                    Libraries.StateItems.Draw(img, new Point(DisplayLocation.X, DisplayLocation.Y + (hero ? 0 : -20)), Color.White, true, 1F);
                }
                #endregion

                    #region Pads

                if (PadsCell.Item != null)
                    Libraries.StateItems.Draw(PadsCell.Item.Info.Image, new Point(DisplayLocation.X, DisplayLocation.Y - 20), Color.White, true, 1F);

                #endregion
            };

            #region Bottoms
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

            GroupButton = new MirButton
            {
                HoverIndex = 432,
                Index = 431,
                Location = new Point(75, 357),
                Library = Libraries.CustomPrguse,
                Parent = this,
                PressedIndex = 433,
                Sound = SoundList.ButtonA,
                Hint = "Group"
            };
            GroupButton.Click += (o, e) =>
            {

                if (GroupDialog.GroupList.Count >= Globals.MaxGroup)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat("Your group already has the maximum number of members.", ChatType.System);
                    return;
                }
                if (GroupDialog.GroupList.Count > 0 && GroupDialog.GroupList[0] != MapObject.User.Name)
                {

                    GameScene.Scene.ChatDialog.ReceiveChat("You are not the leader of your group.", ChatType.System);
                }

                Network.Enqueue(new C.AddMember { Name = Name });
                return;
            };

            FriendButton = new MirButton
            {
                HoverIndex = 435,
                Index = 434,
                Location = new Point(105, 357),
                Library = Libraries.CustomPrguse,
                Parent = this,
                PressedIndex = 436,
                Sound = SoundList.ButtonA,
                Hint = "Friend"
            };
            FriendButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.AddFriend { Name = Name, Blocked = false });
            };

            MailButton = new MirButton
            {
                HoverIndex = 438,
                Index = 437,
                Location = new Point(135, 357),
                Library = Libraries.CustomPrguse,
                Parent = this,
                PressedIndex = 439,
                Sound = SoundList.ButtonA,
                Hint = "Mail"
            };
            MailButton.Click += (o, e) => GameScene.Scene.MailComposeLetterDialog.ComposeMail(Name);

            TradeButton = new MirButton
            {
                HoverIndex = 524,
                Index = 523,
                Location = new Point(165, 357),
                Library = Libraries.CustomPrguse,
                Parent = this,
                PressedIndex = 525,
                Sound = SoundList.ButtonA,
                Hint = "Trade"
            };
            TradeButton.Click += (o, e) => Network.Enqueue(new C.TradeRequest());

            LoverButton = new MirButton
            {
                Index = 604,
                Location = new Point(17, 17),
                Library = Libraries.CustomPrguse,
                Parent = this,
                Sound = SoundList.None
            };
            #endregion

            #region Labals
            NameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(50, 12),
                Size = new Size(190, 20),
                NotControl = true
            };

            NameLabel.Click += (o, e) =>
            {
                GameScene.Scene.ChatDialog.ChatTextBox.SetFocus();
                GameScene.Scene.ChatDialog.ChatTextBox.Text = string.Format("/{0} ", Name);
                GameScene.Scene.ChatDialog.ChatTextBox.Visible = true;
                GameScene.Scene.ChatDialog.ChatTextBox.TextBox.SelectionLength = 0;
                GameScene.Scene.ChatDialog.ChatTextBox.TextBox.SelectionStart = Name.Length + 2;

            };

            GuildLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(50, 33),
                Size = new Size(190, 30),
                NotControl = true,
            };

            ClassImage = new MirImageControl
            {
                Index = 100,
                Library = Libraries.CustomPrguse,
                Location = new Point(27, 40),
                Parent = this,
                NotControl = true,
            };
            #endregion

            #region Item Cells
            WeaponCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Weapon,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(126, 7),
            };

            ArmorCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Armour,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 62),
            };

            HelmetCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Helmet,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 7),
            };


            TorchCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Torch,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(166, 242),
            };

            NecklaceCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Necklace,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 98),
            };

            BraceletLCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletL,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(8, 170),
            };
            BraceletRCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.BraceletR,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 170),
            };
            RingLCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingL,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(6, 206),
            };
            RingRCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.RingR,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 206),
            };

            AmuletCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Amulet,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(6, 242),
            };

            BootsCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Boots,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(46, 242),
            };
            BeltCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Belt,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(86, 242),
            };

            StoneCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Stone,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(126, 242),
            };

            MountCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Mount,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 134),
            };

            PoisonCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Poison,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(86, 7),
            };

            MedalsCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Medals,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(206, 242),
            };

            ShieldCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Shield,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(166, 7),
            };

            PadsCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Pads,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(8, 98),
            };

            TrophyCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.Trophy,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(8, 134),
            };
            #endregion
        }

        public bool ClassWeapon(int Weapon)
        {

            switch (Weapon / 100)
            {
                default:
                    return Class == MirClass.Wizard || Class == MirClass.Warrior || Class == MirClass.Taoist;
                case 1:
                    return Class == MirClass.Assassin;
                case 2:
                    return Class == MirClass.Archer;
            }
        }

        public void RefreshInferface()
        {
            if (hero)
            {
                GroupButton.Visible = false;
                FriendButton.Visible = false;
                MailButton.Visible = false;
                TradeButton.Visible = false;
                LoverButton.Visible = false;
            }
            else
            {
                GroupButton.Visible = true;
                FriendButton.Visible = true;
                MailButton.Visible = true;
                TradeButton.Visible = true;
                LoverButton.Visible = true;
            }


            int offSet = Gender == MirGender.Male ? 0 : 1;

            CharacterPage.Index = 340 + offSet;

            switch (Class)
            {
                case MirClass.Warrior:
                    ClassImage.Index = 100;// + offSet * 5;
                    Hint = "Warrior";
                    break;
                case MirClass.Wizard:
                    ClassImage.Index = 101;// + offSet * 5;
                    Hint = "Wizard";
                    break;
                case MirClass.Taoist:
                    ClassImage.Index = 102;// + offSet * 5;
                    Hint = "Taoist";
                    break;
                case MirClass.Assassin:
                    ClassImage.Index = 103;// + offSet * 5;
                    Hint = "Assassin";
                    break;
                case MirClass.Archer:
                    ClassImage.Index = 104;// + offSet * 5;
                    Hint = "Archer";
                    break;
            }

            NameLabel.Text = Name;
            GuildLabel.Text = GuildName + " " + GuildRank;
            if (LoverName != "")
            {
                LoverButton.Visible = true;
                LoverButton.Hint = LoverName;
            }
            else
                LoverButton.Visible = false;


            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null) continue;
                GameScene.Bind(Items[i]);
            }
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
        }

    }
    public sealed class OptionDialog : MirImageControl
    {
        public MirButton CloseButton, GeneralSettings, GraphicSettings, SoundSettings, DamgeSettings;
        public MirButton LevelEffect, FullScreen, AlwaysOnTop, MonsterNameView, Effect;
        public MirButton DropView, Trade, TargetDead, NameView, DropEffectView;
        public MirButton MissIndicator, DamageIndicator, MagicDamageIndicator, CriticalIndicator, HealIndicator, ManaRecovIndicator, BossHPBar, MobLightEffect;

        public MirImageControl SoundBar, MusicSoundBar, VolumeBar, MusicVolumeBar;


        public OptionDialog()
        {
            Index = 200;
            Library = Libraries.CustomButtons;
            Movable = true;
            Sort = true;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);

            BeforeDraw += OptionPanel_BeforeDraw;

            #region Buttons
            CloseButton = new MirButton
            {
                Index = 360,
                HoverIndex = 361,
                PressedIndex = 362,
                Library = Libraries.CustomPrguse2,
                Location = new Point(Size.Width - 26, 5),
                Parent = this,
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            CloseButton.Click += (o, e) => Hide();

            GeneralSettings = new MirButton
            {
                Index = 211,
                Library = Libraries.CustomButtons,
                Location = new Point(5, 28),
                Parent = this,
                Sound = SoundList.ButtonA
            };
            GeneralSettings.Click += delegate (object o, EventArgs e)
            {
                GeneralClick();
            };

            GraphicSettings = new MirButton
            {
                Index = 212,
                Library = Libraries.CustomButtons,
                Location = new Point(92, 28),
                Parent = this,
                Sound = SoundList.ButtonA
            };
            GraphicSettings.Click += delegate (object o, EventArgs e)
            {
                GraficClick();
            };

            SoundSettings = new MirButton
            {
                Index = 214,
                Library = Libraries.CustomButtons,
                Location = new Point(178, 28),
                Parent = this,
                Sound = SoundList.ButtonA
            };
            SoundSettings.Click += delegate (object o, EventArgs e)
            {
                SoundClick();
            };

            DamgeSettings = new MirButton
            {
                Index = 216,
                Library = Libraries.CustomButtons,
                Location = new Point(264, 28),
                Parent = this,
                Sound = SoundList.ButtonA
            };
            DamgeSettings.Click += delegate (object o, EventArgs e)
            {
                DamgeClick();
            };


            FullScreen = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(312, 67),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                Visible = false
            };

            AlwaysOnTop = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(312, 91),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                Visible = false
            };

            Effect = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 67),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            Effect.Click += (o, e) => Settings.Effect = !Settings.Effect;

            LevelEffect = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 86),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            LevelEffect.Click += (o, e) => Settings.LevelEffect = !Settings.LevelEffect;

            NameView = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 106),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            NameView.Click += (o, e) => Settings.NameView = !Settings.NameView;

            DropView = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 126),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            DropView.Click += (o, e) => Settings.DropView = !Settings.DropView;

            DropEffectView = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 67),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            DropEffectView.Click += (o, e) => Settings.DropEffect = !Settings.DropEffect;

            Trade = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 86),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            Trade.Click += (o, e) => Network.Enqueue(new C.Chat { Message = "@ALLOWTRADE" });

            MonsterNameView = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 106),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            MonsterNameView.Click += (o, e) => Settings.MonsterName = !Settings.MonsterName;

            TargetDead = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 126),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            TargetDead.Click += (o, e) => Settings.TargetDead = !Settings.TargetDead;

            SoundBar = new MirImageControl
            {
                Index = 205,
                Library = Libraries.CustomButtons,
                Location = new Point(262, 64),
                Parent = this,
                DrawImage = false,
                Visible = false
            };
            SoundBar.MouseDown += SoundBar_MouseMove;
            SoundBar.MouseMove += SoundBar_MouseMove;
            SoundBar.BeforeDraw += SoundBar_BeforeDraw;

            VolumeBar = new MirImageControl
            {
                Index = 251,
                Library = Libraries.CustomButtons,
                Location = new Point(260, 62),
                Parent = this,
                NotControl = true,
                Visible = false

            };

            MusicSoundBar = new MirImageControl
            {
                Index = 205,
                Library = Libraries.CustomButtons,
                Location = new Point(264, 98),
                Parent = this,
                DrawImage = false,
                Visible = false
            };
            MusicSoundBar.MouseDown += MusicSoundBar_MouseMove;
            MusicSoundBar.MouseMove += MusicSoundBar_MouseMove;
            MusicSoundBar.MouseUp += MusicSoundBar_MouseUp;
            MusicSoundBar.BeforeDraw += MusicSoundBar_BeforeDraw;

            MusicVolumeBar = new MirImageControl
            {
                Index = 251,
                Library = Libraries.CustomButtons,
                Location = new Point(260, 94),
                Parent = this,
                NotControl = true,
                Visible = false
            };

            MissIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 67),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            MissIndicator.Click += (o, e) => Settings.MissIndicator = !Settings.MissIndicator;

            DamageIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 86),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            DamageIndicator.Click += (o, e) => Settings.DamageIndicator = !Settings.DamageIndicator;

            MagicDamageIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 106),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            MagicDamageIndicator.Click += (o, e) => Settings.MagicDamageIndicator = !Settings.MagicDamageIndicator;

            CriticalIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(171, 126),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            CriticalIndicator.Click += (o, e) => Settings.CriticalIndicator = !Settings.CriticalIndicator;

            HealIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 67),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            HealIndicator.Click += (o, e) => Settings.HealIndicator = !Settings.HealIndicator;

            ManaRecovIndicator = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 86),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            ManaRecovIndicator.Click += (o, e) => Settings.ManaRecovIndicator = !Settings.ManaRecovIndicator;

            MobLightEffect = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 106),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            MobLightEffect.Click += (o, e) => Settings.MobLightEffect = !Settings.MobLightEffect;

            BossHPBar = new MirButton
            {
                Library = Libraries.CustomButtons,
                Location = new Point(325, 126),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17)
            };
            BossHPBar.Click += (o, e) => Settings.BossHPBar = !Settings.BossHPBar;
            RefreshSettings();
            GeneralClick();
            #endregion
        }

        #region Tabs
        public void GeneralClick()
        {
            Index = 200;
            GeneralSettings.Index = 211;
            GraphicSettings.Index = 212;
            SoundSettings.Index = 214;
            DamgeSettings.Index = 216;
            AlwaysOnTop.Visible = false;
            Trade.Visible = true;
            FullScreen.Visible = false;
            LevelEffect.Visible = true;
            TargetDead.Visible = true;
            DropEffectView.Visible = true;
            Effect.Visible = true;
            DropView.Visible = true;
            NameView.Visible = true;
            MonsterNameView.Visible = true;
            SoundBar.Visible = false;
            MusicSoundBar.Visible = false;
            VolumeBar.Visible = false;
            MusicVolumeBar.Visible = false;
            MissIndicator.Visible = false;
            DamageIndicator.Visible = false;
            MagicDamageIndicator.Visible = false;
            CriticalIndicator.Visible = false;
            HealIndicator.Visible = false;
            ManaRecovIndicator.Visible = false;
            BossHPBar.Visible = false;
            MobLightEffect.Visible = false;
        }

        public void GraficClick()
        {
            Index = 201;
            GeneralSettings.Index = 210;
            GraphicSettings.Index = 213;
            SoundSettings.Index = 214;
            DamgeSettings.Index = 216;
            Trade.Visible = false;
            AlwaysOnTop.Visible = true;
            FullScreen.Visible = true;
            LevelEffect.Visible = false;
            TargetDead.Visible = false;
            DropEffectView.Visible = false;
            Effect.Visible = false;
            DropView.Visible = false;
            NameView.Visible = false;
            MonsterNameView.Visible = false;
            SoundBar.Visible = false;
            MusicSoundBar.Visible = false;
            VolumeBar.Visible = false;
            MusicVolumeBar.Visible = false;
            MissIndicator.Visible = false;
            DamageIndicator.Visible = false;
            MagicDamageIndicator.Visible = false;
            CriticalIndicator.Visible = false;
            HealIndicator.Visible = false;
            ManaRecovIndicator.Visible = false;
            BossHPBar.Visible = false;
            MobLightEffect.Visible = false;
        }

        public void SoundClick()
        {
            Index = 202;
            GeneralSettings.Index = 210;
            GraphicSettings.Index = 212;
            SoundSettings.Index = 215;
            DamgeSettings.Index = 216;
            Trade.Visible = false;
            AlwaysOnTop.Visible = false;
            FullScreen.Visible = false;
            LevelEffect.Visible = false;
            TargetDead.Visible = false;
            DropEffectView.Visible = false;
            Effect.Visible = false;
            DropView.Visible = false;
            NameView.Visible = false;
            MonsterNameView.Visible = false;
            SoundBar.Visible = true;
            MusicSoundBar.Visible = true;
            VolumeBar.Visible = true;
            MusicVolumeBar.Visible = true;
            MissIndicator.Visible = false;
            DamageIndicator.Visible = false;
            MagicDamageIndicator.Visible = false;
            CriticalIndicator.Visible = false;
            HealIndicator.Visible = false;
            ManaRecovIndicator.Visible = false;
            BossHPBar.Visible = false;
            MobLightEffect.Visible = false;
        }

        public void DamgeClick()
        {
            Index = 199;
            GeneralSettings.Index = 210;
            GraphicSettings.Index = 212;
            SoundSettings.Index = 214;
            DamgeSettings.Index = 217;
            Trade.Visible = false;
            AlwaysOnTop.Visible = false;
            FullScreen.Visible = false;
            LevelEffect.Visible = false;
            TargetDead.Visible = false;
            DropEffectView.Visible = false;
            Effect.Visible = false;
            DropView.Visible = false;
            NameView.Visible = false;
            MonsterNameView.Visible = false;
            SoundBar.Visible = false;
            MusicSoundBar.Visible = false;
            VolumeBar.Visible = false;
            MusicVolumeBar.Visible = false;
            MissIndicator.Visible = true;
            DamageIndicator.Visible = true;
            MagicDamageIndicator.Visible = true;
            CriticalIndicator.Visible = true;
            HealIndicator.Visible = true;
            ManaRecovIndicator.Visible = true;
            BossHPBar.Visible = true;
            MobLightEffect.Visible = true;
        }

        public void RefreshSettings()
        {
            if (Settings.AllowTrade)
            {
                Trade.Index = 230;
                Trade.HoverIndex = 231;
                Trade.PressedIndex = 232;
            }
            else
            {
                Trade.Index = 235;
                Trade.HoverIndex = 236;
                Trade.PressedIndex = 237;
            }

            if (Settings.MonsterName)
            {
                MonsterNameView.Index = 230;
                MonsterNameView.HoverIndex = 231;
                MonsterNameView.PressedIndex = 232;
            }
            else
            {
                MonsterNameView.Index = 235;
                MonsterNameView.HoverIndex = 236;
                MonsterNameView.PressedIndex = 237;
            }

            if (Settings.FullScreen)
            {
                FullScreen.Index = 230;
                FullScreen.HoverIndex = 231;
                FullScreen.PressedIndex = 232;
            }
            else
            {
                FullScreen.Index = 235;
                FullScreen.HoverIndex = 236;
                FullScreen.PressedIndex = 237;
            }

            if (Settings.TopMost)
            {
                AlwaysOnTop.Index = 230;
                AlwaysOnTop.HoverIndex = 231;
                AlwaysOnTop.PressedIndex = 232;
            }
            else
            {
                AlwaysOnTop.Index = 235;
                AlwaysOnTop.HoverIndex = 236;
                AlwaysOnTop.PressedIndex = 237;
            }

            if (Settings.DropEffect)
            {
                DropEffectView.Index = 230;
                DropEffectView.HoverIndex = 231;
                DropEffectView.PressedIndex = 232;
            }
            else
            {
                DropEffectView.Index = 235;
                DropEffectView.HoverIndex = 236;
                DropEffectView.PressedIndex = 237;
            }

            if (Settings.TargetDead)
            {
                TargetDead.Index = 230;
                TargetDead.HoverIndex = 231;
                TargetDead.PressedIndex = 232;
            }
            else
            {
                TargetDead.Index = 235;
                TargetDead.HoverIndex = 236;
                TargetDead.PressedIndex = 237;
            }

            if (Settings.LevelEffect)
            {
                LevelEffect.Index = 230;
                LevelEffect.HoverIndex = 231;
                LevelEffect.PressedIndex = 232;
            }
            else
            {
                LevelEffect.Index = 235;
                LevelEffect.HoverIndex = 236;
                LevelEffect.PressedIndex = 237;
            }

            if (Settings.Effect)
            {
                Effect.Index = 230;
                Effect.HoverIndex = 231;
                Effect.PressedIndex = 232;
            }
            else
            {
                Effect.Index = 235;
                Effect.HoverIndex = 236;
                Effect.PressedIndex = 237; ;
            }

            if (Settings.DropView)
            {
                DropView.Index = 230;
                DropView.HoverIndex = 231;
                DropView.PressedIndex = 232;
            }
            else
            {
                DropView.Index = 235;
                DropView.HoverIndex = 236;
                DropView.PressedIndex = 237;
            }

            if (Settings.NameView)
            {
                NameView.Index = 230;
                NameView.HoverIndex = 231;
                NameView.PressedIndex = 232;
            }
            else
            {
                NameView.Index = 235;
                NameView.HoverIndex = 236;
                NameView.PressedIndex = 237;
            }

            if (Settings.MissIndicator)
            {
                MissIndicator.Index = 230;
                MissIndicator.HoverIndex = 231;
                MissIndicator.PressedIndex = 232;
            }
            else
            {
                MissIndicator.Index = 235;
                MissIndicator.HoverIndex = 236;
                MissIndicator.PressedIndex = 237;
            }

            if (Settings.DamageIndicator)
            {
                DamageIndicator.Index = 230;
                DamageIndicator.HoverIndex = 231;
                DamageIndicator.PressedIndex = 232;
            }
            else
            {
                DamageIndicator.Index = 235;
                DamageIndicator.HoverIndex = 236;
                DamageIndicator.PressedIndex = 237;
            }

            if (Settings.MagicDamageIndicator)
            {
                MagicDamageIndicator.Index = 230;
                MagicDamageIndicator.HoverIndex = 231;
                MagicDamageIndicator.PressedIndex = 232;
            }
            else
            {
                MagicDamageIndicator.Index = 235;
                MagicDamageIndicator.HoverIndex = 236;
                MagicDamageIndicator.PressedIndex = 237;
            }

            if (Settings.CriticalIndicator)
            {
                CriticalIndicator.Index = 230;
                CriticalIndicator.HoverIndex = 231;
                CriticalIndicator.PressedIndex = 232;
            }
            else
            {
                CriticalIndicator.Index = 235;
                CriticalIndicator.HoverIndex = 236;
                CriticalIndicator.PressedIndex = 237;
            }

            if (Settings.HealIndicator)
            {
                HealIndicator.Index = 230;
                HealIndicator.HoverIndex = 231;
                HealIndicator.PressedIndex = 232;
            }
            else
            {
                HealIndicator.Index = 235;
                HealIndicator.HoverIndex = 236;
                HealIndicator.PressedIndex = 237;
            }

            if (Settings.ManaRecovIndicator)
            {
                ManaRecovIndicator.Index = 230;
                ManaRecovIndicator.HoverIndex = 231;
                ManaRecovIndicator.PressedIndex = 232;
            }
            else
            {
                ManaRecovIndicator.Index = 235;
                ManaRecovIndicator.HoverIndex = 236;
                ManaRecovIndicator.PressedIndex = 237;
            }

            if (Settings.BossHPBar)
            {
                BossHPBar.Index = 230;
                BossHPBar.HoverIndex = 231;
                BossHPBar.PressedIndex = 232;
            }
            else
            {
                BossHPBar.Index = 235;
                BossHPBar.HoverIndex = 236;
                BossHPBar.PressedIndex = 237;
            }

            if (Settings.MobLightEffect)
            {
                MobLightEffect.Index = 230;
                MobLightEffect.HoverIndex = 231;
                MobLightEffect.PressedIndex = 232;
            }
            else
            {
                MobLightEffect.Index = 235;
                MobLightEffect.HoverIndex = 236;
                MobLightEffect.PressedIndex = 237;
            }
        }
        #endregion

        private void ToggleSkillButtons(bool Ctrl)
        {

            foreach (KeyBind KeyCheck in CMain.InputKeys.Keylist)
            {
                if (KeyCheck.Key == Keys.None)
                    continue;
                if ((KeyCheck.function < KeybindOptions.Bar1Skill1) || (KeyCheck.function > KeybindOptions.Bar2Skill8)) continue;
                //need to test this 
                if ((KeyCheck.CutomKey.RequireTilde != 1) && (KeyCheck.CutomKey.RequireTilde != 1)) continue;
                KeyCheck.CutomKey.RequireCtrl = (byte)(Ctrl ? 1 : 0);
                KeyCheck.CutomKey.RequireTilde = (byte)(Ctrl ? 0 : 1);
            }
        }

        private void SoundBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || SoundBar != ActiveControl) return;

            Point p = e.Location.Subtract(SoundBar.DisplayLocation);

            byte volume = (byte)(p.X / (double)SoundBar.Size.Width * 100);
            Settings.Volume = volume;


            double percent = Settings.Volume / 100D;
            if (percent > 1) percent = 1;

            VolumeBar.Location = percent > 0 ? new Point(159 + (int)((SoundBar.Size.Width - 2) * percent), 218) : new Point(159, 218);
        }

        private void SoundBar_BeforeDraw(object sender, EventArgs e)
        {
            if (SoundBar.Library == null) return;

            double percent = Settings.Volume / 100D;
            if (percent > 1) percent = 1;
            if (percent > 0)
            {
                Rectangle section = new Rectangle
                {
                    Size = new Size((int)((SoundBar.Size.Width - 2) * percent), SoundBar.Size.Height)
                };

                SoundBar.Library.Draw(SoundBar.Index, section, SoundBar.DisplayLocation, Color.White, false);
                VolumeBar.Location = new Point(159 + section.Size.Width, 218);
            }
            else
                VolumeBar.Location = new Point(159, 218);
        }

        private void MusicSoundBar_BeforeDraw(object sender, EventArgs e)
        {
            if (MusicSoundBar.Library == null) return;

            double percent = Settings.MusicVolume / 100D;
            if (percent > 1) percent = 1;
            if (percent > 0)
            {
                Rectangle section = new Rectangle
                {
                    Size = new Size((int)((MusicSoundBar.Size.Width - 2) * percent), MusicSoundBar.Size.Height)
                };

                MusicSoundBar.Library.Draw(MusicSoundBar.Index, section, MusicSoundBar.DisplayLocation, Color.White, false);
                MusicVolumeBar.Location = new Point(159 + section.Size.Width, 244);
            }
            else
                MusicVolumeBar.Location = new Point(159, 244);
        }

        public void MusicSoundBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (SoundManager.MusicVol <= -2900)
                SoundManager.MusicVol = -3000;
            if (SoundManager.MusicVol >= -100)
                SoundManager.MusicVol = 0;

            if (SoundManager.Music == null) return;

            SoundManager.Music.SetVolume(SoundManager.MusicVol);

        }

        private void MusicSoundBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || MusicSoundBar != ActiveControl) return;

            Point p = e.Location.Subtract(MusicSoundBar.DisplayLocation);

            byte volume = (byte)(p.X / (double)MusicSoundBar.Size.Width * 100);
            Settings.MusicVolume = volume;


            double percent = Settings.MusicVolume / 100D;
            if (percent > 1) percent = 1;

            MusicVolumeBar.Location = percent > 0 ? new Point(159 + (int)((MusicSoundBar.Size.Width - 2) * percent), 244) : new Point(159, 244);
        }

        private void OptionPanel_BeforeDraw(object sender, EventArgs e)
        {
            RefreshSettings();
        }

        public void Show()
        {
            Visible = true;
            OptionPanel_BeforeDraw(this, null);
        }

        public void Hide()
        {
            Visible = false;
        }


    }
    public sealed class MenuDialog : MirImageControl
    {
        public MirButton ExitButton,
                         LogOutButton,
                         HelpButton,
                         RankingButton,
                         KeyBoardButton,
                         CraftingButton,
                         IntelligentCreatureButton,
                         RideButton,
                         FishingButton,
                         FriendButton,
                         MentorButton,
                         RelationshipButton,
                         GroupButton,
                         GuildButton;

        public MenuDialog()
        {
            Index = 567;
            Parent = GameScene.Scene;
            Library = Libraries.Title;
            Location = new Point(Settings.ScreenWidth - Size.Width, GameScene.Scene.MainDialog.Location.Y - this.Size.Height + 15);
            Sort = true;
            Visible = false;
            Movable = true;

            ExitButton = new MirButton
            {
                HoverIndex = 634,
                Index = 633,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(3, 12),
                PressedIndex = 635,
                Hint = "Exit (" + CMain.InputKeys.GetKey(KeybindOptions.Exit) + ")"
            };
            ExitButton.Click += (o, e) => GameScene.Scene.QuitGame();

            LogOutButton = new MirButton
            {
                HoverIndex = 637,
                Index = 636,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(3, 31),
                PressedIndex = 638,
                Hint = "Log Out (" + CMain.InputKeys.GetKey(KeybindOptions.Logout) + ")"
            };
            LogOutButton.Click += (o, e) => GameScene.Scene.LogOut();


            HelpButton = new MirButton
            {
                Index = 1970,
                HoverIndex = 1971,
                PressedIndex = 1972,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 50),
                Hint = "Help (" + CMain.InputKeys.GetKey(KeybindOptions.Help) + ")"
            };
            HelpButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HelpDialog.Visible)
                    GameScene.Scene.HelpDialog.Hide();
                else
                    GameScene.Scene.HelpDialog.Show();
            };

            KeyBoardButton = new MirButton
            {
                Index = 1973,
                HoverIndex = 1974,
                PressedIndex = 1975,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 69),
                Visible = true,
                Hint = "Key Binds (" + CMain.InputKeys.GetKey(KeybindOptions.KeySettings) + ")"
            };
            KeyBoardButton.Click += (o, e) =>
            {
                if (GameScene.Scene.KeyboardLayoutDialog.Visible)
                    GameScene.Scene.KeyboardLayoutDialog.Hide();
                else
                    GameScene.Scene.KeyboardLayoutDialog.Show();
            };

            RankingButton = new MirButton
            {
                Index = 2000,
                HoverIndex = 2001,
                PressedIndex = 2002,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 88),
                Visible = true,
                Hint = "Ranking (" + CMain.InputKeys.GetKey(KeybindOptions.Ranking) + ")"
            };
            RankingButton.Click += (o, e) =>
            {
                if (GameScene.Scene.RankingDialog.Visible)
                    GameScene.Scene.RankingDialog.Hide();
                else
                    GameScene.Scene.RankingDialog.Show();
            };

            CraftingButton = new MirButton
            {
                Index = 1997,
                HoverIndex = 1998,
                PressedIndex = 1999,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 107),
                Hint = "Crafting(" + CMain.InputKeys.GetKey(KeybindOptions.Crafting) + ")"
            };
            CraftingButton.Click += (o, e) =>
            {              
                if (GameScene.Scene.CraftingDialog.Visible)
                    GameScene.Scene.CraftingDialog.Hide();
                else
                    GameScene.Scene.CraftingDialog.Show();
                
            };

            IntelligentCreatureButton = new MirButton
            {
                Index = 431,
                HoverIndex = 432,
                PressedIndex = 433,
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(3, 126),
                Hint = "Creatures (" + CMain.InputKeys.GetKey(KeybindOptions.Creature) + ")"
            };
            IntelligentCreatureButton.Click += (o, e) =>
            {
                if (GameScene.Scene.IntelligentCreatureDialog.Visible)
                    GameScene.Scene.IntelligentCreatureDialog.Hide();
                else
                    GameScene.Scene.IntelligentCreatureDialog.Show();
            };
            RideButton = new MirButton
            {
                Index = 1976,
                HoverIndex = 1977,
                PressedIndex = 1978,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 145),
                Hint = "Mount (" + CMain.InputKeys.GetKey(KeybindOptions.MountWindow) + ")"
            };
            RideButton.Click += (o, e) =>
            {
                if (GameScene.Scene.MountDialog.Visible)
                    GameScene.Scene.MountDialog.Hide();
                else
                    GameScene.Scene.MountDialog.Show();
            };

            FishingButton = new MirButton
            {
                Index = 1979,
                HoverIndex = 1980,
                PressedIndex = 1981,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 164),
                Hint = "Fishing (" + CMain.InputKeys.GetKey(KeybindOptions.Fishing) + ")"
            };
            FishingButton.Click += (o, e) =>
            {
                if (GameScene.Scene.FishingDialog.Visible)
                    GameScene.Scene.FishingDialog.Hide();
                else
                    GameScene.Scene.FishingDialog.Show();
            };

            FriendButton = new MirButton
            {
                Index = 1982,
                HoverIndex = 1983,
                PressedIndex = 1984,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 183),
                Visible = true,
                Hint = "Friends (" + CMain.InputKeys.GetKey(KeybindOptions.Friends) + ")"
            };
            FriendButton.Click += (o, e) =>
            {
                if (GameScene.Scene.FriendDialog.Visible)
                    GameScene.Scene.FriendDialog.Hide();
                else
                    GameScene.Scene.FriendDialog.Show();
            };

            MentorButton = new MirButton
            {
                Index = 1985,
                HoverIndex = 1986,
                PressedIndex = 1987,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 202),
                Visible = true,
                Hint = "Mentor (" + CMain.InputKeys.GetKey(KeybindOptions.Mentor) + ")"
            };
            MentorButton.Click += (o, e) =>
            {
                if (GameScene.Scene.MentorDialog.Visible)
                    GameScene.Scene.MentorDialog.Hide();
                else
                    GameScene.Scene.MentorDialog.Show();
            };


            RelationshipButton = new MirButton
            {
                Index = 1988,
                HoverIndex = 1989,
                PressedIndex = 1990,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 221),
                Visible = true,
                Hint = "Relationship (" + CMain.InputKeys.GetKey(KeybindOptions.Relationship) + ")"
            };
            RelationshipButton.Click += (o, e) =>
            {
                if (GameScene.Scene.RelationshipDialog.Visible)
                    GameScene.Scene.RelationshipDialog.Hide();
                else
                    GameScene.Scene.RelationshipDialog.Show();
            };

            GroupButton = new MirButton
            {
                Index = 1991,
                HoverIndex = 1992,
                PressedIndex = 1993,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 240),
                Hint = "Groups (" + CMain.InputKeys.GetKey(KeybindOptions.Group) + ")"
            };
            GroupButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupDialog.Visible)
                    GameScene.Scene.GroupDialog.Hide();
                else
                    GameScene.Scene.GroupDialog.Show();
            };

            GuildButton = new MirButton
            {
                Index = 1994,
                HoverIndex = 1995,
                PressedIndex = 1996,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 259),
                Hint = "Guild (" + CMain.InputKeys.GetKey(KeybindOptions.Guilds) + ")"
            };
            GuildButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GuildDialog.Visible)
                    GameScene.Scene.GuildDialog.Hide();
                else
                    GameScene.Scene.GuildDialog.Show();
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


        public sealed class MagicButton : MirControl
        {
            public MirImageControl LevelImage, ExpImage;
            public MirButton SkillButton;
            public MirLabel LevelLabel, NameLabel, ExpLabel, KeyLabel;
            public ClientMagic Magic;
            public MirAnimatedControl CoolDown;

            public MagicButton()
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
                SkillButton.Click += (o, e) => new AssignKeyPanel(Magic);

                LevelImage = new MirImageControl
                {
                    Index = 516,
                    Library = Libraries.CustomTitle,
                    Location = new Point(73, 7),
                    Parent = this,
                    NotControl = true,
                };

                ExpImage = new MirImageControl
                {
                    Index = 517,
                    Library = Libraries.CustomTitle,
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

                KeyLabel = new MirLabel
                {
                    AutoSize = true,
                    Parent = this,
                    Location = new Point(2, 2),
                    NotControl = true,
                };

                CoolDown = new MirAnimatedControl
                {
                    Library = Libraries.CustomPrguse2,
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
                        if ((GameScene.User.HumUp && magic.HumUpEnable) || (magic.OverrideHumUp))
                            ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need4);
                        else
                            ExpLabel.Text = "-";
                        break;
                    case 4:
                        if ((GameScene.User.HumUp && magic.HumUpEnable) || (magic.OverrideHumUp))                            
                            ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need5);
                        else
                            ExpLabel.Text = "-";
                    break;
                    case 5:
                        ExpLabel.Text = "-";
                        break;
                }

            if (Magic.Spell.In(Spell.LastJudgement,Spell.DragonFlames,Spell.ThunderClap,Spell.ChopChopStar,Spell.SoulEaterSwamp,Spell.SoulReaper,Spell.BrokenSoulCut,Spell.HandOfGod))
            {
                ExpLabel.Text = "Hero Spell";
                ExpLabel.ForeColour = Color.Turquoise;
                LevelLabel.Text = "-";
            }
            else
            {
                ExpLabel.ForeColour = Color.White;
            }

                if (Magic.Key > 8)
                {
                    int key = Magic.Key % 8;

                    KeyLabel.Text = string.Format("CTRL" + Environment.NewLine + "F{0}", key != 0 ? key : 8);
                }
                else if (Magic.Key > 0)
                    KeyLabel.Text = string.Format("F{0}", Magic.Key);
                else
                    KeyLabel.Text = string.Empty;

                switch (magic.Spell)
                {  //Warrior
                    case Spell.Fencing:
                        SkillButton.Hint = string.Format("Fencing \n\nHitting accuracy will be increased in accordance\nwith practice level.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Slaying:
                        SkillButton.Hint = string.Format("Slaying \n\nHitting accuracy and destructive power will\nbe increased in accordance with practive level.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Thrusting:
                        SkillButton.Hint = string.Format("Thrusting \n\nIncreases the reach of your hits destructive power\nwill increase in accordance with practive level.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HalfMoon:
                        SkillButton.Hint = string.Format("HalfMoon \n\nCause damage to mobs in a semi circle with\nthe shock waves from your fast moving weapon.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FlamingSword:
                        SkillButton.Hint = string.Format("FlamingSword \n\nCause additional damage by summoning the spirit\nof fire into weapon\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ShoulderDash:
                        SkillButton.Hint = string.Format("ShoulderDash \n\nA warrior can push away mobs by charging\nthem with his shoulder, inflicting damage\nif they hit any obstacle.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.CrossHalfMoon:
                        SkillButton.Hint = string.Format("CrossHalfMoon \n\nA warrior uses two powerfull waves of Half Moon\nto inflict damage on all mobs stood next to them.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.TwinDrakeBlade:
                        SkillButton.Hint = string.Format("TwinDrakeBlade \n\nThe art of making multiple power attacks. It has a\nlow chance of stunning the mob temporarly. Stunned\nmobs get 1.5 times more damage inflicted.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.LionRoar:
                        SkillButton.Hint = string.Format("LionRoar \n\nParalyses mobs , duration increases with skill level.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.BladeAvalanche:
                        SkillButton.Hint = string.Format("BladeAvalanche \n\n3-Way Thrusting.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Entrapment:
                        SkillButton.Hint = string.Format("Entrapment \n\nParalyses mobs and draws them to the caster.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Rage:
                        SkillButton.Hint = string.Format("Rage \n\nEnhances your inner force to increase its power\nfor a certain time. Attack power and duration time\nwill depend on the skill level. Once the skill has been used\n you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ProtectionField:
                        SkillButton.Hint = string.Format("ProtectionField \n\nConcentrates inner force and spreads it to all\n the parts of your body. This will enhance the\nprotection from enemies. Defense power and duration\nwill be depend on the skill level. Once the skill\n has been used, you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SlashingBurst:
                        SkillButton.Hint = string.Format("SlashingBurst \n\nAllows The Warrior to Jump 1 Space Over a Obejct or Monster.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.CounterAttack:
                        SkillButton.Hint = string.Format("CounterAttack \n\n Increases AC and AMC for a short period of time\nChance to defend an attack and counter.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ImmortalSkin:
                        SkillButton.Hint = string.Format("ImmortalSkin \n\n Increase defense to reduce attacks.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Fury:
                        SkillButton.Hint = string.Format("Fury \n\n Increases the warriors attack speed for a set period of time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SliceNDice:
                        SkillButton.Hint = string.Format("SliceNDice \n\n Slice though your attacker with three powerfull hits.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.BladeStorm:
                        SkillButton.Hint = string.Format("BladeStorm \n\n Slice though your attacker with powerfull wind blades.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.BlazingSword:
                        SkillButton.Hint = string.Format("BlazingSword \n\nCause additional damage by summoning the spirit\nof fire into weapon\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                        break;

                //Wizard
                case Spell.FireBall:
                        SkillButton.Hint = string.Format("Fireball \n\nInstant Casting \n\nElements of fire are gathered to form\na fireball. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.DragonFlames:
                        SkillButton.Hint = string.Format("DragonFlames \nChanneling Casting \n\nElements of fire are gathered to form\na DragonFlame. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.BrokenSoulCut:
                        SkillButton.Hint = string.Format("BrokenSoulCut \nChanneling Casting \n\nElements of fire are gathered to form\na DragonFlame. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SoulReaper:
                        SkillButton.Hint = string.Format("SoulReaper \nChanneling Casting \n\nElements of dark are gathered to form\na Dark Void. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.LastJudgement:
                        SkillButton.Hint = string.Format("LastJudgement \nChanneling Casting \n\nElements of fire are gathered to form\na Judgement power. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ThunderClap:
                        SkillButton.Hint = string.Format("ThunderClap \nChanneling Casting \n\nElements of electricity are gathered to form\na Thunder. Throw at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ChopChopStar:
                        SkillButton.Hint = string.Format("ChopChopStar \nChanneling Casting \n\nElements of wind are gathered to form\na cutting blade. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SoulEaterSwamp:
                        SkillButton.Hint = string.Format("SoulEaterSwamp \nChanneling Casting \n\nElements of earth are gathered to form\na explodin blast. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HandOfGod:
                        SkillButton.Hint = string.Format("HandOfGod \nChanneling Casting \n\nElements of fire are gathered to form\na explodin blast. cast at monsters for damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ThunderBolt:
                        SkillButton.Hint = string.Format("Thundebolt \n\nInstant Casting \n\nStrikes the foe with a lightning bolt \ninflicting high damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.GreatFireBall:
                        SkillButton.Hint = string.Format("GreatFireBall \n\nInstant Casting\n\nStronger then fire ball, Great Fire Ball\nwill fire up the mobs.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Repulsion:
                        SkillButton.Hint = string.Format("Repulsion \n\nInstant Casting\n\nPush away mobs useing the power of fire.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HellFire:
                        SkillButton.Hint = string.Format("Hellfire \n\nInstant Casting\n\nShoots out a streak of fire attack\nthe monster in front.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Lightning:
                        SkillButton.Hint = string.Format("Lightning \n\nInstant Casting\n\nShoots out a steak of lightning to attack\nthe monster in front.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ElectricShock:
                        SkillButton.Hint = string.Format("ElectrickShock \n\nInstant Casting\n\nStrong shock wave hits the mob and the\nmob will not be able to move or the mob\nwill get confused and fight for you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Teleport:
                        SkillButton.Hint = string.Format("Teleport \n\nInstant Casting\n\nTeleport to a random spot.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FireWall:
                        SkillButton.Hint = string.Format("FireWall \n\nInstant Casting\n\nThis skill will build a fire wall at a designated\nspot to attack the monster passing the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FireBang:
                        SkillButton.Hint = string.Format("FireBang \n\nInstant Casting\n\nFirebang will burst out fire at a designated spot to\nburn all the monster within the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ThunderStorm:
                        SkillButton.Hint = string.Format("Thunderstorm \n\nInstant Casting\n\nThis skill will make a thunder storm with in a designated area \nto attack the monster with in.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MagicShield:
                        SkillButton.Hint = string.Format("MagicShield \n\nInstant Casting\n\nThis skill will use Mp to create protective\nlayer around you\nAttack will be absorbed by the protective layer\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.TurnUndead:
                        SkillButton.Hint = string.Format("TurnUndead \n\nInstant Casting\n\nThis magic will bring birght light into \npower and attack undead monsters\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.IceStorm:
                        SkillButton.Hint = string.Format("IceStorm \n\nInstant Casting\n\nThis skill will make an ice storm with in a designated \narea to attack the monsters with in\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FlameDisruptor:
                        SkillButton.Hint = string.Format("FlameDisruptor \n\nInstant Casting\n\nFlame from the underground will be brought\ninto surface to attack the mobs.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FrostCrunch:
                        SkillButton.Hint = string.Format("FrostCrunch \n\nInstant Casting\n\nFreeze the elements in the air around the \nmonster to slow them down\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Mirroring:
                        SkillButton.Hint = string.Format("Mirroring \n\nInstant Casting\n\nCreate a mirror image of yourself to attack\nthe monsters together\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FlameField:
                        SkillButton.Hint = string.Format("FlameField \n\nInstant Casting\n\nA powerful spell of fire is used to \ndamage surrounding enemies.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Vampirism:
                        SkillButton.Hint = string.Format("Vampirism \n\nInstant Casting\n\nUsing Mp take away monsters Hp to\nincrease your Hp.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Blizzard:
                        SkillButton.Hint = string.Format("Blizzard \n\nConcentrate inner force and spreads it to all\nthe parts of your body.This will enhance the\nprotection from enemies. Defense power and duration\ntime will depend on the skill level. Once the skill\nhas been used, you will have to wait to use it again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MeteorStrike:
                        SkillButton.Hint = string.Format("MeteorStrike \n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with lumps \nof fire falling from the sky.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.IceThrust:
                        SkillButton.Hint = string.Format("IceThrust \n\nInstant Casting\n\nAttack monsters by creating an ice pillar.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MagicBooster:
                        SkillButton.Hint = string.Format("MagicBooster \n\nLasting Effect\n\nIncrease magical damage, but comsume additional MP.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FastMove:
                        SkillButton.Hint = string.Format("FastMove \n\nLimited Effect\n\nIncrease movemoent with rooted skills.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.StormEscape:
                        SkillButton.Hint = string.Format("StormEscape \n\nLimited Effect\n\nParalyze nearby enemies and teleport to the designated location.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.LavaKing:
                        SkillButton.Hint = string.Format("LavaKing \n\nLimited Effect\n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with lumps lava.\nMay cause a burning poison.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FrozenRains:
                        SkillButton.Hint = string.Format("FrozenRains \n\nLimited Effect\n\nInstant Casting\n\nAttacks all monsters within 5x5 square area with Rains.\nMay cause a Frozen poison.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SummonHolyDragon:
                        SkillButton.Hint = string.Format("SummonHolyDragon \n\nInstant Casting\nRequired Items: Amulet\n\nSummon a Deman spirit.This holy spirit will\nuse strong thunder to attack monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.DragNet:
                    SkillButton.Hint = string.Format("Dragnet \n\nInstant Casting\n\nCast a electic field on monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                    break;

                //Taoist
                case Spell.SpiritSword:
                        SkillButton.Hint = string.Format("SpiritSword \n\nIncreases the chance of hitting the target in\n melee combat.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Healing:
                        SkillButton.Hint = string.Format("Healing \n\nInstant Casting\n\n Heals a single target \nrecovering HP over time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Poisoning:
                        SkillButton.Hint = string.Format("Poisoning \n\nInstant Casting\nRequired Items: Poison Powder\n\nThrow poison at mobs to weaken them.\nUse green poison to weaken Hp.\nUse red poison to weaken defense.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SoulFireBall:
                        SkillButton.Hint = string.Format("SoulFireBall \n\nInstant Casting\nRequired Items: Amulet\n\nPut power into a scroll and throw it at \na mob. The scroll will burst into fire.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SoulShield:
                        SkillButton.Hint = string.Format("SoulShield \n\nInstant Casting\nRequired Items: Amulet\n\nBless the partymembers to strengthen there magic\ndefence.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.BlessedArmour:
                        SkillButton.Hint = string.Format("BlessedArmour \n\nInstant Casting\nRequired Items: Amulet\n\nBless the partymemebers to strenghten there defence.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.TrapHexagon:
                        SkillButton.Hint = string.Format("TrapHexagon \n\nInstant Casting\nRequired Items: Amulet\n\nTrap the monster with this magical power\n to stop them from moving. Any damages\nfrom outside source will allow the monsters\nto move again.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SummonSkeleton:
                        SkillButton.Hint = string.Format("SummonSkeleton \n\nInstant Casting\nSummons a Powerful AOE Skeleton, Which will Fight Side By Side With You\nRequired Items: Amulet.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Hiding:
                        SkillButton.Hint = string.Format("Hiding \n\nInstant Casting\nRequired Items: Amulet\n\nMobs will not be able to spot you for a short\nmoment.Mobs will notice you if you start\nto move around.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MassHiding:
                        SkillButton.Hint = string.Format("MassHiding \n\nInstant Casting\nRequired Items: Amulet\n\nMobs will not be able to spot you or your \nparty members for a short moment. \nMobs will notice you and your party if \nyou start to move around.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Revelation:
                        SkillButton.Hint = string.Format("Revelation \n\nInstant Casting\n\nYou will be able to read Hp of others\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MassHealing:
                        SkillButton.Hint = string.Format("MassHealing \n\nInstant Casting\n\nHeal all injured players in the specified\narea by surrounding them with mana.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SummonShinsu:
                        SkillButton.Hint = string.Format("SummonShinsu \n\nInstant Casting\nSummons a Dog, That Will fight Side By Side with you.\nRequired Items: Amulet.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.UltimateEnhancer:
                        SkillButton.Hint = string.Format("UltimateEnhancer \n\nInstant Casting\nRequired Items: Amulet\n\nAbsorb the energy from the surroundings to increase the stats.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.EnergyRepulsor:
                        SkillButton.Hint = string.Format("EnergyRepulsor \n\nInstant Casting\n\nConcentrate your energy for one big blast to push away the monsters around you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Purification:
                        SkillButton.Hint = string.Format("Purification \n\nInstant Casting\n\nHelp others to recover from poisoning and\nparalysis useing this skill.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SummonHolyDeva:
                        SkillButton.Hint = string.Format("SummonHolyDeva \n\nInstant Casting\nRequired Items: Amulet\n\nSummon a holy spirit.This holy spirit will\nuse strong thunder to attack monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Curse:
                        SkillButton.Hint = string.Format("Curse \n\nInstant Casting\nRequired Items: Amulet\n\nReduces mob attacks (Attack Speed, DC ,MC ,SC)\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Hallucination:
                        SkillButton.Hint = string.Format("Hallucination \n\nInstant Casting\nRequired Items: Amulet\n\nThe monster will only see hallucination \nand attack anyone on the way\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Reincarnation:
                        SkillButton.Hint = string.Format("Reincarnation \n\nInstant Casting\nRequired Items: Amulet\n\nRevives a dead players\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.PoisonCloud:
                        SkillButton.Hint = string.Format("PoisonCloud \n\nInstant Casting\nRequired Items: GreenPoison\n\nThrow the amulet and a very strong\npoison cloud will appear in the area.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.EnergyShield:
                        SkillButton.Hint = string.Format("EnergyShield \n\nInstant Casting\nRequired Items: Amulet\n\nCreate an enegy shield to heal immediately when attacked by monsters.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Plague:
                        SkillButton.Hint = string.Format("Plague \n\nInstant Casting\nRequired Items: Amulet\n\nDecreases targets MP and inflict target with various debuffs\nExample: Stun , Curse , Poison and Slow.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HealingCircle:
                        SkillButton.Hint = string.Format("HealingCircle \n\nInstant Casting\nTreatment area friendly target, and the enemy caused spell damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.PetEnhancer:
                        SkillButton.Hint = string.Format("PetEnhancer \n\nInstant Casting\nStrengthening pets defense and power.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HeadShot:
                        SkillButton.Hint = string.Format("HeadShot \n\nInstant Casting\nRequired Items: Amulet\n\nPut power into a scroll and throw it at \na mob. The scroll will burst into ice.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HolyShield:
                        SkillButton.Hint = string.Format("HolyShield \n\nInstant Casting\nRequired Items: Amulet\n\nShield the partymemebers to strenghten there defence (Ac/Amc).\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                        break;

                //Assassin
                case Spell.FatalSword:
                        SkillButton.Hint = string.Format("FatalSword \n\nIncrease attack damage on the monsters.\nalso increases accuracy a little.\nPassive Skill\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.DoubleSlash:
                        SkillButton.Hint = string.Format("DoubleSlash \n\nSlash the monster twice in a quick motion\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Haste:
                        SkillButton.Hint = string.Format("Haste \n\nIncrease the attack speed\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FlashDash:
                        SkillButton.Hint = string.Format("FlashDash \n\nAttack a monster with quick slash and\nparalize the monster\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.HeavenlySword:
                        SkillButton.Hint = string.Format("HeavenlySword \n\nAttack monsters with in 2 steps radius\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.FireBurst:
                        SkillButton.Hint = string.Format("FireBurst \n\nPush away mobs surrounding you\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Trap:
                        SkillButton.Hint = string.Format("Trap \nInstant casting CoolTime 60 secs\n\nTrap the monster for a short while.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MoonLight:
                        SkillButton.Hint = string.Format("Moonlight \n\nHide yourself from monster by turning invisible\nGreater damage is done when you attack monster using\nthis skill.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MPEater:
                        SkillButton.Hint = string.Format("MpEater \nPassive\nAbsord monsters MP to recharge your MP\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.SwiftFeet:
                        SkillButton.Hint = string.Format("SwiftFeet \n\nIncreased Runing Speed\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.LightBody:
                        SkillButton.Hint = string.Format("LightBody \n\nLighten your body using this skill and move faster\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.PoisonSword:
                        SkillButton.Hint = string.Format("PoisonSword \n\nPoison the monsters with a slash of you\nsword.Poison effect will damage the monster\nover time.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.DarkBody:
                        SkillButton.Hint = string.Format("DarkBody \n\nCreate an illusion of yourself to attack\nthe monster while you become invisible.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.CrescentSlash:
                        SkillButton.Hint = string.Format("CrescentSlash \n\nBurst out of the power of your sword and attack all monsters around you.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.Hemorrhage:
                        SkillButton.Hint = string.Format("Hemorrhage \nPassive\nChance to deal cristical damage and inflict bleeding damage.\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.MoonMist:
                        SkillButton.Hint = string.Format("Moon Mist\nActive\nAbility to hide your self from Monster\nYour first attack will be stronger than normal.");
                        break;
                    case Spell.FuryWaves:
                        SkillButton.Hint = string.Format("FuryWaves \n\nBurst out of the power of your sword and attack all monsters around you.\nThis could push or poison. \nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 :  Magic.Level == 4 ? Magic.Level5 : 0);
                        break;
                    case Spell.ShadowStep:
                        SkillButton.Hint = string.Format("ShadowStep \n\nIncreased Sneak Attack\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : Magic.Level == 3 ? Magic.Level4 : Magic.Level == 4 ? Magic.Level5 : 0);
                        break;

                default:

                        break;
                }



                SkillButton.Index = Magic.Icon * 2;
                SkillButton.PressedIndex = Magic.Icon * 2 + 1;

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
        }
        public sealed class AssignKeyPanel : MirImageControl
        {
            public MirButton SaveButton, NoneButton;

            public MirLabel TitleLabel;
            public MirImageControl MagicImage;
            public MirButton[] FKeys;

            public ClientMagic Magic;
            public byte Key;

            public AssignKeyPanel(ClientMagic magic)
            {
                Magic = magic;
                Key = magic.Key;

                Modal = true;
                Index = 710;
                Library = Libraries.CustomPrguse;
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
                    Size = new Size(180, 34),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak,
                    Text = string.Format("Select the Key for: {0}", magic.Spell)
                };

                NoneButton = new MirButton
                {
                    Index = 287, //154
                    HoverIndex = 288,
                    PressedIndex = 289,
                    Library = Libraries.CustomTitle,
                    Parent = this,
                    Location = new Point(284, 64),
                };
                NoneButton.Click += (o, e) => Key = 0;

                SaveButton = new MirButton
                {
                    Library = Libraries.CustomTitle,
                    Parent = this,
                    Location = new Point(284, 101),
                    Index = 156,
                    HoverIndex = 157,
                    PressedIndex = 158,
                };
                SaveButton.Click += (o, e) =>
                {
                    for (int i = 0; i < MapObject.User.Magics.Count; i++)
                    {
                        if (MapObject.User.Magics[i].Key == Key)
                            MapObject.User.Magics[i].Key = 0;
                    }

                    Network.Enqueue(new C.MagicKey { Spell = Magic.Spell, Key = Key });
                    Magic.Key = Key;
                    foreach (SkillBarDialog Bar in GameScene.Scene.SkillBarDialogs)
                        Bar.Update();

                    Dispose();
                };


                FKeys = new MirButton[16];

                FKeys[0] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(17, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F1"
                };
                FKeys[0].Click += (o, e) => Key = 1;

                FKeys[1] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(49, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F2"
                };
                FKeys[1].Click += (o, e) => Key = 2;

                FKeys[2] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(81, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F3"
                };
                FKeys[2].Click += (o, e) => Key = 3;

                FKeys[3] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(113, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F4"
                };
                FKeys[3].Click += (o, e) => Key = 4;

                FKeys[4] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(150, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F5"
                };
                FKeys[4].Click += (o, e) => Key = 5;

                FKeys[5] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(182, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F6",
                };
                FKeys[5].Click += (o, e) => Key = 6;

                FKeys[6] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(214, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F7"
                };
                FKeys[6].Click += (o, e) => Key = 7;

                FKeys[7] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(246, 58),
                    Sound = SoundList.ButtonA,
                    Text = "F8"
                };
                FKeys[7].Click += (o, e) => Key = 8;


                FKeys[8] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(17, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F1"
                };
                FKeys[8].Click += (o, e) => Key = 9;

                FKeys[9] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(49, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F2"
                };
                FKeys[9].Click += (o, e) => Key = 10;

                FKeys[10] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(81, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F3"
                };
                FKeys[10].Click += (o, e) => Key = 11;

                FKeys[11] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(113, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F4"
                };
                FKeys[11].Click += (o, e) => Key = 12;

                FKeys[12] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(150, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F5"
                };
                FKeys[12].Click += (o, e) => Key = 13;

                FKeys[13] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(182, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F6"
                };
                FKeys[13].Click += (o, e) => Key = 14;

                FKeys[14] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(214, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F7"
                };
                FKeys[14].Click += (o, e) => Key = 15;

                FKeys[15] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.CustomPrguse,
                    Parent = this,
                    Location = new Point(246, 95),
                    Sound = SoundList.ButtonA,
                    Text = "Ctrl" + Environment.NewLine + "F8"
                };
                FKeys[15].Click += (o, e) => Key = 16;

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
    public sealed class BigMapDialog : MirControl
    {

        private float ZoomRatio = 1f, TempZoom = 0f;
        private int tempWidth, tempHeight;
        public List<MirImageControl> PublicEvents = new List<MirImageControl>();
        public List<MirLabel> QuestLocations = new List<MirLabel>();
        public BigMapDialog()
        {
            Location = new Point(130, 100);
            Border = true;
            BorderColour = Color.Lime;
            BeforeDraw += (o, e) => OnBeforeDraw();
            MouseMove += BigMapDialog_MouseMove;
            MouseWheel += BigMap_OnMouseWheel;
            Sort = true;
        }

        private void BigMapDialog_MouseMove(object sender, MouseEventArgs e)
        {
            var wRatio = (float)GameScene.Scene.MapControl.Width / tempWidth;
            var hRatio = (float)GameScene.Scene.MapControl.Height / tempHeight;

            GameScene.Scene.BigMapDialog.Hint = ((int)((e.X - GameScene.Scene.BigMapDialog.Location.X) * wRatio)).ToString() + ":" + ((int)((e.Y - GameScene.Scene.BigMapDialog.Location.Y) * hRatio)).ToString();
        }

        void BigMap_OnMouseWheel(object sender, MouseEventArgs e)
        {
            float count = e.Delta / SystemInformation.MouseWheelScrollDelta / 10f;

            if (ZoomRatio - count <= 0 && count < 0) return;
            if (ZoomRatio + count >= 3 && count > 0) return;

            TempZoom += count;
            //GameScene.Scene.ChatDialog.ReceiveChat(string.Format("{0}", TempZoom.ToString()), ChatType.System2);
        }

        private void OnBeforeDraw()
        {
            MapControl map = GameScene.Scene.MapControl;
            if (map == null || !Visible) return;

            //int index = map.BigMap <= 0 ? map.MiniMap : map.BigMap;
            int index = map.BigMap;

            if (index <= 0)
            {
                if (Visible)
                {
                    Visible = false;
                }
                return;
            }

            TrySort();

            Rectangle viewRect = new Rectangle(0, 0, 600, 400);

            Size = Libraries.MiniMap.GetSize(index);

            viewRect.Width = (int)(Size.Width * ZoomRatio);
            viewRect.Height = (int)(Size.Height * ZoomRatio);

            bool dr = true, inc = true;
            if (viewRect.Width < 300)
            {
                viewRect.Width = 300;
                dr = false;
            }

            if (viewRect.Height < 200)
            {
                viewRect.Height = 200;
                dr = false;
            }

            if (viewRect.Width > Settings.ScreenWidth * 0.9f)
            {
                viewRect.Width = (int)(Settings.ScreenWidth * 0.9f);
                inc = false;
            }

            if (viewRect.Height > Settings.ScreenHeight * 0.9f)
            {
                viewRect.Height = (int)(Settings.ScreenHeight * 0.9f);
                inc = false;
            }

            if (TempZoom > 0f && inc)
            {
                ZoomRatio += TempZoom;
            }

            if (TempZoom < 0f && dr)
            {
                ZoomRatio += TempZoom;
            }

            tempWidth = viewRect.Width;
            tempHeight = viewRect.Height;
            TempZoom = 0f;
            //GameScene.Scene.ChatDialog.ReceiveChat(ZoomRatio.ToString(), ChatType.System2);
            viewRect.X = (Settings.ScreenWidth - viewRect.Width) / 2;
            viewRect.Y = (Settings.ScreenHeight - 120 - viewRect.Height) / 2;

            Location = viewRect.Location;
            Size = viewRect.Size;

            float scaleX = Size.Width / (float)map.Width;
            float scaleY = Size.Height / (float)map.Height;

            viewRect.Location = new Point(
                (int)(scaleX * MapObject.User.CurrentLocation.X) - viewRect.Width / 2,
                (int)(scaleY * MapObject.User.CurrentLocation.Y) - viewRect.Height / 2);

            if (viewRect.Right >= Size.Width)
                viewRect.X = Size.Width - viewRect.Width;
            if (viewRect.Bottom >= Size.Height)
                viewRect.Y = Size.Height - viewRect.Height;

            if (viewRect.X < 0) viewRect.X = 0;
            if (viewRect.Y < 0) viewRect.Y = 0;

            Libraries.MiniMap.Draw(index, Location, Size, Color.FromArgb(255, 255, 255));

            int startPointX = (int)(viewRect.X / scaleX);
            int startPointY = (int)(viewRect.Y / scaleY);

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];


                if (ob.Race == ObjectType.Item || ob.Dead || ob.Race == ObjectType.Spell) continue; // || (ob.ObjectID != MapObject.User.ObjectID)
                float x = ((ob.CurrentLocation.X - startPointX) * scaleX) + Location.X;
                float y = ((ob.CurrentLocation.Y - startPointY) * scaleY) + Location.Y;

                Color colour;

                if ((GroupDialog.GroupList.Contains(ob.Name) && MapObject.User != ob) || ob.Name.EndsWith(string.Format("({0})", MapObject.User.Name)))
                    colour = Color.FromArgb(0, 0, 255);
                else
                    if (ob is PlayerObject)
                    colour = Color.FromArgb(255, 255, 255);
                else if (ob is NPCObject || ob.AI == 6)
                    colour = Color.FromArgb(0, 255, 50);
                else
                    colour = Color.FromArgb(255, 0, 0);

                if (ob.IsHero)
                {
                    colour = Color.Purple;
                }

                DXManager.Sprite.Draw2D(ob is PlayerObject ? DXManager.PlayerRadarTexture : DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(x - 0.5F), (int)(y - 0.5F)), colour);

            }
            DisposeEvents();
            if (Visible)
            {
                for (int i = MapControl.QuestLocations.Count - 1; i >=0; i--)
                {
                    MapQuestLocationClient location = MapControl.QuestLocations[i];
                    float x = ((location.Location.X - startPointX) * scaleX) + Location.X;
                    float y = ((location.Location.Y - startPointY) * scaleY) + Location.Y;
                    string text = "";
                    Color color = Color.Empty;
                    switch(location.Icon)
                    {
                        case QuestIcon.ExclamationBlue:
                            color = Color.DodgerBlue;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationYellow:
                            color = Color.Yellow;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationGreen:
                            color = Color.Green;
                            text = "!";
                            break;
                        case QuestIcon.QuestionBlue:
                            color = Color.DodgerBlue;
                            text = "?";
                            break;
                        case QuestIcon.QuestionWhite:
                            color = Color.White;
                            text = "?";
                            break;
                        case QuestIcon.QuestionYellow:
                            color = Color.Yellow;
                            text = "?";
                            break;
                        case QuestIcon.QuestionGreen:
                            color = Color.Green;
                            text = "?";
                            break;
                        case QuestIcon.QuestionRed:
                            color = Color.Red;
                            text = "?";
                            break;
                        case QuestIcon.ExclamationRed:
                            color = Color.Red;
                            text = "!";
                            break;
                        case QuestIcon.QuestionOrange:
                            color = Color.Orange;
                            text = "?";
                            break;
                        case QuestIcon.ExclamationOrange:
                            color = Color.Orange;
                            text = "!";
                            break;

                        case QuestIcon.ExclamationDarkBlue:
                            color = Color.DarkBlue;
                            text = "!";
                            break;

                        case QuestIcon.QuestionDarkBlue:
                            color = Color.DarkBlue;
                            text = "?";
                            break;

                    }

                    QuestLocations.Add(new MirLabel
                    {
                        AutoSize = true,
                        Parent = GameScene.Scene.MiniMapDialog,
                        Font = new Font(Settings.FontName, 9f, FontStyle.Bold),
                        DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                        Text = text,
                        ForeColour = color,
                        Location = new Point((int)(x - Settings.ScreenWidth + GameScene.Scene.MiniMapDialog.Size.Width) - 6, (int)(y) - 10),
                        NotControl = true,
                        Visible = true,
                        Modal = true
                    });
                }
                for (int i = MapControl.MapEvents.Count - 1; i >= 0; i--)
                {
                    MapEventClientSide mapEvent = MapControl.MapEvents[i];

                    float x = ((mapEvent.Location.X - startPointX) * scaleX) + Location.X;
                    float y = ((mapEvent.Location.Y - startPointY) * scaleY) + Location.Y;

                    int imgIndex = 0;
                    switch (mapEvent.EventType)
                    {
                        case EventType.MonsterSlay:
                            imgIndex = 2259;
                            break;
                        case EventType.Invasion:
                            imgIndex = 1811;
                            break;
                        case EventType.DailyBoss:
                            imgIndex = 2091;
                            break;
                        case EventType.WeeklyBoss:
                            imgIndex = 2092;
                            break;
                    }

                    PublicEvents.Add(new MirImageControl()
                    {
                        Parent = this.Parent,
                        Library = Libraries.Items,
                        Index = imgIndex,
                        Location = new Point((int)(x - 10), (int)(y - 10)),
                        NotControl = false,
                        Visible = true,
                        Hint = mapEvent.EventName,
                        Blending = false,
                        Size = new Size(10, 10)
                    });
                }
            }
        }

        public void DisposeQuestLocations()
        {
            for (int i = 0; i < QuestLocations.Count; i++)
                QuestLocations[i].Dispose();

            QuestLocations.Clear();
        }

        public void DisposeEvents()
        {
            for (int i = 0; i < PublicEvents.Count; i++)
                PublicEvents[i].Dispose();

            PublicEvents.Clear();
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
        }

        public void Toggle()
        {
            DisposeEvents();
            Visible = !Visible;

            Redraw();
        }
    }


        public sealed class DuraStatusDialog : MirImageControl
        {
            public MirButton Character;

            public DuraStatusDialog()
            {
                Size = new Size(40, 19);
                Location = new Point((GameScene.Scene.MiniMapDialog.Location.X + 86), GameScene.Scene.MiniMapDialog.Size.Height);

                Character = new MirButton()
                {
                    Index = 2113,
                    Library = Libraries.Prguse,
                    Parent = this,
                    Size = new Size(20, 19),
                    Location = new Point(20, 0),
                    HoverIndex = 2111,
                    PressedIndex = 2112,
                    Sound = SoundList.ButtonA,
                    Hint = "Dura Panel"
                };
                Character.Click += (o, e) =>
                {
                    if (GameScene.Scene.CharacterDuraPanel.Visible == true)
                    {
                        GameScene.Scene.CharacterDuraPanel.Hide();
                        Settings.DuraView = false;
                    }
                    else
                    {
                        GameScene.Scene.CharacterDuraPanel.Show();
                        Settings.DuraView = true;
                    }
                };
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
            }
        }


    public sealed class CharacterDuraPanel : MirImageControl
    {
        public MirImageControl GrayBackground, Background, Helmet, Armour, Belt, Boots, Weapon, Necklace, RightBracelet, LeftBracelet, RightRing, LeftRing, Torch, Stone, Amulet, Mount, Item1, Item2;

        public CharacterDuraPanel()
        {
            Index = 2105;
            Library = Libraries.Prguse;
            Movable = false;
            Location = new Point(Settings.ScreenWidth - 61, 230);

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

            GetCharacterDura();
        }

        public void GetCharacterDura()
        {
            if (GameScene.Scene.CharacterDialog.Grid[0].Item == null) { Weapon.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[1].Item == null) { Armour.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[2].Item == null) { Helmet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[3].Item == null) { Torch.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[4].Item == null) { Necklace.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[5].Item == null) { LeftBracelet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[6].Item == null) { RightBracelet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[7].Item == null) { LeftRing.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[8].Item == null) { RightRing.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[9].Item == null) { Amulet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[10].Item == null) { Belt.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[11].Item == null) { Boots.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[12].Item == null) { Stone.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[13].Item == null) { Mount.Index = -1; }

            for (int i = 0; i < MapObject.User.Equipment.Length; i++)
            {
                if (MapObject.User.Equipment[i] == null) continue;
                UpdateCharacterDura(MapObject.User.Equipment[i]);
            }
        }
        public void UpdateCharacterDura(UserItem item)
        {
            int Warning = item.MaxDura / 2;
            int Danger = item.MaxDura / 5;
            uint AmuletWarning = item.Info.StackSize / 2;
            uint AmuletDanger = item.Info.StackSize / 5;

            switch (item.Info.Type)
            {
                case ItemType.Amulet: //Based on stacks of 5000
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
                    if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.BraceletR].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.BraceletR].Item.UniqueID)
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
                    else if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.BraceletL].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.BraceletL].Item.UniqueID)
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
                    if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.RingR].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.RingR].Item.UniqueID)
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
                    else if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.RingL].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.RingL].Item.UniqueID)
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
}
