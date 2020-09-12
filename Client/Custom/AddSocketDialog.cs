using Client.MirControls;
using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Custom
{
    public class AddSocketDialog : MirImageControl
    {
        public MirImageControl HammerContainer, ItemContainer;
        public MirItemCell HammerCell, ItemCell;
        public MirLabel Title;
        public MirButton ConfirmButton;
        public MirButton CloseButton;
        
        public AddSocketDialog()
        {

            ConfirmButton = new MirButton
            {
                Parent = this,
            };
            ConfirmButton.Click += ConfirmButton_Click;
            CloseButton = new MirButton
            {
                Parent = this,
            };
            CloseButton.Click += (o, e) => Hide();
            HammerContainer = new MirImageControl
            {
                Index = 989,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(100, 18),
            };
            HammerCell = new MirItemCell
            {
                GridType = MirGridType.AddSocket,
                Parent = HammerContainer,
                CellType = 0
            };
            ItemContainer = new MirImageControl
            {
                Index = 989,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(100, 18),
            };
            ItemCell = new MirItemCell
            {
                GridType = MirGridType.AddSocket,
                Parent = ItemContainer,
                CellType = 1
            };
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //  Send Packet to server
        }

        /// <summary>
        /// Response from Server triggers.
        /// </summary>
        public void UpdateInterface()
        {

        }

        public void Show()
        {
            if (!Visible)
                Visible = true;
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }
    }
}
