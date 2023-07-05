namespace Battleships.App.Services.Spawners;

public class ShipSpawningException : Exception
{
    public ShipSpawningException()
    {
    }

    public ShipSpawningException(string shipName) : base($"Ship {shipName} can't be spawned")
    {
    }
}
