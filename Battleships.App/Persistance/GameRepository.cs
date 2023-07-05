using Battleships.App.Persistance.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Battleships.App.Persistance
{

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
