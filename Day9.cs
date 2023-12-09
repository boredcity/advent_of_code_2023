namespace AdventOfCode {
    class Day9 : IDay {

        public string solve1() {
            var lines = File.ReadLines("./inputs/day9.txt");
            List<int> results = new() {};
            
            foreach (var line in lines) {
                var lineNumbers = line.Split(' ').Select(int.Parse).ToList();
                List<List<int>> iterations = new() { lineNumbers };
                while (true) {
                    var differences = new List<int>() {};
                    var onlySame = true;
                    var lastIteration = iterations.Last();
                    for (var i = 1; i < lastIteration.Count; i++) {
                        var dif = lastIteration[i] - lastIteration[i - 1];
                        if (lastIteration[i] != lastIteration[i - 1]) onlySame = false;
                        differences.Add(dif);
                    }
                    iterations.Add(differences);
                    if (onlySame) break;
                }

                iterations.Reverse();
                results.Add(iterations.Aggregate(0, (acc, iteration) => iteration.Last() + acc));

            }
            return results.Sum().ToString();
        }

        public string solve2() {
            var lines = File.ReadLines("./inputs/day9.txt");
            List<int> results = new() {};
            
            foreach (var line in lines) {
                var lineNumbers = line.Split(' ').Select(int.Parse).ToList();
                List<List<int>> iterations = new() { lineNumbers };
                while (true) {
                    var differences = new List<int>() {};
                    var onlySame = true;
                    var lastIteration = iterations.Last();
                    for (var i = 1; i < lastIteration.Count; i++) {
                        var dif = lastIteration[i] - lastIteration[i - 1];
                        if (lastIteration[i] != lastIteration[i - 1]) onlySame = false;
                        differences.Add(dif);
                    }
                    iterations.Add(differences);
                    if (onlySame) break;
                }

                iterations.Reverse();
                results.Add(iterations.Aggregate(0, (acc, iteration) => iteration[0] - acc));

            }
            return results.Sum().ToString();
        }
    }
}
