namespace AdventOfCode {
    class Day11 : IDay {
        public string Solve1() {
            File.ReadLines("./inputs/day11.txt").Select(line => 
                line.Split(' ').Select(int.Parse).ToList()
            );
            return "";
        }

        public string Solve2() {
            File.ReadLines("./inputs/day11.txt").Select(line => 
                line.Split(' ').Select(int.Parse).ToList()
            );
            return "";
        }
    }
}
