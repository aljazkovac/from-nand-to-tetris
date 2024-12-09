namespace Assembler;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Assembler <file.asm>");
            return;
        }
        string file = args[0];
        var parser = new Parser(file);
        while (parser.HasMoreCommands())
        {
            parser.Advance();
            if (parser.CurrentCommand != null)
            {
                Console.WriteLine(parser.CurrentCommand + " == " + Parser.DetermineInstructionType(parser.CurrentCommand));

            }
            else
            {
                Console.WriteLine("Current command is null.");
            }
        }
    }
}