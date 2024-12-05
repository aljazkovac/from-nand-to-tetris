namespace Assembler;

// C-instruction: dest=comp;jump
// Either the dest or jump fields may be empty.
// If dest is empty, the "=" is omitted;
// If jump is empty, the ";" is omitted.
public class CInstruction(string instruction)
{
    public string Instruction { get; } = instruction;
    
    public InstructionType GetInstructionType()
    {
        return InstructionType.CInstruction;
    }

    public string GetDestination()
    {
        return Instruction[1..2];
    }
}