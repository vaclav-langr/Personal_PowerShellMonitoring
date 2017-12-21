﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text.RegularExpressions;

namespace PowershellMonitor.Operations
{
    class DownloadSpeed : Operation
    {
        public override KeyValuePair<string, string> doOperation(Runspace rs)
        {
            KeyValuePair<string, string> result = new KeyValuePair<string, string>();
            Regex pattern = new Regex(@"^.*\spřijaté.*\s\d*,\d*");
            Regex pattern2 = new Regex(@"\d*,\d*");
            Match m, m2;
            String s;
            float maximum = float.MinValue;

            PowerShell ps = openConnection(rs);
            ps.AddScript("Get-Counter -Counter (((Get-Counter -listset 'rozhraní sítě').paths) | sls \"Bajty\")");
            try
            {
                Collection<PSObject> commandResult = ps.Invoke();
                foreach (PSObject o in commandResult)
                {
                    s = o.Properties["Readings"].Value.ToString();
                    s = s.Replace("\n", " ");
                    List<string> list = s.Split(new string[] { "\\\\" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    foreach (string input in list)
                    {
                        m = pattern.Match(input);
                        if (m.Success)
                        {
                            m2 = pattern2.Match(input);
                            if (maximum < float.Parse(m2.Value))
                            {
                                maximum = float.Parse(m2.Value);
                            }
                        }
                    }
                }
                result = new KeyValuePair<string, string>(getName(), (maximum / 1000).ToString());
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                closeConnection();
            }
            return result;
        }

        public override string getName()
        {
            return "DownloadSpeed";
        }
    }
}
