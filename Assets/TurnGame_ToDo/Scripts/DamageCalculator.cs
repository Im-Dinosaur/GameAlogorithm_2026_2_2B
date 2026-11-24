using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class DamageCalculator : IDamageCalculator
    {
        public DamageResult Calculate(DamageRequest request)
        {
            // TODO 17~19: 스킬 위력은 백분율이며 기본 피해와 속성 배율을 계산합니다.
            float baseDamage = Mathf.Max(1f, (float)request.Attack * request.SkillPower / 100f - request.Defense * 0.5f);
            float element = 1f;
            if (request.SkillElement != BattleElement.None && request.SkillElement == request.Weakness) element = 1.5f;
            else if (request.SkillElement != BattleElement.None && request.SkillElement == request.Resistance) element = 0.5f;
            // TODO 20~22: 치명타, 방어 배율을 적용한 뒤 반올림하고 최소 피해를 보장합니다.
            bool critical = request.RandomValue < 0.1f;
            float damage = baseDamage * element * (critical ? 1.5f : 1f) * (request.Guarding ? 0.5f : 1f);
            return new DamageResult
            {
                Damage = Mathf.Max(1, Mathf.RoundToInt(damage)),
                IsCritical = critical,
                ElementMultiplier = element
            };
        }
    }
}
