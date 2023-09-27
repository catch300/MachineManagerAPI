using Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractionn
{
    public interface IMachineService
    {
        public Task<IEnumerable<MachineDetailDto>> GetMachinesAsnyc();
        public Task<MachineDetailDto> GetMachineByIdAsync(int id);

        public Task<int> CreateMachineAsync(MachineForCreationDto machine);
        public Task UpdateMachineAsync(int machineId, MachineForUpdateDto machine);
        public Task DeleteMachineAsync(int machineId);

    }
}
