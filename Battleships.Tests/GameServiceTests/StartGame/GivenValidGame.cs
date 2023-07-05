using Battleships.App.Services;
using Battleships.App.Services.Models;
using Battleships.Tests.Helpers;

namespace Battleships.Tests.GameServiceTests.StartGame;

public class GivenValidGame : BattleshipsTestFixture
{
    private GameService _sut;
    private ResponseModel<StartGameResultModel> _result;

    [SetUp]
    public void WhenGameWasStarted()
    {
        _sut = new GameService(MockedGameRepository, MockedShipSpawner, DefaultGridConfiguration);
        _result = _sut.StartGame();
    }

    [Test]
    public void ThenCreatedGameIsReturned()
    {
        // Assert
        AssertHelpers.AssertResponseIsSuccessful(_result);
        Assert.That(_result.Result.GridWidth, Is.EqualTo(DefaultGridConfiguration.Value.Width));
        Assert.That(_result.Result.GridHeight, Is.EqualTo(DefaultGridConfiguration.Value.Height));
        Assert.That(_result.Result.GameGuid, Is.Not.Empty);
    }

    [Test]
    public void ThenCreatedGameIsSaved()
    {
        var wasSaved = Games.TryGetValue(_result.Result.GameGuid, out var createdGame);
        Assert.That(wasSaved, Is.True);
        Assert.That(createdGame, Is.Not.Null);
        Assert.That(createdGame.GameGuid, Is.EqualTo(_result.Result.GameGuid));
        Assert.That(createdGame.GridWidth, Is.EqualTo(DefaultGridConfiguration.Value.Width));
        Assert.That(createdGame.GridHeight, Is.EqualTo(DefaultGridConfiguration.Value.Height));
        Assert.That(createdGame.Cells, Is.Not.Null);

        var shipCells = createdGame.Cells.Values.Where(x => x.ShipName == $"{Carrier} 1").ToList();
        Assert.That(shipCells.Count, Is.EqualTo(CarrierSize));        
    }
}
