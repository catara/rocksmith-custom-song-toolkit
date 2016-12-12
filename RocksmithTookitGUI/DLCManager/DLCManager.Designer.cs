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
            this.chbx_XBOX360 = new System.Windows.Forms.CheckBox();
            this.chbx_Mac = new System.Windows.Forms.CheckBox();
            this.rbtn_Population_Groups = new System.Windows.Forms.RadioButton();
            this.chbx_PS3 = new System.Windows.Forms.CheckBox();
            this.cbx_Groups = new System.Windows.Forms.ComboBox();
            this.mainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chbx_PC = new System.Windows.Forms.CheckBox();
            this.btn_Cleanup_MainDB = new System.Windows.Forms.Button();
            this.btn_RePack = new System.Windows.Forms.Button();
            this.rbtn_Population_All = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_Selected = new System.Windows.Forms.RadioButton();
            this.chbx_Rebuild = new System.Windows.Forms.CheckBox();
            this.Export_To = new System.Windows.Forms.Button();
            this.chbx_Additional_Manipulations = new System.Windows.Forms.CheckedListBox();
            this.lbl_Mask = new System.Windows.Forms.Label();
            this.btn_Preview_Artist_Sort = new System.Windows.Forms.Button();
            this.cbx_Activ_Artist_Sort = new System.Windows.Forms.CheckBox();
            this.lbl_Artist_Sort = new System.Windows.Forms.Label();
            this.cbx_Artist_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Artist_Sort = new System.Windows.Forms.TextBox();
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
            this.chbx_CleanDB = new System.Windows.Forms.CheckBox();
            this.rtxt_StatisticsOnReadDLCs = new System.Windows.Forms.RichTextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
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
            this.chbx_DefaultDB = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_GoImport = new System.Windows.Forms.Button();
            this.btm_GoTemp = new System.Windows.Forms.Button();
            this.btm_GoDB = new System.Windows.Forms.Button();
            this.btn_Log = new System.Windows.Forms.Button();
            this.chbx_Configurations = new System.Windows.Forms.ComboBox();
            this.btn_ProfilesSave = new System.Windows.Forms.Button();
            this.btn_ProfileRemove = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbx_Export
            // 
            this.cbx_Export.Enabled = false;
            this.cbx_Export.FormattingEnabled = true;
            this.cbx_Export.Items.AddRange(new object[] {
            "Excel",
            "WebPage"});
            this.cbx_Export.Location = new System.Drawing.Point(242, 262);
            this.cbx_Export.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Export.Name = "cbx_Export";
            this.cbx_Export.Size = new System.Drawing.Size(90, 28);
            this.cbx_Export.TabIndex = 57;
            this.cbx_Export.Text = "Excel";
            this.cbx_Export.SelectedIndexChanged += new System.EventHandler(this.cbx_Export_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chbx_XBOX360);
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
            this.panel2.Location = new System.Drawing.Point(10, 103);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(446, 111);
            this.panel2.TabIndex = 204;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // chbx_XBOX360
            // 
            this.chbx_XBOX360.AutoSize = true;
            this.chbx_XBOX360.Location = new System.Drawing.Point(195, 6);
            this.chbx_XBOX360.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_XBOX360.Name = "chbx_XBOX360";
            this.chbx_XBOX360.Size = new System.Drawing.Size(107, 24);
            this.chbx_XBOX360.TabIndex = 23;
            this.chbx_XBOX360.Text = "XBOX360";
            this.chbx_XBOX360.UseVisualStyleBackColor = true;
            this.chbx_XBOX360.CheckedChanged += new System.EventHandler(this.chbx_XBOX360_CheckedChanged);
            // 
            // chbx_Mac
            // 
            this.chbx_Mac.AutoSize = true;
            this.chbx_Mac.Location = new System.Drawing.Point(126, 6);
            this.chbx_Mac.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Mac.Name = "chbx_Mac";
            this.chbx_Mac.Size = new System.Drawing.Size(65, 24);
            this.chbx_Mac.TabIndex = 22;
            this.chbx_Mac.Text = "Mac";
            this.chbx_Mac.UseVisualStyleBackColor = true;
            this.chbx_Mac.CheckedChanged += new System.EventHandler(this.chbx_Mac_CheckedChanged);
            // 
            // rbtn_Population_Groups
            // 
            this.rbtn_Population_Groups.AutoSize = true;
            this.rbtn_Population_Groups.Location = new System.Drawing.Point(150, 34);
            this.rbtn_Population_Groups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbtn_Population_Groups.Name = "rbtn_Population_Groups";
            this.rbtn_Population_Groups.Size = new System.Drawing.Size(79, 24);
            this.rbtn_Population_Groups.TabIndex = 27;
            this.rbtn_Population_Groups.Text = "Group";
            this.rbtn_Population_Groups.UseVisualStyleBackColor = true;
            this.rbtn_Population_Groups.CheckedChanged += new System.EventHandler(this.rbtn_Population_Groups_CheckedChanged);
            // 
            // chbx_PS3
            // 
            this.chbx_PS3.AutoSize = true;
            this.chbx_PS3.Checked = true;
            this.chbx_PS3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_PS3.Location = new System.Drawing.Point(62, 6);
            this.chbx_PS3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_PS3.Name = "chbx_PS3";
            this.chbx_PS3.Size = new System.Drawing.Size(65, 24);
            this.chbx_PS3.TabIndex = 21;
            this.chbx_PS3.Text = "PS3";
            this.chbx_PS3.UseVisualStyleBackColor = true;
            this.chbx_PS3.CheckedChanged += new System.EventHandler(this.chbx_PS3_CheckedChanged);
            // 
            // cbx_Groups
            // 
            this.cbx_Groups.DataSource = this.mainBindingSource;
            this.cbx_Groups.Enabled = false;
            this.cbx_Groups.FormattingEnabled = true;
            this.cbx_Groups.Location = new System.Drawing.Point(231, 32);
            this.cbx_Groups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Groups.Name = "cbx_Groups";
            this.cbx_Groups.Size = new System.Drawing.Size(199, 28);
            this.cbx_Groups.TabIndex = 28;
            this.cbx_Groups.DropDown += new System.EventHandler(this.cbx_Groups_DropDown);
            this.cbx_Groups.SelectedIndexChanged += new System.EventHandler(this.cbx_Groups_SelectedIndexChanged);
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataMember = "Main";
            this.mainBindingSource.CurrentChanged += new System.EventHandler(this.mainBindingSource_CurrentChanged);
            // 
            // chbx_PC
            // 
            this.chbx_PC.AutoSize = true;
            this.chbx_PC.Location = new System.Drawing.Point(6, 6);
            this.chbx_PC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_PC.Name = "chbx_PC";
            this.chbx_PC.Size = new System.Drawing.Size(56, 24);
            this.chbx_PC.TabIndex = 20;
            this.chbx_PC.Text = "PC";
            this.chbx_PC.UseVisualStyleBackColor = true;
            this.chbx_PC.CheckedChanged += new System.EventHandler(this.chbx_PC_CheckedChanged);
            // 
            // btn_Cleanup_MainDB
            // 
            this.btn_Cleanup_MainDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cleanup_MainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cleanup_MainDB.Location = new System.Drawing.Point(3, 66);
            this.btn_Cleanup_MainDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Cleanup_MainDB.Name = "btn_Cleanup_MainDB";
            this.btn_Cleanup_MainDB.Size = new System.Drawing.Size(116, 37);
            this.btn_Cleanup_MainDB.TabIndex = 15;
            this.btn_Cleanup_MainDB.Text = "Remove";
            this.btn_Cleanup_MainDB.UseVisualStyleBackColor = false;
            this.btn_Cleanup_MainDB.Click += new System.EventHandler(this.btn_Cleanup_MainDB_Click);
            // 
            // btn_RePack
            // 
            this.btn_RePack.BackColor = System.Drawing.SystemColors.Control;
            this.btn_RePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RePack.Location = new System.Drawing.Point(122, 66);
            this.btn_RePack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RePack.Name = "btn_RePack";
            this.btn_RePack.Size = new System.Drawing.Size(110, 37);
            this.btn_RePack.TabIndex = 14;
            this.btn_RePack.Text = "RePack";
            this.btn_RePack.UseVisualStyleBackColor = false;
            this.btn_RePack.Click += new System.EventHandler(this.btn_RePack_Click);
            // 
            // rbtn_Population_All
            // 
            this.rbtn_Population_All.AutoSize = true;
            this.rbtn_Population_All.Location = new System.Drawing.Point(100, 34);
            this.rbtn_Population_All.Margin = new System.Windows.Forms.Padding(0);
            this.rbtn_Population_All.Name = "rbtn_Population_All";
            this.rbtn_Population_All.Size = new System.Drawing.Size(51, 24);
            this.rbtn_Population_All.TabIndex = 26;
            this.rbtn_Population_All.Text = "All";
            this.rbtn_Population_All.UseVisualStyleBackColor = true;
            this.rbtn_Population_All.CheckedChanged += new System.EventHandler(this.rbtn_Population_All_CheckedChanged);
            // 
            // rbtn_Population_Selected
            // 
            this.rbtn_Population_Selected.AutoSize = true;
            this.rbtn_Population_Selected.Checked = true;
            this.rbtn_Population_Selected.Location = new System.Drawing.Point(6, 34);
            this.rbtn_Population_Selected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbtn_Population_Selected.Name = "rbtn_Population_Selected";
            this.rbtn_Population_Selected.Size = new System.Drawing.Size(97, 24);
            this.rbtn_Population_Selected.TabIndex = 25;
            this.rbtn_Population_Selected.TabStop = true;
            this.rbtn_Population_Selected.Text = "Selected";
            this.rbtn_Population_Selected.UseVisualStyleBackColor = true;
            this.rbtn_Population_Selected.CheckedChanged += new System.EventHandler(this.rbtn_Population_Selected_CheckedChanged);
            // 
            // chbx_Rebuild
            // 
            this.chbx_Rebuild.AutoSize = true;
            this.chbx_Rebuild.Location = new System.Drawing.Point(239, 66);
            this.chbx_Rebuild.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Rebuild.Name = "chbx_Rebuild";
            this.chbx_Rebuild.Size = new System.Drawing.Size(89, 24);
            this.chbx_Rebuild.TabIndex = 324;
            this.chbx_Rebuild.Text = "Rebuild";
            this.chbx_Rebuild.UseVisualStyleBackColor = true;
            this.chbx_Rebuild.Visible = false;
            this.chbx_Rebuild.CheckedChanged += new System.EventHandler(this.chbx_Rebuild_CheckedChanged);
            // 
            // Export_To
            // 
            this.Export_To.BackColor = System.Drawing.SystemColors.Control;
            this.Export_To.Enabled = false;
            this.Export_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Export_To.Location = new System.Drawing.Point(124, 255);
            this.Export_To.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Export_To.Name = "Export_To";
            this.Export_To.Size = new System.Drawing.Size(114, 42);
            this.Export_To.TabIndex = 56;
            this.Export_To.Text = "Export";
            this.Export_To.UseVisualStyleBackColor = false;
            this.Export_To.Click += new System.EventHandler(this.Export_To_Click);
            // 
            // chbx_Additional_Manipulations
            // 
            this.chbx_Additional_Manipulations.CheckOnClick = true;
            this.chbx_Additional_Manipulations.FormattingEnabled = true;
            this.chbx_Additional_Manipulations.Items.AddRange(new object[] {
            "01. @Pack Add Increment to all songs Title",
            "02. @Pack Add Increment to all songs Title per artist",
            "03. @Pack Make all DLC IDs unique(&save)",
            "04. @Pack Remove DD",
            "05. <Import and remove DD only for Bass>",
            "06. @Pack Remove DD only for Bass Guitar",
            "07. <Remove the 4sec of the Preview song>",
            "08. @Pack skipp Broken songs",
            "09. @Pack Name to cross-platform Compatible Filenames",
            "10. @Pack Add Preview if missing 00:30 for 30sec (&save)",
            "11. @Pack Make all DLC IDs unique",
            "12. <@PackAdd DD (5 Levels)>",
            "13. Add DD (5 Levels) when missing",
            "14. Import all Duplicates as Alternates",
            "15. Import any Custom as Alternate if an Original exists",
            "16. Move the Imported files to temp/0_old",
            "17. Import with Artist/Title same as Artist/Title Sort",
            "18. Repack with Artist/Title same as Artist/Title Sort",
            "19. <Import without The/Die at the beginning of Artist/Title Sort>",
            "20. <Pack without The/Die at the beginning of Artist/Title Sort>",
            "21. Import with the The/Die at the end of Artist/Title Sort",
            "22. Pack with The/Die at the end of Artist/Title Sort",
            "23. Import with the The/Die only at the end of Artist Sort",
            "24. Pack with The/Die only at the end of Artist Sort",
            "25. @Import Use translation tables for naming standardization",
            "26. If Original don\'t add QAs(NOs;DLC/ORIG;etc.)",
            "27. When packing Add 5 Levels of DD only to Guitar tracks",
            "28. Convert and Transfer/FTP",
            "29. If Original don\'t add QAs(NOs;DLC/ORIG;etc.) except for File Names",
            "30. When NOT importing a Duplicate Move it to _duplicate",
            "31. When NOT importing a broken song Move it to _broken",
            "32. When removing DD use internal logic not DDC",
            "33. When importing alternates add newer/older instead of alt.0author",
            "34. Forcibly Update Import location of all DB fields",
            "35. @Import Add Preview if missing 00:30 for 30sec",
            "36. Remove illegal characters from Songs Metadata",
            "37. Keep the Uncompressed Songs superorganized",
            "38. Import other formats but PC, as well(separately of course)",
            "39. Use only unpacked songs already in the 0/0_Import folder",
            "40. Encrypt PS3 Retails songs, with External tool",
            "41. Delete ORIG HSAN/OGG when Packing Retails songs",
            "42. Try to get Track No. from Spotify ",
            "43. Save Log After Import (DLCManager INSIDE folder)",
            "44. @Import Set the DLCID autom",
            "45. @Pack Set the DLCID autom",
            "46. <Convert Originals>",
            "47. Duplicate Mangement, Title added info is inbetween []",
            "48. Add New Toolkit v. and RePackedByAuthor",
            "49. @Import Remove Multitrack/Live info from Title",
            "50. @Pack Also Copy/FTP",
            "51. @Import place \"obvious\" duplicates at the end of the Process",
            "52. @Import Overrite the XML",
            "53. @Pack keep Bass DD if indicated so",
            "54. @Pack keep All DD if indicated so",
            "55. @Pack consider All songs as beta (place them top of the list)",
            "56. Gen Preview if Preview=Audio or Preview is longer than a min",
            "57. Duplicate manag ignores Multitracks",
            "58. Don\'t save Author when generic (i.e. Custom Song Creator)",
            "59. @Pack try to get Track No again (&don\'t save)",
            "60. @Pack try to get Track No again (&save)",
            "61. @Rebuild don\'t overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)",
            "62. @Rebuild don\'t overwrite Standard Song Info (Cover,Year)",
            "63. <@Pack duplicate singleTracks L->R / R->L>",
            "64. @Pack Remove Remote File if GameData has been read",
            "65. @Pack ONLY Copy/FTP the Last Packed song",
            "66. @Pack ONLY Copy/FTP the Initially Imported song",
            "67. Duplicate manag. ignores Live Songs",
            "68. Import duplicates (hash)",
            "98. @IMPORT>",
            "99. @Pack>"});
            this.chbx_Additional_Manipulations.Location = new System.Drawing.Point(358, 302);
            this.chbx_Additional_Manipulations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Additional_Manipulations.Name = "chbx_Additional_Manipulations";
            this.chbx_Additional_Manipulations.Size = new System.Drawing.Size(440, 151);
            this.chbx_Additional_Manipulations.Sorted = true;
            this.chbx_Additional_Manipulations.TabIndex = 29;
            this.chbx_Additional_Manipulations.Visible = false;
            this.chbx_Additional_Manipulations.SelectedIndexChanged += new System.EventHandler(this.chbx_Additional_Manipualtions_SelectedIndexChanged);
            // 
            // lbl_Mask
            // 
            this.lbl_Mask.AutoSize = true;
            this.lbl_Mask.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Mask.Location = new System.Drawing.Point(9, 678);
            this.lbl_Mask.Name = "lbl_Mask";
            this.lbl_Mask.Size = new System.Drawing.Size(109, 20);
            this.lbl_Mask.TabIndex = 319;
            this.lbl_Mask.Text = "Mask Preview:";
            this.lbl_Mask.Visible = false;
            this.lbl_Mask.Click += new System.EventHandler(this.lbl_Mask_Click);
            // 
            // btn_Preview_Artist_Sort
            // 
            this.btn_Preview_Artist_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist_Sort.Location = new System.Drawing.Point(782, 577);
            this.btn_Preview_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Artist_Sort.Name = "btn_Preview_Artist_Sort";
            this.btn_Preview_Artist_Sort.Size = new System.Drawing.Size(21, 22);
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
            this.cbx_Activ_Artist_Sort.Location = new System.Drawing.Point(760, 578);
            this.cbx_Activ_Artist_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.lbl_Artist_Sort.Location = new System.Drawing.Point(9, 580);
            this.lbl_Artist_Sort.Name = "lbl_Artist_Sort";
            this.lbl_Artist_Sort.Size = new System.Drawing.Size(80, 20);
            this.lbl_Artist_Sort.TabIndex = 316;
            this.lbl_Artist_Sort.Text = "Artist Sort";
            this.lbl_Artist_Sort.Visible = false;
            this.lbl_Artist_Sort.Click += new System.EventHandler(this.lbl_Artist_Sort_Click);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_Artist_Sort.Location = new System.Drawing.Point(598, 575);
            this.cbx_Artist_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Artist_Sort.Name = "cbx_Artist_Sort";
            this.cbx_Artist_Sort.Size = new System.Drawing.Size(156, 28);
            this.cbx_Artist_Sort.TabIndex = 43;
            this.cbx_Artist_Sort.Visible = false;
            this.cbx_Artist_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_Sort_SelectedIndexChanged);
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Location = new System.Drawing.Point(93, 575);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(500, 26);
            this.txt_Artist_Sort.TabIndex = 42;
            this.txt_Artist_Sort.Text = "<Beta><Artist>";
            this.txt_Artist_Sort.Visible = false;
            this.txt_Artist_Sort.TextChanged += new System.EventHandler(this.txt_Artist_Sort_TextChanged);
            // 
            // btn_Preview_File_Name
            // 
            this.btn_Preview_File_Name.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_File_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_File_Name.Location = new System.Drawing.Point(782, 648);
            this.btn_Preview_File_Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_File_Name.Name = "btn_Preview_File_Name";
            this.btn_Preview_File_Name.Size = new System.Drawing.Size(21, 22);
            this.btn_Preview_File_Name.TabIndex = 53;
            this.toolTip1.SetToolTip(this.btn_Preview_File_Name, "Preview File Name Mask");
            this.btn_Preview_File_Name.UseVisualStyleBackColor = false;
            this.btn_Preview_File_Name.Click += new System.EventHandler(this.btn_Preview_File_Name_Click);
            // 
            // btn_Preview_Album
            // 
            this.btn_Preview_Album.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Album.Location = new System.Drawing.Point(782, 611);
            this.btn_Preview_Album.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Album.Name = "btn_Preview_Album";
            this.btn_Preview_Album.Size = new System.Drawing.Size(21, 22);
            this.btn_Preview_Album.TabIndex = 49;
            this.toolTip1.SetToolTip(this.btn_Preview_Album, "Preview Album Mask");
            this.btn_Preview_Album.UseVisualStyleBackColor = false;
            this.btn_Preview_Album.Click += new System.EventHandler(this.btn_Preview_Album_Click);
            // 
            // btn_Preview_Artist
            // 
            this.btn_Preview_Artist.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist.Location = new System.Drawing.Point(782, 546);
            this.btn_Preview_Artist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Artist.Name = "btn_Preview_Artist";
            this.btn_Preview_Artist.Size = new System.Drawing.Size(21, 22);
            this.btn_Preview_Artist.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btn_Preview_Artist, "Preview Artist Mask");
            this.btn_Preview_Artist.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist.Click += new System.EventHandler(this.btn_Preview_Artist_Click);
            // 
            // btn_Preview_Title_Sort
            // 
            this.btn_Preview_Title_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title_Sort.Location = new System.Drawing.Point(782, 509);
            this.btn_Preview_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Title_Sort.Name = "btn_Preview_Title_Sort";
            this.btn_Preview_Title_Sort.Size = new System.Drawing.Size(21, 22);
            this.btn_Preview_Title_Sort.TabIndex = 37;
            this.toolTip1.SetToolTip(this.btn_Preview_Title_Sort, "Preview Title Sort Mask");
            this.btn_Preview_Title_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Title_Sort.Click += new System.EventHandler(this.btn_Preview_Title_Sort_Click);
            // 
            // btn_Preview_Title
            // 
            this.btn_Preview_Title.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title.Location = new System.Drawing.Point(782, 478);
            this.btn_Preview_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Preview_Title.Name = "btn_Preview_Title";
            this.btn_Preview_Title.Size = new System.Drawing.Size(21, 22);
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
            this.cbx_Activ_File_Name.Location = new System.Drawing.Point(760, 649);
            this.cbx_Activ_File_Name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.cbx_Activ_Album.Location = new System.Drawing.Point(760, 614);
            this.cbx_Activ_Album.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.cbx_Activ_Artist.Location = new System.Drawing.Point(760, 546);
            this.cbx_Activ_Artist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.cbx_Activ_Title_Sort.Location = new System.Drawing.Point(760, 512);
            this.cbx_Activ_Title_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.cbx_Activ_Title.Location = new System.Drawing.Point(760, 480);
            this.cbx_Activ_Title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.lbl_Artist.Location = new System.Drawing.Point(10, 546);
            this.lbl_Artist.Name = "lbl_Artist";
            this.lbl_Artist.Size = new System.Drawing.Size(46, 20);
            this.lbl_Artist.TabIndex = 303;
            this.lbl_Artist.Text = "Artist";
            this.lbl_Artist.Visible = false;
            this.lbl_Artist.Click += new System.EventHandler(this.lbl_Artist_Click);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_Artist.Location = new System.Drawing.Point(598, 542);
            this.cbx_Artist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Artist.Name = "cbx_Artist";
            this.cbx_Artist.Size = new System.Drawing.Size(156, 28);
            this.cbx_Artist.TabIndex = 39;
            this.cbx_Artist.Visible = false;
            this.cbx_Artist.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_SelectedIndexChanged);
            // 
            // txt_Artist
            // 
            this.txt_Artist.Location = new System.Drawing.Point(93, 542);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(500, 26);
            this.txt_Artist.TabIndex = 38;
            this.txt_Artist.Text = "<Artist>-<CDLC>-<Avail. Tracks>-<DD>-<QAs>-<Bass_HasDD>";
            this.txt_Artist.Visible = false;
            this.txt_Artist.TextChanged += new System.EventHandler(this.txt_Artist_TextChanged);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_Title.Location = new System.Drawing.Point(598, 474);
            this.cbx_Title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Title.Name = "cbx_Title";
            this.cbx_Title.Size = new System.Drawing.Size(156, 28);
            this.cbx_Title.TabIndex = 31;
            this.cbx_Title.Visible = false;
            this.cbx_Title.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_SelectedIndexChanged);
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(93, 474);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(500, 26);
            this.txt_Title.TabIndex = 30;
            this.txt_Title.Text = "<Title>";
            this.txt_Title.Visible = false;
            this.txt_Title.TextChanged += new System.EventHandler(this.txt_Title_TextChanged);
            // 
            // lbl_File_Name
            // 
            this.lbl_File_Name.AutoSize = true;
            this.lbl_File_Name.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_File_Name.Location = new System.Drawing.Point(9, 648);
            this.lbl_File_Name.Name = "lbl_File_Name";
            this.lbl_File_Name.Size = new System.Drawing.Size(80, 20);
            this.lbl_File_Name.TabIndex = 297;
            this.lbl_File_Name.Text = "File Name";
            this.lbl_File_Name.Visible = false;
            this.lbl_File_Name.Click += new System.EventHandler(this.lbl_File_Name_Click);
            // 
            // lbl_Album
            // 
            this.lbl_Album.AutoSize = true;
            this.lbl_Album.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Album.Location = new System.Drawing.Point(10, 614);
            this.lbl_Album.Name = "lbl_Album";
            this.lbl_Album.Size = new System.Drawing.Size(54, 20);
            this.lbl_Album.TabIndex = 296;
            this.lbl_Album.Text = "Album";
            this.lbl_Album.Visible = false;
            this.lbl_Album.Click += new System.EventHandler(this.lbl_Album_Click);
            // 
            // lbl_Title_Sort
            // 
            this.lbl_Title_Sort.AutoSize = true;
            this.lbl_Title_Sort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title_Sort.Location = new System.Drawing.Point(10, 512);
            this.lbl_Title_Sort.Name = "lbl_Title_Sort";
            this.lbl_Title_Sort.Size = new System.Drawing.Size(72, 20);
            this.lbl_Title_Sort.TabIndex = 295;
            this.lbl_Title_Sort.Text = "Title Sort";
            this.lbl_Title_Sort.Visible = false;
            this.lbl_Title_Sort.Click += new System.EventHandler(this.lbl_Title_Sort_Click);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_File_Name.Location = new System.Drawing.Point(598, 643);
            this.cbx_File_Name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_File_Name.Name = "cbx_File_Name";
            this.cbx_File_Name.Size = new System.Drawing.Size(156, 28);
            this.cbx_File_Name.TabIndex = 51;
            this.cbx_File_Name.Visible = false;
            this.cbx_File_Name.SelectedIndexChanged += new System.EventHandler(this.cbx_File_Name_SelectedIndexChanged);
            // 
            // txt_File_Name
            // 
            this.txt_File_Name.Location = new System.Drawing.Point(93, 643);
            this.txt_File_Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_File_Name.Name = "txt_File_Name";
            this.txt_File_Name.Size = new System.Drawing.Size(500, 26);
            this.txt_File_Name.TabIndex = 50;
            this.txt_File_Name.Text = "<Beta><Broken><CDLC>-<Artist>-<Year>-<Album><Track No.>-<Title>-<DD>-<Avail. Trac" +
    "ks>-<QAs>-v<Version>-<Bass_HasDD>";
            this.txt_File_Name.Visible = false;
            this.txt_File_Name.TextChanged += new System.EventHandler(this.txt_File_Name_TextChanged);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_Album.Location = new System.Drawing.Point(598, 609);
            this.cbx_Album.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Album.Name = "cbx_Album";
            this.cbx_Album.Size = new System.Drawing.Size(156, 28);
            this.cbx_Album.TabIndex = 47;
            this.cbx_Album.Visible = false;
            this.cbx_Album.SelectedIndexChanged += new System.EventHandler(this.cbx_Album_SelectedIndexChanged);
            // 
            // txt_Album
            // 
            this.txt_Album.Location = new System.Drawing.Point(93, 609);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(500, 26);
            this.txt_Album.TabIndex = 46;
            this.txt_Album.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            this.txt_Album.Visible = false;
            this.txt_Album.TextChanged += new System.EventHandler(this.txt_Album_TextChanged);
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
            "<File Name>",
            "<Bonus>",
            "<Artist Short>",
            "<Album Short>",
            "<Author>",
            "<QAs>",
            "<lastConversionDateTime>",
            "<Avail. Tracks>",
            "<Bass_HasDD>",
            "<Timestamp>",
            "<Live>"});
            this.cbx_Title_Sort.Location = new System.Drawing.Point(598, 508);
            this.cbx_Title_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Title_Sort.Name = "cbx_Title_Sort";
            this.cbx_Title_Sort.Size = new System.Drawing.Size(156, 28);
            this.cbx_Title_Sort.TabIndex = 35;
            this.cbx_Title_Sort.Visible = false;
            this.cbx_Title_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_Sort_SelectedIndexChanged);
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Location = new System.Drawing.Point(93, 508);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(500, 26);
            this.txt_Title_Sort.TabIndex = 34;
            this.txt_Title_Sort.Text = "<Year><Album><Track No.><Title>";
            this.txt_Title_Sort.Visible = false;
            this.txt_Title_Sort.TextChanged += new System.EventHandler(this.txt_Title_Sort_TextChanged);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(10, 482);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(38, 20);
            this.lbl_Title.TabIndex = 300;
            this.lbl_Title.Text = "Title";
            this.lbl_Title.Visible = false;
            this.lbl_Title.Click += new System.EventHandler(this.lbl_Title_Click);
            // 
            // chbx_DebugB
            // 
            this.chbx_DebugB.AutoSize = true;
            this.chbx_DebugB.Location = new System.Drawing.Point(468, 6);
            this.chbx_DebugB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_DebugB.Name = "chbx_DebugB";
            this.chbx_DebugB.Size = new System.Drawing.Size(106, 24);
            this.chbx_DebugB.TabIndex = 16;
            this.chbx_DebugB.Text = "Advanced";
            this.toolTip1.SetToolTip(this.chbx_DebugB, "Activates a series of Fields, Options, Log screens.\r\nDisables also:\r\n25. Import U" +
        "se translation tables for naming standardization\r\n16. Move the Imported files to" +
        " temp/0_old\r\n50. Pack Also Copy/FTP");
            this.chbx_DebugB.UseVisualStyleBackColor = true;
            this.chbx_DebugB.CheckedChanged += new System.EventHandler(this.chbx_DebugB_CheckedChanged);
            // 
            // txt_DBFolder
            // 
            this.txt_DBFolder.Location = new System.Drawing.Point(190, 71);
            this.txt_DBFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_DBFolder.Name = "txt_DBFolder";
            this.txt_DBFolder.Size = new System.Drawing.Size(236, 26);
            this.txt_DBFolder.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txt_DBFolder, "Requires a Access 2010 DB.");
            this.txt_DBFolder.TextChanged += new System.EventHandler(this.DBchanged);
            // 
            // btn_DBFolder
            // 
            this.btn_DBFolder.Location = new System.Drawing.Point(432, 74);
            this.btn_DBFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_DBFolder.Name = "btn_DBFolder";
            this.btn_DBFolder.Size = new System.Drawing.Size(33, 22);
            this.btn_DBFolder.TabIndex = 9;
            this.btn_DBFolder.Text = "...";
            this.btn_DBFolder.UseVisualStyleBackColor = true;
            this.btn_DBFolder.Click += new System.EventHandler(this.btn_DBFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 274;
            this.label1.Text = "DB Folder Path";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(693, 254);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(108, 42);
            this.btn_Close.TabIndex = 55;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_RocksmithDLCPath
            // 
            this.txt_RocksmithDLCPath.Location = new System.Drawing.Point(190, 5);
            this.txt_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_RocksmithDLCPath.Name = "txt_RocksmithDLCPath";
            this.txt_RocksmithDLCPath.Size = new System.Drawing.Size(236, 26);
            this.txt_RocksmithDLCPath.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txt_RocksmithDLCPath, "Import Location.\r\nIf it is the Rocksmith Location then Files will be moved out an" +
        "d only replaced with Manged version at Repack.");
            this.txt_RocksmithDLCPath.TextChanged += new System.EventHandler(this.txt_RocksmithDLCPath_TextChanged);
            // 
            // txt_TempPath
            // 
            this.txt_TempPath.Location = new System.Drawing.Point(190, 37);
            this.txt_TempPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_TempPath.Name = "txt_TempPath";
            this.txt_TempPath.Size = new System.Drawing.Size(236, 26);
            this.txt_TempPath.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txt_TempPath, resources.GetString("txt_TempPath.ToolTip"));
            this.txt_TempPath.TextChanged += new System.EventHandler(this.txt_TempPath_TextChanged);
            // 
            // chbx_CleanTemp
            // 
            this.chbx_CleanTemp.AutoSize = true;
            this.chbx_CleanTemp.Location = new System.Drawing.Point(468, 38);
            this.chbx_CleanTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx_CleanTemp.Name = "chbx_CleanTemp";
            this.chbx_CleanTemp.Size = new System.Drawing.Size(125, 24);
            this.chbx_CleanTemp.TabIndex = 17;
            this.chbx_CleanTemp.Text = "Clean Folder";
            this.toolTip1.SetToolTip(this.chbx_CleanTemp, "Cleans the Temp, Old, Duplicate, Repacked Folders");
            this.chbx_CleanTemp.UseVisualStyleBackColor = true;
            this.chbx_CleanTemp.Visible = false;
            this.chbx_CleanTemp.CheckedChanged += new System.EventHandler(this.chbx_CleanTemp_CheckedChanged);
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(432, 8);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(33, 22);
            this.btn_SteamDLCFolder.TabIndex = 3;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // chbx_CleanDB
            // 
            this.chbx_CleanDB.AutoSize = true;
            this.chbx_CleanDB.Location = new System.Drawing.Point(468, 72);
            this.chbx_CleanDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx_CleanDB.Name = "chbx_CleanDB";
            this.chbx_CleanDB.Size = new System.Drawing.Size(111, 24);
            this.chbx_CleanDB.TabIndex = 18;
            this.chbx_CleanDB.Text = "Clean DBs";
            this.toolTip1.SetToolTip(this.chbx_CleanDB, "Cleans Main & Log, DBs");
            this.chbx_CleanDB.UseVisualStyleBackColor = true;
            this.chbx_CleanDB.Visible = false;
            this.chbx_CleanDB.CheckedChanged += new System.EventHandler(this.chbx_CleanDB_CheckedChanged);
            // 
            // rtxt_StatisticsOnReadDLCs
            // 
            this.rtxt_StatisticsOnReadDLCs.Location = new System.Drawing.Point(9, 302);
            this.rtxt_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxt_StatisticsOnReadDLCs.Name = "rtxt_StatisticsOnReadDLCs";
            this.rtxt_StatisticsOnReadDLCs.Size = new System.Drawing.Size(338, 169);
            this.rtxt_StatisticsOnReadDLCs.TabIndex = 264;
            this.rtxt_StatisticsOnReadDLCs.Text = "";
            this.rtxt_StatisticsOnReadDLCs.Visible = false;
            this.rtxt_StatisticsOnReadDLCs.TextChanged += new System.EventHandler(this.rtxt_StatisticsOnReadDLCs_TextChanged);
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(10, 220);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(790, 31);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 263;
            this.pB_ReadDLCs.Click += new System.EventHandler(this.pB_ReadDLCs_Click);
            // 
            // btn_PopulateDB
            // 
            this.btn_PopulateDB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_PopulateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PopulateDB.Location = new System.Drawing.Point(632, 97);
            this.btn_PopulateDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PopulateDB.Name = "btn_PopulateDB";
            this.btn_PopulateDB.Size = new System.Drawing.Size(171, 38);
            this.btn_PopulateDB.TabIndex = 13;
            this.btn_PopulateDB.Text = "Import DLCs";
            this.btn_PopulateDB.UseVisualStyleBackColor = false;
            this.btn_PopulateDB.Click += new System.EventHandler(this.btn_PopulateDB_Click);
            // 
            // btn_OpenMainDB
            // 
            this.btn_OpenMainDB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_OpenMainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenMainDB.Location = new System.Drawing.Point(632, 40);
            this.btn_OpenMainDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_OpenMainDB.Name = "btn_OpenMainDB";
            this.btn_OpenMainDB.Size = new System.Drawing.Size(171, 54);
            this.btn_OpenMainDB.TabIndex = 10;
            this.btn_OpenMainDB.Text = "Open MainDB";
            this.toolTip1.SetToolTip(this.btn_OpenMainDB, "Main DB Listing All ");
            this.btn_OpenMainDB.UseVisualStyleBackColor = false;
            this.btn_OpenMainDB.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // lbl_RocksmithDLCPath
            // 
            this.lbl_RocksmithDLCPath.AutoSize = true;
            this.lbl_RocksmithDLCPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_RocksmithDLCPath.Location = new System.Drawing.Point(8, 6);
            this.lbl_RocksmithDLCPath.Name = "lbl_RocksmithDLCPath";
            this.lbl_RocksmithDLCPath.Size = new System.Drawing.Size(148, 20);
            this.lbl_RocksmithDLCPath.TabIndex = 259;
            this.lbl_RocksmithDLCPath.Text = "Importing DLC path";
            this.lbl_RocksmithDLCPath.Click += new System.EventHandler(this.lbl_RocksmithDLCPath_Click);
            // 
            // btn_TempPath
            // 
            this.btn_TempPath.Location = new System.Drawing.Point(432, 40);
            this.btn_TempPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_TempPath.Name = "btn_TempPath";
            this.btn_TempPath.Size = new System.Drawing.Size(33, 22);
            this.btn_TempPath.TabIndex = 6;
            this.btn_TempPath.Text = "...";
            this.btn_TempPath.UseVisualStyleBackColor = true;
            this.btn_TempPath.Click += new System.EventHandler(this.btn_TempPath_Click);
            // 
            // lbl_TempFolders
            // 
            this.lbl_TempFolders.AutoSize = true;
            this.lbl_TempFolders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TempFolders.Location = new System.Drawing.Point(6, 40);
            this.lbl_TempFolders.Name = "lbl_TempFolders";
            this.lbl_TempFolders.Size = new System.Drawing.Size(135, 20);
            this.lbl_TempFolders.TabIndex = 258;
            this.lbl_TempFolders.Text = "Temp Folder Path";
            this.lbl_TempFolders.Click += new System.EventHandler(this.lbl_TempFolders_Click);
            // 
            // lbl_PreviewText
            // 
            this.lbl_PreviewText.AutoEllipsis = true;
            this.lbl_PreviewText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_PreviewText.Location = new System.Drawing.Point(130, 678);
            this.lbl_PreviewText.Name = "lbl_PreviewText";
            this.lbl_PreviewText.Size = new System.Drawing.Size(672, 54);
            this.lbl_PreviewText.TabIndex = 285;
            this.lbl_PreviewText.Text = "File name Beta(0) CDLC/ORIG-Artist-Year-Album-TrackNo(if existing)-Title-TrackAva" +
    "il(LRBVS)-Version_PltfrmSpcfcs.psarc";
            this.lbl_PreviewText.Visible = false;
            this.lbl_PreviewText.Click += new System.EventHandler(this.lbl_PreviewText_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // btn_ApplyStandardization
            // 
            this.btn_ApplyStandardization.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ApplyStandardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ApplyStandardization.Location = new System.Drawing.Point(577, 141);
            this.btn_ApplyStandardization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ApplyStandardization.Name = "btn_ApplyStandardization";
            this.btn_ApplyStandardization.Size = new System.Drawing.Size(21, 75);
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
            this.btn_LoadRetailSongs.Location = new System.Drawing.Point(708, 141);
            this.btn_LoadRetailSongs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LoadRetailSongs.Name = "btn_LoadRetailSongs";
            this.btn_LoadRetailSongs.Size = new System.Drawing.Size(21, 75);
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
            this.btn_Standardization.Location = new System.Drawing.Point(603, 138);
            this.btn_Standardization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Standardization.Name = "btn_Standardization";
            this.btn_Standardization.Size = new System.Drawing.Size(99, 78);
            this.btn_Standardization.TabIndex = 11;
            this.btn_Standardization.Text = "Open StandardizationDB";
            this.toolTip1.SetToolTip(this.btn_Standardization, "Here you can decide what Standardizations you want to apply to Artist Names, Albu" +
        "m Names, Covers or Short Names");
            this.btn_Standardization.UseVisualStyleBackColor = false;
            this.btn_Standardization.Click += new System.EventHandler(this.btn_Standardization_Click);
            // 
            // chbx_DefaultDB
            // 
            this.chbx_DefaultDB.AutoSize = true;
            this.chbx_DefaultDB.Location = new System.Drawing.Point(468, 105);
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
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(736, 138);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(67, 78);
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
            this.btn_GoImport.Location = new System.Drawing.Point(152, 6);
            this.btn_GoImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_GoImport.Name = "btn_GoImport";
            this.btn_GoImport.Size = new System.Drawing.Size(32, 22);
            this.btn_GoImport.TabIndex = 1;
            this.btn_GoImport.Text = "->";
            this.btn_GoImport.UseVisualStyleBackColor = true;
            this.btn_GoImport.Click += new System.EventHandler(this.btn_GoImport_Click);
            // 
            // btm_GoTemp
            // 
            this.btm_GoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_GoTemp.Location = new System.Drawing.Point(152, 38);
            this.btm_GoTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btm_GoTemp.Name = "btm_GoTemp";
            this.btm_GoTemp.Size = new System.Drawing.Size(32, 22);
            this.btm_GoTemp.TabIndex = 4;
            this.btm_GoTemp.Text = "->";
            this.btm_GoTemp.UseVisualStyleBackColor = true;
            this.btm_GoTemp.Click += new System.EventHandler(this.btm_GoTemp_Click);
            // 
            // btm_GoDB
            // 
            this.btm_GoDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_GoDB.Location = new System.Drawing.Point(152, 72);
            this.btm_GoDB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btm_GoDB.Name = "btm_GoDB";
            this.btm_GoDB.Size = new System.Drawing.Size(32, 22);
            this.btm_GoDB.TabIndex = 7;
            this.btm_GoDB.Text = "->";
            this.btm_GoDB.UseVisualStyleBackColor = true;
            this.btm_GoDB.Click += new System.EventHandler(this.btm_GoDB_Click);
            // 
            // btn_Log
            // 
            this.btn_Log.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Log.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Log.Location = new System.Drawing.Point(9, 255);
            this.btn_Log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Log.Name = "btn_Log";
            this.btn_Log.Size = new System.Drawing.Size(116, 42);
            this.btn_Log.TabIndex = 54;
            this.btn_Log.Text = "Log";
            this.btn_Log.UseVisualStyleBackColor = false;
            this.btn_Log.Click += new System.EventHandler(this.btn_Log_Click);
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
            this.chbx_Configurations.Location = new System.Drawing.Point(576, 5);
            this.chbx_Configurations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Configurations.Name = "chbx_Configurations";
            this.chbx_Configurations.Size = new System.Drawing.Size(153, 28);
            this.chbx_Configurations.TabIndex = 325;
            this.chbx_Configurations.Text = "Select Profile";
            this.chbx_Configurations.DropDown += new System.EventHandler(this.chbx_Configurations_DropDown);
            this.chbx_Configurations.SelectedIndexChanged += new System.EventHandler(this.chbx_Configurations_SelectedIndexChanged);
            this.chbx_Configurations.TextChanged += new System.EventHandler(this.chbx_Configurations_TextChanged);
            // 
            // btn_ProfilesSave
            // 
            this.btn_ProfilesSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ProfilesSave.Location = new System.Drawing.Point(736, 4);
            this.btn_ProfilesSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ProfilesSave.Name = "btn_ProfilesSave";
            this.btn_ProfilesSave.Size = new System.Drawing.Size(32, 31);
            this.btn_ProfilesSave.TabIndex = 386;
            this.btn_ProfilesSave.Text = "+";
            this.btn_ProfilesSave.UseVisualStyleBackColor = true;
            this.btn_ProfilesSave.Click += new System.EventHandler(this.btn_ProfilesSave_Click);
            // 
            // btn_ProfileRemove
            // 
            this.btn_ProfileRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ProfileRemove.Location = new System.Drawing.Point(771, 4);
            this.btn_ProfileRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ProfileRemove.Name = "btn_ProfileRemove";
            this.btn_ProfileRemove.Size = new System.Drawing.Size(32, 31);
            this.btn_ProfileRemove.TabIndex = 388;
            this.btn_ProfileRemove.Text = "-";
            this.btn_ProfileRemove.UseVisualStyleBackColor = true;
            this.btn_ProfileRemove.Click += new System.EventHandler(this.btn_GroupsRemove_Click);
            // 
            // DLCManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_ProfileRemove);
            this.Controls.Add(this.btn_ProfilesSave);
            this.Controls.Add(this.chbx_Configurations);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_Log);
            this.Controls.Add(this.btm_GoDB);
            this.Controls.Add(this.btm_GoTemp);
            this.Controls.Add(this.btn_GoImport);
            this.Controls.Add(this.btn_LoadRetailSongs);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chbx_DefaultDB);
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
            this.Controls.Add(this.chbx_CleanDB);
            this.Controls.Add(this.rtxt_StatisticsOnReadDLCs);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.btn_PopulateDB);
            this.Controls.Add(this.btn_OpenMainDB);
            this.Controls.Add(this.lbl_RocksmithDLCPath);
            this.Controls.Add(this.btn_TempPath);
            this.Controls.Add(this.lbl_TempFolders);
            this.Controls.Add(this.lbl_PreviewText);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DLCManager";
            this.Size = new System.Drawing.Size(920, 1222);
            this.Load += new System.EventHandler(this.DLCManager_Load);
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
        private System.Windows.Forms.CheckBox chbx_CleanDB;
        private System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
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
        private System.Windows.Forms.Button btn_Log;
        private System.Windows.Forms.ComboBox chbx_Configurations;
        private System.Windows.Forms.Button btn_ProfilesSave;
        private System.Windows.Forms.Button btn_ProfileRemove;
    }
}
