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
        public Task<IEnumerable<Faults>> GetAllFaultsAsync(int offset, int limit);
        public Task<int> CountAllFaults();
    }
}
