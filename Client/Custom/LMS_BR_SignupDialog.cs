using Client.MirControls;
using Client.MirGraphics;
using System.Collections.Generic;
using Client.MirNetwork;
using C = ClientPackets;
using Client.MirSounds;
using System.Drawing;

namespace Client.Custom
{
    public class LMS_BR_SignupDialog : MirImageControl
    {
        public MirButton MatchesAvailable, SignUpButton, BRRankingButton, CloseButton;
        public MirLabel PlayersSignedUp;
        public List<LMS_BR_ClientInfo> BRs = new List<LMS_BR_ClientInfo>();
        public LMS_BR_SignupDialog()
        {
            Index = 990;
            Library = Libraries.Prguse;
            Location = new Point(Settings.ScreenWidth / 2 - Size.Width / 2, Settings.ScreenHeight / 2 - Size.Height / 2);

            MatchesAvailable = new MirButton
            {
                Parent = this,
                PressedIndex = 302,
                CenterText = true,
                Location = new Point(33, 38),
                Size = new Size(240, 18),
                Text = "",
                Border = true
            };
            MatchesAvailable.Click += MatchesAvailable_Click;

            CloseButton = new MirButton
            {
                Index = 361,
                HoverIndex = 362,
                PressedIndex = 363,
                Location = new Point(65, 6),
                Library = Libraries.CustomPrguse,
                Sound = SoundList.ButtonA,
                Parent = this,
            };
            CloseButton.Click += (o, e) => Hide();

            /*
            BRRankingButton = new MirButton
            {
                Location = new Point(58, 309),
                Size = new Size(75, 18),
                Text = "Rankings",
                Sound = SoundList.ButtonA,
                CenterText = true,
                Parent = this
            };*/
        }

        private void MatchesAvailable_Click(object sender, System.EventArgs e)
        {
            Network.Enqueue(new C.BR_Signup { });
        }

        public void UpdateInfo(ServerPackets.LMS_BRs p)
        {
            MatchesAvailable.Text = string.Format("{0} {1} Players signed up", p.BR.SignedUp ? "Signed up" : "Not Signed up", p.BR.PlayersSignedUp);
            MatchesAvailable.ForeColour = p.BR.SignedUp ? Color.Lime : Color.Red;
            MatchesAvailable.BorderColour = p.BR.SignedUp ? Color.Aqua : Color.Red;

        }

        public void Show()
        {
            Network.Enqueue(new C.Get_BR_Matches { });
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
