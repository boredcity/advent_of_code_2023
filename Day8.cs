namespace AdventOfCode {
    class Day8 : IDay {
        record MapNode(string name, string left, string right);
        public string solve1() {
            var lines = File.ReadLines("./inputs/day8.txt");
            var i = -1;
            string instructions = "";
            Dictionary<string, MapNode> map = new() { };
            foreach (var line in lines) {
                i++;
                if (i == 0) {
                    instructions = line;
                } else if (line.Length > 0) {
                    var parts = line.Split(" = ");
                    var exits = parts[1].Substring(1, parts[1].Length - 2).Split(", ");
                    map[parts[0]] = new MapNode(parts[0], exits[0], exits[1]);
                }
            }

            var current = map["AAA"];
            var target = map["ZZZ"];
            var stepsTaken = 0;

            while (current != target) {
                var instruction = instructions[stepsTaken % instructions.Count()];
                stepsTaken++;
                current = map[instruction == 'L' ? current.left : current.right];
            }

            return stepsTaken.ToString();
        }

        public string solve2() {
            return "";
            // var lines = File.ReadLines("./inputs/day8.txt");
            // var i = -1;
            // string instructions = "";
            // Dictionary<string, MapNode> map = new() { };
            // foreach (var line in lines) {
            //     i++;
            //     if (i == 0) {
            //         instructions = line;
            //     } else if (line.Length > 0) {
            //         var parts = line.Split(" = ");
            //         var exits = parts[1].Substring(1, parts[1].Length - 2).Split(", ");
            //         map[parts[0]] = new MapNode(parts[0], exits[0], exits[1]);
            //     }
            // }

            // var startingPoints = map.Keys.Where(k => k.EndsWith('A')).Select(k => map[k]);
            // List<int> stepsToZ = new() { };
            // foreach (var startingPoint in startingPoints) {
            //     var current = startingPoint;
            //     var stepsTaken = 0;
            //     while (!current.name.EndsWith('Z')) {
            //         var instruction = instructions[stepsTaken % instructions.Count()];
            //         stepsTaken++;
            //         current = map[instruction == 'L' ? current.left : current.right];
            //     }
            //     stepsToZ.Add(stepsTaken);
            // }
            // return FindLCM(stepsToZ).ToString();
        }

        private static int FindLCM(List<int> numbers) {
            int result = numbers[0];
            for (int i = 1; i < numbers.Count; i++) {
                result = LCM(result, numbers[i]);
            }

            return result;
        }

        private static int LCM(int a, int b) {
            return Math.Abs(a * b) / GCD(a, b);
        }

        private static int GCD(int a, int b) {
            while (b != 0) {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}
