// The Hack assembler reads as input a text file named Prog.asm, containing a Hack
// assembly program, and produces as output a text file named Prog.hack, containing the translated Hack machine code.
// The name of the input file is supplied to the assembler as a command line argument:
// prompt> dotnet run Prog.asm

namespace Assembler;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No file provided");
            return;
        }

        string fileName = args[0];
        Parser parser = new(fileName);
        parser.Parse();
    }
}