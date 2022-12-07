namespace GameRunner;

public class Game : IGame
{
    
        private readonly List<string> _map;
    private readonly Step _startStep;
    private readonly ExitValidationController _exitValidationController;
    private readonly GameAlgorithmController _gameAlgorithmController;
    private readonly List<Step> _finishSteps;
    private readonly List<int> _paths;

    public Game()
    {
        _map = new List<string>();
        _startStep = new Step();
        _exitValidationController = new ExitValidationController();
        _gameAlgorithmController = new GameAlgorithmController();
        _finishSteps = new List<Step>();
        _paths = new List<int>();
    }

    public int Run(string filePath)
    {
        var mapArray = ReadFile(filePath);
        _map.AddRange(mapArray);
        if (_map.Count == 0)
        {
            return 0;
        }

        _finishSteps.AddRange(_exitValidationController.SetFinishSteps(_map));

        if (SetStartStep() == 0 || _finishSteps.Count == 0)
        {
            return 0;
        }

        return PlayGame();
    }
    private string[] ReadFile(string csvFile)
    {
        var list = File.ReadAllLines(csvFile);
        return list;
    }
    private int SetStartStep()
    {
        _startStep.Y = _map.FindIndex(x => x.Contains("X"));
        if (_startStep.Y == -1)
        {
            return 0;
        }
        _startStep.X = _map[_startStep.Y].IndexOf("X");
        return 1;
    }
    private int PlayGame()
    {
        foreach (var targetStep in _finishSteps)
        {
            _startStep.SetDistance(targetStep.X, targetStep.Y);

            var activeSteps = new List<Step> { _startStep };
            var visitedSteps = new List<Step>();

            while (activeSteps.Any())
            {
                var checkStep = activeSteps.OrderByDescending(x => x.CostDistance).Last();

                if (CheckIfCurrentStepMatchesWithFinishStep(checkStep, targetStep) == false)
                {
                    visitedSteps.Add(checkStep);
                    activeSteps.Remove(checkStep);

                    var walkableSteps = _gameAlgorithmController.GetWalkableSteps(_map, checkStep, targetStep);
                    _gameAlgorithmController.AdjustActiveAndVisitedSteps(walkableSteps, activeSteps, visitedSteps, checkStep);
                }
                else
                {
                    visitedSteps.Clear();
                    activeSteps.Clear();
                    EndTheGame(checkStep);
                }
            }
        }
        if (_paths.Any())
        {
            return FindTheFastestPath();
        }
        return 0;
    }
    private static bool CheckIfCurrentStepMatchesWithFinishStep(Step checkStep, Step finish)
    {
        return checkStep.X == finish.X && checkStep.Y == finish.Y;
    }
    private void EndTheGame(Step checkStep)
    {
        var step = checkStep;

        var count = 0;

        while (true)
        {
            if (_map[step.Y][step.X] == ' ')
            {
                count++;
            }

            step = step.Parent;
            if (step == null)
            {
                _paths.Add(count);
                break;
            }
        }
    }
    private int FindTheFastestPath()
    {
        return _paths.AsQueryable().Min();
    }
}