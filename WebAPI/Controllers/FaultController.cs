using Application.Abstraction;
using Application.Services;
using Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/faults")]
    [ApiController]
    public class FaultController : Controller
    {
        private readonly IFaultsService _faultsService;

        public FaultController(IFaultsService faultsService) => _faultsService = faultsService;

        [HttpGet]
        public async Task<IActionResult> GetFaults(int currentPageNumber, int pageSize)
        {
            var malfunctions = await _faultsService.GetAllFaultsAsync(currentPageNumber, pageSize);
            if (malfunctions == null) return NotFound();
            
            return Ok(malfunctions);
        }
    }
}
