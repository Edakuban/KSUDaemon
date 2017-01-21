using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KegelbahnSpielberichtUploadDaemon.BusinessLogic
{
    internal class Logger
    {
        public static void Log(string log)
        {
            try
            {
                using (var sw = new StreamWriter(string.Format("{0}\\Log.txt", AppDomain.CurrentDomain.BaseDirectory), true))
                {
                    sw.WriteLine(string.Format("{0}: {1}", DateTime.Now.ToString(), log));
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
            }
        }
    }
}
