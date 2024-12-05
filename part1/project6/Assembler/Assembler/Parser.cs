using FluentValidation;
using FluentValidation.Results;

namespace Assembler;

// The main function of the parser is to break each assembly instruction into its underlying components (fields and symbols).
// Encapsulates access to the input code. Reads an assembly language instruction, parses it, and provides convenient access
// to the instruction components (fields and symbols). In addition, removes all white space and comments.

internal class Parser
{
    private string? _currentInstruction;
    private readonly IEnumerator<string?> _enumerator;

    internal Parser(string fileName)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        IEnumerable<string?> fileLines = File.ReadLines(fileName);
        _enumerator = fileLines.GetEnumerator();
        _currentInstruction = null;
    }

    /// <summary>
    /// Makes the next instruction from the input, cleaned of inline comments, the current instruction.
    /// Initially there is no current instruction.
    /// </summary>
    private void Advance()
    {
        if (_enumerator.Current == null) return;
        if (_enumerator.Current.Equals(string.Empty))
        {
            Console.WriteLine("Empty line, moving to the next instruction...");
        }
        else if (_enumerator.Current.StartsWith("//"))
        {
            Console.WriteLine("Comment line, moving to the next instruction...");
        }
        else
        {
            _currentInstruction = _enumerator.Current;
            CleanInstructionOfInlineComment(_currentInstruction);
            Console.WriteLine($"Current instruction: {_currentInstruction}");
            try
            {
                InstructionType instructionType = InstructionType(_currentInstruction);
                Console.WriteLine($"Instruction type: {instructionType}");
            }
            catch (ValidationException e)
            {
                Console.WriteLine("Validation error encountered: ");
                foreach (ValidationFailure? error in e.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}");
                    Console.WriteLine("Stopping execution due to validation error.");
                    Environment.Exit(1);
                }
            }
        }
    }

    /// <summary>
    /// Cleans the instruction of inline comment.
    /// </summary>
    /// <param name="instruction"></param>
    private void CleanInstructionOfInlineComment(string? instruction)
    {
        if (instruction == null) return;
        if (!instruction.Contains("//")) return;
        int firstOccurenceOfComment = instruction.IndexOf('/');
        _currentInstruction = instruction.Remove(firstOccurenceOfComment);
    }

    /// <summary>
    /// Determines the type of the given instruction.
    /// </summary>
    /// <param name="instruction"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <returns>The type of the given instruction.</returns>
    private static InstructionType InstructionType(string instruction)
    {
        if (instruction.StartsWith('@'))
        {
            return new AInstruction(instruction).GetInstructionType();
        }

        if (instruction.Contains('=') || instruction.Contains(';'))
        {
            return new CInstruction(instruction).GetInstructionType();
        }
        
        // TODO: Finish and correct this.
        return new LInstruction(instruction).GetInstructionType();
    }

    /// <summary>
    /// Parses the given file.
    /// </summary>
    internal void Parse()
    {
        while (_enumerator.MoveNext())
        {
            Advance();
        }
        Console.WriteLine("Finished parsing the file.");
    }
}