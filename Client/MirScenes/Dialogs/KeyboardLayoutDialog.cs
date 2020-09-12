using Client;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Client.MirScenes.Dialogs
{
    public sealed class KeyboardLayoutDialog : MirImageControl
    {

        public MirLabel CharacterName, PressKeyText, CheckCharacterText;
        public MirImageControl TitleLabel, KeyPanel;
        public MirButton CloseButton, NoticeButton, ResetButton, SaveButton, CancelButton, UpButton, DownButton;
        public MirButton PositionBar;
        public MirTextBox InputKey;
        public MirCheckBox CharacterCheckBox;


        public KeyBindObject[] KeyBinds;
        public KeyBindObject ActiveKey;

        public int ScrollIndex = 0;

        public int ShowCount = 1;

        public int PageRows = 13;

        public KeyboardLayoutDialog()
        {
            Index = 119;
            Library = Libraries.CustomTitle;
            Movable = false;
            Sort = true;
            Location = Center;

            TitleLabel = new MirImageControl
            {
                Index = 3,
                Library = Libraries.CustomTitle,
                Location = new Point(207, 11),
                Parent = this
            };
            CharacterName = new MirLabel
            {
                Text = "Hotkey settings for",
                Location = new Point(130, 39),
                Parent = this,
                AutoSize = true,
                DrawFormat = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter),
                Font = new Font(Settings.FontName, 13f),
                ForeColour = Color.White,
                NotControl = true
            };
            PressKeyText = new MirLabel
            {
                Text = "Please enter the key to be assigned",
                Location = new Point(25, 378),
                Parent = this,
                AutoSize = true,
                DrawFormat = TextFormatFlags.Default,
                Font = new Font(Settings.FontName, 9f),
                ForeColour = Color.White,
                NotControl = true,
                Visible = false
            };
            CloseButton = new MirButton
            {
                Parent = this,
                Index = 361,
                PressedIndex = 363,
                HoverIndex = 362,
                Library = Libraries.CustomPrguse,
                Location = new Point(485, 6),
                Hint = "Exit"
            };
            CloseButton.Click += (o, e) =>
            {
                Hide();
            };

            NoticeButton = new MirButton
            {
                HoverIndex = 147,
                Index = 146,
                Location = new Point(35, 398),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 148,
                Sound = SoundList.ButtonA,
                Hint = "Key Info"
            };
            NoticeButton.Click += (o, e) =>
            {
                bool flag = ActiveKey != null;
                if (!flag)
                {
                    MirMessageBox mirMessageBox = new MirMessageBox("The following keys can not be set with shortcut keys.\n[ Enter ] [ Tab ] [ Pause(break) ] [ Esc ]\nThe following keys can only be used in combination.\n[ Ctrl ] [ Alt ] [ Shift ]", MirMessageBoxButtons.OK);
                    mirMessageBox.Show();
                }
            };

            ResetButton = new MirButton
            {
                HoverIndex = 628,
                Index = 627,
                Location = new Point(120, 398),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 629,
                Sound = SoundList.ButtonA,
                Hint = "Reset Keys."
            };
            ResetButton.Click += (o, e) =>
            {
                bool flag = ActiveKey != null;
                if (!flag)
                {
                    CMain.InputKeys.ResetKey();
                    KeyBindObject[] keyBinds = KeyBinds;
                    for (int j = 0; j < keyBinds.Length; j++)
                    {
                        KeyBindObject keyBindObject = keyBinds[j];
                        keyBindObject.RefreshKey();
                    }
                }
            };

            SaveButton = new MirButton
            {
                HoverIndex = 587,
                Index = 586,
                Location = new Point(430, 398),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 588,
                Sound = SoundList.ButtonA,
                Hint = "Save Changes."
            };
            SaveButton.Click += (o, e) =>
            {
                SaveKey();
            };

            CancelButton = new MirButton
            {
                HoverIndex = 204,
                Index = 203,
                Location = new Point(360, 398),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 205,
                Sound = SoundList.ButtonA,
                Hint = "Cancel Changes."
            };
            CancelButton.Click += (o, e) =>
            {
                CancelKey();
            };

            CharacterCheckBox = new MirCheckBox
            {
                Index = 1346,
                TickedIndex = 1347,
                LabelText = "By character\nkey settings",
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(410, 40)
            };
            MirControl arg_448_0 = CharacterCheckBox;
            //EventHandler arg_448_1;
            //if ((arg_448_1 = KeyboardLayoutDialog.<> c.<> 9__20_5) == null)
            //{
             //   arg_448_1 = (KeyboardLayoutDialog.<> c.<> 9__20_5 = new EventHandler(KeyboardLayoutDialog.<> c.<> 9.<.ctor > b__20_5));
            //}
            //arg_448_0.Click += arg_448_1;

            KeyPanel = new MirImageControl
            {
                Parent = this,
                Size = new Size(500, 280),
                Location = new Point(13, 95),
                Visible = true
            };
            KeyPanel.MouseWheel += new MouseEventHandler(KeyPanel_MouseWheel);

            UpButton = new MirButton
            {
                HoverIndex = 198,
                Index = 197,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(488, 92),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 199,
                Sound = SoundList.ButtonA
            };
            UpButton.Click += (o, e) =>
            {
                bool flag = ScrollIndex == 0;
                if (!flag)
                {
                    ScrollIndex--;
                    UpdateKeyBinds();
                    UpdateScrollPosition();
                }
            };

            DownButton = new MirButton
            {
                HoverIndex = 208,
                Index = 207,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(488, 363),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 209,
                Sound = SoundList.ButtonA
            };
            DownButton.Click += (o, e) =>
            {
                bool flag = ScrollIndex == ShowCount - PageRows;
                if (!flag)
                {
                    ScrollIndex++;
                    UpdateKeyBinds();
                    UpdateScrollPosition();
                }
            };

            PositionBar = new MirButton
            {
                Index = 206,
                Library = Libraries.Prguse2,
                Location = new Point(488, 108),
                Parent = this,
                Movable = true,
                Sound = SoundList.None
            };
            PositionBar.OnMoving += new MouseEventHandler(PositionBar_OnMoving);

            InputKey = new MirTextBox
            {
                BackColour = Color.DarkGray,
                ForeColour = Color.Black,
                Parent = this,
                Size = new Size(1, 1),
                Location = new Point(0, 0),
                MaxLength = 80,
                Visible = false,
                Font = new Font(Settings.FontName, 10f)
            };
            InputKey.TextBox.KeyDown += new KeyEventHandler(InputKey_KeyDown);
            KeyBinds = new KeyBindObject[CMain.InputKeys.Keylist.Count];
            ShowCount = KeyBinds.Length;
            for (int i = 0; i < CMain.InputKeys.Keylist.Count; i++)
            {
                KeyBinds[i] = new KeyBindObject
                {
                    Parent = KeyPanel,
                    Location = new Point(0, i * 22),
                    Size = new Size(500, 20),
                    Key = CMain.InputKeys.Keylist[i]
                };
                KeyBinds[i].MouseWheel += new MouseEventHandler(KeyPanel_MouseWheel);
            }
        }

        private void SaveKey()
        {
            bool flag = ActiveKey == null;
            if (!flag)
            {
                ActiveKey.SaveKey();
                ActiveKey = null;
                InputKey.Visible = false;
                PressKeyText.Visible = false;
            }
        }


        private void CancelKey()
        {
            bool flag = ActiveKey == null;
            if (!flag)
            {
                ActiveKey.CancelKey();
                ActiveKey = null;
                InputKey.Visible = false;
                PressKeyText.Visible = false;
            }
        }

        public void RefreshKeys()
        {
            KeyBindObject[] keyBinds = KeyBinds;
            for (int i = 0; i < keyBinds.Length; i++)
            {
                KeyBindObject keyBindObject = keyBinds[i];
                keyBindObject.RefreshKey();
            }
        }

        private void InputKey_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = ActiveKey == null;
            if (!flag)
            {
                bool flag2 = e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Pause;
                if (flag2)
                {
                    MirMessageBox mirMessageBox = new MirMessageBox("The following keys can not be set with shortcut keys.\n[ Enter ] [ Tab ] [ Pause(break) ] [ Esc ]\nThe following keys can only be used in combination.\n[ Ctrl ] [ Alt ] [ Shift ]", MirMessageBoxButtons.OK);
                    mirMessageBox.Show();
                    CancelKey();
                }
                else
                {
                    bool flag3 = e.KeyCode == Keys.Return;
                    if (flag3)
                    {
                        SaveKey();
                    }
                    else
                    {
                        bool control = e.Control;
                        if (control)
                        {
                            ActiveKey.Key.CutomKey.RequireCtrl = 1;
                        }
                        bool shift = e.Shift;
                        if (shift)
                        {
                            ActiveKey.Key.CutomKey.RequireShift = 1;
                        }
                        bool alt = e.Alt;
                        if (alt)
                        {
                            ActiveKey.Key.CutomKey.RequireAlt = 1;
                        }
                        ActiveKey.Key.CutomKey.Key = e.KeyCode;
                    }
                }
            }
        }

        public void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int x = 488;
            int num = PositionBar.Location.Y;
            bool flag = num >= DownButton.Location.Y - 20;
            if (flag)
            {
                num = DownButton.Location.Y - 20;
            }
            bool flag2 = num < UpButton.Location.Y + 16;
            if (flag2)
            {
                num = UpButton.Location.Y + 16;
            }
            int num2 = num - 108;
            int num3 = 255 / (ShowCount - PageRows);
            double d = (double)(num2 / num3);
            ScrollIndex = (int)Convert.ToInt16(Math.Floor(d));
            bool flag3 = ScrollIndex > ShowCount - PageRows;
            if (flag3)
            {
                ScrollIndex = ShowCount - PageRows;
            }
            bool flag4 = ScrollIndex <= 0;
            if (flag4)
            {
                ScrollIndex = 0;
            }
            UpdateKeyBinds();
            PositionBar.Location = new Point(x, num);
        }

        private void KeyPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int num = e.Delta / SystemInformation.MouseWheelScrollDelta;
            bool flag = ScrollIndex == 0 && num >= 0;
            if (!flag)
            {
                bool flag2 = ScrollIndex == ShowCount - PageRows && num <= 0;
                if (!flag2)
                {
                    ScrollIndex -= ((num > 0) ? 1 : -1);
                    UpdateKeyBinds();
                    UpdateScrollPosition();
                }
            }
        }

        private void UpdateScrollPosition()
        {
            int num = 255 / (ShowCount - PageRows);
            int x = 488;
            int num2 = 108 + ScrollIndex * num;
            bool flag = num2 >= DownButton.Location.Y - 20;
            if (flag)
            {
                num2 = DownButton.Location.Y - 20;
            }
            bool flag2 = num2 < UpButton.Location.Y + 16;
            if (flag2)
            {
                num2 = UpButton.Location.Y + 16;
            }
            PositionBar.Location = new Point(x, num2);
        }

        private void UpdateKeyBinds()
        {
            bool flag = ShowCount < PageRows;
            if (flag)
            {
                ScrollIndex = 0;
            }
            for (int i = 0; i < KeyBinds.Length; i++)
            {
                KeyBinds[i].Location = new Point(0, i * 22 - ScrollIndex * 22);
                bool flag2 = ScrollIndex <= i && ScrollIndex + PageRows > i;
                if (flag2)
                {
                    KeyBinds[i].Show();
                }
                else
                {
                    KeyBinds[i].Hide();
                }
            }
        }

        public void Hide()
        {
            bool flag = ActiveKey != null;
            if (!flag)
            {
                bool flag2 = !Visible;
                if (!flag2)
                {
                    Visible = false;
                }
            }
        }

        public void Show()
        {
            bool visible = Visible;
            if (!visible)
            {
                Visible = true;
                UpdateKeyBinds();
                bool flag = GameScene.User != null;
                if (flag)
                {
                    CharacterName.Text = GameScene.User.Name + "'s HotKey Settings";
                    CharacterName.Location = new Point(256 - CharacterName.Size.Width / 2, 39);
                }
            }
        }
    }
}
