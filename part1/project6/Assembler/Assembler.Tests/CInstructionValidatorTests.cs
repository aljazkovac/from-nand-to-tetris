using FluentValidation.TestHelper;

namespace Assembler.Tests;

public class CInstructionValidatorTests
{
    private readonly CInstructionValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Empty()
    {
        var instruction = new CInstruction("");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }

    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid()
    {
        var instruction = new CInstruction("D=A+1;");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_2()
    {
        var instruction = new CInstruction("M=D-;");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_3()
    {
        var instruction = new CInstruction("M=D-");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_4()
    {
        var instruction = new CInstruction("M=D-A+3;");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_5()
    {
        var instruction = new CInstruction("0JMP");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Have_Error_When_Instruction_Is_Invalid_6()
    {
        var instruction = new CInstruction("0=3");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldHaveValidationErrorFor(i => i.Instruction);
    }
    
    [Fact]
    public void Should_Not_Have_Error_When_Instruction_Is_Valid()
    {
        var instruction = new CInstruction("D=M");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldNotHaveValidationErrorFor(i => i.Instruction);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Instruction_Is_Valid_2()
    {
        var instruction = new CInstruction("0;JMP");
        TestValidationResult<CInstruction>? result = _validator.TestValidate(instruction);
        result.ShouldNotHaveValidationErrorFor(i => i.Instruction);
    }
}
