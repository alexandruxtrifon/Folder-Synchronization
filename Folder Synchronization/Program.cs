using System.Globalization;


//string root = @"C:\Users\Alex\Desktop\root\";
//string destination = @"C:\Users\Alex\Desktop\destination\";
Console.Write("Enter the source folder path: ");
string root = Console.ReadLine();
string[] files = Directory.GetFiles(root);
Console.Write("\nEnter the destination folder path: ");
string destination = Console.ReadLine();
foreach (string file in files)
{
    File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
}
