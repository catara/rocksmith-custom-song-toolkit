using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using RocksmithToolkitGUI;
using RocksmithToolkitGUI.DLCManager;
using RocksmithToolkitLib.Extensions; //dds
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.XmlRepository;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Selection : Form
    {
        public bool IgnoreSong { get; set; }
        public bool StopImport { get; set; }
        public Selection(string slct, string B2Txt, string B3Txt)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {/*string link, string Title, bool B1Visi, bool B2Visi, bool B3Visi, string B1Txt,*/
            InitializeComponent();
            //lbl_Link.Text = link;
            // txt_Description.Text = mss;
            this.Text = "Options at " + (B3Txt.IndexOf(" ") > 0 ? B3Txt.Substring(B3Txt.IndexOf(" "), B3Txt.Length - B3Txt.IndexOf(" ") - 1) : "Packing").Trim();
            IgnoreSong = false;
            StopImport = false;
            //ErrorWindow.ActiveForm.Text = Title;
            //btn_B1.Visible = B1Visi;
            if (B2Txt != "") btn_B2.Visible = true;
            else btn_B2.Visible = false;
            if (B3Txt != "") btn_B3.Visible = true;
            else btn_B3.Visible = false;
            //if (B1Txt != "") btn_B1.Text = B1Txt;
            if (B2Txt != "") btn_B2.Text = B2Txt;
            if (B3Txt != "") btn_B3.Text = B3Txt;/*Comments like 'dlcm_AdditionalManipul%'*/
            //slct = "SELECT Type, Profile_Name, DisplayGroup FROM Groups u WHERE Profile_Name=\"" + c("dlcm_Configurations")+ "\" AND DisplayGroup='Pack' ORDER BY DisplayGroup ASC";
            chbx_Additional_Manipulations = GenerateParamsList(chbx_Additional_Manipulations, slct);
            //chbx_Additional_Manipulations = GenerateParamsLists(chbx_Additional_Manipulations, slct);

            //MessageBox.Show("test0");
            //DB_Path = txt_DBFolder;
            //TempPath = txt_TempPath;
            //RocksmithDLCPath = txt_RocksmithDLCPath;
        }
        //public static CheckedListBox GenerateParamsLists(CheckedListBox chbx_Additional_Manipulations, string slct)
        //{
        //    //Get group and norder index
        //    DataSet dv = new DataSet(); dv = SelectFromDB("Groups", slct, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc);
        //     var n = GetNoRec(dv, cnb, cnc);

        //    //SELECT all Params for current Profile

        //    DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Groups", slct, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc);
        //    var noOfRec = GetNoRec(dsz1, cnb, cnc);

        //    //clear PArams
        //    chbx_Additional_Manipulations.DataSource = null;
        //    for (int i = chbx_Additional_Manipulations.Items.Count - 1; i >= 0; --i)
        //        chbx_Additional_Manipulations.Items.RemoveAt(i);

        //    //AddOrderNo Group Index + order no in group
        //    var DisplayGroup = "";
        //    for (int j = 0; j < noOfRec; j++)
        //    {
        //        DisplayGroup = dsz1.Tables[0].Rows[j][3].ToString();
        //        var Comments = dsz1.Tables[0].Rows[j][4].ToString();
        //        if (Comments.Length == 1) Comments = "0" + Comments;
        //        dsz1.Tables[0].Rows[j][5] = GiveOrder(dv, n, DisplayGroup) + Comments;
        //    }

        //    //OrderList of Params based on Group order and then Item in the group order
        //    var tmp = "";
        //    for (int l = 0; l < noOfRec; l++)
        //        for (int m = l + 1; m < noOfRec; m++)
        //        {
        //            if (dsz1.Tables[0].Rows[l][1].ToString() == "dlcm_AdditionalManipul89" || dsz1.Tables[0].Rows[m][1].ToString() == "dlcm_AdditionalManipul89")
        //                ;
        //            if (dsz1.Tables[0].Rows[m][5].ToString().ToInt32() < dsz1.Tables[0].Rows[l][5].ToString().ToInt32())
        //            {
        //                tmp = dsz1.Tables[0].Rows[l][0].ToString(); dsz1.Tables[0].Rows[l][0] = dsz1.Tables[0].Rows[m][0].ToString(); dsz1.Tables[0].Rows[m][0] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][1].ToString(); dsz1.Tables[0].Rows[l][1] = dsz1.Tables[0].Rows[m][1].ToString(); dsz1.Tables[0].Rows[m][1] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][2].ToString(); dsz1.Tables[0].Rows[l][2] = dsz1.Tables[0].Rows[m][2].ToString(); dsz1.Tables[0].Rows[m][2] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][3].ToString(); dsz1.Tables[0].Rows[l][3] = dsz1.Tables[0].Rows[m][3].ToString(); dsz1.Tables[0].Rows[m][3] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][4].ToString(); dsz1.Tables[0].Rows[l][4] = dsz1.Tables[0].Rows[m][4].ToString(); dsz1.Tables[0].Rows[m][4] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][5].ToString(); dsz1.Tables[0].Rows[l][5] = dsz1.Tables[0].Rows[m][5].ToString(); dsz1.Tables[0].Rows[m][5] = tmp;
        //                tmp = dsz1.Tables[0].Rows[l][6].ToString(); dsz1.Tables[0].Rows[l][6] = dsz1.Tables[0].Rows[m][6].ToString(); dsz1.Tables[0].Rows[m][6] = tmp;
        //            }
        //        }

        //    //add items
        //    DisplayGroup = "";
        //    var z = 0;
        //    for (int k = 0; k < noOfRec; k++)
        //    {
        //        var Type = dsz1.Tables[0].Rows[k][0].ToString();
        //        var Comments = dsz1.Tables[0].Rows[k][1].ToString();

        //        var DisplayName = dsz1.Tables[0].Rows[k][2].ToString();
        //        if (DisplayName == "") continue;
        //        if (DisplayGroup != dsz1.Tables[0].Rows[k][3].ToString())
        //        {
        //            chbx_Additional_Manipulations.Items.Add(dsz1.Tables[0].Rows[k][3].ToString());
        //            chbx_Additional_Manipulations.SetItemCheckState(z, CheckState.Indeterminate);
        //            z++;
        //        }
        //        DisplayGroup = dsz1.Tables[0].Rows[k][3].ToString() == "" ? DisplayGroup : dsz1.Tables[0].Rows[k][3].ToString();
        //        var DisplayPosition = dsz1.Tables[0].Rows[k][4].ToString();
        //        var Groups = dsz1.Tables[0].Rows[k][6].ToString();
        //        chbx_Additional_Manipulations.Items.Add(("Yes" == ConfigRepository.Instance()["dlcm_Debug"] ? DisplayPosition + ". " : "")
        //            + DisplayName + " {" + Comments.Replace("dlcm_AdditionalManipul", "") + "}");
        //        chbx_Additional_Manipulations.SetItemCheckState(z, Groups.ToLower() == "no" ? CheckState.Unchecked : CheckState.Checked);
        //        z++;
        //    }
        //    return chbx_Additional_Manipulations;
        //}

        private void InitializeComponent()
        {
            helpProvider1 = new HelpProvider();
            splitContainer1 = new SplitContainer();
            chbx_Additional_Manipulations = new CheckedListBox();
            btn_FilterParams = new Button();
            txt_FilterParams = new RichTextBox();
            lbl_Link = new LinkLabel();
            btn_B2 = new Button();
            btn_B3 = new Button();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(chbx_Additional_Manipulations);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btn_FilterParams);
            splitContainer1.Panel2.Controls.Add(txt_FilterParams);
            splitContainer1.Panel2.Controls.Add(lbl_Link);
            splitContainer1.Panel2.Controls.Add(btn_B2);
            splitContainer1.Panel2.Controls.Add(btn_B3);
            splitContainer1.Size = new Size(1115, 829);
            splitContainer1.SplitterDistance = 682;
            splitContainer1.TabIndex = 336;
            // 
            // chbx_Additional_Manipulations
            // 
            chbx_Additional_Manipulations.CheckOnClick = true;
            chbx_Additional_Manipulations.FormattingEnabled = true;
            chbx_Additional_Manipulations.HorizontalScrollbar = true;
            chbx_Additional_Manipulations.Items.AddRange(new object[] { "00. @Pack Add Increment to all songs Title", "01. @Pack Add Increment to all songs Title per artist", "02. @Pack Make all DLC IDs unique (&save)", "03. @Pack Remove DD", "04. Backup DB during Startup", "05. @Pack Remove DD only for Bass Guitar", "06. When converting Audio use local folder structure", "07. @Pack skip Broken songs", "08. @Pack Name to cross-platform Compatible Filenames", "09. @Pack Add Preview if missing 00:30 for 30sec (&save)", "10. @Pack Make all DLC IDs unique", "11. <@PackAdd DD (5 Levels)>", "12. Add DD (5 Levels) when missing", "13. Import all Duplicates as Alternates", "14. Import any Custom as Alternate if an Original exists", "15. Move the Imported files to temp/0_old", "16. Import with Artist/Title same as Artist/Title/Album Sort", "17. Repack with Artist/Title same as Artist/Title/Album Sort", "18. <Import without The/Die at the beginning of Artist/Title Sort>", "19. <Pack without The/Die at the beginning of Artist/Title Sort>", "20. Import with the The/Die at the end of Title Sort", "21. Pack with The/Die at the end of Title/Title Sort", "22. Import with the The/Die only at the end of Artist/Album/xx Sort", "23. Pack with The/Die only at the end of Artist/Album/xx Sort", "24. @Import Use translation tables for naming standardization", "25. If Original don't add QAs (NOs;DLC/ORIG;etc.)", "26. When packing Add 5 Levels of DD only to Guitar tracks", "27. Convert and Transfer/FTP", "28. If Original don't add QAs (NOs;DLC/ORIG;etc.) except for File Names", "29. When NOT importing a Duplicate Move it to _duplicate", "30. When NOT importing a broken song Move it to _broken", "31. When removing DD use internal logic not DDC", "32. When importing alternates add newer/older instead of alt.0author", "33. Forcibly Update Import location of all DB fields", "34. @Import Add Preview if missing (lenght> as per config)", "35. Remove illegal characters from Songs Metadata", "36. Keep the Uncompressed Songs superorganized", "37. Import other formats but PC", "38. Import only the unpacked songs already in the \"0/\" Temp folder", "39. Encrypt PS3 Retails songs, with External tool", "40. Delete ORIG HSAN/OGG when Packing Retails songs", "41. Try to get Track No. &Details from Spotify (&yb links)", "42. Save Log After Import (Imported Folder)", "43. @Import Set the DLCID autom", "44. @Pack Set the DLCID autom", "45. <Convert Originals>", "46. Duplicate Mangement, Title added info is inbetween separators: []", "47. Add New Toolkit v. and RePackedByAuthor", "48. @Import Remove Multitrack/Live/Acoustic info from Title", "49. @Pack Also Copy/FTP", "50. @Import Manually assess duplicates at the end", "51. @Import&Unpack Overwrite the XML", "52. @Pack keep Bass DD if indicated so", "53. @Pack keep All DD if indicated so", "54. @Pack consider All songs as beta (place them top of the list)", "55. Gen Preview if Preview=Audio or Preview is longer than config (default 30s)", "56. Duplicate manag ignores Multitracks", "57. Don't save Author when generic (i.e. Custom Song Creator)", "58. @Pack try to get Track No again (&don't save)", "59. @Pack try to get Track No again (&save)", "60. @Rebuild don't overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)", "61. @Rebuild don't overwrite Standard Song Info (Cover,Year)", "62. <@Pack duplicate singleTracks L->R / R->L>", "63. @Pack Remove Remote File if GameData has been read", "64. @Pack ONLY Copy/FTP the Last Packed song", "65. @Pack ONLY Copy/FTP the Initially Imported song", "66. Duplicate manag. ignores Live Songs", "67. Import duplicates (hash)", "68. Delete obvious duplicates (hash) during dupli assesment", "69. Compress AudioFiles to 128VBR @Pack/Import if bigger than 136k", "70. @Repack pack Preview (bugfix)", "71. <@Import/Repack check if Original flag is in the Official list and correct>", "72. Import other formats but PC, as standalone", "73. Add Track Info&Comments beginning of Lyrics", "74. Add Track start into Vocals", "75. Copy to \\0\\0_Old (Overwrites 15 Move to old)", "76. Include Tones/arangements Db changes", "77. After Import open MainDB", "78. @Import Fix Audio Issues at end", "79. @Import Manually Asses All Suspicious Duplicates", "80. Duplicate manag. ignores Acoustic Songs", "81. Any Delete (non psarc) goes to RecycleBin", "82. Show warning that It will connect to Spotify", "83. Import All suspicious Duplicates as Duplicates (Ignore)", "84. When checking Songs validate wem bitrate (10% wem conversion raises the bitrate)", "85. Apply standard naming to all duplicates", "86. Keep XML Manipulations", "87. Use Latest Spotify API (Web)", "88. Gen Preview if Preview is shorter than config (default 10s)", "89. @Mass pack split into xxx (param in xml) songs", "90. When adding times into vocals(74) add only in seconds", "91. Add group to Filename", "92. Package for a HAN enabled PS3", "93. If packaging for a HAN Enabled PS3 then also copy Retail(RS2012) Songs", "94. After lyrics manipulation Open them in Notepad", "95. @Export create Package (in@0_temp)", "96. @Export create Tabs", "97. Pack only never packed Songs (Overwrites 98)", "98. Pack only never packed Songs for the target Platform", "99. <If Group pack ignore songs that are also in other Groups>", "x100. <Pack anew instead of converting (e.g. Pack Orig file)>", "x101. After packing check song", "<102. >", "<103. >", "<104. >", "<105. >", "<106. >", "<107. >", "<108. >", "<109. >", "<110. >", "<111. >", "<112. >", "<113. >", "<114. >", "<115. >", "<116. >", "<117. >", "<118. >", "<119. >", "<120. >", "<121. >", "<122. >", "<123. >", "<124. >", "<125. >", "<126. >", "<127. >", "<128. >", "<129. >", "<130. >", "<131. >", "<132. >", "<133. >", "<134. >", "<135. >", "<136. >", "<137. >", "<138. >", "<139. >", "<140. >", "<141. >", "<142. >", "<143. >", "<144. >", "<145. >", "<146. >", "<147. >", "<148. >", "<149. >", "<150. >", "<151. >", "<152. >", "<153. >", "<154. >", "<155. >", "<156. >", "<157. >", "<158. >", "<159. >", "<160. >", "<161. >", "<162. >", "<163. >", "<164. >", "<165. >", "<166. >", "<167. >", "<168. >", "<169. >", "<170. >", "<171. >", "<172. >", "<173. >", "<174. >", "<175. >", "<176. >", "<177. >", "<178. >", "<179. >", "<180. >", "<181. >", "<182. >", "<183. >", "<184. >", "<185. >", "<186. >", "<187. >", "<188. >", "<189. >", "<190. >", "<191. >", "<192. >", "<193. >", "<194. >", "<195. >", "<196. >", "<197. >", "<198. >", "<199. >", "<200. >" });
            chbx_Additional_Manipulations.Location = new Point(12, 49);
            chbx_Additional_Manipulations.Margin = new Padding(0);
            chbx_Additional_Manipulations.Name = "chbx_Additional_Manipulations";
            chbx_Additional_Manipulations.Size = new Size(1103, 616);
            chbx_Additional_Manipulations.TabIndex = 432;
            // 
            // btn_FilterParams
            // 
            btn_FilterParams.Location = new Point(456, 3);
            btn_FilterParams.Margin = new Padding(4, 5, 4, 5);
            btn_FilterParams.Name = "btn_FilterParams";
            btn_FilterParams.Size = new Size(204, 48);
            btn_FilterParams.TabIndex = 433;
            btn_FilterParams.Text = "Filter Params";
            btn_FilterParams.UseVisualStyleBackColor = true;
            btn_FilterParams.Click += btn_FilterParams_Click;
            // 
            // txt_FilterParams
            // 
            txt_FilterParams.Location = new Point(12, 3);
            txt_FilterParams.Margin = new Padding(4, 3, 4, 3);
            txt_FilterParams.Multiline = false;
            txt_FilterParams.Name = "txt_FilterParams";
            txt_FilterParams.ScrollBars = RichTextBoxScrollBars.None;
            txt_FilterParams.ShortcutsEnabled = false;
            txt_FilterParams.Size = new Size(436, 48);
            txt_FilterParams.TabIndex = 434;
            txt_FilterParams.Text = "";
            // 
            // lbl_Link
            // 
            lbl_Link.AutoSize = true;
            lbl_Link.Location = new Point(667, 19);
            lbl_Link.Name = "lbl_Link";
            lbl_Link.Size = new Size(60, 15);
            lbl_Link.TabIndex = 7;
            lbl_Link.TabStop = true;
            lbl_Link.Text = "linkLabel1";
            lbl_Link.Visible = false;
            lbl_Link.LinkClicked += Lbl_Link_LinkClicked;
            // 
            // btn_B2
            // 
            btn_B2.Dock = DockStyle.Bottom;
            btn_B2.Location = new Point(0, 54);
            btn_B2.Name = "btn_B2";
            btn_B2.Size = new Size(1115, 59);
            btn_B2.TabIndex = 6;
            btn_B2.Text = "OK";
            btn_B2.UseVisualStyleBackColor = true;
            btn_B2.Visible = false;
            btn_B2.Click += btn_Close_Click;
            // 
            // btn_B3
            // 
            btn_B3.Dock = DockStyle.Bottom;
            btn_B3.Location = new Point(0, 113);
            btn_B3.Name = "btn_B3";
            btn_B3.Size = new Size(1115, 30);
            btn_B3.TabIndex = 5;
            btn_B3.Text = "Stop Import";
            btn_B3.UseVisualStyleBackColor = true;
            btn_B3.Click += btn_OK_Click;
            // 
            // Selection
            // 
            AutoSize = true;
            ClientSize = new Size(1115, 829);
            Controls.Add(splitContainer1);
            Name = "Selection";
            Load += Selection_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            saveOptions(chbx_Additional_Manipulations);
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            StopImport = true;
            this.Hide();
        }

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            IgnoreSong = true;
            this.Hide();
        }

        private void lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(lbl_Link.Text as string);
        }

        private void btn_FilterParams_Click(object sender, EventArgs e)
        {
            int i;
            if (chbx_Additional_Manipulations.SelectedIndex + 1 >= chbx_Additional_Manipulations.Items.Count) i = chbx_Additional_Manipulations.SelectedIndex + 1;
            else i = 0;
            //FiltrParams = true;FiltrParams || 

            var index = 0;
            //int index = chbx_Additional_Manipulations.Items.IndexOf(txt_FilterParams.Text);
            for (int j = i; j < chbx_Additional_Manipulations.Items.Count; j++)
                if ((chbx_Additional_Manipulations.Items[j].ToString().ToLower()).IndexOf(txt_FilterParams.Text.ToLower()) >= 0)
                {
                    index = j;
                    break;
                }
            if (index >= 0) chbx_Additional_Manipulations.SetSelected(index, true);
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            if (c("dlcm_AdditionalManipul116") != "Yes" || btn_B2.Text.Contains("Export"))
            {
                this.Hide(); return;
            }
        }
    }
}
