namespace GlassPyramid;

internal static class Program
{
    private static void Main(string[] args)
    {
        var glassPyramid = new GlassPyramid(50);
        
        Console.WriteLine(glassPyramid.Simulate(4, 3));
    }
}