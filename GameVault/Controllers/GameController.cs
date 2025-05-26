using GameVault.Authentication;
using GameVault.Database;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController
{
    private readonly GameContext _gameContext;
    public GameController(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    [HttpPost("create")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<Game> CreateGame(Game game)
    {
        game.Id = Guid.NewGuid();
        await _gameContext.AddAsync(game);
        await _gameContext.SaveChangesAsync();
        return game;
    }

    [HttpGet("getallgames")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<List<Game>> GetGames()
    {
        return await _gameContext.Games.ToListAsync();
    }
}
