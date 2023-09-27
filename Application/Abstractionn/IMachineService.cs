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
        public Task<MachineDetailDto> GetMachinesByIdAsync(int id);

        public Task<int> InsertMachineAsync(MachineForCreationDto machine);
        //public Task<int> UpdateMachineAsync(MachineForCreationDto machine);
        
    }
}
