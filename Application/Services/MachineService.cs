using Application.Abstractionn;
using AutoMapper;
using Contracts;
using Domain.Models;
using Domain.Repositories;


namespace Application.Services
{
    public class MachineService : IMachineService
    {

        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;
        public MachineService(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MachineDetailDto>> GetMachines()
        {
            
            var machines = await _machineRepository.GetAllMachinesAsync();

            var machineDetails = _mapper.Map<IEnumerable<MachineDetailDto>>(machines);

            return machineDetails;
        }

        public async Task<MachineDetailDto> GetMachinesById(int id)
        {
            var machine = await _machineRepository.GetMachinesById(id);

            if (machine == null)
                return null;

            var machineDetail= _mapper.Map<MachineDetailDto>(machine);  
            
            return machineDetail;
        }

        public async Task<int> InsertMachineAsync(MachineForCreationDto machineDto)
        {
            var machine = _mapper.Map<Machine>(machineDto);

            var doesExist = await _machineRepository.DoesMachineExistAsync(machine.Name);
            if (doesExist)
            {
                throw new InvalidOperationException("Machine already exist with that name.");
            }

            return await _machineRepository.AddMachineAsync(machine);
        }
    }
}
