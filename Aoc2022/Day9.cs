using System;
namespace Aoc2022;

public record struct Pos(int X, int Y);

public class Day9 : Problem
{
    private Pos HPos { get; set; } = new (0, 0);
    private Pos TPos { get; set; } = new (0, 0);
    private HashSet<Pos> TLocs { get; set; } = new();

    private List<Pos> Knots { get; set; } = Enumerable.Range(0, 10).Select(_ => new Pos(0, 0)).ToList();
    private HashSet<Pos> T2Locs { get; set; } = new();

    public override void Solve()
    {
        var input = Lines("day9.txt").Select(l =>
        {
            var split = l.Split(' ');
            return (split[0][0], int.Parse(split[1]));
        });

        foreach (var (dir, mag) in input)
        {
            for (var step = 0; step < mag; step++)
            {
                // P1
                HPos = NewPos(HPos, dir);
                TPos = NewTailPos(HPos, TPos);
                TLocs.Add(TPos);

                // P2
                for (var ki = 0; ki < Knots.Count; ki++)
                {
                    var prev = Knots[ki];

                    if (ki == 0)
                    {
                        Knots[ki] = NewPos(Knots[ki], dir);
                    }
                    else
                    {
                        Knots[ki] = NewTailPos(Knots[ki - 1], Knots[ki]);
                    }


                    if (ki == Knots.Count - 1 && !T2Locs.Contains(Knots[ki]))
                    {
                        T2Locs.Add(Knots[ki]);
                    }
                }
            }
        }

        Console.WriteLine(TLocs.Count);
        Console.WriteLine(T2Locs.Count);
    }

    private Pos NewTailPos(Pos head, Pos tail)
    {
        var xDelta = head.X - tail.X;
        var yDelta = head.Y - tail.Y;
        if (Math.Abs(xDelta) <= 1 && Math.Abs(yDelta) <= 1) return tail;
        if (yDelta == 0)
        {
            if (xDelta > 1) return new Pos(tail.X + 1, tail.Y);
            if (xDelta < 1) return new Pos(tail.X - 1, tail.Y);
            return tail;
        }
        else if (xDelta == 0)
        {
            if (yDelta > 1) return new Pos(tail.X, tail.Y + 1);
            if (yDelta < 1) return new Pos(tail.X, tail.Y - 1);
            return tail;
        }
        else
        {
            return new Pos(tail.X + (xDelta / Math.Abs(xDelta)), tail.Y + (yDelta / Math.Abs(yDelta)));
        }
    }

    private Pos NewPos(Pos curr, char dir)
    {
        return dir switch
        {
            'L' => new(curr.X - 1, curr.Y),
            'R' => new(curr.X + 1, curr.Y),
            'U' => new(curr.X, curr.Y + 1),
            'D' => new(curr.X, curr.Y - 1),
            _ => throw new Exception("Invalid"),
        };
    }
}

