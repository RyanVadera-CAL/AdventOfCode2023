using AdventOfCode2023;

var input = await File.ReadAllLinesAsync("input.txt");

var solution = Day04.Solve_B(input);

Console.WriteLine(solution);