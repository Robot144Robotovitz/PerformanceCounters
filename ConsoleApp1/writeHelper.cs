using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class writeHelper
    {

        
        public static void Write(Config config, ArrayList result)
        {
            string csvSeparator = ";";
            
            //we can do different sorts of actions based on config:
            //write in csv with comma, or semicolon, or json, or watnot
            using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(config.GetoOutputFile() + GetFileName(config), true))
            {
                file.WriteLine("sep=" + csvSeparator);
                //TODO: refactor with getNames from Result
                file.WriteLine(string.Format("{1}{0}{2}{0}{3}{0}{4}", csvSeparator, "% Processor Time", "Working Set(RAM)", "Handle Count", "Thread Count") );
                foreach (Result line in result) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(line.getCpuCounter()).Append(csvSeparator);
                    sb.Append(line.getRamCounter()).Append(csvSeparator);
                    sb.Append(line.getPageCounter()).Append(csvSeparator);
                    sb.Append(line.getThreadCounter()).Append(csvSeparator);                    
                    file.WriteLine(sb.ToString());
                    file.Flush();
                }
                
            }

        }

        private static string GetFileName(Config config) {
            string mimeType = ".csv";
            string fileName = Path.DirectorySeparatorChar + "result" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + mimeType;
            return fileName;
        }
    }
}
