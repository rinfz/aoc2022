using System;
namespace Aoc2022;

public class Day6 : Problem
{
    public override void Solve()
    {
        var input = Text("day6.txt");
        int part1 = 0;
        bool gotP1 = false;
        int part2 = 0;
        ReadOnlySpan<char> span = input.ToCharArray();
        for (var i = 0; i < input.Length - 4; i++)
        {
            var window = span.Slice(i, 4);
            var set = new HashSet<char>(window.ToArray());
            if (!gotP1 && set.Count == 4)
            {
                gotP1 = true;
                part1 = i + 4;
            }
            var window2 = span.Slice(i, 14);
            var set2 = new HashSet<char>(window2.ToArray());
            if (set2.Count == 14)
            {
                part2 = i + 14;
                break;
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
}

