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
        
        // Load the file
        string file = args[0];
        var parser = new Parser(file);
        
        // The first pass (build the symbol table)
        var romAddress = 0;
        while (parser.HasMoreCommands())
        {
            parser.Advance();
            if (parser.CurrentCommand == null)
            {
                Console.WriteLine("Current command is null.");
            }
            else
            {
                if (Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.LInstruction))
                {
                    SymbolTable.AddEntry(
                        parser.Symbol() ?? throw new InvalidOperationException("Symbol cannot be null."), romAddress);
                }
                if (Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.AInstruction) ||
                    Parser.DetermineInstructionType(parser.CurrentCommand).Equals(InstructionType.CInstruction))
                {
                    romAddress++;
                }
            }
        }
        
        // The second pass (translate A-instructions and C-instructions, fetch L-instructions from the symbol table,
        // and handle variables)
        var lines = new List<string>();
        var variableAddress = 16;
        // TODO: Probably need to reset the parser here, or create a new one?
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
                    try
                    {
                        var number = Convert.ToInt32(parser.Symbol());
                        const string firstBit = "0";
                        string bits = Convert.ToString(number, 2).PadLeft(15, '0');
                        string translation = firstBit + bits;
                        Console.WriteLine("Machine code for numeric A-instruction " + parser.CurrentCommand + " : " + translation);
                        lines.Add(translation);
                    }
                    // This means that the A-instruction is either a symbolic A-instruction or a variable
                    catch (FormatException)
                    {
                        string? symbol = parser.Symbol();
                        // The A-instruction is a symbolic A-instruction
                        if (symbol != null && SymbolTable.Contains(symbol))
                        {
                            int symbolValue = SymbolTable.GetAddress(symbol);
                            // Translate the symbol value
                            const string firstBit = "0";
                            string bits = Convert.ToString(symbolValue, 2).PadLeft(15, '0');
                            string translation = firstBit + bits;
                            Console.WriteLine("Machine code for symbolic A-instruction " + parser.CurrentCommand + " : " + translation);
                            lines.Add(translation);
                        }
                        else
                        // The A-instruction is a variable
                        {
                            string? variable = parser.Symbol();
                            SymbolTable.AddEntry(variable ?? throw new InvalidOperationException("Symbol cannot be null."), variableAddress);
                            variableAddress++;
                            const string firstBit = "0";
                            string bits = Convert.ToString(variableAddress, 2).PadLeft(15, '0');
                            string translation = firstBit + bits;
                            Console.WriteLine("Machine code for variable A-instruction " + parser.CurrentCommand + " : " + translation);
                            lines.Add(translation);
                        }
                    }
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