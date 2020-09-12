using Client.MirControls;
using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Client.MirSounds;
using System.Windows.Forms;
using S = ServerPackets;
using Client.MirNetwork;
using C = ClientPackets;
using Client.MirObjects;
using System.Text.RegularExpressions;



namespace Client.MirScenes.Dialogs
{
    public sealed class BounsDialog : MirImageControl
    {
        public BounsDialog()
        {
            Index = 256;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;

            #region Labels.

            var _Label = new MirLabel
            {
                Location = new Point(54, 50),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.GreenYellow,
                Font = new Font(Settings.FontName, 14F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Guild Point Abilty Menu",
            };

            var _pointsLabel = new MirLabel
            {
                Location = new Point(8, 426),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.GreenYellow,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Guild Abilty Points :",
            };

            var  _dcLabel = new MirLabel
            {
                Location = new Point(34, 90),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Dc :",
            };

            var _mcLabel = new MirLabel
            {
                Location = new Point(34, 110),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Mc :",
            };

            var _scLabel = new MirLabel
            {
                Location = new Point(34, 130),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Sc :",
            };

            var _acLabel = new MirLabel
            {
                Location = new Point(34, 150),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Ac :",
            };

            var _amcLabel = new MirLabel
            {
                Location = new Point(20, 170),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = "Amc :",
            };

            #endregion

            #region Buttons.

            var _closeButton = new MirButton
            {
                Parent = this,
                Index = 363,
                PressedIndex = 365,
                HoverIndex = 364,
                Library = Libraries.Title,
                Location = new Point(220, 16),
                Hint = "Exit"
            };
            _closeButton.Click += (o, e) => Hide();

            var _saveButton = new MirButton
            {
                Parent = this,
                Index = 156,
                PressedIndex = 158,
                HoverIndex = 157,
                Library = Libraries.Title,
                Location = new Point(30, 16),
                Hint = "Save your Settings"
            };

            var _dcplusButton = new MirButton
            {
                Parent = this,
                Index = 918,
                Library = Libraries.Prguse,
                Location = new Point(220, 94),
                Hint = "Add Dc"
            };

            var _dcminusButton = new MirButton
            {
                Parent = this,
                Index = 917,
                Library = Libraries.Prguse,
                Location = new Point(240, 94),
                Hint = "Remove Dc"
            };

            var _mcplusButton = new MirButton
            {
                Parent = this,
                Index = 918,
                Library = Libraries.Prguse,
                Location = new Point(220, 114),
                Hint = "Add Mc"
            };

            var _mcminusButton = new MirButton
            {
                Parent = this,
                Index = 917,
                Library = Libraries.Prguse,
                Location = new Point(240, 114),
                Hint = "Remove Mc"
            };

            var _scplusButton = new MirButton
            {
                Parent = this,
                Index = 918,
                Library = Libraries.Prguse,
                Location = new Point(220, 134),
                Hint = "Add Sc"
            };

            var _scminusButton = new MirButton
            {
                Parent = this,
                Index = 917,
                Library = Libraries.Prguse,
                Location = new Point(240, 134),
                Hint = "Remove Sc"
            };

            var _acplusButton = new MirButton
            {
                Parent = this,
                Index = 918,
                Library = Libraries.Prguse,
                Location = new Point(220, 154),
                Hint = "Add Ac"
            };

            var _acminusButton = new MirButton
            {
                Parent = this,
                Index = 917,
                Library = Libraries.Prguse,
                Location = new Point(240, 154),
                Hint = "Remove Ac"
            };

            var _amcplusButton = new MirButton
            {
                Parent = this,
                Index = 918,
                Library = Libraries.Prguse,
                Location = new Point(220, 174),
                Hint = "Add Amc"
            };

            var _amcminusButton = new MirButton
            {
                Parent = this,
                Index = 917,
                Library = Libraries.Prguse,
                Location = new Point(240, 174),
                Hint = "Remove Amc"
            };

            #endregion

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
