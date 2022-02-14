using System;
using System.IO;
using System.Net;

namespace np_lesson8
{
    class Program
    {

        static void RequestFtpServer()
        {
            // HttpWebRequest, FtpWebRequest - old classes
            // WebRequest

            var request = WebRequest.Create("ftp://localhost:21") as FtpWebRequest;

            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            var response = request.GetResponse() as FtpWebResponse;
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var data = sr.ReadToEnd();
            Console.WriteLine(data);
        }


        static void UploadFile()
        {
            var request = WebRequest.Create("ftp://localhost:21/destination.txt") as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            var requestStream = request.GetRequestStream();

            var fs = new FileStream("source.txt", FileMode.Open);
            fs.CopyTo(requestStream);
            
            fs.Flush();
            fs.Close();
            requestStream.Close();
        }



        static void DownloadFile()
        {
            var request = WebRequest.Create("ftp://localhost:21/source.txt") as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            var response = request.GetResponse() as FtpWebResponse;
            var stream = response?.GetResponseStream();

            var fs = new FileStream("destionation.txt", FileMode.Create);
            stream.CopyTo(fs);
            stream.Close();
            fs.Close();
        }

        static void Main(string[] args)
        {
            // RequestFtpServer();
            // UploadFile();
            // DownloadFile();
        }
    }
}
