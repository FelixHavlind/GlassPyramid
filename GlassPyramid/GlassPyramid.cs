using System.Text;

namespace GlassPyramid;

public class GlassPyramid
{
    private readonly int _maxDepth;
    private readonly Glass[][] _glasses;

    public GlassPyramid(int maxDepth)
    {
        _maxDepth = maxDepth;
        _glasses = new Glass[maxDepth][];

        for (var depth = 0; depth < _maxDepth; depth++)
        {
            _glasses[depth] = new Glass[depth + 1];

            for (var index = 0; index < _glasses[depth].Length; index++)
                _glasses[depth][index] = new Glass();
        }
    }

    public decimal Simulate(int targetDepth, int targetIndex)
    {
        var counter = 0.0m;
        var topGlass = _glasses[0][0];
        var targetGlass = _glasses[targetDepth][targetIndex];

        while (!targetGlass.Full)
        {
            topGlass.Inflow = 0.1m;

            for (var depth = 0; depth < _maxDepth; depth++)
            {
                for (var index = 0; index < _glasses[depth].Length; index++)
                {
                    var glass = _glasses[depth][index];

                    if (glass.Full && depth < _maxDepth - 1)
                    {
                        _glasses[depth + 1][index].Inflow += glass.Inflow * 0.5m;
                        _glasses[depth + 1][index + 1].Inflow += glass.Inflow * 0.5m;
                    }

                    else
                    {
                        glass.Volume += glass.Inflow;

                        if (Glass.Capacity <= glass.Volume)
                        {
                            glass.Full = true;

                            if (Glass.Capacity < glass.Volume)
                            {
                                var overflow = Glass.Capacity - glass.Volume;

                                if (glass == targetGlass) ;
                                    // TODO: Implement counter offset.

                                if (depth < _maxDepth - 1)
                                {   
                                    _glasses[depth + 1][index].Inflow += overflow * 0.5m;
                                    _glasses[depth + 1][index + 1].Inflow += overflow * 0.5m;
                                }
                            }
                        } 
                    }
                    
                    glass.Inflow = 0.0m;
                }
            }
            
            ++counter;
            Print();
        }

        return counter;
    }

    private void Print()
    {
        var stringBuilder = new StringBuilder();

        for (var depth = 0; depth < _maxDepth; depth++)
        {
            stringBuilder.Append(string.Concat(Enumerable.Repeat("     ", _maxDepth - depth - 1)));
            
            for (var index = 0; index < _glasses[depth].Length; index++)
                stringBuilder.Append('[').Append((_glasses[depth][index].Volume * 100).ToString("000.0000")).Append(']');
            
            stringBuilder.AppendLine();
        }
        
        Console.WriteLine(stringBuilder.ToString());
    }
}