using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PerformanceCounters
    {
        private PerformanceCounter cpuCounter = null;
        private PerformanceCounter ramCounter = null;
        private PerformanceCounter pageCounter = null;

        //
        public Process getProcessFromName(Config config) {
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

        public void InitCounters()
        {
            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use", String.Empty);
                pageCounter = new PerformanceCounter("Paging File", "% Usage", "_Total");
              
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}
