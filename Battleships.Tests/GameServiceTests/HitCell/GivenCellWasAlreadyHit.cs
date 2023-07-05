using Battleships.App.Services.Models;
using Battleships.App.Services;
using Battleships.Tests.Helpers;
using Battleships.App.Persistance.Entities;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenCellWasAlreadyHit : BattleshipsTestFixture
{
    private GameService _sut;
    private readonly Position _carrierPosition = new Position(1, 2);

    [SetUp]
    public void Setup()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
    }

    [Test]
    public void ThenErrorResponseIsReturned()
    {
        // Arrange
        var gameGuid = SeedRandomGame(carrierPosition: _carrierPosition, carrierHit: true);
        var input = new HitCellInputModel(gameGuid, _carrierPosition.X, _carrierPosition.Y);

        // Act
        var result = _sut.HitCell(input);

        // Assert
        AssertHelpers.AssertResponseIsNotSuccessful(result, "already hit");
    }
}
