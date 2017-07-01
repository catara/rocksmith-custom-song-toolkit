﻿namespace SpotifyAPI.Example
{
    partial class ExampleForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.localControl1 = new SpotifyAPI.Example.LocalControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webControl2 = new SpotifyAPI.Example.WebControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1968, 1350);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.localControl1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Size = new System.Drawing.Size(1952, 1303);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Local-API";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // localControl1
            // 
            this.localControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localControl1.Location = new System.Drawing.Point(6, 6);
            this.localControl1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.localControl1.Name = "localControl1";
            this.localControl1.Size = new System.Drawing.Size(1940, 1291);
            this.localControl1.TabIndex = 0;
            this.localControl1.Load += new System.EventHandler(this.localControl1_Load);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webControl2);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Size = new System.Drawing.Size(1952, 1303);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Web-API";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webControl2
            // 
            this.webControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webControl2.Location = new System.Drawing.Point(6, 6);
            this.webControl2.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.webControl2.Name = "webControl2";
            this.webControl2.Size = new System.Drawing.Size(1940, 1291);
            this.webControl2.TabIndex = 0;
            // 
            // ExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1968, 1350);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ExampleForm";
            this.Text = "SpotifyAPI for usage with bcapi's Rocksmith Custom Song Creator Toolkit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private LocalControl localControl1;
        private WebControl webControl2;
    }
}

