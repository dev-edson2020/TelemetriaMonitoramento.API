using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TelemetriaMonitoramento.API.Data;
using TelemetriaMonitoramento.API.Dtos;
using TelemetriaMonitoramento.API.Models;
using TelemetriaMonitoramento.API.Hubs;
using TelemetriaMonitoramento.API.Enums;

namespace TelemetriaMonitoramento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<TelemetryHub> _hubContext;

        public MachinesController(
            AppDbContext context,
            IMapper mapper,
            IHubContext<TelemetryHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDto>>> GetAllMachines()
        {
            var machines = await _context.Machines.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MachineDto>>(machines));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineDto>> GetMachine(Guid id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
                return NotFound();

            return Ok(_mapper.Map<MachineDto>(machine));
        }

        [HttpPost]
        public async Task<ActionResult<MachineDto>> CreateMachine(CreateMachineDto dto)
        {
            var machine = _mapper.Map<Machine>(dto);
            machine.Id = Guid.NewGuid();
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMachine), new { id = machine.Id }, _mapper.Map<MachineDto>(machine));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateMachineStatus(Guid id, [FromBody] UpdateMachineStatusDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
                return NotFound();
            machine.Status = dto.Status;
            machine.Location = dto.Location;
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveStatusUpdate", machine.Id.ToString(), machine.Status.ToString());
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MachineDto>>> FilterByStatus([FromQuery] string status)
        {
            if (!Enum.TryParse<MachineStatus>(status, true, out var parsedStatus))
                return BadRequest("Status inválido.");

            var filtered = await _context.Machines
                .Where(m => m.Status == parsedStatus)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<MachineDto>>(filtered));
        }

    }
}
