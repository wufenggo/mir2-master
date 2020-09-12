using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Custom
{
    public partial class AI_Customizer : Form
    {
        #region AttackType Dynamic Controls
        public CheckBox UsePoisonBoxAType;
        public ComboBox PoisonCBoxAtype;
        public Label PoisonDurationLblAType;
        public TextBox PoisonDurationBoxAType;
        public Label PoisonDamageLblAType;
        public TextBox PoisonDamageBoxAType;
        public Label PoisonTickTimeLblAType;
        public TextBox PoisonTickTimeBoxAType;
        #endregion


        #region DebuffType DynamicControls
        public Label DebuffTypeLbl;
        public ComboBox DebuffTypeBox;
        public Label DebuffDurationLbl;
        public TextBox DebuffDurationBox;
        public Label DebuffAmountLbl;
        public TextBox DebuffAmountBox;
        #endregion
        public AI_Customizer()
        {
            InitializeComponent();
        }

        private void strongVsCBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
