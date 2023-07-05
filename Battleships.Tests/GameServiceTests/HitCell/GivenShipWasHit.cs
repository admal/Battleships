using Battleships.App.Persistance.Entities;
using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.HitCell;

public class GivenShipWasHit : BattleshipsTestFixture
{
    private GameService _sut;
    private Guid _gameGuid;
    private readonly Position _carrierPosition = new Position(1, 2);
    private ResponseModel<HitCellInputResultModel> _result;

    [SetUp]
    public void WhenShipWasHit()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
        _gameGuid = SeedRandomGame(carrierPosition: _carrierPosition);

        // Arrange
        var input = new HitCellInputModel(_gameGuid, _carrierPosition.X, _carrierPosition.Y);

        // Act
        _result = _sut.HitCell(input);
    }

    [Test]
    public void ThenHitStatusAndShipNameIsReturned()
    {
        // Assert
        AssertHelpers.AssertResponseIsSuccessful(_result);
        Assert.That(_result.Result.HitResult, Is.EqualTo(HitCellInputResultModel.HitStatus.ShipHit));
    }

    [Test]
    public void ThenShipIsSunk()
    {
        // Assert
        Assert.That(_result.Result.ShipSunk, Is.EqualTo(Carrier));
    }

    [Test]
    public void ThenGameIsFinished()
    {
        // Assert
        Assert.That(_result.Result.GameFinished, Is.True);
    }
}
