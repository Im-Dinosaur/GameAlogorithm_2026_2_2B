using System.Collections.Generic;
using UnityEngine;
using TurnGame;

namespace TurnGame.Student
{
    public class ExperienceCalculator : IExperienceCalculator
    {
        // TODO 12: 레벨이 오를 때마다 필요 경험치가 50씩 증가합니다.
        public int RequiredExperience(int level) => 100 + (level - 1) * 50;

        public ExperienceResult AddExperience(int level, int currentExperience, int gainedExperience)
        {
            // TODO 13~16: 필요 경험치를 차감하며 가능한 모든 레벨 상승을 처리합니다.
            long experience = (long)currentExperience + gainedExperience;
            int levelsGained = 0;
            while (experience >= RequiredExperience(level))
            {
                experience -= RequiredExperience(level);
                level++;
                levelsGained++;
            }
            return new ExperienceResult { Level = level, RemainingExperience = (int)experience, LevelsGained = levelsGained };
        }
    }
}
