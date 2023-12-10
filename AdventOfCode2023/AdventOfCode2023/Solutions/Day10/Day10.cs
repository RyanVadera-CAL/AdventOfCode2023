namespace AdventOfCode2023.Solutions.Day10;

public class Day10 : ISolveProblems
{
    private HashSet<Tile> _visitedTiles = new ();

    public long Solve(string[] lines)
    {
        var sketch = new Sketch(lines);

        var startingTile = sketch.GetStartingTile();

        _visitedTiles.Add(startingTile);

        var possibleDirections = sketch.GetPossibleMovementDirections(startingTile);

        var travellers = possibleDirections
            .Select(d => new Traveller(startingTile, d, sketch))
            .ToHashSet();

        var reachedAlreadyVisitedTile = false;

        while (!reachedAlreadyVisitedTile && travellers.Any())
        {
            var stuckTravellers = new HashSet<Traveller>();

            foreach (var traveller in travellers)
            {
                if (traveller.TryMoveToNextTile())
                {
                    if (_visitedTiles.Contains(traveller.CurrentTile))
                    {
                        reachedAlreadyVisitedTile = true;
                    }
                    else
                    {
                        _visitedTiles.Add(traveller.CurrentTile);
                    }
                }
                else
                {
                    stuckTravellers.Add(traveller);
                }
            }

            travellers.ExceptWith(stuckTravellers);
        }

        if (!travellers.Any())
        {
            return -1;
        }

        return travellers.First().StepsTaken;
    }
}