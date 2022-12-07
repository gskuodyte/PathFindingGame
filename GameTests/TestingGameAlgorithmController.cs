using GameRunner;

namespace GameTests
{
    public class TestingGameAlgorithmController
    {
        private readonly GameAlgorithmController _sut;

        public TestingGameAlgorithmController()
        {
            _sut = new GameAlgorithmController();
        }

        [Fact]
        public void GetWalkableSteps_WhenOnePossibleWay_ReturnOnePossibleStep()
        {
            var data = _sut.GetWalkableSteps(_map1, CurrentStep, _targetStep);

            Assert.NotNull(data);
            Assert.Single(data);
            Assert.Equal(PossibleStep.ToString(), data[0].ToString());
        }

        [Fact]
        public void GetWalkableSteps_WhenNoWayPossible_ReturnEmptyList()
        {
            var emptyList = new List<Step>();
            var data = _sut.GetWalkableSteps(_map2, CurrentStep, _targetStep);

            Assert.NotNull(data);
            Assert.Equal(emptyList, data);
        }

        [Fact]
        public void GetWalkableSteps_WhenThreePossibleWays_ReturnListWithThreeSteps()
        {
            var data = _sut.GetWalkableSteps(_map3, CurrentStep, _targetStep);

            Assert.NotNull(data);
            Assert.Equal(3, data.Count);
            Assert.Equal(_possibleSteps[0].ToString(), data[0].ToString());
            Assert.Equal(_possibleSteps[1].ToString(), data[1].ToString());
            Assert.Equal(_possibleSteps[2].ToString(), data[2].ToString());
        }

        private readonly List<string> _map1 = new()
        {
            "11111",
            "1 X 1",
            "1 1 1",
            "1   1",
            "111 1"
        };

        private readonly List<string> _map2 = new()
        {
            "11111",
            "1 X 1",
            "1 111",
            "1   1",
            "111 1"
        };

        private readonly List<string> _map3 = new()
        {
            "111 1",
            "1 X  ",
            "1 1 1",
            "1   1",
            "111 1"
        };

        private static readonly Step CurrentStep = new()
        {
            Cost = 1,
            Distance = 3,
            Parent = ParentStep,
            X = 3,
            Y = 1
        };
        private readonly Step _targetStep = new()
        {
            Cost = 0,
            Distance = 0,
            Parent = null,
            X = 3,
            Y = 4,
        };

        private static readonly Step PossibleStep = new()
        {
            Cost = 2,
            Distance = 2,
            Parent = CurrentStep,
            X = 3,
            Y = 2,
        };

        private static readonly Step ParentStep = new()
        {
            Cost = 0,
            Distance = 4,
            Parent = null,
            X = 2,
            Y = 1
        };

        private readonly List<Step> _possibleSteps = new()
        {
            new Step
            {
                Cost = 2,
                Distance = 4,
                Parent = ParentStep,
                X = 3,
                Y = 0
            },
            PossibleStep,
            new Step
            {
                Cost = 2,
                Distance = 4,
                Parent = ParentStep,
                X = 4,
                Y = 1
            }
        };

    }
}