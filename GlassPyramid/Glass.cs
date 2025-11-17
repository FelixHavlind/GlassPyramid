namespace GlassPyramid;

public class Glass(int depth, int index, Glass? subLeftGlass, Glass? subRightGlass)
{
    public Glass(Glass? subLeftGlass, Glass? subRightGlass) : this(-1, -1, subLeftGlass, subRightGlass)
    {}

    public const decimal Capacity = 1.0m;

    public bool Full { get; set; }
    public decimal Volume { get; set; }
    public decimal Inflow { get; set; }
    
    public int Depth { get; init; } = depth;
    public int Index { get; init; } = index;
    public Glass? SubLeftGlass { get; set; } = subLeftGlass;
    public Glass? SubRightGlass { get; set; } = subRightGlass;
}