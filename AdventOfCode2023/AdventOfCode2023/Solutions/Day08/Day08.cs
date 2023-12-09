namespace AdventOfCode2023.Solutions.Day08;

public class Day08 : ISolveProblems
{
    public long Solve(string[] lines)
    {
        var instructions = lines[0];

        var network = new Network(lines);

        return network.NumStepsToTraverseAsGhost(instructions);
    }
}