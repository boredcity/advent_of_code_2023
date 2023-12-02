using System.Linq;

namespace AdventOfCode {

    class Day2 : IDay {

        private Dictionary<string, int> maxCount = new() {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };

        public string solve1() {
            var lines = File.ReadLines("./inputs/day2.txt");
            var sum = 0;
            foreach (var line in lines) {
                var titleAndBody = line.Split(": ");
                var id = int.Parse(titleAndBody[0].Split(' ')[1]);
                var rounds = titleAndBody[1].Split("; ");

                var isValid = true;
                foreach (var round in rounds) {
                    var info = round.Split(", ");
                    foreach (var pair in info) {
                        var splitPair = pair.Split(' ');
                        var maxCountForColor = maxCount[splitPair[1]];
                        if (int.Parse(splitPair[0]) > maxCountForColor) {
                            isValid = false;
                            break;
                        }
                    }
                    if (!isValid) {
                        break;
                    }
                }
                if (isValid) {
                    sum += id;
                }

            }
            return sum.ToString();
        }

        public string solve2() {
            var lines = File.ReadLines("./inputs/day2.txt");
            var sumOfPowers = 0;
            foreach (var line in lines) {
                var titleAndBody = line.Split(": ");
                var id = int.Parse(titleAndBody[0].Split(' ')[1]);
                var rounds = titleAndBody[1].Split("; ");
                Dictionary<string, int> seenCubeCount = new() {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 },
                };
                foreach (var round in rounds) {
                    var info = round.Split(", ");
                    foreach (var pair in info) {
                        var splitPair = pair.Split(' ');
                        var currentCount = seenCubeCount[splitPair[1]];
                        var intoCount = int.Parse(splitPair[0]);
                        seenCubeCount[splitPair[1]] = Math.Max(currentCount, intoCount);
                    }
                }
                sumOfPowers += seenCubeCount.Values.Aggregate(1, (acc, val) => acc * val);

            }
            return sumOfPowers.ToString();
        }
    }
}
