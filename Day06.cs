using System.Numerics;

namespace AdventOfCode {
    class Day06 : IDay {
        sealed private record Race<T>(T time, T bestDistance);

        public string Solve1() {
            var lines = File.ReadLines("./inputs/day06.txt");
            
            List<Race<int>> races = new() {};
            List<int> times = new() {};
            foreach (var line in lines) {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var parsed = split.Skip(1).Select(n => int.Parse(n)).ToList();
                var i = -1;
                foreach (var n in parsed) {
                    i++;
                    if (times.Count <= i) {
                        times.Add(n);
                    } else {
                        races.Add(new Race<int>(times[i], n));
                    }

                }
            }

            var result = 1;
            foreach (var race in races) {
                var optionsCount = 0;
                for (var i = 1; i < race.time; i++) {
                    var distance = i * (race.time - i);
                    if (distance > race.bestDistance) {
                        optionsCount++;
                    }
                }
                result *= optionsCount;
            }

            return result.ToString();
        }

        public string Solve2() {
            var lines = File.ReadLines("./inputs/day06.txt");
            
            List<BigInteger> parts = new() {};
            foreach (var line in lines) {
                var value = BigInteger.Parse(string.Join("", line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
                parts.Add(value); 
            }

            var race = new Race<BigInteger>(parts[0], parts[1]);

            var optionsCount = 0;
            for (var i = 1; i < race.time; i++) {
                var distance = i * (race.time - i);
                if (distance > race.bestDistance) optionsCount++;
            }

            return optionsCount.ToString();
        }
    }
}
