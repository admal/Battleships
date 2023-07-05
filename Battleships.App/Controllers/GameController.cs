using Battleships.App.Services;
using Battleships.App.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ILogger<GameController> _logger;

    public GameController(
        IGameService gameService,
        ILogger<GameController> logger)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult StartGame()
    {
        var result = _gameService.StartGame();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        _logger.LogInformation("Game started with guid {guid}", result.Result.GameGuid);
        return Ok(result.Result);
    }

    [HttpPut("hit/{gameGuid}/{x}/{y}")]
    public IActionResult HitCell(Guid gameGuid, int x, int y)
    {
        var model = new HitCellInputModel(gameGuid, x, y);
        var result = _gameService.HitCell(model);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Result);
    }
}
