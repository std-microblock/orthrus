// Copyright (C) 2020-2022 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HitmanPatcher
{
    public partial class MainForm : Form, ILoggingProvider
    {

        private static readonly Dictionary<string, string> functions = new Dictionary<string, string>
        {
            { "[物品] 解锁所有物品", "items.add-unlockables-stashpoint"},
            {"[物品] 物品数量*100","items.add-unlockables-num" },
            {"[物品] “所有物品”物品","items.all-items" },
            {"[物品] 解锁所有西装","items.add-unlockables-disguise" },
            {"[入口] 解锁所有入口","entrance.unlock-all" },
            {"[计划] 解除计划限制（西装等）","planning.remove-limits" },
        };

        public static readonly Dictionary<string, bool> functionEnabled = new Dictionary<string, bool>
        {
            {"basic.hook-service-addr", true},
            {"items.add-unlockables-stashpoint", true},
            {"items.add-unlockables-num", false},
            {"items.all-items", true},
            {"items.add-unlockables-disguise", true},
            {"entrance.unlock-all", true},
            {"planning.remove-limits", true},
        };

        private static readonly Dictionary<string, string> publicServers = new Dictionary<string, string>
        {
            {"gm.hitmaps.com (Ghost Mode, Roulette)", "gm.hitmaps.com"},
            {"ghostmode.rdil.rocks (Ghost Mode)", "ghostmode.rdil.rocks"}
        };

        private static readonly Dictionary<string, string> publicServersReverse = publicServers.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        private List<string> logs;
        private SynchronizationContext _uiCtx;
        public MainForm()
        {
            _uiCtx = SynchronizationContext.Current;
            InitializeComponent();
            listView1.Columns[0].Width = listView1.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tag = this;
            timer.Tick += (sender, args) => MemoryPatcher.PatchAllProcesses(this, currentSettings.patchOptions);
            timer.Enabled = true;

            try
            {
                currentSettings = Settings.Load();
            }
            catch (Exception)
            {
                currentSettings = new Settings();
            }

            var svr = new WebServer(this);
            svr.Listen();

            log("Patcher ready");

            foreach (var key in functions.Keys.Select((value, i) => new { i, value }))
            {
                checkedFunctions.Items.Add(key.value);
                checkedFunctions.SetItemChecked(key.i, functionEnabled[functions[key.value]]);
            }
        }

        public void log(string msg)
        {
            foreach (string line in msg.Split('\n').Reverse())
            {
                if (listView1.InvokeRequired)
                {
                    _uiCtx.Post(_ =>
                    {
                        listView1.Items.Insert(0, String.Format("[{0:HH:mm:ss}] - {1}", DateTime.Now, line));
                    }, null);
                }
                else
                {
                    listView1.Items.Insert(0, String.Format("[{0:HH:mm:ss}] - {1}", DateTime.Now, line));
                }
            }
        }

        private void buttonRepatch_Click(object sender, EventArgs e)
        {
            MemoryPatcher.patchedprocesses.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentSettings.Save();
        }

        private string getSelectedServerHostname()
        {

            return "localhost:5866";
        }

        private void setSelectedServerHostname(string input)
        {
            string result;

            if (!publicServersReverse.TryGetValue(input, out result))
            {
                result = input;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://gitlab.com/grappigegovert/localghost");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("steam").Length > 0)
            {
                Process.Start("steam://run/863550");
            }
            else
            {
                MessageBox.Show("Please launch steam first, before using this button.");
            }
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm(currentSettings);
            DialogResult result = optionsForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                currentSettings = optionsForm.settings;
            }
        }

        private Settings _currentSettings;
        public Settings currentSettings
        {
            get
            {
                if (_currentSettings.patchOptions.SetCustomConfigDomain)
                {
                    _currentSettings.patchOptions.CustomConfigDomain = getSelectedServerHostname();
                }
                return _currentSettings;
            }
            set
            {
                _currentSettings = value;
                updateTrayDomains();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = listView1.Width - 4 - SystemInformation.VerticalScrollBarWidth;
        }

        private void copyLogToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ListViewItem item in listView1.Items)
            {
                builder.AppendLine(item.Text);
            }
            Clipboard.SetText(builder.ToString());
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;

            this.Focus();
            this.ShowInTaskbar = true;
            trayIcon.Visible = false;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void domainItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                foreach (ToolStripMenuItem item in domainsTrayMenu.DropDownItems)
                {
                    if (item.Text == clickedItem.Text)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }

                setSelectedServerHostname(clickedItem.Text);
            }
        }

        private void updateTrayDomains()
        {
            domainsTrayMenu.DropDownItems.Clear();
            string selectedHostname = getSelectedServerHostname();
            foreach (string domain in publicServers.Values.Concat(currentSettings.domains))
            {
                if (!string.IsNullOrWhiteSpace(domain))
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = domain;
                    item.Click += domainItem_Click;
                    if (domain == selectedHostname)
                    {
                        item.Checked = true;
                    }

                    domainsTrayMenu.DropDownItems.Add(item);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MicroCBer/orthrus");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            checkedFunctions.Visible = !checkedFunctions.Visible;
        }

        private void checkedFunctions_SelectedValueChanged(object sender, EventArgs e)
        {
            functionEnabled[functions[(string)checkedFunctions.Items[checkedFunctions.SelectedIndex]]] =
                checkedFunctions.GetItemChecked(checkedFunctions.SelectedIndex);
        }
    }
}
