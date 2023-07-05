using Battleships.App.Configuration;
using Battleships.App.Persistance;
using Battleships.App.Persistance.Entities;
using Battleships.App.Services.Spawners;
using Microsoft.Extensions.Options;
using Moq;

namespace Battleships.Tests;

[TestFixture]
public class BattleshipsTestFixture
{
    public const string Carrier = "Carrier";
    public const int CarrierSize = 5;
    public const int DefaultGridWidth = 10;
    public const int DefaultGridHeight = 10;

    protected IGameRepository MockedGameRepository;
    protected IOptions<GridConfiguration> DefaultGridConfiguration;
    protected IShipSpawner MockedShipSpawner;

    protected Dictionary<Guid, Game> Games { get; private set; } = new Dictionary<Guid, Game>();

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        SetupGameRepositoryMock();

        DefaultGridConfiguration = Options.Create(new GridConfiguration
        {
            Height = DefaultGridHeight,
            Width = DefaultGridWidth,
            ShipsToSpawn = new List<GridConfiguration.Ship>
            {
                new GridConfiguration.Ship
                {
                    Name = Carrier,
                    Size = CarrierSize,
                    Count = 1
                }
            }
        });

        var shipSpawnerMock = new Mock<IShipSpawner>();
        shipSpawnerMock.Setup(x => x.SpawnShip(It.IsAny<Game>(), It.IsAny<GridConfiguration.Ship>())).Returns(() =>
        {
            var cells = new List<SpawnedCell>();
            for (var i = 0; i < CarrierSize; i++)
            {
                var position = new Position(i, 0);
                cells.Add(new SpawnedCell(position, Carrier));
            }

            return cells;
        });
        MockedShipSpawner = shipSpawnerMock.Object;
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }

    protected Guid SeedRandomGame(
        bool finished = false, Position? carrierPosition = null, bool carrierHit = false)
    {
        var game = new Game(DefaultGridWidth, DefaultGridHeight);
        game.Finished = finished;

        if (carrierPosition != null)
        {
            var cellStatus = carrierHit ? CellStatus.HitShip : CellStatus.Ship;
            game.Cells.Add(carrierPosition, new Cell(cellStatus, Carrier));
        }

        Games.Add(game.GameGuid, game);

        return game.GameGuid;
    }

    private void SetupGameRepositoryMock()
    {
        var gameRepositoryMock = new Mock<IGameRepository>();
        gameRepositoryMock.Setup(x => x.GetGame(It.IsAny<Guid>())).Returns<Guid>(gameGuid =>
        {
            if (Games.ContainsKey(gameGuid))
            {
                return Games[gameGuid];
            }

            return null;
        });

        gameRepositoryMock.Setup(x => x.SaveGame(It.IsAny<Game>())).Callback<Game>(game =>
        {
            if (Games.ContainsKey(game.GameGuid))
            {
                Games[game.GameGuid] = game;
            }
            else
            {
                Games.Add(game.GameGuid, game);
            }
        });

        MockedGameRepository = gameRepositoryMock.Object;
    }
}
