using AdventOfCode;

namespace Main {
    static class Program {
        static void Main() {
            IDay[] days = {
                // new Day01(),
                // new Day02(),
                // new Day03(),
                // new Day04(),
                // new Day05(),
                // new Day06(),
                // new Day07(),
                // new Day08(),
                // new Day09(),
                // new Day11(),
                // new Day12(),
                // new Day13(),
                new Day14(),
            };
            Console.WriteLine($"Results:");
            var i = 0;
            foreach (var day in days) Console.WriteLine($"Day {++i}: {day.Solve1()} and { day.Solve2()}");
        }
    }
}
