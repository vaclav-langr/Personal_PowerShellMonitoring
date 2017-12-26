using System;
using System.Drawing;
using System.Windows.Forms;

namespace PowershellMonitor.UserControls
{
    public partial class PowerShellStation : UserControl, IEquatable<PowerShellStation>
    {
        // Station information
        public bool Status { get; private set; } = true;
        public string ServiceStatus { get; private set; } = "";
        public string ServiceStartMode { get; private set; } = "";
        public string StationName { get; private set; } = "";
        public int DownloadSpeed { get; private set; } = -1;
        public int UploadSpeed { get; private set; } = -1;

        // Status icons
        private Bitmap statusOn = Properties.Resources.ic_fiber_manual_on_white_48dp_2x;
        private Bitmap statusOff = Properties.Resources.ic_fiber_manual_off_white_48dp_2x;
        private Bitmap statusWarrning = Properties.Resources.ic_fiber_manual_on_yellow_48dp_2x;

        // Service status icons
        private Bitmap serviceRunning = Properties.Resources.ic_running_white_48dp_2x;
        private Bitmap serviceStarting = Properties.Resources.ic_up_white_48dp_2x;
        private Bitmap serviceStopping = Properties.Resources.ic_down_white_48dp_2x;
        private Bitmap serviceStopped = Properties.Resources.ic_pause_white_48dp_2x;

        // PowerShell console icon
        private Bitmap powerShellConsoleDefault = Properties.Resources.powershell;
        private Bitmap powerShellConsoleMouseOver = Properties.Resources.powershellMouseOver;

        // Components
        private PictureBox imageStatus;
        private PictureBox imageServiceStatus;
        private PictureBox imagePowerShellConsole;
        private Label textStationName;
        private Label textDownloadSpeed;
        private Label textUploadSpeed;
        private ToolTip toolTipServiceStartMode;

        // Notification
        private NotifyIcon notification;

        // Variables
        private const string defaultSpeedUnit = "kB/s";
        private const string defaultToolTipServiceStartModeText = "ServiceStartMode";
        private const string serviceRunningTitle = "Service is runnig";

        /// <summary>
        /// Sets Click event handler.
        /// </summary>
        /// <param name="stationClick">Custom action</param>
        internal void SetClick(EventHandler stationClick)
        {
            pictureBoxPowerShell.Click += stationClick;
        }

        /// <summary>
        /// Create new PowerShellStation visual component without data input
        /// </summary>
        public PowerShellStation(NotifyIcon notification)
        {
            // Initialize visual components
            InitializeComponent();
            imageStatus = pictureBoxStatus;
            imageServiceStatus = pictureBoxServiceStatus;
            imagePowerShellConsole = pictureBoxPowerShell;
            textStationName = labelStationName;
            textDownloadSpeed = labelSpeedDown;
            textUploadSpeed = labelSpeedUp;
            this.notification = notification;
        }

        /// <summary>
        /// Create new PowerShellStation visual component
        /// </summary>
        /// <param name="status">Indicate whether station is running (ON/OFF)</param>
        /// <param name="serviceStatus">Service can be running, starting, stopping, stopped</param>
        /// <param name="serviceStartMode">Start mode can be automatic, boot, disabled, manual, system</param>
        /// <param name="stationName">IP address or custom name</param>
        /// <param name="downloadSpeed">Download speed in MB/s</param>
        /// <param name="uploadSpeed">Upload speed in MS/s</param>
        public PowerShellStation(NotifyIcon notification, bool status, string serviceStatus, string serviceStartMode, string stationName, int downloadSpeed, int uploadSpeed) : this(notification)
        {
            // Set variables and load data into visual components
            SetStatus(status);
            SetServiceStartMode(serviceStartMode);
            SetStationName(stationName);
            SetDownloadSpeed(downloadSpeed);
            SetUploadSpeed(uploadSpeed);
            SetToolTipServiceStartMode();
            SetServiceStatus(serviceStatus);
        }

        /// <summary>
        /// Set and show station status ON/OFF
        /// </summary>
        /// <param name="status">Indicate whether station is running (ON/OFF)</param>
        public void SetStatus(bool status)
        {
            if (this.Status == status) return;
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
            // Convert serviceStatus to lowerCase
            serviceStatus = serviceStatus.ToLower();

            // Check if not the same
            if (this.ServiceStartMode.Equals(serviceStatus) || !this.Status) return;

            // If service is starting
            if (serviceStatus.Equals("running") || serviceStatus.Equals("starting"))
            {
                // But was stopped before
                if (this.ServiceStatus.Equals("") || this.ServiceStatus.Equals("stopped") || this.ServiceStatus.Equals("stopping"))
                {
                    // Show warrning color
                    imageStatus.Image = statusWarrning;

                    // Do notification
                    notification.ShowBalloonTip(
                        3000, 
                        serviceRunningTitle, 
                        "Service running at" + " " + this.StationName + ". " + "StartMode:" + " " + this.ServiceStartMode, 
                        ToolTipIcon.Warning);  
                }
            }

            // Change image of service status
            this.ServiceStatus = serviceStatus;
            switch (serviceStatus)
            {
                case "running": imageServiceStatus.Image = serviceRunning; break;
                case "starting": imageServiceStatus.Image = serviceStarting; break;
                case "stopping": imageServiceStatus.Image = serviceStopping; break;
                case "stopped": imageServiceStatus.Image = serviceStopped; break;
            }
        }

        /// <summary>
        /// Set and show service start mode
        /// https://msdn.microsoft.com/cs-cz/library/system.serviceprocess.servicestartmode(v=vs.110).aspx
        /// </summary>
        /// <param name="serviceStartMode">Start mode can be automatic, boot, disabled, manual, system</param>
        public void SetServiceStartMode(string serviceStartMode)
        {
            if (this.ServiceStartMode.Equals(serviceStartMode)) return;
            this.ServiceStartMode = serviceStartMode;
        }

        /// <summary>
        /// Set and show station name
        /// </summary>
        /// <param name="stationName">IP address or custom name</param>
        public void SetStationName(string stationName)
        {
            if (this.StationName.Equals(stationName)) return;
            this.StationName = stationName;
            textStationName.Text = stationName;
        }

        /// <summary>
        /// Set and show download speed in MB/s
        /// </summary>
        /// <param name="downloadSpeed">Download speed in MB/s</param>
        public void SetDownloadSpeed(int downloadSpeed)
        {
            if (this.DownloadSpeed == downloadSpeed) return;
            this.DownloadSpeed = downloadSpeed;
            textDownloadSpeed.Text = downloadSpeed + " " + defaultSpeedUnit;
        }

        /// <summary>
        /// Set and show upload speed in MB/s
        /// </summary>
        /// <param name="uploadSpeed">Upload speed in MB/s</param>
        public void SetUploadSpeed(int uploadSpeed)
        {
            if (this.UploadSpeed == uploadSpeed) return;
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
                pictureBoxPowerShell.Visible = true;
                textDownloadSpeed.Visible = true;
                textUploadSpeed.Visible = true;
            }

            // OFF
            else
            {
                pictureBoxServiceStatus.Visible = false;
                pictureBoxSpeedDown.Visible = false;
                pictureBoxSpeedUp.Visible = false;
                pictureBoxPowerShell.Visible = false;
                textDownloadSpeed.Visible = false;
                textUploadSpeed.Visible = false;
            }
        }

        /// <summary>
        /// Set ToolTip for serviceStartMode at serviceStatus image
        /// https://stackoverflow.com/questions/1339524/c-how-do-i-add-a-tooltip-to-a-control
        /// </summary>
        private void SetToolTipServiceStartMode()
        {
            // Create the ToolTip and associate with the Form container.
            toolTipServiceStartMode = new ToolTip
            {
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,

                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for ServiceStatus image
            toolTipServiceStartMode.SetToolTip(this.imageServiceStatus, defaultToolTipServiceStartModeText + ": " + this.ServiceStartMode);
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

        /// <summary>
        /// Change picture of PowerShellConsole button when MouseEnter
        /// </summary>
        private void PictureBoxPowerShell_MouseEnter(object sender, EventArgs e)
        {
            imagePowerShellConsole.Image = powerShellConsoleMouseOver;
        }

        /// <summary>
        /// Change picture of PowerShellConsole button when MouseLeave
        /// </summary>
        private void PictureBoxPowerShell_MouseLeave(object sender, EventArgs e)
        {
            imagePowerShellConsole.Image = powerShellConsoleDefault;
        }
    }
}
