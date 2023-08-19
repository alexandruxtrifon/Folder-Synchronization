using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

string root = @"C:\Users\Alex\Desktop\root\";
string destination = @"C:\Users\Alex\Desktop\destination\";
//Console.Write("Enter the source folder path: ");
//string root = Console.ReadLine();
string[] files = Directory.GetFiles(root);
//Console.Write("\nEnter the destination folder path: ");
//string destination = Console.ReadLine();
var md5 = MD5.Create();
string[] filesdest = Directory.GetFiles(destination);
List<string> rootmd5list = new List<string>();
List<string> destinationmd5list = new List<string>();
foreach (string file in files)
{
    File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
    var stream = File.OpenRead(file);
    var hash = md5.ComputeHash(stream);
    Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
    rootmd5list.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
}
Console.WriteLine("----------");
foreach (string file in filesdest)
{
    var stream = File.OpenRead(file);
    var hash = md5.ComputeHash(stream);
    destinationmd5list.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
    Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
}
foreach(string file in rootmd5list)
{
    Console.WriteLine($"{file}");
}
Console.WriteLine("----------");
foreach (string file in destinationmd5list)
{
    Console.WriteLine($"{file}");
}
bool isEqual = Enumerable.SequenceEqual(rootmd5list.OrderBy(e => e), destinationmd5list.OrderBy(e => e));
if (isEqual)
{
    Console.WriteLine("Lists are Equal");
}
else
{
    Console.WriteLine("Lists are not Equal");
}


void checkmd5()
{

}

class Item
{

}
