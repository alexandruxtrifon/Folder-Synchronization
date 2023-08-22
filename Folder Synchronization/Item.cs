using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchronization
{
    internal class Item
    {
        MD5 _md5 = MD5.Create();
        byte[] _byteHash;


        public string path { get; set; }
        public string name { get; }
        public string directory { get; }
        public string hash { get; }
        public bool verified { get; set; } = false;

        public Item()
        {

        }

    }
}
