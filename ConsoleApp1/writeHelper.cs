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
            //we can do different sorts of actions based on config:
            //write in csv with comma, or semicolon, or json, or watnot
            using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(config.GetoOutputFile() + GetFileName(config), true))
            {
                //TODO: refactor with getNames from Result
                file.WriteLine("% Processor Time; Working Set(RAM); Handle Count; Thread Count");
                foreach (Result line in result) {
                    var res = string.Format("{0}; {1}; {2}; {3}", line.getCpuCounter(), line.getRamCounter(), line.getPageCounter(), line.getThreadCounter());
                    file.WriteLine(res);
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
