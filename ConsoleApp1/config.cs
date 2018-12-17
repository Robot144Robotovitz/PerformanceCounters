using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Config
    {
        private readonly string outputFolder;
        private readonly int interval;
        private readonly string processName;


        public string GetoOutputFile()
        {
            return outputFolder;
        }

        public int Getinterval()
        {
            return interval;
        }
       public string GetProcessName()
        {
            return processName;
        }

        //we might want to read properties from other source (.properties, xml, json, whatnot)
        //in wich case we should just add another constructor
        public Config() {
            outputFolder = ParseOutputFile(ConfigurationManager.AppSettings.Get("outputFile"));
            interval = ParseInterval(ConfigurationManager.AppSettings.Get("interval"));
            processName = ParseProcessName(ConfigurationManager.AppSettings.Get("processName"));
        }

        private int ParseInterval(string input)
        {

            if (Int32.TryParse(input, out int result) && result > 0)
            {
                return result;
            }
            else
            {
                throw new Exception(String.Format("Field interval in config is not valid: interval={0}", input));
            }
        }
        private string ParseOutputFile(string input) {
            if (System.IO.Directory.Exists(input))
            {
                return input;
            }
            else {
                throw new Exception(String.Format("Field outputFile in config is incorrect: outputFile={0}", input) );
            }
        }
        private string ParseProcessName(string input)
        {
            //check if it's null, empty or whitespaces
            if (input!=null && input.Trim().Length!=0)
            {
                return input.Trim();
            }
            else
            {
                throw new Exception(String.Format("Field outputFile in config is incorrect: outputFile={0}", input));
            }
        }



    }
}
