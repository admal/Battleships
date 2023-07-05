using Battleships.App.Persistance.Entities;
using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenHitMissed : BattleshipsTestFixture
{
    private GameService _sut;
    private Guid _gameGuid;
    private readonly Position _carrierPosition = new Position(1, 2);
    private readonly Position _hitPosition = new Position(1, 3);
    private ResponseModel<HitCellInputResultModel> _result;

    [SetUp]
    public void WhenHitWasMade()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
        _gameGuid = SeedRandomGame(carrierPosition: _carrierPosition);

        // Arrange
        var input = new HitCellInputModel(_gameGuid, _hitPosition.X, _hitPosition.Y);

        // Act
        _result = _sut.HitCell(input);
    }

    [Test]
    public void ThenMissedHitResponseIsReturned()
    {
        // Assert
        AssertHelpers.AssertResponseIsSuccessful(_result);
        Assert.That(_result.Result.HitResult, Is.EqualTo(HitCellInputResultModel.HitStatus.MissedHit));
        Assert.That(_result.Result.ShipSunk, Is.Null);
        Assert.That(_result.Result.GameFinished, Is.False);
    }

    [Test]
    public void ThenCellIsUpdatedToMissedHit()
    {
        // Assert
        var game = Games[_gameGuid];
        Assert.That(game.Cells[_carrierPosition].CellStatus, Is.EqualTo(CellStatus.Ship));
        Assert.That(game.Cells[_hitPosition].CellStatus, Is.EqualTo(CellStatus.MissedShot));
    }
}
