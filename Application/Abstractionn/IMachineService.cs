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
        public Task<IEnumerable<MachineDetail>> GetMachines();
        public Task<MachineDetail> GetMachinesById(int id);
    }
}
