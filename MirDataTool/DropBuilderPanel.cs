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
    public partial class DropBuilderPanel : UserControl
    {
        MirDataTool MotherParent;
        public DropBuilderPanel()
        {
            InitializeComponent();
        }
        
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
        }

        public void UpdateList()
        {
            mobListBox.Items.Clear();
            itemListBox.Items.Clear();
            if (MotherParent != null)
            {
                if (MotherParent.ItemPanel != null &&
                    MotherParent.ItemPanel.ItemInfoList != null &&
                    MotherParent.ItemPanel.ItemInfoList.Count > 0)
                {
                    for (int i = 0; i < MotherParent.ItemPanel.ItemInfoList.Count; i++)
                        itemListBox.Items.Add(MotherParent.ItemPanel.ItemInfoList[i]);
                }
                if (MotherParent.MonsterPanel != null &&
                    MotherParent.MonsterPanel.MonsterInfoList != null &&
                    MotherParent.MonsterPanel.MonsterInfoList.Count > 0)
                {
                    for (int i = 0; i < MotherParent.MonsterPanel.MonsterInfoList.Count; i++)
                        mobListBox.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);
                }
            }
        }
    }
}
