using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowershellMonitor.Operations
{
    class UpdateStatus : Operation
    {
        public override KeyValuePair<string, string> doOperation(Runspace rs)
        {
            KeyValuePair<string, string> result = new KeyValuePair<string, string>(getName(), "NotFound!");

            PowerShell ps = openConnection(rs);
            ps.AddScript("Get-Service | select name, status");
            Collection<PSObject> commandResult = ps.Invoke();
            foreach (PSObject o in commandResult)
            {
                if (o.Properties["name"].Value.ToString().Equals("wuauserv")) {
                    result = new KeyValuePair<string, string>(getName(), o.Properties["status"].Value.ToString());
                }
            }
            closeConnection();
            return result;
        }

        public override string getName()
        {
            return "UpdateStatus";
        }
    }
}
