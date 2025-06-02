using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TelemetriaMonitoramento.API.Data;
using TelemetriaMonitoramento.API.Enums;
using TelemetriaMonitoramento.API.Hubs;

namespace TelemetriaMonitoramento.API.Services
{
    public class TelemetrySimulatorService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<TelemetryHub> _hubContext;
        private readonly Random _random = new();

        public TelemetrySimulatorService(IServiceScopeFactory scopeFactory, IHubContext<TelemetryHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var machines = await context.Machines.ToListAsync(stoppingToken);
                    if (machines.Count == 0)
                    {
                        await Task.Delay(5000, stoppingToken);
                        continue;
                    }

                    var machine = machines[_random.Next(machines.Count)];

                    var newLocation = SimulateLocation(machine.Location);
                    var newStatus = SimulateStatus(machine.Status);

                    machine.Location = newLocation;
                    machine.Status = newStatus;

                    await context.SaveChangesAsync(stoppingToken);

                    await _hubContext.Clients.All.SendAsync(
                        "ReceiveStatusUpdate",
                        machine.Id.ToString(),
                        machine.Status.ToString(),
                        newLocation,
                        cancellationToken: stoppingToken
                    );
                }
                catch
                {
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        private string SimulateLocation(string currentLocation)
        {
            var parts = currentLocation.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 &&
                double.TryParse(parts[0], out double lat) &&
                double.TryParse(parts[1], out double lon))
            {
                lat += (_random.NextDouble() - 0.5) / 500;
                lon += (_random.NextDouble() - 0.5) / 500;
                return $"{lat:F6},{lon:F6}";
            }

            return currentLocation;
        }

        private MachineStatus SimulateStatus(MachineStatus currentStatus)
        {
            var values = Enum.GetValues<MachineStatus>();
            if (_random.NextDouble() < 0.7)
                return currentStatus;

            MachineStatus newStatus;
            do
            {
                newStatus = values[_random.Next(values.Length)];
            } while (newStatus == currentStatus);

            return newStatus;
        }
    }
}
