using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class BattleRankCalculator : IBattleRankCalculator
    {
        public string CalculateRank(BattleRankInput input)
        {
            // TODO 32~36: 턴/피해 감점, 전원 생존/스킬 가점으로 등급을 판정합니다.
            int score = 100 - input.TurnCount * 2 - input.DamageTaken / 10 + input.SkillUseCount * 2;
            if (input.SurvivingHeroes == input.TotalHeroes) score += 20;
            if (score >= 90) return "S";
            if (score >= 70) return "A";
            if (score >= 50) return "B";
            return "C";
        }
    }
}
