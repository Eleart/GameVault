﻿using GameVault.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Database;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }
}
