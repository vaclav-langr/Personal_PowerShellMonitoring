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

        Dictionary<Client, BackgroundWorker> clients = new Dictionary<Client, BackgroundWorker>();
        List<BackgroundWorker> workers = new List<BackgroundWorker>();
        public FormMain()
        {
            InitializeComponent();

            //////////////////////////
            panelOverview.Controls.Add(overview);
            overview.Dock = DockStyle.Fill;
            overview.Show();
            //////////////////////////

            foreach (KeyValuePair<Client, BackgroundWorker> kvp in clients)
            {
                kvp.Key.addOperation(new Operations.UpdateStatus());
                kvp.Key.addOperation(new Operations.UpdateStartType());
                kvp.Key.addOperation(new Operations.DownloadSpeed());
                kvp.Key.addOperation(new Operations.UploadSpeed());

                kvp.Value.DoWork += backgroundWorker_DoWork;
                kvp.Value.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
            timer1.Enabled = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Client c = (Client)e.Argument;

            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            result.Add(new KeyValuePair<string, string>("Client", c.ClientName));
            result.Add(new KeyValuePair<string, string>("Online", c.isOnline().ToString()));
            result.AddRange(c.updateInfo());
            e.Result = result;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PowerShellStation pss = new PowerShellStation(false, "", "", -1, -1);
            foreach(KeyValuePair<string, string> kvp in (List<KeyValuePair<string, string>>)e.Result)
            {
                switch(kvp.Key)
                {
                    case "Client":
                        pss.SetStationName(kvp.Value);
                        break;
                    case "Online":
                        pss.SetStatus(kvp.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase) ? true : false);
                        break;
                    case "UpdateStatus":
                        pss.SetServiceStatus(kvp.Value);
                        break;
                    case "UpdateStartType":
                        //missing
                        break;
                    case "UploadSpeed":
                        pss.SetUploadSpeed((int)Math.Round(float.Parse(kvp.Value)));
                        break;
                    case "DownloadSpeed":
                        pss.SetDownloadSpeed((int)Math.Round(float.Parse(kvp.Value)));
                        break;
                }
            }
            overview.AddOrUpdateStation(pss);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Client, BackgroundWorker> kvp in clients)
            {
                kvp.Value.RunWorkerAsync(kvp.Key);
            }
        }
    }
}
