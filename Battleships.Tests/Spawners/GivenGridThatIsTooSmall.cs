using Battleships.App.Configuration;
using Battleships.App.Persistance.Entities;
using Battleships.App.Services.Spawners;

namespace Battleships.Tests.Spawners;

[TestFixture]
public class GivenGridThatIsTooSmall
{
    private RandomShipSpawner _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new RandomShipSpawner();
    }

    [Test]
    public void ThenEmptyListOfCellsIsReturned()
    {
        // Arrange
        var game = new Game(2, 2);
        var ship = new GridConfiguration.Ship { Count = 1, Name = "Carrier", Size = 3 };
        
        // Act & Assert
        Assert.Throws<ShipSpawningException>(() => _sut.SpawnShip(game, ship).ToList());
    }
}
