using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class TurnScheduler : ITurnScheduler
    {
        public int SelectNext(IReadOnlyList<BattleSnapshot> units)
        {
            // TODO 01~04: 생존 유닛의 행동 시간, 동률이면 높은 속도를 비교합니다.
            int best = -1;
            if (units == null) return best;
            for (int i = 0; i < units.Count; i++)
            {
                if (!units[i].IsAlive) continue;
                if (best < 0 || units[i].NextActionTime < units[best].NextActionTime ||
                    (units[i].NextActionTime == units[best].NextActionTime && units[i].Speed > units[best].Speed))
                    best = i;
            }
            return best;
        }

        public float CalculateDelay(int speed, int speedBonus)
        {
            // TODO 05~07: 보너스를 합산하고 최소 속도를 1로 제한합니다.
            long effectiveSpeed = System.Math.Max(1L, (long)speed + speedBonus);
            return 100f / effectiveSpeed;
        }
    }
}
