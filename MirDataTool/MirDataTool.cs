using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirDataTool
{
    public partial class MirDataTool : Form
    {
        #region Global Controls
        public ItemToolPanel ItemPanel
        {
            get { return itemToolPanel ?? null; }
        }
        public GameShopToolPanel GameShopPanel
        {
            get { return gameshopToolPanel ?? null; }
        }
        public MonsterToolPanel MonsterPanel
        {
            get { return monsterToolPanel ?? null; }
        }
        public MapToolPanel MapPanel
        {
            get { return mapToolPanel ?? null; }
        }
        public NPCToolPanel NPCPanel
        {
            get { return npcToolPanel ?? null; }
        }
        public QuestToolPanel QuestPanel
        {
            get { return questToolPanel ?? null; }
        }
        public DropBuilderPanel DropPanel
        {
            get { return dropBuilderPanel1 ?? null; }
        }
        public ConquestToolPanel ConquestPanel
        {
            get { return conquestToolPanel ?? null; }
        }

        public CraftTool CraftToolPanel
        {
            get { return craftTool ?? null; }
        }
        #endregion
        #region Indexing
        private int itemIndex = 0;
        public int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }
        private int questIndex = 0;
        public int QuestIndex
        {
            get { return questIndex; }
            set { questIndex = value; }
        }
        private int monsterIndex = 0;
        public int MonsterIndex
        {
            get { return monsterIndex; }
            set { monsterIndex = value; }
        }
        private int mapIndex = 0;
        public int MapIndex
        {
            get { return mapIndex; }
            set { mapIndex = value; }
        }
        private int npcIndex = 0;
        public int NPCIndex
        {
            get { return npcIndex; }
            set { npcIndex = value; }
        }
        private int gameshopIndex = 0;
        public int GameShopIndex
        {
            get { return gameshopIndex; }
            set { gameshopIndex = value; }
        }
        private int conquestIndex = 0;
        public int ConquestIndex
        {
            get { return conquestIndex; }
            set { conquestIndex = value; }
        }
        private int respawnIndex = 0;
        public int RespawnIndex
        {
            get { return respawnIndex; }
            set { respawnIndex = value; }
        }
        private int craftIndex = 0;
        public int CraftIndex
        {
            get { return craftIndex; }
            set { craftIndex = value; }
        }
        #endregion
        /// <summary>
        /// The Current version of the Tool, Current Version : 0.04
        /// </summary>
        public double ToolVersion = 0.04;
        /// <summary>
        /// Identifies if we need to Save.
        /// </summary>
        public bool NeedSave = false;
        #region Constructor
        /// <summary>
        /// The Constructor of the Tool
        /// </summary>
        public MirDataTool()
        {
            InitializeComponent();
            FormClosing += MirDataTool_FormClosing;
            KeyDown += MirDataTool_KeyDown;
            #region Warn the user of mis matching data!
            DialogResult dialogResult = MessageBox.Show(string.Format("This tool requires all Data match if they are going to interact with one another.\r\n" +
                                                                      "This Means Map Info and Game Shop contents will hold incorrect values.\r\n" +
                                                                      "It is advised you export the data from the Server and then work on that externally." +
                                                                      "Do you wish to continue?"), "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
                Close();
            #endregion
            #region First time run?
            if (Program.firstRun)
            {
                ConfigForm form = new ConfigForm(ToolVersion);
                form.ShowDialog();
            }
            #endregion
            #region Tool Version check
            if (ToolVersion > Settings.ToolVersion)
            {
                ConfigForm form = new ConfigForm(ToolVersion);
                form.ShowDialog();
            }
            #endregion
            #region Initial File Checks
            if ((!File.Exists(Settings.DatabasePath + "ItemInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "QuestInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "MonsterInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "MapInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "NPCInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "GameShopInfo.dat"))||
                (!File.Exists(Settings.DatabasePath + "ConquestInfo.dat")) ||
                (!File.Exists(Settings.DatabasePath + "CraftInfo.dat")))
            {
                DialogResult result = MessageBox.Show(string.Format("One or more .dat files could not be found!\r\n" +
                                                                    "Clicking Yes will bring up the Config dialog for you to locate the .dat file(s).\r\n" +
                                                                    "Clicking No will allow you to create a new .dat file.\r\n" +
                                                                    "Clicking Cancel will close the Tool."), 
                                                                    ".dat(s) not found!", 
                                                                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ConfigForm form = new ConfigForm(ToolVersion);
                    form.ShowDialog();
                }
                else 
                {
                    #region Data file creation
                    if (!File.Exists(Settings.DatabasePath + "ItemInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "ItemInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        ItemIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "QuestInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "QuestInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        QuestIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "MonsterInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "MonsterInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        MonsterIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "MapInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "MapInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        MapIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "NPCInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "NPCInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        NPCIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "GameShopInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "GameShopInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        GameShopIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "ConquestInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "ConquestInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        ConquestIndex = 0;
                    }
                    if (!File.Exists(Settings.DatabasePath + "CraftInfo.dat"))
                    {
                        using (FileStream stream = File.Create(Settings.DatabasePath + "CraftInfo.dat"))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(0);
                            }
                        }
                        CraftIndex = 0;
                    }
                    #endregion
                }
            }
            else
            {
                
                string ReadResult = "";
                #region Load Data
                int highestIndex = 0;                
                if (File.Exists(Settings.DatabasePath + "ItemInfo.dat"))
                {
                    List<ItemInfo> temp = new List<ItemInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "ItemInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new ItemInfo(reader, Settings.DatabaseVersion, Settings.CustomDatabaseVersion));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    ItemIndex = highestIndex;
                    ItemPanel.ItemInfoList = temp;
                    ReadResult += string.Format("{0} Items loaded, Next Item Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "QuestInfo.dat"))
                {
                    highestIndex = 0;
                    List<QuestInfo> temp = new List<QuestInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "QuestInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new QuestInfo(reader));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    QuestIndex = highestIndex;
                    QuestPanel.QuestInfoList = temp;
                    ReadResult += string.Format("{0} Quests loaded, Next Quest Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "MonsterInfo.dat"))
                {
                    highestIndex = 0;
                    List<MonsterInfo> temp = new List<MonsterInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "MonsterInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new MonsterInfo(reader));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    MonsterIndex = highestIndex;
                    MonsterPanel.MonsterInfoList = temp;
                    ReadResult += string.Format("{0} Monsters loaded, Next Monster Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "MapInfo.dat"))
                {
                    highestIndex = 0;
                    List<MapInfo> temp = new List<MapInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "MapInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new MapInfo(reader));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    MapIndex = highestIndex;
                    MapPanel.MapInfoList = temp;
                    ReadResult += string.Format("{0} Maps loaded, Next Map Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "NPCInfo.dat"))
                {
                    highestIndex = 0;
                    List<NPCInfo> temp = new List<NPCInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "NPCInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new NPCInfo(reader));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    NPCIndex = highestIndex;
                    NPCPanel.NPCInfoList = temp;
                    ReadResult += string.Format("{0} NPCs loaded, Next NPC Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "GameShopInfo.dat"))
                {
                    highestIndex = 0;
                    List<GameShopItem> temp = new List<GameShopItem>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "GameShopInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new GameShopItem(reader, true));
                                if (temp[i].GIndex > highestIndex)
                                    highestIndex = temp[i].GIndex;
                            }
                        }
                    }
                    GameShopIndex = highestIndex;
                    GameShopPanel.GameShopInfoList = temp;
                    ReadResult += string.Format("{0} GameShop Items loaded, Next GameShop Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "ConquestInfo.dat"))
                {
                    highestIndex = 0;
                    List<ConquestInfo> temp = new List<ConquestInfo>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "ConquestInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new ConquestInfo(reader));
                                if (temp[i].Index > highestIndex)
                                    highestIndex = temp[i].Index;
                            }
                        }
                    }
                    ConquestIndex = highestIndex;
                    ConquestPanel.ConquestInfos = temp;
                    ReadResult += string.Format("{0} Conquests loaded, Next Conquest Index {1}.\r\n", temp.Count, highestIndex + 1);
                }
                if (File.Exists(Settings.DatabasePath + "CraftInfo.dat"))
                {
                    highestIndex = 0;
                    List<CraftItem> temp = new List<CraftItem>();
                    using (FileStream stream = File.OpenRead(Settings.DatabasePath + "CraftInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                temp.Add(new CraftItem(reader));
                                if (temp[i].Recipie > (byte)highestIndex)
                                    highestIndex = (int)temp[i].Recipie;
                            }
                        }
                    }
                    CraftIndex = highestIndex;
                    CraftToolPanel.CraftItems = temp;
                    ReadResult += string.Format("{0} Crafts loaded, Next Craft Index {1}\r\n", temp.Count, highestIndex + 1);
                }
                #endregion
                MessageBox.Show(ReadResult);
            }
            #endregion
            #region Interface Update check
            if (itemToolPanel.ItemInfoList.Count > 0)
                itemToolPanel.UpdateList();
            #endregion
            #region Set Child Control Parent as the MirDataTool to share functions and interact with each other.
            ConquestPanel.SetChild(this);
            DropPanel.SetChild(this);
            GameShopPanel.SetChild(this);
            ItemPanel.SetChild(this);            
            MapPanel.SetChild(this);
            MonsterPanel.SetChild(this);            
            NPCPanel.SetChild(this);
            QuestPanel.SetChild(this);
            CraftToolPanel.SetChild(this);
            #endregion
        }
        #endregion        
        #region Global Functions

        public MonsterInfo GetMonsterInfo(int index)
        {
            for (int i = 0; i < MonsterPanel.MonsterInfoList.Count; i++)
                if (monsterIndex == MonsterPanel.MonsterInfoList[i].Index)
                    return MonsterPanel.MonsterInfoList[i];
            return null;
        }

        public CraftItem CreateCraftRecipe()
        {
            int idx = CraftIndex + 1;
            CraftToolPanel.CraftItems.Add(new CraftItem { Recipie = (byte)++CraftIndex});
            return GetCraftInfo(idx);
        }

        public CraftItem GetCraftInfo(int index)
        {
            for (int i = 0; i < CraftToolPanel.CraftItems.Count; i++)
                if (CraftToolPanel.CraftItems[i].Recipie == (byte)index)
                    return CraftToolPanel.CraftItems[i];
            return null;
        }

        /// <summary>
        /// Return the new index
        /// </summary>
        /// <returns>Returns Index of the New Item</returns>
        public ItemInfo AddItem()
        {
            int idx = ItemIndex + 1;
            ItemPanel.ItemInfoList.Add(new ItemInfo { Index = ++ItemIndex });            
            return GetItemInfo(idx);
        }

        public MonsterInfo AddMonster()
        {
            int idx = MonsterIndex + 1;
            MonsterPanel.MonsterInfoList.Add(new MonsterInfo { Index = ++MonsterIndex });
            return GetMonsterInfo(idx);
        }

        /// <summary>
        /// Add a new Item to the GameShop
        /// </summary>
        /// <param name="info">The Item to add to the Game Shop</param>
        public void AddGameShopItem(ItemInfo info)
        {

            GameShopPanel.GameShopInfoList.Add(new GameShopItem { GIndex = ++GameShopIndex, ItemIndex = info.Index, Info = info });
            GameShopItem item = GameShopPanel.GameShopInfoList[GameShopPanel.GameShopInfoList.Count - 1];
            DialogResult result = MessageBox.Show("Would you like to edit the Item in the GameShop now?", "Switch to GameShop Tool?", MessageBoxButtons.YesNo);            
            GameShopPanel._SelectedItems.Clear();
            GameShopPanel._SelectedItems.Add(item);
            GameShopPanel.UpdateGameShop(item);
            if (result == DialogResult.Yes)
                tabControl1.SelectedTab = tabGameShopTool;
        }

        /// <summary>
        /// Add new items to the GameShop
        /// </summary>
        /// <param name="info">A list of Items to add to the Game Shop</param>
        public void AddGameShopItem(List<ItemInfo> info)
        {
            if (info == null ||
                info.Count == 0)
                return;
            List<GameShopItem> list = new List<GameShopItem>();
            for (int i = 0; i < info.Count; i++)
            {
                GameShopPanel.GameShopInfoList.Add(new GameShopItem { GIndex = ++GameShopIndex, ItemIndex = info[i].Index, Info = GetItemInfo(info[i].Index) });
                list.Add(GameShopPanel.GameShopInfoList[i]);
            }
            GameShopPanel._SelectedItems.Clear();
            GameShopPanel._SelectedItems = list;
            GameShopPanel.UpdateGameShop(list);
            DialogResult result = MessageBox.Show("Would you like to edit the Item in the GameShop now?", "Switch to GameShop Tool?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                tabControl1.SelectedTab = tabGameShopTool;
        }

        /// <summary>
        /// Get Item by Index.
        /// </summary>
        /// <param name="index">The Index of the item to find.</param>
        /// <returns>Item Information.</returns>
        public ItemInfo GetItemInfo(int index)
        {
            for (int i = 0; i < ItemPanel.ItemInfoList.Count; i++)
                if (ItemPanel.ItemInfoList[i].Index == index)
                    return ItemPanel.ItemInfoList[i];
            return null;
        }
        /// <summary>
        /// Get Item by Name.
        /// </summary>
        /// <param name="name">The name of the Item to find.</param>
        /// <returns>Item Information.</returns>
        public ItemInfo GetItemInfo(string name)
        {
            for (int i = 0; i < ItemPanel.ItemInfoList.Count; i++)
                if (ItemPanel.ItemInfoList[i].Name == name)
                    return ItemPanel.ItemInfoList[i];
            return null;
        }
        #endregion
        #region Mother Controls
        /// <summary>
        /// Upon closing the Control, check if we need to save the data.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The closing arguments</param>
        private void MirDataTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (NeedSave)
            {
                SaveItems();
                SaveQuests();
                SaveMonsters();
                SaveMaps();
                SaveNPCs();
                SaveGameShop();
                SaveConquests();
                SaveCrafts();
                NeedSave = false;
            }
        }
        /// <summary>
        /// To adjust the config file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm(ToolVersion);
            form.ShowDialog();
        }
        /// <summary>
        /// Display a quick guide on how to use the advanced functions of this tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Manually save the data currently selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                //  Item Tool
                case 0:
                    SaveItems();
                    break;
                //  Monster Tool
                case 1:
                    SaveMonsters();
                    break;
                //  Quest Tool
                case 2:
                    SaveQuests();
                    break;
                //  Map Tool
                case 3:
                    SaveMaps();
                    break;
                //  NPC Tool
                case 4:
                    SaveNPCs();
                    break;
                //  GameShop Tool
                case 5:
                    SaveGameShop();
                    break;
                //  DropBuilder Tool
                case 6:
                    //DropPanel.UpdateList();
                    break;
                case 7:
                    SaveCrafts();
                    break;
            }
        }

        /// <summary>
        /// Manually save ALL data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveItems();
            SaveQuests();
            SaveMonsters();
            SaveMaps();
            SaveNPCs();
            SaveGameShop();
            SaveConquests();
            SaveCrafts();
            NeedSave = false;
        }

        /// <summary>
        /// Upon changing tab we'll update the tab they selected incase data has updated!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                //  Item Tool
                case 0:
                    ItemPanel.UpdateList();
                    break;
                //  Monster Tool
                case 1:
                    MonsterPanel.UpdateList();
                    break;
                //  Quest Tool
                case 2:
                    QuestPanel.UpdateList();
                    break;
                //  Map Tool
                case 3:
                    MapPanel.UpdateList();
                    break;
                //  NPC Tool
                case 4:
                    NPCPanel.UpdateList();
                    break;
                //  GameShop Tool
                case 5:
                    GameShopPanel.UpdateList();
                    break;
                //  DropBuilder Tool
                case 6:
                    ConquestPanel.UpdateList();
                    break;
                case 7:                    
                    DropPanel.UpdateList();
                    break;
            }
        }

        /// <summary>
        /// Close without saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NeedSave = false;
            Close();
        }

        private void MirDataTool_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey && e.KeyCode == Keys.S)
                saveToolStripMenuItem_Click(this, null);
            else if (e.KeyCode == Keys.LControlKey && e.KeyCode == Keys.A)
                saveAllToolStripMenuItem_Click(this, null);
            else if (e.KeyCode == Keys.LControlKey && e.KeyCode == Keys.L)
                closeToolStripMenuItem_Click(this, null);
            else if (e.KeyCode == Keys.LControlKey && e.KeyCode == Keys.O)
            {
                ConfigForm form = new ConfigForm(ToolVersion);
                form.ShowDialog();
            }
            else if (e.KeyCode == Keys.LControlKey && e.KeyCode == Keys.H)
                helpToolStripMenuItem_Click(this, null);
        }
        #endregion
        #region Saving
        public void SaveGameShop()
        {
            if (GameShopPanel.GameShopInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "GameShopInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(GameShopPanel.GameShopInfoList.Count);
                        for (int i = 0; i < GameShopPanel.GameShopInfoList.Count; i++)
                            GameShopPanel.GameShopInfoList[i].Save(writer, true);
                    }
                }
            }
        }

        public void SaveQuests()
        {
            if (QuestPanel.QuestInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "QuestInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(QuestPanel.QuestInfoList.Count);
                        for (int i = 0; i < QuestPanel.QuestInfoList.Count; i++)
                            QuestPanel.QuestInfoList[i].Save(writer);
                    }
                }
            }
        }

        public void SaveNPCs()
        {
            if (NPCPanel.NPCInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "NPCInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(NPCPanel.NPCInfoList.Count);
                        for (int i = 0; i < NPCPanel.NPCInfoList.Count; i++)
                            NPCPanel.NPCInfoList[i].Save(writer);
                    }
                }
            }
        }

        public void SaveCrafts()
        {
            if (CraftToolPanel.CraftItems.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "CraftInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(CraftToolPanel.CraftItems.Count);
                        for (int i = 0; i < CraftToolPanel.CraftItems.Count; i++)
                            CraftToolPanel.CraftItems[i].Save(writer);
                    }
                }
            }
        }

        public void SaveConquests()
        {
            if (ConquestPanel.ConquestInfos.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "ConquestInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(ConquestPanel.ConquestInfos.Count);
                        for (int i = 0; i < ConquestPanel.ConquestInfos.Count; i++)
                            ConquestPanel.ConquestInfos[i].Save(writer);
                    }
                }
            }
        }

        public void SaveMaps()
        {
            if (MapPanel.MapInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "MapInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(MapPanel.MapInfoList.Count);
                        for (int i = 0; i < MapPanel.MapInfoList.Count; i++)
                            MapPanel.MapInfoList[i].Save(writer);
                    }
                }
            }
        }

        public void SaveMonsters()
        {
            if (MonsterPanel.MonsterInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "MonsterInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(MonsterPanel.MonsterInfoList.Count);
                        for (int i = 0; i < MonsterPanel.MonsterInfoList.Count; i++)
                            MonsterPanel.MonsterInfoList[i].Save(writer);
                    }
                }
            }
        }

        public void SaveItems()
        {
            if (ItemPanel.ItemInfoList.Count > 0)
            {
                using (FileStream stream = File.Create(Settings.DatabasePath + "ItemInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(ItemPanel.ItemInfoList.Count);
                        for (int i = 0; i < ItemPanel.ItemInfoList.Count; i++)
                            ItemPanel.ItemInfoList[i].Save(writer);
                    }
                }
            }
        }

        #endregion
    }
}
