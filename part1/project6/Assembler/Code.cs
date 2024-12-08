// Translates Hack assembly language mnemonics into binary codes.

namespace Assembler;

/// <summary>
/// Provides methods to translate Hack assembly language mnemonics into binary codes.
/// </summary>
public class Code
{
    /// <summary>
    /// Translates the destination part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The destination mnemonic to translate</param>
    /// <returns>A 3-bit binary string representing the destination</returns>
    public static string Dest(string mnemonic)
    {
        return "3 Bits";
    }

    /// <summary>
    /// Translates the computation part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The computation mnemonic to translate</param>
    /// <returns>A 7-bit binary string representing the computation</returns>
    public static string Comp(string mnemonic)
    {
        return "7 Bits";
    }

    /// <summary>
    /// Translates the jump part of a Hack assembly command into binary.
    /// </summary>
    /// <param name="mnemonic">The jump mnemonic to translate</param>
    /// <returns>A 3-bit binary string representing the jump</returns>
    public static string Jump(string mnemonic)
    {
        return "3 Bits";
    }
}
