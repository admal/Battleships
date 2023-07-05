using Battleships.App.Configuration;
using Battleships.App.Persistance;
using Battleships.App.Services.Models;
using Battleships.App.Services.Spawners;
using Microsoft.Extensions.Options;

namespace Battleships.App.Services;

public interface IGameService
{
    ResponseModel<HitCellInputResultModel> HitCell(HitCellInputModel input);
    ResponseModel<StartGameResultModel> StartGame();
}


public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IShipSpawner _shipSpawner;
    private readonly GridConfiguration _gridConfiguration;

    public GameService(
        IGameRepository gameRepository,
        IShipSpawner shipSpawner,
        IOptions<GridConfiguration> gridConfigurationOptions)
    {
        _gameRepository = gameRepository;
        _shipSpawner = shipSpawner;
        _gridConfiguration = gridConfigurationOptions.Value;
    }

    public ResponseModel<StartGameResultModel> StartGame()
    {
        var game = new Game(_gridConfiguration.Width, _gridConfiguration.Height);
        foreach (var ship in _gridConfiguration.ShipsToSpawn)
        {
            var cells = _shipSpawner.SpawnShip(game, ship).ToList();
            foreach (var cell in cells)
            {
                game.Cells.Add(cell.Postion, cell.Cell);
            }
        }

        _gameRepository.SaveGame(game);

        var result = new StartGameResultModel(game.GameGuid, game.GridWidth, game.GridHeight);
        return ResponseModel<StartGameResultModel>.ForSuccess(result);
    }

    public ResponseModel<HitCellInputResultModel> HitCell(HitCellInputModel input)
    {
        var game = _gameRepository.GetGame(input.GameGuid);
        if (game == null)
        {
            return ResponseModel<HitCellInputResultModel>.ForError("Game not found");
        }

        var validationResult = ValidateHit(input, game);
        if (!validationResult.IsSuccess)
        {
            return ResponseModel<HitCellInputResultModel>.ForError(validationResult.ErrorMessage);
        }

        var resultHitStatus = HitCellInputResultModel.HitStatus.MissedHit;
        string shipSunkName = null;

        if (game.Cells.TryGetValue(input.Postion, out var cell))
        {
            var cellStatus = cell.CellStatus;
            if (cellStatus == CellStatus.Ship)
            {
                game.Cells[input.Postion].CellStatus = CellStatus.HitShip;
                resultHitStatus = HitCellInputResultModel.HitStatus.ShipHit;

                var hitShipName = game.Cells[input.Postion].ShipName;
                var shipSunk = game.Cells.Values
                    .Where(x => x.ShipName == hitShipName)
                    .All(cell => cell.CellStatus == CellStatus.HitShip);
                if (shipSunk)
                {
                    shipSunkName = hitShipName;
                }
            }
            else
            {
                return ResponseModel<HitCellInputResultModel>.ForError("Cell already hit");
            }
        }
        else
        {
            game.Cells.Add(input.Postion, new Cell(CellStatus.MissedShot));
            resultHitStatus = HitCellInputResultModel.HitStatus.MissedHit;
        }

        if (game.Cells.Values.All(cell => cell.CellStatus != CellStatus.Ship))
        {
            game.Finished = true;
        }


        var result = new HitCellInputResultModel(resultHitStatus, game.Finished, shipSunkName);
        return ResponseModel<HitCellInputResultModel>.ForSuccess(result);
    }

    private ResponseModel ValidateHit(HitCellInputModel input, Game game)
    {
        if (game.Finished)
        {
            return ResponseModel.ForError("Game already finished");
        }

        if (input.Postion.X < 0 || input.Postion.X >= game.GridWidth)
        {
            return ResponseModel.ForError("X is out of range");
        }

        if (input.Postion.Y < 0 || input.Postion.Y >= game.GridHeight)
        {
            return ResponseModel.ForError("Y is out of range");
        }

        return ResponseModel.ForSuccess();
    }
}
