using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenGameIsFinished : BattleshipsTestFixture
{
    private GameService _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
    }

    [Test]
    public void ThenErrorResponseIsReturned()
    {
        // Arrange
        var gameGuid = SeedRandomGame(true);

        // Act
        var result = _sut.HitCell(new HitCellInputModel(gameGuid, 1, 1));

        // Assert 
        AssertHelpers.AssertResponseIsNotSuccessful(result, "finished");
    }
}
