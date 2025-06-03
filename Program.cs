using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TelemetriaMonitoramento.API.Data;
using TelemetriaMonitoramento.API.Hubs;
using TelemetriaMonitoramento.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("String de conexão 'DefaultConnection' não foi encontrada.");
}

// Configuração do DbContext corrigida
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuração do tempo de vida do DbContext separadamente
builder.Services.AddScoped<AppDbContext>();

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

// Configuração explícita da porta
app.Urls.Add("http://0.0.0.0:5000");

// Migração do banco de dados com tratamento de erro
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var retries = 0;
        var maxRetries = 5;
        var delay = 5000; // 5 segundos
        
        while (retries < maxRetries)
        {
            try
            {
                db.Database.Migrate();
                break;
            }
            catch (Exception)
            {
                retries++;
                if (retries == maxRetries) throw;
                Thread.Sleep(delay);
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
}

// Swagger sempre disponível
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Telemetria de Máquinas v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<TelemetryHub>("/hub/telemetry");

// Endpoint de health check
app.MapGet("/health", () => Results.Ok());

app.Run();