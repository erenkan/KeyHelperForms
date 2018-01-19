using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace KeyHelperForms
{

    public partial class MainForm : Form
    {
        bool startState = false;
        List<bool> checkState;
        KeyThreadArray threadHelperArray;
        ProcessHandler processHelper;

        public MainForm()
        {
            InitializeComponent();
            checkState = Enumerable.Repeat(false, 10).ToList();
            threadHelperArray = new KeyThreadArray();
            processHelper = new ProcessHandler();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            FillListBox();

            
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listView_Characters.Items.Clear(); //Also works as refresh.
            FillListBox();
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            List<CheckBox> eklenecek = new List<CheckBox>();
            foreach (var item in this.Controls)
            {
                if(item.GetType().ToString()== "System.Windows.Forms.CheckBox"&&((CheckBox)item).Checked)
                {
                    eklenecek.Add((CheckBox)item);
                }
            }
            listView_Characters.SelectedItems[0].Tag = new List<CheckBox>(eklenecek);

            threadHelperArray.ChangeChecks(checkState, listView_Characters.SelectedItems[0].SubItems[1].Text);
            threadHelperArray.StartAll();
            if (!startState)
            {
                button_StartStop.Text = "Stop";
                startState = true;
            }
            else
            {
                threadHelperArray.StopAll();
                button_StartStop.Text = "Start";
                startState = false;
            }
        }
        private void ChangeState(CheckBox currentCheckBox, int index)
        {
            if (currentCheckBox.Checked)
            {
                checkState[index] = true;
            }
            else
            {
                checkState[index] = false;
            }
        }
        private void checkBox_key1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key1, 0);
        }

        private void checkBox_key2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key2, 1);
        }

        private void checkBox_key3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key3, 2);
        }

        private void checkBox_key4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key4, 3);
        }

        private void checkBox_key5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key5, 4);
        }

        private void checkBox_key6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key6, 5);
        }

        private void checkBox_key7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key7, 6);
        }
        private void checkBox_key8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key8, 7);
        }

        private void checkBox_key9_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key9, 8);
        }

        private void checkBox_key0_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key0, 9);
        }

        public void FillListBox()
        {
            /* ListView structure -> 0 : name, 1 : pid, 2 : char */
            List<Process> processList = processHelper.GetProcesses();
            foreach (Process process in processList)
            {
                ListViewItem item = new ListViewItem();

                if (process.ProcessName.ToString() == Variables.processName)
                {
                    //TODO: Here is too ugly, simplify it and move it elsewhere.
                    string characterName = processHelper.ReadAddress(process, Variables.characterNameAddress,String.Empty);
                    string currentPid = process.Id.ToString();
                    int hp = processHelper.ReadAddress(process, Variables.hpAddress, new Int32());
                    int mp = processHelper.ReadAddress(process, Variables.mpAddress, new Int32());
                   
                    
                    item.SubItems.Add(process.ProcessName);
                    item.SubItems.Add(characterName);
                    item.SubItems.Add(hp.ToString());
                    item.SubItems.Add(mp.ToString());
                    item.Text = currentPid;  
                    listView_Characters.Items.Add(item);

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = hp;
                    progressBar1.Value = 60;



                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listView_Characters.SelectedItems[0].Tag!=null)
            foreach (var item in (List<CheckBox>)listView_Characters.SelectedItems[0].Tag)
            {
                MessageBox.Show(item.Text);
            }
        }

        private void listView_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control item2 in this.Controls)
            {
                if (item2.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    ((CheckBox)item2).Checked = false;
            }
            if (listView_Characters.SelectedItems.Count>0&&listView_Characters.SelectedItems[0].Tag != null)
            foreach (var item in (List<CheckBox>)listView_Characters.SelectedItems[0].Tag)
            {
                foreach (Control item2 in this.Controls)
                {
                    if (item2.GetType().ToString() == "System.Windows.Forms.CheckBox"&&item.Name==item2.Name)
                    {
                        ((CheckBox)item2).Checked = true;
                    }
                }
            }
        }
    }
}
