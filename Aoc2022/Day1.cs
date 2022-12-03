using System;
namespace Aoc2022;

public class Day1 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllText(InputPath("day1.txt")).TrimEnd();
        var elves = input.Split("\n\n");
        var calsPerElf = elves.Select((cals) =>
        {
            return cals.Split('\n').Select(int.Parse).Sum();
        });
        Console.WriteLine($"Part 1: {calsPerElf.Max()}");

        var part2 = calsPerElf.OrderDescending().Take(3).Sum();
        Console.Write($"Part 2: {part2}");
    }
}

