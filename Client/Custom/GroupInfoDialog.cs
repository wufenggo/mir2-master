using Client.MirControls;
using Client.MirGraphics;
using Client.MirObjects;
using Client.MirScenes;
using Client.MirScenes.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Custom
{
    public class GroupInfoDialog : MirImageControl
    {
        public static MirImageControl[] Back_ground, Avatar, Gender, ImageClass, Poisons, GroupBackground;

        public static MirButton[] ImageLeader;

        public static MirLabel[] Name;

        public static MirControl[] Health;

        public static List<MirImageControl> BuffPoisonList = new List<MirImageControl>();

        public int _showList = 3;

        public bool firstLine;

        public bool secondLine;

        public PlayerObject ob;

        public GroupInfoDialog()
        {
            Size = new Size(160, 600);
            Location = new Point(GameScene.Scene.Size.Width - Size.Width,
                                 GameScene.Scene.MiniMapDialog.Location.Y + GameScene.Scene.MiniMapDialog.Size.Height);
            
            Avatar = new MirImageControl[15];
            Back_ground = new MirImageControl[15];
            Gender = new MirImageControl[15];
            ImageClass = new MirImageControl[15];
            Poisons = new MirImageControl[15];
            Name = new MirLabel[15];
            Health = new MirControl[15];
            ImageLeader = new MirButton[15];

            for (int i = 0; i < 15; i++)
            {
                
                Back_ground[i] = new MirImageControl
                {
                    Library = Libraries.EdensEliteInter,
                    Parent = this,
                    Visible = false,
                    Location = new Point(0, 0),
                    Index = 12,
                    NotControl = true
                };
                Avatar[i] = new MirImageControl
                {
                    Library = Libraries.EdensEliteInter,
                    Parent = Back_ground[i],
                    Visible = false,
                    Index = 1,
                    NotControl = true
                };
                Gender[i] = new MirImageControl
                {
                    Library = Libraries.EdensEliteInter,
                    Parent = Back_ground[i],
                    Visible = false,
                    Index = 1,
                    NotControl = true
                };
                Health[i] = new MirControl
                {
                    BackColour = Color.FromArgb(160, 0, 0),
                    DrawControlTexture = true,
                    Visible = false,
                    Parent = Back_ground[i],
                    NotControl = true
                };
                ImageClass[i] = new MirImageControl
                {
                    Library = Libraries.EdensEliteInter,
                    Parent = Back_ground[i],
                    Visible = false,
                    Index = 1,
                    NotControl = true
                };
                ImageLeader[i] = new MirButton
                {
                    HoverIndex = 141,
                    PressedIndex = 141,
                    Library = Libraries.EdensEliteInter,
                    Parent = Back_ground[i],
                    Visible = false,
                    Index = 147,
                    NotControl = true
                };
                Name[i] = new MirLabel
                {
                    DrawFormat = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter),
                    AutoSize = true,
                    Parent = Back_ground[i],
                    Visible = false,
                    NotControl = true
                };
            }

        }

        public void UpdateListGroup()
        {
            //  Has to be more than 1 to party
            if (GroupDialog.GroupList.Count > 1)
            {
                for (int i = 0; i < 15; i++)
                {
                    Avatar[i].Visible = false;
                    Name[i].Visible = false;
                    Health[i].Visible = false;
                    Back_ground[i].Visible = false;
                    ImageLeader[i].Visible = false;
                    ImageClass[i].Visible = false;
                }

                #region First instance in the Info Dialog (Main User) regardless of Leadership
                int num = 1;
                Back_ground[0].Visible = true;
                //
                //Back_Ground is the important one you one I think, by default it draw the first instance at 0,0 (top left)
                //
                Back_ground[0].Parent = this;   //  So this is now a child to the base
                Back_ground[0].Location = new Point(0, 0);
                int index = 0;
                switch (GameScene.User.Class)
                {
                    case MirClass.Warrior:
                        index = ((GameScene.User.Gender == MirGender.Male) ? 190 : 189);
                        ImageClass[0].Index = 136;
                        break;
                    case MirClass.Wizard:
                        index = ((GameScene.User.Gender == MirGender.Male) ? 192 : 191);
                        ImageClass[0].Index = 137;
                        break;
                    case MirClass.Taoist:
                        index = ((GameScene.User.Gender == MirGender.Male) ? 188 : 187);
                        ImageClass[0].Index = 138;
                        break;
                    case MirClass.Assassin:
                        index = ((GameScene.User.Gender == MirGender.Male) ? 186 : 185);
                        ImageClass[0].Index = 139;
                        break;
                    case MirClass.Archer:
                        index = ((GameScene.User.Gender == MirGender.Male) ? 184 : 183);
                        ImageClass[0].Index = 140;
                        break;
                }
                //  So if you want to move the whole thing you do it in the constructor
                //  if you want to move individual controls you do it based on what you wish to move
                //  if it's the background (which holds everything and will move along with it holding their position)
                //  you change the point on the Location
                Name[0].Text = GameScene.User.Name;
                Name[0].Parent = Back_ground[0];    //  now this is a child to the background
                Name[0].Location = new Point(42, 8);  //  This is the location on the Parent (I.E Back_Ground)
                Name[0].Visible = true;
                Name[0].ForeColour = GameScene.User.NameColour;
                Name[0].OutLineColour = Color.Black;
                Name[0].OutLine = true;
                

                ImageLeader[0].Parent = Back_ground[0]; //  so is this
                ImageLeader[0].Location = new Point(133, 8);    //  Setting the x y to the background
                ImageLeader[0].BringToFront();

                Health[0].Size = new Size((int)((float)100 * GameScene.User.PercentHealth / 100f) + 1, 11);
                Health[0].Parent = Back_ground[0];
                Health[0].Location = new Point(46, 24);
                Health[0].Visible = true;

                Avatar[0].Index = index;
                Avatar[0].Visible = true;
                Avatar[0].Parent = Back_ground[0];  //  And this and so on..
                Avatar[0].Location = new Point(4, 4);

                ImageClass[0].Visible = true;
                ImageClass[0].Parent = Back_ground[0];
                ImageClass[0].Location = new Point(2, 26);

                //  User is first player on group info
                if (GameScene.User.Name == GroupDialog.GroupList[0])
                    ImageLeader[0].Visible = true;
                else
                    ImageLeader[0].Visible = false;//noob
                #endregion

                List<string> names = new List<string>();
                for (int i = 0; i < GroupDialog.GroupList.Count; i++)
                {
                    if (GameScene.User.Name != GroupDialog.GroupList[i])
                        names.Add(GroupDialog.GroupList[i]);
                }
                int idx = 1;
                for (int k = 0; k < names.Count; k++)
                {
                    if (GameScene.User.Name == names[k])
                        continue;
                    //  Find Player Objects on the current Map
                    for (int l = 0; l < MapControl.Objects.Count; l++)
                    {
                        //  Check if it is a Player
                        if (MapControl.Objects[l] is PlayerObject)
                        {
                            //  Cast it into a Player Object to be used
                            ob = (PlayerObject)MapControl.Objects[l];
                            //  Check it's not the same as the Main User
                            if (GameScene.User.Name == ob.Name)
                                continue;
                            //  Check within range
                            if (Functions.InRange(GameScene.User.CurrentLocation, ob.CurrentLocation, 18))
                            {
                                if (ob.Name == names[k])
                                {
                                    Back_ground[idx].Visible = true;
                                    Back_ground[idx].Parent = this;   //  The background for each group member will be drawin on the Dialog

                                    //  Now every other Back_Ground will be pushed down based on the amount of Back_Grounds already created
                                    Back_ground[idx].Location = new Point(0, 44 * num++);                                   
                                    index = 0;
                                    switch (ob.Class)
                                    {
                                        case MirClass.Warrior:
                                            index = ((ob.Gender == MirGender.Male) ? 190 : 189);
                                            ImageClass[idx].Index = 136;
                                            break;
                                        case MirClass.Wizard:
                                            index = ((ob.Gender == MirGender.Male) ? 192 : 191);
                                            ImageClass[idx].Index = 137;
                                            break;
                                        case MirClass.Taoist:
                                            index = ((ob.Gender == MirGender.Male) ? 188 : 187);
                                            ImageClass[idx].Index = 138;
                                            break;
                                        case MirClass.Assassin:
                                            index = ((ob.Gender == MirGender.Male) ? 186 : 185);
                                            ImageClass[idx].Index = 139;
                                            break;
                                        case MirClass.Archer:
                                            index = ((ob.Gender == MirGender.Male) ? 184 : 183);
                                            ImageClass[idx].Index = 140;
                                            break;
                                    }
                                    Name[idx].Text = string.Format("{0}\t-Level - {1}", ob.Name, ob.Level);
                                    Name[idx].Parent = Back_ground[idx];    //  Each control will be on the background rather than fiddling about with locations
                                    Name[idx].Location = new Point(42, 8);  //  We can stick to just 1 x y as seen here
                                    Name[idx].Visible = true;
                                    Name[idx].ForeColour = GameScene.User.NameColour;
                                    Name[idx].OutLineColour = Color.Black;
                                    Name[idx].OutLine = true;
                                    if (!ImageLeader[0].Visible)
                                        ImageLeader[idx].Visible = true;
                                    else
                                        ImageLeader[idx].Visible = false;

                                    ImageLeader[idx].Parent = Back_ground[idx]; //  Do it for each control
                                    ImageLeader[idx].Location = new Point(133, 8);    //  Setting the x y to the background
                                    ImageLeader[idx].BringToFront();

                                    Health[idx].Size = new Size((int)((float)100 * ob.PercentHealth / 100f) + 1, 11);
                                    Health[idx].Parent = Back_ground[idx];
                                    Health[idx].Location = new Point(46, 24);
                                    Health[idx].Visible = true;

                                    Avatar[idx].Index = index;
                                    Avatar[idx].Visible = true;
                                    Avatar[idx].Parent = Back_ground[idx];
                                    Avatar[idx].Location = new Point(4, 4);

                                    ImageClass[idx].Visible = true;
                                    ImageClass[idx].Parent = Back_ground[idx];
                                    ImageClass[idx].Location = new Point(2, 26);
                                    idx++;
                                }
                            }
                            //  Here we'll grey out the group members out of range, we won't give it health but we'll
                            //  Give it the rest of the information
                            else
                            {

                            }
                        }
                    }
                }
                if (GameScene.Scene.GroupDialog.LockInfo)
                    Show();
                else
                    Hide();
            }
            else
                Hide();
        }

        public void Hide()
        {
            if (Visible)
            {
                Visible = false;
            }
        }

        public void Show()
        {
            
            if (!Visible)
            {
                Visible = true;
            }
        }
    }
}
