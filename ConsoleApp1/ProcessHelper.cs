using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ProcessHelper
    {
        public static Process getProcessFromName(Config config)
        {
            Process p = null;
            Process[] processlist = Process.GetProcessesByName(config.GetProcessName());
            foreach (Process theprocess in processlist)
            {
                //yet again, should use file log here
                Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                p = theprocess;
            }
            return p;
        }

        public static void waitForProcess(Config config)
        {
            //we don't want to wait forever, so let's say we'll wait for ~1 minute
            //should i use a Timer instaed?
            int count = 0;
            Console.WriteLine("The process is not started\n Will wait for the process to start.");

            while (!isProcessAlive(config)) {
                count++;
                Thread.Sleep(50);
                if (count > 3600) throw new Exception("Process has not started in ... minutes. Exiting.");

            }
            //return true;
        }

        public static bool isProcessAlive(Config config ) {
            Process[] processlist = Process.GetProcessesByName(config.GetProcessName());
            if (processlist.Length == 0) return false;
            else return true;
        }

    }
}
