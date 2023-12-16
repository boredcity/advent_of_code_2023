namespace AdventOfCode {

    class Day14 : IDay {

        public string Solve1() {
            var map = File.ReadLines("./inputs/day14.txt").ToList();
            var weightsSum = 0;
            for (var cI = 0; cI < map[0].Length; cI++) {
                var stopIndex = 0;
                for (var rI = 0; rI < map.Count; rI++) {
                    var cell = map[rI][cI];
                    if (cell == '#') stopIndex = rI + 1;
                    else if (cell == 'O') weightsSum += map.Count - stopIndex++;
                }
            }
            return weightsSum.ToString();
        }

        public string Solve2() {
            // var map = File.ReadLines("./inputs/day14.txt").ToList();
            // HashSet<(int, int)> blockers = new();
            // List<(int, int)> rollers = new();
            // (int, int)[] directions = new (int, int)[] {
            //     (-1, 0), (0, -1), (1, 0), (0, 1)
            // };
            // for (var rI = 0; rI < map.Count; rI++) {
            //     for (var cI = 0; cI < map[0].Length; cI++) {
            //         var cell = map[rI][cI];
            //         if (cell == '#') blockers.Add((rI, cI));
            //         if (cell == 'O') rollers.Add((rI, cI));
            //     }
            // }
            // for (var i = 0m; i < 1000000000m; i++) {
            //     foreach (var direction in directions) {
            //         var (dirRowAdj, dirColAdj) = direction;
            //         for (var rollerI = 0; rollerI < rollers.Count; rollerI++) {
            //             var (rI, cI) = rollers[rollerI];
            //             while (!blockers.Contains((rI, cI))) {
            //                 rI += dirRowAdj;
            //                 cI += dirColAdj;
            //             }
                        
            //         }
            //     }
            // }
            return "";
        }
    }
}

