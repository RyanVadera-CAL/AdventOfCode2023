namespace AdventOfCode2023.Solutions.Day10;

public class Sketch
{
    private readonly string[] _map;
    
    private readonly IDictionary<Direction, IEnumerable<char>> _possiblePipes = new Dictionary<Direction, IEnumerable<char>>
    {
        { Direction.North, new List<char>{ '|', '7', 'F' } },
        { Direction.East, new List<char>{ '-', 'J', '7' } },
        { Direction.South, new List<char>{ '|', 'L', 'J' } },
        { Direction.West, new List<char>{ '-', 'L', 'F' } }
    };

    private readonly IDictionary<char, ICollection<Direction>> _possibleDirections = new Dictionary<char, ICollection<Direction>>
    {
        { '|', new[] { Direction.North, Direction.South } },
        { '-', new[] { Direction.East, Direction.West } },
        { 'L', new[] { Direction.North, Direction.East } },
        { 'J', new[] { Direction.North, Direction.West } },
        { '7', new[] { Direction.South, Direction.West } },
        { 'F', new[] { Direction.South, Direction.East } },
        { 'S', new[] { Direction.North, Direction.East, Direction.South, Direction.West } },
        { '.', new List<Direction>() },
    };
    
    public Sketch(string[] map)
    {
        _map = map;
    }

    public HashSet<Direction> GetPossibleMovementDirections(Tile tile)
    {
        var validDirections = new HashSet<Direction>();

        var currentPipe = GetPipe(tile);
        if (currentPipe is null)
        {
            return validDirections;
        }

        foreach (var direction in _possibleDirections[currentPipe.Value])
        {
            var tileInDirection = GetTileInDirection(direction, tile);

            var pipe = GetPipe(tileInDirection);
            if (pipe is null)
            {
                continue;
            }
            
            if (_possiblePipes[direction].Contains(pipe.Value))
            {
                validDirections.Add(direction);
            }
        }

        return validDirections;
    }
    
    public Tile GetStartingTile()
    {
        var i = 0;
        var startingCoords = (Row: -1, Col: -1);
        
        while (startingCoords is { Row: -1, Col: -1 })
        {
            var line = _map[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] != 'S') continue;
                
                startingCoords.Row = i;
                startingCoords.Col = j;
                break;
            }

            i++;
        }

        return new Tile(startingCoords.Row, startingCoords.Col);
    }

    private char? GetPipe(Tile tile)
    {
        if (tile.Row < 0 || 
            tile.Row > _map.Length || 
            tile.Col < 0 || 
            tile.Col > _map[0].Length)
        {
            return null;
        }
        return _map[tile.Row][tile.Col];
    }
    
    public Tile GetTileInDirection(Direction direction, Tile tile)
    {
        return direction switch
        {
            Direction.North => tile with { Row = tile.Row - 1 },
            Direction.East => tile with { Col = tile.Col + 1 },
            Direction.South => tile with { Row = tile.Row + 1 },
            Direction.West => tile with { Col = tile.Col - 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}