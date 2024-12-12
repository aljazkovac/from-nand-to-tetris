// Translates Hack assembly language mnemonics into binary codes.

namespace Assembler;

/// <summary>
/// Provides methods to translate Hack assembly language mnemonics into binary codes.
/// </summary>
public static class Code
{
    private static readonly Dictionary<string, string> CompMnemonicsBits = new()
    {
        { "0", "0101010" },
        { "1", "0111111" },
        { "-1", "0111010" },
        { "D", "0001100" },
        { "A", "0110000" },
        { "M", "1110000" },
        { "!D", "0001101" },
        { "!A", "0110001" },
        { "!M", "1110001" },
        { "-D", "001111" },
        { "-A", "0001111" },
        { "-M", "1001111" },
        { "D+1", "0011111" },
        { "A+1", "0110111" },
        { "M+1", "1110111" },
        { "D-1", "0001110" },
        { "A-1", "0110010" },
        { "M-1", "1110010" },
        { "D+A", "0000010" },
        { "D+M", "1000010" },
        { "D-A", "0010011" },
        { "D-M", "1010011" },
        { "A-D", "0000111" },
        { "M-D", "1000111" },
        { "D&A", "0000000" },
        { "D&M", "1000000" },
        { "D|A", "0010101" },
        { "D|M", "1010101" },
    };
    private static readonly Dictionary<string, string> DestMnemonicsBits = new()
    {
        { "empty", "000" },
        { "M", "001" },
        { "D", "010" },
        { "MD", "011" },
        { "A", "100" },
        { "AM", "101" },
        { "AD", "110" },
        { "AMD", "111" }
    };

    private static readonly Dictionary<string, string> JumpMnemonicsBits = new()
    {
        { "empty", "000" },
        { "JGT", "001" },
        { "JEQ", "010" },
        { "JGE", "011" },
        { "JLT", "100" },
        { "JNE", "101" },
        { "JLE", "110" },
        { "JMP", "111" }
    };
    /// <summary>
    /// Translates the destination part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The destination mnemonic to translate</param>
    /// <returns>A 3-bit binary string representing the destination</returns>
    public static string? Dest(string mnemonic)
    {
        string? translation = DestMnemonicsBits.GetValueOrDefault(mnemonic);
        return translation;
    }

    /// <summary>
    /// Translates the computation part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The computation mnemonic to translate</param>
    /// <returns>A 7-bit binary string representing the computation</returns>
    public static string? Comp(string mnemonic)
    {
        string? translation = CompMnemonicsBits.GetValueOrDefault(mnemonic);
        return translation;
    }

    /// <summary>
    /// Translates the jump part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The jump mnemonic to translate</param>
    /// <returns>A 3-bit binary string representing the jump</returns>
    public static string? Jump(string mnemonic)
    {
        string? translation = JumpMnemonicsBits.GetValueOrDefault(mnemonic);
        return translation;
    }
}