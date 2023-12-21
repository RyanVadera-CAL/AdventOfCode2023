namespace AdventOfCode2023.Solutions.Day11;

public class GalaxyMap
{
    private IList<string> Map { get; }
    private HashSet<(int Row, int Col)> GalaxyLocations { get; set; }

    private HashSet<int> _expandedRows;
    private HashSet<int> _expandedCols;
    
    private const long ExpansionAmount = 1000000;
    
    public GalaxyMap (string[] map)
    {
        Map = map.ToList();
        LocateGalaxies();
        LocateExpandedAreas();
    }
    
    public long FindAllDistances()
    {
        long cumSum = 0;
        
        for (var i = 0; i < GalaxyLocations.Count - 1; i++)
        {
            for (var j = i + 1; j < GalaxyLocations.Count; j++)
            {
                cumSum += FindDistanceBetween(GalaxyLocations.ElementAt(i), GalaxyLocations.ElementAt(j));;
            }    
        }

        return cumSum;
    }

    private void LocateGalaxies()
    {
        GalaxyLocations = new HashSet<(int Row, int Col)>();
        
        for (var y = 0; y < Map.Count; y++)
        {
            for (var x = 0; x < Map[y].Length; x++)
            {
                if (Map[y][x] == '#')
                {
                    GalaxyLocations.Add((y,x));
                }
            }
        }
    }

    private void LocateExpandedAreas()
    {
        var rows = GalaxyLocations.Select(l => l.Row).ToHashSet();
        var cols = GalaxyLocations.Select(l => l.Col).ToHashSet();
        
        _expandedRows = new HashSet<int>();
        _expandedCols = new HashSet<int>();
        
        for (var y = 0; y < Map.Count; y++)
        {
            if (!rows.Contains(y)) _expandedRows.Add(y);
        }

        for (var x = 0; x < Map[0].Length; x++)
        {
            if (!cols.Contains(x)) _expandedCols.Add(x);
        }
    }

    private void ApplyExpansionCorrection()
    {
        var rows = GalaxyLocations.Select(l => l.Row).ToHashSet();
        var cols = GalaxyLocations.Select(l => l.Col).ToHashSet();
        
        var emptyRows = new HashSet<int>();
        var emptyCols = new HashSet<int>();
        
        var emptyRow = "";

        for (var y = 0; y < Map.Count; y++)
        {
            if (!rows.Contains(y))
            {
                emptyRows.Add(y);
                emptyRow = Map[y];
            }
        }

        for (var x = 0; x < Map[0].Length; x++)
        {
            if (!cols.Contains(x)) emptyCols.Add(x);
        }

        var addedRows = 0;
        foreach (var emptyRowIndex in emptyRows)
        {
            Map.Insert(emptyRowIndex + addedRows, emptyRow);
            addedRows++;
        }

        var addedCols = 0;
        foreach (var emptyColIndex in emptyCols)
        {
            for (var i = 0; i < Map.Count; i++)
            {
                Map[i] = Map[i].Insert(emptyColIndex + addedCols, ".");
            }
            addedCols++;
        }
    }

    private long FindDistanceBetween((int Row, int Col) galaxy1, (int Row, int Col) galaxy2)
    {
        var expandedRows = FindNumExpandedRowsBetween(galaxy1.Row, galaxy2.Row);
        var expandedCols = FindNumExpandedColsBetween(galaxy1.Col, galaxy2.Col);

        return Math.Abs(galaxy1.Row - galaxy2.Row) + Math.Abs(galaxy1.Col - galaxy2.Col) + ExpansionAmount * (expandedCols + expandedRows) - expandedRows - expandedCols;
    }

    private int FindNumExpandedRowsBetween(int rowA, int rowB)
    {
        var count = 0;
        
        if (rowB > rowA)
        {
            for (var i = rowA; i <= rowB; i++)
            {
                if (_expandedRows.Contains(i)) count++;
            }
        }
        else
        {
            for (var i = rowB; i <= rowA; i++)
            {
                if (_expandedRows.Contains(i)) count++;
            }
        }

        return count;
    }
    
    private int FindNumExpandedColsBetween(int colA, int colB)
    {
        var count = 0;
        
        if (colB > colA)
        {
            for (var i = colA; i <= colB; i++)
            {
                if (_expandedCols.Contains(i)) count++;
            }
        }
        else
        {
            for (var i = colB; i <= colA; i++)
            {
                if (_expandedCols.Contains(i)) count++;
            }
        }

        return count;
    }

    private void PrintMap()
    {
        for (int i = 0; i < Map.Count; i++)
        {
            for (int j = 0; j < Map[i].Length; j++)
            {
                if (GalaxyLocations.Contains((i, j))) Console.Write('#');
                else Console.Write('.');
            }
            Console.WriteLine();
        }
    }
}