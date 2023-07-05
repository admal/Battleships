using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenGameDoesNotExist : BattleshipsTestFixture
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
        SeedRandomGame();

        // Act
        var result = _sut.HitCell(new HitCellInputModel(Guid.NewGuid(), 1, 1));

        // Assert 
        AssertHelpers.AssertResponseIsNotSuccessful(result, "not found");
    }
}
