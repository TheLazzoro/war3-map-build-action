using CommandLine;

public class ActionInputs
{
    [Option('c', "compile-jass", Required = false, HelpText = "Runs JassHelper on the map")]
    public bool CompileJass { get; set; }
}
