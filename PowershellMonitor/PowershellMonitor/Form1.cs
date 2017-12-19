using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Security;
using System.Management.Automation.Runspaces;
using System.Diagnostics;

namespace PowershellMonitor
{
    public partial class Form1 : Form
    {
        List<Client> clients = new List<Client>();
        public Form1()
        {
            InitializeComponent();

            foreach (Client c in clients)
            {
                c.addOperation(new Operations.UpdateStatus());
                c.addOperation(new Operations.UpdateStartType());
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> result;
            foreach (Client c in clients)
            {
                result = c.updateInfo();
                bool has = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (c.ClientName.Equals(row.Cells[0].Value))
                    {
                        has = true;
                        row.Cells["Client"].Value = c.ClientName;
                        row.Cells["Online"].Value = c.isOnline();
                        foreach (KeyValuePair<string, string> kvp in result)
                        {
                            row.Cells[kvp.Key].Value = kvp.Value;
                        }
                    }
                }
                if (!has)
                {
                    if (!dataGridView1.Columns.Contains("Client"))
                    {
                        dataGridView1.Columns.Add("Client", "Client");
                    }
                    if (!dataGridView1.Columns.Contains("Online"))
                    {
                        dataGridView1.Columns.Add("Online", "Online");
                    }
                    foreach (KeyValuePair<string, string> kvp in result)
                    {
                        if(!dataGridView1.Columns.Contains(kvp.Key))
                        {
                            dataGridView1.Columns.Add(kvp.Key, kvp.Key);
                        }
                    }
                    int row = dataGridView1.Rows.Add();

                    dataGridView1.Rows[row].Cells["Client"].Value = c.ClientName;
                    dataGridView1.Rows[row].Cells["Online"].Value = c.isOnline();
                    foreach(KeyValuePair<string, string> kvp in result)
                    {
                        dataGridView1.Rows[row].Cells[kvp.Key].Value = kvp.Value;
                    }
                }
            }
        }
    }
}
