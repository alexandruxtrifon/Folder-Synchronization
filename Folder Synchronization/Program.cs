using Folder_Synchronization;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

executeCode();
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

// cd C:\Users\Alex\source\repos\Folder Synchronization\Folder Synchronization\bin\Debug\net7.0
// foldersynchronization "C:\Users\Alex\Desktop\root" "C:\Users\Alex\Desktop\destination" "C:\Users\Alex\Desktop\log.txt" 5

void executeCode()
{
    //string root = @"C:\Users\Alex\Desktop\root";
    //string destination = @"C:\Users\Alex\Desktop\destination";
    //string consolePath = @"C:\Users\Alex\Desktop\log.txt";
    //int interval = 10;

    string root = args[0];
    string destination = args[1];
    string consolePath = args[2];
    int interval = int.Parse(args[3]);

    Logger _logger = new Logger(consolePath);

    ItemList sourceItemList = new ItemList(_logger);
    ItemList destinationItemList = new ItemList(_logger);

    sourceItemList.GetAllFilesAndDirectories(root);
    destinationItemList.GetAllFilesAndDirectories(destination);
    sourceItemList.CompareLists(destinationItemList, root, destination);

    timer(sourceItemList, destinationItemList, root, destination, interval);

}
void timer(ItemList sourceItemList, ItemList destinationItemList, string root, string destination, int interval)
{
    Timer timer = null;

    TimerCallback callback = _ =>
    {
        sourceItemList.Clear();
        destinationItemList.Clear();
        sourceItemList.GetAllFilesAndDirectories(root);
        destinationItemList.GetAllFilesAndDirectories(destination);
        sourceItemList.CompareLists(destinationItemList, root, destination);
    };

    timer = new Timer(callback, null, interval * 1000, interval * 1000);

    Console.WriteLine($"Folder Synchronization started. The task will be repeated every {interval} seconds.");
    Console.WriteLine("Press Enter to exit.");
    Console.ReadLine();

    timer.Dispose();
}