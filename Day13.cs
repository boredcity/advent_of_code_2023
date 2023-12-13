namespace AdventOfCode {
    class Day13 : IDay {
        private (bool, bool) CanBeReflection(string row1, string row2, bool smudgeAllowed) {
            var isSmudgeAllowed = smudgeAllowed;
            for (var cI = 0; cI < row1.Length; cI++) {
                if (row1[cI] != row2[cI]) {
                    if (isSmudgeAllowed) {
                        isSmudgeAllowed = false;
                        continue;
                    }
                    return (false, isSmudgeAllowed);
                }
            }
            return (true, isSmudgeAllowed);
        }

        private int GetReflections(string[] lines, bool isSingleSmudgeAllowed) {
            var reflectionLines = 0;
            for (var rI = 1; rI < lines.Length; rI++) {
                var curRow = lines[rI];
                var prevRow = lines[rI - 1];

                var (canBeReflection, allowSmudge) = CanBeReflection(curRow, prevRow, isSingleSmudgeAllowed);
                if (!canBeReflection) continue;
                var top = rI - 2;
                var bottom = rI + 1;
                var isReflection = true;
                while (top >= 0 && bottom < lines.Length) {
                    var (canBeReflectionInner, allowSmudgeInner) = CanBeReflection(lines[top--], lines[bottom++], allowSmudge);
                    allowSmudge = allowSmudgeInner;
                    if (!canBeReflectionInner) {
                        isReflection = false;
                        break;
                    }
                }
                reflectionLines += !isReflection || allowSmudge ? 0 : rI;
            }
            return reflectionLines;
        }

        public static string[] Rotate(string[] input) {
            int numRows = input.Length;
            int numCols = input[0].Length;
            string[] rotated = new string[numCols];
            for (int col = 0; col < numCols; col++) {
                char[] newRow = new char[numRows];
                for (int row = 0; row < numRows; row++) newRow[row] = input[numRows - 1 - row][col];
                rotated[col] = new string(newRow);
            }
            return rotated;
        }

        private string CheckFigures(bool allowSmudge) {
            var columnsToTheLeft = 0;
            var rowsAbove = 0;
            var lines = File.ReadLines("./inputs/day13.txt");
            var currentSegmentLines = new List<string>();
            foreach (var line in lines.Append("")) {
                if (line == "") {
                    var figure = currentSegmentLines.ToArray();
                    var rowI = GetReflections(figure, allowSmudge);
                    rowsAbove += rowI;
                    if (rowI == 0) {
                        var colI = GetReflections(Rotate(figure), allowSmudge);
                        columnsToTheLeft += colI;
                    }
                    currentSegmentLines = new List<string>();
                } else currentSegmentLines.Add(line);
            }
            return (columnsToTheLeft + rowsAbove * 100m).ToString();
        }

        public string Solve1() => CheckFigures(false);
        public string Solve2() => CheckFigures(true);
    }
}
