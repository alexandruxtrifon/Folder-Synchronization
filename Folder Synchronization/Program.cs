using System.Globalization;


string root = @"C:\Users\Alex\Desktop\root\";
string destination = @"C:\Users\Alex\Desktop\destination\";
string[] files = Directory.GetFiles(root);
foreach (string file in files)
{
    File.Copy(file, $"{destination}{Path.GetFileName(file)}", true);
}
