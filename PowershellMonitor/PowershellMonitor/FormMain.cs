using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using PowershellMonitor.UserControls;
using System.Linq;

namespace PowershellMonitor
{
    public partial class FormMain : Form
    {
        Overview overview = new Overview();

        Dictionary<Client, BackgroundWorker> clients = new Dictionary<Client, BackgroundWorker>();
        public FormMain()
        {
            InitializeComponent();

            refreshClientsList();

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

        public void SettingsForm_Closing()
        {
            refreshClientsList();
        }

        private void refreshClientsList()
        {
            List<string> users = Properties.Settings.Default.users.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string[] data;
            Client c;
            foreach (string user in users)
            {
                data = user.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                c = new Client(data[0], data[1], data[2]);

                bool has = false;
                foreach(KeyValuePair<Client, BackgroundWorker> kvp in clients)
                {
                    if(kvp.Key.Equals(c))
                    {
                        has = true;
                    }
                }
                if(!has)
                {
                    clients.Add(c, new BackgroundWorker());
                }
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = false;
            if(e.Control && e.KeyCode == Keys.S)
            {
                SettingsForm sf = new SettingsForm(this);
                sf.Show();
            }
        }
    }
}
