using GameRunner;

namespace GameTests
{
    public class TestingExitValidationController
    {
        private readonly ExitValidationController _sut;

        public TestingExitValidationController()
        {
            _sut = new ExitValidationController();
        }

        [Fact]
        public void CheckWallsForExits_WhenThereIsNoExit_ReturnsEmptyList()
        {
            var data = _sut.SetFinishSteps(_map1);
            Assert.Empty(data);
        }

        [Fact]
        public void CheckWallsForExits_WhenExitsOnTheCorners_ReturnsEmptyList()
        {
            var data = _sut.SetFinishSteps(_map2);
            Assert.Empty(data);
        }

        [Fact]
        public void CheckWallsForExits_WhenMapIsEmpty_ReturnsEmptyList()
        {
            var data = _sut.SetFinishSteps(_map3);
            Assert.Empty(data);
        }

        [Fact]
        public void CheckWallsForExits_WhenExitsOnTheSideWalls_ReturnsExitsList()
        {
            var data = _sut.SetFinishSteps(_map4);
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public void CheckWallsForExits_WhenExitsOnHorizontalWalls_ReturnsExitsList()
        {
            var data = _sut.SetFinishSteps(_map5);
            Assert.Equal(2, data.Count);
        }

        private readonly List<string> _map1 = new()
        {
            "11111",
            "1   1",
            "1   1",
            "1   1",
            "11111"
        };
        private readonly List<string> _map2 = new()
        {
            " 111 ",
            "1  11",
            "11  1",
            "1   1",
            " 111 "
        };
        private readonly List<string> _map3 = new()
        {
            ""
        };
        private readonly List<string> _map4 = new()
        {
            "11111",
            "    1",
            "1   1",
            "1    ",
            "11111"
        };
        private readonly List<string> _map5 = new()
        {
            "111 1",
            "1   1",
            "1   1",
            "1   1",
            "1 111"
        };
    }
}