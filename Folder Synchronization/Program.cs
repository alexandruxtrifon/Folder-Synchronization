using Folder_Synchronization;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;


void executeCode()
{
    string root = @"C:\Users\Alex\Desktop\root";
    string destination = @"C:\Users\Alex\Desktop\destination";
    string consolePath = @"C:\Users\Alex\Desktop\log.txt";
    Logger _logger = new Logger(consolePath);

    ItemList sourceItemList = new ItemList(_logger);
    ItemList destinationItemList = new ItemList(_logger);
    //List<string> rootmd5list = new List<string>();
    //List<string> destinationmd5list = new List<string>();

}

void execute() { 
}
// step 1 check the files in both folders before modifying/ adding.
checkrootlist();
checkdestinationlist();

//foreach (string file in files)
//{
//    File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
//    var stream = File.OpenRead(file);
//    var hash = md5.ComputeHash(stream);
//    Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());

//}
//Console.WriteLine("----------");
//foreach (string file in filesdest)
//{
//    var stream = File.OpenRead(file);
//    var hash = md5.ComputeHash(stream);
//    destinationmd5list.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
//    Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
//}
//foreach(string file in rootmd5list)
//{
//    Console.WriteLine($"{file}");
//}
//Console.WriteLine("----------");
//foreach (string file in destinationmd5list)
//{
//    Console.WriteLine($"{file}");
//}
checkmd5();
void checkmd5()
{
    bool isequal = Enumerable.SequenceEqual(rootmd5list.OrderBy(e => e), destinationmd5list.OrderBy(e => e));
    if (isequal)
    {
        Console.WriteLine("Folders are synchronized");
        return;
    }
    else
    {
        Console.WriteLine("Folders are not synchronized");
        foreach (string file in files)
        {
            string filename = Path.GetFileName(file);
            int rootindex = Array.IndexOf(files, file);
            //if (!destinationmd5list.Contains(rootmd5list[files.ToList().IndexOf(file)]))
            if (rootmd5list.All(x => destinationmd5list.Contains(x)))
            { 
                Console.WriteLine($"{DateTime.Now}  Copying file {filename} to {destination}");
            }
            else if (rootmd5list.Count > destinationmd5list.Count && !rootmd5list[rootindex].Equals(destinationmd5list[rootindex]))
            {
                Console.WriteLine($"{DateTime.Now}  Copying new file {filename} to {destination}");
            }
            //Console.WriteLine($"Copying file {Path.GetFileName(file)} to {destination}");
            File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
            var stream = File.OpenRead(file);
            var hash = md5.ComputeHash(stream);
            //Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
        }
    }
}
void checkrootlist()
{
    foreach (string file in files)
    {
        using (var stream = File.OpenRead(file))
        {
            var hash = md5.ComputeHash(stream);

            rootmd5list.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
        }
        //Console.WriteLine($"File found in root: {Path.GetFileName(file)}");
    }
}
void checkdestinationlist()
{
    foreach (string file in filesdest)
    {
        using (
        var stream = File.OpenRead(file))
        {
            var hash = md5.ComputeHash(stream);
            destinationmd5list.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
        }
        //Console.WriteLine($"File found in destination: {Path.GetFileName(file)}");
    }
}
