using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PerformanceCounters
    {
        private PerformanceCounter cpuCounter = null;
        private PerformanceCounter ramCounter = null;
        private PerformanceCounter handleCounter = null;
        private PerformanceCounter threadCounter = null;

        private ArrayList TotalResult = new ArrayList();
        private Result currentResult = null;

       
       

        public void InitCounters(Config config)
        {
            try
            {
                cpuCounter = new PerformanceCounter("Process", "% Processor Time", config.GetProcessName());
                 ramCounter = new PerformanceCounter("Process", "Working Set", config.GetProcessName());
                handleCounter = new PerformanceCounter("Process", "Handle Count", config.GetProcessName());
                threadCounter = new PerformanceCounter("Process", "Thread Count", config.GetProcessName());

            }
            catch (Exception ex)
            {
               //TODO: handle ex
            }
        }

        //WriteCounters
        private void writeCounters() {
            //TODO: add try, in case if process suddenly dies
            currentResult = new Result(cpuCounter.NextValue(),
                                       ramCounter.NextValue(),
                                       handleCounter.NextValue(),
                                       threadCounter.NextValue() );
            
            TotalResult.Add(currentResult);

        }

        //writeResult
        public void startWriting(Config config) {
            //while (ProcessHelper.isProcessAlive(config))
            for (int i = 0; i<10; i++)
            {
                
                writeCounters();
                Thread.Sleep(config.Getinterval() * 1000);

                //Console.WriteLine("RAM: " + (ram / 1024 / 1024) + " MB; CPU: " + (cpu) + " %");
                //Console.WriteLine("Handles: " + handles + "; threads: " + threads);
                //perfCounters.TryGetValue("Working Set", out value);

            }

        }
        public ArrayList getTotalResult() {
            return TotalResult;
        }



        //closeCounters
        public void closeCounters()
        {
            try
            {
                // dispose of the counters
                if (cpuCounter != null)
                { cpuCounter.Dispose(); }
                if (ramCounter != null)
                { ramCounter.Dispose(); }
                if (handleCounter != null)
                { handleCounter.Dispose(); }
                if (threadCounter != null)
                { threadCounter.Dispose(); }

            }
            finally
            { PerformanceCounter.CloseSharedResources(); }
        }

    }

    class Result
    {
        //TODO: add getName and get ExtraParam
        //use factory?
        public float cpuCounter;
        private float ramCounter;
        private float pageCounter;
        private float threadCounter;

        public float getCpuCounter() { return cpuCounter; }
        public float getRamCounter() { return ramCounter; }
        public float getPageCounter() { return pageCounter; }
        public float getThreadCounter() { return threadCounter; }


        //use setters and getters. This is error-prone and unreadable.
        public Result(float cpuCounter, float ramCounter, float pageCounter, float threadCounter)
        {
            this.cpuCounter = cpuCounter;
            this.ramCounter = ramCounter;
            this.pageCounter = pageCounter;
            this.threadCounter = threadCounter;



        }
    }
}                     
