using FluentValidation;

namespace Assembler;

public partial class CInstructionValidator : AbstractValidator<CInstruction>
{
public CInstructionValidator()
    {
        RuleFor(instruction => instruction.Instruction)
            .NotEmpty().WithMessage("Command cannot be empty.")
            .Must(BeAValidCInstruction).WithMessage("Invalid C-instruction format.");
    }

private static bool BeAValidCInstruction(string command)
{
    return MyRegex().IsMatch(command);
}

    // Regex pattern for a C-instruction: dest=comp;jump
    // dest and jump can be empty, but if dest is empty, "=" is omitted;
    // if jump is empty, ";" is omitted.
    [System.Text.RegularExpressions.GeneratedRegex(
        @"^(?:[AMD]=)?(?:[01AMD]|[AMD][+\-!&|][01AMD])(?:;J(?:GT|EQ|GE|LT|NE|LE|MP))?$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}