using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Ratelimiting;

[MemoryDiagnoser(false)]
public class Benchmarkable
{
    private string input;

    public Benchmarkable() => input = "Hello, World";

    [Benchmark]
    public string SubstringT() => StringUtils.Substring(input, 10, 12);

    [Benchmark]
    public ReadOnlySpan<char> SubstringOpT() => StringUtils.SubstringOp(input.AsSpan(), 10, 12);
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Benchmarkable>();
        // var b = new Benchmarkable();
        // Console.WriteLine(b.SubstringT());
        // Console.WriteLine(b.SubstringOpT().ToString());
    }
}
