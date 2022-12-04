using System;
namespace Aoc2022;

public class Day4 : Problem
{
    public override void Solve()
    {
        IEnumerable<(int, int)[]> input = Lines("day4.txt").Select(line =>
        {
            var parts = line.Split(',');
            var l = parts[0].Split('-');
            var r = parts[1].Split('-');
            return new[] { (int.Parse(l[0]), int.Parse(l[1])), (int.Parse(r[0]), int.Parse(r[1])) };
        });

        var part1 = input.Sum(rs =>
        {
            var a = rs[0];
            var b = rs[1];
            return Convert.ToInt32((a.Item1 <= b.Item1 && a.Item2 >= b.Item2) || (b.Item1 <= a.Item1 && b.Item2 >= a.Item2));
        });
        Console.WriteLine(part1);

        var part2 = input.Sum(rs =>
        {
            //var a = Enumerable.Range(rs[0].Item1, rs[0].Item2 - rs[0].Item1 + 1);
            //var b = Enumerable.Range(rs[1].Item1, rs[1].Item2 - rs[1].Item1 + 1);
            //return a.Intersect(b).Any() ? 1 : 0;
            var (a1, a2) = rs[0];
            var (b1, b2) = rs[1];
            return Convert.ToInt32(
                (b1 >= a1 && b1 <= a2)
                || (b2 >= a1 && b2 <= a2)
                || (a1 >= b1 && a1 <= b2)
                || (a2 >= b1 && a2 <= b2)
            );
        });
        Console.WriteLine(part2);
    }
}

