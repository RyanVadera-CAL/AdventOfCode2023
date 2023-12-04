namespace AdventOfCode2023;

public static class Day01
{
    private static readonly HashSet<char> DigitChars = new()
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    private static readonly Dictionary<string, int> DigitStrings = new()
    {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public static int Solve_A(string[] lines)
    {
        var cumSum = 0;

        foreach (var line in lines)
        {
            var leftPointer = 0;
            var rightPointer = line.Length - 1;

            int? leftDigit = null, rightDigit = null;

            while (leftDigit is null || rightDigit is null)
            {
                if (leftDigit is null && DigitChars.Contains(line[leftPointer]))
                {
                    leftDigit = line[leftPointer] - '0';
                }

                if (rightDigit is null && DigitChars.Contains(line[rightPointer]))
                {
                    rightDigit = line[rightPointer] - '0';
                }

                leftPointer++;
                rightPointer--;
            }

            var number = (leftDigit.Value * 10) + rightDigit.Value;

            cumSum += number;
        }

        return cumSum;
    }
    
    public static int Solve_B(string[] lines)
    {
        var cumSum = 0;

        foreach (var line in lines)
        {
            int? first = null;
            int? last = null;
            
            for (var i = 0; i < line.Length; i++)
            {
                if (DigitChars.Contains(line[i]))
                {
                    last = int.Parse(line[i].ToString());
                    first ??= int.Parse(line[i].ToString());
                }

                var numberString = new string(line.Skip(i).Take(5).ToArray());
                var key = DigitStrings.Keys.FirstOrDefault(k => numberString.StartsWith(k));
                if (key is not null)
                {
                    last = DigitStrings[key];
                    first ??= DigitStrings[key];
                }
            }
            
            cumSum += (first!.Value * 10) + last!.Value;
        }

        return cumSum;
    }
}