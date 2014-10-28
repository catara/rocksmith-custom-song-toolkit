using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//bcapi
using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    public static partial class DataGridViewExtensions
    {
        /// <summary>
        /// Used to determine if the current cell type is a ComboBoxCell
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsComboBoxCell(this DataGridViewCell sender)
        {
            bool Result = false;
            if (sender.EditType != null)
            {
                if (sender.EditType == typeof(DataGridViewComboBoxEditingControl))
                {
                    Result = true;
                }
            }
            return Result;
        }

        /// <summary>
        /// Resize all columns to see longest item in each column
        /// </summary>
        /// <param name="sender"></param>
        /*public static void ExpandColumns(this DataGridView sender)
        {
            foreach (DataGridViewColumn col in sender.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }*/

        //public static string[] ExportRows(this DataGridView sender)
        //{
        //    return (
        //        from row in sender.Rows.Cast<DataGridViewRow>()
        //        where !((DataGridViewRow)row).IsNewRow
        //        let RowItem = string.Join(",", Array.ConvertAll(((DataGridViewRow)row).Cells.Cast<DataGridViewCell>.ToArray(), (DataGridViewCell c) => ((c.Value == null) ? "" : c.Value.ToString())))
        //        select RowItem).ToArray();
        //}
        public static void ExportRows(this DataGridView sender, string fileName)
        {
            if (sender.RowCount > 0)
            {
                var sb = new StringBuilder();

                var headers = sender.Columns.Cast<DataGridViewColumn>();
                sb.AppendLine(string.Join("\t", headers.Select(column => column.HeaderText)));

                foreach (DataGridViewRow row in sender.Rows)
                {
                    if (!row.IsNewRow == true)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        sb.AppendLine(string.Join("\t", cells.Select(cell => cell.Value)));
                    }
                }
                System.IO.File.WriteAllText(fileName, sb.ToString());
            }
            else
            {
                // decide what to do here
            }
        }
    }
}
