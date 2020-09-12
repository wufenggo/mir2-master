using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirDataTool
{
    public partial class ConfigForm : Form
    {
        public double ToolVersion;
        public ConfigForm(double version)
        {
            InitializeComponent();
            ToolVersion = version;
            if (ToolVersion > Settings.ToolVersion)
                Settings.ToolVersion = ToolVersion;
            envirPathBox.Text = Settings.EnvirPath;
            mapPathBox.Text = Settings.MapPath;
            exportPathBox.Text = Settings.ExportPath;
            npcPathBox.Text = Settings.NPCPath;
            questPathBox.Text = Settings.QuestPath;
            versionBox.Text = Settings.DatabaseVersion.ToString();
            cversionBox.Text = Settings.CustomDatabaseVersion.ToString();
            dbPathBox.Text = Settings.DatabasePath;
            dropPathBox.Text = Settings.DropPath;
        }

        private void envirPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.EnvirPath = temp;
                envirPathBox.Text = temp;
            }
        }

        private void mapPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.MapPath = temp;
                mapPathBox.Text = temp;
            }
        }

        private void exportPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.ExportPath = temp;
                exportPathBox.Text = temp;
            }
        }

        private void npcPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.NPCPath = temp;
                npcPathBox.Text = temp;
            }
        }

        private void questPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.QuestPath = temp;
                questPathBox.Text = temp;
            }
        }

        private void versionBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(versionBox.Text, out int val))
            {
                versionBox.BackColor = Color.Red;
                return;
            }
            versionBox.BackColor = SystemColors.Window;
            Settings.DatabaseVersion = val;
        }

        private void cversionBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(versionBox.Text, out int val))
            {
                versionBox.BackColor = Color.Red;
                return;
            }
            versionBox.BackColor = SystemColors.Window;
            Settings.CustomDatabaseVersion = val;
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void questPathBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.QuestPath = temp;
                questPathBox.Text = temp;
            }
        }

        private void npcPathBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.NPCPath = temp;
                npcPathBox.Text = temp;
            }
        }

        private void exportPathBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.ExportPath = temp;
                exportPathBox.Text = temp;
            }
        }

        private void mapPathBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.MapPath = temp;
                mapPathBox.Text = temp;
            }
        }

        private void envirPathBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.EnvirPath = temp;
                envirPathBox.Text = temp;
            }
            
        }

        public string GetDirectoryPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Application.StartupPath;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK &&
                folderBrowserDialog.SelectedPath.Length > 0)
            {
                return folderBrowserDialog.SelectedPath + @"\";
            }
            else
                return "ERROR";
        }

        private void dbPathBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl != sender)
                return;
            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.DatabasePath = temp;
                dbPathBox.Text = temp;
            }
        }

        private void dbPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Maybe possible to read the Database from the server in the end..

            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.DatabasePath = temp;
                dbPathBox.Text = temp;
            }
        }

        private void dropPathBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Maybe possible to read the Database from the server in the end..

            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.DropPath = temp;
                dropPathBox.Text = temp;
            }
        }

        private void dropPathBtn_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            //  Maybe possible to read the Database from the server in the end..

            string temp = GetDirectoryPath();
            if (temp != "ERROR")
            {
                Settings.DropPath = temp;
                dropPathBox.Text = temp;
            }
        }
    }
}
