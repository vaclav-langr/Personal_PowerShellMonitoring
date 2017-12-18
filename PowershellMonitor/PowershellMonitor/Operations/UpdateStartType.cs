using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowershellMonitor.Operations
{
    class UpdateStartType : Operation
    {
        public override List<KeyValuePair<string, string>> doOperation(Runspace rs)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            PowerShell ps = openConnection(rs);
            ps.AddScript("Get-Service | select name, starttype");
            Collection<PSObject> commandResult = ps.Invoke();
            foreach (PSObject o in commandResult)
            {
                if (o.Properties["name"].Value.ToString().Equals("wuauserv"))
                {
                    result.Add(new KeyValuePair<string, string>(o.Properties["name"].Value.ToString(), o.Properties["starttype"].Value.ToString()));
                }
            }
            closeConnection();
            return result;
        }

        public override string getName()
        {
            return "UpdateStartType";
        }
    }
}
