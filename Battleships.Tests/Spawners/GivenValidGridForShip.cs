using Battleships.App.Persistance;
using Battleships.App.Services.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleships.App.Configuration.GridConfiguration;

namespace Battleships.Tests.Spawners;

public class GivenValidGridForShip
{
    private RandomShipSpawner _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new RandomShipSpawner();
    }

    public void ThenShipIsPlaced()
    {
        var game = new Game(5, 5);
        game.Cells.Add(new Position(0, 0), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(1, 1), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(2, 2), new Cell(CellStatus.Ship, "OtherCarrier"));
        game.Cells.Add(new Position(3, 3), new Cell(CellStatus.Ship, "OtherCarrier"));

        var ship = new Ship { Count = 1, Name = "Carrier", Size = 3 };
    }
}
