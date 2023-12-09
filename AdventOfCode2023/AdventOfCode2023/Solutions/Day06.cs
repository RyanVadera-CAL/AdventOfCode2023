namespace AdventOfCode2023;

public class Day06
{
    private readonly string[] _lines;

    public Day06(string[] lines)
    {
        _lines = lines;
    }

    public int Solve_A()
    {
        var times = _lines[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
        var distances = _lines[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

        var cumSum = 1;
        
        for (var i = 0; i < times.Count; i++)
        {
            var time = times[i];
            var distance = distances[i];

            var limits = ApplyQuadraticFormula(-1 * time, distance);

            var minHoldTime = (int) Math.Round(limits.min, MidpointRounding.ToPositiveInfinity);
            var maxHoldTime = (int) Math.Round(limits.max, MidpointRounding.ToNegativeInfinity);

            cumSum *= maxHoldTime - minHoldTime + 1;
        }

        return cumSum;
    }

    public int Solve_B()
    {
        var time = long.Parse(
            _lines[0]
            .Split(':')[1]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Aggregate("", (current, x) => current + x));
        
        var distance = long.Parse(
            _lines[1]
                .Split(':')[1]
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Aggregate("", (current, x) => current + x));
        
        var limits = ApplyQuadraticFormula(-1 * time, distance);

        var minHoldTime = (int) Math.Round(limits.min, MidpointRounding.ToPositiveInfinity);
        var maxHoldTime = (int) Math.Round(limits.max, MidpointRounding.ToNegativeInfinity);

        return maxHoldTime - minHoldTime + 1;
    }

    private (double min, double max) ApplyQuadraticFormula(long b, long c)
    {
        var adjustedC = c + 0.1;
        
        var sqrt = Math.Sqrt((b * b) - (4 * adjustedC));

        return new(((-1 * b - sqrt) / 2), ((-1 * b + sqrt) / 2));
    }

}