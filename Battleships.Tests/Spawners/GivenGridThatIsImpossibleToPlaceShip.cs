using Battleships.App.Persistance;
using Battleships.App.Persistance.Entities;
using Battleships.App.Services.Spawners;
using static Battleships.App.Configuration.GridConfiguration;

namespace Battleships.Tests.Spawners;

[TestFixture]
public class GivenGridThatIsImpossibleToPlaceShip
{
    private RandomShipSpawner _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new RandomShipSpawner();
    }

    [Test]
    public void ThenExceptionIsThrown()
    {
        var game = new Game(5, 5);
        game.Cells.Add(new Position(0, 0), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(1, 1), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(2, 2), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(3, 3), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(4, 4), new Cell(CellStatus.Ship, "OtherCarrier"));
        var ship = new Ship { Count = 1, Name = "Carrier", Size = 5 };

        // Act & Assert
        Assert.Throws<ShipSpawningException>(() => _sut.SpawnShip(game, ship).ToList());
    }
}
