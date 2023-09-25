using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMachineRepository
    {
        public Task<IEnumerable<Machine>> GetMachines();
    }
}
