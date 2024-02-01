if (args.Length != 2)
{
    Console.WriteLine("FolderDiff (foldiff) - CLI utility to compare 2 folders");
    Console.WriteLine();
    Console.WriteLine("Usage: foldiff.exe [path 1] [path 2]");
    Console.WriteLine();
    return;
}

string path1 = args[0];
string path2 = args[1];

if (path1.EndsWith('\\'))
{
    path1 = path1[..^1];
}

if (path2.EndsWith('\\'))
{
    path2 = path2[..^1];
}

if (!Directory.Exists(path1))
{
    Console.WriteLine($"Cannot find directory 1: {path1}");
    return;
}

if (!Directory.Exists(path2))
{
    Console.WriteLine($"Cannot find directory 1: {path2}");
    return;
}

IEnumerable<string> names1 = Directory.GetFileSystemEntries(path1).Select(Path.GetFileName).Cast<string>();
IEnumerable<string> names2 = Directory.GetFileSystemEntries(path2).Select(Path.GetFileName).Cast<string>();

int longestFilenameL = Math.Max(names1.Max(x => x.Length), 4); // for the ????
int longestFilenameR = Math.Max(names2.Max(x => x.Length), 4);

string dir1Name = Path.GetFileName(path1);
string dir2Name = Path.GetFileName(path2);

longestFilenameL = Math.Max(longestFilenameL, dir1Name.Length);
longestFilenameR = Math.Max(longestFilenameR, dir2Name.Length);

Console.WriteLine($"     {dir1Name.PadLeft(longestFilenameL)} | {dir2Name}");
Console.WriteLine($"     {new string('-', longestFilenameL+1)}+{new string('-', longestFilenameR)}");

foreach (string nameLeft in names1.Where(x => !names2.Contains(x)))
{
    Console.WriteLine($"[MR] {nameLeft.PadLeft(longestFilenameL)} | ????");
}

foreach (string nameRight in names2.Where(x => !names1.Contains(x)))
{
    Console.WriteLine($"[ML] {"????".PadLeft(longestFilenameL)} | {nameRight}");
}