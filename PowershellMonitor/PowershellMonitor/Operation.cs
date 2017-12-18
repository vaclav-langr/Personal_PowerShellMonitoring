using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowershellMonitor
{
    abstract class Operation
    {
        private PowerShell connection = null;

        /// <summary>
        /// Gets data from runspace.
        /// </summary>
        /// <param name="rs">Runspace of operation</param>
        /// <returns>List of keys(name of property) and its values</returns>
        abstract public List<KeyValuePair<string, string>> doOperation(Runspace rs);

        /// <summary>
        /// Name of the operation
        /// </summary>
        /// <returns>Name as string</returns>
        abstract public string getName();

        protected PowerShell openConnection(Runspace rs)
        {
            rs.Open();
            connection = PowerShell.Create();
            connection.Runspace = rs;
            return connection;
        }

        protected void closeConnection()
        {
            if(connection != null)
            {
                connection.Runspace.Close();
                connection = null;
            }
        }
    }
}