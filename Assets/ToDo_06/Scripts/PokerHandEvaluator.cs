using System.Collections.Generic;
using System.Linq;

namespace AlgoLesson6
{
    /// <summary>선택한 카드의 족보와 (기본 Chips + 카드 Chips) × 배수를 계산합니다.</summary>
    public sealed class PokerHandEvaluator : IPokerHandEvaluator
    {
        public PokerHandResult Evaluate(IReadOnlyList<CardData> cards)
        {
            List<CardData> safeCards = cards == null
                ? new List<CardData>()
                : cards.ToList();

            if (safeCards.Count == 0)
                return new PokerHandResult { cards = safeCards, multiplier = 1 };

            // TODO 01~04: 숫자별 개수, Flush, Straight, 최대 중복과 Pair 개수.
            Dictionary<int, int> rankCounts = BuildRankCounts(safeCards);
            bool isFlush = IsFlush(safeCards);
            bool isStraight = IsStraight(safeCards);
            int maxSameRank = 0;
            int pairCount = 0;
            foreach (int count in rankCounts.Values)
            {
                if (count > maxSameRank) maxSameRank = count;
                if (count == 2) pairCount++;
            }

            HandRank rank = DetermineRank(safeCards.Count, isFlush, isStraight, maxSameRank, pairCount);
            int cardChips = safeCards.Sum(card => card.ChipValue);
            GetBaseScore(rank, out int baseChips, out int multiplier);

            return new PokerHandResult
            {
                rank = rank,
                baseChips = baseChips,
                cardChips = cardChips,
                multiplier = multiplier,
                cards = safeCards
            };
        }

        private Dictionary<int, int> BuildRankCounts(List<CardData> cards)
        {
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (CardData card in cards)
            {
                if (counts.ContainsKey(card.rank)) counts[card.rank]++;
                else counts.Add(card.rank, 1);
            }
            return counts;
        }

        private bool IsFlush(List<CardData> cards)
        {
            if (cards.Count != 5) return false;
            CardSuit suit = cards[0].suit;
            return cards.All(card => card.suit == suit);
        }

        private bool IsStraight(List<CardData> cards)
        {
            if (cards.Count != 5) return false;
            int[] ranks = cards.Select(card => card.rank).Distinct().OrderBy(rank => rank).ToArray();
            if (ranks.Length != 5) return false;

            // TODO 07: Ace는 A-2-3-4-5에서만 낮은 숫자로도 사용합니다.
            if (ranks.SequenceEqual(new[] { 2, 3, 4, 5, 14 })) return true;
            for (int index = 1; index < ranks.Length; index++)
            {
                if (ranks[index] != ranks[0] + index) return false;
            }
            return true;
        }

        private HandRank DetermineRank(int count, bool flush, bool straight, int maxSame, int pairs)
        {
            // TODO 05: 동시에 성립할 수 있는 조건은 강한 족보부터 검사합니다.
            if (count == 5 && flush && straight) return HandRank.StraightFlush;
            if (maxSame == 4) return HandRank.FourOfAKind;
            if (count == 5 && maxSame == 3 && pairs == 1) return HandRank.FullHouse;
            if (count == 5 && flush) return HandRank.Flush;
            if (count == 5 && straight) return HandRank.Straight;
            if (maxSame == 3) return HandRank.ThreeOfAKind;
            if (pairs == 2) return HandRank.TwoPair;
            if (pairs == 1) return HandRank.Pair;
            return HandRank.HighCard;
        }

        private void GetBaseScore(HandRank rank, out int chips, out int multiplier)
        {
            // TODO 08: 족보별 기본 점수. 모든 선택 카드의 ChipValue는 별도로 더합니다.
            switch (rank)
            {
                case HandRank.StraightFlush: chips = 100; multiplier = 8; break;
                case HandRank.FourOfAKind: chips = 60; multiplier = 7; break;
                case HandRank.FullHouse: chips = 40; multiplier = 4; break;
                case HandRank.Flush: chips = 35; multiplier = 4; break;
                case HandRank.Straight: chips = 30; multiplier = 4; break;
                case HandRank.ThreeOfAKind: chips = 30; multiplier = 3; break;
                case HandRank.TwoPair: chips = 20; multiplier = 2; break;
                case HandRank.Pair: chips = 10; multiplier = 2; break;
                default: chips = 5; multiplier = 1; break;
            }
        }
    }
}
