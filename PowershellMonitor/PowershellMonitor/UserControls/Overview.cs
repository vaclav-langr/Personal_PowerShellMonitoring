using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
