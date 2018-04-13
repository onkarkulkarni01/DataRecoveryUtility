using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DataRecoveryUtility
{
    class ErrorLog
    {
        /// <summary>
        /// This method is for writting the Log file with message parameter
        /// </summary>
        /// <param name="message"></param>
        public static void LogFileWrite(string methodName, Exception ex)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = ConfigurationManager.AppSettings["errorLogFilePath"];

                logFilePath = logFilePath + "ErrorLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) 
                    return;
                
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);

                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) 
                    logDirInfo.Create();
                
                if (!logFileInfo.Exists)
                    fileStream = logFileInfo.Create();
                else
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Error Occured from - " + methodName);
                streamWriter.WriteLine("\n");
                streamWriter.WriteLine("Exception at :- " + DateTime.Now);
                streamWriter.WriteLine(ex.Message);
                streamWriter.WriteLine("\n");
                streamWriter.WriteLine("Stack Trace :- ");
                streamWriter.WriteLine(ex.StackTrace);
                streamWriter.WriteLine("\n");
                streamWriter.WriteLine("Inner Exception :- ");
                streamWriter.WriteLine(ex.InnerException);
                streamWriter.WriteLine("------------------------------------------");
            }
            finally
            {
                if (streamWriter != null) 
                    streamWriter.Close();
                if (fileStream != null) 
                    fileStream.Close();
            }
 
        }

        /// <summary>
        /// This method is for writting the Log file with message parameter
        /// </summary>
        /// <param name="message"></param>
        public static void LogFileWrite(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = ConfigurationManager.AppSettings["errorLogFilePath"];

                logFilePath = logFilePath + "ErrorLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals(""))
                    return;

                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);

                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists)
                    logDirInfo.Create();

                if (!logFileInfo.Exists)
                    fileStream = logFileInfo.Create();
                else
                    fileStream = new FileStream(logFilePath, FileMode.Append);

                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(message);
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
                if (fileStream != null)
                    fileStream.Close();
            }

        }
    }
}