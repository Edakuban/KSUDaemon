using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KegelbahnSpielberichtUploadDaemon.BusinessLogic
{
    internal class CopyMagic
    {
        internal static void DoMagic(Config config)
        {
            try
            {
                var file = GetNewestFile(config.Source);
                CopyFileToFtp(file, config.FtpServer, config.FtpUser, config.FtpPasswd, config.FtpFolder);
            }
            catch (Exception ex)
            {
                Logger.Log("Exception: " + ex.Message);
            }
        }

        private static FileInfo GetNewestFile(string source)
        {
            return new DirectoryInfo(source)
                .GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
        }

        private static void CopyFileToFtp(FileInfo file, string ftpServer, string ftpUser, string ftpPasswd, string ftpFolder)
        {
            var ftpPath = string.Format(
                "ftp://{0}{1}{2}", 
                ftpServer.EndsWith("/") ? ftpServer : ftpServer + "/",
                ftpFolder.EndsWith("/") ? ftpFolder : ftpFolder + "/",
                "spielbericht.html"
            ); 

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUser, ftpPasswd);

            StreamReader sr = new StreamReader(file.FullName);
            byte[] fileContents = Encoding.UTF8.GetBytes(sr.ReadToEnd());
            sr.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
    }
}
