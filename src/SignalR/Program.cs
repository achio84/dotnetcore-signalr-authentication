using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using SignalR;
using SignalR.Authentication;
using SignalR.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
        .AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = HubTokenAuthenticationDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = HubTokenAuthenticationDefaults.AuthenticationScheme;
        })
        .AddHubTokenAuthenticationScheme();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SignalRPolicy", pol => pol.Requirements.Add(new HubRequirement()));
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/signal", async (string message, IHubContext<ExampleHub, IClientContract> context) =>
{
    await context.Clients.All.MessageReceived(message);
    return Results.Ok(message);
});

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ExampleHub>("/example", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.UseHttpsRedirection();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}