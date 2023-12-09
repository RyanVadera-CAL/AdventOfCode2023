namespace AdventOfCode2023;

public static class Day04
{
    public static int Solve_A(string[] lines)
    {
        var cumSum = 0;

        foreach (var line in lines)
        {
            var numbers = line.Split(':')[1];
            
            var ourNumbers = numbers.Split('|')[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToHashSet();
            var winningNumbers = numbers.Split('|')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToHashSet();

            winningNumbers.IntersectWith(ourNumbers);

            var points = Math.Pow(2, winningNumbers.Count - 1);

            cumSum += (int) points;
        }

        return cumSum;
    }
    
    public static int Solve_B(string[] lines)
    {
        return lines.Select((t, i) => GetNumberOfCardsWon(lines, i)).Sum() + lines.Length;
    }

    private static int GetNumberOfCardsWon(string[] lines, int cardIndex)
    {
        var cumSum = 0;
        var line = lines[cardIndex];
        
        var numbers = line.Split(':')[1];
            
        var ourNumbers = numbers.Split('|')[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToHashSet();
        var winningNumbers = numbers.Split('|')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToHashSet();

        winningNumbers.IntersectWith(ourNumbers);

        cumSum += winningNumbers.Count;
        
        for (var i = 1; i <= winningNumbers.Count; i++)
        {
            cumSum += GetNumberOfCardsWon(lines, cardIndex + i);
        }
        
        return cumSum;
    }
}