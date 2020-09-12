using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using S = ServerPackets;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class HeroHelpDialog : MirImageControl
    {
        public List<HeroHelpPage> HeroPages = new List<HeroHelpPage>();

        public MirButton CloseButton, NextButton, PreviousButton;
        public MirLabel PageLabel;
        public HeroHelpPage CurrentPage;

        public int CurrentPageNumber = 0;

        public HeroHelpDialog()
        {
            Index = 920;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;

            Location = Center;

            MirImageControl TitleLabel = new MirImageControl
            {
                Index = 57,
                Library = Libraries.Title,
                Location = new Point(18, 9),
                Parent = this
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(210, 485),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                CurrentPageNumber--;

                if (CurrentPageNumber < 0) CurrentPageNumber = HeroPages.Count - 1;

                DisplayPage(CurrentPageNumber);
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(310, 485),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                CurrentPageNumber++;

                if (CurrentPageNumber > HeroPages.Count - 1) CurrentPageNumber = 0;

                DisplayPage(CurrentPageNumber);
            };

            PageLabel = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 9F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(230, 480),
                Size = new Size(80, 20)
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(509, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            LoadImagePages();

            DisplayPage();
        }

        private void LoadImagePages()
        {
            Point location = new Point(12, 35);

            Dictionary<string, string> keybinds = new Dictionary<string, string>();

            List<HeroHelpPage> imagePages = new List<HeroHelpPage> { 
                new HeroHelpPage("Shortcut Information", -1, new HeroShortcutPage1 { Parent = this } ) { Parent = this, Location = location, Visible = false },                 
                new HeroHelpPage("CreateHero", 29, null) { Parent = this, Location = location, Visible = false }, 
                new HeroHelpPage("ReviveHero", 30, null) { Parent = this, Location = location, Visible = false },
                new HeroHelpPage("HeroItems", 31, null) { Parent = this, Location = location, Visible = false }, 
                new HeroHelpPage("HeroPots", 32, null) { Parent = this, Location = location, Visible = false },
                new HeroHelpPage("HeroAi", 33, null) { Parent = this, Location = location, Visible = false }, 
                new HeroHelpPage("HeroBuff", 34, null) { Parent = this, Location = location, Visible = false },
        };

            HeroPages.AddRange(imagePages);
        }

        public void DisplayPage(string pageName)
        {
            if (HeroPages.Count < 1) return;

            for (int i = 0; i < HeroPages.Count; i++)
            {
                if (HeroPages[i].Title.ToLower() != pageName.ToLower()) continue;

                DisplayPage(i);
                break;
            }
        }

        public void DisplayPage(int id = 0)
        {
            if (HeroPages.Count < 1) return;

            if (id > HeroPages.Count - 1) id = HeroPages.Count - 1;
            if (id < 0) id = 0;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
                if (CurrentPage.HeroPage != null) CurrentPage.HeroPage.Visible = false;
            }

            CurrentPage = HeroPages[id];

            if (CurrentPage == null) return;

            CurrentPage.Visible = true;
            if (CurrentPage.HeroPage != null) CurrentPage.HeroPage.Visible = true;
            CurrentPageNumber = id;

            CurrentPage.PageTitleLabel.Text = id + 1 + ". " + CurrentPage.Title;

            PageLabel.Text = string.Format("{0} / {1}", id + 1, HeroPages.Count);

            Show();
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

    public class HeroShortcutPage1 : HeroShortcutInfoPage
    {
        public HeroShortcutPage1()
        {
            HeroShortcuts = new List<HeroShortcutInfo>();
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroInventory).ToString(), "Inventory window (open / close)"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroEquipment).ToString(), "Status window (open / close)"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroSkills).ToString(), "Skill window (open / close)"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Belt).ToString(), "Belt window (open / close)"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroBelt1).ToString(), "PotBar Key 7"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroBelt2).ToString(), "PotBar Key 8"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroBelt1Alt).ToString(), "PotBar NumPad 7"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.HeroBelt2Alt).ToString(), "PotBar NumPad 8"));
            HeroShortcuts.Add(new HeroShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.SummonHero).ToString(), "Summon/Desummon"));
            HeroShortcuts.Add(new HeroShortcutInfo("Ctrl + Right Click", "Show other players kits"));
            HeroShortcuts.Add(new HeroShortcutInfo("Ctrl + MouseWheel" , "Move hero to pointer."));
            HeroShortcuts.Add(new HeroShortcutInfo("Red Sign !", "Hero General Quest."));
            HeroShortcuts.Add(new HeroShortcutInfo("DarkBlue Sign !", "Hero Dayily Quest."));
            HeroShortcuts.Add(new HeroShortcutInfo("Golden Sign !", "Hero Story Quest."));
            HeroShortcuts.Add(new HeroShortcutInfo("DualSkills" , "Must be learned by Master and Hero."));

            LoadKeyBinds();
        }
    }

    public class HeroShortcutInfo
    {
        public string HeroShortcut { get; set; }
        public string HeroInformation { get; set; }

        public HeroShortcutInfo(string shortcut, string info)
        {
            HeroShortcut = shortcut.Replace("\n", " + ");
            HeroInformation = info;
        }
    }

    public class HeroShortcutInfoPage : MirControl
    {
        protected List<HeroShortcutInfo> HeroShortcuts = new List<HeroShortcutInfo>();

        public HeroShortcutInfoPage()
        {
            Visible = false;

            MirLabel shortcutTitleLabel = new MirLabel
            {
                Text = "Shortcuts",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(13, 75),
                Size = new Size(100, 30)
            };

            MirLabel infoTitleLabel = new MirLabel
            {
                Text = "Information",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(114, 75),
                Size = new Size(405, 30)
            };
        }

        public void LoadKeyBinds()
        {
            if (HeroShortcuts == null) return;

            for (int i = 0; i < HeroShortcuts.Count; i++)
            {
                MirLabel shortcutLabel = new MirLabel
                {
                    Text = HeroShortcuts[i].HeroShortcut,
                    ForeColour = Color.Yellow,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(18, 107 + (20 * i)),
                    Size = new Size(95, 23),
                };

                MirLabel informationLabel = new MirLabel
                {
                    Text = HeroShortcuts[i].HeroInformation,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    ForeColour = Color.White,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(119, 107 + (20 * i)),
                    Size = new Size(400, 23),
                };
            }
        }
    }

        public class HeroHelpPage : MirControl
        {
            public string Title;
            public int ImageID;
            public MirControl HeroPage;

            public MirLabel PageTitleLabel;

            public HeroHelpPage(string title, int imageID, MirControl page)
            {
                Title = title;
                ImageID = imageID;
                HeroPage = page;

                NotControl = true;
                Size = new System.Drawing.Size(508, 396 + 40);

                BeforeDraw += HeroHelpPage_BeforeDraw;

                PageTitleLabel = new MirLabel
                {
                    Text = Title,
                    Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    Parent = this,
                    Size = new System.Drawing.Size(242, 30),
                    Location = new Point(135, 4)
                };
            }

            void HeroHelpPage_BeforeDraw(object sender, EventArgs e)
            {
                if (ImageID < 0) return;

                Libraries.Help.Draw(ImageID, new Point(DisplayLocation.X, DisplayLocation.Y + 40), Color.White, false);
            }
        }
    }