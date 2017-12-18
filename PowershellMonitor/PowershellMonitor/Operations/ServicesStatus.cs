using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowershellMonitor.Operations
{
    class ServicesStatus : Operation
    {
        public override List<KeyValuePair<string, string>> doOperation(Runspace rs)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            PowerShell ps = openConnection(rs);
            ps.AddScript("Get-Service | select name, status");
            Collection<PSObject> commandResult = ps.Invoke();
            foreach(PSObject o in commandResult)
            {
                result.Add(new KeyValuePair<string, string>(o.Properties["name"].Value.ToString(), o.Properties["status"].Value.ToString()));
            }
            closeConnection();
            return result;
        }

        public override string getName()
        {
            return "ServiceStatus";
        }
    }
}
