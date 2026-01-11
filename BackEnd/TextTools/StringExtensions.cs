namespace PomoSyncAPI.BackEnd.TextTools;

public static class StringExtensions
{
    public static bool IsSame(this string? a, string? b)
    {
        return a?.ToLower() == b?.ToLower();
    }
}