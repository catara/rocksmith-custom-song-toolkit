﻿namespace RocksmithToolkitGUI.DLCConverter
{
    partial class DLCConverter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAppId = new System.Windows.Forms.Label();
            this.AppIdTB = new RocksmithToolkitGUI.CueTextBox();
            this.appIdCombo = new System.Windows.Forms.ComboBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.platformTargetCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.platformSourceCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.currentOperationLabel = new System.Windows.Forms.Label();
            this.updateProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::RocksmithToolkitGUI.Properties.Resources.brasil_logo;
            this.pictureBox2.Location = new System.Drawing.Point(8, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(75, 75);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAppId);
            this.groupBox1.Controls.Add(this.AppIdTB);
            this.groupBox1.Controls.Add(this.appIdCombo);
            this.groupBox1.Controls.Add(this.convertButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.platformTargetCombo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.platformSourceCombo);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(7, 93);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(382, 125);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Platform";
            // 
            // lblAppId
            // 
            this.lblAppId.AutoSize = true;
            this.lblAppId.ForeColor = System.Drawing.Color.Black;
            this.lblAppId.Location = new System.Drawing.Point(141, 97);
            this.lblAppId.Name = "lblAppId";
            this.lblAppId.Size = new System.Drawing.Size(142, 13);
            this.lblAppId.TabIndex = 51;
            this.lblAppId.Text = "Enter a Custom App ID here:";
            // 
            // AppIdTB
            // 
            this.AppIdTB.Cue = "APP ID";
            this.AppIdTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.AppIdTB.ForeColor = System.Drawing.Color.Gray;
            this.AppIdTB.Location = new System.Drawing.Point(291, 94);
            this.AppIdTB.Name = "AppIdTB";
            this.AppIdTB.Size = new System.Drawing.Size(82, 20);
            this.AppIdTB.TabIndex = 50;
            // 
            // appIdCombo
            // 
            this.appIdCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appIdCombo.FormattingEnabled = true;
            this.appIdCombo.Location = new System.Drawing.Point(54, 68);
            this.appIdCombo.Margin = new System.Windows.Forms.Padding(2);
            this.appIdCombo.Name = "appIdCombo";
            this.appIdCombo.Size = new System.Drawing.Size(319, 21);
            this.appIdCombo.TabIndex = 2;
            this.appIdCombo.SelectedIndexChanged += new System.EventHandler(this.appIdCombo_SelectedIndexChanged);
            // 
            // convertButton
            // 
            this.convertButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.convertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertButton.ForeColor = System.Drawing.Color.Black;
            this.convertButton.Location = new System.Drawing.Point(214, 20);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(159, 29);
            this.convertButton.TabIndex = 3;
            this.convertButton.Text = "Choose CDLC to Convert";
            this.convertButton.UseVisualStyleBackColor = false;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Target:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // platformTargetCombo
            // 
            this.platformTargetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformTargetCombo.FormattingEnabled = true;
            this.platformTargetCombo.Location = new System.Drawing.Point(54, 43);
            this.platformTargetCombo.Margin = new System.Windows.Forms.Padding(2);
            this.platformTargetCombo.Name = "platformTargetCombo";
            this.platformTargetCombo.Size = new System.Drawing.Size(108, 21);
            this.platformTargetCombo.TabIndex = 1;
            this.platformTargetCombo.SelectedIndexChanged += new System.EventHandler(this.platformTargetCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Source:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // platformSourceCombo
            // 
            this.platformSourceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformSourceCombo.FormattingEnabled = true;
            this.platformSourceCombo.Location = new System.Drawing.Point(54, 17);
            this.platformSourceCombo.Margin = new System.Windows.Forms.Padding(2);
            this.platformSourceCombo.Name = "platformSourceCombo";
            this.platformSourceCombo.Size = new System.Drawing.Size(108, 21);
            this.platformSourceCombo.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(111, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Only Compatible with Rocksmith 2014";
            // 
            // currentOperationLabel
            // 
            this.currentOperationLabel.AutoSize = true;
            this.currentOperationLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.currentOperationLabel.Location = new System.Drawing.Point(16, 226);
            this.currentOperationLabel.Name = "currentOperationLabel";
            this.currentOperationLabel.Size = new System.Drawing.Size(16, 13);
            this.currentOperationLabel.TabIndex = 19;
            this.currentOperationLabel.Text = "...";
            this.currentOperationLabel.Visible = false;
            // 
            // updateProgress
            // 
            this.updateProgress.Location = new System.Drawing.Point(19, 245);
            this.updateProgress.Name = "updateProgress";
            this.updateProgress.Size = new System.Drawing.Size(361, 20);
            this.updateProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.updateProgress.TabIndex = 0;
            this.updateProgress.Visible = false;
            // 
            // DLCConverter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.currentOperationLabel);
            this.Controls.Add(this.updateProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox2);
            this.MinimumSize = new System.Drawing.Size(400, 279);
            this.Name = "DLCConverter";
            this.Size = new System.Drawing.Size(400, 280);
            this.Load += new System.EventHandler(this.DLCConverter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox platformTargetCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox platformSourceCombo;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.ComboBox appIdCombo;
        private CueTextBox AppIdTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentOperationLabel;
        private System.Windows.Forms.ProgressBar updateProgress;
        private System.Windows.Forms.Label lblAppId;
    }
}
