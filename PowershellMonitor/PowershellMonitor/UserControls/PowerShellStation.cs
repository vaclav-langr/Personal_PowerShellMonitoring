using System;
using System.Drawing;
using System.Windows.Forms;

namespace PowershellMonitor.UserControls
{
    public partial class PowerShellStation : UserControl, IEquatable<PowerShellStation>
    {
        // Station information
        public bool Status { get; private set; }
        public string ServiceStatus { get; private set; }
        public string StationName { get; private set; }
        public int DownloadSpeed { get; private set; }
        public int UploadSpeed { get; private set; }

        // Status icons
        private Bitmap statusOn = Properties.Resources.ic_fiber_manual_on_white_48dp_2x;
        private Bitmap statusOff = Properties.Resources.ic_fiber_manual_off_white_48dp_2x;

        // Service status icons
        private Bitmap serviceRunning = Properties.Resources.ic_running_white_48dp_2x;
        private Bitmap serviceStarting = Properties.Resources.ic_up_white_48dp_2x;
        private Bitmap serviceStopping = Properties.Resources.ic_down_white_48dp_2x;
        private Bitmap serviceeStopped = Properties.Resources.ic_pause_white_48dp_2x;

        // Components
        private PictureBox imageStatus;
        private PictureBox imageServiceStatus;
        private Label textStationName;
        private Label textDownloadSpeed;
        private Label textUploadSpeed;

        // Variables
        private const string defaultSpeedUnit = "kB/s";

        /// <summary>
        /// Create new PowerShellStation visual component
        /// </summary>
        /// <param name="status">Indicate whether station is running (ON/OFF)</param>
        /// <param name="serviceStatus"></param>
        /// <param name="stationName">Custom name</param>
        /// <param name="downloadSpeed">Download speed in MB/s</param>
        /// <param name="uploadSpeed">Upload speed in MS/s</param>
        public PowerShellStation(bool status, string serviceStatus, string stationName, int downloadSpeed, int uploadSpeed)
        {
            // Initialize visual components
            InitializeComponent();
            imageStatus = pictureBoxStatus;
            imageServiceStatus = pictureBoxServiceStatus;
            textStationName = labelStationName;
            textDownloadSpeed = labelSpeedDown;
            textUploadSpeed = labelSpeedUp;

            // Set variables and load data into visual components
            SetStatus(status);
            SetServiceStatus(serviceStatus);
            SetStationName(stationName);
            SetDownloadSpeed(downloadSpeed);
            SetUploadSpeed(uploadSpeed);
        }

        /// <summary>
        /// Set and show station status ON/OFF
        /// </summary>
        /// <param name="status">Indicate whether station is running (ON/OFF)</param>
        public void SetStatus(bool status)
        {
            this.Status = status;
            imageStatus.Image = status ? statusOn : statusOff;
            SetComponentsForStatus(status);
        }

        /// <summary>
        /// Set and show service status
        /// </summary>
        /// <param name="status">Service can be running, starting, stopping, stopped</param>
        public void SetServiceStatus(string serviceStatus)
        {
            this.ServiceStatus = serviceStatus;
            switch (serviceStatus.ToLower())
            {
                case "running": imageServiceStatus.Image = serviceRunning; break;
                case "starting": imageServiceStatus.Image = serviceStarting; break;
                case "stopping": imageServiceStatus.Image = serviceStopping; break;
                case "stopped": imageServiceStatus.Image = serviceeStopped; break;
            }
        }

        /// <summary>
        /// Set and show station name
        /// </summary>
        /// <param name="stationName">Custom station name</param>
        public void SetStationName(string stationName)
        {
            this.StationName = stationName;
            textStationName.Text = stationName;
        }

        /// <summary>
        /// Set and show download speed in MB/s
        /// </summary>
        /// <param name="downloadSpeed">Download speed in MB/s</param>
        public void SetDownloadSpeed(int downloadSpeed)
        {
            this.DownloadSpeed = downloadSpeed;
            textDownloadSpeed.Text = downloadSpeed + " " + defaultSpeedUnit;
        }

        /// <summary>
        /// Set and show upload speed in MB/s
        /// </summary>
        /// <param name="uploadSpeed">Upload speed in MB/s</param>
        public void SetUploadSpeed(int uploadSpeed)
        {
            this.UploadSpeed = uploadSpeed;
            textUploadSpeed.Text = uploadSpeed + " " + defaultSpeedUnit;
        }

        /// <summary>
        /// Set visible components not visible when status OFF, and visible when ON
        /// </summary>
        /// <param name="status">Indicate whether station is running (ON/OFF)</param>
        private void SetComponentsForStatus(bool status)
        {
            // ON
            if (status)
            {
                pictureBoxServiceStatus.Visible = true;
                pictureBoxSpeedDown.Visible = true;
                pictureBoxSpeedUp.Visible = true;
                textDownloadSpeed.Visible = true;
                textUploadSpeed.Visible = true;
            }

            // OFF
            else
            {
                pictureBoxServiceStatus.Visible = false;
                pictureBoxSpeedDown.Visible = false;
                pictureBoxSpeedUp.Visible = false;
                textDownloadSpeed.Visible = false;
                textUploadSpeed.Visible = false;
            }
        }

        /// <summary>
        /// Compare THIS object to second one and return true when equals
        /// </summary>
        /// <param name="other">Second PowerShellStation object to compare with called object</param>
        /// <returns>True when PowerShellStation is equal</returns>
        public bool Equals(PowerShellStation other)
        {
            return StationName.Equals(other.StationName);
        }
    }
}
