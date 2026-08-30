using DBStations.Configuration;
using DBStations.Data;
using DBStations.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddHttpClient();
builder.Services.AddTransient<HttpRequester>();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Default connection string not configured");

builder.Services.AddDbContext<DBStationsDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddHostedService<StationPollingService>();
builder.Services.AddHostedService<FacilityPollingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
