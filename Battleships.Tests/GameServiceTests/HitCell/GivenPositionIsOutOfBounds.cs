using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenPositionIsOutOfBounds : BattleshipsTestFixture
{
    private GameService _sut;
    private Guid _gameGuid;

    [SetUp]
    public void Setup()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
        _gameGuid = SeedRandomGame();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(DefaultGridWidth + 1)]
    public void WhenXPositionIsOutOfBoundThenErrorResponseIsReturned(int x)
    {
        // Arrange
        var inputModel = new HitCellInputModel(_gameGuid, x, 1);

        // Act 
        var result = _sut.HitCell(inputModel);

        // Assert
        AssertHelpers.AssertResponseIsNotSuccessful(result, "X is out of range");
    }

    [Test]
    [TestCase(-1)]
    [TestCase(DefaultGridHeight + 1)]
    public void WhenYPositionIsOutOfBoundThenErrorResponseIsReturned(int y)
    {
        // Arrange
        var inputModel = new HitCellInputModel(_gameGuid, 1, y);

        // Act 
        var result = _sut.HitCell(inputModel);

        // Assert
        AssertHelpers.AssertResponseIsNotSuccessful(result, "Y is out of range");
    }
}
