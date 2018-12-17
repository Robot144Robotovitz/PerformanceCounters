
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
       //TODO: add function to handle exceptions and messages


        static void Main(string[] args)

            
        {
            Config config = null;
           
            try
            {
                config = new Config();
            }
            catch (Exception e){
                //should use some sort of file log
                Console.WriteLine(e);
                Console.ReadLine();
                Environment.Exit(0);
            }


            ProcessHelper.waitForProcess(config);

            PerformanceCounters performanceCounters = new PerformanceCounters();
            performanceCounters.InitCounters(config);
            performanceCounters.startWriting(config);

            writeHelper.Write(config, performanceCounters.getTotalResult());
            performanceCounters.closeCounters();


            // Ожидание нажатия клавиши Enter перед завершением работы
            string stuff = Console.ReadLine();
            Console.WriteLine("Saved result to " + config.GetoOutputFile());
            Console.WriteLine(stuff);

            Console.ReadLine();

        }

        
    }
}
