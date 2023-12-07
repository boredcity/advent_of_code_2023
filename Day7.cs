using System.Numerics;

namespace AdventOfCode {
    class Day7 : IDay {
        private readonly Dictionary<char, int> charToWorth;

        public Day7() {
            char[] cardsWorthOrder = { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            this.charToWorth = cardsWorthOrder.Select((ch, i) => (ch, i)).Aggregate(new Dictionary<char, int>(), (dict, info) => {
                var (ch, i) = info;
                dict[ch] = cardsWorthOrder.Length - i;
                return dict;
            });
        }

        sealed record Hand(string cards, int multiplier, int combinationWorth);
        public string solve1() {
            var lines = File.ReadLines("./inputs/day7.txt");
            
            List<Hand> hands = new() {};
            foreach (var line in lines) {
                var parts = line.Split(' ');
                var hand = parts[0];
                var multiplier = int.Parse(parts[1]);
                var charCounts = hand.Aggregate(new Dictionary<char, int>(), (dict, ch) => {
                    if (!dict.ContainsKey(ch)) {
                        dict[ch] = 0;
                    }
                    dict[ch]++;
                    return dict;
                });
                var counts = charCounts.Values.ToList();
                counts.Sort((a,b) => b - a);
                var worth = 0;
                if (counts[0] == 5) {
                    worth = 6;
                } else if (counts[0] == 4) {
                    worth = 5;
                } else if (counts[0] == 3 && counts[1] == 2) {
                    worth = 4;
                } else if (counts[0] == 3) {
                    worth = 3;
                } else if (counts[0] == 2 && counts[1] == 2) {
                    worth = 2;
                } else if (counts[0] == 2) {
                    worth = 1;
                }
                hands.Add(new Hand(hand, multiplier, worth));
            }
            hands.Sort((h1, h2) => {
                var comboWorthDiff = h1.combinationWorth - h2.combinationWorth;
                if (comboWorthDiff != 0) return comboWorthDiff;
                for (var i = 0; i < h1.cards.Length; i++) {
                    var worth1 = charToWorth[h1.cards[i]];
                    var worth2 = charToWorth[h2.cards[i]];
                    if (worth1 != worth2) return worth1 - worth2;
                }
                return 0;
            });
            return hands.Select((h, i) => h.multiplier * (i + 1)).Sum().ToString();
        }

        public string solve2() {
            
            var lines = File.ReadLines("./inputs/day7.txt");
            
            List<Hand> hands = new() {};
            foreach (var line in lines) {
                var parts = line.Split(' ');
                var hand = parts[0];
                var multiplier = int.Parse(parts[1]);
                var charCounts = hand.Aggregate(new Dictionary<char, int>(), (dict, ch) => {
                    if (!dict.ContainsKey(ch)) {
                        dict[ch] = 0;
                    }
                    dict[ch]++;
                    return dict;
                });
                var jokerCount = charCounts.ContainsKey('J') ? charCounts['J'] : 0;
                charCounts.Remove('J');
                var counts = charCounts.Values.ToList();
                counts.Sort((a,b) => b - a);
                var worth = 0;
                if (counts.Count != 0) {
                    counts[0] += jokerCount;
                    if (counts[0] == 5) {
                        worth = 6;
                    } else if (counts[0] == 4) {
                        worth = 5;
                    } else if (counts[0] == 3 && counts[1] == 2) {
                        worth = 4;
                    } else if (counts[0] == 3) {
                        worth = 3;
                    } else if (counts[0] == 2 && counts[1] == 2) {
                        worth = 2;
                    } else if (counts[0] == 2) {
                        worth = 1;
                    }
                } else {
                    worth = 6;
                }
                hands.Add(new Hand(hand, multiplier, worth));
            }
            hands.Sort((h1, h2) => {
                var comboWorthDiff = h1.combinationWorth - h2.combinationWorth;
                if (comboWorthDiff != 0) return comboWorthDiff;
                for (var i = 0; i < h1.cards.Length; i++) {
                    var worth1 = h1.cards[i] == 'J' ? 0 : charToWorth[h1.cards[i]];
                    var worth2 = h2.cards[i] == 'J' ? 0 : charToWorth[h2.cards[i]];
                    if (worth1 != worth2) return worth1 - worth2;
                }
                return 0;
            });
            return hands.Select((h, i) => h.multiplier * (i + 1)).Sum().ToString();
        }
    }
}
