using System.Collections.Generic;
using AlgoLesson7;

namespace AlgoLesson7.Student
{
    /// <summary>생존 적 중 사거리, 거리, 체력, ID 순으로 공격 대상을 선택합니다.</summary>
    public sealed class TargetSelector : ITargetSelector
    {
        public int SelectTarget(UnitSnapshot self, IReadOnlyList<UnitSnapshot> enemies)
        {
            if (enemies == null || enemies.Count == 0) return -1;

            bool hasBest = false;
            int bestId = -1;
            bool bestInRange = false;
            float bestDistanceSquared = float.MaxValue;
            float bestHealth = float.MaxValue;
            float rangeSquared = self.AttackRange * self.AttackRange;

            foreach (UnitSnapshot enemy in enemies)
            {
                // TODO 13~14: 죽은 적을 제외하고 제곱 거리로 사거리를 검사합니다.
                if (!enemy.IsAlive) continue;
                float distanceSquared = (enemy.Position - self.Position).sqrMagnitude;
                bool inRange = distanceSquared <= rangeSquared;

                // TODO 15: 앞선 기준이 같을 때에만 다음 기준을 비교합니다.
                bool better;
                if (!hasBest) better = true;
                else if (inRange != bestInRange) better = inRange;
                else if (distanceSquared != bestDistanceSquared) better = distanceSquared < bestDistanceSquared;
                else if (enemy.CurrentHealth != bestHealth) better = enemy.CurrentHealth < bestHealth;
                else better = enemy.Id < bestId;

                if (!better) continue;
                hasBest = true;
                bestId = enemy.Id;
                bestInRange = inRange;
                bestDistanceSquared = distanceSquared;
                bestHealth = enemy.CurrentHealth;
            }
            return bestId;
        }
    }
}
