using System;
using System.Collections.Generic;
using System.Linq;
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

        //public void CompareLists(ItemList destinationList, string root, string destination)
        //{
        //    ItemList tempSourceList = new ItemList(this);
        //    ItemList tempDestinationList = new ItemList(destinationList);
        //    bool isDifferent = false;
        //    _logger.LogMessage("\n======================================================================");

        //    foreach (Item item in tempSourceList)
        //    {
        //        if (isDifferent == false)
        //        {
        //            bool found = false;
        //            for (int i = IndexOf(item); i < tempDestinationList.Count - IndexOf(item) - 1 && tempDestinationList[i].verified == false && item.verified == false; i++)
        //            {
        //                if (tempDestinationList[i].name != item.name || tempDestinationList[i].directory != item.directory)
        //                {
        //                    found = true;
        //                    //_logger.LogMessage($"Item {item.name} has been modified");
        //                    tempDestinationList[i].verified = true;
        //                    item.verified = true;
        //                    break;
        //                }
        //            }
        //            if (!found)
        //            {
        //                _logger.LogMessage($"Item '{item.name}' is missing in the destination folder. Copying...");
        //                //string destinationPath = Path.Combine(destination, item.directory, item.name);
        //                string destinationPath = Path.Combine(destination, item.name);
        //                //string sourcePath = Path.Combine(root, item.directory, item.name);
        //                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
        //                File.Copy(item.path, destinationPath, true);
        //                _logger.LogMessage($"Copied '{item.name}' to the destination folder");
        //            }
        //        }
        //        else { break; }
        //    }

        //    var verifiedItems = tempDestinationList.Where(item => item.verified == true).ToList();

        //    foreach (Item item in verifiedItems)
        //    {
        //        if (!item.verified)
        //        {
        //            _logger.LogMessage($"Deleting '{item.name}' from the destination folder");
        //            string filePath = Path.Combine(destination, item.name);

        //            if (File.Exists(filePath))
        //            {
        //                File.Delete(filePath);
        //                _logger.LogMessage($"Deleted {item.name} from the destination folder");
        //            }
        //            else if (Directory.Exists(filePath))
        //            {
        //                Directory.Delete(filePath, true);
        //                _logger.LogMessage($"Deleted directory {item.name} from the destination folder");
        //            }
        //        }
        //    }
        //}

        public void CompareLists(ItemList destinationList, string root, string destination)
        {
            ItemList tempSourceList = new ItemList(this);
            ItemList tempDestinationList = new ItemList(destinationList);
            _logger.LogMessage("\n======================================================================");

            // Mark items in destination that are present in source and have the same hash
            foreach (Item sourceItem in tempSourceList)
            {
                foreach (Item destItem in tempDestinationList)
                {
                    if (sourceItem.name == destItem.name && sourceItem.directory == destItem.directory &&
                        sourceItem.hash == destItem.hash)
                    {
                        sourceItem.verified = true;
                        destItem.verified = true;
                        break;
                    }
                }
            }

            // Delete items (files and directories) from destination that are not present in source
            foreach (Item item in tempDestinationList)
            {
                if (!item.verified)
                {
                    string filePath = Path.Combine(destination, item.directory, item.name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        _logger.LogMessage($"Deleted {item.name} from the destination folder");
                    }
                    else if (Directory.Exists(filePath))
                    {
                        Directory.Delete(filePath, true);
                        _logger.LogMessage($"Deleted directory {item.name} from the destination folder");
                    }
                }
            }

            // Copy missing or modified items from source to destination
            foreach (Item item in tempSourceList)
            {
                if (!item.verified)
                {
                    _logger.LogMessage($"Item '{item.name}' is missing or modified in the destination folder. Copying...");
                    string destinationPath = Path.Combine(destination, item.name);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    using (FileStream sourceStream = File.Open(item.path, FileMode.Open, FileAccess.Read))
                    using (FileStream destinationStream = File.Create(destinationPath))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }

                    //File.Copy(item.path, destinationPath, true);
                    _logger.LogMessage($"Copied '{item.name}' to the destination folder");
                }
            }
        }
    }
}
