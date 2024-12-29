using War3Net.Build;

var workingDir = Environment.GetEnvironmentVariable("GITHUB_WORKSPACE");
Console.WriteLine($"Working directory: {workingDir}");
var maps_x = Directory.GetFileSystemEntries(workingDir, "*.w3x", SearchOption.AllDirectories);
var maps_m = Directory.GetFileSystemEntries(workingDir, "*.w3m", SearchOption.AllDirectories);

var files = new List<string>();
files.AddRange(maps_x);
files.AddRange(maps_m);

if (files.Count == 0)
{
    Console.WriteLine($"Did not find any Warcraft III map files");
    throw new Exception();
}
else if (files.Count > 1)
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
string outputDir = Path.Combine(workingDir, "output");
Directory.CreateDirectory(outputDir);
string name = Path.GetFileName(mapFile);
string fullPath = Path.Combine(outputDir, name);

Console.WriteLine("Building map...");
var map = Map.Open(mapFile);
var builder = new MapBuilder(map);
builder.Build(fullPath);

if(!File.Exists(fullPath))
{
    Console.WriteLine("Could not write file.");
    throw new Exception();
}

Console.WriteLine("Build complete!");
Console.WriteLine("Path: " + fullPath);
