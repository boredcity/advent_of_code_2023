using System.Text;

namespace AdventOfCode {

    class Day15 : IDay {
        public string Solve1() {
            var result = 0;
            using (StreamReader reader = new StreamReader("./inputs/day15.txt")) {
                int chCode;
                var currentValue = 0;
                while ((chCode = reader.Read()) != -1) {
                    char ch = (char)chCode;
                    if (ch == ',') {
                        result += currentValue;
                        currentValue = 0;
                        continue;
                    }
                    currentValue += chCode;
                    currentValue *= 17;
                    currentValue %= 256;
                }
                result += currentValue;
            }

            return $"{result}";
        }

        record Lens(string name, int focus);

        public string Solve2() {
            List<Lens>[] boxes = new List<Lens>[256];
            using (StreamReader reader = new StreamReader("./inputs/day15.txt")) {
                var curCommand = new StringBuilder();
                int chCode;
                while ((chCode = reader.Read()) != -1) {
                    char ch = (char)chCode;
                    if (ch != ',') curCommand.Append(ch);
                    if (ch == ',' || reader.Peek() == -1) {
                        var command = curCommand.ToString();
                        var isRemoveCommand = command.EndsWith('-');
                        var parts = command.Split(isRemoveCommand ? '-' : '=');
                        var lensName = parts[0];
                        
                        var boxNumber = 0;
                        foreach (var strCh in lensName) {
                            var asciiCode = (int) strCh;
                            boxNumber += asciiCode;
                            boxNumber *= 17;
                            boxNumber %= 256;
                        }

                        if (isRemoveCommand) {
                            if (boxes[boxNumber] != null) boxes[boxNumber] = boxes[boxNumber].Where(l => l.name != lensName).ToList();
                        } else {
                            var focalLength = int.Parse(parts[1]);
                            if (boxes[boxNumber] == null) boxes[boxNumber] = new List<Lens>();
                            var existing = boxes[boxNumber].FindIndex(l => l.name == lensName);
                            if (existing != -1) boxes[boxNumber][existing] = new Lens(lensName, focalLength);
                            else boxes[boxNumber].Add(new Lens(lensName, focalLength));
                        }
                        curCommand.Clear();
                    }
                }
            }
            var totalPower = 0;
            for (var boxPower = 1; boxPower <= boxes.Length; boxPower++) {
                var box = boxes[boxPower - 1];
                if (box != null) totalPower += box.Select((lens, i) => boxPower * (i + 1) * lens.focus).Sum();
            }
            return totalPower.ToString();
        }
    }
}
