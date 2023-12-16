namespace AdventOfCode {
    class Day16 : IDay {
        private bool isWithinMap(string[] map, int rI, int cI) => !(rI < 0 || cI < 0 || rI >= map.Length || cI >= map[0].Length);

        enum Direction { Up, Right, Down, Left }

        private static Dictionary<Direction, (int, int)> directionToShift = new() {
            { Direction.Up, (-1, 0) },
            { Direction.Down, (1, 0) },
            { Direction.Left, (0, -1) },
            { Direction.Right, (0, 1) },
        };
        public string Solve1() {
            var map = File.ReadLines("./inputs/day16.txt").ToArray();
            return GetEnergizedCount(map, (0, 0, Direction.Right)).ToString();
        }
        public string Solve2() {
            var map = File.ReadLines("./inputs/day16.txt").ToArray();
            var maxValue = 0;
            for (int rI = 0; rI < map.Length; rI++) {
                var isFirstRow = rI == 0;
                var isLastRow = rI == map.Length - 1;
                for (var cI = 0; cI < map[0].Length; cI++) {
                    var isFirstCell = cI == 0;
                    var isLastCell = cI == map[0].Length - 1;

                    if (isFirstRow || isLastRow) maxValue = Math.Max(
                        GetEnergizedCount(map, (rI, cI, isFirstRow ? Direction.Down : Direction.Up)),
                        maxValue
                    );
                    if (isFirstCell || isLastCell) maxValue = Math.Max(
                        GetEnergizedCount(map, (rI, cI, isFirstCell ? Direction.Right : Direction.Left)),
                        maxValue
                    );
                }
            }
            return maxValue.ToString();
        }
        private int GetEnergizedCount(string[] map, (int, int, Direction) initial) {
            Dictionary<(int, int), HashSet<Direction>> visited = new();
            Stack<(int, int, Direction)> toCheck = new();
            toCheck.Push(initial);

            while (toCheck.Count > 0) {
                var (rI, cI, dir) = toCheck.Pop();
                if (!isWithinMap(map, rI, cI)) continue;
                if (!visited.ContainsKey((rI, cI))) visited[(rI, cI)] = new HashSet<Direction>();
                if (visited[(rI, cI)].Contains(dir)) continue;
                else visited[(rI, cI)].Add(dir);

                var cur = map[rI][cI];
                var (dirR, dirC) = directionToShift[dir];
                switch (cur) {
                    case '.':
                        var newR = rI + dirR;
                        var newC = cI + dirC;
                        toCheck.Push((newR, newC, dir));
                        break;
                    case '/':
                        toCheck.Push(dir switch {
                            Direction.Up => (rI, cI + 1, Direction.Right),
                            Direction.Down => (rI, cI - 1, Direction.Left),
                            Direction.Left => (rI + 1, cI, Direction.Down),
                            Direction.Right => (rI - 1, cI, Direction.Up),
                            _ => throw new ArgumentException($"Unknown direction {dir}")
                        });
                        break;
                    case '\\':
                        toCheck.Push(dir switch {
                            Direction.Up => (rI, cI - 1, Direction.Left),
                            Direction.Down => (rI, cI + 1, Direction.Right),
                            Direction.Left => (rI - 1, cI, Direction.Up),
                            Direction.Right => (rI + 1, cI, Direction.Down),
                            _ => throw new ArgumentException($"Unknown direction {dir}")
                        });
                        break;
                    case '|':
                        (int, int, Direction)[] verticalResults = dir switch {
                            Direction.Up => new[] { (rI - 1, cI, Direction.Up) },
                            Direction.Down => new[] { (rI + 1, cI, Direction.Down) },
                            _ => new[] { (rI + 1, cI, Direction.Down), (rI - 1, cI, Direction.Up) }
                        };
                        foreach (var res in verticalResults) toCheck.Push(res);
                        break;
                    case '-':
                        (int, int, Direction)[] horizontalResults = dir switch {
                            Direction.Left => new[] { (rI, cI - 1, Direction.Left) },
                            Direction.Right => new[] { (rI, cI + 1, Direction.Right) },
                            _ => new[] { (rI, cI - 1, Direction.Left), (rI, cI + 1, Direction.Right) }
                        };
                        foreach (var res in horizontalResults) toCheck.Push(res);
                        break;
                    default:
                        throw new ArgumentException($"Unexpected char {cur}");
                }
            }

            return visited.Count;
        }
    }
}
