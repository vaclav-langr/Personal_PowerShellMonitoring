using System.Linq;
using System.Windows.Forms;

namespace PowershellMonitor.UserControls
{
    public partial class Overview : UserControl
    {
        // Variables
        private static int initialTop = 25; // Default distance from top for first PowerShellStation row
        private int indentTop = initialTop;  // How far from top is row in Overview (increase with new rows)
        private int indentLeft = 35;  // Indent of PowerShellStation row from left side of Overview

        /// <summary>
        /// Create Overview with is used to show PowerShellStation rows
        /// </summary>
        public Overview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds new PowerShellStation row to Overview, or update when already exists in Controls
        /// </summary>
        /// <param name="station">PowerShellStation visual component (UserControl)</param>
        public void AddOrUpdateStation(PowerShellStation station)
        {
            // Update PowerShellStation if exists in Controls
            foreach(PowerShellStation pss in Controls)
            {
                if(pss.Equals(station))
                {
                    pss.SetStatus(station.Status);  // Update station (machine) status (ON/OFF)
                    pss.SetUploadSpeed(station.UploadSpeed);  // Update upload speed
                    pss.SetDownloadSpeed(station.DownloadSpeed);  // Update download speed
                    pss.SetServiceStatus(station.ServiceStatus);  // Update service status
                    return;
                }
            }

            // Add new PowerShellStation when already not in Controls
            AddStation(station);
        }

        /// <summary>
        /// Add new PowerShellStation to Overview
        /// </summary>
        /// <param name="station">PowerShellStation visual component (UserControl)</param>
        public void AddStation(PowerShellStation station)
        {
            station.Top = indentTop;  // Set indent from TOP right under last row
            station.Left = indentLeft;  // Set default indent from LEFT side of Overview 
            indentTop += station.Height;  // Increase indent TOP by row height
            this.Controls.Add(station);  // Add and show new row in Overview
        }

        /// <summary>
        /// Clear whole Overview controls (PowerShellStation rows)
        /// </summary>
        public void ClearStations()
        {
            this.Controls.Clear();  // Clear all PowerShellStation rows objects
            indentTop = initialTop;  // Set default indent from TOP
        }
    }
}
