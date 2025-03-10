using Microsoft.AspNetCore.Mvc;
using RedisImMemmoryDB;
using RedisImMemmoryDB.Entities;
using RedisImMemmoryDB.Repositories;
using RedisImMemmoryDB.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IInvestimentRepository, InvestimentRepository>();
builder.Services.AddScoped<IInvestimentService, InvestimentService>();

// configuração do redis cache
builder.Services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = builder.Configuration.GetConnectionString("RedisCache");
    op.InstanceName = "investiments:"; // prefix opticional
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/insertInvestiment",
    async ([FromServices] IInvestimentService service, [FromBody] Investiment investiment) =>
    {
        var investimentId = await service.AddAsync(investiment);
        return Results.Created("", investimentId);
    });

app.MapGet("/investiments/{id}", async ([FromServices] IInvestimentService service, Guid userId) =>
{
    var result = await service.GetByIdAsync(userId);
    return Results.Ok(result);
});

app.Run();