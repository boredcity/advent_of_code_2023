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
                    var neighbor = new CellWithPath(
                        rows[neighborRI][neighborCI],
                        neighborRI,
                        neighborCI,
                        new List<CellWithPath>(cur.path) { cur }
                    );
                    var hasReversePath = Array.Exists(charToShifts[neighbor.content], neighborShift => neighborShift == (-shiftRI, -shiftCI));
                    if (!hasReversePath) continue;
                    if (!visited.Contains(neighbor.GetId())) {
                        planned.Push(neighbor);
                    } else if (neighbor.content == 'S' && cur.path.Count > 2) { // already seen; does it close the loop?
                        loop = neighbor.path;
                        break;
                    }
                }
            }
            return loop;
        }

        public string solve1() {
            var rows = File.ReadLines("./inputs/day10.txt").ToList();
            return ((GetLoop(rows).Count + 1) / 2).ToString();
        }


        public string solve2() {
            var rows = File.ReadLines("./inputs/day10.txt").ToList();
            var loop = GetLoop(rows);

            string directions = "";
            var (rIFirstShift, cIFirstShift) = (loop[1].rI - loop[0].rI, loop[1].cI - loop[0].cI);
            var (rILastShift, cILastShift) = (loop.Last().rI - loop[0].rI, loop.Last().cI - loop[0].cI);

            if (cIFirstShift == 1 || cILastShift == 1) directions += 'R';
            if (cIFirstShift == -1 || cILastShift == -1) directions += 'L';
            if (rIFirstShift == 1 || rILastShift == 1) directions += 'B';
            if (rIFirstShift == -1 || rILastShift == -1) directions += 'T';

            char startContent;
            switch (directions) {
                case "RL": startContent = '-'; break;
                case "RB": startContent = 'F'; break;
                case "RT": startContent = 'L'; break;
                case "LB": startContent = '7'; break;
                case "LT": startContent = 'J'; break;
                default: startContent = '|'; break;
            }

            var loopSet = loop.Aggregate(new HashSet<string>(), (acc, cell) => {
                acc.Add(cell.GetId());
                return acc;
            });
            var cleanRows = rows.Select((row, rI) => row.Select(
                (ch, cI) => {
                    var id = Cell.GetIdFromCoordinates(rI, cI);
                    if (loopSet.Contains(id)) {
                        return id == loop[0].GetId() ? startContent : ch;
                    } else {
                        return ' ';
                    }
                }).ToList()).ToList();

            var count = 0;
            for (var rI = 0; rI < cleanRows.Count; rI++) {
                var isInsideTheLoop = false;
                char? lastUnpaired = null;
                for (var cI = 0; cI < cleanRows[rI].Count; cI++) {
                    var cellValue = cleanRows[rI][cI];
                    if (cellValue == ' ') {
                        if (isInsideTheLoop) {
                            count++;
                            cleanRows[rI][cI] = 'I';
                        }
                    } else if (cellValue == '|') {
                        isInsideTheLoop = !isInsideTheLoop;
                    } else if (cellValue == 'L' || cellValue == 'F') {
                        lastUnpaired = cellValue;
                    } else if (
                        (lastUnpaired == 'L' && cellValue == '7') ||
                        (lastUnpaired == 'F' && cellValue == 'J')
                    ) {
                        lastUnpaired = null;
                        isInsideTheLoop = !isInsideTheLoop;
                    }
                }
            }

            return count.ToString();
        }
    }
}
