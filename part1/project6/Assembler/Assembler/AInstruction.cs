namespace Assembler;

// A-instruction: @value 
// Where value is either a non-negative decimal number or a symbol referring to such number.
public class AInstruction(string instruction)
{
    public string Instruction { get; } = instruction;

    public InstructionType GetInstructionType()
    {
        return InstructionType.AInstruction;
    }

    public string GetSymbol()
    {
        return Instruction[1..];
    }
}

