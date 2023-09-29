using Contracts;
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
    }
}
