using GameRunner;
using Moq;

namespace GameTests
{
    public class TestingGame
    {
        private readonly Game _sut;

        public TestingGame()
        {
            _sut = new Game();
        }
        [Fact]
        public void RunGame_WhenFileIsEmpty_ReturnIntZero()
        {
            var gameMock = new Mock<IGame>();
            gameMock.Setup(x => x.Run(It.IsAny<string>())).Returns(0);

            var sut = new Game();

            var data = sut.Run(@"TestData\map3.txt");

            gameMock.VerifyNoOtherCalls();
            Assert.Equal(0, data);
        }

        [Fact]
        public void RunGame_WhenFileMapDoesNotHaveStartPoint_ReturnIntZero()
        {
            var data = _sut.Run(@"TestData\map4.txt");
            Assert.Equal(0, data);
        }

        [Fact]
        public void RunGame_WhenFileMapDoesNotHaveExit_ReturnIntZero()
        {
            var data = _sut.Run(@"TestData\map5.txt");
            Assert.Equal(0, data);
        }

        [Fact]
        public void RunGame_WhenFileMapHaveExitPointsOnCorner_ReturnIntZero()
        {
            var data = _sut.Run(@"TestData\map6.txt");
            Assert.Equal(0, data);
        }
        [Fact]
        public void RunGame_WhenFileMapHaveNonReachableExitPoint_ReturnIntZero()
        {
            var data = _sut.Run(@"TestData\map7.txt");
            Assert.Equal(0, data);
        }
        [Fact]
        public void RunGame_WhenFileMapHaveNonReachableExitPointAndOneReachable_ReturnJustReachableExitSteps()
        {
            var data = _sut.Run(@"TestData\map8.txt");
            Assert.Equal(9, data);
        }
        [Fact]
        public void RunGame_WhenFileMapHaveTwoExitPointsBesideEachOther_ReturnExitWhichIsFaster()
        {
            var gameMock = new Mock<IGame>();
            gameMock.Setup(x => x.Run(It.IsAny<string>())).Returns(9);

            var sut = new Game();

            var data = sut.Run(@"TestData\map9.txt");

            gameMock.VerifyNoOtherCalls();
            Assert.Equal(9, data);
        }
        [Fact]
        public void RunGame_WhenOneStepToFinish_ReturnOne()
        {
            var data = _sut.Run(@"TestData\map10.txt");
            Assert.Equal(1, data);
        }
        [Fact]
        public void RunGame_WhenStartStepAndFinishStepIsAtTheSamePoint_ReturnZero()
        {
            var data = _sut.Run(@"TestData\map11.txt");
            Assert.Equal(0, data);
        }
        [Fact]
        public void RunGame_WhenFileCorrupted_ReturnZero()
        {
            var data = _sut.Run(@"TestData\map12.txt");
            Assert.Equal(0, data);
        }
    }
}