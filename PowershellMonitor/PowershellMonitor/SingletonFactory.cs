using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowershellMonitor
{
    class SingletonFactory
    {
        private static Dictionary<string, Operation> operations = new Dictionary<string, Operation>();

        public static Operation getOperation(string name)
        {
            if(!operations.ContainsKey(name))
            {
                switch(name)
                {
                    case "UpdateStatus":
                        operations.Add(name, new Operations.UpdateStatus());
                        break;
                    case "UpdateStartType":
                        operations.Add(name, new Operations.UpdateStartType());
                        break;
                    case "UploadSpeed":
                        operations.Add(name, new Operations.UploadSpeed());
                        break;
                    case "DownloadSpeed":
                        operations.Add(name, new Operations.DownloadSpeed());
                        break;
                    default:
                        throw new Exception("Unknown operation");
                }
            }
            return operations[name];
        }
    }
}
