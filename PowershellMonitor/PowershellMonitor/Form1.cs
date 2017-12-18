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
        public Form1()
        {
            InitializeComponent();

            Client c = new Client("", "", "");
            c.addOperation(new Operations.UpdateStartType());
            var test = c.isOnline();
        }
    }
}
