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
        var lines = new List<string>();
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

                if (Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.CInstruction))
                {
                    const string firstBits = "111";
                    string? compBits = Code.Comp(parser.Comp() ?? throw new InvalidOperationException());
                    string? destBits = Code.Dest(parser.Dest() ?? throw new InvalidOperationException());
                    string? jumpBits = Code.Jump(parser.Jump() ?? throw new InvalidOperationException());
                    string translation = firstBits + compBits + destBits + jumpBits;
                    Console.WriteLine("Machine code for C-instruction " + parser.CurrentCommand + " : " + translation);
                    lines.Add(translation);
                }

                if (Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.AInstruction))
                {
                    const string firstBit = "0";
                    var integerValue = Convert.ToInt32(parser.Symbol());
                    string bits = Convert.ToString(integerValue, 2).PadLeft(15, '0');
                    string translation = firstBit + bits;
                    Console.WriteLine("Machine code for A-instruction " + parser.CurrentCommand + " : " + translation);
                    lines.Add(translation);
                }
            }
        }

        string docPath = Environment.CurrentDirectory;
        string? fileNameWithoutType = file.Split('.').FirstOrDefault();
        using var outputFile = new StreamWriter(Path.Combine(docPath, fileNameWithoutType + ".hack"));
        foreach (string line in lines)
            outputFile.WriteLine(line);
    }
}