using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;
    
    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        foreach (Match match in Regex.Matches(_input, @"mul\((\d{1,3}),(\d{1,3})\)"))
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var enabled = true;
        var sum = 0;
        foreach (Match match in Regex.Matches(_input, @"(?:mul\((\d{1,3}),(\d{1,3})\))|(?:do\(\))|(?:don't\(\))"))
        {
            if (match.Value == "do()")
            {
                enabled = true;
                continue;
            }
            
            if (match.Value == "don't()")
            {
                enabled = false;
                continue;
            }

            if (enabled)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }
        
        return new(sum.ToString());
    }
}