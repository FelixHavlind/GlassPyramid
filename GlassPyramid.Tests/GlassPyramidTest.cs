namespace GlassPyramid.Tests;

public class GlassPyramidTest
{
    private readonly GlassPyramid _glassPyramid = new(5);

    [Theory]
    [InlineData(0, 0, 10.0)]
    [InlineData(1, 0, 30.0)]
    [InlineData(3, 1, 83.333)]
    [InlineData(4, 0, 310.0)]
    public void Simulate_ShouldReturnExpectedDecimal_ForMultipleInputTests(int targetDepth, int targetIndex, decimal expectedResult)
    {
        // ACT
        var actualResult = _glassPyramid.Simulate(targetDepth, targetIndex);
        
        // ASSERT
        Assert.Equal(expectedResult, actualResult, precision: 3);
    }
}