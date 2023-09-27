using Dapper;
using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class FaultRepository : IFaultsRepository
    {
        private readonly DbContext _dbContext;

        public FaultRepository(DbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Faults>> GetFaults()
        {
            var query = "SELECT * FROM \"Malfunction\"";

            using (var connection = _dbContext.CreateConnection())
            {
                var malfunctions = await connection.QueryAsync<Faults>(query);

                
                return malfunctions.ToList();
            }
        }
    }
}
