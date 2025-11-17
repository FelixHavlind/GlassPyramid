namespace GlassPyramid;

internal static class Program
{
    private static void Main(string[] args)
    {
        var glassPyramid = new GlassPyramid(5);
        Console.WriteLine(glassPyramid.Calculate(4, 3));
    }
}