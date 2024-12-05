namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    private readonly int _rows;
    private readonly int _cols;

    private static readonly (int dr, int dc)[] _dirs =
    [
        (0, 1),
        (1, 1),
        (1, 0),
        (1, -1),
        (0, -1),
        (-1, -1),
        (-1, 0),
        (-1, 1)
    ];

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
        _rows = _input.Length;
        _cols = _input[0].Length;
    }

    public override ValueTask<string> Solve_1()
    {
        var xmasCount = 0;

        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _cols; col++)
            {
                if (_input[row][col] != 'X')
                    continue;

                foreach (var (dr, dc) in _dirs)
                {
                    var r = row;
                    var c = col;

                    if (!IsInBounds(r + dr * 3, c + dc * 3))
                        continue;

                    if (_input[r + dr][c + dc] == 'M' &&
                        _input[r + dr * 2][c + dc * 2] == 'A' &&
                        _input[r + dr * 3][c + dc * 3] == 'S')
                    {
                        xmasCount++;
                    }
                }
            }
        }

        return new(xmasCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var xPatternCount = 0;

        for (var row = 1; row < _rows - 1; row++)
        {
            for (var col = 1; col < _cols - 1; col++)
            {
                if (_input[row][col] != 'A')
                    continue;

                var hasValidPattern = CheckForXPattern(row, col);
                if (hasValidPattern)
                    xPatternCount++;
            }
        }

        return new(xPatternCount.ToString());
    }

    private bool CheckForXPattern(int row, int col)
    {
        var leftDiag = CheckDiagonal(row - 1, col - 1, row + 1, col + 1);
        var rightDiag = CheckDiagonal(row - 1, col + 1, row + 1, col - 1);

        return leftDiag && rightDiag;
    }

    private bool CheckDiagonal(int r1, int c1, int r2, int c2) =>
        (_input[r1][c1] == 'M' && _input[r2][c2] == 'S') ||
        (_input[r1][c1] == 'S' && _input[r2][c2] == 'M');

    private bool IsInBounds(int row, int col) =>
        row >= 0 && row < _rows && col >= 0 && col < _cols;
}