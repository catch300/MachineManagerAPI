using Application.Abstractionn;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/malfunctions")]
    [ApiController]
    public class MalfunctionController : Controller
    {
        private readonly IFaultsService _faultsService;

        public MalfunctionController(IFaultsService faultsService) => _faultsService = faultsService;

        [HttpGet]
        public async Task<IActionResult> GetMalfunctions()
        {
            var malfunctions = await _faultsService.GetFaults();
            if (malfunctions == null) return NotFound();
            
            return Ok(malfunctions);
        }
    }
}
