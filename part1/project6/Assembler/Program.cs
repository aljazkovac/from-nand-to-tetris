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
            if (parser.CurrentCommand == null)
            {
                Console.WriteLine("Current command is null.");
            }
            else
            {
                Console.WriteLine(parser.CurrentCommand + " == " + Parser.DetermineInstructionType(parser.CurrentCommand));

                if (!Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.CInstruction))
                {
                    continue;
                }

                Console.WriteLine("Dest part: " + parser.Dest());
                Console.WriteLine("Comp part: " + parser.Comp());
                Console.WriteLine("Jump part: " + parser.Jump());
            }
        }
    }
}