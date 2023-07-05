using Battleships.App.Configuration;
using Battleships.App.Persistance;

namespace Battleships.App.Services.Spawners;

public class SpawnedCell
{
    public Cell Cell { get; set; }
    public Position Postion { get; set; }

    public SpawnedCell(Position postion, string shipName)
    {
        Postion = postion;
        Cell = new Cell(CellStatus.Ship, shipName);
    }
}

public interface IShipSpawner
{
    IEnumerable<SpawnedCell> SpawnShip(Game game, GridConfiguration.Ship ship); //???
}
