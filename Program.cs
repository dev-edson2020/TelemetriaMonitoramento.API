using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TelemetriaMonitoramento.API.Data;
using TelemetriaMonitoramento.API.Hubs;
using TelemetriaMonitoramento.API.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Obtem a string de conexão da configuração ou variável de ambiente
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

// Verifica se a string de conexão foi encontrada
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("String de conexão 'DefaultConnection' não foi encontrada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Telemetria de Máquinas",
        Version = "v1",
        Description = "Desafio Técnico - Desenvolvedor Júnior"
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();
builder.Services.AddHostedService<TelemetrySimulatorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Telemetria de Máquinas v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<TelemetryHub>("/hub/telemetry");

app.Run();
