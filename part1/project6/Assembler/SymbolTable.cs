namespace Assembler;

/// <summary>
/// Keeps a correspondence between symbolic labels and numeric addresses.
/// </summary>
public class SymbolTable
{
    private static Dictionary<string, int> _symbolTable = new()
    {
        { "SP", 0 },
        { "LCL", 1 },
        { "ARG", 2 },
        { "THIS", 3 },
        { "THAT", 4 },
        { "R0", 0 },
        { "R1", 1 },
        { "R2", 2 },
        { "R3", 3 },
        { "R4", 4 },
        { "R5", 5 },
        { "R6", 6 },
        { "R7", 7 },
        { "R8", 8 },
        { "R9", 9 },
        { "R10", 10 },
        { "R11", 11 },
        { "R12", 12 },
        { "R13", 13 },
        { "R14", 14 },
        { "R15", 15 },
        { "SCREEN", 16384 },
        { "KBD", 24576 }
    };
    
    /// <summary>
    /// Adds the pair (symbol, address) to the table.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="address"></param>
    public static void AddEntry(string symbol, int address)
    {
        _symbolTable.Add(symbol, address);
    }

    /// <summary>
    /// Does the symbol table contain the given symbol?
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public static bool Contains(string symbol)
    {
        return _symbolTable.ContainsKey(symbol);
    }

    /// <summary>
    /// Returns the address associated with the symbol.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public static int GetAddress(string symbol)
    {
        return _symbolTable.GetValueOrDefault(symbol);
    }
}