using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day08;

public class Network
{
    private readonly IEnumerable<Node> _nodes;
    private readonly Regex _regex = new (@"^(?<NodeName>[A-Z0-9]{3}) = \((?<Left>[A-Z0-9]{3}), (?<Right>[A-Z0-9]{3})\)$");
    
    public Network(string[] nodeInputs)
    {
        _nodes = ParseNodes(nodeInputs);
    }

    public int NumStepsToTraverse(string instructions, string startNodeName, string destNodeName)
    {
        var traveller = new Traveller(_nodes.Single(n => n.Name == startNodeName), instructions,  x => x == "ZZZ");

        traveller.GoToDestination();

        return traveller.StepsTaken;
    }

    public long NumStepsToTraverseAsGhost(string instructions)
    {
        var travellers = _nodes
            .Where(n => n.Name.EndsWith('A'))
            .Select(n => new Traveller(n, instructions, d => d.EndsWith('Z')))
            .ToList();

        foreach (var traveller in travellers)
        {
            traveller.GoToDestination();
        }

        return FindLowestCommonMultiple(travellers.Select(t => (long) t.StepsTaken));
    }

    private long FindLowestCommonMultiple(IEnumerable<long> values)
    {
        return values.Aggregate(LowestCommonMultiple);
    }

    private long LowestCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / HighestCommonFactor(a, b);
    }

    private long HighestCommonFactor(long a, long b)
    {
        return b == 0 ? a : HighestCommonFactor(b, a % b);
    }
    
    private IEnumerable<Node> ParseNodes(string[] lines)
    {
        var nodes = new Dictionary<string, Node>();

        for (var i = 2; i < lines.Length; i++)
        {
            var matches = _regex.Match(lines[i]);

            var node = new Node(matches.Groups["NodeName"].Value, matches.Groups["Left"].Value, matches.Groups["Right"].Value);
            nodes.Add(node.Name, node);
        }

        foreach (var node in nodes)
        {
            node.Value.Left = nodes[node.Value.LeftNodeName];
            node.Value.Right = nodes[node.Value.RightNodeName];
        }

        return nodes.Values;
    }
}