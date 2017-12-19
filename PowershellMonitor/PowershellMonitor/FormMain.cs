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
using PowershellMonitor.UserControls;

namespace PowershellMonitor
{
    public partial class FormMain : Form
    {
        Overview overview = new Overview();

        List<Client> clients = new List<Client>();
        public FormMain()
        {
            InitializeComponent();

            //////////////////////////
            panelOverview.Controls.Add(overview);
            overview.Dock = DockStyle.Fill;
            overview.Show();
            //////////////////////////

            foreach (Client c in clients)
            {
                c.addOperation(SingletonFactory.getOperation("UpdateStatus"));
                c.addOperation(SingletonFactory.getOperation("UpdateStartType"));
                c.addOperation(SingletonFactory.getOperation("DownloadSpeed"));
                c.addOperation(SingletonFactory.getOperation("UploadSpeed"));
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
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

        List<bool> listOfStatus = new List<bool> { true, false };
        List<string> listOfServiceStatus = new List<string> { "running", "starting", "stopping", "stopped" };
        List<int> listOfDownSpeed = new List<int> { 10, 12, 15, 18, 20, 22, 24, 26, 28, 30, 34, 38, 40, 50, 55, 68, 78, 83, 90 };
        List<int> listOfUpSpeed = new List<int> { 2, 4, 6, 8, 10, 12, 16, 18, 20, 21, 25, 28, 34, 38, 42, 50 };
        static Random random = new Random();

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool status = listOfStatus[random.Next(listOfStatus.Count)];
            string serviceStatus = listOfServiceStatus[random.Next(listOfServiceStatus.Count)];
            string stationName = "Station " + random.Next(1, 99);
            int downloadSpeed = listOfDownSpeed[random.Next(listOfDownSpeed.Count)];
            int uploadSpeed = listOfUpSpeed[random.Next(listOfUpSpeed.Count)];

            PowerShellStation powerShellStation = new PowerShellStation(status, serviceStatus, stationName, downloadSpeed, uploadSpeed);
            overview.AddStation(powerShellStation);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            overview.ClearStations();
        }
    }
}
