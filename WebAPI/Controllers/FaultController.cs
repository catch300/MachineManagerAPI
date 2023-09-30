using Application.Abstraction;
using Application.Services;
using Contracts.Faults;
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
            var faults = await _faultsService.GetAllFaultsAsync(currentPageNumber, pageSize);
            if (faults == null) return NotFound();

            if (currentPageNumber > faults.TotalPages)
            {
                return NoContent();
            }
            return Ok(faults);
        }

        [HttpGet("{faultId}")]
        public async Task<IActionResult> GetFaultById(int faultId)
        {
            var fault = await _faultsService.GetFaultByIdAsync(faultId);
            if (fault == null) return NotFound($"Fault with the id: {faultId} does not exist!");

            return Ok(fault);
        }

        [HttpPost]
        public async Task<IActionResult> AddFault([FromBody] FaultForCreationDto fault)
        {
            try
            {
                var faultId = await _faultsService.CreateFaultAsync(fault);
                return CreatedAtAction(nameof(GetFaultById), new { faultId = faultId }, faultId);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{faultId}")]
        public async Task<IActionResult> UpdateFault(int faultId, [FromBody] FaultForUpdatingDto faultForUpdatingDto)
        {
            try
            {
                await _faultsService.UpdateFaultAsync(faultId, faultForUpdatingDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{faultId}")]
        public async Task<IActionResult> UpdateFaultStatus(int faultId, [FromBody] FaultForUpdatingStatusDto faultForUpdatingStatus)
        {
            try
            {
                await _faultsService.UpdateFaultStatusAsync(faultId, faultForUpdatingStatus);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{faultId}")]
        public async Task<IActionResult> DeleteFault(int faultId)
        {
            try
            {
                await _faultsService.DeleteFaultAsync(faultId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
