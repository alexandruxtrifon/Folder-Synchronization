using Folder_Synchronization;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;


switch (args.Length)
{
    case 0:
        Console.WriteLine("You need to input source path, destination path, log path & inverval");
        break;
    case 1:
        Console.WriteLine("You need to input destination path, log path & interval");
        break;
    case 2:
        Console.WriteLine("You need to input log path & interval");
        break;
    case 3:
        Console.WriteLine("You need to input interval");
        break;
    case 4:
        executeCode();
        break;
    default:
        Console.WriteLine("You have entered too many arguments");
        break;
}

void executeCode()
{
    string root = args[0];
    string destination = args[1];
    string consolePath = args[2];
    int interval = int.Parse(args[3]);
    Logger _logger = new Logger(consolePath);

    ItemList sourceItemList = new ItemList(_logger);
    ItemList destinationItemList = new ItemList(_logger);

    sourceItemList.GetAllFilesAndDirectories(root);
    destinationItemList.GetAllFilesAndDirectories(destination);
    sourceItemList.CompareLists(destinationItemList);

    Timer(sourceItemList, destinationItemList, root, destination, interval);

}
void Timer(ItemList sourceItemList, ItemList destinationItemList, string root, string destination, int interval)
{
    Timer timer = null;

    TimerCallback callback = _ =>
    {
        sourceItemList.GetAllFilesAndDirectories(root);
        destinationItemList.GetAllFilesAndDirectories(destination);
        sourceItemList.CompareLists(destinationItemList);
    };

    timer = new Timer(callback, null, interval * 1000, interval * 1000);

    Console.WriteLine($"Folder Synchronization started. The synchronization will be repeated every {interval} seconds.");
    Console.WriteLine("Press Enter to exit.");
    Console.ReadLine();

    timer.Dispose();
}
