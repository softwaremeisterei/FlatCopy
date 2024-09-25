if (args.Length != 2) {
    PrintUsage();
    return;
}

string sourceDir = args[0];
string targetDir = args[1];

if (!Directory.Exists(sourceDir)) {
    Console.WriteLine($"directory {sourceDir} does not exist.");
    return;
}

if (!Directory.Exists(targetDir)) {
    Console.WriteLine($"directory {targetDir} does not exist.");
    return;
}

int counter = 0;
int countOK = 0;
int countERR = 0;
var files = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);

Console.WriteLine($"flat copy files from {sourceDir} to {targetDir}");
Console.WriteLine();

foreach (var filePath in files) {
    var fileName = Path.GetFileName(filePath);
    Console.Write($"{(++counter).ToString().PadLeft(5, ' ')} of {files.Length} {filePath} ");
    try {
        File.Copy(filePath, Path.Combine(targetDir, fileName), false);
        countOK++;
        Console.WriteLine("OK");
    }
    catch (Exception ex) {
        countERR++;
        Console.WriteLine($"ERR {ex.Message}");
    }
}

Console.WriteLine($"{countOK} files copied");
if (countERR > 0) {
    Console.WriteLine($"{countERR} files could not be copied");
}

void PrintUsage()
{
    Console.WriteLine("Usage: FlatCopy <Source Directory> <Target Directory>");
    Console.WriteLine();
    Console.WriteLine("Copies all files in the source and its sub directories to the target directory without copying the folder structure");
}
