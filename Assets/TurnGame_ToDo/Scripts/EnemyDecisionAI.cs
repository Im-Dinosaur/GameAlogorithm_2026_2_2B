using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class EnemyDecisionAI : IEnemyDecisionAI
    {
        public int SelectTarget(IReadOnlyList<BattleSnapshot> units, BattleElement attackElement)
        {
            int best = -1;
            float bestScore = float.MinValue;
            if (units == null) return best;
            for (int i = 0; i < units.Count; i++)
            {
                // TODO 27~31: 생존 아군의 낮은 HP, 마무리 공격, 약점을 점수로 평가합니다.
                BattleSnapshot target = units[i];
                if (!target.IsAlive || target.Team != BattleTeam.Heroes) continue;
                float hpRatio = target.HP / (float)Mathf.Max(1, target.MaxHP);
                float score = (1f - Mathf.Clamp01(hpRatio)) * 40f;
                if (target.HP <= 25) score += 100f;
                if (attackElement != BattleElement.None && target.Weakness == attackElement) score += 30f;
                if (target.Guarding) score -= 15f;
                if (score > bestScore) { bestScore = score; best = i; }
            }
            return best;
        }
    }
}
