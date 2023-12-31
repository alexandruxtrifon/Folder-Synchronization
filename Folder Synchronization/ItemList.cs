﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchronization
{
    internal class ItemList : List<Item>
    {
        Logger _logger;

        public ItemList(Logger logger)
        { _logger = logger; }

        public ItemList(ItemList itemlist)
        {
            this._logger = itemlist._logger;
            foreach (var item in itemlist)
            {
                this.Add(item);
            }
        }

        public ItemList GetAllFilesAndDirectories(string path)
        {
            ItemList tempItemList = new ItemList(_logger);
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                this.Add(new Item(file, _logger));
            }

            string[] directories = Directory.GetDirectories(path);

            foreach (string directory in directories)
            {
                ItemList directoryFiles = GetAllFilesAndDirectories(directory);

                foreach (Item file in directoryFiles)
                {
                    this.Add(new Item(file.path, _logger));
                }
            }
            return tempItemList;
        }

        public override string ToString()
        {
            string outputString = string.Empty;
            foreach (Item item in this)
            {
                outputString += item.name + "\n";
            }
            return outputString;
        }

        public void CompareLists(ItemList destinationList, string root, string destination)
            {
            ItemList tempSourceList = new ItemList(this);
            ItemList tempDestinationList = new ItemList(destinationList);
            _logger.LogMessage("\n======================================================================");
            bool isDifferent = false;

            foreach (Item sourceItem in tempSourceList)
            {
                if (isDifferent == false)
                {
                    bool found = false;

                    for (int i = IndexOf(sourceItem); i < tempDestinationList.Count - IndexOf(sourceItem) - 1 && tempDestinationList[i].verified == false && sourceItem.verified == false; i++)
                    {
                        if (tempDestinationList[i].name != sourceItem.name || tempDestinationList[i].directory != sourceItem.directory)
                        {
                            found = true;
                            //tempDestinationList[i].verified = true;
                            //sourceItem.verified = true;
                            break;
                        }
                    }
                    foreach (Item destItem in tempDestinationList)
                    {
                        if (sourceItem.name == destItem.name &&
                            sourceItem.hash == destItem.hash)
                        {
                            sourceItem.verified = true;
                            destItem.verified = true;
                        }
                    }
                }
            }
            foreach (Item item in tempDestinationList)
            {
                if (!item.verified)
                {
                    string filePath = Path.Combine(destination, item.name);
                    File.Delete(filePath);
                    _logger.LogMessage($"Deleted {item.name} from the destination folder");
                }
            }

            foreach (string dirPath in Directory.GetDirectories(destination))
            {
                string dirName = Path.GetFileName(dirPath);
                if (!tempSourceList.Any(item => item.directory == dirName))
                {
                    Directory.Delete(dirPath, true);
                    _logger.LogMessage($"Deleted directory {dirName} from the destination folder");
                }
            }

            foreach (Item item in tempSourceList)
            {
                if (!item.verified)
                {
                    //_logger.LogMessage($"Copying '{item.name}'");
                    string destinationPath = Path.Combine(destination, item.name);

                    if (File.Exists(destinationPath))
                    {
                        using (var destFileStream = File.Open(destinationPath, FileMode.Open, FileAccess.Read))
                        using (var sourceFileStream = File.Open(item.path, FileMode.Open, FileAccess.Read))
                        using (var destMD5 = MD5.Create())
                        using (var sourceMD5 = MD5.Create())
                        {
                            byte[] destHash = destMD5.ComputeHash(destFileStream);
                            byte[] sourceHash = sourceMD5.ComputeHash(sourceFileStream);

                            if (BitConverter.ToString(destHash) == BitConverter.ToString(sourceHash))
                            {
                                item.verified = true;
                                continue;
                            }
                        }
                    }
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    using (FileStream sourceStream = File.Open(item.path, FileMode.Open, FileAccess.Read))
                    using (FileStream destinationStream = File.Create(destinationPath))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }
                    _logger.LogMessage($"Copied '{item.name}' to the destination folder");
                }
            } 
        }
    }
}
