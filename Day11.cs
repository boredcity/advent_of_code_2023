namespace AdventOfCode {
    class Day11 : IDay {
        public string Solve1() {
            List<string> map = File.ReadLines("./inputs/day11.txt").Select(line => line).ToList();
            int[] emptyPrevRows = new int[map.Count];
            int[] emptyPrevColumns = new int[map[0].Length];
            List<(int, int)> galaxies = new();

            for (var rI = 0; rI < map.Count; rI++) {
                var isEmptyRow = true;
                for (var cI = 0; cI < map.Count; cI++) {
                    if (map[rI][cI] == '#') {
                        isEmptyRow = false;
                        galaxies.Add((rI, cI));
                    }
                }
                if ((rI + 1) < emptyPrevRows.Length) {
                    emptyPrevRows[rI + 1] = emptyPrevRows[rI] + (isEmptyRow ? 1 : 0);
                }
            }

            for (var cI = 0; cI < map.Count; cI++) {
                var isEmptyColumn = true;
                for (var rI = 0; rI < map.Count; rI++) {
                    if (map[rI][cI] == '#') isEmptyColumn = false;
                }
                if ((cI + 1) < emptyPrevColumns.Length) {
                    emptyPrevColumns[cI + 1] = emptyPrevColumns[cI] + (isEmptyColumn ? 1 : 0);
                }
            }

            var distancesSum = 0;

            for (var g1i = 0; g1i < galaxies.Count - 1; g1i++) {
                var galaxy1 = galaxies[g1i];
                for (var g2i = g1i + 1; g2i < galaxies.Count; g2i++) {
                    var galaxy2 = galaxies[g2i];
                    var (g1r, g1c) = galaxy1;
                    var (g2r, g2c) = galaxy2;
                    var distanceR = g1r + emptyPrevRows[g1r] - g2r - emptyPrevRows[g2r];
                    var distanceC = g1c + emptyPrevColumns[g1c] - g2c - emptyPrevColumns[g2c];
                    var totalDistance = Math.Abs(distanceR) + Math.Abs(distanceC);
                    distancesSum += totalDistance;
                }
            }

            return distancesSum.ToString();
        }

        public string Solve2() {
            List<string> map = File.ReadLines("./inputs/day11.txt").Select(line => line).ToList();
            decimal[] emptyPrevRows = new decimal[map.Count];
            decimal[] emptyPrevColumns = new decimal[map[0].Length];
            List<(int, int)> galaxies = new();

            for (var rI = 0; rI < map.Count; rI++) {
                var isEmptyRow = true;
                for (var cI = 0; cI < map.Count; cI++) {
                    if (map[rI][cI] == '#') {
                        isEmptyRow = false;
                        galaxies.Add((rI, cI));
                    }
                }
                if ((rI + 1) < emptyPrevRows.Length) {
                    emptyPrevRows[rI + 1] = emptyPrevRows[rI] + (isEmptyRow ? 1000000m - 1 : 0);
                }
            }

            for (var cI = 0; cI < map.Count; cI++) {
                var isEmptyColumn = true;
                for (var rI = 0; rI < map.Count; rI++) {
                    if (map[rI][cI] == '#') isEmptyColumn = false;
                }
                if ((cI + 1) < emptyPrevColumns.Length) {
                    emptyPrevColumns[cI + 1] = emptyPrevColumns[cI] + (isEmptyColumn ? 1000000m - 1 : 0);
                }
            }

            var distancesSum = 0m;
            for (var g1i = 0; g1i < galaxies.Count - 1; g1i++) {
                var galaxy1 = galaxies[g1i];
                for (var g2i = g1i + 1; g2i < galaxies.Count; g2i++) {
                    var galaxy2 = galaxies[g2i];
                    var (g1r, g1c) = galaxy1;
                    var (g2r, g2c) = galaxy2;
                    var distanceR = g1r + emptyPrevRows[g1r] - g2r - emptyPrevRows[g2r];
                    var distanceC = g1c + emptyPrevColumns[g1c] - g2c - emptyPrevColumns[g2c];
                    var totalDistance = Math.Abs(distanceR) + Math.Abs(distanceC);
                    distancesSum += totalDistance;
                }
            }

            return distancesSum.ToString();
        }
    }
}
