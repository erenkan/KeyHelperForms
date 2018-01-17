﻿using System;
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
            threadHelperArray.ChangeChecks(checkState);
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

        private void btnOffset_Click(object sender, EventArgs e)
        {

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
                    string hp = processHelper.ReadAddress(process, Variables.hpAddress, new Int32());
                    string mp = processHelper.ReadAddress(process, Variables.mpAddress, new Int32());
                    item.SubItems.Add(process.ProcessName);
                    item.SubItems.Add(characterName);
                    item.SubItems.Add(hp);
                    item.SubItems.Add(mp);
                    item.Text = currentPid;  
                    listView_Characters.Items.Add(item);
                }
            }
        }
    }
}
