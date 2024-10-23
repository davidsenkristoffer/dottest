using BenchmarkDotNet.Attributes;

namespace Ratelimiting;

public static class StringUtils
{
    public static ReadOnlySpan<char> SubstringOp(this string input, int start, int end) 
        => SubstringOp(input.AsSpan(), start, end);

    public static ReadOnlySpan<char> SubstringOp(this ReadOnlySpan<char> input, int start, int end)
    {
        if (start >= input.Length - 1) return input;
        if (end < start) return input;
        
        return input[start..end];
    }

    public static string Substring(this string input, int start, int end)
    {
        if (start >= input.Length - 1) return input;
        if (end < start) return input;
        
        return input[start..end];
    }
}