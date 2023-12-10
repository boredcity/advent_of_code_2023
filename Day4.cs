namespace AdventOfCode {
    class Day4 : IDay {
        public string Solve1() {
            var lines = File.ReadLines("./inputs/day4.txt");
            var sum = 0;

            foreach (var line in lines) {
                var numberLists = line.Split(":")[1].Split("|");
                var winningNumbers = numberLists[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();

                var points = (int) Math.Floor(Math.Pow(2, numberLists[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Where(winningNumbers.Contains)
                    .AsEnumerable()
                    .Count() - 1));
                sum += points;
            }

            return sum.ToString();
        }
        public string Solve2() {
            var lines = File.ReadLines("./inputs/day4.txt");
            List<int> copiesPerTicket = new() {};
            var lineIndex = -1;

            foreach (var line in lines) {
                lineIndex++;
                if (lineIndex >= copiesPerTicket.Count) copiesPerTicket.Add(0);
                copiesPerTicket[lineIndex] += 1;
                var numberLists = line.Split(":")[1].Split("|");
                var winningNumbers = numberLists[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                var ticketNumbers = numberLists[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var copyIndex = lineIndex;
                foreach (var copy in ticketNumbers.Where(winningNumbers.Contains)) {
                    copyIndex += 1;
                    if (copyIndex >= copiesPerTicket.Count) copiesPerTicket.Add(0);
                    copiesPerTicket[copyIndex] += copiesPerTicket[lineIndex];
                }
            }
            return copiesPerTicket.Sum().ToString();
        }
    }
}
