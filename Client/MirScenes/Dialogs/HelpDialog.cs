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
    public sealed class HelpDialog : MirImageControl
    {
        public List<HelpPage> Pages = new List<HelpPage>();

        public MirButton CloseButton, NextButton, PreviousButton;
        public MirLabel PageLabel;
        public HelpPage CurrentPage;

        public int CurrentPageNumber = 0;

        public HelpDialog()
        {
            Index = 920;
            Library = Libraries.CustomPrguse;
            Movable = true;
            Sort = true;

            Location = Center;

            MirImageControl TitleLabel = new MirImageControl
            {
                Index = 57,
                Library = Libraries.CustomTitle,
                Location = new Point(18, 9),
                Parent = this
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.CustomPrguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(210, 485),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                CurrentPageNumber--;

                if (CurrentPageNumber < 0) CurrentPageNumber = Pages.Count - 1;

                DisplayPage(CurrentPageNumber);
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.CustomPrguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(310, 485),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                CurrentPageNumber++;

                if (CurrentPageNumber > Pages.Count - 1) CurrentPageNumber = 0;

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
                Library = Libraries.CustomPrguse2,
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

            List<HelpPage> imagePages = new List<HelpPage> { 
                new HelpPage("Shortcut Information", -1, new ShortcutPage1 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("Shortcut Information", -1, new ShortcutPage2 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("Chat Shortcuts", -1, new ShortcutPage3 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("Movements", 0, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("Attacking", 1, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("Collecting Items", 2, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Health", 3, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Skills", 4, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Skills", 5, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Mana", 6, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Chatting", 7, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Groups", 8, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Durability", 9, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Purchasing", 10, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Selling", 11, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Repairing", 12, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Trading", 13, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Inspecting", 14, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 15, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 16, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 17, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 18, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 19, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Statistics", 20, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Quests", 21, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Quests", 22, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Quests", 23, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Quests", 24, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Mounts", 25, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Mounts", 26, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Fishing", 27, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("Gems and Orbs", 28, null) { Parent = this, Location = location, Visible = false },
            };

            Pages.AddRange(imagePages);
        }


        public void DisplayPage(string pageName)
        {
            if (Pages.Count < 1) return;

            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].Title.ToLower() != pageName.ToLower()) continue;

                DisplayPage(i);
                break;
            }
        }

        public void DisplayPage(int id = 0)
        {
            if (Pages.Count < 1) return;

            if (id > Pages.Count - 1) id = Pages.Count - 1;
            if (id < 0) id = 0;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
                if (CurrentPage.Page != null) CurrentPage.Page.Visible = false;
            }

            CurrentPage = Pages[id];

            if (CurrentPage == null) return;

            CurrentPage.Visible = true;
            if (CurrentPage.Page != null) CurrentPage.Page.Visible = true;
            CurrentPageNumber = id;

            CurrentPage.PageTitleLabel.Text = id + 1 + ". " + CurrentPage.Title;

            PageLabel.Text = string.Format("{0} / {1}", id + 1, Pages.Count);

            Show();
        }

        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }

        public void Toggle()
        {
            if (!Visible)
                Show();
            else
                Hide();
        }
    }

    public class ShortcutPage1 : ShortcutInfoPage
    {
        public ShortcutPage1()
        {
            Shortcuts = new List<ShortcutInfo>();
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Exit).ToString(), "Exit the game"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Logout).ToString(), "Log out"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1) + "-" + CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8), "Skill buttons"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Inventory).ToString(), "Inventory window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Equipment).ToString(), "Status window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skills).ToString(), "Skill window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.KeySettings).ToString(), "KeySettings window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Ranking).ToString(), "Ranking window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bigmap).ToString(), "Show the field map"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Autorun).ToString(), "Auto run On / Off"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Friends).ToString(), "Friend window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Guilds).ToString(), "Guild window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Help).ToString(), "Help window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Rental).ToString(), "Rental window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Relationship).ToString(), "Engagement window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mount).ToString(), "Mount / Dismount ride"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Fishing).ToString(), "Fishing Window Open / Close"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Options).ToString(), "Option window (Open / Close)"));
            LoadKeyBinds();
        }
    }
    public class ShortcutPage2 : ShortcutInfoPage
    {
        public ShortcutPage2()
        {
            Shortcuts = new List<ShortcutInfo>();
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Group).ToString(), "Group window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skillbar).ToString(), "Show the skill bar"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Trade).ToString(), "Trade window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Crafting).ToString(), "Crafting window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Minimap).ToString(), "Minimap window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mentor).ToString(), "Mentor window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreaturePickup).ToString(), "Creature Pickup (Multi Mouse Target)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.GameShop).ToString(), "Gameshop window (Open / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Belt).ToString(), "Belt window (0pen / Close)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangePetmode).ToString(), "Toggle pet attack pet"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangeAttackmode).ToString(), "Toggle player attack mode"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodePeace).ToString(), "Peace Mode - Attack monsters only"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGroup).ToString(), "Group Mode - Attack all subjects except your group members"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGuild).ToString(), "Guild Mode - Attack all subjects except your guild members"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeRedbrown).ToString(), "Good/Evil Mode - Attack PK players and monsters only"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeAll).ToString(), "All Attack Mode - Attack all subjects"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreatureAutoPickup).ToString(), "Creature Pickup (Single Mouse Target)"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Pickup).ToString(), "Highlight / Pickup Items"));
            LoadKeyBinds();
        }
    }
    public class ShortcutPage3 : ShortcutInfoPage
    {
        public ShortcutPage3()
        {
            Shortcuts = new List<ShortcutInfo>();
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Screenshot).ToString(), "Screen Capture"));
            Shortcuts.Add(new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Cameramode).ToString(), "Show / Hide interface"));
            Shortcuts.Add(new ShortcutInfo("Ctrl + Right Click", "Show other players kits"));
            Shortcuts.Add(new ShortcutInfo("MobStats)", "Alt + Mouse Point Show mob info."));
            Shortcuts.Add(new ShortcutInfo("/(username)", "Command to whisper to others"));
            Shortcuts.Add(new ShortcutInfo("!(text)", "Command to shout to others nearby"));
            Shortcuts.Add(new ShortcutInfo("!~(text)", "Command to guild chat"));
            Shortcuts.Add(new ShortcutInfo("MouseWheel", "Place pointer on BigMap Scroll to Size."));
            Shortcuts.Add(new ShortcutInfo("RecallSet", "To allow Group recall @EnableGroupRecall."));
            Shortcuts.Add(new ShortcutInfo("RecallSet", "To Recall your Group @GroupRecall."));
            Shortcuts.Add(new ShortcutInfo("RecallSet", "To Recall just one member @RecallMember (Name)."));
            LoadKeyBinds();
        }
    }

    public class ShortcutInfo
    {
        public string Shortcut { get; set; }
        public string Information { get; set; }

        public ShortcutInfo(string shortcut, string info)
        {
            Shortcut = shortcut.Replace("\n", " + ");
            Information = info;
        }
    }

    public class ShortcutInfoPage : MirControl
    {
        protected List<ShortcutInfo> Shortcuts = new List<ShortcutInfo>();

        public ShortcutInfoPage()
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
            if (Shortcuts == null) return;

            for (int i = 0; i < Shortcuts.Count; i++)
            {
                MirLabel shortcutLabel = new MirLabel
                {
                    Text = Shortcuts[i].Shortcut,
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
                    Text = Shortcuts[i].Information,
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

    public class HelpPage : MirControl
    {
        public string Title;
        public int ImageID;
        public MirControl Page;

        public MirLabel PageTitleLabel;

        public HelpPage(string title, int imageID, MirControl page)
        {
            Title = title;
            ImageID = imageID;
            Page = page;

            NotControl = true;
            Size = new System.Drawing.Size(508, 396 + 40);

            BeforeDraw += HelpPage_BeforeDraw;

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

        void HelpPage_BeforeDraw(object sender, EventArgs e)
        {
            if (ImageID < 0) return;

            Libraries.Help.Draw(ImageID, new Point(DisplayLocation.X, DisplayLocation.Y + 40), Color.White, false);
        }
    }
}
