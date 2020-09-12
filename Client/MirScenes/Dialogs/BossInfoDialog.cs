using Client.Custom;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirScenes.Dialogs
{
    public class BossInfoDialog : MirImageControl
    {
        MonsterObject mob;
        public MirLabel mobLabel;
        public MirImageControl mobImage;
        public MirButton CloseButton;
        public List<MirImageControl> Poisons = new List<MirImageControl>();

        public BossInfoDialog()
        {
            Index = 990;
            Library = Libraries.Prguse;
            Visible = true;
            Location = new System.Drawing.Point(Settings.ScreenWidth / 2 - 160, 0);
            Opacity = 0.75f;
            NotControl = true;

            mobImage = new MirImageControl
            {
                NotControl = true,
                Parent = this,
                Visible = true,
                Location = new Point(15, 15),
                Library = Libraries.MobImage,
                GrayScale = true,               //  Add a grey scale to the image? :D
        };
            mobLabel = new MirLabel
            {
                BackColour = Color.Transparent,
                OutLine = false,
                ForeColour = Color.Goldenrod,
                Parent = this,
                NotControl = true,
                Location = new Point(110, 15),
                AutoSize = true
            };
            CloseButton = new MirButton
            {
                Location = new Point(120, 100),
                Index = 316,
                HoverIndex = 315,
                PressedIndex = 317,
                Library = Libraries.Title,
                Visible = false,
                Parent = this
            };
            CloseButton.Click += (o, e) =>
            {
                if (Visible)
                    Hide();
            };
        }
 

        public void UpdateMobHP(uint amount)
        {
            if (mob != null && !mob.Dead)
                mob.CurrentHealth = amount;
            string temp = string.Empty;
            if (mob.Name.Length > 0)
                temp += string.Format("Name : {0}\n", mob.Name);
            temp += string.Format("Defense :\n");
            if (mob.Health > 0)
                temp += string.Format("Health {0}/{1}", mob.CurrentHealth, mob.Health);
            if (mob.MinAC > 0)
                temp += string.Format(", AC {0}", mob.MinAC);
            if (mob.MinMAC > 0)
                temp += string.Format(", MAC {0}", mob.MinMAC);
            if (mob.Agil > 0)
                temp += string.Format(", Agil {0}", mob.Agil);
            temp += "\nAttack :\n";
            if (mob.MinDC > 0)
                temp += string.Format("DC {0}", mob.MinDC);
            if (mob.MinMC > 0)
                temp += string.Format(", MC {0}", mob.MinMC);
            if (mob.MinSC > 0)
                temp += string.Format(", SC {0}", mob.MinSC);
            if (mob.AttkSpeed > 0)
                temp += string.Format(", Attacks Per Second {0}", mob.AttkSpeed / 1000);
            mobLabel.Text = string.Format(temp + ".");
            mobLabel.Visible = true;
            mobLabel.Parent = this;

            mobImage.Index = (int)mob.BaseImage;
        }

        public void UpdateMob(MapObject mon)
        {
            if (mon == null || mon.Race != ObjectType.Monster)
                return;
            if (mon.Race == ObjectType.Monster)
                mob = (MonsterObject)mon;

            if (mob != null && !mob.Dead)
            {
                string temp = string.Empty;
                if (mob.Name.Length > 0)
                    temp += string.Format("Name : {0}\n", mob.Name);
                temp += string.Format("Defense :\n");
                if (mob.Health > 0)
                    temp += string.Format("Health {0}/{1}", mob.CurrentHealth, mob.Health);
                if (mob.MinAC > 0)
                    temp += string.Format(", AC {0}", mob.MinAC);
                if (mob.MinMAC > 0)
                    temp += string.Format(", MAC {0}", mob.MinMAC);
                if (mob.Agil > 0)
                    temp += string.Format(", Agil {0}", mob.Agil);
                temp += "\nAttack :\n";
                if (mob.MinDC > 0)
                    temp += string.Format("DC {0}", mob.MinDC);
                if (mob.MinMC > 0)
                    temp += string.Format(", MC {0}", mob.MinMC);
                if (mob.MinSC > 0)
                    temp += string.Format(", SC {0}", mob.MinSC);
                if (mob.AttkSpeed > 0)
                    temp += string.Format(", Attacks Per Second {0}", mob.AttkSpeed / 1000);
                mobLabel.Text = string.Format(temp + ".");
                mobLabel.Visible = true;
                mobLabel.Parent = this;
                mobImage.Parent = this;
                mobImage.Visible = true;

                mobImage.Index = (int)mob.BaseImage;
            }
        }

        public void Show()
        {
            if (!Visible)
            {
                if (mob != null &&
                    !mob.Dead)
                    Visible = true;
            }
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }
    }
}
