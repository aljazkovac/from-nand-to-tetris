// Encapsulates access to the input code. Reads an assembly language command, parses it, and provides convenient access
// to the command's components (fields and symbols). In addition, removes all white space and comments.

namespace Assembler;

/// <summary>
/// Represents a parser for Hack assembly language.
/// </summary>
/// <param name="file">The path to the input file containing Hack assembly code.</param>
public class Parser(string file)
{
    private readonly StreamReader _reader = new(file);
    
    /// <summary>
    /// The current command being processed from the input file.
    /// </summary>
    public string? CurrentCommand;
    
    /// <summary>
    /// Reads the next command from the input and makes it the current command. Should be called only
    /// if hasMoreCommands() is true. Initially there is no current command.
    /// </summary>
    public void Advance()
    {
        string? line = _reader.ReadLine();
        while (line is not null)
        {
            if (line.StartsWith("//") || string.IsNullOrEmpty(line))
            {
                line = _reader.ReadLine();
            }
            else
            {
                CurrentCommand = line;
                return;
            }
        }
    }

    /// <summary>
    /// Returns the comp mnemonic in the current C-command (28 possibilities).
    /// Should be called only when <see cref="InstructionType"/> is <see cref="InstructionType.CInstruction"/>.
    /// </summary>
    /// <returns>
    /// A string representing the dest mnemonic in the current C-command.
    /// Returns <c>null</c> if the command type is not <see cref="InstructionType.CInstruction"/>.
    /// </returns>
    public string? Comp()
    {
        if (CurrentCommand == null)
        {
            throw new ArgumentNullException(nameof(CurrentCommand), "Current command is null");
        }
        
        if (!DetermineInstructionType(CurrentCommand).Equals(InstructionType.CInstruction))
        {
            return null;
        }

        // Possible formats:
        // 0 (both dest and jump are empty)
        // M=D (jump is empty)
        // 0;JMP (dest is empty)
        // M=D;JGT (all parts present)

        if (Dest() == "empty" && Jump() == "empty")
        {
            return CurrentCommand.Trim();
        }

        if (Jump() == "empty")
        {
            return CurrentCommand.Split('=').Last().Trim();
        }

        if (Dest() == "empty")
        {
            return CurrentCommand.Split(';').First().Trim();
        }

        char[] delimiterChars = [';', '='];
        return CurrentCommand.Split(delimiterChars)[1].Trim();

    }

    /// <summary>
    /// Returns the dest mnemonic in the current C-command (8 possibilities).
    /// Should be called only when <see cref="InstructionType"/> is <see cref="InstructionType.CInstruction"/>.
    /// </summary>
    /// <returns>
    /// A string representing the dest mnemonic in the current C-command.
    /// Returns <c>null</c> if the command type is not <see cref="InstructionType.CInstruction"/>.
    /// </returns>
    public string? Dest()
    {
        if (CurrentCommand == null)
        {
            throw new ArgumentNullException(nameof(CurrentCommand), "Current command is null");
        }

        if (!DetermineInstructionType(CurrentCommand).Equals(InstructionType.CInstruction))
        {
            return null;
        }
        
        return !CurrentCommand.Contains('=') ? "empty" : CurrentCommand.Split('=').First().Trim();
    }
    
    /// <summary>
    /// Returns the jump mnemonic in the current C-command (8 possibilities).
    /// Should be called only when <see cref="InstructionType"/> is <see cref="InstructionType.CInstruction"/>.
    /// </summary>
    /// <returns>
    /// A string representing the dest mnemonic in the current C-command.
    /// Returns <c>null</c> if the command type is not <see cref="InstructionType.CInstruction"/>.
    /// </returns>
    public string? Jump()
    {
        if (CurrentCommand == null)
        {
            throw new ArgumentNullException(nameof(CurrentCommand), "Current command is null");
        }

        if (!DetermineInstructionType(CurrentCommand).Equals(InstructionType.CInstruction))
        {
            return null;
        }

        return !CurrentCommand.Contains(';') ? "empty" : CurrentCommand.Split(';').Last().Trim();
    }

    /// <summary>
    /// Returns the type of the current instruction.
    /// </summary>
    /// <returns>
    /// One of the following <see cref="InstructionType"/> values:
    /// <list type="bullet">
    /// <item>
    /// <description><see cref="InstructionType.AInstruction"/>: Represents an A-instruction (e.g., @Xxx).</description>
    /// </item>
    /// <item>
    /// <description><see cref="InstructionType.CInstruction"/>: Represents a C-instruction (e.g., dest=comp;jump).</description>
    /// </item>
    /// <item>
    /// <description><see cref="InstructionType.LInstruction"/>: Represents a pseudocommand (e.g., (Xxx)).</description>
    /// </item>
    /// </list>
    /// </returns>
    
    public static InstructionType DetermineInstructionType(string instruction)
    {
        if (instruction.StartsWith('@'))
        {
            return InstructionType.AInstruction;
        }
    
        if (instruction.StartsWith('(') && instruction.EndsWith(')'))
        {
            return InstructionType.LInstruction;
        }
    
        if (instruction.Contains('=') || instruction.Contains(';'))
        {
            return InstructionType.CInstruction;
        }
    
        throw new InvalidOperationException($"Invalid instruction format: {instruction}");
    }

    /// <summary>
    /// Retrieves the symbol or decimal value (Xxx) of the current command, such as @Xxx or (Xxx).
    /// Should be called only when <see cref="InstructionType"/> is <see cref="InstructionType.AInstruction"/> or <see cref="InstructionType.LInstruction"/>.
    /// </summary>
    /// <returns>
    /// A string representing the symbol or decimal value of the current command.
    /// Returns <c>null</c> if the command type is not <see cref="InstructionType.AInstruction"/> or <see cref="InstructionType.LInstruction"/>.
    /// </returns>
    public string? Symbol()
    {
        if (CurrentCommand == null)
        {
            throw new ArgumentNullException(nameof(CurrentCommand), "Current command is null");
        }

        if (DetermineInstructionType(CurrentCommand).Equals(InstructionType.AInstruction))
        {
            return CurrentCommand[1..];
        }

        if (DetermineInstructionType(CurrentCommand).Equals(InstructionType.LInstruction))
        {
            return CurrentCommand.TrimStart('(').TrimEnd(')').Trim();
        }

        return null;
    }

    /// <summary>
    /// Determines if there are more commands in the input file to process.
    /// </summary>
    /// <returns>
    /// <c>true</c> if there are more commands to process; otherwise, <c>false</c>.
    /// </returns>
    public bool HasMoreCommands()
    {
        return _reader.Peek() != -1;
    }
}

/// <summary>
/// Represents the type of instruction in Hack assembly language.
/// </summary>
public enum InstructionType
{
    /// <summary>
    /// Represents an A-instruction, which is used to set the A register to a specific value.
    /// This can be a constant value or a symbol that will be resolved to a memory address.
    /// The form of the instruction is @Xxx, where Xxx is a non-negative decimal number or a symbol.
    /// </summary>
    AInstruction,
    /// <summary>
    /// Represents a C-instruction.
    /// The form of the instruction is dest=comp;jump.
    /// Either the dest or the jump fields may be empty.
    /// If dest is empty, the "=" is omitted.
    /// If jump is empty, the semicolon is omitted.
    /// </summary>
    CInstruction,
    /// <summary>
    /// Represents a pseudocommand.
    /// The form of the instruction is (Xxx), where Xxx is a symbol.
    /// </summary>
    LInstruction
}