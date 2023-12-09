namespace AdventOfCode2023.Solutions.Day07;

public class Hand
{
    public string HandTypeValue { get; private set; }
    public string CardsValue { get; private set; }
    
    private readonly ICollection<int> _cards;

    public Hand(string handInput)
    {
        _cards = handInput.Select(CardAsInt).ToList();
        
        HandTypeValue = GetMaximiseHandType();
        CardsValue = HandAsString();
    }

    private string HandAsString()
    {
        return new string(_cards.Select(c => (char)('A' + c)).ToArray());
    }
    
    private string GetHandTypeValue()
    {
        var cardCounts = new int[CardAsInt('A')];
        
        foreach (var card in _cards)
        {
            cardCounts[card - 1]++;
        }
        
        return new string(cardCounts.OrderByDescending( x => x).Select(x => (char)('A' + x)).ToArray());
    }

    private string GetMaximiseHandType()
    {
        var cardCounts = new int[CardAsInt('A')];
        
        foreach (var card in _cards)
        {
            cardCounts[card - 1]++;
        }

        var numJokers = cardCounts[0];
        var cardCountsList = cardCounts.ToList();
        if (numJokers < _cards.Count)
        {
            cardCountsList[0] = 0;
            
            var i = cardCountsList.IndexOf(cardCountsList.Max());
            cardCountsList[i] += numJokers;
        }
        
        return new string(cardCountsList.OrderByDescending( x => x).Select(x => (char)('A' + x)).ToArray());
    }

    private static int CardAsInt(char card)
    {
        return card switch
        {
            'A' => 13,
            'K' => 12,
            'Q' => 11,
            'T' => 10,
            'J' => 1,
            _ => card - '0'
        };
    }
}