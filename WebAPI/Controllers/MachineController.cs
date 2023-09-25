using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/machines")]
    [ApiController]
    public class MachineController : Controller
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController(IMachineRepository machineRepository) => _machineRepository = machineRepository;

        [HttpGet]
        public async Task<IActionResult> GetMachines()
        {
            var machines = await _machineRepository.GetMachines();
            if (machines == null) return NotFound();
            
            return Ok(machines);

        }
    }
}
