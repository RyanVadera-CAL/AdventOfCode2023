namespace AdventOfCode2023.Solutions.Day13;

public class MirrorFinder
{
    private string[] _lines;

    public void SetLines(IEnumerable<string> lines)
    {
        _lines = lines.ToArray();
    }

    public int CountNumBefore()
    {
        if(TryFindHorizontalSymmetry(out var v))
        {
            return 100 * (v + 1);
        }
        
        if(TryFindVerticalSymmetry(out var h))
        {
            return h + 1;
        }

        return 0;
    }
    
    private bool TryFindHorizontalSymmetry(out int numRowsBefore)
    {
        numRowsBefore = -1;
        
        for (var i = 0; i < _lines.Length - 1; i++)
        {
            if (!AreRowsEqual(i, i + 1)) continue;
            
            numRowsBefore = i;
            return true;
        }

        return false;
    }

    private bool TryFindVerticalSymmetry(out int symmetryIndex)
    {
        symmetryIndex = -1;
        
        for (var i = 0; i < _lines[0].Length - 1; i++)
        {
            if (!AreColsEqual(i, i + 1)) continue;
            
            symmetryIndex = i;
            return true;
        }

        return false;
    }

    private bool AreRowsEqual(int rowIndexA, int rowIndexB)
    {
        if (rowIndexA < 0 || rowIndexB >= _lines.Length) return true;


        if (_lines[rowIndexA] != _lines[rowIndexB]) return false;
        
        return AreRowsEqual(rowIndexA - 1, rowIndexB + 1);
    }

    private bool AreColsEqual(int colIndexA, int colIndexB)
    {
        if (colIndexA < 0 || colIndexB >= _lines[0].Length) return true;

        if (_lines.Any(line => line[colIndexA] != line[colIndexB]))
        {
            return false;
        }

        return AreColsEqual(colIndexA - 1, colIndexB + 1);
    }
}