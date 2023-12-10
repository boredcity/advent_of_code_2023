namespace AdventOfCode {
    class Day10 : IDay {
        private static Dictionary<char, (int, int)[]> charToShifts = new Dictionary<char, (int, int)[]>
        {
            {'-', new (int, int)[] { (0, -1), (0, 1) }},
            {'|', new (int, int)[] {(-1, 0), (1, 0)} },
            {'F', new (int, int)[] {(1, 0), (0, 1)} },
            {'L', new (int, int)[] {(-1, 0), (0, 1)} },
            {'7', new (int, int)[] {(1, 0), (0, -1)} },
            {'J', new (int, int)[] {(-1, 0), (0, - 1)} },
            {'S', new (int, int)[] {(0, -1), (-1, 0), (1, 0), (0, 1)} },
            {'.', new (int, int)[] {}},
        };

        private bool IsInBounds(List<string> rows, int rI, int cI) => !(rI < 0 || rI >= rows.Count || cI < 0 || cI >= rows[0].Length);

        public record Cell(char content, int rI, int cI) {
            public string GetId() => GetIdFromCoordinates(rI, cI);
            public static string GetIdFromCoordinates(int rI, int cI) => $"{rI}/{cI}";
            public override string ToString() => $"Cell {{ {content} at {rI}/{cI} }}";
        };

        sealed public record CellWithPath(char content, int rI, int cI, List<CellWithPath> path) : Cell(content, rI, cI) {
            public override string ToString() => $"Cell {{ \"{content}\" at {rI}/{cI}, path: {path.Count} }}";
        };

        private List<CellWithPath> GetLoop(List<string> rows) {
            CellWithPath? start = null;
            for (var rI = 0; rI < rows.Count; rI++) {
                for (var cI = 0; cI < rows[rI].Length; cI++) {
                    if (rows[rI][cI] == 'S') {
                        start = new CellWithPath('S', rI, cI, new List<CellWithPath>());
                        break;
                    }
                }
                if (start != null) break;
            }
            if (start == null) throw new ArgumentException("No cell marked \"S\" in the input");

            var loop = new List<CellWithPath>();
            Stack<CellWithPath> planned = new(new[] { start });

            HashSet<string> visited = new();
            while (loop.Count == 0 && planned.Count > 0) {
                var cur = planned.Pop();
                visited.Add(cur.GetId());
                foreach (var shift in charToShifts[cur.content]) {
                    var (shiftRI, shiftCI) = shift;
                    var neighborRI = shiftRI + cur.rI;
                    var neighborCI = shiftCI + cur.cI;
                    if (!IsInBounds(rows, neighborRI, neighborCI)) continue;
                    var content = rows[neighborRI][neighborCI];
                    var pathToNeighbor = new List<CellWithPath>(cur.path) { cur };
                    if (!visited.Contains(Cell.GetIdFromCoordinates(neighborRI, neighborCI))) {
                        var neighbor = new CellWithPath(
                            content,
                            neighborRI,
                            neighborCI,
                            pathToNeighbor
                        );
                        var neighborHasInvertedShift = Array.Exists(charToShifts[neighbor.content], neighborShift => neighborShift == (-shiftRI, -shiftCI));
                        if (!neighborHasInvertedShift) continue;
                        planned.Push(neighbor);
                    } else if (content == 'S' && cur.path.Count > 2) { // already seen; does it close the loop?
                        loop = pathToNeighbor;
                        break;
                    }
                }
            }
            return loop;
        }

        public string Solve1() {
            var rows = File.ReadLines("./inputs/day10.txt").ToList();
            return ((GetLoop(rows).Count + 1) / 2).ToString();
        }

        private char getStartContent(List<CellWithPath> loop) {
            string directions = "";
            var (rIFirstShift, cIFirstShift) = (loop[1].rI - loop[0].rI, loop[1].cI - loop[0].cI);
            var (rILastShift, cILastShift) = (loop.Last().rI - loop[0].rI, loop.Last().cI - loop[0].cI);

            if (cIFirstShift == 1 || cILastShift == 1) directions += 'R';
            if (cIFirstShift == -1 || cILastShift == -1) directions += 'L';
            if (rIFirstShift == 1 || rILastShift == 1) directions += 'B';
            if (rIFirstShift == -1 || rILastShift == -1) directions += 'T';

            switch (directions) {
                case "RL": return '-';
                case "RB": return 'F';
                case "RT": return 'L';
                case "LB": return '7';
                case "LT": return 'J';
                default: return '|';
            }
        }

        public string Solve2() {
            var rows = File.ReadLines("./inputs/day10.txt").ToList();
            var loop = GetLoop(rows);
            var loopSet = new HashSet<string>();
            loop.ForEach(cell => loopSet.Add(cell.GetId()));

            var insideLoopCount = 0;
            for (var rI = 0; rI < rows.Count; rI++) {
                var isInsideTheLoop = false;
                char? lastUnpaired = null;
                for (var cI = 0; cI < rows[rI].Length; cI++) {
                    var cellContent = rows[rI][cI];
                    if (cellContent == 'S') cellContent = getStartContent(loop);
                    if (!loopSet.Contains(Cell.GetIdFromCoordinates(rI, cI))) {
                        if (isInsideTheLoop) insideLoopCount++;
                    } else if (cellContent == '|') {
                        isInsideTheLoop = !isInsideTheLoop;
                    } else if (cellContent == 'L' || cellContent == 'F') {
                        lastUnpaired = cellContent;
                    } else if (
                        (lastUnpaired == 'L' && cellContent == '7') ||
                        (lastUnpaired == 'F' && cellContent == 'J')
                    ) {
                        lastUnpaired = null;
                        isInsideTheLoop = !isInsideTheLoop;
                    }
                }
            }

            return insideLoopCount.ToString();
        }
    }
}
