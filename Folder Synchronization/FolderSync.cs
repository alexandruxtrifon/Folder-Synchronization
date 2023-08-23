using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchronization
{
    internal class FolderSync
    {
        private Timer _timer;

        private string _root;
        private string _destination;
        private string _consolePath;
        private int _interval;

        public FolderSync(string root, string destination, string consolePath, int interval)
        {
            _root = root;
            _destination = destination;
            _consolePath = consolePath;
            _interval = interval;
        }
        
        public void Start()
        {
        }

        public void Stop()
        {

        }

    }
}
