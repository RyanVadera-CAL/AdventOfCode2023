namespace AdventOfCode2023.Solutions.Day09;

public class Day09 : ISolveProblems
{
    public long Solve(string[] lines)
    {
        var cumSum = 0;
        
        foreach (var line in lines)
        {
            var sequence = new Sequence(line);
            cumSum += sequence.PredictPreviousValue();
        }

        return cumSum;
    }
}