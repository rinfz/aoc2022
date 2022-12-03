using System;
namespace Aoc2022;

public class Rucksack
{
    public string C1 { get; init; }
    public string C2 { get; init; }

    public Rucksack(string line)
    {
        var halfLen = line.Length / 2;
        C1 = line.Substring(0, halfLen);
        C2 = line.Substring(halfLen);
    }

    public char Diff()
    {
        var l = new HashSet<char>(C1.ToCharArray());
        var r = new HashSet<char>(C2.ToCharArray());
        var c = l.Intersect(r);
        return c.First();
    }
}

public class Day3 : Problem
{
    public override void Solve()
    {
        var input = Lines("day3.txt");
        var rucksacks = input.Select(l => new Rucksack(l));
        var part1 = rucksacks.Select(r => Priority(r.Diff())).Sum();
        Console.WriteLine(part1);

        var part2 = input.Chunk(3).Select(g => Priority(Union3(g))).Sum();
        Console.WriteLine(part2);
    }

    public char Union3(string[] rs)
    {
        if (rs.Length != 3)
        {
            throw new ArgumentException("Invalid chunk");
        }

        var a = new HashSet<char>(rs[0].ToCharArray());
        var b = new HashSet<char>(rs[1].ToCharArray());
        var c = new HashSet<char>(rs[2].ToCharArray());

        foreach (char x in a)
        {
            if (b.Contains(x) && c.Contains(x))
            {
                return x;
            }
        }

        throw new Exception("Invalid input");
    }

    public int Priority(char v)
    {
        var i = (int)v;
        if (i <= 90)
        {
            return i - 38;
        }
        else
        {
            return i - 96;
        }
    }
}

