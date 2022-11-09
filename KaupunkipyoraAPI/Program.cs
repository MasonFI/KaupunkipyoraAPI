using KaupunkipyoraAPI.Context;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Repository;
using KaupunkipyoraAPI.Services;
using KaupunkipyoraAPI.Services.Settings;
using System.Data.Common;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Options pattern
builder.Services.Configure<APIOptions>(builder.Configuration);

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

// Add Dapper Unit of work
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddTransient<IBikeRouteRepository, BikeRouteRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Kaupunkipyora API", Version = "v1" });
});

// db-up migrations
builder.Services.AddTransient<IStartupFilter, DatabaseMigrator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kaupunkipyora API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
