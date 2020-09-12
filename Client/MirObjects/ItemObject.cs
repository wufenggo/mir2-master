using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Client.MirGraphics;
using Client.MirScenes;
using S = ServerPackets;
using System.Text.RegularExpressions;

namespace Client.MirObjects
{
    class ItemObject : MapObject
    {
        public override ObjectType Race{
            get { return ObjectType.Item; }
        }
        public override bool Blocking
        {
            get { return false; }
        }

        public Size Size;

        public sbyte floorGrade = 0;
        public ItemObject(uint objectID) : base(objectID)
        {
        }


        public void Load(S.ObjectItem info)
        {
            Name = info.Name;
            NameColour = info.NameColour;
            floorGrade = info.floorColor;
            BodyLibrary = Libraries.FloorItems;

            CurrentLocation = info.Location;
            MapLocation = info.Location;
            GameScene.Scene.MapControl.AddObject(this);
            DrawFrame = info.Image;

            Size = BodyLibrary.GetTrueSize(DrawFrame);

            DrawY = CurrentLocation.Y;

        }
        public void Load(S.ObjectGold info)
        {
            Name = string.Format("Gold ({0:###,###,###})", info.Gold);


            BodyLibrary = Libraries.FloorItems;

            CurrentLocation = info.Location;
            MapLocation = info.Location;
            GameScene.Scene.MapControl.AddObject(this);

            if (info.Gold < 100)
                DrawFrame = 112;
            else if (info.Gold < 200)
                DrawFrame = 113;
            else if (info.Gold < 500)
                DrawFrame = 114;
            else if (info.Gold < 1000)
                DrawFrame = 115;
            else
                DrawFrame = 116;

            Size = BodyLibrary.GetTrueSize(DrawFrame);

            DrawY = CurrentLocation.Y;
        }
        public override void Draw()
        {
            
            
            //  Draw in front
            if (BodyLibrary != null)
                BodyLibrary.Draw(DrawFrame, DrawLocation, DrawColour);
            //  Draw behind
            
            if (Libraries.FloorItemEffect != null && Settings.DropEffect)//give them option :P
                Draw_EE_Effect(DrawLocation);
        }

        public override void Process()
        {
            DrawLocation = new Point((CurrentLocation.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth, (CurrentLocation.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
            DrawLocation.Offset((MapControl.CellWidth - Size.Width) / 2, (MapControl.CellHeight - Size.Height) / 2);
            DrawLocation.Offset(User.OffSetMove);
            DrawLocation.Offset(GlobalDisplayLocationOffset);
            FinalDrawLocation = DrawLocation;

            DisplayRectangle = new Rectangle(DrawLocation, Size);

        }

        long changingIdxTime;
        public override bool MouseOver(Point p)
        {
            return MapControl.MapLocation == CurrentLocation;
            // return DisplayRectangle.Contains(p);
        }


        public override void DrawName()
        {
            CreateLabel(Color.Transparent, false, true);

            if (NameLabel == null) return;
            NameLabel.Location = new Point(
                DisplayRectangle.X + (DisplayRectangle.Width - NameLabel.Size.Width) / 2,
                DisplayRectangle.Y + (DisplayRectangle.Height - NameLabel.Size.Height) / 2 - 20);
            NameLabel.Draw();
        }
        int lastIndex = 0;
        public void Draw_EE_Effect(Point drawLocation)
        {
            if (CMain.Time > changingIdxTime)
            {
                //                          1000 = 1 second : 1000 - 9 seconds(frames) = 100
                changingIdxTime = CMain.Time + 100;//Reduce to make it flash faster, increase to slow it down
                lastIndex++;
                switch (floorGrade)
                {
                    //  Don't draw
                    default:
                    case 0://   none
                        if (lastIndex > 109 ||
                            lastIndex < 100)
                            lastIndex = 100;
                        break;
                    case 1: //Comm Yellow
                        if (lastIndex > 19 ||
                            lastIndex < 10)
                            lastIndex = 10;
                        break;
                    case 2: //Rare DeepSkyBlue
                        if (lastIndex > 59 ||
                            lastIndex < 50)
                            lastIndex = 50;
                        break;
                    case 3: //Len DarkOrange
                        if (lastIndex > 29 ||
                            lastIndex < 20)
                            lastIndex = 20;
                        break;
                    case 4: //Mth Plum
                        if (lastIndex > 49 ||
                            lastIndex < 40)
                            lastIndex = 40;
                        break;
                    case 5: //Quest LightGreen
                        if (lastIndex > 69 ||
                            lastIndex < 60)
                            lastIndex = 60;
                        break;
                    case 6: //Isadded Red
                        if (lastIndex > 39 ||
                            lastIndex < 30)
                            lastIndex = 30;
                        break;

                }                
            }
            Point tmp = DrawLocation;
            
            Libraries.FloorItemEffect.DrawBlend(lastIndex, tmp, DrawColour, true, 0.85f);
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
            
        }

        public override void DrawEffects(bool effectsEnabled)
        {
            
        }

        public void DrawName(int y)
        {
            CreateLabel(Color.FromArgb(100, 0, 24, 48), true, false);

            NameLabel.Location = new Point(
                DisplayRectangle.X + (DisplayRectangle.Width - NameLabel.Size.Width) / 2,
                DisplayRectangle.Y + y + (DisplayRectangle.Height - NameLabel.Size.Height) / 2 - 20);
            NameLabel.Draw();
        }

        private void CreateLabel(Color backColour, bool border, bool outline)
        {
            NameLabel = null;

            for (int i = 0; i < LabelList.Count; i++)
            {
                if (LabelList[i].Text != Name || LabelList[i].Border != border || LabelList[i].BackColour != backColour || LabelList[i].ForeColour != NameColour || LabelList[i].OutLine != outline) continue;
                NameLabel = LabelList[i];
                break;
            }
            if (NameLabel != null && !NameLabel.IsDisposed) return;

            NameLabel = new MirControls.MirLabel
            {
                AutoSize = true,
                BorderColour = Color.Black,
                BackColour = backColour,
                ForeColour = NameColour,
                OutLine = outline,
                Border = border,
                Text = Regex.Replace(Name, @"\d+$", string.Empty),
            };

            LabelList.Add(NameLabel);
        }


    }
}
