using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Server.MirEnvir;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Server.MirDatabase;
using Server.MirForms.Systems;
using S = ServerPackets;
using Server.MirForms;
using System.Collections.Generic;
using System.Drawing;
using Server.Custom;

namespace Server
{
    public partial class SMain : Form
    {
        public static readonly Envir Envir = new Envir(), EditEnvir = new Envir();
        private static readonly ConcurrentQueue<string> MessageLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> DebugLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> ChatLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> PacketLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> ManagerLog = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> GMMailLog = new ConcurrentQueue<string>();
        public SMain()
        {
            InitializeComponent();
            AutoResize();
            var source = new AutoCompleteStringCollection();
            source.AddRange(new string[]
            {
                "Reboot",
                "reboot",
                "tonight",
                "Tonight",
                "tomorrow",
                "Tomorrow",
                "o'clock",
                "O'Clock",
                "O'clock",
                "icey",
                "Icey",
                "log",
                "Log",
                "off",
                "Off",
                "Update",
                "update",
                "Event",
                "event",
                "Patch",
                "patch",
                "News",
                "news",
                "PM",
                "pm",
                "am",
                "AM"
            });
            GlobalMessageTextBox.AutoCompleteCustomSource = source;
            GlobalMessageTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            GlobalMessageTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.SizeChanged += SMain_SizeChanged;
            this.GlobalMessageTextBox.KeyUp += GlobalMessageTextBox_KeyUp;
        }

        private void GlobalMessageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalMessageButton_Click(this, null);
            }
        }

        private void SMain_SizeChanged(object sender, EventArgs e)
        {
            ChatLogTextBox.Size = new Size(ChatLogTextBox.Size.Width, tabPage3.Size.Height - groupBox1.Size.Height - 4);
            GlobalMessageTextBox.Size = new Size(groupBox1.Size.Width - GlobalMessageButton.Size.Width - 8, GlobalMessageTextBox.Size.Height);
        }

        private void AutoResize()
        {
            if (PlayersOnlineListView != null)
            {
                int columnCount = PlayersOnlineListView.Columns.Count;

                foreach (ColumnHeader column in PlayersOnlineListView.Columns)
                {
                    column.Width = PlayersOnlineListView.Width / ( columnCount - 1 ) - 1;
                }
            }
            indexHeader.Width = 2;
        }


        public static void Enqueue(Exception ex)
        {
            if (MessageLog.Count < 100)
            MessageLog.Enqueue(String.Format("[{0}]: {1} - {2}" + Environment.NewLine, DateTime.Now, ex.TargetSite, ex));
            File.AppendAllText(Settings.LogPath + "Log (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                               String.Format("[{0}]: {1} - {2}" + Environment.NewLine, DateTime.Now, ex.TargetSite, ex));
        }

        public static void EnqueueDebugging(string msg)
        {
            if (DebugLog.Count < 100)
            DebugLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "DebugLog (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        public static void EnqueuePacketLog(string msg)
        {
            if (PacketLog.Count < 100)
                PacketLog.Enqueue(string.Format("[{0}]:{1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "PacketLog (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                string.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        public static void EnqueueManagerLog(string msg)
        {
            if(ManagerLog.Count < 100)
                ManagerLog.Enqueue(string.Format("[{0}]:{1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "ManagerLog (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                string.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        public static void EnqueueGMMailLog(string msg)
        {
            if (GMMailLog.Count < 100)
                GMMailLog.Enqueue(string.Format("[{0}]:{1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "GMMailLog (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                string.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        public static void EnqueueChat(string msg)
        {
            if (ChatLog.Count < 100)
            ChatLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "ChatLog (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        public static void Enqueue(string msg)
        {
            if (MessageLog.Count < 100)
            MessageLog.Enqueue(String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
            File.AppendAllText(Settings.LogPath + "Log (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, msg));
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void InterfaceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Text = string.Format("Total: {0}, Real: {1}", Envir.LastCount, Envir.LastRealCount);
                PlayersLabel.Text = string.Format("Players: {0}", Envir.Players.Count);
                MonsterLabel.Text = string.Format("Monsters: {0}", Envir.MonsterCount);
                ConnectionsLabel.Text = string.Format("Connections: {0}", Envir.Connections.Count);
                runningTimeLabel.Text = $"Running Time: {Envir.Stopwatch.ElapsedMilliseconds / 1000 / 60 / 60 / 24}d:{Envir.Stopwatch.ElapsedMilliseconds / 1000 / 60 / 60}h:{Envir.Stopwatch.ElapsedMilliseconds / 1000 / 60}m:{Envir.Stopwatch.ElapsedMilliseconds / 1000 % 60}s";

                if (Envir.LastRunTime > 200)
                    CycleDelayLabel.ForeColor = Color.Red;
                else
                    CycleDelayLabel.ForeColor = Color.Black;
                if (Settings.Multithreaded && (Envir.MobThreads != null))
                {
                    CycleDelayLabel.Text = string.Format("CycleDelays: {0:0000}", Envir.LastRunTime);
                    for (int i = 0; i < Envir.MobThreads.Length; i++)
                    {
                        if (Envir.MobThreads[i] == null) break;
                        if (Envir.MobThreads[i].LastRunTime > 200 &&
                            CycleDelayLabel.ForeColor != Color.Red)
                            CycleDelayLabel.ForeColor = Color.Red;
                        if (CycleDelayLabel.ForeColor != Color.Red)
                            CycleDelayLabel.ForeColor = Color.Black;
                        CycleDelayLabel.Text = CycleDelayLabel.Text + string.Format("|{0:0000}", Envir.MobThreads[i].LastRunTime);

                    }
                }
                else
                    CycleDelayLabel.Text = string.Format("CycleDelay: {0}", Envir.LastRunTime);

                while (!MessageLog.IsEmpty)
                {

                    if (!MessageLog.TryDequeue(out string message)) continue;

                    LogTextBox.AppendText(message);
                }

                while (!DebugLog.IsEmpty)
                {

                    if (!DebugLog.TryDequeue(out string message)) continue;

                    DebugLogTextBox.AppendText(message);
                }

                while (!ChatLog.IsEmpty)
                {

                    if (!ChatLog.TryDequeue(out string message)) continue;

                    ChatLogTextBox.AppendText(message);
                }
                ProcessPlayersOnlineTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ProcessPlayersOnlineTab()
        {
            if (PlayersOnlineListView != null)
            {
                if (PlayersOnlineListView.Items.Count != Envir.Players.Count)
                {
                    PlayersOnlineListView.Items.Clear();

                    for (int i = PlayersOnlineListView.Items.Count; i < Envir.Players.Count; i++)
                    {
                        CharacterInfo character = Envir.Players[i].Info;

                        ListViewItem tempItem = character.CreateListView();

                        PlayersOnlineListView.Items.Add(tempItem);
                    }
                }
            }
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.Start();
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.Stop();
            Envir.MonsterCount = 0;
        }

        private void SMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Envir.Stop();
            Envir.Stop_Manager();
        }

        private void closeServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void itemInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemInfoForm form = new ItemInfoForm();

            form.ShowDialog();
        }

        private void monsterInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonsterInfoForm form = new MonsterInfoForm();

            form.ShowDialog();
        }

        private void craftInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Crafting form = new Crafting();
            form.ShowDialog();
        }

        private void nPCInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NPCInfoForm form = new NPCInfoForm();

            form.ShowDialog();
        }

        private void balanceConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceConfigForm form = new BalanceConfigForm();

            form.ShowDialog();
        }

        private void questInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestInfoForm form = new QuestInfoForm();

            form.ShowDialog();
        }

        private void heroQuestInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeroQuestInfoForm form = new HeroQuestInfoForm();

            form.ShowDialog();
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();

            form.ShowDialog();
        }

        private void balanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceConfigForm form = new BalanceConfigForm();

            form.ShowDialog();
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfoForm form = new AccountInfoForm();

            form.ShowDialog();
        }

        private void mapInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapInfoForm form = new MapInfoForm();

            form.ShowDialog();
        }

        private void itemInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ItemInfoForm form = new ItemInfoForm();

            form.ShowDialog();
        }

        private void monsterInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MonsterInfoForm form = new MonsterInfoForm();

            form.ShowDialog();
        }

        private void nPCInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NPCInfoForm form = new NPCInfoForm();

            form.ShowDialog();
        }

        private void questInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            QuestInfoForm form = new QuestInfoForm();

            form.ShowDialog();
        }

        private void dragonSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DragonInfoForm form = new DragonInfoForm();

            form.ShowDialog();
        }

        private void miningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiningInfoForm form = new MiningInfoForm();

            form.ShowDialog();
        }

        private void guildsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuildInfoForm form = new GuildInfoForm();

            form.ShowDialog();
        }

        private void fishingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(0);

            form.ShowDialog();
        }

        private void GlobalMessageButton_Click(object sender, EventArgs e)
        {
            if (GlobalMessageTextBox.Text.Length < 1) return;

            foreach (var player in Envir.Players)
            {
                player.ReceiveChat(GlobalMessageTextBox.Text, ChatType.Announcement);
            }

            EnqueueChat(GlobalMessageTextBox.Text);
            GlobalMessageTextBox.Text = string.Empty;
        }

        private void PlayersOnlineListView_DoubleClick(object sender, EventArgs e)
        {
            ListView list = (ListView)sender;

            if (list.SelectedItems.Count > 0)
            {
                ListViewItem item = list.SelectedItems[0];
                string index = item.SubItems[0].Text;

                PlayerInfoForm form = new PlayerInfoForm(Convert.ToUInt32(index));

                form.ShowDialog();
            }
        }

        public void ProcessGuildViewTab()
        {

            GuildListView.Items.Clear();


            for (int i = 0; i < Envir.GuildList.Count; i++)
            {
                Server.MirObjects.GuildObject Guild = Envir.GuildList[i];

                ListViewItem tempItem = new ListViewItem(Guild.Guildindex.ToString()) { Tag = this };

                    tempItem.SubItems.Add(Guild.Name);

                if (Guild.Ranks.Count > 0 && Guild.Ranks[0].Members.Count > 0)
                    tempItem.SubItems.Add(Guild.Ranks[0].Members[0].name);
                else
                    tempItem.SubItems.Add("Not Existing");

                tempItem.SubItems.Add(Guild.Membercount.ToString());
                tempItem.SubItems.Add(Guild.Level.ToString());
                tempItem.SubItems.Add(Guild.Gold.ToString());
                tempItem.SubItems.Add(Guild.HasGT ? Guild.GTRent.ToString() : "None");

                GuildListView.Items.Add(tempItem);
            }
        }


        private void GuildListView_DoubleClick(object sender, EventArgs e)
        {

            ListViewNF list = (ListViewNF)sender;

            if (list.SelectedItems.Count > 0)
            {
                ListViewItem item = list.SelectedItems[0];
                int index = Int32.Parse(item.Text);

                Server.MirObjects.GuildObject Guild = Envir.GetGuild(index);

                MirForms.GuildItemForm form = new MirForms.GuildItemForm()
                {
                    GuildName = Guild.Name,
                    main = this,
                };

                if (Guild == null) return;

                foreach (var i in Guild.StoredItems)
                {
                    if (i == null) continue;
                    ListViewItem tempItem = new ListViewItem(i.Item.UniqueID.ToString()) { Tag = this };

                    Server.MirDatabase.CharacterInfo character = Envir.GetCharacterInfo((int)i.UserId);
                    if (character != null)
                        tempItem.SubItems.Add(character.Name);
                    else if (i.UserId == -1)
                        tempItem.SubItems.Add("Server");
                    else
                        tempItem.SubItems.Add("Unknown");

                    tempItem.SubItems.Add(i.Item.Name);
                    tempItem.SubItems.Add(i.Item.Count.ToString());
                    tempItem.SubItems.Add(i.Item.CurrentDura.ToString() + "/" + i.Item.MaxDura.ToString());

                    form.GuildItemListView.Items.Add(tempItem);
                }

                foreach(var r in Guild.Ranks)
                    foreach(var m in r.Members)
                    {
                        ListViewItem tempItem = new ListViewItem(m.name) { Tag = this };
                        tempItem.SubItems.Add(r.Name);
                        form.MemberListView.Items.Add(tempItem);
                    }


                form.ShowDialog();
            }
        }



        private void PlayersOnlineListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = PlayersOnlineListView.Columns[e.ColumnIndex].Width;
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(1);

            form.ShowDialog();
        }

        private void goodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(2);

            form.ShowDialog();
        }

        private void relationshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(4);

            form.ShowDialog();
        }

        private void refiningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(3);

            form.ShowDialog();
        }

        private void mentorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(5);

            form.ShowDialog();
        }

        private void magicInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MagicInfoForm form = new MagicInfoForm();
            form.ShowDialog();
        }

        private void SMain_Load(object sender, EventArgs e)
        {
            if (Settings.UseServerManager)
                Envir.StartManager();
            EditEnvir.LoadDB();
            Envir.Start();
            AutoResize();
        }
        public const string BackUpPath = @".\Back Up\";
        public const string AccountDBPath = BackUpPath + @"Account\";

        public const string GuildDBPath = BackUpPath + @"Guilds\";
        public const string ConquestDBPath = BackUpPath + @"Conquest\";
        public const string CraftingDBPath = BackUpPath + @"Crafting\";
        public const string DragonDBPath = BackUpPath + @"Dragon\";
        public const string GameShopDBPath = BackUpPath + @"GameShop\";
        public const string ItemDBPath = BackUpPath + @"Item\";
        public const string MagicDBPath = BackUpPath + @"Magic\";
        public const string MapDBPath = BackUpPath + @"Map\";
        public const string MobDBPath = BackUpPath + @"Mob\";
        public const string NPCDBPath = BackUpPath + @"NPC\";
        public const string QuestDBPath = BackUpPath + @"Quest\";
        public const string RecipeShopDBPath = BackUpPath + @"RecipeShop\";
        public const string RespawnTickDBPath = BackUpPath + @"Respawn\";
        public const string VersionDBPath = BackUpPath + @"Version\";

        public void InitiateBackup()
        {

        }

        private void gemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(6);

            form.ShowDialog();
        }

        private void conquestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConquestInfoForm form = new ConquestInfoForm();

            form.ShowDialog();
        }

        private void rebootServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.Reboot();
        }

        private void respawnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(7);
            
            form.ShowDialog();
        }

        private void monsterTunerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SMain.Envir.Running)
            {
                MessageBox.Show("Server must be running to tune monsters", "Notice",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            MonsterTunerForm form = new MonsterTunerForm();

            form.ShowDialog();
        }

        private void rleoadRecipeShopMenuItem_Click(object sender, EventArgs e)
        {
            int u = 0;


            foreach (var NewItem in EditEnvir.RecipeShopContents)
            {
                var OldItem = Envir.RecipeShopContents.Find(x => x.Index == NewItem.Index);
                if (OldItem != null)
                {
                    OldItem = NewItem;
                }
                else
                {
                    
                    Envir.RecipeShopContents.Add(NewItem);
                    u++;
                }
            }

            SMain.Enqueue("[Recipe Shop DataBase] total items :" + Envir.RecipeShopContents.Count.ToString());
            SMain.Enqueue("[Recipe Shop DataBase] " + (Envir.RecipeShopContents.Count - u).ToString() + " has been updated");
            SMain.Enqueue("[Recipe Shop DataBase] " + u.ToString() + " has been added");
        }

        private void reloadBeneMenuItem_Click(object sender, EventArgs e)
        {
            Settings.LoadBeneConfigs();
        }

        private void exportItemInfo_Click(object sender, EventArgs e)
        {
            using (FileStream stream = File.Create(@".\ItemInfo.dat"))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(DateTime.Now.ToBinary());
                writer.Write(Envir.ItemInfoList.Count);
                for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                {
                    EditEnvir.ItemInfoList[i].Save(writer);
                }
            }
        }

        private void reloadHumMobsMenuItem_Click(object sender, EventArgs e)
        {
            Settings.LoadHumMobs();
        }

        private void robotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.LoadRobot();
            MessageLog.Enqueue(String.Format("[{0}]: Robot reloaded" + Environment.NewLine, DateTime.Now));
        }

        private void dropsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var t in Envir.MonsterInfoList)
                t.LoadDrops();

            foreach (var t in Envir.MonsterInfoList)
                t.LoadEliteDrops();

            Envir.LoadFishingDrops();
            Envir.LoadAwakeningMaterials();
            Envir.LoadStrongBoxDrops();
            Envir.LoadBlackStoneDrops();
            Envir.LoadRuneMaterials();
            Envir.FortuneBoxWeapon();
            Envir.FortuneBoxArmour();
            Envir.FortuneBoxAccessory();
            MessageLog.Enqueue(String.Format("[{0}]: Drops reloaded" + Environment.NewLine, DateTime.Now));
        }

        private void nPCScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Envir.MapList.Count; i++)
            {
                for (int j = 0; j < Envir.MapList[i].NPCs.Count; j++)
                {
                    Envir.MapList[i].NPCs[j].LoadInfo(true);
                    Envir.MapList[i].NPCs[j].Broadcast(Envir.MapList[i].NPCs[j].GetUpdateInfo());
                }
            }

            Envir.DefaultNPC.LoadInfo(true);

            MessageLog.Enqueue(String.Format("[{0}]: NPC Scripts reloaded" + Environment.NewLine, DateTime.Now));
        }

        private void MainTab_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessGuildViewTab();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int u = 0;


            foreach (var NewItem in EditEnvir.ItemInfoList)
            {
                ItemInfo OldItem = Envir.ItemInfoList.Find(x => x.Index == NewItem.Index);
                if (OldItem != null)
                {
                    OldItem.UpdateItem(NewItem);
                }
                else
                {
                    ItemInfo CloneItem = ItemInfo.CloneItem(NewItem);
                    Envir.ItemInfoList.Add(CloneItem);
                    u++;
                }
            }

            SMain.Enqueue("[Item DataBase] total items :" + Envir.ItemInfoList.Count.ToString());
            SMain.Enqueue("[Item DataBase] " + (Envir.ItemInfoList.Count - u).ToString() + " has been updated");
            SMain.Enqueue("[Item DataBase] " + u.ToString() + " has been added");
            /*
            foreach (var c in Envir.Connections)// update all info on players items
            {
                if (!c.Connected) continue;

                foreach (var i in c.SentItemInfo)
                {
                    var ni = new S.UpdateItemInfo { Info = i };
                    //get name for updatetoname
                    if (ni.Info.UpdateTo > 0)
                        ni.updateto = Envir.GetItemInfo(ni.Info.UpdateTo).FriendlyName;

                    c.Enqueue(ni);

                }
            }
            */
            
            foreach (var p in Envir.Players) // refresh all existing players stats
            {
                if (p.Info == null) continue;

                p.RefreshStats();
                p.Enqueue(new S.RefreshStats());

            }
            
        }




        private void REGameShopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int u = 0;


            foreach (var NewItem in EditEnvir.GameShopList)
            {
                var OldItem = Envir.GameShopList.Find(x => x.GIndex == NewItem.GIndex);
                if (OldItem != null)
                {
                    OldItem.UpdateItem(NewItem);
                }
                else
                {
                    var CloneItem = GameShopItem.CloneItem(NewItem);
                    Envir.GameShopList.Add(CloneItem);
                    u++;
                }
            }

            SMain.Enqueue("[Gameshop DataBase] total items :" + Envir.GameShopList.Count.ToString());
            SMain.Enqueue("[Gameshop DataBase] " + (Envir.GameShopList.Count - u).ToString() + " has been updated");
            SMain.Enqueue("[Gameshop DataBase] " + u.ToString() + " has been added");

            foreach (var p in Envir.Players)// update all info on players items
            {
                if (p.Info == null) continue;

                p.GetGameShop();

            }
        }


        private void monsterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int u = 0;


            foreach (var NewMob in EditEnvir.MonsterInfoList)
            {
                MonsterInfo OldMob = Envir.MonsterInfoList.Find(x => x.Index == NewMob.Index);
                if (OldMob != null)
                {
                    OldMob.UpdateMonster(NewMob);
                }
                else
                {
                    MonsterInfo CloneMonster = MonsterInfo.CloneMonster(NewMob);
                    Envir.MonsterInfoList.Add(CloneMonster);
                    u++;
                }
            }

            SMain.Enqueue("[Monster DataBase] total monsters :" + Envir.MonsterInfoList.Count.ToString());
            SMain.Enqueue("[Monster DataBase] " + (Envir.MonsterInfoList.Count - u).ToString() + " has been updated");
            SMain.Enqueue("[Monster DataBase] " + u.ToString() + " has been added");


        }

        private void magicToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (var NewMagic in EditEnvir.MagicInfoList)
            {
                MagicInfo OldMagic = Envir.MagicInfoList.Find(x => x.Spell == NewMagic.Spell);
                if (OldMagic != null)
                {
                   OldMagic.Copy(NewMagic);
                }
            }

            foreach(var p in Envir.Players)
            {
                foreach (var Magic in Envir.MagicInfoList)
                {
                    p.Enqueue(new S.RefreshMagic { Magic = (new UserMagic(Magic.Spell)).CreateClientMagic() });
                }
            }

            SMain.Enqueue("[Magic DataBase] total magics :" + Envir.MagicInfoList.Count.ToString());
            SMain.Enqueue("[Magic DataBase] " + (Envir.MagicInfoList.Count).ToString() + " has been updated");

        }

        private void reloadShieldConfigStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.ReloadShield();
        }

        private void craftingReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newCrafts = 0;
            int updatedCrafts = 0;
            foreach(var craft in EditEnvir.CraftItems)
            {
                CraftItem tmp = Envir.CraftItems.Find(x => x.Recipie == craft.Recipie);
                if (tmp != null)
                {
                    tmp = craft;
                    updatedCrafts++;
                }
                else
                {
                    CraftItem newCraft = craft;
                    Envir.CraftItems.Add(newCraft);
                    newCrafts++;
                }
            }
            SMain.Enqueue("[Recipes] total :" + Envir.CraftItems.Count.ToString());
            SMain.Enqueue(string.Format("[Recipes] Updated : {0} | New Recipes : {1}", updatedCrafts, newCrafts));
            //Envir.LoadCrafts();
        }

        private void lineMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.LoadLineMessages();
        }

        private void LoginNoticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.LoadLogNotice();
        }

        private void mapEXPConfigMenuItem_Click(object sender, EventArgs e)
        {
            MapEXPForm form = new MapEXPForm();

            form.ShowDialog();
        }

        private void questToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int updated = 0;
            int added = 0;
            List<QuestInfo> newList = new List<QuestInfo>();
            foreach (var NewQuest in EditEnvir.QuestInfoList)
            {
                QuestInfo OldQuest = Envir.QuestInfoList.Find(x => x.Index == NewQuest.Index);
                if (OldQuest != null)
                {                    
                    OldQuest.UpdateQuestInfo(NewQuest);
                    OldQuest.LoadInfo();
                    updated++;
                }
                else if (OldQuest == null)
                {
                    QuestInfo CloneQuest = QuestInfo.CloneQuest(NewQuest);
                    CloneQuest.LoadInfo();
                    Envir.QuestInfoList.Add(CloneQuest);
                    added++;
                }                
            }
            Enqueue("[Quest DataBase] total quests :" + Envir.QuestInfoList.Count.ToString());
            Enqueue(string.Format("[Quest Database] {0} quests have been updated", updated));
            Enqueue("[Quest DataBase] " + added.ToString() + " have been added");

            nPCScriptToolStripMenuItem_Click(null, null);


            foreach (var p in Envir.Players)
            {

                p.Connection.SentQuestInfo.Clear();
                p.Enqueue(new S.RefreshQuestInfo());         
                p.GetQuestInfo();

                for (int i = 0; i < p.CurrentQuests.Count; i++)
                {
                    if (p.CurrentQuests[i].Completed)
                        continue;

                    p.CurrentQuests[i].ResyncTasks();
                    p.SendUpdateQuest(p.CurrentQuests[i], QuestState.Update);

                }
                p.GetCompletedQuests();
                p.GetClientCompletedQuests();
            }
            
        }

        private void heroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeroConfigForm form = new HeroConfigForm();
            form.ShowDialog();
        }

        private void GrpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInfoForm form = new SystemInfoForm(8);

            form.ShowDialog();
        }

        private void recipeShopToolItem_Click(object sender, EventArgs e)
        {
            RecipeShopForm form = new RecipeShopForm();
            form.ShowDialog();
        }

        private void gameshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameShop form = new GameShop();
            form.ShowDialog();
        }

        private void raidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaidDialog form = new RaidDialog();
            form.ShowDialog();
        }

        private void logNoticeReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Envir.LoadLogNotice();
        }
    }
}
