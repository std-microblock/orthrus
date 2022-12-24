// Copyright (C) 2020 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HitmanPatcher
{
    public partial class OptionsForm : Form
    {
        private string customDomain;

        public OptionsForm(Settings currentSettings)
        {
            InitializeComponent();
            comboBoxVersion.Items.AddRange(HitmanVersion.Versions.ToArray<object>());
            this.settings = currentSettings;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public Settings settings
        {
            get
            {
                return new Settings()
                {
                    patchOptions = new MemoryPatcher.Options()
                    {
                        DisableCertPinning = checkBoxCertPin.Checked,
                        AlwaysSendAuthHeader = checkBoxAuthHead.Checked,
                        SetCustomConfigDomain = checkBoxSetDomain.Checked,
                        CustomConfigDomain = this.customDomain,
                        UseHttp = checkBoxHttp.Checked,
                        DisableForceOfflineOnFailedDynamicResources = checkBoxNoForceOffline.Checked,
                        ForcedVersion = comboBoxVersion.Text == null ? "" : comboBoxVersion.Text
                    },
                    startInTray = checkBoxTrayStart.Checked,
                    minimizeToTray = checkBoxTrayMinimize.Checked,
                    domains = textBoxDomains.Lines.Where(d => !string.IsNullOrWhiteSpace(d)).ToList()
                };
            }
            private set
            {
                checkBoxCertPin.Checked = value.patchOptions.DisableCertPinning;
                checkBoxAuthHead.Checked = value.patchOptions.AlwaysSendAuthHeader;
                checkBoxSetDomain.Checked = value.patchOptions.SetCustomConfigDomain;
                this.customDomain = value.patchOptions.CustomConfigDomain;
                checkBoxHttp.Checked = value.patchOptions.UseHttp;
                checkBoxNoForceOffline.Checked = value.patchOptions.DisableForceOfflineOnFailedDynamicResources;
                checkBoxForceVersion.Checked = value.patchOptions.ForcedVersion != "";
                comboBoxVersion.Text = value.patchOptions.ForcedVersion;
                checkBoxTrayStart.Checked = value.startInTray;
                checkBoxTrayMinimize.Checked = value.minimizeToTray;
                textBoxDomains.Lines = value.domains.ToArray();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.settings = new Settings();
        }

        private void checkBoxForceVersion_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxForceVersion.Checked)
            {
                comboBoxVersion.Enabled = true;
                comboBoxVersion.SelectedIndex = 0;
            }
            else
            {
                comboBoxVersion.Enabled = false;
                comboBoxVersion.SelectedIndex = -1;
            }
        }

        private void textBoxDomains_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool invalid = false;
            foreach (string line in textBoxDomains.Lines)
            {
                if (line.Length > 160)
                {
                    invalid = true;
                }
            }
            if (invalid)
            {
                MessageBox.Show("One or more domains are more than 160 characters long;"
                                + Environment.NewLine + "Don't.", "Error");
                e.Cancel = true;
            }
        }
    }
}
