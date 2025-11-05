namespace GlassPyramid;

public class Glass
{
    public static readonly decimal Capacity = 1.0m;
    
    public bool Full { get; set; }
    public decimal Volume { get; set; }
    public decimal Inflow { get; set; }
}