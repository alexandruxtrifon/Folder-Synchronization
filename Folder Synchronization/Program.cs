using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

string root = @"C:\Users\Alex\Desktop\root\";
string destination = @"C:\Users\Alex\Desktop\destination\";
//Console.Write("Enter the source folder path: ");
//string root = Console.ReadLine();
string[] files = Directory.GetFiles(root);
//Console.Write("\nEnter the destination folder path: ");
//string destination = Console.ReadLine();
var md5 = MD5.Create();
string[] filesdest = Directory.GetFiles(destination);

foreach (string file in files)
{
    File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
    var stream = File.OpenRead(file);
    var hash = md5.ComputeHash(stream);
    Console.WriteLine($"{Path.GetFileName(file)} -> " + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
}



class Item
{

}
