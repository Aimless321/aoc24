namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input;
    private readonly List<(int a, int b)> _rules;
    private readonly List<string> _incorrect;
    
    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
        _rules = new List<(int a, int b)>();
        _incorrect = new List<string>();
    }
    
    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        
        foreach (var line in _input)
        {
            if (line.Equals(""))
            {
                continue;
            }

            if (line.Contains("|"))
            {
                var parts = line.Split('|');
                _rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                continue;
            }

            var pages = line.Split(",").Select(int.Parse).ToList();

            if (IsValid(pages))
            {
                sum += pages[pages.Count / 2];
            }
            else
            {
                _incorrect.Add(line);
            }
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        
        foreach (var update in _incorrect)
        {
            var pages = update.Split(",").Select(int.Parse).ToList();
            while (!IsValid(pages))
            {
                foreach ((int a, int b) in _rules)
                {
                    if (!pages.Contains(a) || !pages.Contains(b))
                    {
                        continue;
                    }
                    
                    if (pages.IndexOf(a) > pages.IndexOf(b))
                    {
                        (pages[pages.IndexOf(a)], pages[pages.IndexOf(b)]) = (pages[pages.IndexOf(b)], pages[pages.IndexOf(a)]);
                    }
                }
            }
            
            sum += pages[pages.Count / 2];
        }
        
        return new(sum.ToString());
    }

    private bool IsValid(List<int> pages)
    {
        var valid = true;
        foreach ((int a, int b) in _rules)
        {
            if (!pages.Contains(a) || !pages.Contains(b))
            {
                continue;
            }

            if (pages.IndexOf(a) > pages.IndexOf(b))
            {
                valid = false;
                break;
            }
        }

        return valid;
    }
}