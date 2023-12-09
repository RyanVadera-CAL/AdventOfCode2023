using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public static class Day02
{
    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    private static readonly Regex Regex =
        new (@"Game (?<Game>\d+): (?<Pull>(((?<blue>\d+) blue)|((?<green>\d+) green)|((?<red>\d+) red)){1}[,; ]*)+");
    
    public static int Solve_A(string input)
    {
        var res = Regex.Matches(input);
        var cumSum = 0;

        foreach (var row in res.AsQueryable())
        {
            var gameNum = int.Parse(row.Groups["Game"].Value);

            var maxRed = row.Groups["red"].Captures.Select(c => int.Parse(c.Value)).Max();
            var maxGreen = row.Groups["green"].Captures.Select(c => int.Parse(c.Value)).Max();
            var maxBlue = row.Groups["blue"].Captures.Select(c => int.Parse(c.Value)).Max();

            if (maxRed <= MaxRed && maxGreen <= MaxGreen && maxBlue <= MaxBlue)
            {
                cumSum += gameNum;
            }
        }
        
        return cumSum;
    }

    public static int Solve_B(string input)
    {
        var res = Regex.Matches(input);
        var cumSum = 0;

        foreach (var row in res.AsQueryable())
        {
            var maxRed = row.Groups["red"].Captures.Select(c => int.Parse(c.Value)).Max();
            var maxGreen = row.Groups["green"].Captures.Select(c => int.Parse(c.Value)).Max();
            var maxBlue = row.Groups["blue"].Captures.Select(c => int.Parse(c.Value)).Max();

            var gamePower = maxRed * maxGreen * maxBlue;

            cumSum += gamePower;
        }
        
        return cumSum;
    }
}