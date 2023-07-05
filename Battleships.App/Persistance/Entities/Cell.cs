namespace Battleships.App.Persistance.Entities;

public class Cell
{
    public CellStatus CellStatus { get; set; }
    public string? ShipName { get; set; }

    public Cell(CellStatus cellStatus)
    {
        CellStatus = cellStatus;
    }

    public Cell(CellStatus cellStatus, string? shipName) : this(cellStatus)
    {
        ShipName = shipName;
    }
}
public enum CellStatus
{
    Ship,
    HitShip,
    MissedShot
}
