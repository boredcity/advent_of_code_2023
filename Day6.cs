using System.Numerics;

namespace AdventOfCode {
    class Day6 : IDay {
        public string solve1() {
            var lines = File.ReadLines("./inputs/day6.txt");
            
            List<List<int>> races = new() {};
            foreach (var line in lines) {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var parsed = split.Skip(1).Select(n => int.Parse(n)).ToList();
                var i = -1;
                foreach (var n in parsed) {
                    i++;
                    if (races.Count <= i) {
                        races.Add(new List<int> { n });
                    } else {
                        races[i].Add(n);
                    }

                }
            }

            var result = 1;
            foreach (var race in races) {
                var time = race[0];
                var bestDistance = race[1];
                var optionsCount = 0;
                for (var i = 1; i < time; i++) {
                    var distance = i * (time - i);
                    if (distance > bestDistance) {
                        optionsCount++;
                    }
                }
                result *= optionsCount;
            }

            return result.ToString();
        }

        public string solve2() {
            var lines = File.ReadLines("./inputs/day6.txt");
            
            List<BigInteger> race = new() {};
            foreach (var line in lines) {
                var value = BigInteger.Parse(string.Join("", line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
                race.Add(value); 
            }

            var time = race[0];
            var bestDistance = race[1];
            var optionsCount = 0;
            for (var i = 1; i < time; i++) {
                var distance = i * (time - i);
                if (distance > bestDistance) optionsCount++;
            }

            return optionsCount.ToString();
        }
    }
}
