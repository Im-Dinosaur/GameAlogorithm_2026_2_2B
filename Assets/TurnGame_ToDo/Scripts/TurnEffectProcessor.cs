using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class TurnEffectProcessor : ITurnEffectProcessor
    {
        public float Apply(float delay, TurnEffectType effect, float amount)
        {
            // TODO 23~26: 턴 효과를 적용하고 대기 시간이 음수가 되지 않게 합니다.
            switch (effect)
            {
                case TurnEffectType.Haste: delay -= amount; break;
                case TurnEffectType.Slow:
                case TurnEffectType.Delay: delay += amount; break;
                case TurnEffectType.Stun: delay += Mathf.Max(5f, amount); break;
            }
            return Mathf.Max(0f, delay);
        }
    }
}
