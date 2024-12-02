namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly IEnumerable<string> _input;

    public Day02()
    {
        _input = File.ReadLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int reports = 0;
        foreach (var line in _input)
        {
            if (IsSafe(line.Split(' ')))
            {
                reports++;
            }
        }

        return new(reports.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int reports = 0;
        foreach (var line in _input)
        {
            var nums = line.Split(' ');

            if (IsSafe(nums))
            {
                reports++;
                continue;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                var removed = nums.Where((item, index) => index != i).ToArray();
                if (IsSafe(removed))
                {
                    reports++;
                    break;
                }
            }
        }

        return new(reports.ToString());
    }

    private bool IsSafe(string[] chars)
    {
        bool safe = true;
        bool? isGoingUp = null;
        int? lastNum = null;
        foreach (var c in chars)
        {
            var num = int.Parse(c);

            if (!lastNum.HasValue)
            {
                lastNum = num;
                continue;
            }

            if (num == lastNum)
            {
                safe = false;
                break;
            }
                
            if (!isGoingUp.HasValue)
            {
                isGoingUp = num > lastNum;
            }

            if (isGoingUp.Value && num < lastNum || !isGoingUp.Value && num > lastNum)
            {
                safe = false;
                break;
            }

            if (Math.Abs(num - lastNum.Value) > 3)
            {
                safe = false;
                break;
            }

            lastNum = num;
        }

        return safe;
    }
}
