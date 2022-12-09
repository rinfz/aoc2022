using System;
namespace Aoc2022;

public class Day8 : Problem
{
    public override void Solve()
    {
        var grid = InitGrid();
        var part1 = new HashSet<(int, int)>();
        var m = grid.GetUpperBound(1);
        var n = grid.GetUpperBound(0);
        for (var i = 0; i <= n; i++)
        {
            var col = Col(grid, i);
            part1.UnionWith(FindVisible(col).Select(r => (r, i))); // top
            col.Reverse();
            part1.UnionWith(FindVisible(col).Select(r => (n-r, i))); // bottom
            var row = Row(grid, i);
            part1.UnionWith(FindVisible(row).Select(c => (i, c))); // left
            row.Reverse();
            part1.UnionWith(FindVisible(row).Select(c => (i, m-c))); // right
        }
        Console.WriteLine(part1.Count);

        var part2 = -1;
        for (var y = 1; y < n; y++)
        {
            for (var x = 1; x < m; x++)
            {
                var score = ScenicScore(grid, y, x);
                if (score > part2) part2 = score;
            }
        }
        Console.WriteLine(part2);
    }

    private int ScenicScore(int[,] grid, int y, int x)
    {
        var h = grid[y, x];
        var row = Row(grid, y);
        var l = FindVisible2(row.Take(x).Reverse(), h);
        var r = FindVisible2(row.Skip(x + 1), h);
        var col = Col(grid, x);
        var u = FindVisible2(col.Take(y).Reverse(), h);
        var d = FindVisible2(col.Skip(y + 1), h);
        return l * r * u * d;
    }

    private int FindVisible2(IEnumerable<int> trees, int h)
    {
        var result = 0;
        foreach (var t in trees)
        {
            result += 1;
            if (t >= h) break;
        }
        return result;
    }

    private IEnumerable<int> FindVisible(List<int> trees)
    {
        var curr = -1;
        for (var i = 0; i < trees.Count; i++)
        {
            if (trees[i] > curr)
            {
                curr = trees[i];
                yield return i;
            }
        }
    }

    private List<int> Col(int[,] grid, int x)
    {
        var result = new List<int>();
        for (int i = grid.GetLowerBound(0); i <= grid.GetUpperBound(0); i++)
        {
            result.Add(grid[i, x]);
        }
        return result;
    }

    private List<int> Row(int[,] grid, int y)
    {
        var result = new List<int>();
        for (int i = grid.GetLowerBound(1); i <= grid.GetUpperBound(1); i++)
        {
            result.Add(grid[y, i]);
        }
        return result;
    }

    private int[,] InitGrid()
    {
        var input = Lines("day8.txt");
        var m = input.Count;
        var n = input[0].Length;
        int[,] grid = new int[n, m];
        var y = 0;
        foreach (var line in input)
        {
            var x = 0;
            foreach (var h in line)
            {
                grid[y, x] = h - '0';
                x++;
            }
            y++;
        }
        return grid;
    }
}

