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
        //should use list<PCdata> here.
        //add extra class PCdata, to store this stuff: "Process", "% Processor Time"
        //use enum to store all counters, then iterate thru enum to create the List<PCdata>
        //private PerformanceCounter cpuCounter = null;
        //private PerformanceCounter ramCounter = null;
        //private PerformanceCounter handleCounter = null;
        //private PerformanceCounter threadCounter = null;

        private List<PC> performaneCountersList = new List<PC>();

        private ArrayList TotalResult = new ArrayList();
        private List<float> currentResult = null;


        public List<PC> getPerformaneCountersList() {
            return this.performaneCountersList;
        }

        public void InitCounters(Config config)
        {

            performaneCountersList.Add(new PC("Process", "% Processor Time"));
            performaneCountersList.Add(new PC("Process", "Working Set"));
            performaneCountersList.Add(new PC("Process", "Handle Count"));
            performaneCountersList.Add(new PC("Process", "Thread Count"));

            foreach (PC pc in performaneCountersList)
            {
                try
                {
                    //cpuCounter = new PerformanceCounter("Process", "% Processor Time", config.GetProcessName());
                    // ramCounter = new PerformanceCounter("Process", "Working Set", config.GetProcessName());
                    //handleCounter = new PerformanceCounter("Process", "Handle Count", config.GetProcessName());
                    //threadCounter = new PerformanceCounter("Process", "Thread Count", config.GetProcessName());
                    pc.setPerformanceCounter(config);


                }
                catch (Exception ex)
                {
                    Program.HandleMessage("Failed to create performance counter: " + ex.Message);

                }
            }
            
        }

        //WriteCounters
        private void writeCounters() {

            currentResult = new List<float>(4);
            foreach (PC pc in performaneCountersList) {
                currentResult.Add(pc.PerformanceCounter.NextValue());
            }
            

            TotalResult.Add(currentResult);

        }

        //writeResult
        public void startWriting(Config config) {
            Program.HandleMessage("Writing perfomance counters");
            //for (int i = 0; i<10; i++)
            while (ProcessHelper.isProcessAlive(config))
            {
                try
                {
                    writeCounters();
                }
                catch (UnauthorizedAccessException e) {
                    Program.HandleMessage("Don't have access to process : " + e.Message);
                    break;
                }
                catch (Exception e)
                {
                    Program.HandleMessage("Something wrong whith the process: " + e.Message);
                    break;
                }
                Thread.Sleep(config.Getinterval() * 1000);

            }

        }
        public ArrayList getTotalResult() {
            return TotalResult;
        }



        //closeCounters
        public void closeCounters()
        {
            Program.HandleMessage("Closing the counters");
            
            try
            {
                // dispose of the counters
                //if (cpuCounter != null)
                //{ cpuCounter.Dispose(); }
                //if (ramCounter != null)
                //{ ramCounter.Dispose(); }
                //if (handleCounter != null)
                //{ handleCounter.Dispose(); }
                //if (threadCounter != null)
                //{ threadCounter.Dispose(); }
                foreach (PC pc in performaneCountersList)
                {
                    pc.PerformanceCounter.Dispose();
                }

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


    //used to describe a performane counter
    class PC
    {
       
        public PC(string categoryName, string counterName) {
            this.CategoryName = categoryName;
            this.CounterName = counterName;
            this.DsiplayName = counterName;

        }

        public PC(string categoryName, string counterName, string dsiplayName)
        {
            this.CategoryName = categoryName;
            this.CounterName = counterName;
            this.DsiplayName = dsiplayName;

        }

        public void setPerformanceCounter(Config config) {
            this.PerformanceCounter = new PerformanceCounter(this.CategoryName, this.CounterName, config.GetProcessName());
        }

        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string DsiplayName { get; set; }
        public PerformanceCounter PerformanceCounter;
    }

    
}                     
