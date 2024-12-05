using FluentValidation;

namespace Assembler;

// A-instruction: @value 
// Where value is either a non-negative decimal number or a symbol referring to such number.
// TODO: Add validation for symbol referring to such number.

public class AInstructionValidator : AbstractValidator<AInstruction>
{
    public AInstructionValidator()
    {
        RuleFor(instruction => instruction.Instruction).NotEmpty().WithMessage("A-instruction cannot be empty.");
        RuleFor(instruction => instruction.Instruction).Matches(@"^@\d+$").WithMessage("A-instruction must start with '@' followed by a non-negative decimal number.");
    }
}

