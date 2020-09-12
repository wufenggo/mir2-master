using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirScenes.Dialogs
{
    public class HeroStashDialog : MirImageControl
    {
        public MirButton CloseButton;
        public MirLabel Label;
        public static string ActiveHero = string.Empty;

        public HeroIcon[] Icons = new HeroIcon[8];
        public HeroIcon Chosen;

        public HeroStashDialog()
        {
            Index = 1688;
            Library = Libraries.Prguse;
            Visible = true;
            Location = new System.Drawing.Point(Settings.ScreenWidth / 2 - 160, Settings.ScreenHeight / 2 - 80);

            Label = new MirLabel
            {
                Location = new Point(140, 5),
                AutoSize = true,
                Parent = this,
                BackColour = Color.Transparent,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 10F),
                NotControl = true,
                Text = "Hero Stash",
            };

            CloseButton = new MirButton
            {
                Location = new Point(330, 4),
                Index = 360,
                HoverIndex = 361,
                PressedIndex = 362,
                Library = Libraries.Prguse2,
                Visible = true,
                Parent = this,
                Hint = "Close"
            };
            CloseButton.Click += (o, e) =>
            {
                if (Visible)
                    Hide();
            };

            Chosen = new HeroIcon()
            {
                Location = new Point(20, 65),
                Visible = true,
                Parent = this,
                Enabled = true,              
            };

            for (int i = 0; i < Icons.Length; i++)
            {
                Icons[i] = new HeroIcon()
                {
                    Location = new Point(99 + (i % 4 * 60), 61 + (i / 4 * 41)),
                    Visible = true,
                    Parent = this,
                    Border = true,

                };
                Icons[i].BeforeDraw += HeroStashDialog_BeforeDraw;
            }
        }

        private void HeroStashDialog_BeforeDraw(object sender, EventArgs e)
        {
            for (int i = 0; i < Icons.Length; i++)
            {
                if (i > GameScene.User.HeroCap - 1 || Icons[i].Name.Text == string.Empty)
                {
                    Icons[i].Index = 1689;
                    Icons[i].Border = false;
                    continue;

                }
                else if (Icons[i].Name.Text != ActiveHero)
                {
                    Icons[i].Enabled = true;
                    Icons[i].Border = true;
                    Icons[i].BorderColour = Color.Red;
                }
            }

        }

        public void Show()
        {
            if (!Visible)
            {
               Visible = true;
            }
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }

        public void RefreshGUI()
        {
            
            for (int i = 0; i < Icons.Length; i++)
            {
                if (GameScene.User.HeroCap < 8)
                {
                    if (Icons[i].Name.Text == ActiveHero)
                    {
                        Chosen.Index = Icons[i].Index;
                        Chosen.Name.Text = Icons[i].Name.Text;
                        Icons[i].Border = true;
                        Icons[i].BorderColour = Color.Lime;
                    }
                }

            }

        }
    }


    public class HeroIcon : MirImageControl
    {
        public MirLabel Name;

        public HeroIcon()
        {
            Index = 1770;
            Library = Libraries.Prguse;
            Visible = true;

            Name = new MirLabel()
            {
                Location = new Point(-3, 24),
                Visible = true,
                Parent = this,
                Text = string.Empty,
                AutoSize = true,
                NotControl = true,
                Font = new Font(Settings.FontName, 5F),
             };

            Click += (o, e) =>
            {
                Network.Enqueue(new ClientPackets.StashHero() { Name = Name.Text });
            };
        }


    }
}
