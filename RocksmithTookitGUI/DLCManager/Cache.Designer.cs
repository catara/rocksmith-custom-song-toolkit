using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class Cache
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            label1 = new System.Windows.Forms.Label();
            chbx_RemoveDD = new System.Windows.Forms.CheckBox();
            cmb_Filter = new System.Windows.Forms.ComboBox();
            btn_SelectAll = new System.Windows.Forms.Button();
            chbx_Selected = new System.Windows.Forms.CheckBox();
            btn_SelectNone = new System.Windows.Forms.Button();
            btn_GroupLoad = new System.Windows.Forms.Button();
            btn_GroupSave = new System.Windows.Forms.Button();
            btn_GroupsAdd = new System.Windows.Forms.Button();
            btn_GroupsRemove = new System.Windows.Forms.Button();
            chbx_AllGroups = new System.Windows.Forms.CheckedListBox();
            chbx_Group = new System.Windows.Forms.ComboBox();
            chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btn_Export2IndividualSong = new System.Windows.Forms.Button();
            txt_FTPPath = new CueTextBox();
            btn_FTP = new System.Windows.Forms.Button();
            chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            cbx_Format = new System.Windows.Forms.ComboBox();
            btn_SteamDLCFolder = new System.Windows.Forms.Button();
            btn_Prev = new System.Windows.Forms.Button();
            btn_NextItem = new System.Windows.Forms.Button();
            txt_Counter = new CueTextBox();
            btn_ExpandSelCrossP = new System.Windows.Forms.Button();
            chbx_Autosave = new System.Windows.Forms.CheckBox();
            btn_OpeHSAN = new System.Windows.Forms.Button();
            btn_OpenCorrespondence = new System.Windows.Forms.Button();
            txt_AudioPreviewPath = new CueTextBox();
            txt_AudioPath = new CueTextBox();
            pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            chbx_AutoPlay = new System.Windows.Forms.CheckBox();
            chbx_Songs2Cache = new System.Windows.Forms.CheckBox();
            txt_Platform = new CueTextBox();
            txt_SongsHSANPath = new CueTextBox();
            txt_PSARCName = new CueTextBox();
            btn_InvertAll = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            btn_PlayPreview = new System.Windows.Forms.Button();
            btn_PlayAudio = new System.Windows.Forms.Button();
            lbl_NoRec = new System.Windows.Forms.Label();
            btn_GenerateHSAN = new System.Windows.Forms.Button();
            rtxt_Comments = new System.Windows.Forms.RichTextBox();
            txt_AlbumArtPath = new CueTextBox();
            txt_Identifier = new CueTextBox();
            btn_DecompressAll = new System.Windows.Forms.Button();
            btn_Close = new System.Windows.Forms.Button();
            txt_Arrangements = new CueTextBox();
            txt_Title = new CueTextBox();
            txt_ArtistSort = new CueTextBox();
            txt_Artist = new CueTextBox();
            txt_AlbumYear = new CueTextBox();
            chbx_Removed = new System.Windows.Forms.CheckBox();
            picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            btn_Save = new System.Windows.Forms.Button();
            txt_ID = new CueTextBox();
            btn_OpenMainDB = new System.Windows.Forms.Button();
            txt_Album = new CueTextBox();
            DataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(chbx_RemoveDD);
            splitContainer1.Panel1.Controls.Add(cmb_Filter);
            splitContainer1.Panel1.Controls.Add(btn_SelectAll);
            splitContainer1.Panel1.Controls.Add(chbx_Selected);
            splitContainer1.Panel1.Controls.Add(btn_SelectNone);
            splitContainer1.Panel1.Controls.Add(btn_GroupLoad);
            splitContainer1.Panel1.Controls.Add(btn_GroupSave);
            splitContainer1.Panel1.Controls.Add(btn_GroupsAdd);
            splitContainer1.Panel1.Controls.Add(btn_GroupsRemove);
            splitContainer1.Panel1.Controls.Add(chbx_AllGroups);
            splitContainer1.Panel1.Controls.Add(chbx_Group);
            splitContainer1.Panel1.Controls.Add(chbx_RemoveBassDD);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Controls.Add(btn_Prev);
            splitContainer1.Panel1.Controls.Add(btn_NextItem);
            splitContainer1.Panel1.Controls.Add(txt_Counter);
            splitContainer1.Panel1.Controls.Add(btn_ExpandSelCrossP);
            splitContainer1.Panel1.Controls.Add(chbx_Autosave);
            splitContainer1.Panel1.Controls.Add(btn_OpeHSAN);
            splitContainer1.Panel1.Controls.Add(btn_OpenCorrespondence);
            splitContainer1.Panel1.Controls.Add(txt_AudioPreviewPath);
            splitContainer1.Panel1.Controls.Add(txt_AudioPath);
            splitContainer1.Panel1.Controls.Add(pB_ReadDLCs);
            splitContainer1.Panel1.Controls.Add(chbx_AutoPlay);
            splitContainer1.Panel1.Controls.Add(chbx_Songs2Cache);
            splitContainer1.Panel1.Controls.Add(txt_Platform);
            splitContainer1.Panel1.Controls.Add(txt_SongsHSANPath);
            splitContainer1.Panel1.Controls.Add(txt_PSARCName);
            splitContainer1.Panel1.Controls.Add(btn_InvertAll);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(btn_PlayPreview);
            splitContainer1.Panel1.Controls.Add(btn_PlayAudio);
            splitContainer1.Panel1.Controls.Add(lbl_NoRec);
            splitContainer1.Panel1.Controls.Add(btn_GenerateHSAN);
            splitContainer1.Panel1.Controls.Add(rtxt_Comments);
            splitContainer1.Panel1.Controls.Add(txt_AlbumArtPath);
            splitContainer1.Panel1.Controls.Add(txt_Identifier);
            splitContainer1.Panel1.Controls.Add(btn_DecompressAll);
            splitContainer1.Panel1.Controls.Add(btn_Close);
            splitContainer1.Panel1.Controls.Add(txt_Arrangements);
            splitContainer1.Panel1.Controls.Add(txt_Title);
            splitContainer1.Panel1.Controls.Add(txt_ArtistSort);
            splitContainer1.Panel1.Controls.Add(txt_Artist);
            splitContainer1.Panel1.Controls.Add(txt_AlbumYear);
            splitContainer1.Panel1.Controls.Add(chbx_Removed);
            splitContainer1.Panel1.Controls.Add(picbx_AlbumArtPath);
            splitContainer1.Panel1.Controls.Add(btn_Save);
            splitContainer1.Panel1.Controls.Add(txt_ID);
            splitContainer1.Panel1.Controls.Add(btn_OpenMainDB);
            splitContainer1.Panel1.Controls.Add(txt_Album);
            splitContainer1.Panel1MinSize = 220;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(DataGridView1);
            splitContainer1.Size = new System.Drawing.Size(1241, 440);
            splitContainer1.SplitterDistance = 220;
            splitContainer1.TabIndex = 397;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(162, 95);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(36, 16);
            label1.TabIndex = 499;
            label1.Text = "Filter:";
            // 
            // chbx_RemoveDD
            // 
            chbx_RemoveDD.Checked = true;
            chbx_RemoveDD.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_RemoveDD.Enabled = false;
            chbx_RemoveDD.Location = new System.Drawing.Point(1046, 133);
            chbx_RemoveDD.Margin = new System.Windows.Forms.Padding(2);
            chbx_RemoveDD.Name = "chbx_RemoveDD";
            chbx_RemoveDD.Size = new System.Drawing.Size(102, 18);
            chbx_RemoveDD.TabIndex = 498;
            chbx_RemoveDD.Text = "Remove DD";
            chbx_RemoveDD.UseVisualStyleBackColor = true;
            // 
            // cmb_Filter
            // 
            cmb_Filter.FormattingEnabled = true;
            cmb_Filter.Items.AddRange(new object[] { "0ALL", "PC", "Mac", "PS3", "XBOX", "Removed", "Selected" });
            cmb_Filter.Location = new System.Drawing.Point(202, 95);
            cmb_Filter.Margin = new System.Windows.Forms.Padding(2);
            cmb_Filter.Name = "cmb_Filter";
            cmb_Filter.Size = new System.Drawing.Size(140, 23);
            cmb_Filter.TabIndex = 497;
            // 
            // btn_SelectAll
            // 
            btn_SelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            btn_SelectAll.Location = new System.Drawing.Point(4, 27);
            btn_SelectAll.Margin = new System.Windows.Forms.Padding(2);
            btn_SelectAll.Name = "btn_SelectAll";
            btn_SelectAll.Size = new System.Drawing.Size(74, 22);
            btn_SelectAll.TabIndex = 495;
            btn_SelectAll.Text = "Select All";
            btn_SelectAll.UseVisualStyleBackColor = true;
            btn_SelectAll.Click += btn_SelectAll_Click;
            // 
            // chbx_Selected
            // 
            chbx_Selected.Location = new System.Drawing.Point(82, 61);
            chbx_Selected.Margin = new System.Windows.Forms.Padding(2);
            chbx_Selected.Name = "chbx_Selected";
            chbx_Selected.Size = new System.Drawing.Size(78, 18);
            chbx_Selected.TabIndex = 494;
            chbx_Selected.Text = "Selected";
            chbx_Selected.UseVisualStyleBackColor = true;
            chbx_Selected.Click += chbx_Selected_Click;
            // 
            // btn_SelectNone
            // 
            btn_SelectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            btn_SelectNone.Location = new System.Drawing.Point(4, 5);
            btn_SelectNone.Margin = new System.Windows.Forms.Padding(2);
            btn_SelectNone.Name = "btn_SelectNone";
            btn_SelectNone.Size = new System.Drawing.Size(74, 22);
            btn_SelectNone.TabIndex = 496;
            btn_SelectNone.Text = "Select None";
            btn_SelectNone.UseVisualStyleBackColor = true;
            btn_SelectNone.Click += btn_SelectNone_Click;
            // 
            // btn_GroupLoad
            // 
            btn_GroupLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GroupLoad.Location = new System.Drawing.Point(663, 104);
            btn_GroupLoad.Margin = new System.Windows.Forms.Padding(2);
            btn_GroupLoad.Name = "btn_GroupLoad";
            btn_GroupLoad.Size = new System.Drawing.Size(47, 24);
            btn_GroupLoad.TabIndex = 493;
            btn_GroupLoad.Text = "Load List";
            btn_GroupLoad.UseVisualStyleBackColor = true;
            btn_GroupLoad.Click += btn_GroupLoad_Click;
            // 
            // btn_GroupSave
            // 
            btn_GroupSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GroupSave.Location = new System.Drawing.Point(614, 105);
            btn_GroupSave.Margin = new System.Windows.Forms.Padding(2);
            btn_GroupSave.Name = "btn_GroupSave";
            btn_GroupSave.Size = new System.Drawing.Size(50, 24);
            btn_GroupSave.TabIndex = 492;
            btn_GroupSave.Text = "Save List";
            btn_GroupSave.UseVisualStyleBackColor = true;
            btn_GroupSave.Click += btn_GroupSave_Click;
            // 
            // btn_GroupsAdd
            // 
            btn_GroupsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GroupsAdd.Location = new System.Drawing.Point(710, 107);
            btn_GroupsAdd.Margin = new System.Windows.Forms.Padding(2);
            btn_GroupsAdd.Name = "btn_GroupsAdd";
            btn_GroupsAdd.Size = new System.Drawing.Size(22, 20);
            btn_GroupsAdd.TabIndex = 490;
            btn_GroupsAdd.Text = "+";
            btn_GroupsAdd.UseVisualStyleBackColor = true;
            btn_GroupsAdd.Click += btn_GroupsAdd_Click;
            // 
            // btn_GroupsRemove
            // 
            btn_GroupsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GroupsRemove.Location = new System.Drawing.Point(730, 107);
            btn_GroupsRemove.Margin = new System.Windows.Forms.Padding(2);
            btn_GroupsRemove.Name = "btn_GroupsRemove";
            btn_GroupsRemove.Size = new System.Drawing.Size(22, 20);
            btn_GroupsRemove.TabIndex = 489;
            btn_GroupsRemove.Text = "-";
            btn_GroupsRemove.UseVisualStyleBackColor = true;
            btn_GroupsRemove.Click += btn_GroupsRemove_Click;
            // 
            // chbx_AllGroups
            // 
            chbx_AllGroups.CheckOnClick = true;
            chbx_AllGroups.FormattingEnabled = true;
            chbx_AllGroups.Location = new System.Drawing.Point(614, 7);
            chbx_AllGroups.Margin = new System.Windows.Forms.Padding(2);
            chbx_AllGroups.Name = "chbx_AllGroups";
            chbx_AllGroups.Size = new System.Drawing.Size(186, 94);
            chbx_AllGroups.Sorted = true;
            chbx_AllGroups.TabIndex = 491;
            // 
            // chbx_Group
            // 
            chbx_Group.FormattingEnabled = true;
            chbx_Group.Location = new System.Drawing.Point(614, 133);
            chbx_Group.Margin = new System.Windows.Forms.Padding(2);
            chbx_Group.Name = "chbx_Group";
            chbx_Group.Size = new System.Drawing.Size(186, 23);
            chbx_Group.TabIndex = 488;
            // 
            // chbx_RemoveBassDD
            // 
            chbx_RemoveBassDD.Checked = true;
            chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_RemoveBassDD.Location = new System.Drawing.Point(1046, 115);
            chbx_RemoveBassDD.Margin = new System.Windows.Forms.Padding(2);
            chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            chbx_RemoveBassDD.Size = new System.Drawing.Size(120, 18);
            chbx_RemoveBassDD.TabIndex = 487;
            chbx_RemoveBassDD.Text = "Remove Bass DD";
            chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_Export2IndividualSong);
            groupBox1.Controls.Add(txt_FTPPath);
            groupBox1.Controls.Add(btn_FTP);
            groupBox1.Controls.Add(chbx_PreSavedFTP);
            groupBox1.Controls.Add(cbx_Format);
            groupBox1.Controls.Add(btn_SteamDLCFolder);
            groupBox1.Location = new System.Drawing.Point(932, 33);
            groupBox1.Margin = new System.Windows.Forms.Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2);
            groupBox1.Size = new System.Drawing.Size(304, 64);
            groupBox1.TabIndex = 486;
            groupBox1.TabStop = false;
            groupBox1.Text = "Package";
            // 
            // btn_Export2IndividualSong
            // 
            btn_Export2IndividualSong.ForeColor = System.Drawing.Color.DodgerBlue;
            btn_Export2IndividualSong.Location = new System.Drawing.Point(220, 14);
            btn_Export2IndividualSong.Margin = new System.Windows.Forms.Padding(2);
            btn_Export2IndividualSong.Name = "btn_Export2IndividualSong";
            btn_Export2IndividualSong.Size = new System.Drawing.Size(74, 46);
            btn_Export2IndividualSong.TabIndex = 449;
            btn_Export2IndividualSong.Text = "Export Individual";
            btn_Export2IndividualSong.UseVisualStyleBackColor = true;
            btn_Export2IndividualSong.Click += btn_Export2IndividualSong_Click;
            // 
            // txt_FTPPath
            // 
            txt_FTPPath.Cue = "FTP_Path";
            txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            txt_FTPPath.Location = new System.Drawing.Point(114, 14);
            txt_FTPPath.Margin = new System.Windows.Forms.Padding(2);
            txt_FTPPath.Name = "txt_FTPPath";
            txt_FTPPath.Size = new System.Drawing.Size(76, 20);
            txt_FTPPath.TabIndex = 292;
            txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_FTP
            // 
            btn_FTP.Location = new System.Drawing.Point(4, 14);
            btn_FTP.Margin = new System.Windows.Forms.Padding(2);
            btn_FTP.Name = "btn_FTP";
            btn_FTP.Size = new System.Drawing.Size(110, 44);
            btn_FTP.TabIndex = 291;
            btn_FTP.Text = "Copy/FTP Back to Game Folder";
            btn_FTP.UseVisualStyleBackColor = true;
            btn_FTP.Click += btn_FTP_Click;
            // 
            // chbx_PreSavedFTP
            // 
            chbx_PreSavedFTP.FormattingEnabled = true;
            chbx_PreSavedFTP.Items.AddRange(new object[] { "US", "EU" });
            chbx_PreSavedFTP.Location = new System.Drawing.Point(173, 35);
            chbx_PreSavedFTP.Margin = new System.Windows.Forms.Padding(2);
            chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            chbx_PreSavedFTP.Size = new System.Drawing.Size(46, 23);
            chbx_PreSavedFTP.TabIndex = 434;
            chbx_PreSavedFTP.Text = "US";
            chbx_PreSavedFTP.Visible = false;
            // 
            // cbx_Format
            // 
            cbx_Format.FormattingEnabled = true;
            cbx_Format.Items.AddRange(new object[] { "PC", "PS3", "Mac", "PS4", "XBOX360" });
            cbx_Format.Location = new System.Drawing.Point(114, 36);
            cbx_Format.Margin = new System.Windows.Forms.Padding(2);
            cbx_Format.Name = "cbx_Format";
            cbx_Format.Size = new System.Drawing.Size(57, 23);
            cbx_Format.TabIndex = 306;
            cbx_Format.Text = "PS3";
            // 
            // btn_SteamDLCFolder
            // 
            btn_SteamDLCFolder.Location = new System.Drawing.Point(196, 16);
            btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2);
            btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 14);
            btn_SteamDLCFolder.TabIndex = 307;
            btn_SteamDLCFolder.Text = "...";
            btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            btn_SteamDLCFolder.Click += btn_SteamDLCFolder_Click;
            // 
            // btn_Prev
            // 
            btn_Prev.Location = new System.Drawing.Point(124, 21);
            btn_Prev.Margin = new System.Windows.Forms.Padding(2);
            btn_Prev.Name = "btn_Prev";
            btn_Prev.Size = new System.Drawing.Size(18, 22);
            btn_Prev.TabIndex = 485;
            btn_Prev.Text = "<";
            btn_Prev.UseVisualStyleBackColor = true;
            btn_Prev.Click += btn_Prev_Click;
            // 
            // btn_NextItem
            // 
            btn_NextItem.Location = new System.Drawing.Point(142, 21);
            btn_NextItem.Margin = new System.Windows.Forms.Padding(2);
            btn_NextItem.Name = "btn_NextItem";
            btn_NextItem.Size = new System.Drawing.Size(18, 22);
            btn_NextItem.TabIndex = 484;
            btn_NextItem.Text = ">";
            btn_NextItem.UseVisualStyleBackColor = true;
            btn_NextItem.Click += btn_NextItem_Click;
            // 
            // txt_Counter
            // 
            txt_Counter.Cue = "Up/Down";
            txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Counter.ForeColor = System.Drawing.Color.Gray;
            txt_Counter.Location = new System.Drawing.Point(750, 109);
            txt_Counter.Margin = new System.Windows.Forms.Padding(2);
            txt_Counter.Name = "txt_Counter";
            txt_Counter.Size = new System.Drawing.Size(52, 20);
            txt_Counter.TabIndex = 483;
            txt_Counter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_Counter.Visible = false;
            // 
            // btn_ExpandSelCrossP
            // 
            btn_ExpandSelCrossP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            btn_ExpandSelCrossP.Location = new System.Drawing.Point(4, 84);
            btn_ExpandSelCrossP.Margin = new System.Windows.Forms.Padding(2);
            btn_ExpandSelCrossP.Name = "btn_ExpandSelCrossP";
            btn_ExpandSelCrossP.Size = new System.Drawing.Size(156, 46);
            btn_ExpandSelCrossP.TabIndex = 482;
            btn_ExpandSelCrossP.Text = "Extend current Selection cross Platforms";
            btn_ExpandSelCrossP.UseVisualStyleBackColor = true;
            btn_ExpandSelCrossP.Click += btn_ExpandSelCrossP_Click;
            // 
            // chbx_Autosave
            // 
            chbx_Autosave.Checked = true;
            chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_Autosave.Location = new System.Drawing.Point(994, 11);
            chbx_Autosave.Margin = new System.Windows.Forms.Padding(2);
            chbx_Autosave.Name = "chbx_Autosave";
            chbx_Autosave.Size = new System.Drawing.Size(85, 18);
            chbx_Autosave.TabIndex = 481;
            chbx_Autosave.Text = "AutoSave";
            chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // btn_OpeHSAN
            // 
            btn_OpeHSAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_OpeHSAN.Location = new System.Drawing.Point(4, 179);
            btn_OpeHSAN.Margin = new System.Windows.Forms.Padding(2);
            btn_OpeHSAN.Name = "btn_OpeHSAN";
            btn_OpeHSAN.Size = new System.Drawing.Size(110, 36);
            btn_OpeHSAN.TabIndex = 480;
            btn_OpeHSAN.Text = "Open HSAN file";
            btn_OpeHSAN.UseVisualStyleBackColor = true;
            btn_OpeHSAN.Click += btn_OpeHSAN_Click;
            // 
            // btn_OpenCorrespondence
            // 
            btn_OpenCorrespondence.BackColor = System.Drawing.SystemColors.Info;
            btn_OpenCorrespondence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_OpenCorrespondence.Location = new System.Drawing.Point(4, 133);
            btn_OpenCorrespondence.Margin = new System.Windows.Forms.Padding(2);
            btn_OpenCorrespondence.Name = "btn_OpenCorrespondence";
            btn_OpenCorrespondence.Size = new System.Drawing.Size(110, 46);
            btn_OpenCorrespondence.TabIndex = 479;
            btn_OpenCorrespondence.Text = "Open WEM2OGG Corespondence DB";
            btn_OpenCorrespondence.UseVisualStyleBackColor = false;
            btn_OpenCorrespondence.Click += btn_OpenCorrespondence_Click;
            // 
            // txt_AudioPreviewPath
            // 
            txt_AudioPreviewPath.Cue = "Audio Preview Path";
            txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            txt_AudioPreviewPath.Location = new System.Drawing.Point(806, 160);
            txt_AudioPreviewPath.Margin = new System.Windows.Forms.Padding(2);
            txt_AudioPreviewPath.Name = "txt_AudioPreviewPath";
            txt_AudioPreviewPath.ReadOnly = true;
            txt_AudioPreviewPath.Size = new System.Drawing.Size(420, 20);
            txt_AudioPreviewPath.TabIndex = 478;
            txt_AudioPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AudioPreviewPath.Visible = false;
            // 
            // txt_AudioPath
            // 
            txt_AudioPath.Cue = "Audio Path";
            txt_AudioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioPath.ForeColor = System.Drawing.Color.Gray;
            txt_AudioPath.Location = new System.Drawing.Point(614, 160);
            txt_AudioPath.Margin = new System.Windows.Forms.Padding(2);
            txt_AudioPath.Name = "txt_AudioPath";
            txt_AudioPath.ReadOnly = true;
            txt_AudioPath.Size = new System.Drawing.Size(188, 20);
            txt_AudioPath.TabIndex = 477;
            txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AudioPath.Visible = false;
            // 
            // pB_ReadDLCs
            // 
            pB_ReadDLCs.Location = new System.Drawing.Point(346, 181);
            pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            pB_ReadDLCs.Maximum = 20000;
            pB_ReadDLCs.Name = "pB_ReadDLCs";
            pB_ReadDLCs.Size = new System.Drawing.Size(880, 24);
            pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.TabIndex = 476;
            // 
            // chbx_AutoPlay
            // 
            chbx_AutoPlay.Enabled = false;
            chbx_AutoPlay.Location = new System.Drawing.Point(202, 122);
            chbx_AutoPlay.Margin = new System.Windows.Forms.Padding(2);
            chbx_AutoPlay.Name = "chbx_AutoPlay";
            chbx_AutoPlay.Size = new System.Drawing.Size(136, 18);
            chbx_AutoPlay.TabIndex = 475;
            chbx_AutoPlay.Text = "<AutoPlay>";
            chbx_AutoPlay.UseVisualStyleBackColor = true;
            // 
            // chbx_Songs2Cache
            // 
            chbx_Songs2Cache.Checked = true;
            chbx_Songs2Cache.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_Songs2Cache.Location = new System.Drawing.Point(1046, 97);
            chbx_Songs2Cache.Margin = new System.Windows.Forms.Padding(2);
            chbx_Songs2Cache.Name = "chbx_Songs2Cache";
            chbx_Songs2Cache.Size = new System.Drawing.Size(120, 18);
            chbx_Songs2Cache.TabIndex = 474;
            chbx_Songs2Cache.Text = "Only cache.psarc";
            chbx_Songs2Cache.UseVisualStyleBackColor = true;
            // 
            // txt_Platform
            // 
            txt_Platform.Cue = "Arrangements";
            txt_Platform.Enabled = false;
            txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Platform.ForeColor = System.Drawing.Color.Black;
            txt_Platform.Location = new System.Drawing.Point(392, 27);
            txt_Platform.Margin = new System.Windows.Forms.Padding(2);
            txt_Platform.Name = "txt_Platform";
            txt_Platform.Size = new System.Drawing.Size(48, 20);
            txt_Platform.TabIndex = 473;
            txt_Platform.Text = "Platform";
            txt_Platform.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SongsHSANPath
            // 
            txt_SongsHSANPath.Cue = "Arrangements";
            txt_SongsHSANPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_SongsHSANPath.ForeColor = System.Drawing.Color.Black;
            txt_SongsHSANPath.Location = new System.Drawing.Point(390, 67);
            txt_SongsHSANPath.Margin = new System.Windows.Forms.Padding(2);
            txt_SongsHSANPath.Name = "txt_SongsHSANPath";
            txt_SongsHSANPath.ReadOnly = true;
            txt_SongsHSANPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            txt_SongsHSANPath.Size = new System.Drawing.Size(222, 20);
            txt_SongsHSANPath.TabIndex = 472;
            txt_SongsHSANPath.Text = "Songs.HSAN Path";
            txt_SongsHSANPath.Visible = false;
            // 
            // txt_PSARCName
            // 
            txt_PSARCName.Cue = "Arrangements";
            txt_PSARCName.Enabled = false;
            txt_PSARCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PSARCName.ForeColor = System.Drawing.Color.Black;
            txt_PSARCName.Location = new System.Drawing.Point(482, 27);
            txt_PSARCName.Margin = new System.Windows.Forms.Padding(2);
            txt_PSARCName.Name = "txt_PSARCName";
            txt_PSARCName.Size = new System.Drawing.Size(128, 20);
            txt_PSARCName.TabIndex = 471;
            txt_PSARCName.Text = "PSARC Name";
            txt_PSARCName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_InvertAll
            // 
            btn_InvertAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            btn_InvertAll.Location = new System.Drawing.Point(4, 47);
            btn_InvertAll.Margin = new System.Windows.Forms.Padding(2);
            btn_InvertAll.Name = "btn_InvertAll";
            btn_InvertAll.Size = new System.Drawing.Size(74, 26);
            btn_InvertAll.TabIndex = 470;
            btn_InvertAll.Text = "Invert All";
            btn_InvertAll.UseVisualStyleBackColor = true;
            btn_InvertAll.Click += btn_InvertAll_Click;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(342, 95);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(65, 16);
            label2.TabIndex = 469;
            label2.Text = "Comments";
            // 
            // btn_PlayPreview
            // 
            btn_PlayPreview.Location = new System.Drawing.Point(274, 145);
            btn_PlayPreview.Margin = new System.Windows.Forms.Padding(2);
            btn_PlayPreview.Name = "btn_PlayPreview";
            btn_PlayPreview.Size = new System.Drawing.Size(68, 70);
            btn_PlayPreview.TabIndex = 468;
            btn_PlayPreview.Text = "Play Preview";
            btn_PlayPreview.UseVisualStyleBackColor = true;
            btn_PlayPreview.Click += btn_PlayPreview_Click;
            // 
            // btn_PlayAudio
            // 
            btn_PlayAudio.Location = new System.Drawing.Point(202, 145);
            btn_PlayAudio.Margin = new System.Windows.Forms.Padding(2);
            btn_PlayAudio.Name = "btn_PlayAudio";
            btn_PlayAudio.Size = new System.Drawing.Size(68, 70);
            btn_PlayAudio.TabIndex = 467;
            btn_PlayAudio.Text = "Play Audio";
            btn_PlayAudio.UseVisualStyleBackColor = true;
            btn_PlayAudio.Click += btn_PlayAudio_Click;
            // 
            // lbl_NoRec
            // 
            lbl_NoRec.Location = new System.Drawing.Point(82, 7);
            lbl_NoRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbl_NoRec.Name = "lbl_NoRec";
            lbl_NoRec.Size = new System.Drawing.Size(52, 16);
            lbl_NoRec.TabIndex = 466;
            lbl_NoRec.Text = " Records";
            // 
            // btn_GenerateHSAN
            // 
            btn_GenerateHSAN.Location = new System.Drawing.Point(934, 95);
            btn_GenerateHSAN.Margin = new System.Windows.Forms.Padding(2);
            btn_GenerateHSAN.Name = "btn_GenerateHSAN";
            btn_GenerateHSAN.Size = new System.Drawing.Size(110, 61);
            btn_GenerateHSAN.TabIndex = 465;
            btn_GenerateHSAN.Text = "Regenerate HSAN and  Pack";
            btn_GenerateHSAN.UseVisualStyleBackColor = true;
            btn_GenerateHSAN.Click += btn_GenerateHSAN_Click;
            // 
            // rtxt_Comments
            // 
            rtxt_Comments.Location = new System.Drawing.Point(346, 111);
            rtxt_Comments.Margin = new System.Windows.Forms.Padding(2);
            rtxt_Comments.Name = "rtxt_Comments";
            rtxt_Comments.Size = new System.Drawing.Size(266, 68);
            rtxt_Comments.TabIndex = 464;
            rtxt_Comments.Text = "";
            // 
            // txt_AlbumArtPath
            // 
            txt_AlbumArtPath.Cue = "Album Art Path";
            txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumArtPath.Location = new System.Drawing.Point(806, 136);
            txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2);
            txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            txt_AlbumArtPath.ReadOnly = true;
            txt_AlbumArtPath.Size = new System.Drawing.Size(126, 20);
            txt_AlbumArtPath.TabIndex = 463;
            txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AlbumArtPath.Visible = false;
            // 
            // txt_Identifier
            // 
            txt_Identifier.Cue = "IDentifier";
            txt_Identifier.Enabled = false;
            txt_Identifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Identifier.ForeColor = System.Drawing.Color.Gray;
            txt_Identifier.Location = new System.Drawing.Point(388, 45);
            txt_Identifier.Margin = new System.Windows.Forms.Padding(2);
            txt_Identifier.Name = "txt_Identifier";
            txt_Identifier.Size = new System.Drawing.Size(222, 20);
            txt_Identifier.TabIndex = 462;
            // 
            // btn_DecompressAll
            // 
            btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_DecompressAll.Location = new System.Drawing.Point(112, 181);
            btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            btn_DecompressAll.Name = "btn_DecompressAll";
            btn_DecompressAll.Size = new System.Drawing.Size(86, 34);
            btn_DecompressAll.TabIndex = 461;
            btn_DecompressAll.Text = "Open Main DB";
            btn_DecompressAll.UseVisualStyleBackColor = false;
            btn_DecompressAll.Click += btn_DecompressAll_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Close.Location = new System.Drawing.Point(1164, 8);
            btn_Close.Margin = new System.Windows.Forms.Padding(2);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(72, 26);
            btn_Close.TabIndex = 460;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // txt_Arrangements
            // 
            txt_Arrangements.Cue = "Arrangements";
            txt_Arrangements.Enabled = false;
            txt_Arrangements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Arrangements.ForeColor = System.Drawing.Color.Gray;
            txt_Arrangements.Location = new System.Drawing.Point(388, 7);
            txt_Arrangements.Margin = new System.Windows.Forms.Padding(2);
            txt_Arrangements.Name = "txt_Arrangements";
            txt_Arrangements.Size = new System.Drawing.Size(222, 20);
            txt_Arrangements.TabIndex = 459;
            txt_Arrangements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Title
            // 
            txt_Title.Cue = "Title";
            txt_Title.Enabled = false;
            txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Title.ForeColor = System.Drawing.Color.Gray;
            txt_Title.Location = new System.Drawing.Point(166, 49);
            txt_Title.Margin = new System.Windows.Forms.Padding(2);
            txt_Title.Name = "txt_Title";
            txt_Title.Size = new System.Drawing.Size(222, 20);
            txt_Title.TabIndex = 458;
            // 
            // txt_ArtistSort
            // 
            txt_ArtistSort.Cue = "ArtistSort";
            txt_ArtistSort.Enabled = false;
            txt_ArtistSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ArtistSort.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistSort.Location = new System.Drawing.Point(166, 27);
            txt_ArtistSort.Margin = new System.Windows.Forms.Padding(2);
            txt_ArtistSort.Name = "txt_ArtistSort";
            txt_ArtistSort.Size = new System.Drawing.Size(222, 20);
            txt_ArtistSort.TabIndex = 457;
            // 
            // txt_Artist
            // 
            txt_Artist.Cue = "Artist";
            txt_Artist.Enabled = false;
            txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Artist.ForeColor = System.Drawing.Color.Gray;
            txt_Artist.Location = new System.Drawing.Point(166, 7);
            txt_Artist.Margin = new System.Windows.Forms.Padding(2);
            txt_Artist.Name = "txt_Artist";
            txt_Artist.Size = new System.Drawing.Size(222, 20);
            txt_Artist.TabIndex = 456;
            // 
            // txt_AlbumYear
            // 
            txt_AlbumYear.Cue = "AlbumYear";
            txt_AlbumYear.Enabled = false;
            txt_AlbumYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumYear.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumYear.Location = new System.Drawing.Point(340, 71);
            txt_AlbumYear.Margin = new System.Windows.Forms.Padding(2);
            txt_AlbumYear.Name = "txt_AlbumYear";
            txt_AlbumYear.Size = new System.Drawing.Size(48, 20);
            txt_AlbumYear.TabIndex = 455;
            txt_AlbumYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chbx_Removed
            // 
            chbx_Removed.Location = new System.Drawing.Point(82, 43);
            chbx_Removed.Margin = new System.Windows.Forms.Padding(2);
            chbx_Removed.Name = "chbx_Removed";
            chbx_Removed.Size = new System.Drawing.Size(83, 18);
            chbx_Removed.TabIndex = 451;
            chbx_Removed.Text = "Removed";
            chbx_Removed.UseVisualStyleBackColor = true;
            // 
            // picbx_AlbumArtPath
            // 
            picbx_AlbumArtPath.Location = new System.Drawing.Point(806, 7);
            picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2);
            picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            picbx_AlbumArtPath.Size = new System.Drawing.Size(126, 126);
            picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picbx_AlbumArtPath.TabIndex = 453;
            picbx_AlbumArtPath.TabStop = false;
            // 
            // btn_Save
            // 
            btn_Save.ForeColor = System.Drawing.Color.Green;
            btn_Save.Location = new System.Drawing.Point(1076, 8);
            btn_Save.Margin = new System.Windows.Forms.Padding(2);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new System.Drawing.Size(84, 26);
            btn_Save.TabIndex = 450;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // txt_ID
            // 
            txt_ID.Cue = "ID";
            txt_ID.Enabled = false;
            txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ID.ForeColor = System.Drawing.Color.Gray;
            txt_ID.Location = new System.Drawing.Point(82, 23);
            txt_ID.Margin = new System.Windows.Forms.Padding(2);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new System.Drawing.Size(40, 20);
            txt_ID.TabIndex = 454;
            // 
            // btn_OpenMainDB
            // 
            btn_OpenMainDB.Location = new System.Drawing.Point(112, 135);
            btn_OpenMainDB.Margin = new System.Windows.Forms.Padding(2);
            btn_OpenMainDB.Name = "btn_OpenMainDB";
            btn_OpenMainDB.Size = new System.Drawing.Size(86, 42);
            btn_OpenMainDB.TabIndex = 449;
            btn_OpenMainDB.Text = "Open DB in M$ Access";
            btn_OpenMainDB.UseVisualStyleBackColor = true;
            btn_OpenMainDB.Click += btn_OpenMainDB_Click;
            // 
            // txt_Album
            // 
            txt_Album.Cue = "Album";
            txt_Album.Enabled = false;
            txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_Album.ForeColor = System.Drawing.Color.Gray;
            txt_Album.Location = new System.Drawing.Point(166, 71);
            txt_Album.Margin = new System.Windows.Forms.Padding(2);
            txt_Album.Name = "txt_Album";
            txt_Album.Size = new System.Drawing.Size(172, 20);
            txt_Album.TabIndex = 452;
            // 
            // DataGridView1
            // 
            DataGridView1.AllowUserToAddRows = false;
            DataGridView1.AllowUserToDeleteRows = false;
            DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            DataGridView1.Location = new System.Drawing.Point(0, 0);
            DataGridView1.Margin = new System.Windows.Forms.Padding(2);
            DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridView1.RowHeadersWidth = 61;
            DataGridView1.Size = new System.Drawing.Size(1241, 216);
            DataGridView1.TabIndex = 40;
            DataGridView1.CellMouseEnter += DataGridView1_CellContentClick_1;
            // 
            // Cache
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1241, 440);
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "Cache";
            Text = "List of songs delivered with the Retail version of Rocksmith";
            FormClosing += Cache_FormClosing;
            Load += Cache_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbx_RemoveDD;
        private System.Windows.Forms.ComboBox cmb_Filter;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckBox chbx_Selected;
        private System.Windows.Forms.Button btn_SelectNone;
        private System.Windows.Forms.Button btn_GroupLoad;
        private System.Windows.Forms.Button btn_GroupSave;
        private System.Windows.Forms.Button btn_GroupsAdd;
        private System.Windows.Forms.Button btn_GroupsRemove;
        private System.Windows.Forms.CheckedListBox chbx_AllGroups;
        private System.Windows.Forms.ComboBox chbx_Group;
        private System.Windows.Forms.CheckBox chbx_RemoveBassDD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Export2IndividualSong;
        private CueTextBox txt_FTPPath;
        private System.Windows.Forms.Button btn_FTP;
        private System.Windows.Forms.ComboBox chbx_PreSavedFTP;
        private System.Windows.Forms.ComboBox cbx_Format;
        private System.Windows.Forms.Button btn_SteamDLCFolder;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.Button btn_NextItem;
        private CueTextBox txt_Counter;
        private System.Windows.Forms.Button btn_ExpandSelCrossP;
        private System.Windows.Forms.CheckBox chbx_Autosave;
        private System.Windows.Forms.Button btn_OpeHSAN;
        private System.Windows.Forms.Button btn_OpenCorrespondence;
        private CueTextBox txt_AudioPreviewPath;
        private CueTextBox txt_AudioPath;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private System.Windows.Forms.CheckBox chbx_AutoPlay;
        private System.Windows.Forms.CheckBox chbx_Songs2Cache;
        private CueTextBox txt_Platform;
        private CueTextBox txt_SongsHSANPath;
        private CueTextBox txt_PSARCName;
        private System.Windows.Forms.Button btn_InvertAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_PlayPreview;
        private System.Windows.Forms.Button btn_PlayAudio;
        private System.Windows.Forms.Label lbl_NoRec;
        private System.Windows.Forms.Button btn_GenerateHSAN;
        private System.Windows.Forms.RichTextBox rtxt_Comments;
        private CueTextBox txt_AlbumArtPath;
        private CueTextBox txt_Identifier;
        private System.Windows.Forms.Button btn_DecompressAll;
        private System.Windows.Forms.Button btn_Close;
        private CueTextBox txt_Arrangements;
        private CueTextBox txt_Title;
        private CueTextBox txt_ArtistSort;
        private CueTextBox txt_Artist;
        private CueTextBox txt_AlbumYear;
        private System.Windows.Forms.CheckBox chbx_Removed;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private System.Windows.Forms.Button btn_Save;
        private CueTextBox txt_ID;
        private System.Windows.Forms.Button btn_OpenMainDB;
        private CueTextBox txt_Album;
        internal System.Windows.Forms.DataGridView DataGridView1;
    }
}