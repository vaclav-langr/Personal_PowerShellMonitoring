using System.Linq;
using System.Windows.Forms;

namespace PowershellMonitor.UserControls
{
    public partial class Overview : UserControl
    {
        // Variables
        private int currentTop = 25;
        private int indentLeft = 35;

        public Overview()
        {
            InitializeComponent();
        }

        public void AddOrUpdateStation(PowerShellStation station)
        {
            foreach(PowerShellStation pss in Controls)
            {
                if(pss.Equals(station))
                {
                    pss.SetStatus(station.Status);
                    pss.SetUploadSpeed(station.UploadSpeed);
                    pss.SetDownloadSpeed(station.DownloadSpeed);
                    pss.SetServiceStatus(station.ServiceStatus);
                    return;
                }
            }
            AddStation(station);
        }

        public void AddStation(PowerShellStation station)
        {
            station.Top = currentTop;
            station.Left = indentLeft;
            currentTop += station.Height;
            this.Controls.Add(station);
        }

        public void ClearStations()
        {
            this.Controls.Clear();
            currentTop = 25;
        }
    }
}
