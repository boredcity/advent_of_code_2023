using System.Text;

namespace AdventOfCode {
    class Day12 : IDay {
        public string Solve1() {
            return File.ReadLines("./inputs/day12-test.txt").Aggregate(0m, (sum, line) => {
                var parts = line.Split(' ');
                var (scheme, brokenRanges) = (parts[0], parts[1].Split(',').Select(n => int.Parse(n)).ToList());
                var availableIntervals = scheme.Split('.', StringSplitOptions.RemoveEmptyEntries);
                var possibleWaysCount = GetPossiblePlacementsCount(brokenRanges, availableIntervals, 0, 0, new Dictionary<string, decimal>());
                return sum + possibleWaysCount;
            }).ToString();
        }

        public string Solve2() {
            var i = 0;
            return File.ReadLines("./inputs/day12-test.txt").Aggregate(0m, (sum, line) => {
                var parts = line.Split(' ');
                var (scheme, brokenRanges) = ($"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}", $"{parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}".Split(',').Select(n => int.Parse(n)).ToList());
                var availableIntervals = scheme.Split('.', StringSplitOptions.RemoveEmptyEntries);
                var possibleWaysCount = GetPossiblePlacementsCount(brokenRanges, availableIntervals, 0, 0, new Dictionary<string, decimal>());
                Console.WriteLine($"{++i}: {possibleWaysCount}");
                return sum + possibleWaysCount;
            }).ToString();
        }

        private decimal GetPossiblePlacementsCount(
            List<int> rangesToFit,
            string[] availableIntervals,
            int rangesToFitIndex,
            int availableIntervalsIndex,
            Dictionary<string, decimal> memo
        ) {
            if (rangesToFitIndex >= rangesToFit.Count) return availableIntervals.Skip(availableIntervalsIndex).Any(i =>i.Contains('#')) ? 0 : 1;
            if (availableIntervalsIndex >= availableIntervals.Length) return 0;
            var intervalToUse = availableIntervals[availableIntervalsIndex];
            var rangeToSubtract = rangesToFit[rangesToFitIndex];

            var memoKey = $"{rangesToFitIndex}/{availableIntervalsIndex}/{intervalToUse}";
            if (memo.ContainsKey(memoKey)) return memo[memoKey];

            var resultOfSkippingInterval = GetPossiblePlacementsCount(rangesToFit, availableIntervals, rangesToFitIndex, availableIntervalsIndex + 1, memo);
            var maxWaysToFitCount = intervalToUse.Length - rangeToSubtract + 1;
            var resultsOfUsingInterval = 0m;
            var nextRange = (rangesToFitIndex + 1) >= rangesToFit.Count ? 0 : rangesToFit[rangesToFitIndex + 1];
            for (var offset = 0; offset < maxWaysToFitCount; offset++) {
                var dividerIndex = rangeToSubtract + offset;
                if (
                    (offset > 0 && intervalToUse[offset - 1] == '#') ||
                    (dividerIndex < intervalToUse.Length && intervalToUse[dividerIndex] == '#')
                ) continue; // cannot start right after or end right before "#"

                if (nextRange > intervalToUse.Length - dividerIndex - 1) {
                    resultsOfUsingInterval += GetPossiblePlacementsCount(rangesToFit, availableIntervals, rangesToFitIndex + 1, availableIntervalsIndex + 1, memo);
                } else {
                    var intervalsAfterRangeFit = (string[])availableIntervals.Clone();
                    var updatedInterval = intervalsAfterRangeFit[availableIntervalsIndex].Substring(dividerIndex + 1);
                    intervalsAfterRangeFit[availableIntervalsIndex] = updatedInterval;
                    resultsOfUsingInterval += GetPossiblePlacementsCount(rangesToFit, intervalsAfterRangeFit, rangesToFitIndex + 1, availableIntervalsIndex, memo);
                }
            }
            memo[memoKey] = resultOfSkippingInterval + resultsOfUsingInterval;
            return resultOfSkippingInterval + resultsOfUsingInterval;
        }
    }
}
