using Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractionn
{
    public interface IFaultsService
    {
        public Task<IEnumerable<MachineDetail>> GetFaults();
    }
}
