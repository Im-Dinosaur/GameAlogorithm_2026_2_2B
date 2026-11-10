using System.Collections.Generic;
using AlgoLesson7;

namespace AlgoLesson7.Student
{
    /// <summary>살아 있는 서로 다른 이름의 유닛을 집계해 활성 시너지를 계산합니다.</summary>
    public sealed class SynergyCalculator : ISynergyCalculator
    {
        public SynergyResult Calculate(IReadOnlyList<UnitSnapshot> allies)
        {
            SynergyResult result = new SynergyResult();
            if (allies == null || allies.Count == 0) return result;

            Dictionary<UnitRace, int> raceCounts = result.RaceCounts;
            Dictionary<UnitJob, int> jobCounts = result.JobCounts;
            HashSet<string> countedNames = new HashSet<string>();

            foreach (UnitSnapshot unit in allies)
            {
                // TODO 01~04: 생존 확인 → 이름 중복 확인 → 종족/직업별 집계.
                if (!unit.IsAlive) continue;
                if (!countedNames.Add(unit.UnitName)) continue;
                raceCounts[unit.Race] = GetCount(raceCounts, unit.Race) + 1;
                jobCounts[unit.Job] = GetCount(jobCounts, unit.Job) + 1;
            }

            int warriorCount = GetCount(jobCounts, UnitJob.Warrior);
            int rangerCount = GetCount(jobCounts, UnitJob.Ranger);
            int mageCount = GetCount(jobCounts, UnitJob.Mage);
            int humanCount = GetCount(raceCounts, UnitRace.Human);
            int beastCount = GetCount(raceCounts, UnitRace.Beast);
            int machineCount = GetCount(raceCounts, UnitRace.Machine);

            // TODO 05~06: 강한 Warrior 단계만 적용하며 정확히 2명도 포함합니다.
            if (warriorCount >= 4)
            {
                result.WarriorArmor = 5f;
                result.ActiveNames.Add("Warrior 4");
            }
            else if (warriorCount >= 2)
            {
                result.WarriorArmor = 2f;
                result.ActiveNames.Add("Warrior 2");
            }

            // TODO 07~11: 직업/종족별 기준 인원을 만족하면 효과를 활성화합니다.
            if (rangerCount >= 2)
            {
                result.RangerAttackSpeedMultiplier = 1.2f;
                result.ActiveNames.Add("Ranger 2");
            }
            if (mageCount >= 2)
            {
                result.MageAttackMultiplier = 1.25f;
                result.ActiveNames.Add("Mage 2");
            }
            if (humanCount >= 3)
            {
                result.HumanHealthMultiplier = 1.2f;
                result.ActiveNames.Add("Human 3");
            }
            if (beastCount >= 3)
            {
                result.BeastAttackMultiplier = 1.2f;
                result.ActiveNames.Add("Beast 3");
            }
            if (machineCount >= 2)
            {
                result.MachineShield = 15f;
                result.ActiveNames.Add("Machine 2");
            }
            return result;
        }

        private static int GetCount<TKey>(Dictionary<TKey, int> counts, TKey key)
        {
            // TODO 12: 등록되지 않은 종족/직업의 개수는 0입니다.
            return counts.TryGetValue(key, out int count) ? count : 0;
        }
    }
}
