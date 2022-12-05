using System;
using System.Text.RegularExpressions;

namespace Aoc2022;

public class Day5 : Problem
{
    public override void Solve()
    {
        var input = Text("day5.txt");
        var parts = input.Split("\n\n");
        var crates = parts[0].Split('\n');
        var cols = crates[^1];
        var stackIdxs = cols.Select((c, i) => char.IsWhiteSpace(c) ? -1 : i).Where(x => x != -1).ToList();
        var stacks = new List<List<char>>(stackIdxs.Count);
        foreach (var line in crates[0..^1])
        {
            var s = 0;
            foreach (var idx in stackIdxs)
            {
                if (stacks.Count < s + 1) stacks.Add(new List<char>());
                if (!char.IsWhiteSpace(line[idx]))
                {
                    stacks[s].Add(line[idx]);
                }
                s++;
            }
        }

        var instructions = parts[1].Split('\n');
        var re = new Regex(@"move (\d+) from (\d) to (\d)");
        foreach (var instr in instructions)
        {
            var matches = re.Matches(instr).First().Groups;
            var qty = int.Parse(matches[1].Value);
            var src = int.Parse(matches[2].Value);
            var dst = int.Parse(matches[3].Value);

            var sStack = stacks[src - 1];
            var dStack = stacks[dst - 1];
            var moved = sStack.ToArray()[0..qty];
            sStack.RemoveRange(0, qty);
            // in part 1, this was moved.Reverse()
            dStack.InsertRange(0, moved);
        }

        var part2 = string.Join("", stacks.Select(s => s.First()));
        Console.WriteLine(part2);
    }
}

