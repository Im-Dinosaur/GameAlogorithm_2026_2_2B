using System.Collections.Generic;

namespace AlgoLesson6
{
    /// <summary>손패의 1~5장 조합을 백트래킹하여 점수가 가장 높은 패를 찾습니다.</summary>
    public sealed class BestHandAdvisor
    {
        private readonly IPokerHandEvaluator evaluator;

        public BestHandAdvisor(IPokerHandEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public PokerHandResult FindBest(IReadOnlyList<CardData> hand)
        {
            PokerHandResult best = new PokerHandResult();
            if (hand == null || hand.Count == 0) return best;

            List<CardData> selected = new List<CardData>();
            // TODO 09: 빈 선택부터 시작해 최대 5장을 탐색합니다.
            Search(hand, 0, 5, selected, ref best);
            return best;
        }

        private void Search(IReadOnlyList<CardData> hand, int start, int maxCount,
            List<CardData> selected, ref PokerHandResult best)
        {
            // TODO 10: 1~5장의 모든 부분 조합을 평가합니다.
            if (selected.Count > 0)
            {
                PokerHandResult candidate = evaluator.Evaluate(selected);
                if (best.cards.Count == 0 || candidate.Score > best.Score) best = candidate;
            }

            // TODO 11: 5장에 도달하면 더 선택하지 않습니다.
            if (selected.Count >= maxCount) return;

            for (int index = start; index < hand.Count; index++)
            {
                selected.Add(hand[index]);                 // TODO 12: 선택
                Search(hand, index + 1, maxCount, selected, ref best); // TODO 13: 재귀
                selected.RemoveAt(selected.Count - 1);      // TODO 14: 선택 취소
            }
        }
    }
}
