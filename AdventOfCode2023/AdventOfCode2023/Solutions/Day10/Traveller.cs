namespace AdventOfCode2023.Solutions.Day10;

public class Traveller
{
    public Tile CurrentTile { get; private set; }
    
    public int StepsTaken { get; private set; }
    private readonly Sketch _sketch;
    private Direction? _directionOfNextTile;

    public Traveller(Tile startingTile, Direction initialDirection, Sketch sketch)
    {
        CurrentTile = startingTile;
        _directionOfNextTile = initialDirection;
        _sketch = sketch;
    }

    public bool TryMoveToNextTile()
    {
        if (StepsTaken > 0)
        {
            CalculateDirectionOfMovement();
        }

        if (_directionOfNextTile is null) return false;

        MoveToNextTile();

        return true;
    }

    private void CalculateDirectionOfMovement()
    {
        if (_directionOfNextTile is null) return;
        
        var previousTileDirection = GetOppositeDirection(_directionOfNextTile.Value);
        
        _directionOfNextTile = null;
        
        var validDirections = _sketch.GetPossibleMovementDirections(CurrentTile);
        
        validDirections.RemoveWhere(d => d == previousTileDirection);

        if (validDirections.Count == 0) return;
        if (validDirections.Count > 1)
        {
            Console.WriteLine("Multiple possible directions found!");
            return;
        }

        _directionOfNextTile = validDirections.Single();
    }

    private void MoveToNextTile()
    {
        CurrentTile = _sketch.GetTileInDirection(_directionOfNextTile!.Value, CurrentTile);
        StepsTaken++;
    }
    
    private Direction GetOppositeDirection(Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.South,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            Direction.West => Direction.East,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}