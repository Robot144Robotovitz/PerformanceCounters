﻿
using System.Collections.Specialized;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Program
    {
        //TODO: add function to handle exceptions and messages
        private static Config config = null;
        private static PerformanceCounters performanceCounters = new PerformanceCounters();

        static void Main(string[] args)

            
        {
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);


            
            try
            {
                config = new Config();
            }
            catch (Exception e){
                //should use some sort of file log
                HandleMessage(e);
                Console.ReadLine();
                Environment.Exit(0);
            }


           

            ProcessHelper.waitForProcess(config);

            
            performanceCounters.InitCounters(config);

            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            HandleMessage("Start writing in file");
            performanceCounters.startWriting(config);

            writeHelper.Write(config, performanceCounters);
            performanceCounters.closeCounters();


            // Ожидание нажатия клавиши Enter перед завершением работы          
            HandleMessage("Saved result to " + config.GetoOutputFile());
            Console.ReadLine();

        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e, Config config, PerformanceCounters performanceCounters)
        {
            writeHelper.Write(config, performanceCounters);
            performanceCounters.closeCounters();

        }



        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            HandleMessage("Exiting system due to external CTRL-C, or process kill, or shutdown" );


            //do your cleanup here
            //Thread.Sleep(5000); //simulate some cleanup delay
            writeHelper.Write(config, performanceCounters);
            performanceCounters.closeCounters();

            HandleMessage("Saved result to " + config.GetoOutputFile());

            HandleMessage("Cleanup complete");

            //allow main to run off
            //bool exitSystem = add;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion


        //should use some file logger, probably. 
        //There's Log4cs library, but in a current situation that's an overkill
        public static void  HandleMessage(string msg) {
            Console.WriteLine(msg);
        }

        public static void HandleMessage(Exception msg)
        {
            Console.WriteLine(msg);
        }

    }
}
