using FluentValidation.TestHelper;

namespace Assembler.Tests;

public class AInstructionValidatorTests
{
    private readonly AInstructionValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Empty()
    {
        var instruction = new AInstruction("");
        TestValidationResult<AInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }

    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid()
    {
        var instruction = new AInstruction("@abc");
        TestValidationResult<AInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_2()
    {
        var instruction = new AInstruction("@100a");
        TestValidationResult<AInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_3()
    {
        var instruction = new AInstruction("@100@");
        TestValidationResult<AInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Instruction_Is_Valid()
    {
        var instruction = new AInstruction("@123");
        TestValidationResult<AInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldNotHaveValidationErrorFor(i => i.Instruction);
    }
}