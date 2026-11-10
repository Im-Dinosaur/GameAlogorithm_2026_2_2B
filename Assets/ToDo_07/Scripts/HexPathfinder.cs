using System.Collections.Generic;
using AlgoLesson7;

namespace AlgoLesson7.Student
{
    /// <summary>목표 옆의 빈 칸까지 최단 경로를 탐색하고 다음 한 칸을 반환합니다.</summary>
    public sealed class HexPathfinder : IHexPathfinder
    {
        public AutoChessBoardCell FindNextStep(AutoChessBoardCell start,
            AutoChessBoardCell target, IReadOnlyList<AutoChessBoardCell> cells)
        {
            if (start == null || target == null || cells == null) return null;
            if (HexGridUtility.GetDistance(start, target) <= 1) return null;

            // TODO 16: 적이 점유한 목표 자체가 아니라 인접한 빈 칸으로 이동합니다.
            HashSet<AutoChessBoardCell> goals = new HashSet<AutoChessBoardCell>();
            foreach (AutoChessBoardCell cell in cells)
            {
                if (cell.Occupant == null && HexGridUtility.GetDistance(cell, target) == 1)
                    goals.Add(cell);
            }
            if (goals.Count == 0) return null;

            // TODO 17~18: cameFrom은 방문 표시와 경로 복원을 함께 담당합니다.
            Queue<AutoChessBoardCell> frontier = new Queue<AutoChessBoardCell>();
            Dictionary<AutoChessBoardCell, AutoChessBoardCell> cameFrom =
                new Dictionary<AutoChessBoardCell, AutoChessBoardCell>();
            frontier.Enqueue(start);
            cameFrom[start] = null;

            // TODO 19: FIFO 순서로 탐색해 가장 가까운 목표 칸을 찾습니다.
            while (frontier.Count > 0)
            {
                AutoChessBoardCell current = frontier.Dequeue();
                if (goals.Contains(current))
                {
                    // TODO 20: 역추적하되 이동은 start 다음 한 칸만 반환합니다.
                    AutoChessBoardCell step = current;
                    while (cameFrom[step] != start) step = cameFrom[step];
                    return step;
                }

                foreach (AutoChessBoardCell neighbor in HexGridUtility.GetOpenNeighbors(current, cells))
                {
                    if (cameFrom.ContainsKey(neighbor)) continue;
                    cameFrom[neighbor] = current;
                    frontier.Enqueue(neighbor);
                }
            }
            return null;
        }
    }
}
