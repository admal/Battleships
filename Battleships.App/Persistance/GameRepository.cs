using Microsoft.Extensions.Caching.Memory;

namespace Battleships.App.Persistance
{
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

    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position postion &&
                   X == postion.X &&
                   Y == postion.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public enum CellStatus
    {
        Ship,
        HitShip,
        MissedShot
    }

    public interface IGameRepository
    {
        Game GetGame(Guid gameGuid);
        void SaveGame(Game game);
    }

    public class GameRepository : IGameRepository
    {
        private readonly IMemoryCache _memoryCache;

        public GameRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Game GetGame(Guid gameGuid)
        {
            if (_memoryCache.TryGetValue(gameGuid, out Game game))
            {
                return game;
            }

            return null;
        }

        public void SaveGame(Game game)
        {
            _memoryCache.Set(game.GameGuid, game);
        }
    }
}
