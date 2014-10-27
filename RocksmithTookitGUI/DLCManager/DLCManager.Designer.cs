using System;
using System.Data;
using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class DLCManagerWindow
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txt_RocksmithDLCPath = new System.Windows.Forms.TextBox();
            this.txt_TempPath = new System.Windows.Forms.TextBox();
            this.chbx_CleanFolder = new System.Windows.Forms.CheckBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.chbx_CleanDB = new System.Windows.Forms.CheckBox();
            this.chbx_RS2012DLC = new System.Windows.Forms.CheckBox();
            this.lbl_StatisticsOnReadDLCs = new System.Windows.Forms.Label();
            this.rtxt_StatisticsOnReadDLCs = new System.Windows.Forms.RichTextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.btn_PopulateDB = new System.Windows.Forms.Button();
            this.btn_DecompressAll = new System.Windows.Forms.Button();
            this.btn_ReadFolder = new System.Windows.Forms.Button();
            this.lbl_RocksmithDLCPath = new System.Windows.Forms.Label();
            this.btn_TempPath = new System.Windows.Forms.Button();
            this.lbl_TempFolders = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_DBFolder = new System.Windows.Forms.TextBox();
            this.btn_DBFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chbx_Debug = new System.Windows.Forms.CheckBox();
            this.chbx_DebugB = new System.Windows.Forms.CheckBox();
            this.pnl_ApplyChanges = new System.Windows.Forms.Panel();
            this.rbtn_Xbox = new System.Windows.Forms.RadioButton();
            this.rbtn_Mac = new System.Windows.Forms.RadioButton();
            this.rbtn_PS3 = new System.Windows.Forms.RadioButton();
            this.rbtn_All = new System.Windows.Forms.RadioButton();
            this.grpbx_ApplyChanges = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbx_Add_Album = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Track = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Year = new System.Windows.Forms.CheckBox();
            this.btn_RePack = new System.Windows.Forms.Button();
            this.chbx_Normalize = new System.Windows.Forms.CheckBox();
            this.lbl_PreviewText = new System.Windows.Forms.Label();
            this.grpbx_Title_Normalization = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.chbx_Add_DD = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Beta = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Group = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Alternate_Version = new System.Windows.Forms.CheckBox();
            this.chbx_Add_MultiTrack_Details = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Avail_Instrumets = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Rating = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Description = new System.Windows.Forms.CheckBox();
            this.chbx_Add_Comments = new System.Windows.Forms.CheckBox();
            this.chbx_Add_CDCL = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtn_Normal = new System.Windows.Forms.RadioButton();
            this.rbnt_Sort = new System.Windows.Forms.RadioButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtn_Population_Groups = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_All = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_Selected = new System.Windows.Forms.RadioButton();
            this.txt_Title_Sort = new System.Windows.Forms.TextBox();
            this.cbx_Title_Sort = new System.Windows.Forms.ComboBox();
            this.cbx_Album = new System.Windows.Forms.ComboBox();
            this.txt_Album = new System.Windows.Forms.TextBox();
            this.cbx_File_Name = new System.Windows.Forms.ComboBox();
            this.txt_File_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.cbx_Title = new System.Windows.Forms.ComboBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbx_Artist = new System.Windows.Forms.ComboBox();
            this.txt_Artist = new System.Windows.Forms.TextBox();
            this.cbx_Activ_Title = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Title_Sort = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Artist = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Album = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_File_Name = new System.Windows.Forms.CheckBox();
            this.btn_Preview_Title = new System.Windows.Forms.Button();
            this.btn_Preview_Title_Sort = new System.Windows.Forms.Button();
            this.btn_Preview_Artist = new System.Windows.Forms.Button();
            this.btn_Preview_Album = new System.Windows.Forms.Button();
            this.btn_Preview_File_Name = new System.Windows.Forms.Button();
            this.btn_Preview_Artist_Sort = new System.Windows.Forms.Button();
            this.cbx_Activ_Artist_Sort = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_Artist_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Artist_Sort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chbx_Additional_Manipualtions = new System.Windows.Forms.CheckedListBox();
            this.cbx_Groups = new System.Windows.Forms.ComboBox();
            this.mainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filesDataSet = new RocksmithToolkitGUI.FilesDataSet();
            this.mainTableAdapter = new RocksmithToolkitGUI.FilesDataSetTableAdapters.MainTableAdapter();
            this.btn_Cleanup_MainDB = new System.Windows.Forms.Button();
            this.chbx_Skip_PC = new System.Windows.Forms.CheckBox();
            this.chbx_Skip_ORIG = new System.Windows.Forms.CheckBox();
            this.chbx_Rebuild = new System.Windows.Forms.CheckBox();
            this.Export_To = new System.Windows.Forms.Button();
            this.cbx_Export = new System.Windows.Forms.ComboBox();
            this.pnl_ApplyChanges.SuspendLayout();
            this.grpbx_ApplyChanges.SuspendLayout();
            this.grpbx_Title_Normalization.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_RocksmithDLCPath
            // 
            this.txt_RocksmithDLCPath.Location = new System.Drawing.Point(115, 2);
            this.txt_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RocksmithDLCPath.Name = "txt_RocksmithDLCPath";
            this.txt_RocksmithDLCPath.Size = new System.Drawing.Size(182, 20);
            this.txt_RocksmithDLCPath.TabIndex = 192;
            // 
            // txt_TempPath
            // 
            this.txt_TempPath.Location = new System.Drawing.Point(115, 26);
            this.txt_TempPath.Margin = new System.Windows.Forms.Padding(2);
            this.txt_TempPath.Name = "txt_TempPath";
            this.txt_TempPath.Size = new System.Drawing.Size(182, 20);
            this.txt_TempPath.TabIndex = 191;
            // 
            // chbx_CleanFolder
            // 
            this.chbx_CleanFolder.AutoSize = true;
            this.chbx_CleanFolder.Checked = true;
            this.chbx_CleanFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CleanFolder.Location = new System.Drawing.Point(321, 28);
            this.chbx_CleanFolder.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_CleanFolder.Name = "chbx_CleanFolder";
            this.chbx_CleanFolder.Size = new System.Drawing.Size(85, 17);
            this.chbx_CleanFolder.TabIndex = 190;
            this.chbx_CleanFolder.Text = "Clean Folder";
            this.chbx_CleanFolder.UseVisualStyleBackColor = true;
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(297, 4);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_SteamDLCFolder.TabIndex = 187;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // chbx_CleanDB
            // 
            this.chbx_CleanDB.AutoSize = true;
            this.chbx_CleanDB.Checked = true;
            this.chbx_CleanDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CleanDB.Location = new System.Drawing.Point(321, 67);
            this.chbx_CleanDB.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_CleanDB.Name = "chbx_CleanDB";
            this.chbx_CleanDB.Size = new System.Drawing.Size(71, 17);
            this.chbx_CleanDB.TabIndex = 186;
            this.chbx_CleanDB.Text = "Clean DB";
            this.chbx_CleanDB.UseVisualStyleBackColor = true;
            this.chbx_CleanDB.CheckedChanged += new System.EventHandler(this.chbx_CleanDB_CheckedChanged);
            // 
            // chbx_RS2012DLC
            // 
            this.chbx_RS2012DLC.AutoSize = true;
            this.chbx_RS2012DLC.Enabled = false;
            this.chbx_RS2012DLC.Location = new System.Drawing.Point(361, 542);
            this.chbx_RS2012DLC.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_RS2012DLC.Name = "chbx_RS2012DLC";
            this.chbx_RS2012DLC.Size = new System.Drawing.Size(181, 17);
            this.chbx_RS2012DLC.TabIndex = 185;
            this.chbx_RS2012DLC.Text = "Package the 2012 DLCs, as well";
            this.chbx_RS2012DLC.UseVisualStyleBackColor = true;
            // 
            // lbl_StatisticsOnReadDLCs
            // 
            this.lbl_StatisticsOnReadDLCs.AutoSize = true;
            this.lbl_StatisticsOnReadDLCs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_StatisticsOnReadDLCs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_StatisticsOnReadDLCs.Location = new System.Drawing.Point(-18, 536);
            this.lbl_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_StatisticsOnReadDLCs.Name = "lbl_StatisticsOnReadDLCs";
            this.lbl_StatisticsOnReadDLCs.Size = new System.Drawing.Size(139, 13);
            this.lbl_StatisticsOnReadDLCs.TabIndex = 184;
            this.lbl_StatisticsOnReadDLCs.Text = "Statistics on Read PC DLCs";
            this.lbl_StatisticsOnReadDLCs.Click += new System.EventHandler(this.lbl_StatisticsOnReadDLCs_Click);
            // 
            // rtxt_StatisticsOnReadDLCs
            // 
            this.rtxt_StatisticsOnReadDLCs.Location = new System.Drawing.Point(5, 405);
            this.rtxt_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.rtxt_StatisticsOnReadDLCs.Name = "rtxt_StatisticsOnReadDLCs";
            this.rtxt_StatisticsOnReadDLCs.Size = new System.Drawing.Size(518, 94);
            this.rtxt_StatisticsOnReadDLCs.TabIndex = 183;
            this.rtxt_StatisticsOnReadDLCs.Text = "";
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(5, 381);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(517, 20);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 182;
            // 
            // btn_PopulateDB
            // 
            this.btn_PopulateDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_PopulateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PopulateDB.Location = new System.Drawing.Point(460, 38);
            this.btn_PopulateDB.Margin = new System.Windows.Forms.Padding(2);
            this.btn_PopulateDB.Name = "btn_PopulateDB";
            this.btn_PopulateDB.Size = new System.Drawing.Size(73, 25);
            this.btn_PopulateDB.TabIndex = 181;
            this.btn_PopulateDB.Text = "Import";
            this.btn_PopulateDB.UseVisualStyleBackColor = false;
            this.btn_PopulateDB.Click += new System.EventHandler(this.btn_PopulateDB_Click);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(460, 2);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(73, 35);
            this.btn_DecompressAll.TabIndex = 180;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click_1);
            // 
            // btn_ReadFolder
            // 
            this.btn_ReadFolder.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_ReadFolder.Enabled = false;
            this.btn_ReadFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadFolder.Location = new System.Drawing.Point(437, 591);
            this.btn_ReadFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ReadFolder.Name = "btn_ReadFolder";
            this.btn_ReadFolder.Size = new System.Drawing.Size(77, 19);
            this.btn_ReadFolder.TabIndex = 179;
            this.btn_ReadFolder.Text = "Read & Decompress";
            this.btn_ReadFolder.UseVisualStyleBackColor = false;
            this.btn_ReadFolder.Click += new System.EventHandler(this.btn_ReadFolder_Click_1);
            // 
            // lbl_RocksmithDLCPath
            // 
            this.lbl_RocksmithDLCPath.AutoSize = true;
            this.lbl_RocksmithDLCPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_RocksmithDLCPath.Location = new System.Drawing.Point(11, 9);
            this.lbl_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_RocksmithDLCPath.Name = "lbl_RocksmithDLCPath";
            this.lbl_RocksmithDLCPath.Size = new System.Drawing.Size(101, 13);
            this.lbl_RocksmithDLCPath.TabIndex = 178;
            this.lbl_RocksmithDLCPath.Text = "Importing DLC path:";
            // 
            // btn_TempPath
            // 
            this.btn_TempPath.Location = new System.Drawing.Point(297, 28);
            this.btn_TempPath.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TempPath.Name = "btn_TempPath";
            this.btn_TempPath.Size = new System.Drawing.Size(22, 15);
            this.btn_TempPath.TabIndex = 175;
            this.btn_TempPath.Text = "...";
            this.btn_TempPath.UseVisualStyleBackColor = true;
            this.btn_TempPath.Click += new System.EventHandler(this.btn_TempPath_Click_1);
            // 
            // lbl_TempFolders
            // 
            this.lbl_TempFolders.AutoSize = true;
            this.lbl_TempFolders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TempFolders.Location = new System.Drawing.Point(11, 33);
            this.lbl_TempFolders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TempFolders.Name = "lbl_TempFolders";
            this.lbl_TempFolders.Size = new System.Drawing.Size(94, 13);
            this.lbl_TempFolders.TabIndex = 176;
            this.lbl_TempFolders.Text = "Temp Folder Path:";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(460, 65);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
            this.btn_Close.TabIndex = 193;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_DBFolder
            // 
            this.txt_DBFolder.Location = new System.Drawing.Point(115, 65);
            this.txt_DBFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txt_DBFolder.Name = "txt_DBFolder";
            this.txt_DBFolder.Size = new System.Drawing.Size(182, 20);
            this.txt_DBFolder.TabIndex = 197;
            // 
            // btn_DBFolder
            // 
            this.btn_DBFolder.Location = new System.Drawing.Point(297, 67);
            this.btn_DBFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DBFolder.Name = "btn_DBFolder";
            this.btn_DBFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_DBFolder.TabIndex = 194;
            this.btn_DBFolder.Text = "...";
            this.btn_DBFolder.UseVisualStyleBackColor = true;
            this.btn_DBFolder.Click += new System.EventHandler(this.btn_DBFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(11, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 195;
            this.label1.Text = "DB Folder Path";
            // 
            // chbx_Debug
            // 
            this.chbx_Debug.AutoSize = true;
            this.chbx_Debug.Location = new System.Drawing.Point(-15, -15);
            this.chbx_Debug.MaximumSize = new System.Drawing.Size(55, 55);
            this.chbx_Debug.MinimumSize = new System.Drawing.Size(3, 3);
            this.chbx_Debug.Name = "chbx_Debug";
            this.chbx_Debug.Size = new System.Drawing.Size(55, 17);
            this.chbx_Debug.TabIndex = 198;
            this.chbx_Debug.Text = "Debug";
            this.chbx_Debug.UseVisualStyleBackColor = true;
            // 
            // chbx_DebugB
            // 
            this.chbx_DebugB.AutoSize = true;
            this.chbx_DebugB.Checked = true;
            this.chbx_DebugB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_DebugB.Location = new System.Drawing.Point(403, 69);
            this.chbx_DebugB.Name = "chbx_DebugB";
            this.chbx_DebugB.Size = new System.Drawing.Size(58, 17);
            this.chbx_DebugB.TabIndex = 199;
            this.chbx_DebugB.Text = "Debug";
            this.chbx_DebugB.UseVisualStyleBackColor = true;
            this.chbx_DebugB.CheckedChanged += new System.EventHandler(this.chbx_DebugB_CheckedChanged);
            // 
            // pnl_ApplyChanges
            // 
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_Xbox);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_Mac);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_PS3);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_All);
            this.pnl_ApplyChanges.Location = new System.Drawing.Point(3, 14);
            this.pnl_ApplyChanges.Name = "pnl_ApplyChanges";
            this.pnl_ApplyChanges.Size = new System.Drawing.Size(225, 25);
            this.pnl_ApplyChanges.TabIndex = 204;
            // 
            // rbtn_Xbox
            // 
            this.rbtn_Xbox.AutoSize = true;
            this.rbtn_Xbox.Location = new System.Drawing.Point(153, 3);
            this.rbtn_Xbox.Name = "rbtn_Xbox";
            this.rbtn_Xbox.Size = new System.Drawing.Size(68, 17);
            this.rbtn_Xbox.TabIndex = 207;
            this.rbtn_Xbox.Text = "XBox360";
            this.rbtn_Xbox.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mac
            // 
            this.rbtn_Mac.AutoSize = true;
            this.rbtn_Mac.Location = new System.Drawing.Point(112, 3);
            this.rbtn_Mac.Name = "rbtn_Mac";
            this.rbtn_Mac.Size = new System.Drawing.Size(46, 17);
            this.rbtn_Mac.TabIndex = 206;
            this.rbtn_Mac.Text = "Mac";
            this.rbtn_Mac.UseVisualStyleBackColor = true;
            // 
            // rbtn_PS3
            // 
            this.rbtn_PS3.AutoSize = true;
            this.rbtn_PS3.Checked = true;
            this.rbtn_PS3.Location = new System.Drawing.Point(69, 3);
            this.rbtn_PS3.Name = "rbtn_PS3";
            this.rbtn_PS3.Size = new System.Drawing.Size(45, 17);
            this.rbtn_PS3.TabIndex = 205;
            this.rbtn_PS3.TabStop = true;
            this.rbtn_PS3.Text = "PS3";
            this.rbtn_PS3.UseVisualStyleBackColor = true;
            // 
            // rbtn_All
            // 
            this.rbtn_All.AutoSize = true;
            this.rbtn_All.Location = new System.Drawing.Point(3, 3);
            this.rbtn_All.Name = "rbtn_All";
            this.rbtn_All.Size = new System.Drawing.Size(70, 17);
            this.rbtn_All.TabIndex = 204;
            this.rbtn_All.Text = "All Others";
            this.rbtn_All.UseVisualStyleBackColor = true;
            // 
            // grpbx_ApplyChanges
            // 
            this.grpbx_ApplyChanges.Controls.Add(this.pnl_ApplyChanges);
            this.grpbx_ApplyChanges.Enabled = false;
            this.grpbx_ApplyChanges.Location = new System.Drawing.Point(4, 85);
            this.grpbx_ApplyChanges.Name = "grpbx_ApplyChanges";
            this.grpbx_ApplyChanges.Size = new System.Drawing.Size(243, 42);
            this.grpbx_ApplyChanges.TabIndex = 205;
            this.grpbx_ApplyChanges.TabStop = false;
            this.grpbx_ApplyChanges.Text = "RePack format";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 13);
            this.label2.TabIndex = 206;
            this.label2.Text = "Will be used store decompressed version of all DLCs and the original version";
            // 
            // chbx_Add_Album
            // 
            this.chbx_Add_Album.AutoSize = true;
            this.chbx_Add_Album.Location = new System.Drawing.Point(6, 18);
            this.chbx_Add_Album.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Album.Name = "chbx_Add_Album";
            this.chbx_Add_Album.Size = new System.Drawing.Size(77, 17);
            this.chbx_Add_Album.TabIndex = 207;
            this.chbx_Add_Album.Text = "Add Album";
            this.chbx_Add_Album.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Track
            // 
            this.chbx_Add_Track.AutoSize = true;
            this.chbx_Add_Track.Location = new System.Drawing.Point(6, 39);
            this.chbx_Add_Track.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Track.Name = "chbx_Add_Track";
            this.chbx_Add_Track.Size = new System.Drawing.Size(76, 17);
            this.chbx_Add_Track.TabIndex = 208;
            this.chbx_Add_Track.Text = "Add Track";
            this.chbx_Add_Track.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Year
            // 
            this.chbx_Add_Year.AutoSize = true;
            this.chbx_Add_Year.Location = new System.Drawing.Point(6, 60);
            this.chbx_Add_Year.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Year.Name = "chbx_Add_Year";
            this.chbx_Add_Year.Size = new System.Drawing.Size(70, 17);
            this.chbx_Add_Year.TabIndex = 209;
            this.chbx_Add_Year.Text = "Add Year";
            this.chbx_Add_Year.UseVisualStyleBackColor = true;
            // 
            // btn_RePack
            // 
            this.btn_RePack.BackColor = System.Drawing.SystemColors.Control;
            this.btn_RePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RePack.Location = new System.Drawing.Point(4, 132);
            this.btn_RePack.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RePack.Name = "btn_RePack";
            this.btn_RePack.Size = new System.Drawing.Size(77, 78);
            this.btn_RePack.TabIndex = 210;
            this.btn_RePack.Text = "RePack";
            this.btn_RePack.UseVisualStyleBackColor = false;
            this.btn_RePack.Click += new System.EventHandler(this.btn_RePack_Click);
            // 
            // chbx_Normalize
            // 
            this.chbx_Normalize.AutoSize = true;
            this.chbx_Normalize.Checked = true;
            this.chbx_Normalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Normalize.Enabled = false;
            this.chbx_Normalize.Location = new System.Drawing.Point(177, 529);
            this.chbx_Normalize.Name = "chbx_Normalize";
            this.chbx_Normalize.Size = new System.Drawing.Size(122, 17);
            this.chbx_Normalize.TabIndex = 211;
            this.chbx_Normalize.Text = "Normalize File Name";
            this.chbx_Normalize.UseVisualStyleBackColor = true;
            // 
            // lbl_PreviewText
            // 
            this.lbl_PreviewText.AutoEllipsis = true;
            this.lbl_PreviewText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_PreviewText.Location = new System.Drawing.Point(74, 346);
            this.lbl_PreviewText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PreviewText.Name = "lbl_PreviewText";
            this.lbl_PreviewText.Size = new System.Drawing.Size(448, 35);
            this.lbl_PreviewText.TabIndex = 212;
            this.lbl_PreviewText.Text = "File name CDLC/ORIG-Artist-Year-Album-TrackNo(if existing)-Title-TrackAvail(LRBVS" +
    ")-Version";
            // 
            // grpbx_Title_Normalization
            // 
            this.grpbx_Title_Normalization.Controls.Add(this.checkBox3);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_DD);
            this.grpbx_Title_Normalization.Controls.Add(this.checkBox1);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Beta);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Group);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Alternate_Version);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_MultiTrack_Details);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Avail_Instrumets);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Rating);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Description);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Comments);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_CDCL);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Album);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Track);
            this.grpbx_Title_Normalization.Controls.Add(this.chbx_Add_Year);
            this.grpbx_Title_Normalization.Location = new System.Drawing.Point(14, 552);
            this.grpbx_Title_Normalization.Name = "grpbx_Title_Normalization";
            this.grpbx_Title_Normalization.Size = new System.Drawing.Size(413, 104);
            this.grpbx_Title_Normalization.TabIndex = 206;
            this.grpbx_Title_Normalization.TabStop = false;
            this.grpbx_Title_Normalization.Text = "Title Normalization:";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(330, 39);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(78, 17);
            this.checkBox3.TabIndex = 221;
            this.checkBox3.Text = "Instr. rating";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_DD
            // 
            this.chbx_Add_DD.AutoSize = true;
            this.chbx_Add_DD.Location = new System.Drawing.Point(298, 60);
            this.chbx_Add_DD.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_DD.Name = "chbx_Add_DD";
            this.chbx_Add_DD.Size = new System.Drawing.Size(108, 17);
            this.chbx_Add_DD.TabIndex = 220;
            this.chbx_Add_DD.Text = "Add Dinamic Diff.";
            this.chbx_Add_DD.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(298, 81);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 219;
            this.checkBox1.Text = "Add Broken";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Beta
            // 
            this.chbx_Add_Beta.AutoSize = true;
            this.chbx_Add_Beta.Location = new System.Drawing.Point(208, 81);
            this.chbx_Add_Beta.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Beta.Name = "chbx_Add_Beta";
            this.chbx_Add_Beta.Size = new System.Drawing.Size(70, 17);
            this.chbx_Add_Beta.TabIndex = 218;
            this.chbx_Add_Beta.Text = "Add Beta";
            this.chbx_Add_Beta.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Group
            // 
            this.chbx_Add_Group.AutoSize = true;
            this.chbx_Add_Group.Location = new System.Drawing.Point(208, 60);
            this.chbx_Add_Group.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Group.Name = "chbx_Add_Group";
            this.chbx_Add_Group.Size = new System.Drawing.Size(77, 17);
            this.chbx_Add_Group.TabIndex = 217;
            this.chbx_Add_Group.Text = "Add Group";
            this.chbx_Add_Group.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Alternate_Version
            // 
            this.chbx_Add_Alternate_Version.AutoSize = true;
            this.chbx_Add_Alternate_Version.Location = new System.Drawing.Point(208, 18);
            this.chbx_Add_Alternate_Version.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Alternate_Version.Name = "chbx_Add_Alternate_Version";
            this.chbx_Add_Alternate_Version.Size = new System.Drawing.Size(90, 17);
            this.chbx_Add_Alternate_Version.TabIndex = 215;
            this.chbx_Add_Alternate_Version.Text = "Add Alt. Vers.";
            this.chbx_Add_Alternate_Version.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_MultiTrack_Details
            // 
            this.chbx_Add_MultiTrack_Details.AutoSize = true;
            this.chbx_Add_MultiTrack_Details.Location = new System.Drawing.Point(208, 39);
            this.chbx_Add_MultiTrack_Details.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_MultiTrack_Details.Name = "chbx_Add_MultiTrack_Details";
            this.chbx_Add_MultiTrack_Details.Size = new System.Drawing.Size(118, 17);
            this.chbx_Add_MultiTrack_Details.TabIndex = 216;
            this.chbx_Add_MultiTrack_Details.Text = "Add MTrack details";
            this.chbx_Add_MultiTrack_Details.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Avail_Instrumets
            // 
            this.chbx_Add_Avail_Instrumets.AutoSize = true;
            this.chbx_Add_Avail_Instrumets.Location = new System.Drawing.Point(97, 81);
            this.chbx_Add_Avail_Instrumets.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Avail_Instrumets.Name = "chbx_Add_Avail_Instrumets";
            this.chbx_Add_Avail_Instrumets.Size = new System.Drawing.Size(100, 17);
            this.chbx_Add_Avail_Instrumets.TabIndex = 214;
            this.chbx_Add_Avail_Instrumets.Text = "Add Avail. Instr.";
            this.chbx_Add_Avail_Instrumets.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Rating
            // 
            this.chbx_Add_Rating.AutoSize = true;
            this.chbx_Add_Rating.Location = new System.Drawing.Point(97, 18);
            this.chbx_Add_Rating.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Rating.Name = "chbx_Add_Rating";
            this.chbx_Add_Rating.Size = new System.Drawing.Size(79, 17);
            this.chbx_Add_Rating.TabIndex = 211;
            this.chbx_Add_Rating.Text = "Add Rating";
            this.chbx_Add_Rating.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Description
            // 
            this.chbx_Add_Description.AutoSize = true;
            this.chbx_Add_Description.Location = new System.Drawing.Point(97, 39);
            this.chbx_Add_Description.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Description.Name = "chbx_Add_Description";
            this.chbx_Add_Description.Size = new System.Drawing.Size(101, 17);
            this.chbx_Add_Description.TabIndex = 212;
            this.chbx_Add_Description.Text = "Add Description";
            this.chbx_Add_Description.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_Comments
            // 
            this.chbx_Add_Comments.AutoSize = true;
            this.chbx_Add_Comments.Location = new System.Drawing.Point(97, 60);
            this.chbx_Add_Comments.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_Comments.Name = "chbx_Add_Comments";
            this.chbx_Add_Comments.Size = new System.Drawing.Size(97, 17);
            this.chbx_Add_Comments.TabIndex = 213;
            this.chbx_Add_Comments.Text = "Add Comments";
            this.chbx_Add_Comments.UseVisualStyleBackColor = true;
            // 
            // chbx_Add_CDCL
            // 
            this.chbx_Add_CDCL.AutoSize = true;
            this.chbx_Add_CDCL.Location = new System.Drawing.Point(6, 81);
            this.chbx_Add_CDCL.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_Add_CDCL.Name = "chbx_Add_CDCL";
            this.chbx_Add_CDCL.Size = new System.Drawing.Size(76, 17);
            this.chbx_Add_CDCL.TabIndex = 210;
            this.chbx_Add_CDCL.Text = "Add CDLC";
            this.chbx_Add_CDCL.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(391, 504);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 42);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add to";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtn_Normal);
            this.panel1.Controls.Add(this.rbnt_Sort);
            this.panel1.Location = new System.Drawing.Point(3, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 25);
            this.panel1.TabIndex = 204;
            // 
            // rbtn_Normal
            // 
            this.rbtn_Normal.AutoSize = true;
            this.rbtn_Normal.Checked = true;
            this.rbtn_Normal.Location = new System.Drawing.Point(70, 3);
            this.rbtn_Normal.Name = "rbtn_Normal";
            this.rbtn_Normal.Size = new System.Drawing.Size(58, 17);
            this.rbtn_Normal.TabIndex = 205;
            this.rbtn_Normal.TabStop = true;
            this.rbtn_Normal.Text = "Normal";
            this.rbtn_Normal.UseVisualStyleBackColor = true;
            this.rbtn_Normal.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbnt_Sort
            // 
            this.rbnt_Sort.AutoSize = true;
            this.rbnt_Sort.Location = new System.Drawing.Point(3, 3);
            this.rbnt_Sort.Name = "rbnt_Sort";
            this.rbnt_Sort.Size = new System.Drawing.Size(44, 17);
            this.rbnt_Sort.TabIndex = 204;
            this.rbnt_Sort.Text = "Sort";
            this.rbnt_Sort.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(321, 4);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(85, 17);
            this.checkBox2.TabIndex = 216;
            this.checkBox2.Text = "Clean Folder";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(85, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 55);
            this.groupBox2.TabIndex = 207;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Repack Only";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtn_Population_Groups);
            this.panel2.Controls.Add(this.rbtn_Population_All);
            this.panel2.Controls.Add(this.rbtn_Population_Selected);
            this.panel2.Location = new System.Drawing.Point(6, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 39);
            this.panel2.TabIndex = 204;
            // 
            // rbtn_Population_Groups
            // 
            this.rbtn_Population_Groups.AutoSize = true;
            this.rbtn_Population_Groups.Location = new System.Drawing.Point(3, 19);
            this.rbtn_Population_Groups.Name = "rbtn_Population_Groups";
            this.rbtn_Population_Groups.Size = new System.Drawing.Size(59, 17);
            this.rbtn_Population_Groups.TabIndex = 206;
            this.rbtn_Population_Groups.Text = "Groups";
            this.rbtn_Population_Groups.UseVisualStyleBackColor = true;
            this.rbtn_Population_Groups.CheckedChanged += new System.EventHandler(this.rbtn_Groups_CheckedChanged);
            // 
            // rbtn_Population_All
            // 
            this.rbtn_Population_All.AutoSize = true;
            this.rbtn_Population_All.Checked = true;
            this.rbtn_Population_All.Location = new System.Drawing.Point(71, 3);
            this.rbtn_Population_All.Margin = new System.Windows.Forms.Padding(0);
            this.rbtn_Population_All.Name = "rbtn_Population_All";
            this.rbtn_Population_All.Size = new System.Drawing.Size(36, 17);
            this.rbtn_Population_All.TabIndex = 205;
            this.rbtn_Population_All.TabStop = true;
            this.rbtn_Population_All.Text = "All";
            this.rbtn_Population_All.UseVisualStyleBackColor = true;
            // 
            // rbtn_Population_Selected
            // 
            this.rbtn_Population_Selected.AutoSize = true;
            this.rbtn_Population_Selected.Location = new System.Drawing.Point(3, 3);
            this.rbtn_Population_Selected.Name = "rbtn_Population_Selected";
            this.rbtn_Population_Selected.Size = new System.Drawing.Size(67, 17);
            this.rbtn_Population_Selected.TabIndex = 204;
            this.rbtn_Population_Selected.Text = "Selected";
            this.rbtn_Population_Selected.UseVisualStyleBackColor = true;
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Location = new System.Drawing.Point(51, 236);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(341, 20);
            this.txt_Title_Sort.TabIndex = 217;
            this.txt_Title_Sort.Text = "<Year><Album><Track No.><Title>";
            // 
            // cbx_Title_Sort
            // 
            this.cbx_Title_Sort.FormattingEnabled = true;
            this.cbx_Title_Sort.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Title_Sort.Location = new System.Drawing.Point(395, 235);
            this.cbx_Title_Sort.Name = "cbx_Title_Sort";
            this.cbx_Title_Sort.Size = new System.Drawing.Size(105, 21);
            this.cbx_Title_Sort.TabIndex = 218;
            this.cbx_Title_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_SelectedIndexChanged);
            // 
            // cbx_Album
            // 
            this.cbx_Album.FormattingEnabled = true;
            this.cbx_Album.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Album.Location = new System.Drawing.Point(395, 299);
            this.cbx_Album.Name = "cbx_Album";
            this.cbx_Album.Size = new System.Drawing.Size(105, 21);
            this.cbx_Album.TabIndex = 220;
            this.cbx_Album.SelectedIndexChanged += new System.EventHandler(this.cbx_Album_SelectedIndexChanged);
            // 
            // txt_Album
            // 
            this.txt_Album.Location = new System.Drawing.Point(51, 300);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(340, 20);
            this.txt_Album.TabIndex = 219;
            this.txt_Album.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            // 
            // cbx_File_Name
            // 
            this.cbx_File_Name.FormattingEnabled = true;
            this.cbx_File_Name.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_File_Name.Location = new System.Drawing.Point(396, 323);
            this.cbx_File_Name.Name = "cbx_File_Name";
            this.cbx_File_Name.Size = new System.Drawing.Size(104, 21);
            this.cbx_File_Name.TabIndex = 222;
            this.cbx_File_Name.SelectedIndexChanged += new System.EventHandler(this.cbx_FileName_SelectedIndexChanged);
            // 
            // txt_File_Name
            // 
            this.txt_File_Name.Location = new System.Drawing.Point(63, 324);
            this.txt_File_Name.Margin = new System.Windows.Forms.Padding(2);
            this.txt_File_Name.Name = "txt_File_Name";
            this.txt_File_Name.Size = new System.Drawing.Size(329, 20);
            this.txt_File_Name.TabIndex = 221;
            this.txt_File_Name.Text = "<Beta><Broken><CDLC> - <Artist> - <Year> - <Album><Track No.> - <Title> - <DD> - " +
    "<Avail. Instr.> - v<Version>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(1, 238);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 223;
            this.label4.Text = "Title Sort";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(2, 303);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 224;
            this.label5.Text = "Album";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(2, 327);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 225;
            this.label6.Text = "File Name:";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(1, 216);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(27, 13);
            this.lbl_Title.TabIndex = 228;
            this.lbl_Title.Text = "Title";
            // 
            // cbx_Title
            // 
            this.cbx_Title.FormattingEnabled = true;
            this.cbx_Title.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Title.Location = new System.Drawing.Point(395, 213);
            this.cbx_Title.Name = "cbx_Title";
            this.cbx_Title.Size = new System.Drawing.Size(105, 21);
            this.cbx_Title.TabIndex = 227;
            this.cbx_Title.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(51, 214);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(341, 20);
            this.txt_Title.TabIndex = 226;
            this.txt_Title.Text = "<Title>";
            this.txt_Title.TextChanged += new System.EventHandler(this.txt_Title_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(1, 259);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 231;
            this.label7.Text = "Artist";
            // 
            // cbx_Artist
            // 
            this.cbx_Artist.FormattingEnabled = true;
            this.cbx_Artist.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Artist.Location = new System.Drawing.Point(395, 256);
            this.cbx_Artist.Name = "cbx_Artist";
            this.cbx_Artist.Size = new System.Drawing.Size(105, 21);
            this.cbx_Artist.TabIndex = 230;
            this.cbx_Artist.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_SelectedIndexChanged);
            // 
            // txt_Artist
            // 
            this.txt_Artist.Location = new System.Drawing.Point(51, 257);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(341, 20);
            this.txt_Artist.TabIndex = 229;
            this.txt_Artist.Text = "<Artist>";
            // 
            // cbx_Activ_Title
            // 
            this.cbx_Activ_Title.AutoSize = true;
            this.cbx_Activ_Title.Checked = true;
            this.cbx_Activ_Title.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title.Location = new System.Drawing.Point(502, 217);
            this.cbx_Activ_Title.Name = "cbx_Activ_Title";
            this.cbx_Activ_Title.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Title.TabIndex = 232;
            this.cbx_Activ_Title.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_CheckedChanged);
            // 
            // cbx_Activ_Title_Sort
            // 
            this.cbx_Activ_Title_Sort.AutoSize = true;
            this.cbx_Activ_Title_Sort.Checked = true;
            this.cbx_Activ_Title_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title_Sort.Location = new System.Drawing.Point(502, 238);
            this.cbx_Activ_Title_Sort.Name = "cbx_Activ_Title_Sort";
            this.cbx_Activ_Title_Sort.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Title_Sort.TabIndex = 233;
            this.cbx_Activ_Title_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_Sort_CheckedChanged);
            // 
            // cbx_Activ_Artist
            // 
            this.cbx_Activ_Artist.AutoSize = true;
            this.cbx_Activ_Artist.Checked = true;
            this.cbx_Activ_Artist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist.Location = new System.Drawing.Point(502, 260);
            this.cbx_Activ_Artist.Name = "cbx_Activ_Artist";
            this.cbx_Activ_Artist.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Artist.TabIndex = 234;
            this.cbx_Activ_Artist.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_CheckedChanged);
            // 
            // cbx_Activ_Album
            // 
            this.cbx_Activ_Album.AutoSize = true;
            this.cbx_Activ_Album.Checked = true;
            this.cbx_Activ_Album.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Album.Location = new System.Drawing.Point(502, 303);
            this.cbx_Activ_Album.Name = "cbx_Activ_Album";
            this.cbx_Activ_Album.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Album.TabIndex = 235;
            this.cbx_Activ_Album.UseVisualStyleBackColor = true;
            this.cbx_Activ_Album.CheckedChanged += new System.EventHandler(this.cbx_Activ_Album_CheckedChanged);
            // 
            // cbx_Activ_File_Name
            // 
            this.cbx_Activ_File_Name.AutoSize = true;
            this.cbx_Activ_File_Name.Checked = true;
            this.cbx_Activ_File_Name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_File_Name.Location = new System.Drawing.Point(502, 327);
            this.cbx_Activ_File_Name.Name = "cbx_Activ_File_Name";
            this.cbx_Activ_File_Name.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_File_Name.TabIndex = 236;
            this.cbx_Activ_File_Name.UseVisualStyleBackColor = true;
            this.cbx_Activ_File_Name.CheckedChanged += new System.EventHandler(this.cbx_Activ_File_Name_CheckedChanged);
            // 
            // btn_Preview_Title
            // 
            this.btn_Preview_Title.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title.Location = new System.Drawing.Point(516, 216);
            this.btn_Preview_Title.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Title.Name = "btn_Preview_Title";
            this.btn_Preview_Title.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Title.TabIndex = 237;
            this.btn_Preview_Title.UseVisualStyleBackColor = false;
            this.btn_Preview_Title.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Preview_Title_Sort
            // 
            this.btn_Preview_Title_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title_Sort.Location = new System.Drawing.Point(516, 237);
            this.btn_Preview_Title_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Title_Sort.Name = "btn_Preview_Title_Sort";
            this.btn_Preview_Title_Sort.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Title_Sort.TabIndex = 238;
            this.btn_Preview_Title_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Title_Sort.Click += new System.EventHandler(this.btn_Preview_Title_Sort_Click);
            // 
            // btn_Preview_Artist
            // 
            this.btn_Preview_Artist.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist.Location = new System.Drawing.Point(516, 259);
            this.btn_Preview_Artist.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Artist.Name = "btn_Preview_Artist";
            this.btn_Preview_Artist.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Artist.TabIndex = 239;
            this.btn_Preview_Artist.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist.Click += new System.EventHandler(this.btn_Preview_Artist_Click);
            // 
            // btn_Preview_Album
            // 
            this.btn_Preview_Album.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Album.Location = new System.Drawing.Point(516, 302);
            this.btn_Preview_Album.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Album.Name = "btn_Preview_Album";
            this.btn_Preview_Album.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Album.TabIndex = 240;
            this.btn_Preview_Album.UseVisualStyleBackColor = false;
            this.btn_Preview_Album.Click += new System.EventHandler(this.btn_Preview_Album_Click);
            // 
            // btn_Preview_File_Name
            // 
            this.btn_Preview_File_Name.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_File_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_File_Name.Location = new System.Drawing.Point(516, 326);
            this.btn_Preview_File_Name.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_File_Name.Name = "btn_Preview_File_Name";
            this.btn_Preview_File_Name.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_File_Name.TabIndex = 241;
            this.btn_Preview_File_Name.UseVisualStyleBackColor = false;
            this.btn_Preview_File_Name.Click += new System.EventHandler(this.btn_Preview_File_Name_Click);
            // 
            // btn_Preview_Artist_Sort
            // 
            this.btn_Preview_Artist_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist_Sort.Location = new System.Drawing.Point(516, 280);
            this.btn_Preview_Artist_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Artist_Sort.Name = "btn_Preview_Artist_Sort";
            this.btn_Preview_Artist_Sort.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Artist_Sort.TabIndex = 246;
            this.btn_Preview_Artist_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist_Sort.Click += new System.EventHandler(this.btn_Preview_Artist_Sort_Click);
            // 
            // cbx_Activ_Artist_Sort
            // 
            this.cbx_Activ_Artist_Sort.AutoSize = true;
            this.cbx_Activ_Artist_Sort.Checked = true;
            this.cbx_Activ_Artist_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist_Sort.Location = new System.Drawing.Point(502, 281);
            this.cbx_Activ_Artist_Sort.Name = "cbx_Activ_Artist_Sort";
            this.cbx_Activ_Artist_Sort.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Artist_Sort.TabIndex = 245;
            this.cbx_Activ_Artist_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_Sort_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(1, 280);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 244;
            this.label3.Text = "Artist_Sort";
            // 
            // cbx_Artist_Sort
            // 
            this.cbx_Artist_Sort.FormattingEnabled = true;
            this.cbx_Artist_Sort.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Artist_Sort.Location = new System.Drawing.Point(395, 277);
            this.cbx_Artist_Sort.Name = "cbx_Artist_Sort";
            this.cbx_Artist_Sort.Size = new System.Drawing.Size(105, 21);
            this.cbx_Artist_Sort.TabIndex = 243;
            this.cbx_Artist_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_Sort_SelectedIndexChanged);
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Location = new System.Drawing.Point(51, 278);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(341, 20);
            this.txt_Artist_Sort.TabIndex = 242;
            this.txt_Artist_Sort.Text = "<Beta><Artist>";
            this.txt_Artist_Sort.TextChanged += new System.EventHandler(this.txt_Artist_Sort_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(1, 346);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 247;
            this.label8.Text = "Mask Preview:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(481, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 249;
            this.label10.Text = "Activ|Prev";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // chbx_Additional_Manipualtions
            // 
            this.chbx_Additional_Manipualtions.FormattingEnabled = true;
            this.chbx_Additional_Manipualtions.Items.AddRange(new object[] {
            "1. Add Increment to all songs",
            "2. Add Increment to all songs(&Separately per artist)",
            "3. Make all DLC IDs unique(&save)",
            "4. Add DD (4 Levels)",
            "5. Remove DD",
            "6. Remove DD only for Bass Guitar",
            "7. Remove the 4sec of the Preview song",
            "8. Don\'t repack Broken songs",
            "9. Pack to cross-platform Compatible Filenames",
            "10. Generate random 30sec Preview",
            "11. Use shortnames in the Filename for Artist&Album",
            "12. Repack Originals",
            "13. Repack PC",
            "14. Import all as Alternates",
            "15. Import any Custom as Alternate if an Original exists",
            "16. Move Original Imported files to temp/0_old"});
            this.chbx_Additional_Manipualtions.Location = new System.Drawing.Point(237, 89);
            this.chbx_Additional_Manipualtions.Name = "chbx_Additional_Manipualtions";
            this.chbx_Additional_Manipualtions.Size = new System.Drawing.Size(295, 109);
            this.chbx_Additional_Manipualtions.TabIndex = 251;
            this.chbx_Additional_Manipualtions.SelectedIndexChanged += new System.EventHandler(this.chbx_Additional_Manipualtions_SelectedIndexChanged);
            // 
            // cbx_Groups
            // 
            this.cbx_Groups.DataSource = this.mainBindingSource;
            this.cbx_Groups.Enabled = false;
            this.cbx_Groups.FormattingEnabled = true;
            this.cbx_Groups.Location = new System.Drawing.Point(88, 186);
            this.cbx_Groups.Name = "cbx_Groups";
            this.cbx_Groups.Size = new System.Drawing.Size(120, 21);
            this.cbx_Groups.TabIndex = 252;
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataMember = "Main";
            this.mainBindingSource.DataSource = this.filesDataSet;
            // 
            // filesDataSet
            // 
            this.filesDataSet.DataSetName = "FilesDataSet";
            this.filesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mainTableAdapter
            // 
            this.mainTableAdapter.ClearBeforeFill = true;
            // 
            // btn_Cleanup_MainDB
            // 
            this.btn_Cleanup_MainDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cleanup_MainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cleanup_MainDB.Location = new System.Drawing.Point(427, 360);
            this.btn_Cleanup_MainDB.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cleanup_MainDB.Name = "btn_Cleanup_MainDB";
            this.btn_Cleanup_MainDB.Size = new System.Drawing.Size(103, 21);
            this.btn_Cleanup_MainDB.TabIndex = 253;
            this.btn_Cleanup_MainDB.Text = "Remove Only";
            this.btn_Cleanup_MainDB.UseVisualStyleBackColor = false;
            this.btn_Cleanup_MainDB.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chbx_Skip_PC
            // 
            this.chbx_Skip_PC.AutoSize = true;
            this.chbx_Skip_PC.Enabled = false;
            this.chbx_Skip_PC.Location = new System.Drawing.Point(278, 542);
            this.chbx_Skip_PC.Name = "chbx_Skip_PC";
            this.chbx_Skip_PC.Size = new System.Drawing.Size(81, 17);
            this.chbx_Skip_PC.TabIndex = 214;
            this.chbx_Skip_PC.Text = "Repack PC";
            this.chbx_Skip_PC.UseVisualStyleBackColor = true;
            // 
            // chbx_Skip_ORIG
            // 
            this.chbx_Skip_ORIG.AutoSize = true;
            this.chbx_Skip_ORIG.Enabled = false;
            this.chbx_Skip_ORIG.Location = new System.Drawing.Point(278, 526);
            this.chbx_Skip_ORIG.Name = "chbx_Skip_ORIG";
            this.chbx_Skip_ORIG.Size = new System.Drawing.Size(89, 17);
            this.chbx_Skip_ORIG.TabIndex = 213;
            this.chbx_Skip_ORIG.Text = "Repack Orig.";
            this.chbx_Skip_ORIG.UseVisualStyleBackColor = true;
            // 
            // chbx_Rebuild
            // 
            this.chbx_Rebuild.AutoSize = true;
            this.chbx_Rebuild.Enabled = false;
            this.chbx_Rebuild.Location = new System.Drawing.Point(375, 45);
            this.chbx_Rebuild.Name = "chbx_Rebuild";
            this.chbx_Rebuild.Size = new System.Drawing.Size(86, 17);
            this.chbx_Rebuild.TabIndex = 254;
            this.chbx_Rebuild.Text = "Rebuild Only";
            this.chbx_Rebuild.UseVisualStyleBackColor = true;
            // 
            // Export_To
            // 
            this.Export_To.BackColor = System.Drawing.SystemColors.Control;
            this.Export_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Export_To.Location = new System.Drawing.Point(283, 360);
            this.Export_To.Margin = new System.Windows.Forms.Padding(2);
            this.Export_To.Name = "Export_To";
            this.Export_To.Size = new System.Drawing.Size(76, 21);
            this.Export_To.TabIndex = 255;
            this.Export_To.Text = "Export";
            this.Export_To.UseVisualStyleBackColor = false;
            this.Export_To.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // cbx_Export
            // 
            this.cbx_Export.FormattingEnabled = true;
            this.cbx_Export.Items.AddRange(new object[] {
            "Excel",
            "WebPage"});
            this.cbx_Export.Location = new System.Drawing.Point(361, 360);
            this.cbx_Export.Name = "cbx_Export";
            this.cbx_Export.Size = new System.Drawing.Size(61, 21);
            this.cbx_Export.TabIndex = 256;
            this.cbx_Export.Text = "Excel";
            // 
            // DLCManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbx_Export);
            this.Controls.Add(this.Export_To);
            this.Controls.Add(this.chbx_Rebuild);
            this.Controls.Add(this.btn_Cleanup_MainDB);
            this.Controls.Add(this.cbx_Groups);
            this.Controls.Add(this.chbx_Additional_Manipualtions);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_Preview_Artist_Sort);
            this.Controls.Add(this.cbx_Activ_Artist_Sort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_Artist_Sort);
            this.Controls.Add(this.txt_Artist_Sort);
            this.Controls.Add(this.btn_Preview_File_Name);
            this.Controls.Add(this.btn_Preview_Album);
            this.Controls.Add(this.btn_Preview_Artist);
            this.Controls.Add(this.btn_Preview_Title_Sort);
            this.Controls.Add(this.btn_Preview_Title);
            this.Controls.Add(this.chbx_RS2012DLC);
            this.Controls.Add(this.chbx_Skip_ORIG);
            this.Controls.Add(this.cbx_Activ_File_Name);
            this.Controls.Add(this.chbx_Skip_PC);
            this.Controls.Add(this.cbx_Activ_Album);
            this.Controls.Add(this.cbx_Activ_Artist);
            this.Controls.Add(this.cbx_Activ_Title_Sort);
            this.Controls.Add(this.cbx_Activ_Title);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbx_Artist);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.cbx_Title);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_File_Name);
            this.Controls.Add(this.txt_File_Name);
            this.Controls.Add(this.cbx_Album);
            this.Controls.Add(this.txt_Album);
            this.Controls.Add(this.cbx_Title_Sort);
            this.Controls.Add(this.txt_Title_Sort);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpbx_Title_Normalization);
            this.Controls.Add(this.lbl_PreviewText);
            this.Controls.Add(this.chbx_Normalize);
            this.Controls.Add(this.btn_RePack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpbx_ApplyChanges);
            this.Controls.Add(this.chbx_DebugB);
            this.Controls.Add(this.chbx_Debug);
            this.Controls.Add(this.txt_DBFolder);
            this.Controls.Add(this.btn_DBFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_RocksmithDLCPath);
            this.Controls.Add(this.txt_TempPath);
            this.Controls.Add(this.chbx_CleanFolder);
            this.Controls.Add(this.btn_SteamDLCFolder);
            this.Controls.Add(this.chbx_CleanDB);
            this.Controls.Add(this.lbl_StatisticsOnReadDLCs);
            this.Controls.Add(this.rtxt_StatisticsOnReadDLCs);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.btn_PopulateDB);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.btn_ReadFolder);
            this.Controls.Add(this.lbl_RocksmithDLCPath);
            this.Controls.Add(this.btn_TempPath);
            this.Controls.Add(this.lbl_TempFolders);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DLCManagerWindow";
            this.Size = new System.Drawing.Size(542, 656);
            this.pnl_ApplyChanges.ResumeLayout(false);
            this.pnl_ApplyChanges.PerformLayout();
            this.grpbx_ApplyChanges.ResumeLayout(false);
            this.grpbx_Title_Normalization.ResumeLayout(false);
            this.grpbx_Title_Normalization.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_RocksmithDLCPath;
        private System.Windows.Forms.TextBox txt_TempPath;
        private System.Windows.Forms.CheckBox chbx_CleanFolder;
        private System.Windows.Forms.Button btn_SteamDLCFolder;
        private System.Windows.Forms.CheckBox chbx_CleanDB;
        private System.Windows.Forms.CheckBox chbx_RS2012DLC;
        private System.Windows.Forms.Label lbl_StatisticsOnReadDLCs;
        private System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private System.Windows.Forms.Button btn_PopulateDB;
        private System.Windows.Forms.Button btn_DecompressAll;
        private System.Windows.Forms.Button btn_ReadFolder;
        private System.Windows.Forms.Label lbl_RocksmithDLCPath;
        private System.Windows.Forms.Button btn_TempPath;
        private System.Windows.Forms.Label lbl_TempFolders;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_DBFolder;
        private System.Windows.Forms.Button btn_DBFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbx_Debug;
        private System.Windows.Forms.CheckBox chbx_DebugB;
        private System.Windows.Forms.Panel pnl_ApplyChanges;
        private System.Windows.Forms.RadioButton rbtn_Xbox;
        private System.Windows.Forms.RadioButton rbtn_Mac;
        private System.Windows.Forms.RadioButton rbtn_PS3;
        private System.Windows.Forms.RadioButton rbtn_All;
        private System.Windows.Forms.GroupBox grpbx_ApplyChanges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbx_Add_Album;
        private System.Windows.Forms.CheckBox chbx_Add_Track;
        private System.Windows.Forms.CheckBox chbx_Add_Year;
        private System.Windows.Forms.Button btn_RePack;
        private System.Windows.Forms.CheckBox chbx_Normalize;
        private System.Windows.Forms.Label lbl_PreviewText;
        private System.Windows.Forms.GroupBox grpbx_Title_Normalization;
        private System.Windows.Forms.CheckBox chbx_Add_CDCL;
        private System.Windows.Forms.CheckBox chbx_Add_Avail_Instrumets;
        private System.Windows.Forms.CheckBox chbx_Add_Rating;
        private System.Windows.Forms.CheckBox chbx_Add_Description;
        private System.Windows.Forms.CheckBox chbx_Add_Comments;
        private System.Windows.Forms.CheckBox chbx_Add_Alternate_Version;
        private System.Windows.Forms.CheckBox chbx_Add_MultiTrack_Details;
        private System.Windows.Forms.CheckBox chbx_Add_Group;
        private System.Windows.Forms.CheckBox chbx_Add_Beta;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chbx_Add_DD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtn_Normal;
        private System.Windows.Forms.RadioButton rbnt_Sort;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtn_Population_All;
        private System.Windows.Forms.RadioButton rbtn_Population_Selected;
        private System.Windows.Forms.TextBox txt_Title_Sort;
        private System.Windows.Forms.ComboBox cbx_Title_Sort;
        private System.Windows.Forms.ComboBox cbx_Album;
        private System.Windows.Forms.TextBox txt_Album;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox cbx_File_Name;
        private System.Windows.Forms.TextBox txt_File_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.ComboBox cbx_Title;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbx_Artist;
        private System.Windows.Forms.TextBox txt_Artist;
        private System.Windows.Forms.CheckBox cbx_Activ_Title;
        private System.Windows.Forms.CheckBox cbx_Activ_Title_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist;
        private System.Windows.Forms.CheckBox cbx_Activ_Album;
        private System.Windows.Forms.CheckBox cbx_Activ_File_Name;
        private System.Windows.Forms.Button btn_Preview_Title;
        private System.Windows.Forms.Button btn_Preview_Title_Sort;
        private System.Windows.Forms.Button btn_Preview_Artist;
        private System.Windows.Forms.Button btn_Preview_Album;
        private System.Windows.Forms.Button btn_Preview_File_Name;
        private System.Windows.Forms.Button btn_Preview_Artist_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist_Sort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_Artist_Sort;
        private System.Windows.Forms.TextBox txt_Artist_Sort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckedListBox chbx_Additional_Manipualtions;
        private RadioButton rbtn_Population_Groups;
        private ComboBox cbx_Groups;
        private BindingSource mainBindingSource;
        private FilesDataSet filesDataSet;
        private FilesDataSetTableAdapters.MainTableAdapter mainTableAdapter;
        private Button btn_Cleanup_MainDB;
        private CheckBox chbx_Skip_PC;
        private CheckBox chbx_Skip_ORIG;
        private CheckBox chbx_Rebuild;
        private Button Export_To;
        private ComboBox cbx_Export;
    }
}
