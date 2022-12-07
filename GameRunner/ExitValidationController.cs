namespace GameRunner
{
    public class ExitValidationController
    {
        private readonly List<Step> _finishSteps;

        public ExitValidationController()
        {
            _finishSteps = new List<Step>();
        }

        public List<Step> SetFinishSteps(List<string> map)
        {
            CheckSideWallsForExit(map);
            CheckUpperWallForExit(map);
            CheckLowerWallForExit(map);

            return _finishSteps;
        }
        private void CheckLowerWallForExit(List<string> map)
        {
            for (var i = 1; i < map.Last().Length - 1; i++)
            {
                if (map.Last()[i] == ' ')
                {
                    _finishSteps.Add(new Step { X = i, Y = map.Count - 1 });
                }
            }
        }
        private void CheckUpperWallForExit(List<string> map)
        {
            for (var i = 1; i < map[0].Length - 1; i++)
            {
                if (map[0][i] == ' ')
                {
                    _finishSteps.Add(new Step { X = i, Y = map.IndexOf(map[0]) });
                }
            }
        }
        private void CheckSideWallsForExit(List<string> map)
        {
            for (var i = 1; i < map.Count - 1; i++)
            {
                var array = map[i].ToCharArray();

                if (array[0] == ' ')
                {
                    _finishSteps.Add(new Step { X = 0, Y = i });
                }

                if (array.Last() == ' ')
                {
                    _finishSteps.Add(new Step { X = map.Count - 1, Y = i });
                }
            }
        }
    }
}