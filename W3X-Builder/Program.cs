using CommandLine;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Testing custom GH action");

var projectDir = Environment.GetEnvironmentVariable("GITHUB_WORKSPACE");
Console.WriteLine($"Project directory: {projectDir}");
Directory.GetFileSystemEntries(projectDir, "*", SearchOption.AllDirectories).ToList().ForEach(x => Console.WriteLine(x));