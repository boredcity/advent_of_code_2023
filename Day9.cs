namespace AdventOfCode {
    class Day9 : IDay {
        public int calculate(List<int> list) => list.Exists(x => x != 0)
            ? list.Last() + calculate(list.Zip(list.Skip(1), (el1, el2) => el2 - el1).ToList())
            : 0;

        public string solve1() => File.ReadLines("./inputs/day9.txt").Select(line => 
                calculate(line.Split(' ').Select(int.Parse).ToList())
            ).Sum().ToString();

        public string solve2() => File.ReadLines("./inputs/day9.txt").Select(line => 
                calculate(line.Split(' ').Select(int.Parse).Reverse().ToList())
            ).Sum().ToString();
    }
}
