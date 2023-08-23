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
            foreach(Item item in this)
            {
                outputString += item.name + "\n";
            }
            return outputString;
        }

        public void CompareLists(ItemList destinationList)
        {
            ItemList tempSourceList = new ItemList(this);
            ItemList tempDestinationList = new ItemList(destinationList);
            bool isDifferent = false;
            _logger.LogMessage("\n======================================================================");

            foreach(Item item in tempSourceList)
            {
                if(isDifferent == false)
                {
                    for(int i=0;i<tempDestinationList.Count -1 && tempDestinationList[i].verified == false && item.verified == false; i++)
                    {
                        if (tempDestinationList[i].name != item.name || tempDestinationList[i].directory != item.directory)
                        {
                            _logger.LogMessage($"Item {item.name} has been modified");
                            tempDestinationList[i].verified = true;
                            item.verified = true;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
