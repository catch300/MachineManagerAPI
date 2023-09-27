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
        public Task<Machine> GetMachinesById(int id);
        public Task<bool> DoesMachineExistAsync(string machineName);
        public Task<int> AddMachineAsync(Machine machine);
    }
}
