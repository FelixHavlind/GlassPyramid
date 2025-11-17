using System.Text;

namespace GlassPyramid;

public class GlassPyramid(int maxDepth)
{
    private int _targetDepth;
    private int _targetIndex;
    
    public decimal Simulate(int targetDepth, int targetIndex) 
    {
        _targetDepth = targetDepth;
        _targetIndex = targetIndex;
        var glasses = new Glass[maxDepth][];

        for (var depth = 0; depth < maxDepth; depth++)
        {
            glasses[depth] = new Glass[depth + 1];

            for (var index = 0; index < glasses[depth].Length; index++)
                glasses[depth][index] = new Glass(null, null);
        }
        
        var counter = 0.0m;
        var topGlass = glasses[0][0];
        var targetGlass = glasses[targetDepth][targetIndex];

        while (!targetGlass.Full) 
        {
            topGlass.Inflow = 0.1m;

            for (var depth = 0; depth < maxDepth; depth++)
            {
                for (var index = 0; index < glasses[depth].Length; index++)
                {
                    var glass = glasses[depth][index];

                    if (glass.Full)
                    {
                        if (depth < maxDepth - 1)
                        {
                            glasses[depth + 1][index].Inflow += glass.Inflow * 0.5m;
                            glasses[depth + 1][index + 1].Inflow += glass.Inflow * 0.5m;
                        }

                        else
                            glass.Inflow = 0.0m;
                    }

                    else
                    {
                        glass.Volume += glass.Inflow;

                        if (Glass.Capacity <= glass.Volume)
                        {
                            glass.Full = true;

                            if (Glass.Capacity < glass.Volume)
                            {
                                var overflow = glass.Volume - Glass.Capacity;

                                if (glass == targetGlass)
                                    counter -= 1 * (overflow / glass.Inflow);
                                
                                if (depth < maxDepth - 1)
                                {   
                                    glasses[depth + 1][index].Inflow += overflow * 0.5m;
                                    glasses[depth + 1][index + 1].Inflow += overflow * 0.5m;
                                }

                                glass.Volume = Glass.Capacity;
                            }
                        } 
                    }
                    
                    glass.Inflow = 0.0m;
                }
            }
            
            ++counter;
            // Print(glasses);
        }

        return counter;
    }
    
    public decimal Calculate(int targetDepth, int targetIndex)
    {
        var topGlass = InitCalculate(targetDepth, targetIndex);
        
        Console.WriteLine(topGlass.Depth + ", " + topGlass.Index);

        throw new NotImplementedException();
    }

    private static Glass InitCalculate(int targetDepth, int targetIndex)
    {
        var glassQueue = new Queue<Glass>([new Glass(targetDepth, targetIndex, null, null)]);

        while (0 < glassQueue.Count)
        {
            var glass = glassQueue.Dequeue();
            
            if (glass is { Depth: 0, Index: 0 })
            {
                return glass;
            }
            
            var parentDepth = glass.Depth - 1;
            var parentLeftIndex = glass.Index - 1;
            var parentRightIndex = glass.Index;

            if (0 <= parentLeftIndex && parentRightIndex <= parentDepth)
            {
                if (0 == parentLeftIndex)
                {
                    glassQueue.Enqueue(new Glass(parentDepth, parentLeftIndex, null, glass));
                }

                else if (parentRightIndex == parentDepth)
                {
                    glassQueue.Enqueue(new Glass(parentDepth, parentRightIndex, glass, null));   
                }

                else
                {
                    
                }   
            }
        }

        throw new InvalidOperationException("This should not be possible");
    }

    private void Print(Glass[][] glasses)
    {
        var stringBuilder = new StringBuilder();

        for (var depth = 0; depth < maxDepth; depth++)
        {
            stringBuilder.Append(string.Concat(Enumerable.Repeat("     ", maxDepth - depth - 1)));
            
            for (var index = 0; index < glasses[depth].Length; index++)
                stringBuilder.Append('[').Append((glasses[depth][index].Volume * 100).ToString("000.0000")).Append(']');
            
            stringBuilder.AppendLine();
        }
        
        Console.WriteLine(stringBuilder.ToString());
    }

    private bool HasSingleChild(Glass glass)
    {
        var targetDepth = _targetDepth;
        var targetIndex = _targetIndex;

        while (targetIndex <= targetDepth)
        {
            if (glass.Depth == targetDepth && glass.Index == targetIndex)
            {
                return true;
            }

            targetDepth--;
        }

        targetDepth = _targetDepth;

        while (0 <= targetIndex)
        {
            if (glass.Depth == targetDepth && glass.Index == targetIndex)
            {
                return true;
            }

            targetDepth--;
            targetIndex--;
        }

        return false;
    }
}