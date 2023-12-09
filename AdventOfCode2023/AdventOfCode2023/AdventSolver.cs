using AdventOfCode2023.Solutions.Day07;

namespace AdventOfCode2023;

public class AdventSolver
{
    private readonly string[] _inputLines;
    
    public AdventSolver(string inputFilePath)
    {
        _inputLines = File.ReadAllLines(inputFilePath);
    }
    
    public int SolveToday()
    {
        var today = DateTime.Today;
        return SolveForDay(today.Day);
    }
    
    public int SolveForDay(int day)
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
            default:
                throw new NotImplementedException();
        }
    }
}