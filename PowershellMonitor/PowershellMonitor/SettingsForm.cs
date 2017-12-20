using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowershellMonitor
{
    public partial class SettingsForm : Form
    {
        private FormMain fm;
        public SettingsForm(FormMain parent)
        {
            InitializeComponent();
            fm = parent;
            List<string> users = Properties.Settings.Default.users.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(string user in users)
            {
                string[] data = user.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                dataGridView1.Rows.Add(data);
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView1.CellValidating -= dataGridView1_CellValidating;
            StringBuilder sb = new StringBuilder("");
            for (var j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                var row = dataGridView1.Rows[j];
                for (var i = 0; i < row.Cells.Count; i++)
                {
                    sb.Append(row.Cells[i].Value);
                    sb.Append(":");
                }
                sb.Length = sb.Length - 1;
                sb.Append("|");
            }
            Properties.Settings.Default.users = sb.ToString();
            Properties.Settings.Default.Save();
            fm.SettingsForm_Closing();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if("".Equals(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue) || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue == null)
            {
                e.Cancel = true;
            }
        }
    }
}
