using Dapper;
using Domain.Models;
using Domain.Repositories;
using System.Text.RegularExpressions;


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

        public async Task<Machine> GetMachinesByIdAsync(int machineId)
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
                //var machineDictionary = new Dictionary<int, Machine>();

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
                //return machineEntry.FirstOrDefault();
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

        public async Task<int> AddMachineAsync(Machine machine)
        {
            var sql = @"
                    INSERT INTO ""Machines"" (""Name"")
                    VALUES (@Name)
                    RETURNING ""MachineId"";
                ";
            using (var connection = _dbContext.CreateConnection())
            {
                var machineId = await connection.ExecuteScalarAsync<int>(sql, param: new { Name = machine.Name });
                return machineId;
            }
        }
    }
}
