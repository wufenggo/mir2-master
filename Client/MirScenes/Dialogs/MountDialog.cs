using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class MountDialog : MirImageControl
    {
        public MirLabel MountName;
        public MirButton CloseButton, MountButton, HelpButton;
        private MirAnimatedControl MountImage;
        public MirImageControl LoyaltyBar, ExperienceBar;
        public MirItemCell[] Grid;

        public int StartIndex = 0;

        public MountDialog()
        {
            Index = 167;
            Library = Libraries.CustomPrguse;
            Movable = true;
            Sort = true;
            Location = new Point(10, 30);
            BeforeDraw += MountDialog_BeforeDraw;

            MountName = new MirLabel
            {
                Location = new Point(30, 20),
                DrawFormat = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter),
                Parent = this,
                NotControl = true,
            };

            MountButton = new MirButton
            {
                Library = Libraries.CustomPrguse,
                Parent = this,
                Sound = SoundList.ButtonA,
                Location = new Point(262, 70)
            };
            MountButton.Click += (o, e) =>
            {
                if (CanRide())
                {
                    Ride();
                }
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Library = Libraries.CustomPrguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            HelpButton = new MirButton
            {
                Index = 257,
                HoverIndex = 258,
                PressedIndex = 259,
                Library = Libraries.CustomPrguse2,
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            HelpButton.Click += (o, e) => GameScene.Scene.HelpDialog.DisplayPage("Mounts");

            MountImage = new MirAnimatedControl
            {
                Animated = false,
                AnimationCount = 16,
                AnimationDelay = 100,
                Index = 0,
                Library = Libraries.CustomPrguse,
                Loop = true,
                Parent = this,
                NotControl = true,
                UseOffSet = true
            };

            ExperienceBar = new MirImageControl
            {
                Index = 172,
                Library = Libraries.CustomPrguse,
                Location = new Point(39, 283),
                Parent = this,
                DrawImage = false,
                NotControl = false,
                Hint = "Experience: 0%"
            };
            ExperienceBar.BeforeDraw += new EventHandler(ExperienceBar_BeforeDraw);

            LoyaltyBar = new MirImageControl
            {
                Index = 173,
                Library = Libraries.CustomPrguse,
                Location = new Point(39, 310),
                Parent = this,
                DrawImage = false,
                NotControl = false,
                Hint = "Loyalty: 0%"
            };
            LoyaltyBar.BeforeDraw += new EventHandler(LoyaltyBar_BeforeDraw);

            Grid = new MirItemCell[Enum.GetNames(typeof(MountSlot)).Length];

            Grid[(int)MountSlot.Reins] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Reins,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)

            };
            Grid[(int)MountSlot.Bells] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Bells,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

            Grid[(int)MountSlot.Saddle] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Saddle,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

            Grid[(int)MountSlot.Ribbon] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Ribbon,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };


            Grid[(int)MountSlot.Mask] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Mask,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

        }

        private void ExperienceBar_BeforeDraw(object sender, EventArgs e)
        {
            bool flag = ExperienceBar.Library == null;
            if (!flag)
            {
                double num = (double)MapObject.User.Equipment[13].CurrentDura / (double)MapObject.User.Equipment[13].MaxDura;
                //double num = (double)MapObject.User.Equipment[13].Experiencie / (double)MapObject.User.Equipment[13].NeedExperiencie;
                bool flag2 = num > 1.0;
                if (flag2)
                {
                    num = 1.0;
                }
                bool flag3 = num <= 0.0;
                if (!flag3)
                {
                    Rectangle section = new Rectangle
                    {
                        Size = new Size((int)((double)ExperienceBar.Size.Width * num), ExperienceBar.Size.Height)
                    };
                    ExperienceBar.Library.Draw(this.ExperienceBar.Index, section, ExperienceBar.DisplayLocation, Color.White, false);
                    ExperienceBar.Hint = string.Format("Experience : {0:#0.##%}", num);
                }
            }
        }

        private void LoyaltyBar_BeforeDraw(object sender, EventArgs e)
        {
            bool flag = LoyaltyBar.Library == null;
            if (!flag)
            {
                double num = (double)MapObject.User.Equipment[13].CurrentDura / (double)MapObject.User.Equipment[13].MaxDura;
                bool flag2 = num > 1.0;
                if (flag2)
                {
                    num = 1.0;
                }
                bool flag3 = num <= 0.0;
                if (!flag3)
                {
                    Rectangle section = new Rectangle
                    {
                        Size = new Size((int)((double)LoyaltyBar.Size.Width * num), LoyaltyBar.Size.Height)
                    };
                    LoyaltyBar.Library.Draw(LoyaltyBar.Index, section, LoyaltyBar.DisplayLocation, Color.White, false);
                    LoyaltyBar.Hint = string.Format("Loyalty : {0:#0.##%}", num);
                }
            }
        }

        void MountDialog_BeforeDraw(object sender, EventArgs e)
        {
            RefreshDialog();
        }

        public void RefreshDialog()
        {
            SwitchType();
            DrawMountAnimation();
        }

        private void SwitchType()
        {
            UserItem MountItem = GameScene.User.Equipment[(int)EquipmentSlot.Mount];
            UserItem[] MountSlots = null;

            if (MountItem != null)
            {
                MountSlots = MountItem.Slots;
            }

            if (MountSlots == null) return;

            int x = 0, y = 0;

            switch (MountSlots.Length)
            {
                case 4:
                    Index = 160;
                    StartIndex = 1170;
                    MountName.Size = new Size(30, 20);
                    MountImage.Location = new Point(110, 230);
                    MountName.Size = new Size(215, 15);
                    MountButton.Index = 164;
                    MountButton.HoverIndex = 165;
                    MountButton.PressedIndex = 166;
                    MountButton.Location = new Point(210, 70);
                    CloseButton.Location = new Point(245, 3);
                    HelpButton.Location = new Point(221, 3);
                    ExperienceBar.Index = 175;
                    LoyaltyBar.Index = 173;
                    Grid[(int)MountSlot.Mask].Visible = false;
                    x = 1; y = 1;
                    break;
                case 5:
                    Index = 167;
                    StartIndex = 1330;
                    MountName.Location = new Point(55, 20);
                    MountImage.Location = new Point(2, 40);
                    MountName.Size = new Size(215, 15);
                    MountButton.Index = 155;
                    MountButton.HoverIndex = 156;
                    MountButton.PressedIndex = 157;
                    MountButton.Location = new Point(262, 70);
                    CloseButton.Location = new Point(295, 3);
                    HelpButton.Location = new Point(271, 3);
                    ExperienceBar.Index = 175;
                    LoyaltyBar.Index = 173;
                    Grid[(int)MountSlot.Mask].Visible = true;
                    x = 0; y = 0;
                    break;
            }

            Grid[(int)MountSlot.Reins].Location = new Point(36 + x, 323 + y);
            Grid[(int)MountSlot.Bells].Location = new Point(90 + x, 323 + y);
            Grid[(int)MountSlot.Saddle].Location = new Point(144 + x, 323 + y);
            Grid[(int)MountSlot.Ribbon].Location = new Point(198 + x, 323 + y);
            Grid[(int)MountSlot.Mask].Location = new Point(252 + x, 323 + y);
        }

        private void DrawMountAnimation()
        {
            if (GameScene.User.MountType < 0)
            {
                MountImage.Index = 0;
                MountImage.Animated = false;
            }
            else
            {
                MountImage.Index = StartIndex + (GameScene.User.MountType * 20);
                MountImage.Animated = true;

                UserItem item = MapObject.User.Equipment[(int)EquipmentSlot.Mount];

                if (item != null)
                {
                    MountName.Text = item.FriendlyName;
                }
            }

        }

        public bool CanRide()
        {
            if (GameScene.User.MountType < 0 || GameScene.User.MountTime + 500 > CMain.Time) return false;
            if (GameScene.User.CurrentAction != MirAction.Standing && GameScene.User.CurrentAction != MirAction.MountStanding) return false;

            return true;
        }

        public void Ride()
        {
            Network.Enqueue(new C.Chat { Message = "@ride" });
        }


        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
        public void Show()
        {
            if (Visible) return;
            if (GameScene.User.MountType < 0)
            {
                MirMessageBox messageBox = new MirMessageBox("You do not own a mount.", MirMessageBoxButtons.OK);
                messageBox.Show();
                return;
            }

            Visible = true;
        }

        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }
    }
}
