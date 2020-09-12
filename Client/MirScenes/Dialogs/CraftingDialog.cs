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
using ServerPackets;

namespace Client.MirScenes.Dialogs
{
    public class CraftingDialog : MirImageControl
    {
        public MirItemCell endResultingItem;
        public long craftProgress = 0;
        public MirLabel TitleLabel;
        public MirLabel LearntRecipe;
        /// <summary>
        /// Standard buttons
        /// </summary>
        public MirButton CraftButton;
        public MirButton CloseButton, CraftBuyButton;
        /// <summary>
        /// This will hold the Recipes as labels.
        /// </summary>
        public MirLabel[] Recipes;
        /// <summary>
        /// This will hold the Required items for the selected recipe and will display each item requirment on the client
        /// </summary>
        public MirImageControl[] RequiredItems;
        public MirImageControl[] RequiredItemFrames;
        public MirLabel[] RequiredItemsNames;
        public MirLabel[] RequiredItemAmount;
        /// <summary>
        /// This will hold the end result item to display
        /// </summary>
        //public MirImageControl EndResult;
        //public MirImageControl RewardFrame;
        public MirLabel EndResultName;
        public MirLabel Reward;
        public MirLabel Requirement;

        /// <summary>
        /// This will display the item that can be used in order to craft without learning the recipie.
        /// </summary>
        public MirLabel RecipieItemName;
        public MirImageControl RecipieItemFrame, RecipeItem;

        public MirLabel CraftTimeLabel;

        /// <summary>
        /// In order to know which recipe we're going to attempt to craft
        /// </summary>
        public byte SelectedRecipe = 0;

        public MirButton CollectButton;
        public MirLabel ProgressLabel;

        public MirLabel RequiredGold, RequiredLevel, RequiredClass;

        public MirButton PositionBar, UpButton, DownButton;


        public List<string> CurrentLines = new List<string>();
        private int _index = 0;
        public int MaximumLines = 13;

        /// <summary>
        /// The constructor
        /// </summary>
        public CraftingDialog()
        {
            LocationChanged += CraftingDialog_LocationChanged;
            Index = 230;
            Library = Libraries.CustomPrguse2;
            Movable = true;
            Sort = true;
            int tmp = GameScene.Scene.Recipe_ShopDialog == null ? 0 : GameScene.Scene.Recipe_ShopDialog.Size.Width;
            Location = new Point((Settings.ScreenWidth / 3 - Size.Width / 3 + tmp), (Settings.ScreenHeight / 2 - Size.Height / 2));
            Click += CraftingDialog_Click;
            MouseWheel += NPCDialog_MouseWheel;
            Sort = true;
            Recipes = new MirLabel[60];

            CloseButton = new MirButton
            {
                Parent = this,
                Index = 360,
                PressedIndex = 362,
                HoverIndex = 361,
                Library = Libraries.Prguse2,
                Location = new Point(414, 12),
                Hint = "Exit"
            };
            CloseButton.Click += (o, e) => Hide();

            CraftBuyButton = new MirButton
            {
                Parent = this,
                Index = 312,
                PressedIndex = 314,
                HoverIndex = 313,
                Library = Libraries.CustomTitle,
                Location = new Point(260, 308),
                Hint = string.Format("Open Recipe Shop.")
            };
            CraftBuyButton.Click += (o, e) =>
             {
                 Network.Enqueue(new C.GetRecipeShop { });
             };

            UpButton = new MirButton
            {
                Index = 197,
                HoverIndex = 198,
                PressedIndex = 199,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 14),
                Location = new Point(170, 44),
                Sound = SoundList.ButtonA,
                Visible = false
            };
            UpButton.Click += (o, e) =>
            {
                if (_index <= 0)
                    return;

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
                Location = new Point(170, 312),
                Sound = SoundList.ButtonA,
                Visible = false
            };
            DownButton.Click += (o, e) =>
            {
                if (_index + MaximumLines >= CurrentLines.Count)
                    return;

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
                Location = new Point(170, 57),
                Parent = this,
                Movable = true,
                Sound = SoundList.None,
                Visible = false
            };
            PositionBar.OnMoving += PositionBar_OnMoving;
            RequiredClass = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(192, 246),
                AutoSize = true,
                Font = new Font(Settings.FontName, 8F)
            };
            CraftButton = new MirButton
            {
                Parent = this,
                Location = new Point(358, 308),
                Index = 360,
                HoverIndex = 361,
                PressedIndex = 362,
                Library = Libraries.CustomTitle,
                Visible = false,
            };
            //  Send the Packet to Rquest crafting
            CraftButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.RequestCraft { Recipe = SelectedRecipe });
            };
            RequiredItems = new MirImageControl[5];
            RequiredItemAmount = new MirLabel[5];
            RequiredItemsNames = new MirLabel[5];
            RequiredItemFrames = new MirImageControl[5];
            EndResultName = new MirLabel
            {
                Parent = this,
                NotControl = true,
                AutoSize = true,
                Location = new Point(324, 178),
                Text = "",
                BackColour = Color.Transparent,
                Font = new Font(Settings.FontName, 12F)
            };
            
            RequiredGold = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(192, 218),
                AutoSize = true,
                Font = new Font(Settings.FontName, 8F)
            };

            RequiredLevel = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(316, 246),
                AutoSize = true,
                Font = new Font(Settings.FontName, 8F)
            };

            TitleLabel = new MirLabel
            {
                Location = new Point(70, 40),
                AutoSize = true,
                Parent = this,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                ForeColour = Color.Goldenrod
            };

            LearntRecipe = new MirLabel
            {
                Location = new Point(485, 40),
                Font = new Font(Settings.FontName, 12F),
                AutoSize = true,
                NotControl = true,
                ForeColour = Color.Goldenrod,
                Parent = this
            };

            Requirement = new MirLabel
            {
                Location = new Point(240, 94),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = ""
            };

            CraftTimeLabel = new MirLabel
            {
                Location = new Point(324, 190),
                AutoSize = true,
                Parent = this,
                ForeColour = Color.Goldenrod,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                BackColour = Color.Transparent,
                Text = ""
            };

            RecipieItemFrame = new MirImageControl
            {
                Location = new Point(281, 56),
                Library = Libraries.Prguse,
                Index = 989,
                NotControl = false,
                Parent = this,
            };

            RecipeItem = new MirImageControl
            {
                Parent = RecipieItemFrame,
                Location = new Point(5, 5),
                Library = Libraries.Items,
            };

            ProgressLabel = new MirLabel
            {
                Location = new Point(500, 300),
                AutoSize = true,
                Parent = this,
                BackColour = Color.Transparent,
                NotControl = true,
                Visible = false,
            };
            /*
            RewardFrame = new MirImageControl
            {
                Parent = this,
                NotControl = true,
                Location = new Point(281, 171),
                Library = Libraries.Prguse,
                Index = 989,
                Visible = false,
            };

            EndResult = new MirImageControl
            {
                Parent = RewardFrame,
                Library = Libraries.Items,
                UseOffSet = false,
                Location = new Point(0, 0)
            };
            */
            CollectButton = new MirButton
            {
                Parent = this,
                Location = new Point(390, 308),
                Visible = false,
                Library = Libraries.CustomTitle,
                Index = 400,
                HoverIndex = 401,
                PressedIndex = 402
            };
            CollectButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.CollectCraft { Rcipe = SelectedRecipe });
            };
            endResultingItem = new MirItemCell
            {
                BorderColour = Color.Lime,
                GridType = MirGridType.None,
                Library = Libraries.Items,
                Parent = this,
                Location = new Point(281, 171),

            };
        }

        private void CraftingDialog_LocationChanged(object sender, EventArgs e)
        {
            if (GameScene.Scene.Recipe_ShopDialog != null &&
                GameScene.Scene.Recipe_ShopDialog.Visible)
            {
                int X = Location.X - GameScene.Scene.Recipe_ShopDialog.Size.Width;
                if (X < 0)
                    GameScene.Scene.Recipe_ShopDialog.Location = new Point(Location.X + Size.Width, Location.Y);
                else
                    GameScene.Scene.Recipe_ShopDialog.Location = new Point(Location.X - GameScene.Scene.Recipe_ShopDialog.Size.Width,
                                                                           Location.Y);
            }
        }

        void NPCDialog_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (_index == 0 && count >= 0)
                return;
            if (_index == CurrentLines.Count - 1 && count <= 0)
                return;
            if (CurrentLines.Count <= MaximumLines)
                return;

            _index -= count;

            if (_index < 0)
                _index = 0;
            if (_index + MaximumLines > CurrentLines.Count - 1)
                _index = CurrentLines.Count - MaximumLines;

            NewText(CurrentLines, false);

            UpdatePositionBar();
        }

        void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int x = 170;
            int y = PositionBar.Location.Y;

            if (y >= 170)
                y = 170;
            if (y <= 57)
                y = 57;

            int location = y - 47;
            int interval = 108 / (CurrentLines.Count - MaximumLines);

            double yPoint = location / interval;

            _index = Convert.ToInt16(Math.Floor(yPoint));

            NewText(CurrentLines, false);

            PositionBar.Location = new Point(x, y);
        }

        private void UpdatePositionBar()
        {
            if (CurrentLines.Count <= MaximumLines)
                return;

            int interval = 108 / (CurrentLines.Count - MaximumLines);

            int x = 170;
            int y = 48 + (_index * interval);

            if (y >= 170)
                y = 170;
            if (y <= 57)
                y = 57;

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

            if (lines.Count > MaximumLines)
            {
                UpButton.Visible = true;
                DownButton.Visible = true;
                PositionBar.Visible = true;
            }
            else
            {
                UpButton.Visible = false;
                DownButton.Visible = false;
                PositionBar.Visible = false;
            }

            for (int i = 0; i < Recipes.Length; i++)
            {
                if (Recipes[i] != null)
                    Recipes[i].Text = "";
            }

            int lastLine = lines.Count > MaximumLines ? ((MaximumLines + _index) > lines.Count ? lines.Count : (MaximumLines + _index)) : lines.Count;

            for (int i = _index; i < lastLine; i++)
            {
                Recipes[i] = new MirLabel
                {
                    Font = new Font(Settings.FontName, 8F),
                    DrawFormat = TextFormatFlags.WordBreak,
                    Visible = true,
                    Parent = this,
                    Size = new Size(260, 14),
                    Location = new Point(21, 46 + (i - _index) * 20),
                    NotControl = false
                };
                Recipes[i].Click += CraftingDialog_Click1;
                ;
                if (i >= lines.Count)
                {
                    Recipes[i].Text = string.Empty;
                    continue;
                }

                string currentLine = lines[i];

                int oldLength = currentLine.Length;

                Recipes[i].Text = currentLine;
                CraftItem item = RecipeList.Where(p => p.RecipeName == Recipes[i].Text).FirstOrDefault();
                if (item != null)
                {
                    for (int u = 0; u < GameScene.User.Recipes.Count; u++)
                    {
                        if (GameScene.User.Recipes[u].Recipe == RecipeList[i].Recipie)
                        {
                            if (!GameScene.User.Recipes[u].CraftEnd &&
                                !GameScene.User.Recipes[u].Collected &&
                                GameScene.User.Recipes[u].InPrcoess &&
                                GameScene.User.Recipes[u].CraftEndTime > DateTime.Now.Ticks)
                            {
                                Recipes[i].Text = RecipeList[i].RecipeName + " (In Progress)";
                                Recipes[i].ForeColour = Color.Aqua;
                            }
                            if (GameScene.User.Recipes[u].CraftEnd &&
                                !GameScene.User.Recipes[u].Collected &&
                                GameScene.User.Recipes[u].InPrcoess &&
                                GameScene.User.Recipes[u].CraftEndTime < DateTime.Now.Ticks)
                            {
                                Recipes[i].Text = RecipeList[i].RecipeName + " (Completed)";
                                Recipes[i].ForeColour = Color.Lime;
                            }
                        }
                    }
                }
                if (!Recipes[i].Text.Contains("(In Progress)") && !Recipes[i].Text.Contains("(Completed)"))
                {
                    bool Available = CheckPlayerCanView(RecipeList[i].Recipie);
                    if (!Available)
                    {
                        Recipes[i].ForeColour = Color.Red;
                        Recipes[i].Text += " (Unavailable)";
                    }
                    else
                    {
                        Recipes[i].ForeColour = Color.White;
                        Recipes[i].Text += " (Available)";
                    }
                }
                Recipes[i].MouseWheel += NPCDialog_MouseWheel;
            }
        }

        private void CraftingDialog_Click1(object sender, EventArgs e)
        {
            MirLabel label = (MirLabel)sender;
            if (label == null)
                return;
            FindLabel((MirLabel)sender);
        }

        private void CraftingDialog_Click(object sender, EventArgs e)
        {
            FindLabel(null);
        }

        /// <summary>
        /// We'll need to dispose labels each time we've received a new Recipe list.
        /// </summary>
        public void DisposeLabels()
        {
            if (Recipes != null &&
                Recipes.Length > 0)
            {
                for (int i = 0; i < Recipes.Length; i++)
                {
                    if (Recipes[i] != null &&
                        !Recipes[i].IsDisposed)
                    {
                        Recipes[i].Dispose();
                        Recipes[i] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Dispose all of the labels & Images
        /// </summary>
        public void DisposeImages()
        {
            for (int i = 0; i < RequiredItems.Length; i++)
            {
                if (RequiredItems[i] != null &&
                    !RequiredItems[i].IsDisposed)
                {
                    RequiredItems[i].Dispose();
                    RequiredItems[i] = null;
                }
            }
            for (int i = 0; i < RequiredItemAmount.Length; i++)
            {
                if (RequiredItemAmount[i] != null &&
                    !RequiredItemAmount[i].IsDisposed)
                {
                    RequiredItemAmount[i].Dispose();
                    RequiredItemAmount[i] = null;
                }
            }
            for (int i = 0; i < RequiredItemsNames.Length; i++)
            {
                if (RequiredItemsNames[i] != null &&
                    !RequiredItemsNames[i].IsDisposed)
                {
                    RequiredItemsNames[i].Dispose();
                    RequiredItemsNames[i] = null;
                }
            }
            for (int i = 0; i < RequiredItemFrames.Length; i++)
            {
                if (RequiredItemFrames[i] != null &&
                    !RequiredItemFrames[i].IsDisposed)
                {
                    RequiredItemFrames[i].Dispose();
                    RequiredItemFrames[i] = null;
                }
            }
            /*
            if (EndResult != null)
            {
                EndResult.Dispose();
                EndResult = null;
            }*/
            if (endResultingItem != null)
            {
                endResultingItem.Dispose();
                endResultingItem = null;
            }
            if (Reward != null &&
                !Reward.IsDisposed)
            {
                Reward.Dispose();
                Reward = null;
            }
            if (Requirement != null &&
                !Requirement.IsDisposed)
            {
                Requirement.Dispose();
                Requirement = null;
            }
            if (CraftTimeLabel != null &&
                !CraftTimeLabel.IsDisposed)
            {
                CraftTimeLabel.Dispose();
                CraftTimeLabel = null;
            }
            if (RequiredGold != null &&
                !RequiredGold.IsDisposed)
            {
                RequiredGold.Dispose();
                RequiredGold = null;
            }
            if (RequiredLevel != null &&
                !RequiredLevel.IsDisposed)
            {
                RequiredLevel.Dispose();
                RequiredLevel = null;
            }
            if (RequiredClass != null &&
                !RequiredClass.IsDisposed)
            {
                RequiredClass.Dispose();
                RequiredClass = null;
            }
            if (LearntRecipe != null &&
                !LearntRecipe.IsDisposed)
            {
                LearntRecipe.Dispose();
                LearntRecipe = null;
            }
            
            if (EndResultName != null &&
                !EndResultName.IsDisposed)
            {
                EndResultName.Dispose();
                EndResultName = null;
            }
            
            if (TitleLabel != null &&
                !TitleLabel.IsDisposed)
            {
                TitleLabel.Dispose();
                TitleLabel = null;
            }
            /*
            if (RewardFrame != null &&
                !RewardFrame.IsDisposed)
            {
                RewardFrame.Dispose();
                RewardFrame = null;
            }
            */
            if (RecipieItemFrame != null &&
                !RecipieItemFrame.IsDisposed)
            {
                RecipieItemFrame.Dispose();
                RecipieItemFrame = null;
            }
            if (RecipeItem != null &&
                !RecipeItem.IsDisposed)
            {
                RecipeItem.Dispose();
                RecipeItem = null;
            }
            if (RecipieItemName != null &&
                !RecipieItemName.IsDisposed)
            {
                RecipieItemName.Dispose();
                RecipieItemName = null;
            }
            if (CollectButton != null &&
                !CollectButton.IsDisposed)
            {
                CollectButton.Dispose();
                CollectButton = null;
            }
            if (ProgressLabel != null &&
                !ProgressLabel.IsDisposed)
            {
                ProgressLabel.Dispose();
                ProgressLabel = null;
            }

        }

        public void RefreshRecipes()
        {
            DisposeImages();
            List<string> temp = new List<string>();
            for (int i = 0; i < RecipeList.Count; i++)
                temp.Add(RecipeList[i].RecipeName);
            NewText(temp);
        }

        /// <summary>
        /// This will be used in order to update the Item images on the client.
        /// </summary>
        public List<CraftItem> RecipeList = new List<CraftItem>();

        public bool CheckPlayerCanView(int recipe)
        {
            bool canView = false;
            if (GameScene.User.Recipes.Count > 0)
            {
                for (int i = 0; i < GameScene.User.Recipes.Count; i++)
                {
                    if (GameScene.User.Recipes[i].Recipe == recipe &&
                        GameScene.User.Recipes[i].Learnt)
                        canView = true;
                }
            }

            for (int x = 0; x < GameScene.User.Inventory.Length; x++)
            {
                if (GameScene.User.Inventory[x] != null)
                    if (GameScene.User.Inventory[x].Info.Type == ItemType.Scroll &&
                        GameScene.User.Inventory[x].Info.Shape == 13 &&
                        GameScene.User.Inventory[x].Info.Effect == (byte)recipe)
                        canView = true;
            }

            return canView;
        }

        public bool ShowButton = false;

        /// <summary>
        /// This will update the interface window with the recipies (to the labels) and populate the list.
        /// </summary>
        /// <param name="p">The packet received from server</param>
        public void UpdateList(List<CraftItem> p)
        {
            //  here
            DisposeImages();
            RecipeList.Clear();
            RecipeList = p;
            List<string> temp = new List<string>();
            for (int i = 0; i < RecipeList.Count; i++)
                temp.Add(RecipeList[i].RecipeName);
            NewText(temp);
        }

        /// <summary>
        /// When we click on the Recipe (Label) we'll need to display the required items as well as the end result, we'll also need to update the selected recipe that'll be used when sending the request to the server.
        /// </summary>
        /// <param name="label"></param>
        public void FindLabel(MirLabel label)
        {
            //  Dispose of the previous Required images & end result
            DisposeImages();
            CraftButton.Visible = false;
            bool CheckCanCraft = false;

            LearntRecipe = new MirLabel
            {
                Location = new Point(485, 40),
                Font = new Font(Settings.FontName, 12F),
                AutoSize = true,
                NotControl = true,
                ForeColour = Color.Goldenrod,
                Parent = this
            };

            TitleLabel = new MirLabel
            {
                Location = new Point(70, 40),
                AutoSize = true,
                Parent = this,
                Font = new Font(Settings.FontName, 12F),
                NotControl = true,
                ForeColour = Color.Goldenrod
            };

            RequiredGold = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(192, 218),
                AutoSize = true,
                ForeColour = Color.Lime,
                Font = new Font(Settings.FontName, 8F)
            };

            RequiredLevel = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(316, 246),
                AutoSize = true,
                ForeColour = Color.Lime,
                Font = new Font(Settings.FontName, 8F)
            };

            RequiredClass = new MirLabel
            {
                Parent = this,
                NotControl = true,
                Location = new Point(192, 246),
                AutoSize = true,
                ForeColour = Color.Lime,
                Font = new Font(Settings.FontName, 8F)
            };

            ProgressLabel = new MirLabel
            {
                Location = new Point(530, 220),
                AutoSize = true,
                Parent = this,
                BackColour = Color.Transparent,
                NotControl = true,
                Visible = false,
            };

            //  If there is no selected label, we'll clear the interface of the images & labels
            CollectButton = new MirButton
            {
                Parent = this,
                Location = new Point(390, 308),
                Visible = false,
                Library = Libraries.CustomTitle,
                Index = 400,
                HoverIndex = 401,
                PressedIndex = 402
            };
            CollectButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.CollectCraft { Rcipe = SelectedRecipe, });
                RefreshRecipes();
            };

            if (label == null)
            {
                LearntRecipe.Text = "";
                TitleLabel.Text = "";
                SelectedRecipe = 0;
                return;
            }
            if (label.Text.Contains("(Unavailable)"))
                return;

            //  Set the selected recipie by labels index
            if (Recipes.Length > 0)
            {
                for (int i = 0; i < Recipes.Length; i++)
                {
                    if (Recipes[i] == label)
                    {
                        SelectedRecipe = RecipeList[i].Recipie;

                        if (GameScene.User.Recipes.Count > 0)
                        {
                            for (int x = 0; i < GameScene.User.Recipes.Count; i++)
                            {
                                if (GameScene.User.Recipes[x].Recipe == RecipeList[i].Recipie &&
                                    GameScene.User.Recipes[x].Learnt)
                                    LearntRecipe.Text = "Learnt";
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < GameScene.User.Inventory.Length; i++)
            {
                if (GameScene.User.Inventory[i] != null)
                {
                    if (GameScene.User.Inventory[i].Info.Type == ItemType.Scroll &&
                        GameScene.User.Inventory[i].Info.Shape == 13 &&
                        GameScene.User.Inventory[i].Info.Effect >= 0)
                    {
                        if (GameScene.User.Inventory[i].Info.Effect == SelectedRecipe)
                        {
                            CraftButton.Visible = true;
                            if (!LearntRecipe.Text.Contains("Learnt"))
                            {
                                LearntRecipe.Visible = false;
                                RecipieItemFrame = new MirImageControl
                                {
                                    Location = new Point(281, 56),
                                    Library = Libraries.Prguse,
                                    Index = 989,
                                    NotControl = false,
                                    Parent = this,
                                    Visible = true
                                };
                                RecipeItem = new MirImageControl
                                {
                                    Parent = RecipieItemFrame,
                                    Location = new Point(5, 5),
                                    Library = Libraries.Items,
                                    Index = GameScene.User.Inventory[i].Info.Image,
                                    Hint = string.Format("You can use a {0} to craft this item.", GameScene.User.Inventory[i].Info.FriendlyName)
                                };
                            }
                        }
                    }
                }
            }
            if ((RecipieItemName != null &&
                RecipieItemName.Text.Length == 0) &&
                (LearntRecipe != null &&
                LearntRecipe.Text.Length == 0) &&
                !label.Text.Contains("(In Progress)") &&
                !label.Text.Contains("(Completed)") && !label.Text.Contains("Available"))
                return;

            //  Get the Current recipe selected
            CraftItem item = RecipeList.Where(e => e.Recipie == SelectedRecipe).FirstOrDefault();

            if (item != null)
            {
                Requirement = new MirLabel
                {
                    Location = new Point(240, 94),
                    AutoSize = true,
                    Parent = this,
                    ForeColour = Color.Goldenrod,
                    Font = new Font(Settings.FontName, 12F),
                    NotControl = true,
                    BackColour = Color.Transparent,
                    Text = "*Requirements*"
                };
                //  The base location for the required items
                int x = 191;
                int y = 120;
                int y0 = 138;
                bool[] any = new bool[item.Requirments.Count];
                //  Cycle through the Required Items
                for (int i = 0; i < item.Requirments.Count; i++)
                {
                    ItemInfo ingredient = GameScene.GetInfo(item.Requirments[i].ItemIndex);
                    RequiredItemFrames[i] = new MirImageControl
                    {
                        Parent = this,
                        Library = Libraries.Prguse,
                        Index = 989,
                        Location = new Point(x, y),
                        UseOffSet = true,
                    };
                    //  Create a new image for each item required
                    RequiredItems[i] = new MirImageControl
                    {
                        Parent = RequiredItemFrames[i],
                        Library = Libraries.Items,
                        Index = ingredient.Image,
                        Hint = string.Format("Crafting for a {0} requires (x{1}) of {2}.", item.ItemResult.FriendlyName, item.Requirments[i].Amount, ingredient.FriendlyName),
                        UseOffSet = true,
                        Location = new Point(4, 4)
                    };

                    Color clr = Color.Red;

                    for (int b = 0; b < GameScene.User.Inventory.Length; b++)
                    {
                        if (GameScene.User.Inventory[b] != null)
                        {
                            if (GameScene.User.Inventory[b].Info.Index == ingredient.Index &&
                                GameScene.User.Inventory[b].Count >= item.Requirments[i].Amount)
                            {
                                clr = Color.Lime;
                                any[i] = true;
                            }
                        }
                    }

                    //  Create a label ontop of the image to display the required amount
                    RequiredItemAmount[i] = new MirLabel
                    {
                        Parent = RequiredItems[i],
                        Text = item.Requirments[i].Amount.ToString(),
                        AutoSize = true,
                        NotControl = true,
                        Font = new Font(Settings.FontName, 8F),
                        BackColour = Color.Transparent,
                        ForeColour = clr
                    };
                    //  increase the base x axis for the location fo the next image
                    x += 45;
                    y0 += 12;
                }
                //  Create the End result image to display
                bool anySet = true;
                for (int i = 0; i < any.Length; i++)
                {
                    if (any[i] != true)
                        anySet = false;
                }
                CheckCanCraft = anySet;
                
                EndResultName = new MirLabel
                {
                    Parent = this,
                    NotControl = true,
                    AutoSize = true,
                    Location = new Point(324, 178),
                    Text = item.ItemResult.FriendlyName,
                    BackColour = Color.Transparent,
                    Font = new Font(Settings.FontName, 8F)
                };
                /*
                RewardFrame = new MirImageControl
                {
                    Parent = this,
                    NotControl = false,
                    Location = new Point(281, 171),
                    Library = Libraries.Prguse,
                    Index = 989,
                    Visible = true,
                };

                EndResult = new MirImageControl
                {
                    Index = item.ItemResult.Image,
                    Parent = RewardFrame,
                    Library = Libraries.Items,
                    UseOffSet = true,
                    Location = new Point(0, 0),
                    Hint = string.Format("You will gain {0} on\ncompletion of crafting.", item.ItemResult.FriendlyName),  // Can we show item info on hoover
                };
                */
                UserItem uItem = new UserItem(item.ItemResult);
                
                endResultingItem = new MirItemCell
                {
                    GridType = MirGridType.Crafting,
                    Library = Libraries.Items,
                    Parent = this,
                    Location = new Point(281, 171),
                    Locked = true,
                };
                endResultingItem.Item = uItem;
                //TitleLabel.Text = item.RecipeName;

                if (GameScene.Gold < item.GoldRequired)
                {
                    RequiredGold.ForeColour = Color.Red;
                    anySet = false;
                }
                else
                    RequiredLevel.ForeColour = Color.Lime;
                RequiredGold.Text = "Required Gold : " + item.GoldRequired.ToString("###,###,##0");

                if (GameScene.User.Level < item.LevelRequired)
                {
                    RequiredLevel.ForeColour = Color.Red;
                    anySet = false;
                }
                else
                    RequiredLevel.ForeColour = Color.Lime;
                RequiredLevel.Text = string.Format("Required Level : {0}", item.LevelRequired);

                if (item.RequiredClass != MirClass.NONE)
                {
                    if (GameScene.User.Class != item.RequiredClass)
                    {
                        RequiredClass.ForeColour = Color.Red;
                        anySet = false;
                    }
                    else
                        RequiredLevel.ForeColour = Color.Lime;
                }
                else
                    RequiredLevel.ForeColour = Color.Lime;

                RequiredClass.Text = string.Format("Required Class : {0}", item.RequiredClass.ToString());
                string temp = "Craft duration";
                if (item.RequiredTimeDay > 0)
                    temp += string.Format(" {0} days", item.RequiredTimeDay);
                if (item.RequiredTimeHour > 0)
                    temp += string.Format(" {0} hours", item.RequiredTimeHour);
                if (item.RequiredTimeMinute > 0)
                    temp += string.Format(" {0} minutes", item.RequiredTimeMinute);
                temp += ".";
                if (!temp.Contains("days") &&
                    !temp.Contains("day") &&
                    !temp.Contains("hours") &&
                    !temp.Contains("hour") &&
                    !temp.Contains("minutes") &&
                    !temp.Contains("minute"))
                {
                    temp = "Instant.";
                }
                CraftTimeLabel = new MirLabel
                {
                    Location = new Point(324, 190),
                    AutoSize = true,
                    Parent = this,
                    ForeColour = Color.Goldenrod,
                    Font = new Font(Settings.FontName, 8F),
                    NotControl = true,
                    BackColour = Color.Transparent,
                    Text = temp,
                };
                CraftButton.Visible = anySet;
                for (int i = 0; i < GameScene.User.Recipes.Count; i++)
                {

                    if (GameScene.User.Recipes[i].Recipe == SelectedRecipe)
                    {
                        if (DateTime.Now.Ticks > GameScene.User.Recipes[i].CraftEndTime &&
                            !GameScene.User.Recipes[i].Collected &&
                            GameScene.User.Recipes[i].InPrcoess &&
                            GameScene.User.Recipes[i].CraftEnd)
                        {
                            CollectButton.Visible = true;
                            ProgressLabel.Visible = false;
                        }
                        else if (DateTime.Now.Ticks < GameScene.User.Recipes[i].CraftEndTime &&
                            GameScene.User.Recipes[i].InPrcoess &&
                            !GameScene.User.Recipes[i].Collected &&
                            !GameScene.User.Recipes[i].CraftEnd)
                        {
                            craftProgress = GameScene.User.Recipes[i].CraftEndTime;

                            DateTime timeNow = DateTime.UtcNow;
                            DateTime crafttime = new DateTime(GameScene.User.Recipes[i].CraftEndTime);
                            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                            timeNow = TimeZoneInfo.ConvertTimeFromUtc(timeNow, zone);
                            crafttime = TimeZoneInfo.ConvertTimeFromUtc(crafttime, zone);
                            TimeSpan span = new TimeSpan();
                            span = crafttime - timeNow;
                            ProgressLabel.Text = string.Format("Your request is in progress.\nWill be ready in : \n{0}", GameScene.Scene.ToReadableString(span));
                            ProgressLabel.Visible = true;
                            CollectButton.Visible = false;
                        }
                    }
                }
            }
        }


        public void Hide()
        {
            SelectedRecipe = 0;
            DisposeImages();
            DisposeLabels();
            if (GameScene.Scene.Recipe_ShopDialog.Visible)
                GameScene.Scene.Recipe_ShopDialog.Hide();
            if (!Visible) return;
            Visible = false;
        }

        public void Show()
        {
            if (Visible) return;
            Visible = true;
            Network.Enqueue(new C.GetRecipes { });
            UpdateList(RecipeList);
            //  Get the width of the Recipe Window and push the Crafting dialog to the right
            int tmp = GameScene.Scene.Recipe_ShopDialog == null ? 0 : GameScene.Scene.Recipe_ShopDialog.Size.Width;
                                    //  Half Screen Width     Half the Crafting Dialog width
            Location = new Point((Settings.ScreenWidth / 2 -
                                    //  Half the Crafting Dialog width
                                    Size.Width / 2 + 
                                    //  Now push to the right by the width of the recipe window
                                    tmp), 
                                    //  Half screen height  
                                    (Settings.ScreenHeight / 2 - 
                                    //  Half the Crafting Dialog height
                                    Size.Height / 2));
        }
    }

    public class Recipe_Shop : MirImageControl
    {
        public MirButton buyButton, cancelButton, closeButton, UpButton, DownButton, PositionBar;
        public MirInputBox amountBox;
        public MirGoodsCell[] Cells;
        public List<UserItem> Goods = new List<UserItem>();
        public MirImageControl BuyLabel;
        public RecipieShopContents contents;
        UserItem selectedItem;
        byte PurchaseAmount = 0;

        public int StartIndex;

        public Recipe_Shop()
        {

            Index = 1000;
            Library = Libraries.CustomPrguse;

            Parent = GameScene.Scene.CraftingDialog;

            amountBox = new MirInputBox("Enter desired Amount")
            {
                Visible = false
            };
            amountBox.InputTextBox.Text = "1";
            amountBox.OKButton.Click += (o, e) =>
            {
                if (!byte.TryParse(amountBox.InputTextBox.Text, out PurchaseAmount))
                    return;
                amountBox.Dispose();
            };
            Cells = new MirGoodsCell[8];
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new MirGoodsCell
                {
                    Parent = this,
                    Location = new Point(10, 34 + i * 33),
                    Sound = SoundList.ButtonC,
                    isRecipeShop = true
                };
                Cells[i].Click += (o, e) =>
                {
                    selectedItem = ((MirGoodsCell)o).Item;
                    Update();
                };
                Cells[i].MouseWheel += RecipeShop_MouseWheel;
                Cells[i].DoubleClick += (o, e) => BuyItem();
            }
            closeButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(216, 3),
                Library = Libraries.CustomPrguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            closeButton.Click += (o, e) => { Hide(); };

            BuyLabel = new MirImageControl
            {
                Index = 27,
                Library = Libraries.CustomTitle,
                Parent = this,
                Location = new Point(20, 9),
            };

            UpButton = new MirButton
            {
                Index = 197,
                HoverIndex = 198,
                Library = Libraries.CustomPrguse2,
                Location = new Point(218, 35),
                Parent = this,
                PressedIndex = 199,
                Sound = SoundList.ButtonA
            };
            UpButton.Click += (o, e) =>
            {
                if (StartIndex == 0) return;
                StartIndex--;
                Update();
            };

            DownButton = new MirButton
            {
                Index = 207,
                HoverIndex = 208,
                Library = Libraries.CustomPrguse2,
                Location = new Point(218, 284),
                Parent = this,
                PressedIndex = 209,
                Sound = SoundList.ButtonA
            };
            DownButton.Click += (o, e) =>
            {
                if (Goods.Count <= 8) return;

                if (StartIndex == Goods.Count - 8) return;
                StartIndex++;
                Update();
            };

            PositionBar = new MirButton
            {
                Index = 205,
                HoverIndex = 206,
                Library = Libraries.CustomPrguse2,
                Location = new Point(218, 49),
                Parent = this,
                PressedIndex = 206,
                Movable = true,
                Sound = SoundList.None
            };
            PositionBar.OnMoving += RecipeBar_OnMoving;
            PositionBar.MouseUp += (o, e) => Update();

            buyButton = new MirButton
            {
                HoverIndex = 313,
                Index = 312,
                Location = new Point(77, 304),
                Library = Libraries.CustomTitle,
                Parent = this,
                PressedIndex = 314,
                Sound = SoundList.ButtonA,
            };
            buyButton.Click += (o, e) =>
            {
                BuyItem();
            };
        }

        public void BuyItem()
        {
            if (selectedItem == null)
            {
                MirMessageBox msg = new MirMessageBox("No Item selected");
                return;
            }
            uint Price = 0;
            for (int i = 0; i < contents.Contents.Count; i++)
                if (contents.Contents[i].ItemBeingSold.UniqueID == selectedItem.UniqueID)
                    Price = contents.Contents[i].ItemPrice;
            if (Price > GameScene.Gold)
            {
                MirMessageBox msg = new MirMessageBox(string.Format("Not enough Gold\r\nYou need {0} more.", Price - GameScene.Gold));
                return;
            }
            MirAmountBox amountBox = new MirAmountBox("Purchase Amount :", selectedItem.Image, 20, 1, 1);
            amountBox.OKButton.Click += (o, e) =>
            {
                if (amountBox.Amount > 0)
                {
                    uint totalPrice = Price;
                    for (int i = 0; i < amountBox.Amount; i++)
                        totalPrice += Price;
                    if (totalPrice > GameScene.Gold)
                    {
                        MirMessageBox msg = new MirMessageBox(string.Format("Not enough Gold\r\nYou need {0} more.", totalPrice - GameScene.Gold));
                        return;
                    }
                    else
                    {
                        byte FreeSpace = (byte)MapObject.User.GetFreeInventorySpace();
                        if (FreeSpace >= amountBox.Amount)                        
                            Network.Enqueue(new C.BuyRecipe { Amount = (byte)amountBox.Amount, RequestItem = selectedItem.UniqueID });                        
                        else
                        { 
                            MirMessageBox msg = new MirMessageBox(string.Format("Not enough Room\r\nYou need {0} more.", FreeSpace - amountBox.Amount));
                            return;
                        }
                    }
                }
            };
            amountBox.Show();
        }
        public void UpdateContent(S.RecipieShopContents _info)
        {
            Goods.Clear();
            
            if (_info == null)
                return;
            if (_info.Contents == null)
                return;
            if (_info.Contents.Count == 0)
                return;
            contents = _info;
            for (int i = 0; i < _info.Contents.Count; i++)
            {
                _info.Contents[i].ItemBeingSold.Info = GameScene.GetInfo(_info.Contents[i].ItemBeingSold.ItemIndex);
                Goods.Add(_info.Contents[i].ItemBeingSold);
            }
            Update();
        }

        public void Show()
        {
            if (!Visible)
                Visible = true;
            Location = new Point(GameScene.Scene.CraftingDialog.Location.X - Size.Width, GameScene.Scene.CraftingDialog.Location.Y);
        }

        public void Hide()
        {
            if (Visible)
                Visible = false;
        }

        private void RecipeShop_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (StartIndex == 0 && count >= 0) return;
            if (StartIndex == Goods.Count - 1 && count <= 0) return;

            StartIndex -= count;
            Update();
        }

        private void RecipeBar_OnMoving(object sender, MouseEventArgs e)
        {
            const int x = 218;
            int y = PositionBar.Location.Y;
            if (y >= 282 - PositionBar.Size.Height) y = 282 - PositionBar.Size.Height;
            if (y < 49) y = 49;

            int h = 233 - PositionBar.Size.Height;
            h = (int)Math.Round(((y - 49) / (h / (float)(Goods.Count - 8))));

            PositionBar.Location = new Point(x, y);

            if (h == StartIndex) return;
            StartIndex = h;
            Update();
        }

        private void Update()
        {
            if (StartIndex > Goods.Count - 8) StartIndex = Goods.Count - 8;
            if (StartIndex <= 0) StartIndex = 0;

            if (Goods.Count > 8)
            {
                PositionBar.Visible = true;
                int h = 233 - PositionBar.Size.Height;
                h = (int)((h / (float)(Goods.Count - 8)) * StartIndex);
                PositionBar.Location = new Point(218, 49 + h);
            }
            else
                PositionBar.Visible = false;


            for (int i = 0; i < 8; i++)
            {
                if (i + StartIndex >= Goods.Count)
                {
                    Cells[i].Visible = false;
                    continue;
                }
                Cells[i].Visible = true;
                //  Assign a dummy item for Client display
                Cells[i].Item = Goods[i + StartIndex];
                //  Give it a border if the item is valid
                Cells[i].Border = selectedItem != null && Cells[i].Item == selectedItem;
                uint Price = 0;
                //  Cycle through the contents to find the price of the item
                for (int x = 0; x < contents.Contents.Count; x++)
                {
                    //  Check by Unique ID
                    if (contents.Contents[x].ItemBeingSold.UniqueID == Goods[i + StartIndex].UniqueID)
                        //  Set the price for the content
                        Price = contents.Contents[x].ItemPrice;
                }
                
                // If the user can't afford the item
                byte costPercent = (byte)(Price / (float)GameScene.Gold * 100);   //  Get the percent
                int RemainingGold = 100 - costPercent;
                if (RemainingGold > 100)
                    RemainingGold = 100;
                if (RemainingGold >= 90)
                    Cells[i].PriceLabel.ForeColour = Color.Lime;    //  Easily affordable
                else if (RemainingGold >= 75)
                    Cells[i].PriceLabel.ForeColour = Color.Green;
                else if (RemainingGold >= 50)
                    Cells[i].PriceLabel.ForeColour = Color.White;
                else if (RemainingGold >= 25)
                    Cells[i].PriceLabel.ForeColour = Color.Orange;
                else if (RemainingGold >= 10)
                    Cells[i].PriceLabel.ForeColour = Color.OrangeRed;
                else
                    Cells[i].PriceLabel.ForeColour = Color.Red;
                
                //  Set the color to Lime    Could maybe expand it to show different colors showing how expensive it is
                //Cells[i].PriceLabel.ForeColour = Color.Lime;
                //  Apply the price to the label
                Cells[i].PriceLabel.Text = Price.ToString("###,###,##0") + " gold";
                Cells[i].usePearls = false;//pearl currency
                Cells[i].isRecipeShop = true;
            }
        }
    }
}

