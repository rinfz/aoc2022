using System;
namespace Aoc2022;

public class Day7 : Problem
{
    private Dictionary<string, int> Files { get; set; } = new();
    private Dictionary<string, List<(bool, string)>> Dirs { get; set; } = new();

    public override void Solve()
    {
        Init();

        var total = 70_000_000;
        var target = 30_000_000;
        var unused = total - DirSize("/");

        int part1 = 0;
        var part2 = new List<int>();

        foreach (var dir in Dirs.Keys)
        {
            var size = DirSize(dir);
            if (size <= 100_000)
            {
                part1 += size;
            }

            if (unused + size >= target)
            {
                part2.Add(size);
            }
        }

        Console.WriteLine(part1);
        Console.WriteLine(part2.Min());
    }

    private Dictionary<string, int> Cache { get; set; } = new();
    private int DirSize(string name)
    {
        if (Cache.ContainsKey(name)) return Cache[name];
        int result = 0;
        foreach (var (isDir, filename) in Dirs[name])
        {
            if (isDir)
            {
                result += DirSize(filename);
            }
            else
            {
                result += Files[DirNameS(name, filename)];
            }
        }
        Cache[name] = result;
        return result;
    }

    private void Init()
    {
        var input = Lines("day7.txt").Skip(1);
        var currDir = new Stack<string>();
        Dirs["/"] = new List<(bool, string)>();
        currDir.Push("/");

        foreach (var line in input)
        {
            if (line == "$ ls")
            {
                continue;
            }

            if (line.StartsWith("$ cd"))
            {
                var parts = line.Split(" ");
                var toDir = parts[^1];
                if (toDir == "..")
                {
                    currDir.Pop();
                }
                else
                {
                    currDir.Push(DirName(currDir, toDir));
                }
            }
            else
            {
                var parts = line.Split(' ');
                if (line.StartsWith("dir"))
                {
                    // handle dir
                    AddDir(currDir, parts[1]);
                    Dirs[currDir.Peek()].Add((true, DirName(currDir, parts[1])));
                }
                else
                {
                    // handle file
                    Dirs[currDir.Peek()].Add((false, parts[1]));
                    Files[DirName(currDir, parts[1])] = int.Parse(parts[0]);
                }
            }
        }
    }

    private void AddDir(Stack<string> currStack, string name)
    {
        var curr = currStack.Peek();
        var dn = DirName(currStack, name);
        if (!Dirs.ContainsKey(dn))
        {
            Dirs[dn] = new List<(bool, string)>();
        }
    }

    private string DirName(Stack<string> curr, string name)
    {
        var cn = curr.Peek();
        return DirNameS(curr.Peek(), name);
    }

    private string DirNameS(string c, string name)
    {
        if (c.EndsWith("/"))
        {
            return $"{c}{name}";
        }
        else
        {
            return $"{c}/{name}";
        }
    }
}

