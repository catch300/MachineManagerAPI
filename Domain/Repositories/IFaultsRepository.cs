using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFaultsRepository
    {
        public Task<int> CountAllFaults();
        public Task<bool> HasActiveFaultAsync(int machineId);
        public Task<IEnumerable<Faults>> GetAllFaultsAsync(int offset, int limit);
        public Task<Faults> GetFaultByIdAsync(int id);
        public Task<int> CreateFaultAsync(Faults fault);
        public Task UpdateFaultAsync(int faultId, Faults fault);
        public Task UpdateFaultStatusAsync(int faultId, Faults fault);
        public Task DeleteFaultAsync(int faultId);


    }
}
