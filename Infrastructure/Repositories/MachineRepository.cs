using Dapper;
using Domain.Models;
using Domain.Repositories;
using System.Data;


namespace Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly DbContext _dbContext;

        public MachineRepository(DbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
        {
            var query = @"
                        SELECT
                            m.""MachineId"",
                            m.""Name"",
                            f.""MachineId"",
                            f.""Name"",
                            f.""StartTime"",
                            f.""EndTime""
                        FROM
                            ""Machines"" m
                        LEFT JOIN
                            ""Faults"" f ON m.""MachineId"" = f.""MachineId"";";

                using (var connection = _dbContext.CreateConnection())
                {
                    var machineDictionary = new Dictionary<int, Machine>();

                    var machines = await connection.QueryAsync<Machine, Faults, Machine>(query,
                        (machine, fault) =>
                        {
                            if (!machineDictionary.TryGetValue(machine.MachineId, out var machineEntry))
                            {
                                machineEntry = machine;
                                machineEntry.Faults = new List<Faults>();
                                machineDictionary.Add(machine.MachineId, machineEntry);
                            }
                            if (fault != null) machineEntry.Faults.Add(fault);
                            return machineEntry;
                        },
                        splitOn: "MachineId");
                    return machineDictionary.Values;
                }
            }

        public async Task<Machine> GetMachineByIdAsync(int machineId)
        {
            var query = @"
                        SELECT
                            m.""MachineId"",
                            m.""Name"",
                            f.""MachineId"",
                            f.""Name"",
                            f.""StartTime"",
                            f.""EndTime""
                        FROM
                            ""Machines"" m
                        LEFT JOIN
                            ""Faults"" f ON m.""MachineId"" = f.""MachineId""
                        WHERE m.""MachineId"" = @MachineId";

            using (var connection = _dbContext.CreateConnection())
            {   

                Machine machineEntry = null;
                await connection.QueryAsync<Machine, Faults, Machine>(query,
                    (machine, fault) =>
                    {
                        if (machineEntry == null)
                        {
                            machineEntry = machine;
                            machineEntry.Faults = new List<Faults> {};
                        }

                        if (fault != null) machineEntry.Faults.Add(fault);

                        return machineEntry;

                    },
                    param: new { MachineId = machineId },
                    splitOn: "MachineId"
                    );
                return machineEntry;
            }
        }
        public async Task<bool> DoesMachineExistAsync(string machineName)
        {
            var query = @"SELECT COUNT(*) FROM ""Machines"" WHERE ""Name"" = @Name;";

            using (var connection = _dbContext.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(query, param: new { Name = machineName });
                return count > 0;
            }
        }

        public async Task<int> CreateMachineAsync(Machine machine)
        {
            var query = @"
                    INSERT INTO ""Machines"" (""Name"")
                    VALUES (@Name)
                    RETURNING ""MachineId"";
                ";
            using (var connection = _dbContext.CreateConnection())
            {
                var machineId = await connection.ExecuteScalarAsync<int>(query, param: new { Name = machine.Name });
                return machineId;
            }
        }

        public async Task UpdateMachineAsync(int machineId, Machine machine)
        {
            var query = @"UPDATE ""Machines""
	                        SET ""Name""=@Name
	                        WHERE ""MachineId"" = @MachineId";
            var parameters = new DynamicParameters();
            parameters.Add("MachineId", machineId, DbType.Int32);
            parameters.Add("Name", machine.Name, DbType.String);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteMachineAsync(int machineId)
        {
            var query = @"DELETE FROM ""Machines""
	                        WHERE ""MachineId""=@MachineId";
            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, param: new {machineId});

            }
        }
    }
}
