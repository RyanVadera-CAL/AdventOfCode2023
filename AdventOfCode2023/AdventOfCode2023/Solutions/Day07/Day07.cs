namespace AdventOfCode2023.Solutions.Day07;

public class Day07 : ISolveProblems
{
    public int Solve(string[] lines)
    {
        var hands = new List<(Hand Hand, int Bid)>();
        
        foreach (var line in lines)
        {
            var (hand, bid) = line.Split(' ') switch { var x => (x[0], int.Parse(x[1])) };
            hands.Add(new (new Hand(hand), bid));
        }

        var orderedHands = hands
            .OrderBy(x => x.Hand.HandTypeValue)
            .ThenBy(x => x.Hand.CardsValue)
            .ToList();

        var score = 0;

        for (var i = 0; i < orderedHands.Count; i++)
        {
            score += orderedHands[i].Bid * (i + 1);
        }

        return score;
    }
}