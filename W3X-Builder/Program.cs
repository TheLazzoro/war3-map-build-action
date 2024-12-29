using War3Net.Build;

var workingDir = Environment.GetEnvironmentVariable("GITHUB_WORKSPACE");
Console.WriteLine($"Working directory: {workingDir}");
var files = Directory.GetFileSystemEntries(workingDir, ".w3x, .w3m", SearchOption.AllDirectories);

if (files.Length == 0)
{
    Console.WriteLine($"Did not find any Warcraft III map files");
    throw new Exception();
}
else if (files.Length > 1)
{
    Console.WriteLine($"Multiple Warcraft III map files found in repository. This action can only continue with a single map file.");
    Console.WriteLine($"Found:");
    foreach (var file in files)
    {
        Console.WriteLine(file);
    }
    throw new Exception();
}

string mapFile = files[0];
string name = Path.GetFileName(mapFile);
string fullPath = Path.Combine(workingDir, name);

Console.WriteLine("Building map...");
var map = Map.Open(mapFile);
var builder = new MapBuilder(map);
builder.Build(fullPath);
Console.WriteLine("Build complete!");
Console.WriteLine("Path: " + fullPath);
