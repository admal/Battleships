using Battleships.App.Configuration;
using Battleships.App.Extensions;
using Battleships.App.Persistance;

namespace Battleships.App.Services.Spawners;

public class RandomShipSpawner : IShipSpawner
{
    public IEnumerable<SpawnedCell> SpawnShip(Game game, GridConfiguration.Ship ship)
    {
        var random = new Random();
        var isHorizontal = random.NextBool();

        var widthConstraint = isHorizontal ? game.GridWidth - ship.Size : game.GridWidth;
        var heightConstraint = isHorizontal ? game.GridHeight : game.GridHeight - ship.Size;

        if (widthConstraint < 0 || heightConstraint < 0)
        {
            throw new ShipSpawningException();
        }

        // really simple check to prevent infinite loop, it should be done with more sophisticated validation
        var iterationCount = 0;
        var maxIterationCount = game.GridWidth * game.GridHeight * 100;

        var shipPlaced = false;
        while (!shipPlaced && iterationCount <= maxIterationCount)
        {
            iterationCount++;
            var x = random.Next(0, widthConstraint);
            var y = random.Next(0, heightConstraint);

            var canShipBePlaced = CanShipBePlaced(new Position(x, y), ship.Size, isHorizontal, game);
            if (!canShipBePlaced)
            {
                continue;
            }

            for (var i = 0; i < ship.Size; i++)
            {
                var cell = isHorizontal
                    ? new SpawnedCell(new Position(x + i, y), ship.Name)
                    : new SpawnedCell(new Position(x, y + i), ship.Name);
                yield return cell;
            }
            shipPlaced = true;
        }

        if (iterationCount > maxIterationCount)
        {
            throw new ShipSpawningException();
        }
    }

    private bool CanShipBePlaced(Position startPosition, int shipSize, bool isHorizontal, Game game)
    {
        for (var i = 0; i < shipSize; i++)
        {
            var position = isHorizontal
                ? new Position(startPosition.X + i, startPosition.Y)
                : new Position(startPosition.X, startPosition.Y + i);

            if (game.Cells.ContainsKey(position))
            {
                return false;
            }
        }

        return true;
    }
}
