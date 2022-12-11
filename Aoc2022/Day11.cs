using System;
namespace Aoc2022;

public class Monkey
{
    public string Id { get; set; }
    public Queue<long> Items { get; set; } = new();

    private string OpLHS { get; set; }
    private string OpKind { get; set; }
    private string OpRHS { get; set; }

    public int DivBy { get; set; }
    private int TrueTarget { get; set; }
    private int FalseTarget { get; set; }

    public Monkey(string id, IEnumerable<long> items, string op, int divBy, int trueTarget, int falseTarget)
    {
        Id = id;
        foreach (var item in items) Items.Enqueue(item);
        var opParts = op.Split(' ');
        OpLHS = opParts[0];
        OpKind = opParts[1];
        OpRHS = opParts[2];
        DivBy = divBy;
        TrueTarget = trueTarget;
        FalseTarget = falseTarget;
    }

    public long AdjustWorryLevel(long value)
    {
        if (OpKind == "*")
        {
            if (OpRHS == "old") return value * value;
            else return value * int.Parse(OpRHS);
        }
        else
        {
            if (OpRHS == "old") return value + value;
            else return value + int.Parse(OpRHS);
        }
    }

    public int TestWorry(long worry)
    {
        if (worry % DivBy == 0) return TrueTarget;
        else return FalseTarget;
    }
}

public class Day11 : Problem
{
    private Dictionary<string, long> Inspections { get; set; } = new();

    public override void Solve()
    {
        var monkeys = Init();
        var lcm = monkeys.Select(m => m.DivBy).Aggregate((a, b) => a * b);

        for (var round = 0; round < 10_000; round++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    var item = monkey.Items.Dequeue();
                    Inspections[monkey.Id] += 1;
                    var worry = monkey.AdjustWorryLevel(item);
                    //worry /= 3; // part 1
                    var nw = (long)(worry % lcm); // part 2
                    var target = monkey.TestWorry(nw);
                    monkeys[target].Items.Enqueue(nw);
                }
            }
        }

        long part2 = Inspections.Values.OrderDescending().Take(2).Aggregate((a, b) => a * b);
        Console.WriteLine(part2);
    }

    private List<Monkey> Init()
    {
        var input = Text("day11.txt");
        var parts = input.Split("\n\n");
        var result = new List<Monkey>();

        foreach (var part in parts)
        {
            var lines = part.Split('\n');
            var id = lines[0].Split(' ')[1];
            Inspections[id] = 0;
            var items = lines[1].Split(": ")[1].Split(", ").Select(i => long.Parse(i));
            var op = lines[2].Split(" = ")[1];
            var divBy = int.Parse(lines[3].Split(' ')[^1]);
            var trueTarget = int.Parse(lines[4].Split(' ')[^1]);
            var falseTarget = int.Parse(lines[5].Split(' ')[^1]);
            result.Add(new Monkey(id, items, op, divBy, trueTarget, falseTarget));
        }

        return result;
    }
}

