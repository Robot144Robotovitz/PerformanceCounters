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

        
        public static void Write(Config config, PerformanceCounters PCs )
        {
            string csvSeparator = ";";
            
            //we can do different sorts of actions based on config:
            //write in csv with comma, or semicolon, or json, or watnot
            using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(config.GetoOutputFile() + GetFileName(config), true))
            {
                file.WriteLine("sep=" + csvSeparator);
                //TODO: refactor with getNames from Result
                StringBuilder sb = new StringBuilder();
                foreach (PC pc in PCs.getPerformaneCountersList()) {
                    sb.Append(pc.DsiplayName).Append(csvSeparator);
                }
                file.WriteLine(sb.ToString());
                foreach (List<float> line in PCs.getTotalResult()) {
                    sb.Clear();
                    //theres probably a better solution
                    foreach (float fl in line) {
                        sb.Append(fl).Append(csvSeparator);
                    }          
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
