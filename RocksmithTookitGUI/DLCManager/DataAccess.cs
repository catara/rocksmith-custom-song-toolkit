using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//bcapi
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using RocksmithToolkitGUI.DLCManager;

namespace RocksmithToolkitGUI.DLCManager
{
    class DataAccess
    {
        public DataAccess() { }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs, ref BindingSource bsPositions, ref BindingSource bsBadges)
        {
            DataSet ds = new DataSet();
            var DB_Path = "../../../../tmp";
            //DB_Path = "tmp\\Demo.mdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT PersonName, ContactPosition FROM People;", cn);
                da.Fill(ds, "Names");
                da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                da.Fill(ds, "PositionType");
                da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                da.Fill(ds, "Badge");
            }

            DataGridViewTextBoxColumn PeopleNamesColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PersonName",
                HeaderText = "Names"
            };

            bsPositions.DataSource = ds.Tables["PositionType"];
            bsBadges.DataSource = ds.Tables["Badge"];

            DataGridViewComboBoxColumn ContactPositionColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "ContactPosition",
                DataSource = bsPositions,
                DisplayMember = "ContactPosition",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                Name = "ContactsColumn",
                HeaderText = "Position",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueMember = "ContactPosition"
            };

            DataGridViewComboBoxColumn BadgeColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Badge",
                DataSource = bsBadges,
                DisplayMember = "Badge",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                Name = "BadgeColumn",
                HeaderText = "Badge",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueMember = "Badge"
            };

            DataGridView.AutoGenerateColumns = false;

            DataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                PeopleNamesColumn,
                ContactPositionColumn,
                BadgeColumn
            }
            );

            ds.Tables["Names"].AcceptChanges();

            bs.DataSource = ds.Tables["Names"];
            DataGridView.DataSource = bs;
            //DataGridView.ExpandColumns();
        }
    }
}
