using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day08;

public class Network
{
    private readonly IEnumerable<Node> _nodes;
    private readonly Regex _regex = new (@"^(?<NodeName>[A-Z]{3}) = \((?<Left>[A-Z]{3}), (?<Right>[A-Z]{3})\)$");
    
    public Network(string[] nodeInputs)
    {
        _nodes = ParseNodes(nodeInputs);
    }

    public int NumStepsToTraverse(string instructions, string startNodeName, string destNodeName)
    {
        var currentNode = _nodes.Single(n => n.Name == startNodeName);
        var instructionPointer = 0;
        var numSteps = 0;

        while (currentNode.Name != destNodeName)
        {
            numSteps++;
            var direction = instructions[instructionPointer];
            currentNode = GetNextNode(currentNode, direction);

            if (instructionPointer == instructions.Length - 1)
            {
                instructionPointer = 0;
            }
            else
            {
                instructionPointer++;
            }
        }

        return numSteps;
    }
    
    private Node GetNextNode(Node currentNode, char direction)
    {
        return direction switch
        {
            'L' => currentNode.Left,
            'R' => currentNode.Right,
            _ => throw new ArgumentException()
        };
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