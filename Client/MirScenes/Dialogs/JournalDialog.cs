using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using ClientPackets;
using ServerPackets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Client.MirScenes.Dialogs
{
    public class JournalDialog : MirImageControl
    {
        public MirLabel Title;

        public MirButton CloseButton, GainButton, UpButton, DownButton, PositionBar;

        public MirCheckBox CompleteCheckBox;

        private int ScrollBarBaseX = 295;

        public JournalDialog()
        {
            Index = 230;
            Library = Libraries.Prguse2;
            Sort = true;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            Title = new MirLabel
            {
                Text = "Achievements",
                Font = new Font(Settings.FontName, 10f),
                Parent = this,
                NotControl = true,
                Location = new Point(110, 45),
                Size = new Size(660, 40),
                ForeColour = Color.LightPink,
                OutLineColour = Color.Black
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(692, 5),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA
            };
            CloseButton.Click += (o, e) =>
            {
                Hide();
            };

            GainButton = new MirButton
            {
                HoverIndex = 251,
                Index = 250,
                Location = new Point(575, 300),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 252,
                Sound = SoundList.ButtonA,
                CenterText = true,
                Text = "Get Reward"
            };

            UpButton = new MirButton
            {
                HoverIndex = 198,
                Index = 197,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX, 73),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 199,
                Sound = SoundList.ButtonA
            };

            DownButton = new MirButton
            {
                HoverIndex = 208,
                Index = 207,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX, 310),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 209,
                Sound = SoundList.ButtonA
            };

            PositionBar = new MirButton
            {
                Index = 206,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX, 88),
                Parent = this,
                Movable = true,
                Sound = SoundList.None
            };

            CompleteCheckBox = new MirCheckBox
            {
                Index = 1346,
                TickedIndex = 1347,
                LabelText = "Exclude the completion list",
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(315, 50)
            };

        }

        public void Show()
        {
            bool visible = Visible;
            if (!visible)
            {
                Visible = true;
            }
        }

        public void Hide()
        {
            bool flag = !Visible;
            if (!flag)
            {
                Visible = false;
            }
        }

        public void Toggle()
        {
            Visible = !Visible;
            Redraw();
        }
    }
}
