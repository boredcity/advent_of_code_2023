namespace AdventOfCode {

    class Day1 : IDay {

        public string Solve1() {
            var lines = File.ReadLines("./inputs/day1.txt");
            var sum = 0;
            foreach (var line in lines) {
                char? first = null;
                char? last = null;
                foreach (var lineChar in line) {
                    if (char.IsDigit(lineChar)) {
                        last = lineChar;
                        if (!first.HasValue) {
                            first = lineChar;
                        }
                    }
                }
                var result = $"{first}{last}";
                sum += int.Parse(result);
            }
            return sum.ToString();
        }

        public string Solve2() {
            var lines = File.ReadLines("./inputs/day1.txt");
            var sum = 0;
            foreach (var line in lines) {
                char? first = null;
                char? last = null;
                var wordToCurrentMatch = new Dictionary<string, string>();
                foreach (var kvp in wordToDigit) {
                    wordToCurrentMatch[kvp.Key] = "";
                }

                foreach (var lineChar in line) {
                    char? digit = null;
                    if (char.IsDigit(lineChar)) {
                        digit = lineChar;
                    } else {
                        // greedy; will not work for something like "ninine"
                        foreach (var kvp in wordToCurrentMatch) {
                            var currentMatchLength = kvp.Value.Length;
                            var expectedChar = kvp.Key[currentMatchLength];
                            var lastRequired = currentMatchLength == kvp.Key.Length - 1;
                            if (lineChar == expectedChar) {
                                if (lastRequired) {
                                    digit = wordToDigit[kvp.Key];
                                } else {
                                    wordToCurrentMatch[kvp.Key] += lineChar;
                                }
                            } else {
                                wordToCurrentMatch[kvp.Key] = kvp.Key.First() == lineChar ? lineChar.ToString() : "";
                            }
                        }
                    }

                    if (digit.HasValue) {
                        if (!first.HasValue) {
                            first = digit;
                        }
                        last = digit;
                    }
                }

                var result = $"{first}{last}";
                sum += int.Parse(result);
            }
            return sum.ToString();
        }

        private static readonly Dictionary<string, char> wordToDigit = new()
        {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' }
        };
    }
}

