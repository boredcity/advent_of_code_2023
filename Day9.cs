namespace AdventOfCode {
    class Day9 : IDay {
        private int Calculate(List<int> list) => list.Exists(x => x != 0)
            ? list.Last() + Calculate(list.Zip(list.Skip(1), (el1, el2) => el2 - el1).ToList())
            : 0;

        public string Solve1() => File.ReadLines("./inputs/day9.txt").Select(line => 
                Calculate(line.Split(' ').Select(int.Parse).ToList())
            ).Sum().ToString();

        public string Solve2() => File.ReadLines("./inputs/day9.txt").Select(line => 
                Calculate(line.Split(' ').Select(int.Parse).Reverse().ToList())
            ).Sum().ToString();
    }
}
