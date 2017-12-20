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
                return _clientName;
            }
            private set
            {
                _clientName = value;
            }
        }

        public bool SSL
        {
            get
            {
                return _ssl;
            }
            set
            {
                _ssl = value;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if(value > 0)
                {
                    _port = value;
                }
            }
        }

        public string Username
        {
            get
            {
                return _userName;
            }
            private set
            {
                _userName = value;
            }
        }

        private void setPassword(string password)
        {
            _password = new SecureString();
            for(int i = 0; i < password.Length; i++)
            {
                _password.AppendChar(password[i]);
            }
        } 

        public bool isOnline()
        {
            Ping p = new Ping();
            PingReply pr = p.Send(ClientName);
            return pr.Status == IPStatus.Success;
        }

        public List<KeyValuePair<string, string>> updateInfo()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            if (!isOnline())
            {
                return data;
            }

            PSCredential rc = new PSCredential(Username, _password);
            WSManConnectionInfo ci = new WSManConnectionInfo(SSL, ClientName, Port, _appName, _uri, rc);

            foreach (Operation o in operations)
            {
                var runspace = RunspaceFactory.CreateRunspace(ci);
                var result = o.doOperation(runspace);
                data.Add(result);
            }
            return data;
        }

        public void addOperation(Operation o)
        {
            operations.Add(o);
        }
    }
}
