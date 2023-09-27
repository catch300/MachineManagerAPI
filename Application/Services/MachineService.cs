using Application.Abstractionn;
using Contracts;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class MachineService : IMachineService
    {

        private readonly IMachineRepository _machineRepository;
        public MachineService(IMachineRepository machineRepository) => _machineRepository = machineRepository;

        public async Task<IEnumerable<MachineDetail>> GetMachines()
        {
            var machines = await _machineRepository.GetAllMachinesAsync();

            var machineDetails = machines.Select(machine => new MachineDetail
            {
                MachineId = machine.MachineId,
                MachineName = machine.Name,
                FaultNames = machine.Faults != null ? machine.Faults.Select(x => x.Name).ToList() : new List<string>(),
                AverageFaultDuration = machine.Faults != null && machine.Faults.Any()
                                            ? machine.Faults.Average(f => (f.EndTime - f.StartTime).TotalMinutes) : 0
            });

            return machineDetails;
        }

        public async Task<MachineDetail> GetMachinesById(int id)
        {
            var machine = await _machineRepository.GetMachinesById(id);

            if (machine == null)
                return null;

            var detail = new MachineDetail 
            {
                MachineId = machine.MachineId,
                MachineName = machine.Name,
                FaultNames = machine.Faults != null ? machine.Faults.Select(x => x.Name).ToList() : new List<string>(),
                AverageFaultDuration = machine.Faults != null && machine.Faults.Any()
                                            ? machine.Faults.Average(f => (f.EndTime - f.StartTime).TotalMinutes) : 0
            };
            
            return detail;
        }

    }
}
