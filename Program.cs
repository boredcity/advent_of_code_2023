using AdventOfCode;
namespace Main {
    class Program {
        static void Main() {
            IDay[] days = {
                // new Day1(),
                // new Day2(),
                // new Day3(),
                // new Day4(),
                new Day5()
            };
            Console.WriteLine($"Results:");
            foreach (var day in days) {
                var first = day.solve1();
                var second = day.solve2();
                Console.WriteLine($"${day}: {first} / {second}");
            }
        }
    }
}
