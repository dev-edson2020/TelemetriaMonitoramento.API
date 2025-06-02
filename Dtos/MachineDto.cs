namespace TelemetriaMonitoramento.API.Dtos
{
    public class MachineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Enums.MachineStatus Status { get; set; }
    }
}
