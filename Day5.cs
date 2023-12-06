using System.Diagnostics;
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

        record Range(BigInteger from, BigInteger to);
        record ConverterRange(BigInteger from, BigInteger to, BigInteger shift);

        public string solve2() {
            var lines = File.ReadLines("./inputs/day5.txt");
            List<Range> ranges = new();
            List<List<ConverterRange>> converters = new() {};

            foreach (var line in lines) {
                if (line.StartsWith("seeds")) {
                    var allSeedNumbers = line.Split(": ")[1].Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                    for (var i = 0; i < allSeedNumbers.Count; i += 2) {
                        var pair = new Range(allSeedNumbers[0], allSeedNumbers[0] + allSeedNumbers[1]);
                        ranges.Add(pair);
                    }
                } else {
                    if (line.Length < 1) {
                        continue;
                    } else if (char.IsLetter(line[0])) {
                        converters.Add(new List<ConverterRange>());
                    } else {
                        var parts = line.Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                        converters[converters.Count - 1]?.Add(new ConverterRange(parts[1], parts[1] + parts[2], parts[0]));
                    }
                }
            }

            foreach(var r in ranges) {

                foreach(var conv in converters) {
                    foreach(var convInternal in conv) {
                        if (r.from >= convInternal.from || r.to <= convInternal.to) {
                            Console.WriteLine(convInternal.shift);

                        }
                    }
                }
            }
            return "";
        }
    }
}
