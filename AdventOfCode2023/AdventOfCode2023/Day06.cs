namespace AdventOfCode2023;

public class Day06
{
    private readonly string[] _lines;
    private readonly IList<int> _times;
    private readonly IList<int> _distances;

    public Day06(string[] lines)
    {
        _lines = lines;

        _times = _lines[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
        _distances = _lines[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
    }

    public int Solve_A()
    {
        var cumSum = 1;
        
        for (var i = 0; i < _times.Count; i++)
        {
            var time = _times[i];
            var distance = _distances[i];

            var limits = ApplyQuadraticFormula(-1 * time, distance);

            var minHoldTime = (int) Math.Round(limits.min, MidpointRounding.ToPositiveInfinity);
            var maxHoldTime = (int) Math.Round(limits.max, MidpointRounding.ToNegativeInfinity);

            cumSum *= maxHoldTime - minHoldTime + 1;
        }

        return cumSum;
    }

    private (double min, double max) ApplyQuadraticFormula(int b, int c)
    {
        var adjustedC = c + 0.1;
        
        var sqrt = Math.Sqrt((b * b) - (4 * adjustedC));

        return new(((-1 * b - sqrt) / 2), ((-1 * b + sqrt) / 2));
    }

}