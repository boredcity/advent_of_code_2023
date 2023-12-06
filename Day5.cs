using System.Numerics;

namespace AdventOfCode {
    class Day5 : IDay {
        public string solve1() {
            var lines = File.ReadLines("./inputs/day5.txt");
            List<BigInteger> seeds = new();
            List<List<List<BigInteger>>> converters = new() {};

            foreach (var line in lines) {
                if (line.StartsWith("seeds")) {
                    seeds = line.Split(": ")[1].Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                } else {
                    if (line.Length < 1) {
                        continue;
                    } else if (char.IsLetter(line[0])) {
                        converters.Add(new List<List<BigInteger>>());
                    } else {
                        converters[converters.Count - 1].Add(line.Split(' ').Select(s => BigInteger.Parse(s)).ToList());
                    }
                }
            }

            BigInteger? minResult = null;

            foreach (var seed in seeds) {
                var currentValue = seed;
                foreach (var converter in converters) {
                    foreach (var converterOption in converter) {
                        var destRangeStart = converterOption[0];
                        var sourceRangeStart = converterOption[1];
                        var rangeLen = converterOption[2];
                        var shift = destRangeStart - sourceRangeStart;
                        var sourceRangeEnd = sourceRangeStart + rangeLen;
                        if (currentValue >= sourceRangeStart && currentValue < sourceRangeEnd) {
                            currentValue += shift;
                            break;
                        }
                    }
                }
                if (!minResult.HasValue || minResult > currentValue) {
                    minResult = currentValue;
                }
            }

            return minResult?.ToString() ?? "No solution";
        }

        public string solve2() {
            // var lines = File.ReadLines("./inputs/day5.txt");
            // List<List<BigInteger>> ranges = new();
            // List<List<List<BigInteger>>> converters = new() {};

            // foreach (var line in lines) {
            //     if (line.StartsWith("seeds")) {
            //         var allSeedNumbers = line.Split(": ")[1].Split(' ').Select(s => BigInteger.Parse(s)).ToList();
            //         for (var i = 0; i < allSeedNumbers.Count; i += 2) {
            //             List<BigInteger> pair = new() {allSeedNumbers[0], allSeedNumbers[0] + allSeedNumbers[1]};
            //             ranges.Add(pair);
            //         }
            //     } else {
            //         if (line.Length < 1) {
            //             continue;
            //         } else if (char.IsLetter(line[0])) {
            //             converters.Add(new List<List<BigInteger>>());
            //         } else {
            //             converters[converters.Count - 1].Add(line.Split(' ').Select(s => BigInteger.Parse(s)).ToList());
            //         }
            //     }
            // }

            // foreach (var converter in converters) {
            //     List<List<BigInteger>> temp = new() {};
            //     List<List<BigInteger>> used = new() {};
            //     foreach (var prevStepRange in ranges) {
            //         foreach (var converterOption in converter) {
            //             var destRangeStart = converterOption[0];
            //             var sourceRangeStart = converterOption[1];
            //             var rangeLen = converterOption[2];
            //             var shift = destRangeStart - sourceRangeStart;
            //             var sourceRangeEnd = sourceRangeStart + rangeLen;
            //             var prevStart = prevStepRange[0];
            //             var prevEnd = prevStepRange[1];
            //             List<BigInteger> usedRange = new() { BigInteger.Max(prevStart, sourceRangeStart), BigInteger.Min(prevEnd, sourceRangeEnd)};
            //             if (usedRange[0] < usedRange[1]) {
            //                 List<BigInteger> resultingRange = new() {
            //                     usedRange[0] + shift,
            //                     usedRange[1] + shift
            //                 };
            //                 temp.Add(resultingRange);
            //                 used.Add(usedRange);
            //             }
            //         }

            //         // add 1=1 mapping
                    
            //         Console.WriteLine($"temp.Count {temp.Count}");
            //     }
            //     ranges = temp;
            // }

            // ranges.ForEach(r => Console.WriteLine(r[0]));

            // return ranges.Aggregate(ranges[0][0], (acc, r) => r[0] < acc ? r[0] : acc).ToString() ?? "No solution";
            return "WiP";
        }
    }
}
