using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMachineRepository
    {
        public Task<IEnumerable<Machine>> GetAllMachinesAsync();
        public Task<Machine> GetMachineByIdAsync(int id);
        public Task<bool> DoesMachineExistAsync(string machineName);
        public Task<int> CreateMachineAsync(Machine machine);
        public Task UpdateMachineAsync(int machineId, Machine machine);
        public Task DeleteMachineAsync(int machineId);
    }
}
