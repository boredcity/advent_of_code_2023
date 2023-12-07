using System.Numerics;

namespace AdventOfCode {
    class Day5 : IDay {

        sealed record RangeWithShift(BigInteger from, BigInteger to, BigInteger shift);

        public string solve1() {
            var lines = File.ReadLines("./inputs/day5.txt");
            List<BigInteger> seeds = new();
            List<List<RangeWithShift>> converters = new() {};

            foreach (var line in lines) {
                if (line.StartsWith("seeds")) {
                    seeds = line.Split(": ")[1].Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                } else {
                    if (line.Length < 1) {
                        continue;
                    } else if (char.IsLetter(line[0])) {
                        converters.Add(new List<RangeWithShift>());
                    } else {
                        var parts = line.Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                        var destinationStart = parts[0];
                        var sourceStart = parts[1];
                        var rangeLength = parts[2];
                        converters[converters.Count - 1].Add(new RangeWithShift(sourceStart, sourceStart + rangeLength, destinationStart - sourceStart));
                    }
                }
            }

            BigInteger? minResult = null;

            foreach (var seed in seeds) {
                var currentValue = seed;
                foreach (var converter in converters) {
                    foreach (var condition in converter) {
                        if (currentValue >= condition.from && currentValue < condition.to) {
                            currentValue += condition.shift;
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
            var lines = File.ReadLines("./inputs/day5.txt");
            List<RangeWithShift> ranges = new();
            List<List<RangeWithShift>> converters = new() {};

            foreach (var line in lines) {
                if (line.StartsWith("seeds")) {
                    var allSeedNumbers = line.Split(": ")[1].Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                    for (var i = 0; i < allSeedNumbers.Count; i += 2) {
                        ranges.Add(new RangeWithShift(allSeedNumbers[i], allSeedNumbers[i] + allSeedNumbers[i+1], 0));
                    }
                } else {
                    if (line.Length < 1) {
                        continue;
                    } else if (char.IsLetter(line[0])) {
                        converters.Add(new List<RangeWithShift>());
                    } else {
                        var parts = line.Split(' ').Select(s => BigInteger.Parse(s)).ToList();
                        var destinationStart = parts[0];
                        var sourceStart = parts[1];
                        var rangeLength = parts[2];
                        converters[converters.Count - 1].Add(new RangeWithShift(sourceStart, sourceStart + rangeLength, destinationStart - sourceStart));
                    }
                }
            }

            foreach (var converter in converters) {
                foreach (var condition in converter) {
                    List<RangeWithShift> newRanges = new() {};
                    foreach (var curRange in ranges) {
                        var hasIntersection = (
                            curRange.from <= condition.from && condition.from < curRange.to
                        ) || (
                            curRange.from < condition.to && condition.to <= curRange.to
                        ) || (
                            condition.from <= curRange.from && curRange.to <= condition.to
                        );
                        if (!hasIntersection) {
                            newRanges.Add(curRange);
                            continue;
                        }
                        var intersectionStart = BigInteger.Max(curRange.from, condition.from);
                        var intersectionEnd = BigInteger.Min(curRange.to, condition.to);

                        if (curRange.from < intersectionStart) {
                            var beforeIntersectionRange = new RangeWithShift(
                                curRange.from,
                                intersectionStart,
                                0
                            );
                            newRanges.Add(beforeIntersectionRange);
                        }

                        var shiftedIntersectionRange = new RangeWithShift(
                            intersectionStart,
                            intersectionEnd,
                            condition.shift
                        );
                        newRanges.Add(shiftedIntersectionRange);
                        if (curRange.to > intersectionEnd) {
                            var afterIntersectionRange = new RangeWithShift(
                                intersectionEnd,
                                curRange.to,
                                0
                            );
                            newRanges.Add(afterIntersectionRange);
                        }
                    }
                    ranges = newRanges;
                }
                ranges = ranges.Select(r => new RangeWithShift(r.from + r.shift, r.to + r.shift, 0)).ToList();
            }

            return ranges.Aggregate(new BigInteger(-1), (acc, r) => (acc == -1 || r.from < acc) ? r.from : acc).ToString();
        }
    }
}
