using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirDataTool
{
    public partial class CraftTool : UserControl
    {
        MirDataTool MotherParent;
        public List<CraftItem> CraftItems = new List<CraftItem>();
        public List<CraftItem> selected_crafts;
        public byte SelectedClassID = 0;

        public CraftTool()
        {
            InitializeComponent();
            itemSelect.Hide();
            selectResult.Hide();

            reqClassCboBox.Items.AddRange(Enum.GetValues(typeof(MirClass)).Cast<object>().ToArray());
        }

        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
            for (int i = 0; i < MotherParent.ItemPanel.ItemInfoList.Count; i++)
            {
                if (MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.CraftingMaterial ||
                    MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.Ore ||
                    MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.Gem ||
                    MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.Scroll ||
                    MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.Awakening ||
                    MotherParent.ItemPanel.ItemInfoList[i].Type == ItemType.RuneStone)
                    itemsCBox.Items.Add(MotherParent.ItemPanel.ItemInfoList[i]);

                pickItemCBox.Items.Add(MotherParent.ItemPanel.ItemInfoList[i]);
            }
            UpdateInterface();
        }

        public void RefreshCraftList()
        {
            recipeList.SelectedIndexChanged -= recipeList_SelectedIndexChanged;
            List<bool> selected = new List<bool>();
            for (int i = 0; i < recipeList.Items.Count; i++)
                selected.Add(recipeList.GetSelected(i));
            recipeList.Items.Clear();
            for (int i = 0; i < CraftItems.Count; i++)
                recipeList.Items.Add(CraftItems[i]);
            for (int i = 0; i < selected.Count; i++)
                recipeList.SetSelected(i, selected[i]);

            recipeList.SelectedIndexChanged += recipeList_SelectedIndexChanged;
        }
        public bool newItem = false;
        public void UpdateInterface()
        {

            if (recipeList.Items.Count != CraftItems.Count)
            {
                //  Clear the list
                recipeList.Items.Clear();
                //  Clear the Materials
                requiredMaterials.Items.Clear();
                //  Reload the Craft list
                for (int i = 0; i < CraftItems.Count; i++)
                    recipeList.Items.Add(CraftItems[i]);
            }
            //  New item selected index is always last one
            if (newItem)
                recipeList.SelectedIndex = recipeList.Items.Count - 1;
            //  Selected Craft will now be the new one or the previously selected if it's not a new craft
            selected_crafts = recipeList.SelectedItems.Cast<CraftItem>().ToList();
            //  Nothing selected
            if (selected_crafts.Count == 0)
            {
                recipeNameBox.Text = string.Empty;
                resultBox.Text = string.Empty;
                reqClassCboBox.SelectedItem = null;
                reqGoldBox.Text = string.Empty;
                reqLevelBox.Text = string.Empty;
                requiredMaterials.Items.Clear();
                timeBox.Checked = false;
                dayBox.Text = "0";
                hourbox.Text = "0";
                minuteBox.Text = "0";
                rateBox.Text = "100";
                return;
            }
            //  The first Craft on the list
            CraftItem item = selected_crafts[0];
            recipeNameBox.Text = selected_crafts[0].RecipeName;
            if (selected_crafts[0].ItemResult != null)
                resultBox.Text = selected_crafts[0].ItemResult.Name;
            reqGoldBox.Text = selected_crafts[0].GoldRequired.ToString();
            reqClassCboBox.SelectedItem = selected_crafts[0].RequiredClass;
            reqLevelBox.Text = selected_crafts[0].LevelRequired.ToString();
            timeBox.Checked = false;
            if (selected_crafts[0].RequiredTimeDay > 0)
                timeBox.Checked = true;
            if (selected_crafts[0].RequiredTimeHour > 0)
                timeBox.Checked = true;
            if (selected_crafts[0].RequiredTimeMinute > 0)
                timeBox.Checked = true;
            dayBox.Text = selected_crafts[0].RequiredTimeDay.ToString();
            hourbox.Text = selected_crafts[0].RequiredTimeHour.ToString();
            minuteBox.Text = selected_crafts[0].RequiredTimeMinute.ToString();
            rateBox.Text = selected_crafts[0].CraftSuccessRate.ToString();
            //  The other Crafts selected on the list box while checking if values are the same and showing empty if they are different
            for (int i = 0; i < selected_crafts.Count; i++)
            {
                recipeNameBox.Text = selected_crafts[i].RecipeName;
                if (selected_crafts[0].ItemResult != null)
                    resultBox.Text = selected_crafts[i].ItemResult.Name;
                reqGoldBox.Text = selected_crafts[i].GoldRequired.ToString();
                reqClassCboBox.SelectedItem = selected_crafts[i].RequiredClass;
                reqLevelBox.Text = selected_crafts[i].LevelRequired.ToString();
                requiredMaterials.Items.Clear();
                for (int x = 0; x < selected_crafts[i].Requirments.Count; x++)
                    requiredMaterials.Items.Add(selected_crafts[i].Requirments[x]);
                timeBox.Checked = false;
                if (selected_crafts[0].RequiredTimeDay > 0)
                    timeBox.Checked = true;
                if (selected_crafts[0].RequiredTimeHour > 0)
                    timeBox.Checked = true;
                if (selected_crafts[0].RequiredTimeMinute > 0)
                    timeBox.Checked = true;
                dayBox.Text = selected_crafts[i].RequiredTimeDay.ToString();
                hourbox.Text = selected_crafts[i].RequiredTimeHour.ToString();
                minuteBox.Text = selected_crafts[i].RequiredTimeMinute.ToString();
                rateBox.Text = selected_crafts[i].CraftSuccessRate.ToString();
            }
        }

        private void addNewCraft_Click(object sender, EventArgs e)
        {
            newItem = true;
            selected_crafts.Clear();
            selected_crafts.Add(MotherParent.CreateCraftRecipe());
            UpdateInterface();
            newItem = false;
        }

        private void reqClassCboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (recipeList.SelectedItem == null)
                return;
            selected_crafts[0].RequiredClass = (MirClass)reqClassCboBox.SelectedItem;
            UpdateInterface();
        }

        private void reqGoldBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (recipeList.SelectedItem == null)
                return;
            if (!uint.TryParse(reqGoldBox.Text, out uint tmp))
            {
                reqGoldBox.BackColor = Color.Red;
                return;
            }
            selected_crafts[0].GoldRequired = tmp;
        }

        private void reqLevelBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (recipeList.SelectedItem == null)
                return;
            if (!ushort.TryParse(reqLevelBox.Text, out ushort tmp))
            {
                reqLevelBox.BackColor = Color.Red;
                return;
            }
            selected_crafts[0].LevelRequired = tmp;

        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (itemsCBox.SelectedItem == null)
                return;
            if (recipeList.SelectedItem == null)
                return;
            if (!byte.TryParse(amountBox.Text, out byte tmp))
            {
                amountBox.BackColor = Color.Red;
                return;
            }
            ItemInfo temp = (ItemInfo)itemsCBox.SelectedItem;
            selected_crafts[0].Requirments.Add(new CraftItemRequirement { /*Item = (ItemInfo)itemsCBox.SelectedItem*/ ItemIndex = temp.Index, Amount = tmp });
            itemSelect.Hide();
            UpdateInterface();
        }
        private void addMat_Click(object sender, EventArgs e)
        {
            if (recipeList.SelectedItem == null)
                return;
            itemSelect.Show();
        }

        private void recipeNameBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (recipeList.SelectedItem == null)
                return;
            for (int i = 0; i < selected_crafts.Count; i++)
                selected_crafts[i].RecipeName = ActiveControl.Text;

            RefreshCraftList();
        }

        private void recipeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectResult.Hide();
        }

        private void selectResultItemBtn_Click(object sender, EventArgs e)
        {
            if (recipeList.SelectedItem == null)
                return;
            selectResult.Show();
        }

        private void selectResultItem_Click(object sender, EventArgs e)
        {
            if (pickItemCBox.SelectedItem == null)
                return;
            selected_crafts[0].ItemResult = (ItemInfo)pickItemCBox.SelectedItem;
            resultBox.Text = selected_crafts[0].ItemResult.Name;
            selectResult.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (recipeList.SelectedItem == null)
                return;
            if (requiredMaterials.SelectedItem == null)
                return;
            CraftItemRequirement tmp = (CraftItemRequirement)requiredMaterials.SelectedItem;
            if (tmp != null)
            {
                selected_crafts[0].Requirments.Remove(tmp);
                UpdateInterface();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            itemSelect.Hide();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (selected_crafts.Count == 0)
                return;

            if (MessageBox.Show("Are you sure you want to remove the selected Recipe?", "Remove Recipe?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            for (int i = 0; i < selected_crafts.Count; i++)
                CraftItems.Remove(selected_crafts[i]);

            UpdateInterface();
        }

        private void dayBox_TextChanged(object sender, EventArgs e)
        {

            if (recipeList.SelectedItem == null)
                return;
            if (!byte.TryParse(dayBox.Text, out byte tmpByte))
            {
                minuteBox.BackColor = Color.Red;
                return;
            }
            selected_crafts[0].RequiredTimeDay = tmpByte;
            UpdateInterface();

        }

        private void hourbox_TextChanged(object sender, EventArgs e)
        {

            if (recipeList.SelectedItem == null)
                return;
            if (!byte.TryParse(hourbox.Text, out byte tmpByte))
            {
                minuteBox.BackColor = Color.Red;
                return;
            }
            selected_crafts[0].RequiredTimeHour = tmpByte;
            UpdateInterface();

        }

        private void minuteBox_TextChanged(object sender, EventArgs e)
        {

            if (recipeList.SelectedItem == null)
                return;
            if (!byte.TryParse(minuteBox.Text, out byte tmpByte))
            {
                minuteBox.BackColor = Color.Red;
                return;
            }
            selected_crafts[0].RequiredTimeMinute = tmpByte;
            UpdateInterface();
        }

        private void timeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selected_crafts.Count == 0)
                return;
            if (selected_crafts[0] == null)
            {
                if (timeBox.Checked)
                {
                    dayBox.ReadOnly = false;
                    hourbox.ReadOnly = false;
                    minuteBox.ReadOnly = false;
                }
                else
                {
                    dayBox.ReadOnly = true;
                    hourbox.ReadOnly = true;
                    minuteBox.ReadOnly = true;
                    selected_crafts[0].RequiredTimeDay = 0;
                    selected_crafts[0].RequiredTimeHour = 0;
                    selected_crafts[0].RequiredTimeMinute = 0;
                    UpdateInterface();
                }
            }
        }

        private void rateBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (recipeList.SelectedItem == null)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempByte))
            {
                ActiveControl.BackColor = Color.Red;
            }
            for (int i = 0; i < selected_crafts.Count; i++)
                selected_crafts[i].CraftSuccessRate = tempByte;

            RefreshCraftList();
        }
    }
}
