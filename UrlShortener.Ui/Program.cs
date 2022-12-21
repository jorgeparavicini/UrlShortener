using UrlShortener.Ui.Data;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<StatisticService>();
builder.Services.AddSingleton<AddressResolverService>();
builder.Services.AddSingleton<ShortenAddressService>();
builder.Services.AddHttpClient();
builder.Services.AddHealthChecks().ForwardToPrometheus();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseHttpMetrics();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapMetrics();

app.Run();
