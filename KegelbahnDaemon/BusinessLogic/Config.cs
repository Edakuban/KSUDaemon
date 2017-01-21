using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KegelbahnSpielberichtUploadDaemon.BusinessLogic
{
    internal class Config
    {
        public string Source { get; set; }
        public string FtpServer { get; set; }
        public string FtpUser { get; set; }
        public string FtpPasswd { get; set; }
        public string FtpFolder{ get; set; }
        public int Interval { get; set; }

        public Config(string config)
        {
            var xDoc = XDocument.Load(config);
            Source = xDoc.Elements("config").Elements("source").FirstOrDefault().Value;
            FtpServer = xDoc.Elements("config").Elements("ftpserver").FirstOrDefault().Value;
            FtpUser = xDoc.Elements("config").Elements("ftpuser").FirstOrDefault().Value;
            FtpPasswd = xDoc.Elements("config").Elements("ftppasswd").FirstOrDefault().Value;
            FtpFolder = xDoc.Elements("config").Elements("ftpfolder").FirstOrDefault().Value;
            Interval = int.Parse(xDoc.Elements("config").Elements("interval").FirstOrDefault().Value) * 1000;
        }
    }
}
