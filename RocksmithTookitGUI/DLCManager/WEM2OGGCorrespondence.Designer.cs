using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class WEM2OGGCorrespondence
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
            DataGridView1 = new System.Windows.Forms.DataGridView();
            chbx_Save_All = new System.Windows.Forms.CheckBox();
            button8 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            btn_Close = new System.Windows.Forms.Button();
            btn_DecompressAll = new System.Windows.Forms.Button();
            txt_Identifier = new CueTextBox();
            txt_Platform = new CueTextBox();
            txt_ID = new CueTextBox();
            txt_EncryptedID = new CueTextBox();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // DataGridView1
            // 
            DataGridView1.AllowUserToOrderColumns = true;
            DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView1.Location = new System.Drawing.Point(16, 60);
            DataGridView1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            DataGridView1.Name = "DataGridView1";
            DataGridView1.RowHeadersWidth = 61;
            DataGridView1.Size = new System.Drawing.Size(1756, 1046);
            DataGridView1.TabIndex = 38;
            DataGridView1.CellClick += DataGridView1_CellContentClick_1;
            DataGridView1.CellContentDoubleClick += DataGridView1_CellContentClick_1;
            DataGridView1.CellDoubleClick += DataGridView1_CellContentClick_1;
            // 
            // chbx_Save_All
            // 
            chbx_Save_All.AutoSize = true;
            chbx_Save_All.Enabled = false;
            chbx_Save_All.Location = new System.Drawing.Point(2100, 32);
            chbx_Save_All.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            chbx_Save_All.Name = "chbx_Save_All";
            chbx_Save_All.Size = new System.Drawing.Size(73, 36);
            chbx_Save_All.TabIndex = 124;
            chbx_Save_All.Text = "All";
            chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.ForeColor = System.Drawing.Color.Green;
            button8.Location = new System.Drawing.Point(2184, 14);
            button8.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(168, 50);
            button8.TabIndex = 123;
            button8.Text = "Save";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(1868, 10);
            button3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(220, 90);
            button3.TabIndex = 122;
            button3.Text = "Open DB in M$ Access";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button1_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_Close.Location = new System.Drawing.Point(2208, 992);
            btn_Close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(144, 54);
            btn_Close.TabIndex = 273;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // btn_DecompressAll
            // 
            btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_DecompressAll.Location = new System.Drawing.Point(2204, 914);
            btn_DecompressAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_DecompressAll.Name = "btn_DecompressAll";
            btn_DecompressAll.Size = new System.Drawing.Size(148, 86);
            btn_DecompressAll.TabIndex = 276;
            btn_DecompressAll.Text = "Open Main DB";
            btn_DecompressAll.UseVisualStyleBackColor = false;
            btn_DecompressAll.Click += btn_DecompressAll_Click;
            // 
            // txt_Identifier
            // 
            txt_Identifier.Cue = "Identifier";
            txt_Identifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Identifier.ForeColor = System.Drawing.Color.Gray;
            txt_Identifier.Location = new System.Drawing.Point(1908, 160);
            txt_Identifier.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txt_Identifier.Name = "txt_Identifier";
            txt_Identifier.Size = new System.Drawing.Size(440, 32);
            txt_Identifier.TabIndex = 132;
            // 
            // txt_Platform
            // 
            txt_Platform.Cue = "Platform";
            txt_Platform.Enabled = false;
            txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Platform.ForeColor = System.Drawing.Color.Gray;
            txt_Platform.Location = new System.Drawing.Point(1908, 112);
            txt_Platform.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txt_Platform.Name = "txt_Platform";
            txt_Platform.Size = new System.Drawing.Size(440, 32);
            txt_Platform.TabIndex = 131;
            // 
            // txt_ID
            // 
            txt_ID.Cue = "ID";
            txt_ID.Enabled = false;
            txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_ID.ForeColor = System.Drawing.Color.Gray;
            txt_ID.Location = new System.Drawing.Point(1768, 14);
            txt_ID.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new System.Drawing.Size(88, 32);
            txt_ID.TabIndex = 128;
            // 
            // txt_EncryptedID
            // 
            txt_EncryptedID.Cue = "EncryptedID";
            txt_EncryptedID.Enabled = false;
            txt_EncryptedID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_EncryptedID.ForeColor = System.Drawing.Color.Gray;
            txt_EncryptedID.Location = new System.Drawing.Point(1908, 212);
            txt_EncryptedID.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txt_EncryptedID.Name = "txt_EncryptedID";
            txt_EncryptedID.Size = new System.Drawing.Size(440, 32);
            txt_EncryptedID.TabIndex = 126;
            // 
            // WEM2OGGCorrespondence
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(2330, 1230);
            Controls.Add(btn_DecompressAll);
            Controls.Add(btn_Close);
            Controls.Add(txt_Identifier);
            Controls.Add(txt_Platform);
            Controls.Add(DataGridView1);
            Controls.Add(chbx_Save_All);
            Controls.Add(button8);
            Controls.Add(txt_ID);
            Controls.Add(button3);
            Controls.Add(txt_EncryptedID);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "WEM2OGGCorrespondence";
            Text = "WEM 2 OGG Corespondence DB";
            Load += Standardization_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button3;
        private CueTextBox txt_ID;
        private CueTextBox txt_EncryptedID;
        private CueTextBox txt_Identifier;
        private CueTextBox txt_Platform;
        private UtilitiesFunctions.MainDBfields filed;
        private DLCPackageData datas;
        private string author;
        private string tkversion;
        private string dD;
        private string bass;
        private string guitar;
        private string combo;
        private string rhythm;
        private string lead;
        private string tunnings;
        private int i;
        private int norows;
        private string original_FileName;
        private string art_hash;
        private string audio_hash;
        private string audioPreview_hash;
        private List<string> alist;
        private List<string> blist;

        public WEM2OGGCorrespondence()
        {
            //this.filed = filed;
            //datas = datas;
            //this.author = author;
            //this.tkversion = tkversion;
            //this.dD = dD;
            //this.bass = bass;
            //this.guitar = guitar;
            //this.combo = combo;
            //this.rhythm = rhythm;
            //this.lead = lead;
            //this.tunnings = tunnings;
            //this.i = i;
            //this.norows = norows;
            //this.original_FileName = original_FileName;
            //this.art_hash = art_hash;
            //this.audio_hash = audio_hash;
            //this.audioPreview_hash = audioPreview_hash;
            //this.alist = alist;
            //this.blist = blist;
        }

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_DecompressAll;
    }
}