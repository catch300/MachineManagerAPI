using Dapper;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly DbContext _dbContext;

        public MachineRepository(DbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Machine>> GetMachines()
        {
            var query = "SELECT * FROM Machines";
            using (var connection = _dbContext.CreateConnection())
            {
                var machines = await connection.QueryAsync<Machine>(query);

                return machines.ToList();
            }
        }
    }
}
