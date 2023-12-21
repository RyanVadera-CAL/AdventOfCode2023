namespace AdventOfCode2023.Solutions.Day11;

public class Day11 : ISolveProblems
{
    public long Solve(string[] lines)
    {
        var galaxyMap = new GalaxyMap(lines);

        return galaxyMap.FindAllDistances();
    }
}