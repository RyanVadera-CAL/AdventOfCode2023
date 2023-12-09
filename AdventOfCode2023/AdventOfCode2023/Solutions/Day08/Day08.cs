namespace AdventOfCode2023.Solutions.Day08;

public class Day08 : ISolveProblems
{
    public int Solve(string[] lines)
    {
        var instructions = lines[0];

        var network = new Network(lines);

        return network.NumStepsToTraverse(instructions, "AAA", "ZZZ");
    }
}