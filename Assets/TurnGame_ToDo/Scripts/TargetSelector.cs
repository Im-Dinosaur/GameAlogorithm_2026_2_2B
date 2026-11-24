using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class TargetSelector : ITargetSelector
    {
        public int SelectTarget(IReadOnlyList<BattleSnapshot> units, BattleTeam targetTeam)
        {
            // TODO 08~11: 지정 팀의 생존 유닛 중 HP 비율, 현재 HP 순서로 선택합니다.
            int best = -1;
            if (units == null) return best;
            for (int i = 0; i < units.Count; i++)
            {
                if (!units[i].IsAlive || units[i].Team != targetTeam) continue;
                if (best < 0) { best = i; continue; }
                // 정수 교차 곱으로 나눗셈 오차 없이 비율을 비교합니다.
                long currentRatio = (long)units[i].HP * System.Math.Max(1, units[best].MaxHP);
                long bestRatio = (long)units[best].HP * System.Math.Max(1, units[i].MaxHP);
                if (currentRatio < bestRatio || (currentRatio == bestRatio && units[i].HP < units[best].HP))
                    best = i;
            }
            return best;
        }
    }
}
