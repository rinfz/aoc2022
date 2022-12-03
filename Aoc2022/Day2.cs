using System;
namespace Aoc2022;

public class Day2 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllLines(InputPath("day2.txt")).Select((line) => line.Split(' '));
        var part1 = input.Select(ResultScore).Sum();
        Console.WriteLine(part1);

        var part2 = input.Select(Outcome).Sum();
        Console.WriteLine(part2);
    }

    private int Outcome(string[] parts)
    {
        // x: lose, y: draw, z: win
        return parts switch
        {
            ["A", "X"] => 3 + 0,
            ["A", "Y"] => 1 + 3,
            ["A", "Z"] => 2 + 6,
            ["B", "X"] => 1 + 0,
            ["B", "Y"] => 2 + 3,
            ["B", "Z"] => 3 + 6,
            ["C", "X"] => 2 + 0,
            ["C", "Y"] => 3 + 3,
            ["C", "Z"] => 1 + 6,
            _ => 0,
        };
    }

    private int ResultScore(string[] parts)
    {
        return parts switch
        {
            ["A", "X"] => 3 + 1,
            ["A", "Y"] => 6 + 2,
            ["A", "Z"] => 0 + 3,
            ["B", "X"] => 0 + 1,
            ["B", "Y"] => 3 + 2,
            ["B", "Z"] => 6 + 3,
            ["C", "X"] => 6 + 1,
            ["C", "Y"] => 0 + 2,
            ["C", "Z"] => 3 + 3,
            _ => 0,
        };
    }
}

