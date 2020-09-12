using Server.MirDatabase;
using Server.MirObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class PlayerInfoForm : Form
    {
        CharacterInfo Character = null;

        public ListView.SelectedListViewItemCollection QuestIndexChecked { get; private set; }

        public PlayerInfoForm()
        {
            InitializeComponent();
        }

        public PlayerInfoForm(uint playerId)
        {
            InitializeComponent();

            PlayerObject player = SMain.Envir.GetPlayer(playerId);

            if (player == null)
            {
                Close();
                return;
            }

            Character = SMain.Envir.GetCharacterInfo(player.Name);

            UpdatePlayerInfo();
        }

        private void UpdatePlayerInfo()
        {
            HeroList.Items.Clear();
            QuestList.Items.Clear();
            IndexTextBox.Text = Character.Index.ToString();
            NameTextBox.Text = Character.Name;
            LevelTextBox.Text = Character.Level.ToString();

            GoldLabel.Text = String.Format("{0:n0}", Character.AccountInfo.Gold);
            GGLabel.Text = String.Format("{0:n0}", Character.AccountInfo.Credit);

            if (Character.Player != null)
                CurrentMapLabel.Text = string.Format("{0} {1}:{2}", Character.Player.CurrentMap.Info.FileName, Character.CurrentLocation.X, Character.CurrentLocation.Y);
            else
                CurrentMapLabel.Text = "OFFLINE";

            PKPointsLabel.Text = Character.PKPoints.ToString();
            CurrentIPLabel.Text = Character.AccountInfo.LastIP;
            OnlineTimeLabel.Text = (DateTime.Now - Character.LastDate).TotalMinutes.ToString("##") + " minutes";

            ChatBanExpiryTextBox.Text = Character.ChatBanExpiryDate.ToString();

            foreach (var q in Character.CompletedQuests)
            {
                var quest = SMain.Envir.QuestInfoList.FirstOrDefault(x => x.Index == q);
                if (quest == null) continue;

                string heroQuest = quest.HeroQuest ? "[H]" : "";

                var itm = new ListViewItem($"{heroQuest}{q.ToString()}") { Tag = this };
                itm.SubItems.Add("Completed");

                QuestList.Items.Add(itm);
            }

            foreach (var q in Character.CurrentQuests)
            {
                string heroQuest = q.Info.HeroQuest ? "[H]" : "";

                var itm = new ListViewItem($"{heroQuest}{q.Index.ToString()}") { Tag = this };
                itm.SubItems.Add("Active");

                QuestList.Items.Add(itm);
            }

            if (Character.Player != null)
            {

                int count = 1;
                foreach (var h in Character.Player.HeroList)
                {
                    var itm = new ListViewItem(count.ToString()) { Tag = this };
                    itm.SubItems.Add(h.HeroName);
                    itm.SubItems.Add(h.HeroLevel.ToString());
                    itm.SubItems.Add(h.autoPotSystem.isEnabled.ToString());
                    itm.SubItems.Add(h.isLocked.ToString());
                    itm.SubItems.Add(h.HeroClass.ToString());
                    itm.SubItems.Add(h.HeroGender.ToString());
                    itm.SubItems.Add(h.Active.ToString());
                    itm.SubItems.Add(h.HeroExperience.ToString());
                    itm.SubItems.Add(h.Deleted.ToString());

                    HeroList.Items.Add(itm);

                    count++;
                }

            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            CharacterInfo info = Character;

            info.Name = NameTextBox.Text;
            info.Level = Convert.ToByte(LevelTextBox.Text);
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (Character.Player == null) return;

            if (SendMessageTextBox.Text.Length < 1) return;

            Character.Player.ReceiveChat(SendMessageTextBox.Text, ChatType.Announcement);
        }

        private void KickButton_Click(object sender, EventArgs e)
        {
            if (Character.Player == null) return;

            Character.Player.Connection.SendDisconnect(4);
            //also update account so player can't log back in for x minutes?
        }

        private void KillButton_Click(object sender, EventArgs e)
        {
            if (Character.Player == null) return;

            Character.Player.Die();
        }

        private void KillPetsButton_Click(object sender, EventArgs e)
        {
            if (Character.Player == null) return;

            for (int i = Character.Player.Pets.Count - 1; i >= 0; i--)
                Character.Player.Pets[i].Die();
        }
        private void SafeZoneButton_Click(object sender, EventArgs e)
        {
            Character.Player.Teleport(SMain.Envir.GetMap(Character.BindMapIndex), Character.BindLocation);
        }

        private void ChatBanButton_Click(object sender, EventArgs e)
        {
            Character.ChatBanned = true;


            DateTime.TryParse(ChatBanExpiryTextBox.Text, out DateTime date);

            Character.ChatBanExpiryDate = date;
        }

        private void ChatBanExpiryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!DateTime.TryParse(ActiveControl.Text, out DateTime temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
        }

        private void OpenAccountButton_Click(object sender, EventArgs e)
        {
            string accountId = Character.AccountInfo.AccountID;

            AccountInfoForm form = new AccountInfoForm(accountId, true);

            form.ShowDialog();
        }

        private void GoldLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {

                MirForms.PlayerItemForm form = new MirForms.PlayerItemForm();

                foreach (var i in Character.Equipment)
                {
                    if (i == null) continue;
                    ListViewItem tempItem = new ListViewItem(i.ItemIndex.ToString()) { Tag = this };

                    tempItem.SubItems.Add("Equipment");
                    tempItem.SubItems.Add(i.Name);
                    tempItem.SubItems.Add(i.Count.ToString());
                    tempItem.SubItems.Add(i.CurrentDura.ToString() + "/" + i.MaxDura.ToString());
                    tempItem.SubItems.Add(i.UniqueID.ToString());
                    form.PlayersItemListView.Items.Add(tempItem);
                }

                foreach (var i in Character.Inventory)
                {
                    if (i == null) continue;
                    ListViewItem tempItem = new ListViewItem(i.ItemIndex.ToString()) { Tag = this };

                    tempItem.SubItems.Add("Inventory");
                    tempItem.SubItems.Add(i.Name);
                    tempItem.SubItems.Add(i.Count.ToString());
                    tempItem.SubItems.Add(i.CurrentDura.ToString() + "/" + i.MaxDura.ToString());
                    tempItem.SubItems.Add(i.UniqueID.ToString());
                    form.PlayersItemListView.Items.Add(tempItem);
                }

                foreach (var i in Character.AccountInfo.Storage)
                {
                    if (i == null) continue;
                    ListViewItem tempItem = new ListViewItem(i.ItemIndex.ToString()) { Tag = this };

                    tempItem.SubItems.Add("Storage");
                    tempItem.SubItems.Add(i.Name);
                    tempItem.SubItems.Add(i.Count.ToString());
                    tempItem.SubItems.Add(i.CurrentDura.ToString() + "/" + i.MaxDura.ToString());
                    tempItem.SubItems.Add(i.UniqueID.ToString());
                    form.PlayersItemListView.Items.Add(tempItem);
                }

                foreach (var i in Character.QuestInventory)
                {
                    if (i == null) continue;
                    ListViewItem tempItem = new ListViewItem(i.ItemIndex.ToString()) { Tag = this };

                    tempItem.SubItems.Add("QuestInventory");
                    tempItem.SubItems.Add(i.Name);
                    tempItem.SubItems.Add(i.Count.ToString());
                    tempItem.SubItems.Add(i.CurrentDura.ToString() + "/" + i.MaxDura.ToString());
                    tempItem.SubItems.Add(i.UniqueID.ToString());
                    form.PlayersItemListView.Items.Add(tempItem);
                }

                foreach (var mail in Character.Mail)
                {
                    foreach (var i in mail.Items)
                    {
                        if (i == null) continue;
                        ListViewItem tempItem = new ListViewItem(i.ItemIndex.ToString()) { Tag = this };

                        tempItem.SubItems.Add("Mail");
                        tempItem.SubItems.Add(i.Name);
                        tempItem.SubItems.Add(i.Count.ToString());
                        tempItem.SubItems.Add(i.CurrentDura.ToString() + "/" + i.MaxDura.ToString());
                        tempItem.SubItems.Add(i.UniqueID.ToString());
                        form.PlayersItemListView.Items.Add(tempItem);
                    }
                }




                form.ShowDialog();

            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuestIndexChecked = QuestList.SelectedItems;
        }

        private void DeleteQuestButton_Click(object sender, EventArgs e)
        {
            if (QuestIndexChecked == null) return;
            
            foreach (var item in QuestIndexChecked)
            {
                var listViewItem = item as ListViewItem;

                if (int.TryParse(listViewItem.SubItems[0].Text, out int questIdx))
                {
                    Character.CompletedQuests.RemoveAll(x => x == questIdx);
                    Character.CurrentQuests.RemoveAll(x => x.Index == questIdx);
                }

            }

            UpdatePlayerInfo();
        }
    }
}
