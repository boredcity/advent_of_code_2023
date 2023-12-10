using AdventOfCode;

namespace Main {
    static class Program {
        static void Main() {
            IDay[] days = {
                new Day1(),
                new Day2(),
                new Day3(),
                new Day4(),
                new Day5(),
                new Day6(),
                new Day7(),
                new Day8(),
                new Day9(),
                new Day10(),
            };
            Console.WriteLine($"Results:");
            var i = 0;
            foreach (var day in days) Console.WriteLine($"Day {++i}: {day.Solve1()} and { day.Solve2()}");
        }
    }
}
