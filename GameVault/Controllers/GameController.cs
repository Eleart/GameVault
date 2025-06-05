using GameVault.Authentication;
using GameVault.Database;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Controllers;

[ApiController]
[Route("[controller]")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class GameController
{
    private readonly GameContext _gameContext;
    public GameController(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    [HttpPost()]
    public async Task<Game> CreateGame(Game game)
    {
        game.Id = Guid.NewGuid();
        await _gameContext.AddAsync(game);
        await _gameContext.SaveChangesAsync();
        return game;
    }

    [HttpGet("all")]
    public async Task<List<Game>> GetGames()
    {
        return await _gameContext.Games.ToListAsync();
    }

    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
    {
        var gameToDelete = _gameContext.Games.FirstOrDefault(x => x.Id == gameId);

        if (gameToDelete is null)
        {
            return new NotFoundObjectResult($"Game with id: {gameId}, not found");
        }

        return new OkObjectResult(gameToDelete);
    }

    [HttpDelete("{gameId}")]
    public async Task<IActionResult> DeleteGame([FromRoute] Guid gameId)
    {
        var gameToDelete = _gameContext.Games.FirstOrDefault(x => x.Id == gameId);

        if (gameToDelete is null)
        {
            return new NotFoundObjectResult($"Game with id: {gameId}, not found");
        }

        _gameContext.Games.Remove(gameToDelete);
        await _gameContext.SaveChangesAsync();
        return new OkObjectResult(gameToDelete);
    }

    [HttpPut("{gameId}")]
    public async Task<IActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] Game gameToUpdate)
    {
        if (gameToUpdate is null)
        {
            return new BadRequestObjectResult("Game to update is null.");
        }
        var existingGame = _gameContext.Games.FirstOrDefault(x => x.Id == gameId);

        if (existingGame is null)
        {
            return new NotFoundObjectResult($"Game with id: {gameId}, not found");
        }

        existingGame.Name = gameToUpdate.Name;
        existingGame.State = gameToUpdate.State;
        existingGame.Plateform = gameToUpdate.Plateform;

        await _gameContext.SaveChangesAsync();
        return new OkObjectResult(existingGame);
    }
}
