using Contracts.Faults;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IFaultsService
    {
        public Task<IPaginatedList<IEnumerable<FaultDto>>> GetAllFaultsAsync(int currentPageNumber, int pageSize);
        public Task<FaultDto> GetFaultByIdAsync(int id);
        public Task<int> CreateFaultAsync(FaultForCreationDto faultForCreation);
        public Task UpdateFaultAsync(int faultId, FaultForUpdatingDto faultForUpdatingDto);
        public Task UpdateFaultStatusAsync(int faultId, FaultForUpdatingStatusDto faultForUpdatingStatus);
        public Task DeleteFaultAsync(int faultId);
    }
}
