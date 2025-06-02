namespace TelemetriaMonitoramento.API.Dtos
{
    public class CreateMachineDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Enums.MachineStatus Status { get; set; }
    }
}
