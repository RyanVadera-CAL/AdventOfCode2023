using AdventOfCode2023.Solutions.Day07;
using AdventOfCode2023.Solutions.Day08;
using AdventOfCode2023.Solutions.Day09;

namespace AdventOfCode2023;

public class AdventSolver
{
    private readonly string[] _inputLines;
    
    public AdventSolver(string inputFilePath)
    {
        _inputLines = File.ReadAllLines(inputFilePath);
    }
    
    public long SolveToday()
    {
        var today = DateTime.Today;
        return SolveForDay(today.Day);
    }
    
    public long SolveForDay(int day)
    {
        var solver = GetSolver(day);

        return solver.Solve(_inputLines);
    }

    private ISolveProblems GetSolver(int day)
    {
        switch (day)
        {
            case 7:
                return new Day07();
            case 8:
                return new Day08();
            case 9:
                return new Day09();
            default:
                throw new NotImplementedException();
        }
    }
}