namespace GameRunner
{
    public class GameAlgorithmController
    {

        public List<Step> GetWalkableSteps(List<string> map, Step currentStep, Step targetStep)
        {
            var possibleSteps = new List<Step>
            {
                new()
                {
                    X = currentStep.X, Y = currentStep.Y - 1, Parent = currentStep, Cost = currentStep.Cost + 1
                },
                new()
                {
                    X = currentStep.X, Y = currentStep.Y + 1, Parent = currentStep, Cost = currentStep.Cost + 1
                },
                new()
                {
                    X = currentStep.X - 1, Y = currentStep.Y, Parent = currentStep, Cost = currentStep.Cost + 1
                },
                new()
                {
                    X = currentStep.X + 1, Y = currentStep.Y, Parent = currentStep, Cost = currentStep.Cost + 1
                },
            };

            possibleSteps.ForEach(tile => tile.SetDistance(targetStep.X, targetStep.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleSteps
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                .Where(tile => map[tile.Y][tile.X] == ' ')
                .ToList();
        }
        public void AdjustActiveAndVisitedSteps(List<Step> walkableSteps, List<Step> activeSteps, List<Step> visitedSteps, Step checkStep)
        {
            foreach (var walkableStep in walkableSteps)
            {
                if (visitedSteps.Any(x => x.X == walkableStep.X && x.Y == walkableStep.Y))
                    continue;

                if (activeSteps.Any(x => x.X == walkableStep.X && x.Y == walkableStep.Y))
                {
                    var existingStep = activeSteps.First(x => x.X == walkableStep.X && x.Y == walkableStep.Y);
                    if (existingStep.CostDistance > walkableStep.CostDistance)   //or  checkStep???
                    {
                        activeSteps.Remove(existingStep);
                        activeSteps.Add(walkableStep);
                    }
                }
                else
                {
                    activeSteps.Add(walkableStep);
                }
            }
        }
    }
}
