
using System.Collections.Specialized;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private string processName;
        private int  interval; //how oftes should we coolect the data


        static void Main(string[] args)
        {
            //should use some sort of file log
            try
            {
                Config config = new Config();
            }
            catch (Exception e){
                Console.WriteLine(e);
                Environment.Exit(0);
            }



            string processName = "Lightshot";
            Process p = null;
            System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcessesByName(processName);
            foreach (System.Diagnostics.Process theprocess in processlist)
            {
                Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                p = theprocess;
            }


           // PerformanceCounterCategory[] Array = PerformanceCounterCategory.GetCategories();
            //for (int i = 0; i < Array.Length; i++)
            //{
            //    Console.Out.WriteLine("{0}. Name={1} Help={2}", i, Array[i].CategoryName, Array[i].CategoryHelp);
            //}

            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", p.ProcessName);
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", p.ProcessName);
            //PerformanceCounter newCounter = new PerformanceCounter("AverageCounter64SampleCategory", "AverageCounter64Sample", p.ProcessName);


            //create PCs
            Dictionary<string, List<double>> perfCounters = new Dictionary<string, List<double>>();
            //perfCounters.Add(ramCounter)
            perfCounters.Add("Working Set", new List<double>());
            perfCounters.Add("% Processor Time", new List<double>());
            //perfCounters.Add("Working Set", new List<double>());

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                double ram = ramCounter.NextValue();
                double cpu = cpuCounter.NextValue();
                Console.WriteLine("RAM: " + (ram / 1024 / 1024) + " MB; CPU: " + (cpu) + " %");
                //perfCounters.TryGetValue("Working Set", out value);

            }


            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt", true))
            {
                file.WriteLine("Fourth line");
            }

            // Ожидание нажатия клавиши Enter перед завершением работы
            string stuff = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(stuff);

            Console.ReadLine();

        }

        String setProperties(){
            string sAttr = ConfigurationManager.AppSettings.Get("Key0");
            //var appSettings = ConfigurationManager.AppSettings;

            return ConfigurationManager.AppSettings.Get("Key0");
        }
    }
}
