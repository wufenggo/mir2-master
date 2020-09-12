
using Client.MirControls;
using Client.MirGraphics;
using Client.MirScenes;
using System.Drawing;
using Client.MirNetwork;
using S = ServerPackets;
using C = ClientPackets;
using Client.MirSounds;

namespace Client.Custom
{
    public class EE_Mob_Active_Effects
    {
        //  0   =   Poison
        //  1   =   Buff
        public sbyte EffectType;
        public PoisonType PType;
        public BuffType BType;
    }

    public sealed class SocketingDialog : MirImageControl
    {
        public MirItemCell[] ItemCells = new MirItemCell[4];

        public MirLabel CanRemove;

        public MirImageControl[] RuneSlots = new MirImageControl[4];

        public static UserItem[] Items = new UserItem[4];
        public static Sockets[] Runes = new Sockets[3];
        public static int[] ItemsIdx = new int[4];
        public MirImageControl SocketsContainer;
        public MirButton CloseBtn, AplyBtn;
        public SocketingDialog()
        {
            Index = 143;
            Library = Libraries.EdensEliteInter;
            Location = new Point(Settings.ScreenWidth / 2 - this.Size.Width / 2, Settings.ScreenHeight / 2 - this.Size.Height / 2);
            Sort = true;
            Movable = true;

            CloseBtn = new MirButton
            {
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(236, 0),
                Index = 7,
                HoverIndex = 8,
                PressedIndex = 9,
                Sound = SoundList.ButtonA,
                Hint = "Exit"
            };
            CloseBtn.Click += (o, e) => Hide();

            AplyBtn = new MirButton
            {
                Parent = this,
                Library = Libraries.CustomTitle,
                Location = new Point(160, 20),
                Index = 712,
                HoverIndex = 713,
                PressedIndex = 714,
                Sound = SoundList.ButtonA,
                Hint = "Forge"
            };
            AplyBtn.Click += (o, e) => Hide();



            SocketsContainer = new MirImageControl
            {
                Index = 28,
                Library = Libraries.EdensEliteInter,
                Location = new Point(12, 56),
                Parent = this,
            };

            RuneSlots[0] = new MirImageControl
            {
                Index = 989,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(100, 18),
            };
            //  Main Item
            ItemCells[0] = new MirItemCell
            {
                BorderColour = Color.OrangeRed,
                GridType = MirGridType.Socketing,
                Library = Libraries.Items,
                Parent = RuneSlots[0],
                Location = new Point(2, 2),
                ItemSlot = 0,
                Hint = "Place your Item here."
            };
            RuneSlots[1] = new MirImageControl
            {
                Index = 16,
                Library = Libraries.Prguse2,
                Parent = SocketsContainer,
                Location = new Point(32, 9),
                Visible = false
            };
            //  Runes
            ItemCells[1] = new MirItemCell
            {
                BorderColour = Color.Aqua,
                GridType = MirGridType.Socketing,
                Library = Libraries.Items,
                Parent = RuneSlots[1],
                Location = new Point(2, 2),
                ItemSlot = 1,
                Visible = false,
                Hint = "Place your Rune here."
            };
            RuneSlots[2] = new MirImageControl
            {
                Index = 16,
                Library = Libraries.Prguse2,
                Parent = SocketsContainer,
                Location = new Point(96, 9),
                Visible = false
            };
            ItemCells[2] = new MirItemCell
            {
                BorderColour = Color.Aqua,
                GridType = MirGridType.Socketing,
                Library = Libraries.Items,
                Parent = RuneSlots[2],
                Location = new Point(2, 2),
                ItemSlot = 2,
                Visible = false,
                Hint = "Place your Rune here."
            };
            RuneSlots[3] = new MirImageControl
            {
                Index = 16,
                Library = Libraries.Prguse2,
                Parent = SocketsContainer,
                Location = new Point(160, 9),
                Visible = false
            };
            ItemCells[3] = new MirItemCell
            {
                BorderColour = Color.Aqua,
                GridType = MirGridType.Socketing,
                Library = Libraries.Items,
                Parent = RuneSlots[3],
                Location = new Point(2, 2),
                ItemSlot = 3,
                Visible = false,
                Hint = "Place your Rune here."
            };
        }

        public void Hide()
        {
            if (Visible)
            {
                Visible = false;
                #region Clear everything
                for (int i = 0; i < Items.Length; i++)
                    Items[i] = null;
                for (int i = 0; i < Runes.Length; i++)
                    Runes[i] = null;
                for (int i = 1; i < RuneSlots.Length; i++)
                    RuneSlots[i].Visible = false;
                for (int i = 0; i < ItemCells.Length; i++)
                {
                    ItemCells[i].Item = null;
                    if (i > 0)
                    {
                        ItemCells[i].Visible = false;
                        ItemCells[i].Locked = false;
                    }
                }
                #endregion
                #region Unlock any locked cells
                for (int i = 0; i < GameScene.Scene.InventoryDialog.Grid.Length; i++)
                    if (GameScene.Scene.InventoryDialog.Grid[i].Locked)
                        GameScene.Scene.InventoryDialog.Grid[i].Locked = false;
                for (int i = 0; i < GameScene.Scene.CharacterDialog.Grid.Length; i++)
                    if (GameScene.Scene.CharacterDialog.Grid[i].Locked)
                        GameScene.Scene.CharacterDialog.Grid[i].Locked = false;
                #endregion
            }
        }

        public void Show()
        {
            if (!Visible)
                Visible = true;
        }

        public void DisplayResult(S.AddRuneResult info)
        {
            ItemInfo temp = null;
            if (info != null)
            {
                switch (info.RuneResult)
                {
                    case 0:         //  Add Succ
                        {
                            ItemCells[info.slot].Item = info.Item;
                            temp = GameScene.GetInfo(info.Item.ItemIndex);
                            ItemCells[info.slot].Item.Info = temp;
                            ItemCells[info.slot].Index = temp.Image;
                            Items[info.slot] = info.Item;
                        }
                        break;
                    case 1:         //  Remove Succ
                        {
                            ItemCells[info.slot].Item = info.Item;
                            temp = GameScene.GetInfo(info.Item.ItemIndex);
                            ItemCells[info.slot].Item.Info = temp;
                            ItemCells[info.slot].Index = temp.Image;
                            Items[info.slot] = info.Item;
                        }
                        break;
                }
            }
        }

        public void ClearRuneSockets()
        {
            ItemCells[1].Item = null;
            ItemCells[2].Item = null;
            ItemCells[3].Item = null;
            Items[1] = null;
            Items[2] = null;
            Items[3] = null;
            Runes[0] = null;
            Runes[1] = null;
            Runes[2] = null;
        }

        public void PopulateRuneCells(S.GetSocketInfo info)
        {
            ClearRuneSockets();
            if (ItemCells[0].Item != null)
            {
                if (ItemCells[0].Item.SocketCount > 0)
                {
                    switch (ItemCells[0].Item.SocketCount)
                    {
                        case 1:
                            RuneSlots[1].Visible = true;
                            RuneSlots[2].Visible = false;
                            RuneSlots[3].Visible = false;
                            ItemCells[1].Visible = true;
                            ItemCells[2].Visible = false;
                            ItemCells[3].Visible = false;
                            break;
                        case 2:
                            RuneSlots[1].Visible = true;
                            RuneSlots[2].Visible = true;
                            RuneSlots[3].Visible = false;
                            ItemCells[1].Visible = true;
                            ItemCells[2].Visible = true;
                            ItemCells[3].Visible = false;
                            break;
                        case 3:
                            RuneSlots[1].Visible = true;
                            RuneSlots[2].Visible = true;
                            RuneSlots[3].Visible = true;
                            ItemCells[1].Visible = true;
                            ItemCells[2].Visible = true;
                            ItemCells[3].Visible = true;
                            break;
                    }
                }
            }

            if (info != null)
            {
                for (int i = 0; i < info.items.Count; i++)
                {
                    if (info.items[i] != null &&
                        info.items[i].SocketedItem != null)
                    {
                        ItemInfo temp = GameScene.GetInfo(info.items[i].SocketedItem.ItemIndex);
                        info.items[i].SocketedItem.Info = temp;

                        Items[info.items[i].Slot] = info.items[i].SocketedItem;
                        ItemCells[info.items[i].Slot].Index = temp.Image;
                        ItemCells[info.items[i].Slot].Item = info.items[i].SocketedItem;
                    }
                }
            }
        }

        public void RemoveRune(S.RemoveRune p)
        {
            MirItemCell toCell;
            MirItemCell fromCell;

            int index = -1;

            for (int i = 0; i < ItemCells.Length; i++)
            {
                if (ItemCells[i] == null)
                    continue;
                if (ItemCells[i].Item == null)
                    continue;
                if (ItemCells[i].Item.UniqueID != p.UniqueID)
                    continue;
                index = i;
                break;
            }
            fromCell = ItemCells[index];



            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    toCell = GameScene.Scene.InventoryDialog.Grid[p.To - GameScene.User.BeltIdx];
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null)
                return;

            toCell.Locked = false;
            fromCell.Locked = false;

            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            GameScene.Scene.CharacterDuraPanel.GetCharacterDura();
            GameScene.User.RefreshStats();
        }

        public void ItemCell_Click(byte cell = 0)
        {
            if (cell == 0 && Items[cell] != null)
            {
                ItemCells[0].Item = Items[0];
                ItemInfo temp = GameScene.GetInfo(Items[0].ItemIndex);
                int img = Items[0].Image;
                if (temp.AllowLvlSys)
                {
                    switch (Items[0].LvlSystem)
                    {
                        case 1:
                            //  Stop all items being scanned
                            if (temp.LevelItemLooks[0] > 0)
                            {
                                img = (short)temp.LevelItemLooks[0];
                            }
                            break;
                        case 2:
                            if (temp.LevelItemLooks[1] > 0)
                            {
                                img = (short)temp.LevelItemLooks[1];
                            }
                            break;
                        case 3:
                            if (temp.LevelItemLooks[2] > 0)
                            {
                                img = (short)temp.LevelItemLooks[2];
                            }
                            break;
                        case 4:

                            if (temp.LevelItemLooks[3] > 0)
                            {
                                img = (short)temp.LevelItemLooks[3];
                            }
                            break;
                        case 5:
                            if (temp.LevelItemLooks[4] > 0)
                            {
                                img = (short)temp.LevelItemLooks[4];
                            }
                            break;
                        case 6:
                            if (temp.LevelItemLooks[5] > 0)
                            {
                                img = (short)temp.LevelItemLooks[5];
                            }
                            break;
                        case 7:
                            if (temp.LevelItemLooks[6] > 0)
                            {
                                img = (short)temp.LevelItemLooks[6];
                            }
                            break;
                        case 8:
                            if (temp.LevelItemLooks[7] > 0)
                            {
                                img = (short)temp.LevelItemLooks[7];
                            }
                            break;
                        case 9:
                            if (temp.LevelItemLooks[8] > 0)
                            {
                                img = (short)temp.LevelItemLooks[8];
                            }
                            break;
                        case 10:
                            if (temp.LevelItemLooks[9] > 0)
                            {
                                img = (short)temp.LevelItemLooks[9];
                            }
                            break;
                        case 0:
                        default:
                            img = Items[0].Image;
                            break;
                    }
                }
                ItemCells[0].Index = img != 0 ? img : Items[0].Image;//here for the socketing
                if (ItemCells[0] != null && ItemCells[0].Item.SocketCount > 0)
                {
                    if (ItemCells[0].Item.SocketCount == 1)
                    {
                        RuneSlots[1].Visible = true;
                        ItemCells[1].Visible = true;
                    }
                    else if (ItemCells[0].Item.SocketCount == 2)
                    {
                        RuneSlots[1].Visible = true;
                        ItemCells[1].Visible = true;
                        RuneSlots[2].Visible = true;
                        ItemCells[2].Visible = true;
                    }
                    else if (ItemCells[0].Item.SocketCount == 3)
                    {
                        RuneSlots[1].Visible = true;
                        ItemCells[1].Visible = true;
                        RuneSlots[2].Visible = true;
                        ItemCells[2].Visible = true;
                        RuneSlots[3].Visible = true;
                        ItemCells[3].Visible = true;
                    }
                }
            }
            else
            {
                if (Items[cell] != null)
                {
                    ItemCells[cell].Item = Items[cell];
                    if (ItemCells[cell] != null && Runes[cell].CanRemove && !ItemCells[cell].Locked)
                    {
                        Network.Enqueue(new C.SocketRuneStone { Item = ItemCells[cell].Item.UniqueID, slot = cell });
                    }
                }
            }
        }
    }
}
