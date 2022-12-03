using System;
namespace Aoc2022;

public abstract class Problem
{
    public abstract void Solve();

    public virtual string InputPath(string filename)
    {
        return Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "inputs", filename);
    }

    public List<string> Lines(string filename)
    {
        return File.ReadAllLines(InputPath(filename)).ToList();
    }
}

