using System;
namespace Aoc2022;

public class Day10 : Problem
{
    public override void Solve()
    {
        var input = Lines("day10.txt");
        int? value = null;
        int X = 1;
        var cycle = 1;
        var i = 0;
        var part1 = 0;
        var offset = 0;
        while (i < input.Count)
        {
            if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
            {
                part1 += SignalStrength(cycle, X);
            }
            if (cycle == 41 || cycle == 81 || cycle == 121 || cycle == 161 || cycle == 201 || cycle == 241)
            {
                Console.Write('\n');
                offset = cycle - 1;
            }
            // part 2
            var pix = cycle - (offset + 1);
            if (X - 1 == pix || X == pix || X + 1 == pix)
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
            var line = input[i];
            if (value is not null)
            {
                X += (int)value;
                value = null;
                i += 1;
            }
            else if (line != "noop")
            {
                value = int.Parse(line.Split(' ')[1]);
            }
            else
            {
                i += 1;
            }
            cycle += 1;
        }
        Console.WriteLine("\n{0}", part1);
    }

    private int SignalStrength(int cycle, int X) => cycle * X;
}

