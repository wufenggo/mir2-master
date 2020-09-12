using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Client.MirScenes.Dialogs
{
    public class TimerDialog : MirImageControl
    {
        public MirImageControl Layout;
        public MirLabel TextLabel1, TextLabel2;
        private long CurrentTime = 0;

        //private int layoutOffset = 220;

        public TimerDialog()
        {
            Index = 1361;
            Library = Libraries.Prguse;
            Movable = false;
            Sort = false;
            Location = new Point(Settings.ScreenWidth / 2 - Size.Width / 2, Settings.ScreenHeight / 10 - Size.Height / 2);
            Opacity = 0f;
            TextLabel1 = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 17F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(0, -22),
                Size = new Size(660, 40),
                ForeColour = Color.Yellow,
                OutLineColour = Color.Empty,
                BackColour = Color.Empty,
            };

            TextLabel2 = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 15F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(0, 0),
                Size = new Size(660, 40),
                ForeColour = Color.Red,
                OutLineColour = Color.Black,
            };

         /*   Layout = new MirImageControl
            {
                Index = 0,
                Library = Libraries.ScreenImage,
                Location = new Point(layoutOffset, 0),
                Parent = this,
                Visible = false
            };*/


            AfterDraw += TimeNotice_AfterDraw;
        }

        private void TimeNotice_AfterDraw(object sender, EventArgs e)
        {
            TextLabel2.Text = ((CurrentTime - CMain.Time) / 1000).ToString();

            if (CurrentTime < CMain.Time)
            {
                Hide();
            }
        }

        public void ShowNotice(string text, int type = 0, int time = 10000)
        {
            if (Layout != null)
                Layout.Visible = false;

            TextLabel1.Text = "[" + text + "]";
            TextLabel1.Visible = true;

            TextLabel2.Visible = type == 1;
            TextLabel2.Text = (time / 1000).ToString();

            NotControl = true;

            Show();
            CurrentTime = CMain.Time + time * 1000;
        }

      /*  public void ShowImage(int img, int time = 10000)
        {
            TextLabel1.Visible = false;
            TextLabel2.Visible = false;

            Point offset = Libraries.ScreenImage.GetOffSet(img);

            Layout.Location = new Point(layoutOffset + offset.X, 0 + offset.Y);
            Layout.Index = img;
            Layout.Visible = true;

            NotControl = true;

            Show();
            CurrentTime = CMain.Time + time * 1000;
        }*/

        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
    }
}
