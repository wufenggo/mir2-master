using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.MirForms
{
    public partial class RecipeShopForm : Form
    {

        public Envir Envir
        {
            get { return SMain.EditEnvir; }
        }

        
        public RecipeShop _selectedContent;
        public ItemInfo _selectedItem;
        public RecipeShopForm()
        {
            InitializeComponent();
            selectResult.Parent = this;
            selectResult.Hide();
            for (int i = 0; i < Envir.RecipeShopContents.Count; i++)
                listBox1.Items.Add(Envir.RecipeShopContents[i]);
            for (int i = 0; i < Envir.ItemInfoList.Count; i++)
            {
                //  put the Item type here to filter
                if (Envir.ItemInfoList[i].Type == ItemType.Scroll)  //  Just an example, change what to you want
                    pickItemCBox.Items.Add(Envir.ItemInfoList[i]);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //  Create a basic one
            Envir.CreateRecipieShopContent();
            //  Select the last item (I.E The new object added) This will update the form as well.
            _selectedContent = Envir.RecipeShopContents[Envir.RecipeShopContents.Count - 1];
            UpdateInterface(true);
            listBox1.SelectedItem = _selectedContent;
        }

        public void UpdateInterface(bool refresh = false)
        {
            //  Clear the List Box of objects
            if (refresh)
            {
                listBox1.Items.Clear();
                //  Cycle through the Object List
                for (int i = 0; i < Envir.RecipeShopContents.Count; i++)
                    //  Add each object to the List Box
                    listBox1.Items.Add(Envir.RecipeShopContents[i]);
            }
            if (listBox1.SelectedItem != null)
                _selectedContent = (RecipeShop)listBox1.SelectedItem;
            //  Set Boxes to defaults
            itemBox.Text = "";
            priceBox.Text = "";
            //  If an object is Selected
            if (_selectedContent != null)
            {
                //  Set the text boxes with the Objects Values
                if(_selectedContent.ItemBeingSold != null)
                    itemBox.Text = _selectedContent.ItemBeingSold.Name;
                if(_selectedContent.ItemPrice > 0)
                    priceBox.Text = _selectedContent.ItemPrice.ToString();

                if (listBox1.SelectedItem != _selectedItem)
                    listBox1.SelectedItem = _selectedContent;
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface(); 
        }

        private void selectItemBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Can't add if there's nothing selected
            if (_selectedContent == null)
            {
                MessageBox.Show("You must select Content before making edits.", "No Content", MessageBoxButtons.OK);
                return;
            }

            //  Show the Item Picker
            selectResult.Show();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Can't delete something that doesn't exist!
            if (_selectedContent == null)
            {
                MessageBox.Show("You must select Content before making edits.", "No Content", MessageBoxButtons.OK);
                return;
            }
            bool found = false;
            //  Search the Object List
            for (int i = 0; i < Envir.RecipeShopContents.Count; i++)
                //  Is it the same as the selected object?
                if (_selectedContent == Envir.RecipeShopContents[i])
                    //  Yes
                    found = true;
            //  Was the object selected found?
            if (found) 
                //  Yes, delete it (remove)
                Envir.RecipeShopContents.Remove(_selectedContent);
            //  Update the form.
            if (Envir.RecipeShopContents.Count == 0)
                Envir.RecipeIndex = 0;
            UpdateInterface(true);
            //listBox1.SelectedIndex = 0;
        }

        private void priceBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Can't adjust if there's nothing selected
            if (_selectedContent == null)
            {
                MessageBox.Show("You must select Content before making edits.", "No Content", MessageBoxButtons.OK);
                return;
            }
            if (!uint.TryParse(priceBox.Text, out uint tmp))
            {
                //  The input is incorrect so force the back ground color to be red indicating the value is wrong
                priceBox.BackColor = Color.Red;
                //  Discontinue the code to prevent errors
                return;
            }
            //  Incase it was set to Red (Invalid input)
            priceBox.BackColor = SystemColors.Window;
            if (_selectedContent != null)
            {
                for (int i = 0; i < Envir.RecipeShopContents.Count; i++)
                    //  Is it the same Object?
                    if (Envir.RecipeShopContents[i].Index == _selectedContent.Index)
                        //  It is! assign the new value to it
                        Envir.RecipeShopContents[i].ItemPrice = tmp;
                _selectedContent.ItemPrice = tmp;
            }
        }

        private void selectResultItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            //  Can't adjust if there's nothing selected
            if (_selectedContent == null)
            {
                MessageBox.Show("You must select Content before making edits.", "No Content", MessageBoxButtons.OK);
                return;
            }
            if (pickItemCBox.SelectedItem != null)
            {
                ItemInfo tmp = (ItemInfo)pickItemCBox.SelectedItem;
                if (tmp != null)
                {
                    _selectedItem = tmp;
                    for (int i = 0; i < Envir.RecipeShopContents.Count; i++)
                        if (_selectedContent == Envir.RecipeShopContents[i])
                            Envir.RecipeShopContents[i].ItemBeingSold = _selectedItem;
                    selectResult.Hide();
                    UpdateInterface(true);//    Duhh
                }                
            }
        }

        private void RecipeShopForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Envir.SaveDB();
        }

        private void RecipeShopForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                selectResult.Hide();
            }
        }
    }
}
