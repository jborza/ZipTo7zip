// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var zipFiles = Directory.GetFiles(".", "*.zip");
foreach (var zipFile in zipFiles)
{
    string _7z = "c:\\Program Files\\7-Zip\\7z";
    string noExtension = Path.GetFileNameWithoutExtension(zipFile);
    // extract
    Process.Start(_7z, $"x \"{zipFile}\" -o\"{noExtension}\"").WaitForExit();
    Directory.SetCurrentDirectory(noExtension);
    // repack as 7z
    string _7zname = $"{noExtension}.7z";
    Process.Start(_7z, $"a \"{_7zname}\" * -r -mx9").WaitForExit();
    // move 7z to parent directory
    File.Move(_7zname, "..\\" + _7zname);
    // go back
    Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetParent(noExtension).FullName).FullName);
    // remove the extracted directory
    Directory.Delete(noExtension, true);
    // remove the original zip name
    File.Delete(zipFile);
}
