namespace AdventOfCode {

    class Day3 : IDay {
        public string Solve1() {
            var lines = File.ReadLines("./inputs/day3.txt").Append(null);
            var sum = 0;
            string?[] currentLines = { null, null, null };

            foreach (var line in lines) {
                currentLines[0] = currentLines[1];
                currentLines[1] = currentLines[2];
                currentLines[2] = line + '.';

                var middleLine = currentLines[1];
                if (middleLine == null) {
                    continue;
                }

                int? currentNumStart = null;
                int? currentNumEnd = null;

                for (int i = 0; i < middleLine.Length; i++) {
                    var ch = middleLine[i];
                    if (char.IsDigit(ch)) {
                        if (currentNumStart.HasValue) {
                            currentNumEnd = i;
                        } else {
                            currentNumStart = i;
                        }
                        continue;
                    }
                    if (currentNumStart.HasValue) {
                        int start = (int)currentNumStart;
                        if (!currentNumEnd.HasValue) {
                            currentNumEnd = start;
                        }
                        int end = (int)currentNumEnd;
                        var checkStart = Math.Max(0, start - 1);
                        var checkEnd = Math.Min(middleLine.Length - 1, end + 1);

                        var isPartNumber = false;
                        foreach (var currentLine in currentLines) {
                            if (currentLine == null) continue;
                            for (var j = checkStart; j <= checkEnd; j++) {
                                if (currentLine.Length > j && !char.IsDigit(currentLine[j]) && currentLine[j] != '.') {
                                    isPartNumber = true;
                                    break;
                                }
                            }
                            if (isPartNumber) break;
                        }
                        if (isPartNumber) {
                            var number = middleLine.Substring(start, end + 1 - start);
                            sum += int.Parse(number);
                        }
                        currentNumStart = null;
                        currentNumEnd = null;
                    }
                }
            }

            return sum.ToString();
        }


        public string Solve2() {
            var lines = File.ReadLines("./inputs/day3.txt").Append(null);
            var lineIndex = -1;
            string?[] currentLines = { null, null, null };
            Dictionary<string, int> potentialGears = new() { };
            Dictionary<string, int> gears = new() { };

            foreach (var line in lines) {
                lineIndex++;
                currentLines[0] = currentLines[1];
                currentLines[1] = currentLines[2];
                currentLines[2] = line + '.';

                var middleLine = currentLines[1];
                if (middleLine == null) {
                    continue;
                }

                int? currentNumStart = null;
                int? currentNumEnd = null;

                for (int i = 0; i < middleLine.Length; i++) {
                    var ch = middleLine[i];
                    if (char.IsDigit(ch)) {
                        if (currentNumStart.HasValue) {
                            currentNumEnd = i;
                        } else {
                            currentNumStart = i;
                        }
                        continue;
                    }
                    if (currentNumStart.HasValue) {
                        int start = (int)currentNumStart;
                        if (!currentNumEnd.HasValue) {
                            currentNumEnd = start;
                        }
                        int end = (int)currentNumEnd;
                        var checkStart = Math.Max(0, start - 1);
                        var checkEnd = Math.Min(middleLine.Length - 1, end + 1);

                        var k = -1;
                        var number = int.Parse(middleLine.Substring(start, end + 1 - start));
                        foreach (var currentLine in currentLines) {
                            k++;
                            if (currentLine == null) continue;
                            for (var j = checkStart; j <= checkEnd; j++) {
                                if (currentLine.Length > j && currentLine[j] == '*') {
                                    var key = $"{lineIndex + k}/{j}";
                                    var isPotentialGear = potentialGears.ContainsKey(key);
                                    if (isPotentialGear) {
                                        gears[key] = number * potentialGears[key];
                                    } else {
                                        potentialGears[key] = number;
                                    }

                                }
                            }
                        }
                        currentNumStart = null;
                        currentNumEnd = null;
                    }
                }
            }

            return gears.Values.Aggregate(0, (acc, v) => acc + v).ToString();
        }
    }
}
