using Grpc.Net.Client;
using UrlShortener.API;
using UrlShortener.API.Controllers;
using UrlShortener.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IShortenerService, ShortenerService>();
builder.Services.AddHealthChecks();

using var statsChannel =
    GrpcChannel.ForAddress(builder.Configuration.GetConnectionString("gRPCStatistics")!);
var statsClient = new Statistics.StatisticsClient(statsChannel);
builder.Services.AddSingleton(statsClient);

using var serviceChannel =
    GrpcChannel.ForAddress(builder.Configuration.GetConnectionString("gRPCService")!);
var serviceClient = new Shortener.ShortenerClient(serviceChannel);
builder.Services.AddSingleton(serviceClient);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
