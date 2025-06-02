namespace TelemetriaMonitoramento.API.Dtos
{
    public class UpdateMachineStatusDto
    {
        public string Location { get; set; } = string.Empty;
        public Enums.MachineStatus Status { get; set; }
    }
}
