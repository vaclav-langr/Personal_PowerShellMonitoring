using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net.NetworkInformation;
using System.Security;

namespace PowershellMonitor
{
    class Client
    {
        private string _clientName;
        private bool _ssl = false;
        private int _port = 5985;
        private string _appName = "/wsman";
        private string _uri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";
        private string _userName;
        private SecureString _password;
        private List<Operation> operations = new List<Operation>();

        public Client(string client, string username, string password)
        {
            ClientName = client;
            Username = username;
            setPassword(password);
        }

        public string ClientName {
            get
            {
                return this._clientName;
            }
            private set
            {
                this._clientName = value;
            }
        }

        public bool SSL
        {
            get
            {
                return this._ssl;
            }
            set
            {
                this._ssl = value;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                if(value > 0)
                {
                    this._port = value;
                }
            }
        }

        public string Username
        {
            get
            {
                return this._userName;
            }
            private set
            {
                this._userName = value;
            }
        }

        private void setPassword(string password)
        {
            this._password = new SecureString();
            for(int i = 0; i < password.Length; i++)
            {
                this._password.AppendChar(password[i]);
            }
        } 

        public bool isOnline()
        {
            Ping p = new Ping();
            PingReply pr = p.Send(this.ClientName);
            return pr.Status == IPStatus.Success;
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> updateInfo()
        {
            Dictionary<string, List<KeyValuePair<string, string>>> data = new Dictionary<string, List<KeyValuePair<string, string>>>();

            PSCredential rc = new PSCredential(this.Username, this._password);
            WSManConnectionInfo ci = new WSManConnectionInfo(this.SSL, this.ClientName, this.Port, this._appName, this._uri, rc);

            foreach (Operation o in operations)
            {
                var runspace = RunspaceFactory.CreateRunspace(ci);
                var result = o.doOperation(runspace);
                data.Add(o.getName(), result);
            }
            return data;
        }

        public void addOperation(Operation o)
        {
            operations.Add(o);
        }
    }
}
