namespace RocksmithToolkitGUI.DLCManager
{
    partial class TonesDB
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
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txt_lastConversionDateTime = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_AmpPedalKey = new System.Windows.Forms.ComboBox();
            this.chbx_AmpKnobValues = new System.Windows.Forms.ComboBox();
            this.chbx_AmpCategory = new System.Windows.Forms.ComboBox();
            this.chbx_AmpType = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetType = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetPedalKey = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetKnobValues = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetCategory = new System.Windows.Forms.ComboBox();
            this.txt_Keyy = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Volume = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Custom = new System.Windows.Forms.CheckBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CDLC_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Arrangement_Name = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Save_All = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(1174, 149);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.txt_lastConversionDateTime);
            this.Panel1.Controls.Add(this.chbx_AmpPedalKey);
            this.Panel1.Controls.Add(this.chbx_AmpKnobValues);
            this.Panel1.Controls.Add(this.chbx_AmpCategory);
            this.Panel1.Controls.Add(this.chbx_AmpType);
            this.Panel1.Controls.Add(this.chbx_CabinetType);
            this.Panel1.Controls.Add(this.chbx_CabinetPedalKey);
            this.Panel1.Controls.Add(this.chbx_CabinetKnobValues);
            this.Panel1.Controls.Add(this.chbx_CabinetCategory);
            this.Panel1.Controls.Add(this.txt_Keyy);
            this.Panel1.Controls.Add(this.txt_Volume);
            this.Panel1.Controls.Add(this.chbx_Custom);
            this.Panel1.Controls.Add(this.txt_ID);
            this.Panel1.Controls.Add(this.txt_CDLC_ID);
            this.Panel1.Controls.Add(this.txt_Arrangement_Name);
            this.Panel1.Controls.Add(this.chbx_Save_All);
            this.Panel1.Controls.Add(this.button8);
            this.Panel1.Controls.Add(this.button3);
            this.Panel1.Controls.Add(this.chbx_Broken);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 155);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1174, 114);
            this.Panel1.TabIndex = 41;
            // 
            // txt_lastConversionDateTime
            // 
            this.txt_lastConversionDateTime.Cue = "lastConversionDateTime";
            this.txt_lastConversionDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_lastConversionDateTime.ForeColor = System.Drawing.Color.Gray;
            this.txt_lastConversionDateTime.Location = new System.Drawing.Point(374, 32);
            this.txt_lastConversionDateTime.Name = "txt_lastConversionDateTime";
            this.txt_lastConversionDateTime.Size = new System.Drawing.Size(113, 20);
            this.txt_lastConversionDateTime.TabIndex = 139;
            // 
            // chbx_AmpPedalKey
            // 
            this.chbx_AmpPedalKey.FormattingEnabled = true;
            this.chbx_AmpPedalKey.Location = new System.Drawing.Point(527, 87);
            this.chbx_AmpPedalKey.Name = "chbx_AmpPedalKey";
            this.chbx_AmpPedalKey.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpPedalKey.TabIndex = 138;
            // 
            // chbx_AmpKnobValues
            // 
            this.chbx_AmpKnobValues.FormattingEnabled = true;
            this.chbx_AmpKnobValues.Location = new System.Drawing.Point(527, 60);
            this.chbx_AmpKnobValues.Name = "chbx_AmpKnobValues";
            this.chbx_AmpKnobValues.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpKnobValues.TabIndex = 137;
            // 
            // chbx_AmpCategory
            // 
            this.chbx_AmpCategory.FormattingEnabled = true;
            this.chbx_AmpCategory.Location = new System.Drawing.Point(527, 33);
            this.chbx_AmpCategory.Name = "chbx_AmpCategory";
            this.chbx_AmpCategory.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpCategory.TabIndex = 136;
            // 
            // chbx_AmpType
            // 
            this.chbx_AmpType.FormattingEnabled = true;
            this.chbx_AmpType.Location = new System.Drawing.Point(527, 6);
            this.chbx_AmpType.Name = "chbx_AmpType";
            this.chbx_AmpType.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpType.TabIndex = 135;
            // 
            // chbx_CabinetType
            // 
            this.chbx_CabinetType.FormattingEnabled = true;
            this.chbx_CabinetType.Location = new System.Drawing.Point(745, 86);
            this.chbx_CabinetType.Name = "chbx_CabinetType";
            this.chbx_CabinetType.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetType.TabIndex = 134;
            // 
            // chbx_CabinetPedalKey
            // 
            this.chbx_CabinetPedalKey.FormattingEnabled = true;
            this.chbx_CabinetPedalKey.Location = new System.Drawing.Point(745, 59);
            this.chbx_CabinetPedalKey.Name = "chbx_CabinetPedalKey";
            this.chbx_CabinetPedalKey.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetPedalKey.TabIndex = 133;
            // 
            // chbx_CabinetKnobValues
            // 
            this.chbx_CabinetKnobValues.FormattingEnabled = true;
            this.chbx_CabinetKnobValues.Location = new System.Drawing.Point(745, 32);
            this.chbx_CabinetKnobValues.Name = "chbx_CabinetKnobValues";
            this.chbx_CabinetKnobValues.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetKnobValues.TabIndex = 132;
            // 
            // chbx_CabinetCategory
            // 
            this.chbx_CabinetCategory.FormattingEnabled = true;
            this.chbx_CabinetCategory.Location = new System.Drawing.Point(745, 5);
            this.chbx_CabinetCategory.Name = "chbx_CabinetCategory";
            this.chbx_CabinetCategory.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetCategory.TabIndex = 131;
            // 
            // txt_Keyy
            // 
            this.txt_Keyy.Cue = "Key";
            this.txt_Keyy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Keyy.ForeColor = System.Drawing.Color.Gray;
            this.txt_Keyy.Location = new System.Drawing.Point(265, 45);
            this.txt_Keyy.Name = "txt_Keyy";
            this.txt_Keyy.Size = new System.Drawing.Size(56, 20);
            this.txt_Keyy.TabIndex = 130;
            // 
            // txt_Volume
            // 
            this.txt_Volume.Cue = "CDLC_ID";
            this.txt_Volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Volume.ForeColor = System.Drawing.Color.Gray;
            this.txt_Volume.Location = new System.Drawing.Point(265, 65);
            this.txt_Volume.Name = "txt_Volume";
            this.txt_Volume.Size = new System.Drawing.Size(56, 20);
            this.txt_Volume.TabIndex = 129;
            // 
            // chbx_Custom
            // 
            this.chbx_Custom.AutoSize = true;
            this.chbx_Custom.Enabled = false;
            this.chbx_Custom.Location = new System.Drawing.Point(903, 64);
            this.chbx_Custom.Name = "chbx_Custom";
            this.chbx_Custom.Size = new System.Drawing.Size(61, 17);
            this.chbx_Custom.TabIndex = 128;
            this.chbx_Custom.Text = "Custom";
            this.chbx_Custom.UseVisualStyleBackColor = true;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(265, 90);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 127;
            // 
            // txt_CDLC_ID
            // 
            this.txt_CDLC_ID.Cue = "CDLC_ID";
            this.txt_CDLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CDLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_CDLC_ID.Location = new System.Drawing.Point(265, 23);
            this.txt_CDLC_ID.Name = "txt_CDLC_ID";
            this.txt_CDLC_ID.Size = new System.Drawing.Size(56, 20);
            this.txt_CDLC_ID.TabIndex = 126;
            // 
            // txt_Arrangement_Name
            // 
            this.txt_Arrangement_Name.Cue = "Arrangement Name";
            this.txt_Arrangement_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Arrangement_Name.ForeColor = System.Drawing.Color.Gray;
            this.txt_Arrangement_Name.Location = new System.Drawing.Point(265, 6);
            this.txt_Arrangement_Name.Name = "txt_Arrangement_Name";
            this.txt_Arrangement_Name.Size = new System.Drawing.Size(222, 20);
            this.txt_Arrangement_Name.TabIndex = 125;
            // 
            // chbx_Save_All
            // 
            this.chbx_Save_All.AutoSize = true;
            this.chbx_Save_All.Enabled = false;
            this.chbx_Save_All.Location = new System.Drawing.Point(994, 64);
            this.chbx_Save_All.Name = "chbx_Save_All";
            this.chbx_Save_All.Size = new System.Drawing.Size(37, 17);
            this.chbx_Save_All.TabIndex = 124;
            this.chbx_Save_All.Text = "All";
            this.chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(994, 33);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 26);
            this.button8.TabIndex = 123;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(125, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 35);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Enabled = false;
            this.chbx_Broken.Location = new System.Drawing.Point(903, 49);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 121;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Location = new System.Drawing.Point(-153, 96);
            this.CheckBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(139, 17);
            this.CheckBox1.TabIndex = 34;
            this.CheckBox1.Text = "Show only MessageBox";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1006, 8);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // TonesDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 269);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "TonesDB";
            this.Text = "TonesDB";
            this.Load += new System.EventHandler(this.TonesDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chbx_Broken;
        private CueTextBox txt_Keyy;
        private CueTextBox txt_Volume;
        private System.Windows.Forms.CheckBox chbx_Custom;
        private CueTextBox txt_ID;
        private CueTextBox txt_CDLC_ID;
        private CueTextBox txt_Arrangement_Name;
        private System.Windows.Forms.ComboBox chbx_AmpPedalKey;
        private System.Windows.Forms.ComboBox chbx_AmpKnobValues;
        private System.Windows.Forms.ComboBox chbx_AmpCategory;
        private System.Windows.Forms.ComboBox chbx_AmpType;
        private System.Windows.Forms.ComboBox chbx_CabinetType;
        private System.Windows.Forms.ComboBox chbx_CabinetPedalKey;
        private System.Windows.Forms.ComboBox chbx_CabinetKnobValues;
        private System.Windows.Forms.ComboBox chbx_CabinetCategory;
        private CueTextBox txt_lastConversionDateTime;
        private System.Windows.Forms.Button btn_Close;
    }
}