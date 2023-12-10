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
            {'S', new (int, int)[] {(0, -1), (-1, 0), (1, 0), (0, 1)} }
        };

        private bool IsInBounds(List<string> rows, int rI, int cI) => !(rI < 0 || rI >= rows.Count || cI < 0 || cI >= rows[0].Length);

        public record Cell(char content, int rI, int cI) {
            public string GetId() => GetIdFromCoordinates(rI, cI);
            public static string GetIdFromCoordinates(int rI, int cI) =>  $"{rI}/{cI}";
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
                        new List<CellWithPath>(cur.path) {cur}
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
            // var rows = File.ReadLines("./inputs/day10.txt").ToList();
            // var loop = GetLoop(rows);

            // HashSet<string> inLoop = new() { };
            // loop.ForEach(cell => {
            //     inLoop.Add(cell.GetId());

            // });

            return "";
        }
    }
}
