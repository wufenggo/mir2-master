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
    public partial class GameShopToolPanel : UserControl
    {
        MirDataTool MotherParent;
        public List<GameShopItem> GameShopInfoList = new List<GameShopItem>();
        public List<GameShopItem> _SelectedItems = new List<GameShopItem>();
        public GameShopToolPanel()
        {
            InitializeComponent();
        }
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;

        }
        private void GameShopListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        public void UpdateGameShop(List<GameShopItem> info)
        {
            GameShopListBox.Items.Clear();
            for (int i = 0; i < GameShopInfoList.Count; i++)
                GameShopListBox.Items.Add(GameShopInfoList[i]);
            for (int i = 0; i < GameShopListBox.Items.Count; i++)
            {
                GameShopItem tmp = (GameShopItem)GameShopListBox.Items[i];
                for (int x = 0; x < _SelectedItems.Count; x++)
                {
                    if (tmp == _SelectedItems[x])
                    {
                        GameShopListBox.SetSelected(i, true);
                    }
                }
            }
            UpdateInterface();
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        public void UpdateGameShop(GameShopItem info)
        {
            GameShopListBox.Items.Clear();
            for (int i = 0; i < GameShopInfoList.Count; i++)
                GameShopListBox.Items.Add(GameShopInfoList[i]);

            GameShopListBox.SelectedIndex = GameShopListBox.Items.Count - 1;
            UpdateInterface();
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        public void UpdateList()
        {
            GameShopListBox.Items.Clear();
            for (int i = 0; i < GameShopInfoList.Count; i++)
                GameShopListBox.Items.Add(GameShopInfoList[i]);
            UpdateInterface();
        }

        public void UpdateInterface(bool refreshList = false)
        {
            _SelectedItems = GameShopListBox.SelectedItems.Cast<GameShopItem>().ToList();
            if (refreshList)
            {
                UpdateList();
            }

            if (_SelectedItems.Count == 0)
            {
                GoldPrice_textbox.Text = String.Empty;
                GPPrice_textbox.Text = String.Empty;
                Stock_textbox.Text = String.Empty;
                Individual_checkbox.Checked = false;
                Class_combo.Text = "All";
                Category_textbox.Text = "";
                TopItem_checkbox.Checked = false;
                DealofDay_checkbox.Checked = false;
                ItemDetails_gb.Visible = false;
                TotalSold_label.Text = "0";
                LeftinStock_label.Text = "";
                Count_textbox.Text = String.Empty;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                return;
            }

            ItemDetails_gb.Visible = true;

            GoldPrice_textbox.Text = _SelectedItems[0].GoldPrice.ToString();
            GPPrice_textbox.Text = _SelectedItems[0].CreditPrice.ToString();
            Stock_textbox.Text = _SelectedItems[0].Stock.ToString();
            Individual_checkbox.Checked = _SelectedItems[0].iStock;
            Class_combo.Text = _SelectedItems[0].Class;
            Category_textbox.Text = _SelectedItems[0].Category;
            TopItem_checkbox.Checked = _SelectedItems[0].TopItem;
            DealofDay_checkbox.Checked = _SelectedItems[0].Deal;
            Count_textbox.Text = _SelectedItems[0].Count.ToString();
            checkBox1.Checked = _SelectedItems[0].CanBuyCredit;
            checkBox2.Checked = _SelectedItems[0].CanBuyGold;
            //GetStats();

        }

        private void UpdateGameShopList()
        {
            GameShopListBox.SelectedIndexChanged -= GameShopListBox_SelectedIndexChanged;
            GameShopListBox.Items.Clear();
            for (int i = 0; i < GameShopInfoList.Count; i++)
            {
                if (ClassFilter_lb.Text == "All Classes" || GameShopInfoList[i].Class == ClassFilter_lb.Text)
                    if (SectionFilter_lb.Text == "All Items" || GameShopInfoList[i].TopItem && SectionFilter_lb.Text == "Top Items" || GameShopInfoList[i].Deal && SectionFilter_lb.Text == "Sale Items" || GameShopInfoList[i].Date > DateTime.Now.AddDays(-7) && SectionFilter_lb.Text == "New Items")
                        if (CategoryFilter_lb.Text == "All Categories" || GameShopInfoList[i].Category == CategoryFilter_lb.Text)
                            GameShopListBox.Items.Add(GameShopInfoList[i]);
            }
            GameShopListBox.SelectedIndexChanged += GameShopListBox_SelectedIndexChanged;
        }

        private void LoadGameShopItems()
        {


            ClassFilter_lb.Items.Clear();
            CategoryFilter_lb.Items.Clear();
            GameShopListBox.Items.Clear();

            ClassFilter_lb.Items.Add("All Classes");
            CategoryFilter_lb.Items.Add("All Categories");


            for (int i = 0; i < GameShopInfoList.Count; i++)
            {
                if (!ClassFilter_lb.Items.Contains(GameShopInfoList[i].Class)) ClassFilter_lb.Items.Add(GameShopInfoList[i].Class);
                if (!CategoryFilter_lb.Items.Contains(GameShopInfoList[i].Category)) CategoryFilter_lb.Items.Add(GameShopInfoList[i].Category);

                GameShopListBox.Items.Add(GameShopInfoList[i]);
            }

            ClassFilter_lb.Text = "All Classes";
            CategoryFilter_lb.Text = "All Categories";
            SectionFilter_lb.Text = "All Items";
        }

        private void GoldPrice_textbox_TextChanged(object sender, EventArgs e)
        {


            if (!uint.TryParse(GoldPrice_textbox.Text, out uint temp))
            {
                GoldPrice_textbox.BackColor = Color.Red;
                return;
            }

            GoldPrice_textbox.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].GoldPrice = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void GPPrice_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }

            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CreditPrice = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void Class_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            string temp = ActiveControl.Text;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Class = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void TopItem_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].TopItem = TopItem_checkbox.Checked;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItems.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Items?", "Remove Items?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
            {
                GameShopInfoList.Remove(_SelectedItems[i]);
            }

            LoadGameShopItems();
            UpdateInterface();
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void DealofDay_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Deal = DealofDay_checkbox.Checked;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void Category_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            string temp = ActiveControl.Text;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Category = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void Stock_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }

            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Stock = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
            //GetStats();
        }

        private void Individual_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].iStock = Individual_checkbox.Checked;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void CredxGold_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!short.TryParse(ActiveControl.Text, out short temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }

            ActiveControl.BackColor = SystemColors.Window;
            Settings.CredxGold = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void Count_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }

            if (temp < 1)
            {
                temp = 1;
                ActiveControl.Text = "1";
            }
            else if (temp > _SelectedItems[0].Info.StackSize)
            {
                temp = _SelectedItems[0].Info.StackSize;
                ActiveControl.Text = _SelectedItems[0].Info.StackSize.ToString();
            }

            ActiveControl.BackColor = SystemColors.Window;
            _SelectedItems[0].Count = temp;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void ClassFilter_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGameShopList();
        }

        private void SectionFilter_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGameShopList();
        }

        private void CategoryFilter_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGameShopList();
        }

        private void ResetFilter_button_Click(object sender, EventArgs e)
        {
            ClassFilter_lb.Text = "All Classes";
            CategoryFilter_lb.Text = "All Categories";
            SectionFilter_lb.Text = "All Items";
            UpdateGameShopList();

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanBuyGold = checkBox2.Checked;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanBuyCredit = checkBox1.Checked;
            if (MotherParent != null)
                MotherParent.NeedSave = true;
        }
    }
}
