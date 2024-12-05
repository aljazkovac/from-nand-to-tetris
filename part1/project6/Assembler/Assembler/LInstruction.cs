namespace Assembler;

// TODO: Write comment about this code.
public class LInstruction(string instruction)
{
    public string Instruction { get; } = instruction;
    
    public InstructionType GetInstructionType()
    {
        return InstructionType.LInstruction;
    }

    public string GetSymbol()
    {
        return Instruction[1..];
    }
}