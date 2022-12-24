// Copyright (C) 2020 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	partial class OptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.checkBoxNoForceOffline = new System.Windows.Forms.CheckBox();
			this.checkBoxHttp = new System.Windows.Forms.CheckBox();
			this.checkBoxSetDomain = new System.Windows.Forms.CheckBox();
			this.checkBoxAuthHead = new System.Windows.Forms.CheckBox();
			this.checkBoxCertPin = new System.Windows.Forms.CheckBox();
			this.buttonReset = new System.Windows.Forms.Button();
			this.comboBoxVersion = new System.Windows.Forms.ComboBox();
			this.checkBoxForceVersion = new System.Windows.Forms.CheckBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPagePatchOptions = new System.Windows.Forms.TabPage();
			this.tabPageDomains = new System.Windows.Forms.TabPage();
			this.labelCustomDomains = new System.Windows.Forms.Label();
			this.textBoxDomains = new System.Windows.Forms.TextBox();
			this.tabPageTrayOptions = new System.Windows.Forms.TabPage();
			this.checkBoxTrayMinimize = new System.Windows.Forms.CheckBox();
			this.checkBoxTrayStart = new System.Windows.Forms.CheckBox();
			this.tabControl.SuspendLayout();
			this.tabPagePatchOptions.SuspendLayout();
			this.tabPageDomains.SuspendLayout();
			this.tabPageTrayOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(273, 190);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(73, 32);
			this.buttonSave.TabIndex = 0;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(348, 190);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(0);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(73, 32);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// checkBoxNoForceOffline
			// 
			this.checkBoxNoForceOffline.AutoSize = true;
			this.checkBoxNoForceOffline.Location = new System.Drawing.Point(8, 105);
			this.checkBoxNoForceOffline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxNoForceOffline.Name = "checkBoxNoForceOffline";
			this.checkBoxNoForceOffline.Size = new System.Drawing.Size(241, 21);
			this.checkBoxNoForceOffline.TabIndex = 4;
			this.checkBoxNoForceOffline.Text = "Make dynamic resources optional";
			this.checkBoxNoForceOffline.UseVisualStyleBackColor = true;
			// 
			// checkBoxHttp
			// 
			this.checkBoxHttp.AutoSize = true;
			this.checkBoxHttp.Location = new System.Drawing.Point(8, 80);
			this.checkBoxHttp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxHttp.Name = "checkBoxHttp";
			this.checkBoxHttp.Size = new System.Drawing.Size(184, 21);
			this.checkBoxHttp.TabIndex = 3;
			this.checkBoxHttp.Text = "Use http instead of https";
			this.checkBoxHttp.UseVisualStyleBackColor = true;
			// 
			// checkBoxSetDomain
			// 
			this.checkBoxSetDomain.AutoSize = true;
			this.checkBoxSetDomain.Location = new System.Drawing.Point(8, 55);
			this.checkBoxSetDomain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxSetDomain.Name = "checkBoxSetDomain";
			this.checkBoxSetDomain.Size = new System.Drawing.Size(240, 21);
			this.checkBoxSetDomain.TabIndex = 2;
			this.checkBoxSetDomain.Text = "Set Online_VersionConfigDomain";
			this.checkBoxSetDomain.UseVisualStyleBackColor = true;
			// 
			// checkBoxAuthHead
			// 
			this.checkBoxAuthHead.AutoSize = true;
			this.checkBoxAuthHead.Location = new System.Drawing.Point(8, 30);
			this.checkBoxAuthHead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxAuthHead.Name = "checkBoxAuthHead";
			this.checkBoxAuthHead.Size = new System.Drawing.Size(190, 21);
			this.checkBoxAuthHead.TabIndex = 1;
			this.checkBoxAuthHead.Text = "Always send Auth header";
			this.checkBoxAuthHead.UseVisualStyleBackColor = true;
			// 
			// checkBoxCertPin
			// 
			this.checkBoxCertPin.AutoSize = true;
			this.checkBoxCertPin.Location = new System.Drawing.Point(8, 5);
			this.checkBoxCertPin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxCertPin.Name = "checkBoxCertPin";
			this.checkBoxCertPin.Size = new System.Drawing.Size(155, 21);
			this.checkBoxCertPin.TabIndex = 0;
			this.checkBoxCertPin.Text = "Disable cert pinning";
			this.checkBoxCertPin.UseVisualStyleBackColor = true;
			// 
			// buttonReset
			// 
			this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonReset.Location = new System.Drawing.Point(4, 190);
			this.buttonReset.Margin = new System.Windows.Forms.Padding(0);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(153, 34);
			this.buttonReset.TabIndex = 4;
			this.buttonReset.Text = "Reset defaults";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// comboBoxVersion
			// 
			this.comboBoxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxVersion.Enabled = false;
			this.comboBoxVersion.FormattingEnabled = true;
			this.comboBoxVersion.Location = new System.Drawing.Point(127, 128);
			this.comboBoxVersion.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxVersion.Name = "comboBoxVersion";
			this.comboBoxVersion.Size = new System.Drawing.Size(205, 24);
			this.comboBoxVersion.TabIndex = 5;
			// 
			// checkBoxForceVersion
			// 
			this.checkBoxForceVersion.AutoSize = true;
			this.checkBoxForceVersion.Location = new System.Drawing.Point(8, 130);
			this.checkBoxForceVersion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxForceVersion.Name = "checkBoxForceVersion";
			this.checkBoxForceVersion.Size = new System.Drawing.Size(116, 21);
			this.checkBoxForceVersion.TabIndex = 6;
			this.checkBoxForceVersion.Text = "Force version";
			this.checkBoxForceVersion.UseVisualStyleBackColor = true;
			this.checkBoxForceVersion.CheckedChanged += new System.EventHandler(this.checkBoxForceVersion_CheckedChanged);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPagePatchOptions);
			this.tabControl.Controls.Add(this.tabPageDomains);
			this.tabControl.Controls.Add(this.tabPageTrayOptions);
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(425, 187);
			this.tabControl.TabIndex = 10;
			// 
			// tabPagePatchOptions
			// 
			this.tabPagePatchOptions.Controls.Add(this.checkBoxNoForceOffline);
			this.tabPagePatchOptions.Controls.Add(this.checkBoxCertPin);
			this.tabPagePatchOptions.Controls.Add(this.checkBoxForceVersion);
			this.tabPagePatchOptions.Controls.Add(this.checkBoxHttp);
			this.tabPagePatchOptions.Controls.Add(this.comboBoxVersion);
			this.tabPagePatchOptions.Controls.Add(this.checkBoxAuthHead);
			this.tabPagePatchOptions.Controls.Add(this.checkBoxSetDomain);
			this.tabPagePatchOptions.Location = new System.Drawing.Point(4, 25);
			this.tabPagePatchOptions.Name = "tabPagePatchOptions";
			this.tabPagePatchOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePatchOptions.Size = new System.Drawing.Size(417, 158);
			this.tabPagePatchOptions.TabIndex = 0;
			this.tabPagePatchOptions.Text = "Patch Options";
			this.tabPagePatchOptions.UseVisualStyleBackColor = true;
			// 
			// tabPageDomains
			// 
			this.tabPageDomains.Controls.Add(this.labelCustomDomains);
			this.tabPageDomains.Controls.Add(this.textBoxDomains);
			this.tabPageDomains.Location = new System.Drawing.Point(4, 25);
			this.tabPageDomains.Name = "tabPageDomains";
			this.tabPageDomains.Size = new System.Drawing.Size(417, 158);
			this.tabPageDomains.TabIndex = 2;
			this.tabPageDomains.Text = "Domains";
			this.tabPageDomains.UseVisualStyleBackColor = true;
			// 
			// labelCustomDomains
			// 
			this.labelCustomDomains.AutoSize = true;
			this.labelCustomDomains.Location = new System.Drawing.Point(8, 10);
			this.labelCustomDomains.Name = "labelCustomDomains";
			this.labelCustomDomains.Size = new System.Drawing.Size(281, 17);
			this.labelCustomDomains.TabIndex = 1;
			this.labelCustomDomains.Text = "Enter custom domains below (one per line):";
			// 
			// textBoxDomains
			// 
			this.textBoxDomains.Location = new System.Drawing.Point(8, 32);
			this.textBoxDomains.Multiline = true;
			this.textBoxDomains.Name = "textBoxDomains";
			this.textBoxDomains.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxDomains.Size = new System.Drawing.Size(401, 123);
			this.textBoxDomains.TabIndex = 0;
			this.textBoxDomains.WordWrap = false;
			this.textBoxDomains.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDomains_Validating);
			// 
			// tabPageTrayOptions
			// 
			this.tabPageTrayOptions.Controls.Add(this.checkBoxTrayMinimize);
			this.tabPageTrayOptions.Controls.Add(this.checkBoxTrayStart);
			this.tabPageTrayOptions.Location = new System.Drawing.Point(4, 25);
			this.tabPageTrayOptions.Name = "tabPageTrayOptions";
			this.tabPageTrayOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTrayOptions.Size = new System.Drawing.Size(417, 158);
			this.tabPageTrayOptions.TabIndex = 1;
			this.tabPageTrayOptions.Text = "Tray Options";
			this.tabPageTrayOptions.UseVisualStyleBackColor = true;
			// 
			// checkBoxTrayMinimize
			// 
			this.checkBoxTrayMinimize.AutoSize = true;
			this.checkBoxTrayMinimize.Location = new System.Drawing.Point(8, 30);
			this.checkBoxTrayMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxTrayMinimize.Name = "checkBoxTrayMinimize";
			this.checkBoxTrayMinimize.Size = new System.Drawing.Size(128, 21);
			this.checkBoxTrayMinimize.TabIndex = 5;
			this.checkBoxTrayMinimize.Text = "Minimize to tray";
			this.checkBoxTrayMinimize.UseVisualStyleBackColor = true;
			// 
			// checkBoxTrayStart
			// 
			this.checkBoxTrayStart.AutoSize = true;
			this.checkBoxTrayStart.Location = new System.Drawing.Point(8, 5);
			this.checkBoxTrayStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxTrayStart.Name = "checkBoxTrayStart";
			this.checkBoxTrayStart.Size = new System.Drawing.Size(103, 21);
			this.checkBoxTrayStart.TabIndex = 4;
			this.checkBoxTrayStart.Text = "Start in tray";
			this.checkBoxTrayStart.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(425, 226);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "OptionsForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Advanced Options";
			this.tabControl.ResumeLayout(false);
			this.tabPagePatchOptions.ResumeLayout(false);
			this.tabPagePatchOptions.PerformLayout();
			this.tabPageDomains.ResumeLayout(false);
			this.tabPageDomains.PerformLayout();
			this.tabPageTrayOptions.ResumeLayout(false);
			this.tabPageTrayOptions.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.CheckBox checkBoxAuthHead;
		private System.Windows.Forms.CheckBox checkBoxCertPin;
		private System.Windows.Forms.CheckBox checkBoxSetDomain;
		private System.Windows.Forms.CheckBox checkBoxHttp;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.ComboBox comboBoxVersion;
		private System.Windows.Forms.CheckBox checkBoxForceVersion;
		private System.Windows.Forms.CheckBox checkBoxNoForceOffline;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPagePatchOptions;
		private System.Windows.Forms.TabPage tabPageDomains;
		private System.Windows.Forms.TabPage tabPageTrayOptions;
		private System.Windows.Forms.CheckBox checkBoxTrayMinimize;
		private System.Windows.Forms.CheckBox checkBoxTrayStart;
		private System.Windows.Forms.Label labelCustomDomains;
		private System.Windows.Forms.TextBox textBoxDomains;
	}
}