namespace GlassPyramid;

internal static class Program
{
    private static void Main(string[] args)
    {
        var glassPyramid = new GlassPyramid(15);
        
        Console.WriteLine(glassPyramid.Simulate(3, 1));
    }
}