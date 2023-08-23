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
        Logger _logger;

        public string path { get; set; }
        public string name { get; }
        public string directory { get; }
        public string hash { get; }
        public bool verified { get; set; } = false;

        public Item(string path, Logger logger)
        {
            this.path = path;
            this.name = Path.GetFileName(path);
            _logger = logger;

            if (Path.GetDirectoryName(path) != null)
            {
                this.directory = Path.GetFileName(Path.GetDirectoryName(path));
            }
            else
            {
                this.directory = string.Empty;
            }
            
            var stream = File.OpenRead(path);
            _byteHash = _md5.ComputeHash(stream);
            hash = BitConverter.ToString(_byteHash).Replace("-", "").ToLowerInvariant();
            stream.Close();

            if (this.path != _logger.consolePath)
            {
                _logger.LogMessage($"The file named {name} has been created on path {path}");
            }
        }
    }
}
