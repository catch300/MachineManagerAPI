using Dapper;
using Domain.Models;
using Domain.Repositories;
using System.Data;
using Npgsql;
using Npgsql.Internal.TypeHandling;
using System.Reflection.PortableExecutable;

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

        public async Task<bool> HasActiveFaultAsync(int machineId)
        {
            var query = @"SELECT EXISTS (
                          SELECT 1
                          FROM ""Faults""
                          WHERE ""MachineId"" = @MachineId
                          AND ""IsResolved"" = false
                      );";

            using (var connection = _dbContext.CreateConnection())
            {
                var hasActiveFault = await connection.ExecuteScalarAsync<bool>(query, new { MachineId = machineId });
                return hasActiveFault;
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

        public async Task<Faults> GetFaultByIdAsync(int id)
        {
            var query = @"
                        SELECT * FROM ""Faults""
                        WHERE ""FaultId"" = @FaultId";

            using (var connection = _dbContext.CreateConnection())
            {
                var faults = await connection.QuerySingleOrDefaultAsync<Faults>(query, param: new { FaultId = id });
                return faults;
            }
        }

        public async Task<int> CreateFaultAsync(Faults fault)
        {
            var query = @"INSERT INTO ""Faults""
                            (""Name"",
                            ""StartTime"",
                            ""EndTime"",
                            ""Description"",
                            ""IsResolved"",
                            ""MachineId"",
                            ""Priority"")
	                    VALUES (
                            @Name,
                            @StartTime,
                            @EndTime,
                            @Description,
                            @IsResolved, 
                            @MachineId, 
                            @Priority)
                        RETURNING ""FaultId"";";


            var parameters = new DynamicParameters();
            parameters.Add("Name", fault.Name);
            parameters.Add("StartTime", fault.StartTime);
            parameters.Add("EndTime", fault.EndTime);
            parameters.Add("Description", fault.Description);
            parameters.Add("IsResolved", fault.IsResolved);
            parameters.Add("MachineId", fault.MachineId);
            parameters.Add("Priority", fault.EndTime);

            using (var connection = _dbContext.CreateConnection())
            {
                
                var faultId = await connection.ExecuteScalarAsync<int>(query, parameters);
                return faultId;
            }
        }

        public async Task UpdateFaultAsync(int faultId, Faults fault)
        {
            var query = @"UPDATE ""Faults""
	                    SET ""Name""        = @Name,
                            ""StartTime""   = @StartTime,
                            ""EndTime""     = @EndTime,
                            ""Description"" = @Description,
                            ""IsResolved""  = @IsResolved,
                            ""MachineId""   = @MachineId,
                            ""Priority""    = @Priority
	                    WHERE ""FaultId""   = @FaultId;";

            var parameters = new DynamicParameters();
            parameters.Add("Name", fault.Name);
            parameters.Add("StartTime", fault.StartTime);
            parameters.Add("EndTime", fault.EndTime);
            parameters.Add("Description", fault.Description);
            parameters.Add("IsResolved", fault.IsResolved);
            parameters.Add("MachineId", fault.MachineId);
            parameters.Add("Priority", fault.EndTime);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateFaultStatusAsync(int faultId, Faults fault)
        {
            // TODO
            // if IsResolved field is set to true then also set EndTime to current time ??

            //  SET
            //  ""EndTime"" = CASE WHEN @IsResolved THEN NOW() ELSE ""EndTime"" END

            var query = @"UPDATE ""Faults""
                       SET ""IsResolved"" = @IsResolved
                       WHERE ""FaultId"" = @FaultId;";

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { FaultId = faultId, IsResolved = fault.IsResolved });
            }
        }
        
        public async Task DeleteFaultAsync(int faultId)
        {
            var query = @"DELETE FROM ""Faults""
	                        WHERE ""FaultId""=@FaultId";
            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, param: new { faultId });

            }
        }
    }
}
