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

namespace Server.MirForms.Systems
{
    public partial class MapEXPForm : Form
    {

        public Envir Envir
        {
            get { return SMain.Envir; }
        }

        public Map selectedMap;

        public MapEXPForm()
        {
            InitializeComponent();
            //  On start-up we must enable/disable some controls
            selectedMapLabel.AutoSize = true;
            startBoostBtn.Enabled = false;
            durationBox.Enabled = false;
            expAmountBox.Enabled = false;
            DMGIncreaseBox.Enabled = false;
            //  populate the Combo Box with maps with spawns and monsters MUST be able to drop items
            selectmapCBox.Items.AddRange(Envir.MapList.Where(e => e.Respawns.Count > 0 && !e.Info.NoDropMonster).Cast<object>().ToArray());
        }

        /// <summary>
        /// Ensure we're not doubling the maps up
        /// </summary>
        /// <param name="index">the Maps index</param>
        /// <returns>true or false</returns>
        public bool CheckIfExist(int index)
        {
            //  Ensure the ListBox is valid & populated
            if (selectedMapsBox.Items != null &&
                selectedMapsBox.Items.Count > 0)
            {
                //  Cycle through the ListBox items (Maps)
                for (int i = 0; i < selectedMapsBox.Items.Count; i++)
                {
                    //  Ensure the selected item (Map) is valid
                    if (selectedMapsBox.Items[i] != null)
                    {
                        //  Conver the ListBox item back to a Map
                        Map tempMap = (Map)selectedMapsBox.Items[i];
                        //  Ensure we haven't converted an invalid map and check the index doesn't match
                        if (tempMap == null || tempMap.Info.Index != index)
                            continue;
                        //  Return true if it does exist
                        else
                            return true;
                    }
                }
            }
            return false;
        }

        private void selectmapCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  When the index of the combo box changes, we'll assign the selected index as a map to the selected map variable
            selectedMap = (Map)selectmapCBox.SelectedItem;
        }

        private void expAmountBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(expAmountBox.Text, out byte temp))
            {
                expAmountBox.BackColor = Color.Red;
                startBoostBtn.Enabled = false;
                return;
            }
            expAmountBox.BackColor = SystemColors.Window;
            if (expAmountBox.Text.Length > 0 &&
                durationBox.Text.Length > 0)
                startBoostBtn.Enabled = true;
        }

        private void durationBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!long.TryParse(expAmountBox.Text, out long temp))
            {
                expAmountBox.BackColor = Color.Red;
                startBoostBtn.Enabled = false;
                return;
            }
            expAmountBox.BackColor = SystemColors.Window;
            if (expAmountBox.Text.Length > 0 &&
                durationBox.Text.Length > 0)
                startBoostBtn.Enabled = true;
        }

        private void startBoostBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            if (long.TryParse(durationBox.Text, out long tempLong))
            {
                tempLong = Settings.Minute * tempLong;
                selectedMap.EXPIncreaseDuration = tempLong;
                if (byte.TryParse(expAmountBox.Text, out byte tempByte))
                {
                    selectedMap.EXPIncrease = tempByte;

                    if (byte.TryParse(DMGIncreaseBox.Text, out byte tempByte2))
                        selectedMap.DMGIncrease = tempByte2;

                    for (int m = 0; m < selectedMapsBox.Items.Count; m++)
                    {
                        Map tempMap = (Map)selectedMapsBox.Items[m];
                        if (tempMap == null)
                            return;
                        for (int i = 0; i < Envir.MapList.Count; i++)
                            if (Envir.MapList[i] != null)
                                if (Envir.MapList[i].Info.Index == tempMap.Info.Index)
                                    Envir.MapList[i].SetMapEXPIncrease(tempByte, Envir.Time + tempLong, tempByte2);
                    }
                }
            }
        }

        /// <summary>
        /// Add the selected map in the combo box to the ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addmapBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Ensure the selected map is valid
            if (selectedMap == null)
                return;

            //  Check if the map already exists within the ListBox
            if (CheckIfExist(selectedMap.Info.Index))
                return;
            //  Add the map to the ListBox
            selectedMapsBox.Items.Add(selectedMap);
            //  Check if the ListBox has more than 1 item
            if (selectedMapsBox.Items.Count > 1)
                //  Display how many Maps are going to be effected
                selectedMapLabel.Text = string.Format("{0} maps selected.", selectedMapsBox.Items.Count);
            else
                //  Display the only Map that's going to be effected.
                selectedMapLabel.Text = string.Format("{0} selected", selectedMapsBox.Items[0]);

            //  Now if the item count is more than 0, we'll enable the buttons
            if (selectedMapsBox.Items.Count > 0)
            {
                durationBox.Enabled = true;
                DMGIncreaseBox.Enabled = true;
                expAmountBox.Enabled = true;
            }
        }

        private void DMGIncreaseBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(DMGIncreaseBox.Text, out byte temp))
            {
                DMGIncreaseBox.BackColor = Color.Red;
                startBoostBtn.Enabled = false;
                return;
            }
            DMGIncreaseBox.BackColor = SystemColors.Window;
            if (DMGIncreaseBox.Text.Length > 0 &&
                durationBox.Text.Length > 0)
                startBoostBtn.Enabled = true;
        }
    }
}
