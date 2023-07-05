using Battleships.App.Persistance.Entities;

namespace Battleships.App.Services.Models;

public class HitCellInputModel
{
    public Guid GameGuid { get; }
    public Position Postion { get; }

    public HitCellInputModel(Guid gameGuid, int x, int y)
    {
        GameGuid = gameGuid;
        Postion = new Position(x, y);
    }
}

public class HitCellInputResultModel
{
    public HitStatus HitResult { get; }
    public bool GameFinished { get; }
    public string ShipSunk { get; set; }

    public HitCellInputResultModel(HitStatus hitResult, bool gameFinished, string shipSunk)
    {
        HitResult = hitResult;
        GameFinished = gameFinished;
        ShipSunk = shipSunk;
    }

    public enum HitStatus
    {
        ShipHit,
        MissedHit
    }
}
