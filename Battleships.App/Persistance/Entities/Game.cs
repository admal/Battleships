namespace Battleships.App.Persistance.Entities;

public class Game
{
    public Guid GameGuid { get; }
    public bool Finished { get; set; }
    public int GridWidth { get; }
    public int GridHeight { get; }
    public Dictionary<Position, Cell> Cells { get; }

    public Game(int gridWidth, int gridHeight)
    {
        GameGuid = Guid.NewGuid();
        GridWidth = gridWidth;
        GridHeight = gridHeight;
        Cells = new Dictionary<Position, Cell>();
    }
}
