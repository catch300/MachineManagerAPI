using Dapper;
using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class FaultRepository : IFaultsRepository
    {
        private readonly DbContext _dbContext;

        public FaultRepository(DbContext dbContext) => _dbContext = dbContext;

        public async Task<int> CountAllFaults()
        {
            var query = @"SELECT COUNT(*) FROM ""Faults"" ";

            using (var connection = _dbContext.CreateConnection())
            {

                var count = await connection.ExecuteScalarAsync<int>(query);
                return count;
            }
        }

        public async Task<IEnumerable<Faults>> GetAllFaultsAsync(int offset, int limit)
        {
            var query = @"SELECT * FROM ""Faults""
                        ORDER BY 
                            CASE WHEN ""Priority"" = 'Low' THEN 1 
                                 WHEN ""Priority"" = 'Medium' THEN 2 
                                 WHEN ""Priority"" = 'High' THEN 3 
                            END ASC, 
                            ""StartTime"" DESC 
                            OFFSET @Offset ROWS
                            FETCH NEXT @Limit ROWS ONLY";
//                        OFFSET @Offset ROWS
//                        LIMIT @Limit";

            using (var connection = _dbContext.CreateConnection())
            {
                var faults = await connection.QueryAsync<Faults>(query, param: new { Offset = offset, Limit = limit });

                return faults;
            }
        }
    }
}
