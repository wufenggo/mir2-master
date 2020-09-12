using Client.MirControls;
using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Client.MirSounds;
using System.Windows.Forms;
using Client.MirNetwork;
using C = ClientPackets;
using S = ServerPackets;
using System.Text.RegularExpressions;

namespace Client.MirScenes.Dialogs
{
    //thx to Pete107/Petesn00beh for the base of this :p
    public class HeroRankingDialog : MirImageControl
    {
        public MirButton AllButton, WarButton, WizButton, TaoButton, SinButton, ArchButton, Tab7, NextButton, PrevButton, UpButton, DownButton, PositionBar;
        public MirButton CloseButton;
        public MirLabel MyRank;


        public byte RankType = 0;
        public int RowOffset = 0;
        public List<HeroRank> RankList = new List<HeroRank>();
        //public HeroRankRow[] Ranks;

        public MirLabel[] TextLabel;
        public MirLabel[] NameLebel;
        public MirLabel[] ClassLabel;
        public MirLabel[] LevelLabel;
        public MirLabel[] OwnerLabel;
        /*
        public MirLabel[] OwnerNames;
        */

        public HeroRankingDialog()
        {
            Index = 1330;
            Library = Libraries.CustomPrguse2;
            Movable = true;
            Sort = true;
            Location = new Point((800 - Size.Width) / 2, (600 - Size.Height) / 2);
            TextLabel = new MirLabel[20];
            NameLebel = new MirLabel[20];
            ClassLabel = new MirLabel[20];
            LevelLabel = new MirLabel[20];
            OwnerLabel = new MirLabel[20];

            CloseButton = new MirButton
            {
                Parent = this,
                Index = 361,
                PressedIndex = 363,
                HoverIndex = 362,
                Library = Libraries.CustomPrguse,
                Location = new Point(300, 3),
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            CloseButton.Click += (o, e) => Hide();

            AllButton = new MirButton
            {
                Index = 751,
                PressedIndex = 752,
                HoverIndex = 753,
                Library = Libraries.Title,
                Hint = "Overall TOP 20",
                Location = new Point(10, 38),
                Parent = this,
                Sound = SoundList.ButtonA,

            };
            AllButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 0 });
            };

            TaoButton = new MirButton
            {
                Index = 760,
                PressedIndex = 761,
                HoverIndex = 762,
                Library = Libraries.Title,
                Hint = "TOP 20 Taoists",
                Location = new Point(40, 38),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            TaoButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 1 });
            };

            WarButton = new MirButton
            {
                Index = 754,
                PressedIndex = 755,
                HoverIndex = 756,
                Library = Libraries.Title,
                Hint = "TOP 20 Warriors",
                Location = new Point(60, 38),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            WarButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 2 });
            };

            WizButton = new MirButton
            {
                Index = 763,
                PressedIndex = 764,
                HoverIndex = 765,
                Library = Libraries.Title,
                Hint = "TOP 20 Wizards",
                Location = new Point(80, 38),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            WizButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 3 });
            };

            SinButton = new MirButton
            {
                Index = 757,
                PressedIndex = 758,
                HoverIndex = 759,
                Library = Libraries.Title,
                Hint = "TOP 20 Assasins",
                Location = new Point(100, 38),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            SinButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 4 });
            };

            ArchButton = new MirButton
            {
                Index = 766,
                PressedIndex = 767,
                HoverIndex = 768,
                Library = Libraries.Title,
                Hint = "TOP 20 Archers",
                Location = new Point(120, 38),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            ArchButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GetHeroRank { RType = 5 });
            };

            NextButton = new MirButton
            {
                Index = 207,
                HoverIndex = 208,
                PressedIndex = 209,
                Library = Libraries.Prguse2,
                Location = new Point(299, 386),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                if (_index + MaximumLines >= CurrentLines.Count) return;

                _index++;

                NewText(CurrentLines, false);
                UpdatePositionBar();
            };

            PrevButton = new MirButton
            {
                Index = 197,
                HoverIndex = 198,
                PressedIndex = 199,
                Library = Libraries.Prguse2,
                Location = new Point(299, 100),
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            PrevButton.Click += (o, e) => 
            {
                if (_index <= 0) return;

                _index--;

                NewText(CurrentLines, false);
                UpdatePositionBar();
            };

            MyRank = new MirLabel
            {
                Text = "",
                Parent = this,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                ForeColour = Color.BurlyWood,
                Location = new Point(229, 36),
                Size = new Size(82, 22),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                //AutoSize = true
            };

            UpButton = new MirButton
            {
                Index = 197,
                HoverIndex = 198,
                PressedIndex = 199,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 14),
                Location = new Point(417, 34),
                Sound = SoundList.ButtonA,
                Visible = false
            };
            UpButton.Click += (o, e) =>
            {
                if (_index <= 0) return;

                _index--;

                NewText(CurrentLines, false);
                UpdatePositionBar();
            };

            DownButton = new MirButton
            {
                Index = 207,
                HoverIndex = 208,
                Library = Libraries.Prguse2,
                PressedIndex = 209,
                Parent = this,
                Size = new Size(16, 14),
                Location = new Point(417, 175),
                Sound = SoundList.ButtonA,
                Visible = false
            };
            DownButton.Click += (o, e) =>
            {
                if (_index + MaximumLines >= CurrentLines.Count) return;

                _index++;

                NewText(CurrentLines, false);
                UpdatePositionBar();
            };

            PositionBar = new MirButton
            {
                Index = 205,
                HoverIndex = 206,
                PressedIndex = 206,
                Library = Libraries.Prguse2,
                Location = new Point(417, 47),
                Parent = this,
                Movable = true,
                Sound = SoundList.None,
                Visible = false
            };
            PositionBar.OnMoving += PositionBar_OnMoving;

            /*
            Ranks = new HeroRankRow[100];
            
            OwnerNames = new MirLabel[10];
            
            for (int i = 0; i < Ranks.Length; i++)
            {
                Ranks[i] = new HeroRankRow()
                {
                    Parent = this,
                    //Location = new Point(32, 98 + i * 15),
                    Size = new Size(285, 15),
                };
            }
            */
            //MyRank.Click += (o, e) => GoToMyRank();

        }

        public List<string> CurrentLines = new List<string>();
        private int _index = 0;
        public int MaximumLines = 8;
        void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int x = 417;
            int y = PositionBar.Location.Y;

            if (y >= 155) y = 155;
            if (y <= 47) y = 47;

            int location = y - 47;
            int interval = 108 / (CurrentLines.Count - MaximumLines);

            double yPoint = location / interval;

            _index = Convert.ToInt16(Math.Floor(yPoint));

            NewText(CurrentLines, false);

            PositionBar.Location = new Point(x, y);
        }
        private void UpdatePositionBar()
        {
            if (CurrentLines.Count <= MaximumLines) return;

            int interval = 108 / (CurrentLines.Count - MaximumLines);

            int x = 417;
            int y = 48 + (_index * interval);

            if (y >= 155) y = 155;
            if (y <= 47) y = 47;

            PositionBar.Location = new Point(x, y);
        }
        public void NewText(List<string> lines, bool resetIndex = true)
        {
            if (resetIndex)
            {
                _index = 0;
                CurrentLines = lines;
                UpdatePositionBar();
            }

            if (resetIndex)
            {
                if (Index == -1)
                {
                    MaximumLines = 8;
                    if (lines.Count > MaximumLines)
                    {
                        Index = 385;
                        UpButton.Visible = true;
                        DownButton.Visible = true;
                        PositionBar.Visible = true;
                    }
                    else
                    {
                        Index = 384;
                        UpButton.Visible = false;
                        DownButton.Visible = false;
                        PositionBar.Visible = false;
                    }
                }
                else
                {
                    MaximumLines = 30;
                    UpButton.Visible = false;
                    DownButton.Visible = false;
                    PositionBar.Visible = false;
                }
            }

            for (int i = 0; i < TextLabel.Length; i++)
            {
                if (TextLabel[i] != null) TextLabel[i].Text = "";
                if (NameLebel[i] != null) NameLebel[i].Text = "";
                if (ClassLabel[i] != null) ClassLabel[i].Text = "";
                if (LevelLabel[i] != null) LevelLabel[i].Text = "";
                //if (OwnerLabel[i] != null) OwnerLabel[i].Text = "";
            }

            int lastLine = lines.Count > MaximumLines ? ((MaximumLines + _index) > lines.Count ? lines.Count : (MaximumLines + _index)) : lines.Count;

            for (int i = _index; i < lastLine; i++)
            {
                
                TextLabel[i] = new MirLabel
                {
                    //Font = font,
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    NotControl = false,
                    Location = new Point(32, 98 + i * 15),
                    Size = new Size(24, 14),
                    ForeColour = i == 0 ? Color.Gold : i == 1 ? Color.Silver : i == 2 ? Color.SaddleBrown : Color.White
                    //AutoSize = true
                };
                NameLebel[i] = new MirLabel
                {
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    NotControl = false,
                    Location = new Point(102, 98 + i * 15),
                    Size = new Size(48, 14),
                    ForeColour = i == 0 ? Color.Gold : i == 1 ? Color.Silver : i == 2 ? Color.SaddleBrown : Color.White
                    //AutoSize = true
                };
                ClassLabel[i] = new MirLabel
                {
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    NotControl = false,
                    Location = new Point(182, 98 + i * 15),
                    Size = new Size(48, 14),
                    ForeColour = i == 0 ? Color.Gold : i == 1 ? Color.Silver : i == 2 ? Color.SaddleBrown : Color.White
                };
                LevelLabel[i] = new MirLabel
                {
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    NotControl = false,
                    Location = new Point(262, 98 + i * 15),
                    Size = new Size(32, 14),
                    ForeColour = i == 0 ? Color.Gold : i == 1 ? Color.Silver : i == 2 ? Color.SaddleBrown : Color.White
                };
                /*
                OwnerLabel[i] = new MirLabel
                {
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    NotControl = false,
                    Location = new Point(450, 98 + i * 15),
                    Size = new Size(48, 14)
                };
                */
                if (i >= lines.Count)
                {
                    TextLabel[i].Text = string.Empty;
                    NameLebel[i].Text = string.Empty;
                    ClassLabel[i].Text = string.Empty;
                    LevelLabel[i].Text = string.Empty;
                    //OwnerLabel[i].Text = string.Empty;
                    continue;
                }

                string currentLine = lines[i];

                string[] temps = currentLine.Split(',');


                //  Rank
                TextLabel[i].Text = temps[0];
                //  Name
                NameLebel[i].Text = temps[1];
                //  Class
                ClassLabel[i].Text = temps[2];
                //  Level
                LevelLabel[i].Text = temps[3];
                //  Owner
                //OwnerLabel[i].Text = temps[4];
                TextLabel[i].MouseWheel += RankDialog_MouseWheel;

            }
        }


        private void RankDialog_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (_index == 0 && count >= 0) return;
            if (_index == CurrentLines.Count - 1 && count <= 0) return;
            if (CurrentLines.Count <= MaximumLines) return;

            _index -= count;

            if (_index < 0) _index = 0;
            if (_index + MaximumLines > CurrentLines.Count - 1) _index = CurrentLines.Count - MaximumLines;

            NewText(CurrentLines, false);

            UpdatePositionBar();
        }


        public void SetRanks(S.HeroRanking p)
        {
            RankList = p.list;
            //UpdateInterface();
        }
        
        public void UpdateInterface()
        {
            List<string> tmp = new List<string>();
            int longestName = 0, longestOwnerName = 0; ;
            int classLength = MirClass.Assassin.ToString().Length;
            int levelLength = 0;
            for (int i = 0; i < RankList.Count; i++)
            {
                if (GameScene.Hero != null)
                    if (GameScene.User.Name == RankList[i].OwnerName &&
                        RankList[i].Name.Contains(GameScene.Hero.Name))
                        MyRank.Text = string.Format("Ranked: {0}", i + 1);
                if (longestName < RankList[i].Name.Length)
                    longestName = RankList[i].Name.Length;
                if (longestOwnerName < RankList[i].OwnerName.Length)
                    longestOwnerName = RankList[i].OwnerName.Length;
                if (levelLength < RankList[i].Level.ToString().Length)
                    levelLength = RankList[i].Level.ToString().Length;
            }
            
            for (int i = 0; i < RankList.Count; i++)
            {
                string tmpStr = string.Format("{0},{1},{2},{3},{4}", (i + 1), RankList[i].Name, RankList[i].HeroesClass.ToString(), RankList[i].Level, RankList[i].OwnerName);
                tmp.Add(tmpStr);
            }
            NewText(tmp);
        }

        public void Show()
        {
            if (Visible) return;
            Visible = true;
            Network.Enqueue(new C.GetHeroRank { RType = 0 });
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
    }

    public class HeroRankRow : MirControl
    {
        public MirLabel NameLabel;
        public MirLabel RankLabel;
        public MirLabel ClassLabel;
        public MirLabel LevelLabel;
        public HeroRank HRank;
        public long Index;
        public HeroRankRow()
        {
            Sound = SoundList.ButtonA;
            BorderColour = Color.Lime;
            Visible = false;
            //Click += (o, e) => Inspect();
            RankLabel = new MirLabel
            {
                Location = new Point(0, 0),
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                NotControl = true,
            };
            NameLabel = new MirLabel
            {
                Location = new Point(45, 0),
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                NotControl = true,
            };
            ClassLabel = new MirLabel
            {
                Location = new Point(150, 0),
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                NotControl = true,
            };

            LevelLabel = new MirLabel
            {
                Location = new Point(220, 0),
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                NotControl = true,
            };
        }
        public void Update(HeroRank listing, int RankIndex)
        {
            HRank = listing;
            RankLabel.Text = RankIndex.ToString();
            LevelLabel.Text = HRank.Level.ToString();
            ClassLabel.Text = HRank.HeroesClass.ToString();
            NameLabel.Text = listing.Name;
            Index = listing.Owner;
            if (RankLabel.Text == "1")
            {
                RankLabel.ForeColour = Color.Gold;
                NameLabel.ForeColour = Color.Gold;
                LevelLabel.ForeColour = Color.Gold;
                ClassLabel.ForeColour = Color.Gold;
            }
            if (RankLabel.Text == "2")
            {
                RankLabel.ForeColour = Color.Silver;
                NameLabel.ForeColour = Color.Silver;
                LevelLabel.ForeColour = Color.Silver;
                ClassLabel.ForeColour = Color.Silver;
            }
            if (RankLabel.Text == "3")
            {
                RankLabel.ForeColour = Color.RosyBrown;
                NameLabel.ForeColour = Color.RosyBrown;
                LevelLabel.ForeColour = Color.RosyBrown;
                ClassLabel.ForeColour = Color.RosyBrown;
            }
            else if (NameLabel.Text == GameScene.User.Name)
            {
                RankLabel.ForeColour = Color.Green;
                NameLabel.ForeColour = Color.Green;
                LevelLabel.ForeColour = Color.Green;
                ClassLabel.ForeColour = Color.Green;
            }
            else if (Convert.ToInt32(RankLabel.Text) > 3)
            {
                RankLabel.ForeColour = Color.White;
                NameLabel.ForeColour = Color.White;
                LevelLabel.ForeColour = Color.White;
                ClassLabel.ForeColour = Color.White;
            }

            Visible = true;

        }
        public void Clear()
        {
            Visible = false;
            NameLabel.Text = string.Empty;
            RankLabel.Text = string.Empty;
            LevelLabel.Text = string.Empty;
            ClassLabel.Text = string.Empty;
        }

        public void Inspect()
        {
            if (CMain.Time <= GameScene.InspectTime/* && Index == InspectDialog.InspectID*/) return;

            GameScene.InspectTime = CMain.Time + 500;
            InspectDialog.InspectID = (uint)Index;
            Network.Enqueue(new C.Inspect { ObjectID = (uint)Index, Ranking = true, Hero = true, HeroName = HRank.Name });
        }
    }
}