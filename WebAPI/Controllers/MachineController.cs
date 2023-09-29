using Application.Abstraction;
using Contracts.Machines;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/machines")]
    [ApiController]
    public class MachineController : Controller
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService) => _machineService = machineService;

        [HttpGet]
        public async Task<IActionResult> GetMachines()
        {
            var machines = await _machineService.GetMachinesAsnyc();
            if (machines == null) return NotFound();

            return Ok(machines);

        }

        [HttpGet("{machineId}")]
        public async Task<IActionResult> GetMachineById(int machineId)
        {
            var machines = await _machineService.GetMachineByIdAsync(machineId);
            if (machines == null) return NotFound($"Machine with the id: {machineId} does not exist!");

            return Ok(machines);
        }

        [HttpPost]
        public async Task<IActionResult> AddMachine([FromBody] MachineForCreationDto machine)
        {
            try
            {
                var machineId = await _machineService.CreateMachineAsync(machine);
                return CreatedAtAction(nameof(GetMachineById), new { machineId = machineId }, machine);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{machineId}")]
        public async Task<IActionResult> UpdateMachine(int machineId, [FromBody] MachineForUpdateDto machineForUpdate)
        {
            try
            {
                await _machineService.UpdateMachineAsync(machineId, machineForUpdate);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{machineId}")]
        public async Task<IActionResult> DeleteMachine(int machineId)
        {
            try
            {
                await _machineService.DeleteMachineAsync(machineId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
