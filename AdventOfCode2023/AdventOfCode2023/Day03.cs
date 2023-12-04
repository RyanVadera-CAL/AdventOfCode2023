namespace AdventOfCode2023;

public static class Day03
{
    public static int Solve_A(string[] lines)
    {
        var cumSum = 0;
        
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];
                if (int.TryParse(c.ToString(), out _) || c == '.')
                {
                    continue;
                }
                
                var left = TryGetNumberAt(lines[i], j - 1);
                var right = TryGetNumberAt(lines[i], j + 1);
                cumSum += left ?? 0;
                cumSum += right ?? 0;
                
                if (i > 0)
                {
                    var up = TryGetNumberAt(lines[i - 1], j);
                    cumSum += up ?? 0;

                    if (up is null)
                    {
                        var tl = TryGetNumberAt(lines[i - 1], j - 1);
                        var tr = TryGetNumberAt(lines[i - 1], j + 1);

                        cumSum += tl ?? 0;
                        cumSum += tr ?? 0;
                    }
                }

                if (i <= lines.Length)
                {
                    var down = TryGetNumberAt(lines[i + 1], j);
                    cumSum += down ?? 0;
                    if (down is null)
                    {
                        var dl = TryGetNumberAt(lines[i + 1], j - 1);
                        var dr = TryGetNumberAt(lines[i + 1], j + 1);

                        cumSum += dl ?? 0;
                        cumSum += dr ?? 0;
                    }
                }
            }
        }

        return cumSum;
    }

    public static int Solve_B(string[] lines)
    {
        var cumSum = 0;
        
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];
                if (c != '*')
                {
                    continue;
                }

                var nums = new List<int?>();
                
                var left = TryGetNumberAt(lines[i], j - 1);
                var right = TryGetNumberAt(lines[i], j + 1);
                nums.Add(left);
                nums.Add(right);

                if (i > 0)
                {
                    var up = TryGetNumberAt(lines[i - 1], j);
                    nums.Add(up);
                    if (up is null)
                    {
                        var tl = TryGetNumberAt(lines[i - 1], j - 1);
                        var tr = TryGetNumberAt(lines[i - 1], j + 1);
                        nums.Add(tl);
                        nums.Add(tr);
                    }
                }

                if (i <= lines.Length)
                {
                    var down = TryGetNumberAt(lines[i + 1], j);
                    nums.Add(down);
                    if (down is null)
                    {
                        var dl = TryGetNumberAt(lines[i + 1], j - 1);
                        var dr = TryGetNumberAt(lines[i + 1], j + 1);
                        nums.Add(dl);
                        nums.Add(dr);
                    }
                }

                var actualNums = nums.Where(n => n.HasValue).Select(x => x!.Value).ToList();
                if (actualNums.Count == 2)
                {
                    cumSum += actualNums[0] * actualNums[1];
                }
            }
        }

        return cumSum;
    }

    private static int? TryGetNumberAt(string line, int col)
    {
        if (col < 0 || col >= line.Length)
        {
            return null;
        }
        
        var num = line[col];
        if (!int.TryParse(num.ToString(), out _))
        {
            return null;
        }

        var leftNumber = CheckLeft(line, col);
        var rightNumber = CheckRight(line, col);
        
        return int.Parse($"{leftNumber}{num}{rightNumber}");
    }

    private static int? CheckLeft(string line, int pos)
    {
        if (pos == 0)
        {
            return null;
        }

        if (int.TryParse(line[pos - 1].ToString(), out var currentInt))
        {
            var leftInt = CheckLeft(line, pos - 1);
            return (leftInt * 10 ?? 0) + currentInt;
        }

        return null;
    }

    private static string? CheckRight(string line, int pos)
    {
        if (pos >= line.Length - 1)
        {
            return null;
        }
        
        if (int.TryParse(line[pos + 1].ToString(), out var currentInt))
        {
            var rightInt = CheckRight(line, pos + 1);
            if (rightInt is not null)
            {
                return $"{currentInt}{rightInt}";
            }

            return currentInt.ToString();
        }

        return null;
    }
}