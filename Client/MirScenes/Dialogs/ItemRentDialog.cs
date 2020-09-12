using System;
using System.Diagnostics;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using System.Drawing;
using System.Windows.Forms;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class ItemRentDialog : MirImageControl
    {
        //private readonly MirLabel _nameLabel, _rentalPriceLabel;
        //private readonly MirButton _lockButton, _rentalPriceButton;

        public ItemRentDialog()
        {
            Index = 238;
            Library = Libraries.Prguse;
            Movable = true;
            Size = new Size(204, 109);
            Location = new Point(Settings.ScreenWidth - Size.Width - Size.Width / 2, Size.Height + Size.Height / 2);
            Sort = true;

            var closeButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(180, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            //closeButton.Click += (sender, args) =>
        }
    }
}