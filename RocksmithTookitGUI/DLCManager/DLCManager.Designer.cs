using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class DLCManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DLCManager));
            this.cbx_Export = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chbx_iOS = new System.Windows.Forms.CheckBox();
            this.chbx_PS4 = new System.Windows.Forms.CheckBox();
            this.txt_NoOfSplits = new System.Windows.Forms.ComboBox();
            this.mainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rbtn_Population_PackNO = new System.Windows.Forms.RadioButton();
            this.btm_GoRepack = new System.Windows.Forms.Button();
            this.lbl_NoRec2 = new System.Windows.Forms.Label();
            this.chbx_XBOX360 = new System.Windows.Forms.CheckBox();
            this.btn_Debbug = new System.Windows.Forms.Button();
            this.chbx_Mac = new System.Windows.Forms.CheckBox();
            this.rbtn_Population_Groups = new System.Windows.Forms.RadioButton();
            this.chbx_PS3 = new System.Windows.Forms.CheckBox();
            this.cbx_Groups = new System.Windows.Forms.ComboBox();
            this.chbx_PC = new System.Windows.Forms.CheckBox();
            this.btn_Cleanup_MainDB = new System.Windows.Forms.Button();
            this.btn_RePack = new System.Windows.Forms.Button();
            this.rbtn_Population_All = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_Selected = new System.Windows.Forms.RadioButton();
            this.chbx_Rebuild = new System.Windows.Forms.CheckBox();
            this.chbx_DefaultDB = new System.Windows.Forms.CheckBox();
            this.Export_To = new System.Windows.Forms.Button();
            this.chbx_Additional_Manipulations = new System.Windows.Forms.CheckedListBox();
            this.lbl_Mask = new System.Windows.Forms.Label();
            this.btn_Preview_Artist_Sort = new System.Windows.Forms.Button();
            this.cbx_Activ_Artist_Sort = new System.Windows.Forms.CheckBox();
            this.lbl_Artist_Sort = new System.Windows.Forms.Label();
            this.cbx_Artist_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Artist_Sort = new System.Windows.Forms.TextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.btn_Preview_File_Name = new System.Windows.Forms.Button();
            this.btn_Preview_Album = new System.Windows.Forms.Button();
            this.btn_Preview_Artist = new System.Windows.Forms.Button();
            this.btn_Preview_Title_Sort = new System.Windows.Forms.Button();
            this.btn_Preview_Title = new System.Windows.Forms.Button();
            this.cbx_Activ_File_Name = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Album = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Artist = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Title_Sort = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Title = new System.Windows.Forms.CheckBox();
            this.lbl_Artist = new System.Windows.Forms.Label();
            this.cbx_Artist = new System.Windows.Forms.ComboBox();
            this.txt_Artist = new System.Windows.Forms.TextBox();
            this.cbx_Title = new System.Windows.Forms.ComboBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.lbl_File_Name = new System.Windows.Forms.Label();
            this.lbl_Album = new System.Windows.Forms.Label();
            this.lbl_Title_Sort = new System.Windows.Forms.Label();
            this.cbx_File_Name = new System.Windows.Forms.ComboBox();
            this.txt_File_Name = new System.Windows.Forms.TextBox();
            this.cbx_Album = new System.Windows.Forms.ComboBox();
            this.txt_Album = new System.Windows.Forms.TextBox();
            this.cbx_Title_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Title_Sort = new System.Windows.Forms.TextBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.chbx_DebugB = new System.Windows.Forms.CheckBox();
            this.txt_DBFolder = new System.Windows.Forms.TextBox();
            this.btn_DBFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_RocksmithDLCPath = new System.Windows.Forms.TextBox();
            this.txt_TempPath = new System.Windows.Forms.TextBox();
            this.chbx_CleanTemp = new System.Windows.Forms.CheckBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.rtxt_StatisticsOnReadDLCs = new System.Windows.Forms.RichTextBox();
            this.btn_PopulateDB = new System.Windows.Forms.Button();
            this.btn_OpenMainDB = new System.Windows.Forms.Button();
            this.lbl_RocksmithDLCPath = new System.Windows.Forms.Label();
            this.btn_TempPath = new System.Windows.Forms.Button();
            this.lbl_TempFolders = new System.Windows.Forms.Label();
            this.lbl_PreviewText = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_ApplyStandardization = new System.Windows.Forms.Button();
            this.btn_LoadRetailSongs = new System.Windows.Forms.Button();
            this.btn_Standardization = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_GoImport = new System.Windows.Forms.Button();
            this.btm_GoTemp = new System.Windows.Forms.Button();
            this.btm_GoDB = new System.Windows.Forms.Button();
            this.btn_OpenDB = new System.Windows.Forms.Button();
            this.chbx_Configurations = new System.Windows.Forms.ComboBox();
            this.btn_ProfilesSave = new System.Windows.Forms.Button();
            this.btn_ProfileRemove = new System.Windows.Forms.Button();
            this.btn_OpenLogsFolder = new System.Windows.Forms.Button();
            this.btn_Enable_CDLC = new System.Windows.Forms.Button();
            this.btn_CalcNoOfImports = new System.Windows.Forms.Button();
            this.btn_CopyDefaultDBtoTemp = new System.Windows.Forms.Button();
            this.btn_Param = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Preview_Lyric_Info = new System.Windows.Forms.Button();
            this.btn_Preview_Album_Sort = new System.Windows.Forms.Button();
            this.btn_Album2SortA = new System.Windows.Forms.Button();
            this.btn_FilterParams = new System.Windows.Forms.Button();
            this.lbl_Settings = new System.Windows.Forms.Label();
            this.lbl_Log = new System.Windows.Forms.Label();
            this.cbx_Activ_Lyric_Info = new System.Windows.Forms.CheckBox();
            this.lbl_LyricInfo = new System.Windows.Forms.Label();
            this.cbx_Lyric_Info = new System.Windows.Forms.ComboBox();
            this.txt_Lyric_Info = new System.Windows.Forms.TextBox();
            this.cbx_Activ_Album_Sort = new System.Windows.Forms.CheckBox();
            this.lbl_AlbumSort = new System.Windows.Forms.Label();
            this.cbx_Album_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Album_Sort = new System.Windows.Forms.TextBox();
            this.txt_FilterParams = new System.Windows.Forms.RichTextBox();
            this.lbl_Access = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbx_Export
            // 
            this.cbx_Export.FormattingEnabled = true;
            this.cbx_Export.Items.AddRange(new object[] {
            "Excel",
            "WebPage"});
            this.cbx_Export.Location = new System.Drawing.Point(198, 240);
            this.cbx_Export.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Export.Name = "cbx_Export";
            this.cbx_Export.Size = new System.Drawing.Size(112, 28);
            this.cbx_Export.TabIndex = 57;
            this.cbx_Export.Text = "WebPage";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_Access);
            this.panel2.Controls.Add(this.chbx_iOS);
            this.panel2.Controls.Add(this.chbx_PS4);
            this.panel2.Controls.Add(this.txt_NoOfSplits);
            this.panel2.Controls.Add(this.rbtn_Population_PackNO);
            this.panel2.Controls.Add(this.btm_GoRepack);
            this.panel2.Controls.Add(this.lbl_NoRec2);
            this.panel2.Controls.Add(this.chbx_XBOX360);
            this.panel2.Controls.Add(this.btn_Debbug);
            this.panel2.Controls.Add(this.chbx_Mac);
            this.panel2.Controls.Add(this.rbtn_Population_Groups);
            this.panel2.Controls.Add(this.chbx_PS3);
            this.panel2.Controls.Add(this.cbx_Groups);
            this.panel2.Controls.Add(this.chbx_PC);
            this.panel2.Controls.Add(this.btn_Cleanup_MainDB);
            this.panel2.Controls.Add(this.btn_RePack);
            this.panel2.Controls.Add(this.rbtn_Population_All);
            this.panel2.Controls.Add(this.rbtn_Population_Selected);
            this.panel2.Controls.Add(this.chbx_Rebuild);
            this.panel2.Location = new System.Drawing.Point(9, 96);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(572, 104);
            this.panel2.TabIndex = 204;
            // 
            // chbx_iOS
            // 
            this.chbx_iOS.AutoSize = true;
            this.chbx_iOS.Enabled = false;
            this.chbx_iOS.Location = new System.Drawing.Point(364, 6);
            this.chbx_iOS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_iOS.Name = "chbx_iOS";
            this.chbx_iOS.Size = new System.Drawing.Size(61, 24);
            this.chbx_iOS.TabIndex = 412;
            this.chbx_iOS.Text = "iOS";
            this.chbx_iOS.UseVisualStyleBackColor = true;
            // 
            // chbx_PS4
            // 
            this.chbx_PS4.AutoSize = true;
            this.chbx_PS4.Enabled = false;
            this.chbx_PS4.Location = new System.Drawing.Point(298, 6);
            this.chbx_PS4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_PS4.Name = "chbx_PS4";
            this.chbx_PS4.Size = new System.Drawing.Size(65, 24);
            this.chbx_PS4.TabIndex = 411;
            this.chbx_PS4.Text = "PS4";
            this.chbx_PS4.UseVisualStyleBackColor = true;
            // 
            // txt_NoOfSplits
            // 
            this.txt_NoOfSplits.DataSource = this.mainBindingSource;
            this.txt_NoOfSplits.FormattingEnabled = true;
            this.txt_NoOfSplits.Location = new System.Drawing.Point(501, 30);
            this.txt_NoOfSplits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_NoOfSplits.MaxDropDownItems = 100;
            this.txt_NoOfSplits.Name = "txt_NoOfSplits";
            this.txt_NoOfSplits.Size = new System.Drawing.Size(67, 28);
            this.txt_NoOfSplits.TabIndex = 410;
            this.toolTip1.SetToolTip(this.txt_NoOfSplits, "Setlists/Groups of CDLCs defined by the User.");
            this.txt_NoOfSplits.DropDown += new System.EventHandler(this.Txt_NoOfSplits_DropDown);
            this.txt_NoOfSplits.TextChanged += new System.EventHandler(this.txt_NoOfSplits_SelectedIndexChanged);
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataMember = "Main";
            // 
            // rbtn_Population_PackNO
            // 
            this.rbtn_Population_PackNO.AutoSize = true;
            this.rbtn_Population_PackNO.Checked = true;
            this.rbtn_Population_PackNO.Location = new System.Drawing.Point(435, 30);
            this.rbtn_Population_PackNO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtn_Population_PackNO.Name = "rbtn_Population_PackNO";
            this.rbtn_Population_PackNO.Size = new System.Drawing.Size(69, 24);
            this.rbtn_Population_PackNO.TabIndex = 409;
            this.rbtn_Population_PackNO.TabStop = true;
            this.rbtn_Population_PackNO.Text = "Pack";
            this.toolTip1.SetToolTip(this.rbtn_Population_PackNO, "You can Start Multiple instances of DLCManager(MainDB Button to manage this proce" +
        "ss) to speed up the packing process ");
            this.rbtn_Population_PackNO.UseVisualStyleBackColor = true;
            this.rbtn_Population_PackNO.CheckedChanged += new System.EventHandler(this.rbtn_Population_PackNO_CheckedChanged);
            // 
            // btm_GoRepack
            // 
            this.btm_GoRepack.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpKeyword(this.btm_GoRepack, "Open the folder contaning Repack-ed CDLCs.");
            this.btm_GoRepack.Location = new System.Drawing.Point(428, 3);
            this.btm_GoRepack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btm_GoRepack.Name = "btm_GoRepack";
            this.helpProvider1.SetShowHelp(this.btm_GoRepack, true);
            this.btm_GoRepack.Size = new System.Drawing.Size(33, 24);
            this.btm_GoRepack.TabIndex = 390;
            this.btm_GoRepack.Text = "->";
            this.btm_GoRepack.UseVisualStyleBackColor = true;
            this.btm_GoRepack.Click += new System.EventHandler(this.btm_GoRepack_Click);
            // 
            // lbl_NoRec2
            // 
            this.lbl_NoRec2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoRec2.Location = new System.Drawing.Point(336, 54);
            this.lbl_NoRec2.Name = "lbl_NoRec2";
            this.lbl_NoRec2.Size = new System.Drawing.Size(115, 29);
            this.lbl_NoRec2.TabIndex = 325;
            this.lbl_NoRec2.Text = " Records";
            this.toolTip1.SetToolTip(this.lbl_NoRec2, "Group/Selected//All");
            // 
            // chbx_XBOX360
            // 
            this.chbx_XBOX360.AutoSize = true;
            this.chbx_XBOX360.Location = new System.Drawing.Point(195, 6);
            this.chbx_XBOX360.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_XBOX360.Name = "chbx_XBOX360";
            this.chbx_XBOX360.Size = new System.Drawing.Size(107, 24);
            this.chbx_XBOX360.TabIndex = 23;
            this.chbx_XBOX360.Text = "XBOX360";
            this.chbx_XBOX360.UseVisualStyleBackColor = true;
            this.chbx_XBOX360.CheckStateChanged += new System.EventHandler(this.chbx_XBOX360_CheckedChanged);
            // 
            // btn_Debbug
            // 
            this.btn_Debbug.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.5F);
            this.btn_Debbug.Location = new System.Drawing.Point(454, 66);
            this.btn_Debbug.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Debbug.Name = "btn_Debbug";
            this.btn_Debbug.Size = new System.Drawing.Size(111, 34);
            this.btn_Debbug.TabIndex = 404;
            this.btn_Debbug.Text = "DebugRandomCode";
            this.btn_Debbug.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Debbug.UseVisualStyleBackColor = true;
            this.btn_Debbug.Visible = false;
            this.btn_Debbug.Click += new System.EventHandler(this.btn_Debbug_Click);
            // 
            // chbx_Mac
            // 
            this.chbx_Mac.AutoSize = true;
            this.chbx_Mac.Location = new System.Drawing.Point(126, 6);
            this.chbx_Mac.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_Mac.Name = "chbx_Mac";
            this.chbx_Mac.Size = new System.Drawing.Size(65, 24);
            this.chbx_Mac.TabIndex = 22;
            this.chbx_Mac.Text = "Mac";
            this.chbx_Mac.UseVisualStyleBackColor = true;
            this.chbx_Mac.CheckStateChanged += new System.EventHandler(this.chbx_Mac_CheckedChanged);
            // 
            // rbtn_Population_Groups
            // 
            this.rbtn_Population_Groups.AutoSize = true;
            this.rbtn_Population_Groups.Location = new System.Drawing.Point(54, 32);
            this.rbtn_Population_Groups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtn_Population_Groups.Name = "rbtn_Population_Groups";
            this.rbtn_Population_Groups.Size = new System.Drawing.Size(79, 24);
            this.rbtn_Population_Groups.TabIndex = 27;
            this.rbtn_Population_Groups.Text = "Group";
            this.toolTip1.SetToolTip(this.rbtn_Population_Groups, "Group songs then use this to Delete/Pack");
            this.rbtn_Population_Groups.UseVisualStyleBackColor = true;
            this.rbtn_Population_Groups.CheckedChanged += new System.EventHandler(this.rbtn_Population_Groups_CheckedChanged);
            // 
            // chbx_PS3
            // 
            this.chbx_PS3.AutoSize = true;
            this.chbx_PS3.Checked = true;
            this.chbx_PS3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_PS3.Location = new System.Drawing.Point(63, 6);
            this.chbx_PS3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_PS3.Name = "chbx_PS3";
            this.chbx_PS3.Size = new System.Drawing.Size(65, 24);
            this.chbx_PS3.TabIndex = 21;
            this.chbx_PS3.Text = "PS3";
            this.chbx_PS3.UseVisualStyleBackColor = true;
            this.chbx_PS3.TextChanged += new System.EventHandler(this.chbx_PS3_CheckedChanged);
            // 
            // cbx_Groups
            // 
            this.cbx_Groups.DataSource = this.mainBindingSource;
            this.cbx_Groups.Enabled = false;
            this.cbx_Groups.FormattingEnabled = true;
            this.cbx_Groups.Location = new System.Drawing.Point(135, 32);
            this.cbx_Groups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Groups.Name = "cbx_Groups";
            this.cbx_Groups.Size = new System.Drawing.Size(199, 28);
            this.cbx_Groups.TabIndex = 28;
            this.toolTip1.SetToolTip(this.cbx_Groups, "Setlists/Groups of CDLCs defined by the User.");
            this.cbx_Groups.DropDown += new System.EventHandler(this.cbx_Groups_DropDown);
            this.cbx_Groups.SelectedIndexChanged += new System.EventHandler(this.cbx_Groups_SelectedIndexChanged);
            // 
            // chbx_PC
            // 
            this.chbx_PC.AutoSize = true;
            this.chbx_PC.Location = new System.Drawing.Point(6, 6);
            this.chbx_PC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_PC.Name = "chbx_PC";
            this.chbx_PC.Size = new System.Drawing.Size(56, 24);
            this.chbx_PC.TabIndex = 20;
            this.chbx_PC.Text = "PC";
            this.chbx_PC.UseVisualStyleBackColor = true;
            this.chbx_PC.CheckStateChanged += new System.EventHandler(this.chbx_PC_CheckedChanged);
            // 
            // btn_Cleanup_MainDB
            // 
            this.btn_Cleanup_MainDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cleanup_MainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cleanup_MainDB.Location = new System.Drawing.Point(3, 66);
            this.btn_Cleanup_MainDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Cleanup_MainDB.Name = "btn_Cleanup_MainDB";
            this.btn_Cleanup_MainDB.Size = new System.Drawing.Size(117, 34);
            this.btn_Cleanup_MainDB.TabIndex = 15;
            this.btn_Cleanup_MainDB.Text = "Remove";
            this.toolTip1.SetToolTip(this.btn_Cleanup_MainDB, "Removed CDCL Imported from the DB and disk.");
            this.btn_Cleanup_MainDB.UseVisualStyleBackColor = false;
            this.btn_Cleanup_MainDB.Click += new System.EventHandler(this.btn_Cleanup_MainDB_Click);
            // 
            // btn_RePack
            // 
            this.btn_RePack.BackColor = System.Drawing.SystemColors.Control;
            this.btn_RePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RePack.Location = new System.Drawing.Point(123, 66);
            this.btn_RePack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RePack.Name = "btn_RePack";
            this.btn_RePack.Size = new System.Drawing.Size(111, 34);
            this.btn_RePack.TabIndex = 14;
            this.btn_RePack.Text = "RePack";
            this.toolTip1.SetToolTip(this.btn_RePack, "Mass Pack CDCLs. (Fixes audio, Gets spotify info and saves it, Marks as Brken if " +
        "it fails. All based on Optionns selected)");
            this.btn_RePack.UseVisualStyleBackColor = false;
            this.btn_RePack.Click += new System.EventHandler(this.Btn_RePack_Click);
            // 
            // rbtn_Population_All
            // 
            this.rbtn_Population_All.AutoSize = true;
            this.rbtn_Population_All.Location = new System.Drawing.Point(3, 32);
            this.rbtn_Population_All.Margin = new System.Windows.Forms.Padding(0);
            this.rbtn_Population_All.Name = "rbtn_Population_All";
            this.rbtn_Population_All.Size = new System.Drawing.Size(51, 24);
            this.rbtn_Population_All.TabIndex = 26;
            this.rbtn_Population_All.Text = "All";
            this.toolTip1.SetToolTip(this.rbtn_Population_All, "Pack/Delete All");
            this.rbtn_Population_All.UseVisualStyleBackColor = true;
            this.rbtn_Population_All.CheckedChanged += new System.EventHandler(this.rbtn_Population_All_CheckedChanged);
            // 
            // rbtn_Population_Selected
            // 
            this.rbtn_Population_Selected.AutoSize = true;
            this.rbtn_Population_Selected.Location = new System.Drawing.Point(339, 30);
            this.rbtn_Population_Selected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtn_Population_Selected.Name = "rbtn_Population_Selected";
            this.rbtn_Population_Selected.Size = new System.Drawing.Size(97, 24);
            this.rbtn_Population_Selected.TabIndex = 25;
            this.rbtn_Population_Selected.Text = "Selected";
            this.toolTip1.SetToolTip(this.rbtn_Population_Selected, "EAch song has a Selected attribute for quick \"Selections\"/list-ing");
            this.rbtn_Population_Selected.UseVisualStyleBackColor = true;
            this.rbtn_Population_Selected.CheckedChanged += new System.EventHandler(this.rbtn_Population_Selected_CheckedChanged);
            // 
            // chbx_Rebuild
            // 
            this.chbx_Rebuild.AutoSize = true;
            this.chbx_Rebuild.Enabled = false;
            this.chbx_Rebuild.Location = new System.Drawing.Point(240, 59);
            this.chbx_Rebuild.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_Rebuild.Name = "chbx_Rebuild";
            this.chbx_Rebuild.Size = new System.Drawing.Size(89, 24);
            this.chbx_Rebuild.TabIndex = 324;
            this.chbx_Rebuild.Text = "Rebuild";
            this.chbx_Rebuild.UseVisualStyleBackColor = true;
            this.chbx_Rebuild.Visible = false;
            this.chbx_Rebuild.CheckedChanged += new System.EventHandler(this.chbx_Rebuild_CheckedChanged);
            // 
            // chbx_DefaultDB
            // 
            this.chbx_DefaultDB.AutoSize = true;
            this.chbx_DefaultDB.Location = new System.Drawing.Point(503, 68);
            this.chbx_DefaultDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx_DefaultDB.Name = "chbx_DefaultDB";
            this.chbx_DefaultDB.Size = new System.Drawing.Size(143, 24);
            this.chbx_DefaultDB.TabIndex = 19;
            this.chbx_DefaultDB.Text = "Use DefaultDB";
            this.toolTip1.SetToolTip(this.chbx_DefaultDB, "Uses the Default DB stored in the App folder");
            this.chbx_DefaultDB.UseVisualStyleBackColor = true;
            this.chbx_DefaultDB.Visible = false;
            this.chbx_DefaultDB.CheckedChanged += new System.EventHandler(this.chbx_DefaultDB_CheckedChanged);
            // 
            // Export_To
            // 
            this.Export_To.BackColor = System.Drawing.SystemColors.Control;
            this.Export_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Export_To.Location = new System.Drawing.Point(110, 240);
            this.Export_To.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Export_To.Name = "Export_To";
            this.Export_To.Size = new System.Drawing.Size(84, 39);
            this.Export_To.TabIndex = 56;
            this.Export_To.Text = "Export as";
            this.toolTip1.SetToolTip(this.Export_To, "Export Access Db and connected files as ...");
            this.Export_To.UseVisualStyleBackColor = false;
            this.Export_To.Click += new System.EventHandler(this.Export_To_Click);
            // 
            // chbx_Additional_Manipulations
            // 
            this.chbx_Additional_Manipulations.CheckOnClick = true;
            this.chbx_Additional_Manipulations.FormattingEnabled = true;
            this.chbx_Additional_Manipulations.Items.AddRange(new object[] {
            "00. @Pack Add Increment to all songs Title",
            "01. @Pack Add Increment to all songs Title per artist",
            "02. @Pack Make all DLC IDs unique (&save)",
            "03. @Pack Remove DD",
            "04. Backup DB during Startup",
            "05. @Pack Remove DD only for Bass Guitar",
            "06. When converting Audio use local folder structure",
            "07. @Pack skip Broken songs",
            "08. @Pack Name to cross-platform Compatible Filenames",
            "09. @Pack Add Preview if missing 00:30 for 30sec (&save)",
            "10. @Pack Make all DLC IDs unique",
            "11. <@PackAdd DD (5 Levels)>",
            "12. Add DD (5 Levels) when missing",
            "13. Import all Duplicates as Alternates",
            "14. Import any Custom as Alternate if an Original exists",
            "15. Move the Imported files to temp/0_old",
            "16. Import with Artist/Title same as Artist/Title/Album Sort",
            "17. Repack with Artist/Title same as Artist/Title/Album Sort",
            "18. <Import without The/Die at the beginning of Artist/Title Sort>",
            "19. <Pack without The/Die at the beginning of Artist/Title Sort>",
            "20. Import with the The/Die at the end of Title Sort",
            "21. Pack with The/Die at the end of Title/Title Sort",
            "22. Import with the The/Die only at the end of Artist/Album/xx Sort",
            "23. Pack with The/Die only at the end of Artist/Album/xx Sort",
            "24. @Import Use translation tables for naming standardization",
            "25. If Original don\'t add QAs (NOs;DLC/ORIG;etc.)",
            "26. When packing Add 5 Levels of DD only to Guitar tracks",
            "27. Convert and Transfer/FTP",
            "28. If Original don\'t add QAs (NOs;DLC/ORIG;etc.) except for File Names",
            "29. When NOT importing a Duplicate Move it to _duplicate",
            "30. When NOT importing a broken song Move it to _broken",
            "31. When removing DD use internal logic not DDC",
            "32. When importing alternates add newer/older instead of alt.0author",
            "33. Forcibly Update Import location of all DB fields",
            "34. @Import Add Preview if missing (lenght> as per config)",
            "35. Remove illegal characters from Songs Metadata",
            "36. Keep the Uncompressed Songs superorganized",
            "37. Import other formats but PC",
            "38. Import only the unpacked songs already in the \"0/\" Temp folder",
            "39. Encrypt PS3 Retails songs, with External tool",
            "40. Delete ORIG HSAN/OGG when Packing Retails songs",
            "41. Try to get Track No. &Details from Spotify (&yb links)",
            "42. Save Log After Import (Imported Folder)",
            "43. @Import Set the DLCID autom",
            "44. @Pack Set the DLCID autom",
            "45. <Convert Originals>",
            "46. Duplicate Mangement, Title added info is inbetween separators: []",
            "47. Add New Toolkit v. and RePackedByAuthor",
            "48. @Import Remove Multitrack/Live/Acoustic info from Title",
            "49. @Pack Also Copy/FTP",
            "50. @Import Manually assess duplicates at the end",
            "51. @Import&Unpack Overwrite the XML",
            "52. @Pack keep Bass DD if indicated so",
            "53. @Pack keep All DD if indicated so",
            "54. @Pack consider All songs as beta (place them top of the list)",
            "55. Gen Preview if Preview=Audio or Preview is longer than config (default 30s)",
            "56. Duplicate manag ignores Multitracks",
            "57. Don\'t save Author when generic (i.e. Custom Song Creator)",
            "58. @Pack try to get Track No again (&don\'t save)",
            "59. @Pack try to get Track No again (&save)",
            "60. @Rebuild don\'t overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)",
            "61. @Rebuild don\'t overwrite Standard Song Info (Cover,Year)",
            "62. <@Pack duplicate singleTracks L->R / R->L>",
            "63. @Pack Remove Remote File if GameData has been read",
            "64. @Pack ONLY Copy/FTP the Last Packed song",
            "65. @Pack ONLY Copy/FTP the Initially Imported song",
            "66. Duplicate manag. ignores Live Songs",
            "67. Import duplicates (hash)",
            "68. Delete obvious duplicates (hash) during dupli assesment",
            "69. Compress AudioFiles to 128VBR @Pack/Import if bigger than 136k",
            "70. @Repack pack Preview (bugfix)",
            "71. <@Import/Repack check if Original flag is in the Official list and correct>",
            "72. Import other formats but PC, as standalone",
            "73. Add Track Info&Comments beginning of Lyrics",
            "74. Add Track start into Vocals",
            "75. Copy to \\0\\0_Old (Overwrites 15 Move to old)",
            "76. Include Tones/arangements Db changes",
            "77. After Import open MainDB",
            "78. @Import Fix Audio Issues at end",
            "79. @Import Manually Asses All Suspicious Duplicates",
            "80. Duplicate manag. ignores Acoustic Songs",
            "81. Any Delete (non psarc) goes to RecycleBin",
            "82. Show warning that It will connect to Spotify",
            "83. Import All suspicious Duplicates as Duplicates (Ignore)",
            "84. When checking Songs validate wem bitrate (10% wem conversion raises the bitra" +
                "te)",
            "85. Apply standard naming to all duplicates",
            "86. Keep XML Manipulations",
            "87. Use Latest Spotify API (Web)",
            "88. Gen Preview if Preview is shorter than config (default 10s)",
            "89. @Mass pack split into xxx (param in xml) songs",
            "90. When adding times into vocals(74) add only in seconds",
            "91. Add group to Filename",
            "92. Package for a HAN enabled PS3",
            "93. If packaging for a HAN Enabled PS3 then also copy Retail(RS2012) Songs",
            "94. After lyrics manipulation Open them in Notepad",
            "95. @Export create Package (in@0_temp)",
            "96. @Export create Tabs",
            "97. Pack only never packed Songs (Overwrites 98)",
            "98. Pack only never packed Songs for the target Platform",
            "99. <If Group pack ignore songs that are also in other Groups>",
            "x100. <Pack anew instead of converting (e.g. Pack Orig file)>",
            "x101. After packing check song",
            "<102. >",
            "<103. >",
            "<104. >",
            "<105. >",
            "<106. >",
            "<107. >",
            "<108. >",
            "<109. >",
            "<110. >",
            "<111. >",
            "<112. >",
            "<113. >",
            "<114. >",
            "<115. >",
            "<116. >",
            "<117. >",
            "<118. >",
            "<119. >",
            "<120. >",
            "<121. >",
            "<122. >",
            "<123. >",
            "<124. >",
            "<125. >",
            "<126. >",
            "<127. >",
            "<128. >",
            "<129. >",
            "<130. >",
            "<131. >",
            "<132. >",
            "<133. >",
            "<134. >",
            "<135. >",
            "<136. >",
            "<137. >",
            "<138. >",
            "<139. >",
            "<140. >",
            "<141. >",
            "<142. >",
            "<143. >",
            "<144. >",
            "<145. >",
            "<146. >",
            "<147. >",
            "<148. >",
            "<149. >",
            "<150. >",
            "<151. >",
            "<152. >",
            "<153. >",
            "<154. >",
            "<155. >",
            "<156. >",
            "<157. >",
            "<158. >",
            "<159. >",
            "<160. >",
            "<161. >",
            "<162. >",
            "<163. >",
            "<164. >",
            "<165. >",
            "<166. >",
            "<167. >",
            "<168. >",
            "<169. >",
            "<170. >",
            "<171. >",
            "<172. >",
            "<173. >",
            "<174. >",
            "<175. >",
            "<176. >",
            "<177. >",
            "<178. >",
            "<179. >",
            "<180. >",
            "<181. >",
            "<182. >",
            "<183. >",
            "<184. >",
            "<185. >",
            "<186. >",
            "<187. >",
            "<188. >",
            "<189. >",
            "<190. >",
            "<191. >",
            "<192. >",
            "<193. >",
            "<194. >",
            "<195. >",
            "<196. >",
            "<197. >",
            "<198. >",
            "<199. >",
            "<200. >"});
            this.chbx_Additional_Manipulations.Location = new System.Drawing.Point(12, 538);
            this.chbx_Additional_Manipulations.Margin = new System.Windows.Forms.Padding(0);
            this.chbx_Additional_Manipulations.Name = "chbx_Additional_Manipulations";
            this.chbx_Additional_Manipulations.Size = new System.Drawing.Size(697, 165);
            this.chbx_Additional_Manipulations.TabIndex = 29;
            this.chbx_Additional_Manipulations.Visible = false;
            this.chbx_Additional_Manipulations.SelectedIndexChanged += new System.EventHandler(this.chbx_Additional_Manipualtions_SelectedIndexChanged);
            // 
            // lbl_Mask
            // 
            this.lbl_Mask.AutoSize = true;
            this.lbl_Mask.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Mask.Location = new System.Drawing.Point(9, 510);
            this.lbl_Mask.Name = "lbl_Mask";
            this.lbl_Mask.Size = new System.Drawing.Size(109, 20);
            this.lbl_Mask.TabIndex = 319;
            this.lbl_Mask.Text = "Mask Preview:";
            this.lbl_Mask.Visible = false;
            // 
            // btn_Preview_Artist_Sort
            // 
            this.btn_Preview_Artist_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist_Sort.Location = new System.Drawing.Point(783, 372);
            this.btn_Preview_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Artist_Sort.Name = "btn_Preview_Artist_Sort";
            this.btn_Preview_Artist_Sort.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Artist_Sort.TabIndex = 45;
            this.toolTip1.SetToolTip(this.btn_Preview_Artist_Sort, "Preview Artist Sort Mask");
            this.btn_Preview_Artist_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist_Sort.Click += new System.EventHandler(this.btn_Preview_Artist_Sort_Click);
            // 
            // cbx_Activ_Artist_Sort
            // 
            this.cbx_Activ_Artist_Sort.AutoSize = true;
            this.cbx_Activ_Artist_Sort.Checked = true;
            this.cbx_Activ_Artist_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist_Sort.Location = new System.Drawing.Point(759, 372);
            this.cbx_Activ_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Artist_Sort.Name = "cbx_Activ_Artist_Sort";
            this.cbx_Activ_Artist_Sort.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Artist_Sort.TabIndex = 44;
            this.cbx_Activ_Artist_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist_Sort.Visible = false;
            this.cbx_Activ_Artist_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_Sort_CheckedChanged);
            // 
            // lbl_Artist_Sort
            // 
            this.lbl_Artist_Sort.AutoSize = true;
            this.lbl_Artist_Sort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Artist_Sort.Location = new System.Drawing.Point(9, 374);
            this.lbl_Artist_Sort.Name = "lbl_Artist_Sort";
            this.lbl_Artist_Sort.Size = new System.Drawing.Size(80, 20);
            this.lbl_Artist_Sort.TabIndex = 316;
            this.lbl_Artist_Sort.Text = "Artist Sort";
            this.lbl_Artist_Sort.Visible = false;
            // 
            // cbx_Artist_Sort
            // 
            this.cbx_Artist_Sort.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Artist_Sort.Location = new System.Drawing.Point(597, 368);
            this.cbx_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Artist_Sort.Name = "cbx_Artist_Sort";
            this.cbx_Artist_Sort.Size = new System.Drawing.Size(157, 28);
            this.cbx_Artist_Sort.TabIndex = 43;
            this.cbx_Artist_Sort.Visible = false;
            this.cbx_Artist_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_Sort_SelectedIndexChanged);
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Location = new System.Drawing.Point(102, 368);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(490, 26);
            this.txt_Artist_Sort.TabIndex = 42;
            this.txt_Artist_Sort.Text = "<Beta><Artist>";
            this.txt_Artist_Sort.Visible = false;
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(9, 206);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(795, 28);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 263;
            this.toolTip1.SetToolTip(this.pB_ReadDLCs, "Progress bar for different operations of CDLC Manager.");
            // 
            // btn_Preview_File_Name
            // 
            this.btn_Preview_File_Name.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_File_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_File_Name.Location = new System.Drawing.Point(783, 454);
            this.btn_Preview_File_Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_File_Name.Name = "btn_Preview_File_Name";
            this.btn_Preview_File_Name.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_File_Name.TabIndex = 53;
            this.toolTip1.SetToolTip(this.btn_Preview_File_Name, "Preview File Name Mask");
            this.btn_Preview_File_Name.UseVisualStyleBackColor = false;
            this.btn_Preview_File_Name.Click += new System.EventHandler(this.btn_Preview_File_Name_Click);
            // 
            // btn_Preview_Album
            // 
            this.btn_Preview_Album.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Album.Location = new System.Drawing.Point(783, 399);
            this.btn_Preview_Album.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Album.Name = "btn_Preview_Album";
            this.btn_Preview_Album.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Album.TabIndex = 49;
            this.toolTip1.SetToolTip(this.btn_Preview_Album, "Preview Album Mask");
            this.btn_Preview_Album.UseVisualStyleBackColor = false;
            this.btn_Preview_Album.Click += new System.EventHandler(this.btn_Preview_Album_Click);
            // 
            // btn_Preview_Artist
            // 
            this.btn_Preview_Artist.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist.Location = new System.Drawing.Point(783, 342);
            this.btn_Preview_Artist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Artist.Name = "btn_Preview_Artist";
            this.btn_Preview_Artist.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Artist.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btn_Preview_Artist, "Preview Artist Mask");
            this.btn_Preview_Artist.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist.Click += new System.EventHandler(this.btn_Preview_Artist_Click);
            // 
            // btn_Preview_Title_Sort
            // 
            this.btn_Preview_Title_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title_Sort.Location = new System.Drawing.Point(783, 314);
            this.btn_Preview_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Title_Sort.Name = "btn_Preview_Title_Sort";
            this.btn_Preview_Title_Sort.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Title_Sort.TabIndex = 37;
            this.toolTip1.SetToolTip(this.btn_Preview_Title_Sort, "Preview Title Sort Mask");
            this.btn_Preview_Title_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Title_Sort.Click += new System.EventHandler(this.btn_Preview_Title_Sort_Click);
            // 
            // btn_Preview_Title
            // 
            this.btn_Preview_Title.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title.Location = new System.Drawing.Point(783, 285);
            this.btn_Preview_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Title.Name = "btn_Preview_Title";
            this.btn_Preview_Title.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Title.TabIndex = 33;
            this.toolTip1.SetToolTip(this.btn_Preview_Title, "Preview Title Mask");
            this.btn_Preview_Title.UseVisualStyleBackColor = false;
            this.btn_Preview_Title.Click += new System.EventHandler(this.btn_Preview_Title_Click);
            // 
            // cbx_Activ_File_Name
            // 
            this.cbx_Activ_File_Name.AutoSize = true;
            this.cbx_Activ_File_Name.Checked = true;
            this.cbx_Activ_File_Name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_File_Name.Location = new System.Drawing.Point(759, 454);
            this.cbx_Activ_File_Name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_File_Name.Name = "cbx_Activ_File_Name";
            this.cbx_Activ_File_Name.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_File_Name.TabIndex = 52;
            this.cbx_Activ_File_Name.UseVisualStyleBackColor = true;
            this.cbx_Activ_File_Name.Visible = false;
            this.cbx_Activ_File_Name.CheckedChanged += new System.EventHandler(this.cbx_Activ_File_Name_CheckedChanged);
            // 
            // cbx_Activ_Album
            // 
            this.cbx_Activ_Album.AutoSize = true;
            this.cbx_Activ_Album.Checked = true;
            this.cbx_Activ_Album.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Album.Location = new System.Drawing.Point(759, 402);
            this.cbx_Activ_Album.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Album.Name = "cbx_Activ_Album";
            this.cbx_Activ_Album.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Album.TabIndex = 48;
            this.cbx_Activ_Album.UseVisualStyleBackColor = true;
            this.cbx_Activ_Album.Visible = false;
            this.cbx_Activ_Album.CheckedChanged += new System.EventHandler(this.cbx_Activ_Album_CheckedChanged);
            // 
            // cbx_Activ_Artist
            // 
            this.cbx_Activ_Artist.AutoSize = true;
            this.cbx_Activ_Artist.Checked = true;
            this.cbx_Activ_Artist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist.Location = new System.Drawing.Point(759, 342);
            this.cbx_Activ_Artist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Artist.Name = "cbx_Activ_Artist";
            this.cbx_Activ_Artist.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Artist.TabIndex = 40;
            this.cbx_Activ_Artist.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist.Visible = false;
            this.cbx_Activ_Artist.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_CheckedChanged);
            // 
            // cbx_Activ_Title_Sort
            // 
            this.cbx_Activ_Title_Sort.AutoSize = true;
            this.cbx_Activ_Title_Sort.Checked = true;
            this.cbx_Activ_Title_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title_Sort.Location = new System.Drawing.Point(759, 315);
            this.cbx_Activ_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Title_Sort.Name = "cbx_Activ_Title_Sort";
            this.cbx_Activ_Title_Sort.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Title_Sort.TabIndex = 36;
            this.cbx_Activ_Title_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title_Sort.Visible = false;
            this.cbx_Activ_Title_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_Sort_CheckedChanged);
            // 
            // cbx_Activ_Title
            // 
            this.cbx_Activ_Title.AutoSize = true;
            this.cbx_Activ_Title.Checked = true;
            this.cbx_Activ_Title.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title.Location = new System.Drawing.Point(759, 288);
            this.cbx_Activ_Title.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Title.Name = "cbx_Activ_Title";
            this.cbx_Activ_Title.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Title.TabIndex = 32;
            this.cbx_Activ_Title.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title.Visible = false;
            this.cbx_Activ_Title.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_CheckedChanged);
            // 
            // lbl_Artist
            // 
            this.lbl_Artist.AutoSize = true;
            this.lbl_Artist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Artist.Location = new System.Drawing.Point(9, 342);
            this.lbl_Artist.Name = "lbl_Artist";
            this.lbl_Artist.Size = new System.Drawing.Size(46, 20);
            this.lbl_Artist.TabIndex = 303;
            this.lbl_Artist.Text = "Artist";
            this.lbl_Artist.Visible = false;
            // 
            // cbx_Artist
            // 
            this.cbx_Artist.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Artist.Location = new System.Drawing.Point(597, 339);
            this.cbx_Artist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Artist.Name = "cbx_Artist";
            this.cbx_Artist.Size = new System.Drawing.Size(157, 28);
            this.cbx_Artist.TabIndex = 39;
            this.cbx_Artist.Visible = false;
            this.cbx_Artist.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_SelectedIndexChanged);
            // 
            // txt_Artist
            // 
            this.txt_Artist.Location = new System.Drawing.Point(102, 339);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(490, 26);
            this.txt_Artist.TabIndex = 38;
            this.txt_Artist.Text = "<Artist>-<CDLC>-<Avail. Tracks>-<DD>-<QAs>-<Bass_HasDD>";
            this.txt_Artist.Visible = false;
            // 
            // cbx_Title
            // 
            this.cbx_Title.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Title.Location = new System.Drawing.Point(597, 282);
            this.cbx_Title.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Title.Name = "cbx_Title";
            this.cbx_Title.Size = new System.Drawing.Size(157, 28);
            this.cbx_Title.TabIndex = 31;
            this.cbx_Title.Visible = false;
            this.cbx_Title.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_SelectedIndexChanged);
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(102, 282);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(490, 26);
            this.txt_Title.TabIndex = 30;
            this.txt_Title.Text = "<Title>";
            this.txt_Title.Visible = false;
            // 
            // lbl_File_Name
            // 
            this.lbl_File_Name.AutoSize = true;
            this.lbl_File_Name.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_File_Name.Location = new System.Drawing.Point(9, 453);
            this.lbl_File_Name.Name = "lbl_File_Name";
            this.lbl_File_Name.Size = new System.Drawing.Size(80, 20);
            this.lbl_File_Name.TabIndex = 297;
            this.lbl_File_Name.Text = "File Name";
            this.lbl_File_Name.Visible = false;
            // 
            // lbl_Album
            // 
            this.lbl_Album.AutoSize = true;
            this.lbl_Album.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Album.Location = new System.Drawing.Point(9, 402);
            this.lbl_Album.Name = "lbl_Album";
            this.lbl_Album.Size = new System.Drawing.Size(54, 20);
            this.lbl_Album.TabIndex = 296;
            this.lbl_Album.Text = "Album";
            this.lbl_Album.Visible = false;
            // 
            // lbl_Title_Sort
            // 
            this.lbl_Title_Sort.AutoSize = true;
            this.lbl_Title_Sort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title_Sort.Location = new System.Drawing.Point(9, 312);
            this.lbl_Title_Sort.Name = "lbl_Title_Sort";
            this.lbl_Title_Sort.Size = new System.Drawing.Size(72, 20);
            this.lbl_Title_Sort.TabIndex = 295;
            this.lbl_Title_Sort.Text = "Title Sort";
            this.lbl_Title_Sort.Visible = false;
            // 
            // cbx_File_Name
            // 
            this.cbx_File_Name.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_File_Name.Location = new System.Drawing.Point(597, 448);
            this.cbx_File_Name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_File_Name.Name = "cbx_File_Name";
            this.cbx_File_Name.Size = new System.Drawing.Size(157, 28);
            this.cbx_File_Name.TabIndex = 51;
            this.cbx_File_Name.Visible = false;
            this.cbx_File_Name.SelectedIndexChanged += new System.EventHandler(this.cbx_File_Name_SelectedIndexChanged);
            // 
            // txt_File_Name
            // 
            this.txt_File_Name.Location = new System.Drawing.Point(102, 448);
            this.txt_File_Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_File_Name.Name = "txt_File_Name";
            this.txt_File_Name.Size = new System.Drawing.Size(490, 26);
            this.txt_File_Name.TabIndex = 50;
            this.txt_File_Name.Text = "<CDLC>-<Artist>-<Year>-<Album><Track No.>-<Title>-<DD>-<Avail. Tracks>-<QAs>-v<Ve" +
    "rsion>-<Bass_HasDD>";
            this.txt_File_Name.Visible = false;
            // 
            // cbx_Album
            // 
            this.cbx_Album.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Album.Location = new System.Drawing.Point(597, 396);
            this.cbx_Album.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Album.Name = "cbx_Album";
            this.cbx_Album.Size = new System.Drawing.Size(157, 28);
            this.cbx_Album.TabIndex = 47;
            this.cbx_Album.Visible = false;
            this.cbx_Album.SelectedIndexChanged += new System.EventHandler(this.cbx_Album_SelectedIndexChanged);
            // 
            // txt_Album
            // 
            this.txt_Album.Location = new System.Drawing.Point(102, 396);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(490, 26);
            this.txt_Album.TabIndex = 46;
            this.txt_Album.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            this.txt_Album.Visible = false;
            // 
            // cbx_Title_Sort
            // 
            this.cbx_Title_Sort.DropDownWidth = 200;
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Title_Sort.Location = new System.Drawing.Point(597, 310);
            this.cbx_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Title_Sort.Name = "cbx_Title_Sort";
            this.cbx_Title_Sort.Size = new System.Drawing.Size(157, 28);
            this.cbx_Title_Sort.TabIndex = 35;
            this.cbx_Title_Sort.Visible = false;
            this.cbx_Title_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_Sort_SelectedIndexChanged);
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Location = new System.Drawing.Point(102, 310);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(490, 26);
            this.txt_Title_Sort.TabIndex = 34;
            this.txt_Title_Sort.Text = "<Year><Album><Track No.><Title>";
            this.txt_Title_Sort.Visible = false;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(8, 285);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(38, 20);
            this.lbl_Title.TabIndex = 300;
            this.lbl_Title.Text = "Title";
            this.lbl_Title.Visible = false;
            // 
            // chbx_DebugB
            // 
            this.chbx_DebugB.AutoSize = true;
            this.chbx_DebugB.Location = new System.Drawing.Point(464, 6);
            this.chbx_DebugB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_DebugB.Name = "chbx_DebugB";
            this.chbx_DebugB.Size = new System.Drawing.Size(106, 24);
            this.chbx_DebugB.TabIndex = 16;
            this.chbx_DebugB.Text = "Advanced";
            this.toolTip1.SetToolTip(this.chbx_DebugB, "Activates a series of Advanced Options. \r\nDisables also:\r\n25. Import Use translat" +
        "ion tables for naming standardization\r\n16. Move the Imported files to temp/0_old" +
        "\r\n50. Pack Also Copy/FTP");
            this.chbx_DebugB.UseVisualStyleBackColor = true;
            this.chbx_DebugB.CheckedChanged += new System.EventHandler(this.chbx_DebugB_CheckedChanged);
            // 
            // txt_DBFolder
            // 
            this.txt_DBFolder.Location = new System.Drawing.Point(189, 66);
            this.txt_DBFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_DBFolder.Name = "txt_DBFolder";
            this.txt_DBFolder.Size = new System.Drawing.Size(235, 26);
            this.txt_DBFolder.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txt_DBFolder, "Requires a Access 2010 DB.");
            this.txt_DBFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_DBFolder_KeyDown);
            this.txt_DBFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_DBFolder_KeyPress);
            this.txt_DBFolder.Leave += new System.EventHandler(this.txt_DBFolder_Leave);
            // 
            // btn_DBFolder
            // 
            this.btn_DBFolder.Location = new System.Drawing.Point(428, 66);
            this.btn_DBFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btn_DBFolder.Name = "btn_DBFolder";
            this.btn_DBFolder.Size = new System.Drawing.Size(33, 30);
            this.btn_DBFolder.TabIndex = 9;
            this.btn_DBFolder.Text = "...";
            this.toolTip1.SetToolTip(this.btn_DBFolder, "Select an M$ Access DB File to store the CDLC & User metadata.");
            this.btn_DBFolder.UseVisualStyleBackColor = true;
            this.btn_DBFolder.Click += new System.EventHandler(this.btn_DBFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 274;
            this.label1.Text = "DB Folder Path";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(714, 240);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(90, 39);
            this.btn_Close.TabIndex = 55;
            this.btn_Close.Text = "Close";
            this.toolTip1.SetToolTip(this.btn_Close, "Revert back to Normal Rocksmith ToolKit functions.");
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_RocksmithDLCPath
            // 
            this.txt_RocksmithDLCPath.Location = new System.Drawing.Point(189, 4);
            this.txt_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_RocksmithDLCPath.Name = "txt_RocksmithDLCPath";
            this.txt_RocksmithDLCPath.Size = new System.Drawing.Size(235, 26);
            this.txt_RocksmithDLCPath.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txt_RocksmithDLCPath, "Import Location.\r\nIf it is the Rocksmith Location then Files will be moved out an" +
        "d only replaced with Manged version at Repack.");
            this.txt_RocksmithDLCPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_RocksmithDLCPath_KeyPress);
            this.txt_RocksmithDLCPath.Leave += new System.EventHandler(this.txt_RocksmithDLCPath_Leave);
            // 
            // txt_TempPath
            // 
            this.txt_TempPath.Location = new System.Drawing.Point(189, 34);
            this.txt_TempPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_TempPath.Name = "txt_TempPath";
            this.txt_TempPath.Size = new System.Drawing.Size(235, 26);
            this.txt_TempPath.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txt_TempPath, resources.GetString("txt_TempPath.ToolTip"));
            this.txt_TempPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TempPath_KeyPress);
            // 
            // chbx_CleanTemp
            // 
            this.chbx_CleanTemp.AutoSize = true;
            this.chbx_CleanTemp.Location = new System.Drawing.Point(492, 38);
            this.chbx_CleanTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx_CleanTemp.Name = "chbx_CleanTemp";
            this.chbx_CleanTemp.Size = new System.Drawing.Size(154, 24);
            this.chbx_CleanTemp.TabIndex = 17;
            this.chbx_CleanTemp.Text = "Clean DB&&Temp";
            this.toolTip1.SetToolTip(this.chbx_CleanTemp, "Cleans the Temp, Old, Duplicate, Repacked, Broken Folders");
            this.chbx_CleanTemp.UseVisualStyleBackColor = true;
            this.chbx_CleanTemp.Visible = false;
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(428, 6);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(33, 30);
            this.btn_SteamDLCFolder.TabIndex = 3;
            this.btn_SteamDLCFolder.Text = "...";
            this.toolTip1.SetToolTip(this.btn_SteamDLCFolder, "Select an Importing CDLC Folder");
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // rtxt_StatisticsOnReadDLCs
            // 
            this.rtxt_StatisticsOnReadDLCs.Location = new System.Drawing.Point(10, 728);
            this.rtxt_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxt_StatisticsOnReadDLCs.Name = "rtxt_StatisticsOnReadDLCs";
            this.rtxt_StatisticsOnReadDLCs.Size = new System.Drawing.Size(790, 106);
            this.rtxt_StatisticsOnReadDLCs.TabIndex = 264;
            this.rtxt_StatisticsOnReadDLCs.Text = "";
            this.rtxt_StatisticsOnReadDLCs.Visible = false;
            // 
            // btn_PopulateDB
            // 
            this.btn_PopulateDB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_PopulateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PopulateDB.Location = new System.Drawing.Point(652, 90);
            this.btn_PopulateDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PopulateDB.Name = "btn_PopulateDB";
            this.btn_PopulateDB.Size = new System.Drawing.Size(152, 36);
            this.btn_PopulateDB.TabIndex = 13;
            this.btn_PopulateDB.Text = "Import DLCs";
            this.toolTip1.SetToolTip(this.btn_PopulateDB, resources.GetString("btn_PopulateDB.ToolTip"));
            this.btn_PopulateDB.UseVisualStyleBackColor = false;
            this.btn_PopulateDB.Click += new System.EventHandler(this.btn_PopulateDB_Click);
            // 
            // btn_OpenMainDB
            // 
            this.btn_OpenMainDB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_OpenMainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenMainDB.Location = new System.Drawing.Point(652, 38);
            this.btn_OpenMainDB.Margin = new System.Windows.Forms.Padding(0);
            this.btn_OpenMainDB.Name = "btn_OpenMainDB";
            this.btn_OpenMainDB.Size = new System.Drawing.Size(152, 51);
            this.btn_OpenMainDB.TabIndex = 10;
            this.btn_OpenMainDB.Text = "Open MainDB";
            this.toolTip1.SetToolTip(this.btn_OpenMainDB, "Main DB Listing All Imported CDLC");
            this.btn_OpenMainDB.UseVisualStyleBackColor = false;
            this.btn_OpenMainDB.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // lbl_RocksmithDLCPath
            // 
            this.lbl_RocksmithDLCPath.AutoSize = true;
            this.lbl_RocksmithDLCPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_RocksmithDLCPath.Location = new System.Drawing.Point(9, 6);
            this.lbl_RocksmithDLCPath.Name = "lbl_RocksmithDLCPath";
            this.lbl_RocksmithDLCPath.Size = new System.Drawing.Size(148, 20);
            this.lbl_RocksmithDLCPath.TabIndex = 259;
            this.lbl_RocksmithDLCPath.Text = "Importing DLC path";
            // 
            // btn_TempPath
            // 
            this.btn_TempPath.Location = new System.Drawing.Point(428, 36);
            this.btn_TempPath.Margin = new System.Windows.Forms.Padding(0);
            this.btn_TempPath.Name = "btn_TempPath";
            this.btn_TempPath.Size = new System.Drawing.Size(33, 30);
            this.btn_TempPath.TabIndex = 6;
            this.btn_TempPath.Text = "...";
            this.toolTip1.SetToolTip(this.btn_TempPath, resources.GetString("btn_TempPath.ToolTip"));
            this.btn_TempPath.UseVisualStyleBackColor = true;
            this.btn_TempPath.Click += new System.EventHandler(this.btn_TempPath_Click);
            // 
            // lbl_TempFolders
            // 
            this.lbl_TempFolders.AutoSize = true;
            this.lbl_TempFolders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TempFolders.Location = new System.Drawing.Point(6, 38);
            this.lbl_TempFolders.Name = "lbl_TempFolders";
            this.lbl_TempFolders.Size = new System.Drawing.Size(135, 20);
            this.lbl_TempFolders.TabIndex = 258;
            this.lbl_TempFolders.Text = "Temp Folder Path";
            // 
            // lbl_PreviewText
            // 
            this.lbl_PreviewText.AutoEllipsis = true;
            this.lbl_PreviewText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PreviewText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_PreviewText.Location = new System.Drawing.Point(129, 510);
            this.lbl_PreviewText.Name = "lbl_PreviewText";
            this.lbl_PreviewText.Size = new System.Drawing.Size(672, 28);
            this.lbl_PreviewText.TabIndex = 285;
            this.lbl_PreviewText.Text = "FN: Beta(0)CDLC/ORIG-Artist-Year-Album-TrackNo(ifexisting)-Title-TrackAvail(LRBVS" +
    ")-Version.psarc";
            this.lbl_PreviewText.Visible = false;
            // 
            // btn_ApplyStandardization
            // 
            this.btn_ApplyStandardization.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ApplyStandardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ApplyStandardization.Location = new System.Drawing.Point(588, 128);
            this.btn_ApplyStandardization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ApplyStandardization.Name = "btn_ApplyStandardization";
            this.btn_ApplyStandardization.Size = new System.Drawing.Size(21, 72);
            this.btn_ApplyStandardization.TabIndex = 61;
            this.btn_ApplyStandardization.Text = "Apply";
            this.toolTip1.SetToolTip(this.btn_ApplyStandardization, "Apply Standardization Rules");
            this.btn_ApplyStandardization.UseVisualStyleBackColor = false;
            this.btn_ApplyStandardization.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_LoadRetailSongs
            // 
            this.btn_LoadRetailSongs.BackColor = System.Drawing.SystemColors.Control;
            this.btn_LoadRetailSongs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LoadRetailSongs.Location = new System.Drawing.Point(714, 128);
            this.btn_LoadRetailSongs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LoadRetailSongs.Name = "btn_LoadRetailSongs";
            this.btn_LoadRetailSongs.Size = new System.Drawing.Size(21, 72);
            this.btn_LoadRetailSongs.TabIndex = 62;
            this.btn_LoadRetailSongs.Text = "Load";
            this.toolTip1.SetToolTip(this.btn_LoadRetailSongs, "Load Retail files: Disc Songs, DLC Songs, RS1DLC Songs");
            this.btn_LoadRetailSongs.UseVisualStyleBackColor = false;
            this.btn_LoadRetailSongs.Click += new System.EventHandler(this.btn_LoadRetailSongs_Click);
            // 
            // btn_Standardization
            // 
            this.btn_Standardization.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Standardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Standardization.Location = new System.Drawing.Point(612, 128);
            this.btn_Standardization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Standardization.Name = "btn_Standardization";
            this.btn_Standardization.Size = new System.Drawing.Size(99, 74);
            this.btn_Standardization.TabIndex = 11;
            this.btn_Standardization.Text = "Open StandardizationDB";
            this.toolTip1.SetToolTip(this.btn_Standardization, "Here you can decide what Standardizations you want to apply to Artist Names, Albu" +
        "m Names, Covers or Short Names");
            this.btn_Standardization.UseVisualStyleBackColor = false;
            this.btn_Standardization.Click += new System.EventHandler(this.btn_Standardization_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(738, 128);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(66, 72);
            this.button5.TabIndex = 12;
            this.button5.Text = "Open RetailDB";
            this.toolTip1.SetToolTip(this.button5, "Screen to manage the Game Default Screens for RS1Retail, RS1DLC, or RS2014 Retail" +
        " packs, Crossplatform songs.");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btn_GoImport
            // 
            this.btn_GoImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GoImport.Location = new System.Drawing.Point(153, 6);
            this.btn_GoImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GoImport.Name = "btn_GoImport";
            this.btn_GoImport.Size = new System.Drawing.Size(33, 24);
            this.btn_GoImport.TabIndex = 1;
            this.btn_GoImport.Text = "<->";
            this.toolTip1.SetToolTip(this.btn_GoImport, "Open Importing CDLC Folder");
            this.btn_GoImport.UseVisualStyleBackColor = true;
            this.btn_GoImport.Click += new System.EventHandler(this.btn_GoImport_Click);
            // 
            // btm_GoTemp
            // 
            this.btm_GoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_GoTemp.Location = new System.Drawing.Point(153, 36);
            this.btm_GoTemp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btm_GoTemp.Name = "btm_GoTemp";
            this.btm_GoTemp.Size = new System.Drawing.Size(33, 24);
            this.btm_GoTemp.TabIndex = 4;
            this.btm_GoTemp.Text = "<->";
            this.toolTip1.SetToolTip(this.btm_GoTemp, "Open the location that stores the decompressed CDLCs");
            this.btm_GoTemp.UseVisualStyleBackColor = true;
            this.btm_GoTemp.Click += new System.EventHandler(this.btm_GoTemp_Click);
            // 
            // btm_GoDB
            // 
            this.btm_GoDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_GoDB.Location = new System.Drawing.Point(153, 68);
            this.btm_GoDB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btm_GoDB.Name = "btm_GoDB";
            this.btm_GoDB.Size = new System.Drawing.Size(33, 24);
            this.btm_GoDB.TabIndex = 7;
            this.btm_GoDB.Text = "<->";
            this.toolTip1.SetToolTip(this.btm_GoDB, "Open the locatiomn of the M$ Access DB used to store the CDLC metadata.");
            this.btm_GoDB.UseVisualStyleBackColor = true;
            this.btm_GoDB.Click += new System.EventHandler(this.btm_GoDB_Click);
            // 
            // btn_OpenDB
            // 
            this.btn_OpenDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_OpenDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenDB.Location = new System.Drawing.Point(8, 240);
            this.btn_OpenDB.Margin = new System.Windows.Forms.Padding(0);
            this.btn_OpenDB.Name = "btn_OpenDB";
            this.btn_OpenDB.Size = new System.Drawing.Size(96, 39);
            this.btn_OpenDB.TabIndex = 54;
            this.btn_OpenDB.Text = "DB Viewer";
            this.toolTip1.SetToolTip(this.btn_OpenDB, "Opens the Metadata DB using a 3rd party tool (not M$ Access)");
            this.btn_OpenDB.UseVisualStyleBackColor = false;
            this.btn_OpenDB.Click += new System.EventHandler(this.btn_Log_Click);
            // 
            // chbx_Configurations
            // 
            this.chbx_Configurations.FormattingEnabled = true;
            this.chbx_Configurations.Items.AddRange(new object[] {
            "DevHome",
            "AltDevHome",
            "Mac",
            "PC",
            "DevVirtualMachine"});
            this.chbx_Configurations.Location = new System.Drawing.Point(576, 4);
            this.chbx_Configurations.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbx_Configurations.Name = "chbx_Configurations";
            this.chbx_Configurations.Size = new System.Drawing.Size(154, 28);
            this.chbx_Configurations.TabIndex = 325;
            this.chbx_Configurations.Text = "Select Profile";
            this.toolTip1.SetToolTip(this.chbx_Configurations, "Dropdown listing a set of Saved profiles for Folders to ne used in CDCL Managemen" +
        "t.");
            this.chbx_Configurations.SelectedIndexChanged += new System.EventHandler(this.Chbx_Configurations_SelectedIndexChanged);
            // 
            // btn_ProfilesSave
            // 
            this.btn_ProfilesSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ProfilesSave.Location = new System.Drawing.Point(735, 4);
            this.btn_ProfilesSave.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ProfilesSave.Name = "btn_ProfilesSave";
            this.btn_ProfilesSave.Size = new System.Drawing.Size(33, 28);
            this.btn_ProfilesSave.TabIndex = 386;
            this.btn_ProfilesSave.Text = "+";
            this.toolTip1.SetToolTip(this.btn_ProfilesSave, "Add a new Profile.");
            this.btn_ProfilesSave.UseVisualStyleBackColor = true;
            this.btn_ProfilesSave.Click += new System.EventHandler(this.btn_ProfilesSave_Click);
            // 
            // btn_ProfileRemove
            // 
            this.btn_ProfileRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ProfileRemove.Location = new System.Drawing.Point(771, 4);
            this.btn_ProfileRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ProfileRemove.Name = "btn_ProfileRemove";
            this.btn_ProfileRemove.Size = new System.Drawing.Size(33, 28);
            this.btn_ProfileRemove.TabIndex = 388;
            this.btn_ProfileRemove.Text = "-";
            this.toolTip1.SetToolTip(this.btn_ProfileRemove, "Remove Existing Profile.");
            this.btn_ProfileRemove.UseVisualStyleBackColor = true;
            this.btn_ProfileRemove.Click += new System.EventHandler(this.btn_GroupsRemove_Click);
            // 
            // btn_OpenLogsFolder
            // 
            this.btn_OpenLogsFolder.BackColor = System.Drawing.SystemColors.Control;
            this.btn_OpenLogsFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenLogsFolder.Location = new System.Drawing.Point(316, 240);
            this.btn_OpenLogsFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btn_OpenLogsFolder.Name = "btn_OpenLogsFolder";
            this.btn_OpenLogsFolder.Size = new System.Drawing.Size(114, 39);
            this.btn_OpenLogsFolder.TabIndex = 389;
            this.btn_OpenLogsFolder.Text = "Logs Folder";
            this.toolTip1.SetToolTip(this.btn_OpenLogsFolder, "Opens Log Folder contaning Debug or Import, or Repack loggin info.");
            this.btn_OpenLogsFolder.UseVisualStyleBackColor = false;
            this.btn_OpenLogsFolder.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_Enable_CDLC
            // 
            this.btn_Enable_CDLC.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Enable_CDLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Enable_CDLC.Location = new System.Drawing.Point(434, 240);
            this.btn_Enable_CDLC.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Enable_CDLC.Name = "btn_Enable_CDLC";
            this.btn_Enable_CDLC.Size = new System.Drawing.Size(118, 39);
            this.btn_Enable_CDLC.TabIndex = 390;
            this.btn_Enable_CDLC.Text = "Enable CDCL";
            this.toolTip1.SetToolTip(this.btn_Enable_CDLC, "Checks and Enable Rocksmith to show Custom Downloadabe extra-user-made Content.");
            this.btn_Enable_CDLC.UseVisualStyleBackColor = false;
            this.btn_Enable_CDLC.Click += new System.EventHandler(this.btn_Enable_CDLC_Click);
            // 
            // btn_CalcNoOfImports
            // 
            this.btn_CalcNoOfImports.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CalcNoOfImports.Location = new System.Drawing.Point(612, 96);
            this.btn_CalcNoOfImports.Margin = new System.Windows.Forms.Padding(0);
            this.btn_CalcNoOfImports.Name = "btn_CalcNoOfImports";
            this.btn_CalcNoOfImports.Size = new System.Drawing.Size(38, 27);
            this.btn_CalcNoOfImports.TabIndex = 392;
            this.btn_CalcNoOfImports.Text = "<>";
            this.toolTip1.SetToolTip(this.btn_CalcNoOfImports, "Refresh CDCL 2 Import.");
            this.btn_CalcNoOfImports.UseVisualStyleBackColor = true;
            this.btn_CalcNoOfImports.Click += new System.EventHandler(this.btn_CalcNoOfImports_Click);
            // 
            // btn_CopyDefaultDBtoTemp
            // 
            this.btn_CopyDefaultDBtoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CopyDefaultDBtoTemp.Location = new System.Drawing.Point(464, 68);
            this.btn_CopyDefaultDBtoTemp.Margin = new System.Windows.Forms.Padding(0);
            this.btn_CopyDefaultDBtoTemp.Name = "btn_CopyDefaultDBtoTemp";
            this.btn_CopyDefaultDBtoTemp.Size = new System.Drawing.Size(27, 24);
            this.btn_CopyDefaultDBtoTemp.TabIndex = 402;
            this.btn_CopyDefaultDBtoTemp.Text = "<";
            this.toolTip1.SetToolTip(this.btn_CopyDefaultDBtoTemp, "Copies the Default DB to Temp folder");
            this.btn_CopyDefaultDBtoTemp.UseVisualStyleBackColor = true;
            this.btn_CopyDefaultDBtoTemp.Click += new System.EventHandler(this.btn_CopyDefaultDBtoTemp_Click);
            // 
            // btn_Param
            // 
            this.btn_Param.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Param.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Param.Location = new System.Drawing.Point(558, 240);
            this.btn_Param.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Param.Name = "btn_Param";
            this.btn_Param.Size = new System.Drawing.Size(75, 39);
            this.btn_Param.TabIndex = 403;
            this.btn_Param.Text = "Param";
            this.toolTip1.SetToolTip(this.btn_Param, "Open XML containing the Basic Options for DLCManager (Please note each profile mo" +
        "st imp settings are maintained at DB level in the table Groups)");
            this.btn_Param.UseVisualStyleBackColor = false;
            this.btn_Param.Click += new System.EventHandler(this.btn_Param_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(636, 240);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 39);
            this.btn_Save.TabIndex = 408;
            this.btn_Save.Text = "Save";
            this.toolTip1.SetToolTip(this.btn_Save, "Open XML containing the Basic Options for DLCManager (Please note each profile mo" +
        "st imp settings are maintained at DB level in the table Groups)");
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btn_Preview_Lyric_Info
            // 
            this.btn_Preview_Lyric_Info.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Lyric_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Lyric_Info.Location = new System.Drawing.Point(783, 484);
            this.btn_Preview_Lyric_Info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Lyric_Info.Name = "btn_Preview_Lyric_Info";
            this.btn_Preview_Lyric_Info.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Lyric_Info.TabIndex = 412;
            this.toolTip1.SetToolTip(this.btn_Preview_Lyric_Info, "Preview File Name Mask");
            this.btn_Preview_Lyric_Info.UseVisualStyleBackColor = false;
            this.btn_Preview_Lyric_Info.Click += new System.EventHandler(this.Btn_Lyric_Info_Click);
            // 
            // btn_Preview_Album_Sort
            // 
            this.btn_Preview_Album_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Album_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Album_Sort.Location = new System.Drawing.Point(783, 426);
            this.btn_Preview_Album_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Album_Sort.Name = "btn_Preview_Album_Sort";
            this.btn_Preview_Album_Sort.Size = new System.Drawing.Size(21, 21);
            this.btn_Preview_Album_Sort.TabIndex = 417;
            this.toolTip1.SetToolTip(this.btn_Preview_Album_Sort, "Preview Album Mask");
            this.btn_Preview_Album_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Album_Sort.Click += new System.EventHandler(this.Btn_Album_Sort_Click);
            // 
            // btn_Album2SortA
            // 
            this.btn_Album2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Album2SortA.Location = new System.Drawing.Point(464, 38);
            this.btn_Album2SortA.Name = "btn_Album2SortA";
            this.btn_Album2SortA.Size = new System.Drawing.Size(27, 24);
            this.btn_Album2SortA.TabIndex = 429;
            this.btn_Album2SortA.Text = ">";
            this.toolTip1.SetToolTip(this.btn_Album2SortA, "Copy path DB folder");
            this.btn_Album2SortA.UseVisualStyleBackColor = true;
            this.btn_Album2SortA.Click += new System.EventHandler(this.btn_Album2SortA_Click);
            // 
            // btn_FilterParams
            // 
            this.btn_FilterParams.Location = new System.Drawing.Point(714, 652);
            this.btn_FilterParams.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_FilterParams.Name = "btn_FilterParams";
            this.btn_FilterParams.Size = new System.Drawing.Size(90, 72);
            this.btn_FilterParams.TabIndex = 430;
            this.btn_FilterParams.Text = "Filter Params";
            this.toolTip1.SetToolTip(this.btn_FilterParams, "Screen to manage the Game Default Screens for RS1Retail, RS1DLC, or RS2014 Retail" +
        " packs, Crossplatform songs.");
            this.btn_FilterParams.UseVisualStyleBackColor = true;
            this.btn_FilterParams.Click += new System.EventHandler(this.btn_FilterParams_Click);
            // 
            // lbl_Settings
            // 
            this.lbl_Settings.AutoSize = true;
            this.lbl_Settings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Settings.Location = new System.Drawing.Point(526, 543);
            this.lbl_Settings.Name = "lbl_Settings";
            this.lbl_Settings.Size = new System.Drawing.Size(153, 20);
            this.lbl_Settings.TabIndex = 406;
            this.lbl_Settings.Text = ":Settings  {ParamID}";
            // 
            // lbl_Log
            // 
            this.lbl_Log.AutoSize = true;
            this.lbl_Log.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Log.Location = new System.Drawing.Point(696, 734);
            this.lbl_Log.Name = "lbl_Log";
            this.lbl_Log.Size = new System.Drawing.Size(72, 20);
            this.lbl_Log.TabIndex = 407;
            this.lbl_Log.Text = ":Live Log";
            // 
            // cbx_Activ_Lyric_Info
            // 
            this.cbx_Activ_Lyric_Info.AutoSize = true;
            this.cbx_Activ_Lyric_Info.Checked = true;
            this.cbx_Activ_Lyric_Info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Lyric_Info.Location = new System.Drawing.Point(759, 484);
            this.cbx_Activ_Lyric_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Lyric_Info.Name = "cbx_Activ_Lyric_Info";
            this.cbx_Activ_Lyric_Info.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Lyric_Info.TabIndex = 411;
            this.cbx_Activ_Lyric_Info.UseVisualStyleBackColor = true;
            this.cbx_Activ_Lyric_Info.Visible = false;
            this.cbx_Activ_Lyric_Info.CheckedChanged += new System.EventHandler(this.Cbx_Activ_Lyric_Info_CheckedChanged);
            // 
            // lbl_LyricInfo
            // 
            this.lbl_LyricInfo.AutoSize = true;
            this.lbl_LyricInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_LyricInfo.Location = new System.Drawing.Point(9, 478);
            this.lbl_LyricInfo.Name = "lbl_LyricInfo";
            this.lbl_LyricInfo.Size = new System.Drawing.Size(73, 20);
            this.lbl_LyricInfo.TabIndex = 413;
            this.lbl_LyricInfo.Text = "Lyric Info";
            this.lbl_LyricInfo.Visible = false;
            // 
            // cbx_Lyric_Info
            // 
            this.cbx_Lyric_Info.DropDownWidth = 200;
            this.cbx_Lyric_Info.FormattingEnabled = true;
            this.cbx_Lyric_Info.Items.AddRange(new object[] {
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Lyric_Info.Location = new System.Drawing.Point(597, 478);
            this.cbx_Lyric_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Lyric_Info.Name = "cbx_Lyric_Info";
            this.cbx_Lyric_Info.Size = new System.Drawing.Size(157, 28);
            this.cbx_Lyric_Info.TabIndex = 410;
            this.cbx_Lyric_Info.Visible = false;
            this.cbx_Lyric_Info.SelectedIndexChanged += new System.EventHandler(this.cbx_Lyric_Info_SelectedIndexChanged);
            // 
            // txt_Lyric_Info
            // 
            this.txt_Lyric_Info.Location = new System.Drawing.Point(102, 478);
            this.txt_Lyric_Info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Lyric_Info.Name = "txt_Lyric_Info";
            this.txt_Lyric_Info.Size = new System.Drawing.Size(490, 26);
            this.txt_Lyric_Info.TabIndex = 409;
            this.txt_Lyric_Info.Text = "<Instr>";
            this.txt_Lyric_Info.Visible = false;
            // 
            // cbx_Activ_Album_Sort
            // 
            this.cbx_Activ_Album_Sort.AutoSize = true;
            this.cbx_Activ_Album_Sort.Checked = true;
            this.cbx_Activ_Album_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Album_Sort.Location = new System.Drawing.Point(759, 429);
            this.cbx_Activ_Album_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Activ_Album_Sort.Name = "cbx_Activ_Album_Sort";
            this.cbx_Activ_Album_Sort.Size = new System.Drawing.Size(22, 21);
            this.cbx_Activ_Album_Sort.TabIndex = 416;
            this.cbx_Activ_Album_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Album_Sort.Visible = false;
            this.cbx_Activ_Album_Sort.CheckedChanged += new System.EventHandler(this.Cbx_Activ_Album_Sort_CheckedChanged);
            // 
            // lbl_AlbumSort
            // 
            this.lbl_AlbumSort.AutoSize = true;
            this.lbl_AlbumSort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_AlbumSort.Location = new System.Drawing.Point(9, 429);
            this.lbl_AlbumSort.Name = "lbl_AlbumSort";
            this.lbl_AlbumSort.Size = new System.Drawing.Size(92, 20);
            this.lbl_AlbumSort.TabIndex = 418;
            this.lbl_AlbumSort.Text = "Album Sort ";
            this.lbl_AlbumSort.Visible = false;
            // 
            // cbx_Album_Sort
            // 
            this.cbx_Album_Sort.DropDownWidth = 200;
            this.cbx_Album_Sort.FormattingEnabled = true;
            this.cbx_Album_Sort.Items.AddRange(new object[] {
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
            "<Groups>",
            "<GroupIndex>",
            "<BetaOrGroupIndex>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Title Sort>",
            "<Artist Sort>",
            "<Album Sort>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Avail. Tracks w Bonus>",
            "<Avail. Tracks w Favorite>",
            "<Avail. Tracks and Timings>",
            "<Avail. Tracks and ShortTimings>",
            "<Avail. Tracks and ShortTimings&Bonus>",
            "<Avail. Tracks and ShortTimings&Bonus&Favorite>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<TimestampShort>",
            "<Live>",
            "<Acoustic>",
            "<Instrumental>",
            "<EP>",
            "<Uncensored>",
            "<SoundTrack>",
            "<Single>",
            "<Track version>",
            "<LyricsLanguage>",
            "<IntheWorks>",
            "<FullAlbum>",
            "<Manipulated>",
            "<CDLC_ID>",
            "<DLCM Release>",
            "<DLCM ReleaseName>",
            "<DLCM ReleaseVersion>",
            "<Date>",
            "<DigitechDropFlag>",
            "<DigitechDropDetails>"});
            this.cbx_Album_Sort.Location = new System.Drawing.Point(597, 423);
            this.cbx_Album_Sort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_Album_Sort.Name = "cbx_Album_Sort";
            this.cbx_Album_Sort.Size = new System.Drawing.Size(157, 28);
            this.cbx_Album_Sort.TabIndex = 415;
            this.cbx_Album_Sort.Visible = false;
            // 
            // txt_Album_Sort
            // 
            this.txt_Album_Sort.Location = new System.Drawing.Point(102, 423);
            this.txt_Album_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Album_Sort.Name = "txt_Album_Sort";
            this.txt_Album_Sort.Size = new System.Drawing.Size(490, 26);
            this.txt_Album_Sort.TabIndex = 414;
            this.txt_Album_Sort.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            this.txt_Album_Sort.Visible = false;
            // 
            // txt_FilterParams
            // 
            this.txt_FilterParams.Location = new System.Drawing.Point(714, 537);
            this.txt_FilterParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_FilterParams.Name = "txt_FilterParams";
            this.txt_FilterParams.Size = new System.Drawing.Size(88, 106);
            this.txt_FilterParams.TabIndex = 431;
            this.txt_FilterParams.Text = "";
            this.txt_FilterParams.Visible = false;
            // 
            // lbl_Access
            // 
            this.lbl_Access.AutoSize = true;
            this.lbl_Access.ForeColor = System.Drawing.Color.Red;
            this.lbl_Access.Location = new System.Drawing.Point(243, 84);
            this.lbl_Access.Name = "lbl_Access";
            this.lbl_Access.Size = new System.Drawing.Size(118, 20);
            this.lbl_Access.TabIndex = 432;
            this.lbl_Access.Text = "DB Folder Path";
            this.lbl_Access.Visible = false;
            // 
            // DLCManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.txt_FilterParams);
            this.Controls.Add(this.btn_FilterParams);
            this.Controls.Add(this.btn_Album2SortA);
            this.Controls.Add(this.btn_Preview_Album_Sort);
            this.Controls.Add(this.cbx_Activ_Album_Sort);
            this.Controls.Add(this.lbl_AlbumSort);
            this.Controls.Add(this.cbx_Album_Sort);
            this.Controls.Add(this.txt_Album_Sort);
            this.Controls.Add(this.btn_Preview_Lyric_Info);
            this.Controls.Add(this.cbx_Activ_Lyric_Info);
            this.Controls.Add(this.lbl_LyricInfo);
            this.Controls.Add(this.cbx_Lyric_Info);
            this.Controls.Add(this.txt_Lyric_Info);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lbl_Log);
            this.Controls.Add(this.lbl_Settings);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.btn_Param);
            this.Controls.Add(this.btn_CopyDefaultDBtoTemp);
            this.Controls.Add(this.chbx_DefaultDB);
            this.Controls.Add(this.btn_CalcNoOfImports);
            this.Controls.Add(this.btn_Enable_CDLC);
            this.Controls.Add(this.btn_OpenLogsFolder);
            this.Controls.Add(this.btn_ProfileRemove);
            this.Controls.Add(this.btn_ProfilesSave);
            this.Controls.Add(this.chbx_Configurations);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_OpenDB);
            this.Controls.Add(this.btm_GoDB);
            this.Controls.Add(this.btm_GoTemp);
            this.Controls.Add(this.btn_GoImport);
            this.Controls.Add(this.btn_LoadRetailSongs);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btn_ApplyStandardization);
            this.Controls.Add(this.btn_Standardization);
            this.Controls.Add(this.cbx_Export);
            this.Controls.Add(this.Export_To);
            this.Controls.Add(this.chbx_Additional_Manipulations);
            this.Controls.Add(this.lbl_Mask);
            this.Controls.Add(this.btn_Preview_Artist_Sort);
            this.Controls.Add(this.cbx_Activ_Artist_Sort);
            this.Controls.Add(this.lbl_Artist_Sort);
            this.Controls.Add(this.cbx_Artist_Sort);
            this.Controls.Add(this.txt_Artist_Sort);
            this.Controls.Add(this.btn_Preview_File_Name);
            this.Controls.Add(this.btn_Preview_Album);
            this.Controls.Add(this.btn_Preview_Artist);
            this.Controls.Add(this.btn_Preview_Title_Sort);
            this.Controls.Add(this.btn_Preview_Title);
            this.Controls.Add(this.cbx_Activ_File_Name);
            this.Controls.Add(this.cbx_Activ_Album);
            this.Controls.Add(this.cbx_Activ_Artist);
            this.Controls.Add(this.cbx_Activ_Title_Sort);
            this.Controls.Add(this.cbx_Activ_Title);
            this.Controls.Add(this.lbl_Artist);
            this.Controls.Add(this.cbx_Artist);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.cbx_Title);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.lbl_File_Name);
            this.Controls.Add(this.lbl_Album);
            this.Controls.Add(this.lbl_Title_Sort);
            this.Controls.Add(this.cbx_File_Name);
            this.Controls.Add(this.txt_File_Name);
            this.Controls.Add(this.cbx_Album);
            this.Controls.Add(this.txt_Album);
            this.Controls.Add(this.cbx_Title_Sort);
            this.Controls.Add(this.txt_Title_Sort);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.chbx_DebugB);
            this.Controls.Add(this.txt_DBFolder);
            this.Controls.Add(this.btn_DBFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_RocksmithDLCPath);
            this.Controls.Add(this.txt_TempPath);
            this.Controls.Add(this.chbx_CleanTemp);
            this.Controls.Add(this.btn_SteamDLCFolder);
            this.Controls.Add(this.rtxt_StatisticsOnReadDLCs);
            this.Controls.Add(this.btn_PopulateDB);
            this.Controls.Add(this.btn_OpenMainDB);
            this.Controls.Add(this.lbl_RocksmithDLCPath);
            this.Controls.Add(this.btn_TempPath);
            this.Controls.Add(this.lbl_TempFolders);
            this.Controls.Add(this.lbl_PreviewText);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(921, 1146);
            this.MinimumSize = new System.Drawing.Size(921, 1146);
            this.Name = "DLCManager";
            this.Size = new System.Drawing.Size(921, 1146);
            this.Load += new System.EventHandler(this.DLCManager_Load);
            this.Enter += new System.EventHandler(this.DLCManager_Enter);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbx_Export;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtn_Population_Groups;
        private System.Windows.Forms.RadioButton rbtn_Population_All;
        private System.Windows.Forms.RadioButton rbtn_Population_Selected;
        private System.Windows.Forms.BindingSource mainBindingSource;
        private System.Windows.Forms.Button Export_To;
        private System.Windows.Forms.CheckBox chbx_Rebuild;
        private System.Windows.Forms.Button btn_Cleanup_MainDB;
        private System.Windows.Forms.ComboBox cbx_Groups;
        private System.Windows.Forms.CheckedListBox chbx_Additional_Manipulations;
        private System.Windows.Forms.Label lbl_Mask;
        private System.Windows.Forms.Button btn_Preview_Artist_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist_Sort;
        private System.Windows.Forms.Label lbl_Artist_Sort;
        private System.Windows.Forms.ComboBox cbx_Artist_Sort;
        private System.Windows.Forms.TextBox txt_Artist_Sort;
        private System.Windows.Forms.Button btn_Preview_File_Name;
        private System.Windows.Forms.Button btn_Preview_Album;
        private System.Windows.Forms.Button btn_Preview_Artist;
        private System.Windows.Forms.Button btn_Preview_Title_Sort;
        private System.Windows.Forms.Button btn_Preview_Title;
        private System.Windows.Forms.CheckBox cbx_Activ_File_Name;
        private System.Windows.Forms.CheckBox cbx_Activ_Album;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist;
        private System.Windows.Forms.CheckBox cbx_Activ_Title_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Title;
        private System.Windows.Forms.Label lbl_Artist;
        private System.Windows.Forms.ComboBox cbx_Artist;
        private System.Windows.Forms.TextBox txt_Artist;
        private System.Windows.Forms.ComboBox cbx_Title;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Label lbl_File_Name;
        private System.Windows.Forms.Label lbl_Album;
        private System.Windows.Forms.Label lbl_Title_Sort;
        private System.Windows.Forms.ComboBox cbx_File_Name;
        private System.Windows.Forms.TextBox txt_File_Name;
        private System.Windows.Forms.ComboBox cbx_Album;
        private System.Windows.Forms.TextBox txt_Album;
        private System.Windows.Forms.ComboBox cbx_Title_Sort;
        private System.Windows.Forms.TextBox txt_Title_Sort;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.CheckBox chbx_DebugB;
        private System.Windows.Forms.TextBox txt_DBFolder;
        private System.Windows.Forms.Button btn_DBFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_RocksmithDLCPath;
        private System.Windows.Forms.TextBox txt_TempPath;
        private System.Windows.Forms.CheckBox chbx_CleanTemp;
        private System.Windows.Forms.Button btn_SteamDLCFolder;
        public System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs;
        private System.Windows.Forms.Button btn_PopulateDB;
        private System.Windows.Forms.Button btn_OpenMainDB;
        private System.Windows.Forms.Label lbl_RocksmithDLCPath;
        private System.Windows.Forms.Button btn_TempPath;
        private System.Windows.Forms.Label lbl_TempFolders;
        private System.Windows.Forms.Label lbl_PreviewText;
        private System.Windows.Forms.Button btn_RePack;
        private System.Windows.Forms.CheckBox chbx_XBOX360;
        private System.Windows.Forms.CheckBox chbx_Mac;
        private System.Windows.Forms.CheckBox chbx_PS3;
        private System.Windows.Forms.CheckBox chbx_PC;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btn_Standardization;
        private System.Windows.Forms.Button btn_ApplyStandardization;
        private System.Windows.Forms.CheckBox chbx_DefaultDB;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btn_LoadRetailSongs;
        private System.Windows.Forms.Button btn_GoImport;
        private System.Windows.Forms.Button btm_GoTemp;
        private System.Windows.Forms.Button btm_GoDB;
        private System.Windows.Forms.Button btn_OpenDB;
        private System.Windows.Forms.ComboBox chbx_Configurations;
        private System.Windows.Forms.Button btn_ProfilesSave;
        private System.Windows.Forms.Button btn_ProfileRemove;
        private System.Windows.Forms.Label lbl_NoRec2;
        private System.Windows.Forms.Button btn_OpenLogsFolder;
        private System.Windows.Forms.Button btm_GoRepack;
        private System.Windows.Forms.Button btn_Enable_CDLC;
        private System.Windows.Forms.Button btn_CalcNoOfImports;
        private System.Windows.Forms.Button btn_CopyDefaultDBtoTemp;
        private System.Windows.Forms.Button btn_Param;
        private System.Windows.Forms.Button btn_Debbug;
        public ProgressBar pB_ReadDLCs;
        private Label lbl_Settings;
        private Label lbl_Log;
        private RadioButton rbtn_Population_PackNO;
        private ComboBox txt_NoOfSplits;
        private Button btn_Save;
        private Button btn_Preview_Lyric_Info;
        private CheckBox cbx_Activ_Lyric_Info;
        private Label lbl_LyricInfo;
        private ComboBox cbx_Lyric_Info;
        private TextBox txt_Lyric_Info;
        private Button btn_Preview_Album_Sort;
        private CheckBox cbx_Activ_Album_Sort;
        private Label lbl_AlbumSort;
        private ComboBox cbx_Album_Sort;
        private TextBox txt_Album_Sort;
        private CheckBox chbx_iOS;
        private CheckBox chbx_PS4;
        private Button btn_Album2SortA;
        private Button btn_FilterParams;
        public RichTextBox txt_FilterParams;
        private Label lbl_Access;
        //public static ProgressBar pB_ReadDLCs;
    }
}
