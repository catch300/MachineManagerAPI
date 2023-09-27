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
                            Machine machineEntry = new Machine();
                            if (!machineDictionary.TryGetValue(machine.MachineId, out machineEntry))
                            {
                                machineEntry = machine;
                                machineEntry.Faults = new List<Faults>();
                                machineDictionary.Add(machineEntry.MachineId, machineEntry);
                            }
                            machineEntry.Faults.Add(fault);
                            return machineEntry;
                        },
                        splitOn: "MachineId");
                    return machineDictionary.Values;
                }
            }

        public async Task<Machine> GetMachinesById(int machineId)
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
                var machineDictionary = new Dictionary<int, Machine>();

                var machine = await connection.QueryAsync<Machine, Faults, Machine>(query,
                    (machine, fault) =>
                    {
                        Machine machineEntry = new Machine();
                        if (!machineDictionary.TryGetValue(machine.MachineId, out machineEntry))
                        {
                            machineEntry = machine;
                            machineEntry.Faults = new List<Faults>();
                            machineDictionary.Add(machineEntry.MachineId, machineEntry);
                        }

                        machineEntry.Faults.Add(fault);
                        return machineEntry;
                    },
                    param: new { MachineId = machineId },
                    splitOn: "MachineId"
                    ); ;

                return machine.FirstOrDefault();
            }

        }
    }
}
