using Microsoft.AspNetCore.Mvc;
using RedisImMemmoryDB;
using RedisImMemmoryDB.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvestimentService, InvestimentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/insertInvestiment", async ([FromServices] IInvestimentService service, [FromBody] Investiment investiment) => {
    await service.AddInvestimentAsync(investiment);
    return Results.Created("", investiment.Id);
});

app.MapGet("/investiments/{id}", async ([FromServices] IInvestimentService service, Guid userId) =>
{
    var result = await service.GetInvestimentsAsync(userId);
    return Results.Ok(result);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
