using System;
using System.Collections.Generic;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Client.MirNetwork;

using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class ItemRentalDialog : MirImageControl
    {
        //private readonly ItemRow[] _itemRows = new ItemRow[3];
        private DateTime _lastRequestTime = DateTime.Now;

        public ItemRentalDialog()
        {
            Index = 230;
            Library = Libraries.CustomPrguse;
            Movable = true;
            Size = new Size(400, 174);
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            Sort = true;

            // Rented Tab

            var _rentedTabButton = new MirButton
            {
                Index = 804,
                Location = new Point(8, 32),
                Size = new Size(72, 23),
                Library = Libraries.CustomTitle,
                Parent = this,
                Sound = SoundList.ButtonA,
                Enabled = false
            };

            // Borrowed Tab

            var _borrowedTabButton = new MirButton
            {
                Index = 807,
                Location = new Point(81, 32),
                Size = new Size(84, 23),
                Library = Libraries.CustomTitle,
                Parent = this,
                Sound = SoundList.ButtonA,
                Enabled = false
            };

            // Rent Item Button

            var _rentItemButton = new MirButton
            {
                Index = 808,
                HoverIndex = 809,
                Location = new Point(295, 144),
                Size = new Size(85, 29),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 810,
                Sound = SoundList.ButtonA,
            };
            //rentItemButton.Click += (o, e) =>
            //{
            //  Network.Enqueue(new C.ItemRentalRequest());
            // };

            // Close Button

            var closeButton = new MirButton
            {
                HoverIndex = 362,
                Index = 361,
                Location = new Point(375, 3),
                Library = Libraries.CustomPrguse,
                Parent = this,
                PressedIndex = 363,
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            closeButton.Click += (o, e) => Hide();
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
}