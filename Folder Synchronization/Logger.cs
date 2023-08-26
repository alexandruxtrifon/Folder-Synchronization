using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchronization
{
    internal class Logger
    {
        public string consolePath { get; set; }
        private readonly object fileLock = new object();
        public void LogMessage(string message)
        {
            LogMessageInConsole(message);
            LogMessageInLogFile(message);
        }

        public void LogMessageInConsole(string message, bool withTimestamp = true)
        {
            if (withTimestamp)
            {
                Console.WriteLine($@"[{DateTime.Now}]: {message}");
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        public void LogMessageInLogFile(string message, bool withTimestamp = true)
        {
            lock (fileLock)
            {
                if (withTimestamp)
                {
                    File.AppendAllText(consolePath, $@"[{DateTime.Now}]: {message}" + "\n");
                }
                else
                {
                    File.AppendAllText(consolePath, (message + "\n"));
                }
            }
        }
        public Logger(string consolePath)
        {
            this.consolePath = consolePath;

            if(!File.Exists(consolePath)&& Path.GetExtension(consolePath) == ".txt")
            {
                this.consolePath = consolePath + $@"/log.txt";
                FileStream consoleFile = File.Create(this.consolePath);
                consoleFile.Close();
            }
            string message = "\n-----------------------------------------------------------";
            LogMessageInLogFile(message, false);
            LogMessageInConsole(message, false);
        }
    }
}
