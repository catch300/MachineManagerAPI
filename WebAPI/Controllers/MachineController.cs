using Application.Abstractionn;
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
            var machines = await _machineService.GetMachines();
            if (machines == null) return NotFound();

            return Ok(machines);

        }

        [HttpGet("{id}", Name ="MachineById")]
        public async Task<IActionResult> GetMachinesById(int id)
        {
            var machines = await _machineService.GetMachinesById(id);
            if (machines == null) return NotFound();

            return Ok(machines);

        }
    }
}
