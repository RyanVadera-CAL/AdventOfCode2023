namespace AdventOfCode2023.Solutions.Day08;

public class Traveller
{
    public int StepsTaken { get; private set; }
    private bool IsAtDestination => _isAtDest(_currentNode.Name);
    private Node _currentNode;
    private readonly string _instructions;
    private readonly Predicate<string> _isAtDest;

    public Traveller(Node startingNode, string instructions, Predicate<string> isAtDestination)
    {
        StepsTaken = 0;
        _currentNode = startingNode;
        _instructions = instructions;
        _isAtDest = isAtDestination;
    }

    public void GoToDestination()
    {
        while (!IsAtDestination)
        {
            MoveForward();
        }
    }
    
    private void MoveForward()
    {
        var direction = _instructions[StepsTaken % _instructions.Length];
        
        _currentNode = direction switch
        {
            'L' => _currentNode.Left,
            'R' => _currentNode.Right,
            _ => throw new ArgumentException()
        };

        StepsTaken++;
    }
}