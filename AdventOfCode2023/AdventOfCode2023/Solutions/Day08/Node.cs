namespace AdventOfCode2023.Solutions.Day08;

public class Node
{
    public readonly string LeftNodeName;
    public readonly string RightNodeName;
    public string Name { get; init; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(string name, string leftNodeName, string rightNodeName)
    {
        LeftNodeName = leftNodeName;
        RightNodeName = rightNodeName;
        Name = name;
    }

    public Node GoLeft()
    {
        return Left;
    }

    public Node GoRight()
    {
        return Right;
    }
}