using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using System.Drawing;
using System.Windows.Forms;

using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class ItemRentingDialog : MirImageControl
    {
        public static UserItem RentalItem;

        public readonly MirItemCell ItemCell;
        public uint RentalPeriod;
        /*
        private readonly MirLabel _nameLabel, _rentalPeriodLabel;
        private readonly MirButton _lockButton, _setRentalPeriodButton, _confirmButton;
        */
        public ItemRentingDialog()
        {
            Index = 238;
            Library = Libraries.Prguse;
            Movable = true;
            Size = new Size(204, 109);
            Location = new Point(Settings.ScreenWidth - Size.Width - Size.Width / 2, Size.Height * 2 + Size.Height / 2 + 15);
            Sort = true;

            // Confirm Button
            /*
            _confirmButton = new MirButton
            {
                Index = 812,
                HoverIndex = 813,
                Location = new Point(130, 76),
                Size = new Size(58, 28),
                Library = Libraries.Title,
                Parent = this,
                PressedIndex = 814,
                Sound = SoundList.ButtonA,
                Enabled = false
            };
            //_confirmButton.Click += (o, e) =>
            //{
            //   Network.Enqueue(new C.ConfirmItemRental());
            //};

            // Close Button

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
            */
        }
    }
}