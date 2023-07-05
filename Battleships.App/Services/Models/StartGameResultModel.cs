namespace Battleships.App.Services.Models;

public class StartGameResultModel
{
    public Guid GameGuid { get; }
    public int GridWidth { get; }
    public int GridHeight { get; }

    public StartGameResultModel(Guid guidGame, int gridWidth, int gridHeight)
    {
        GameGuid = guidGame;
        GridWidth = gridWidth;
        GridHeight = gridHeight;
    }
}
