using FluentValidation;

namespace Assembler;

// TODO: Fix this
public class LInstructionValidator : AbstractValidator<LInstruction>
{
    public LInstructionValidator()
    {
        RuleFor(instruction => instruction.Instruction)
            .NotEmpty().WithMessage("Command cannot be empty.");
    }
}