namespace AdventOfCode2023.Solutions.Day13;

public class Day13 : ISolveProblems
{
    public long Solve(string[] lines)
    {
        var cumSum = 0;
        var mf = new MirrorFinder();

        var lineBuffer = new List<string>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                mf.SetLines(lineBuffer);
                cumSum += mf.CountNumBefore();
                lineBuffer.Clear();
            }
            else lineBuffer.Add(line);
        }
        
        mf.SetLines(lineBuffer);
        cumSum += mf.CountNumBefore();
        
        return cumSum;
    }
}