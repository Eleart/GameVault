using GameVault.Authentication;
using GameVault.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", SwaggerConfiguration.OpenApiSecurityScheme);

    c.AddSecurityRequirement(SwaggerConfiguration.Requirement);
});
builder.Services.AddDbContextPool<GameContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GameContext")));

builder.Services.AddScoped<ApiKeyAuthFilter>();
var app = builder.Build();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<GameContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
