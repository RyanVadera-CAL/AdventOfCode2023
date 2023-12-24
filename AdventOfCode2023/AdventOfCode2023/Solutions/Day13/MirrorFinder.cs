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

    private bool AreRowsEqual(int rowIndexA, int rowIndexB, int strikes = 0)
    {
        if (rowIndexA < 0 || rowIndexB >= _lines.Length)
        {
            return strikes == 1;
        }

        for (var i = 0; i < _lines[rowIndexA].Length; i++)
        {
            if (_lines[rowIndexA][i] == _lines[rowIndexB][i]) continue;
            if (strikes > 0) return false;
            strikes++;
        }

        return AreRowsEqual(rowIndexA - 1, rowIndexB + 1, strikes);
    }

    private bool AreColsEqual(int colIndexA, int colIndexB, int strikes = 0)
    {
        if (colIndexA < 0 || colIndexB >= _lines[0].Length)
        {
            return strikes == 1;
        }

        for (var i = 0; i < _lines.Length; i++)
        {
            if (_lines[i][colIndexA] == _lines[i][colIndexB]) continue;
            if (strikes > 0) return false;
            strikes++;
        }

        return AreColsEqual(colIndexA - 1, colIndexB + 1, strikes);
    }
}