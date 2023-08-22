using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchronization
{
    internal class Logger
    {
        private string consolePath { get; set; }
        public Logger(string consolePath)
        {
            this.consolePath = consolePath;
        }
    }
}
