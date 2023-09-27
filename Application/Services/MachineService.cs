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
        public async Task<IEnumerable<MachineDetailDto>> GetMachinesAsnyc()
        {
            
            var machines = await _machineRepository.GetAllMachinesAsync();

            var machineDetails = _mapper.Map<IEnumerable<MachineDetailDto>>(machines);

            return machineDetails;
        }

        public async Task<MachineDetailDto> GetMachineByIdAsync(int id)
        {
            var machine = await _machineRepository.GetMachineByIdAsync(id);

            if (machine == null)
                return null;

            var machineDetail= _mapper.Map<MachineDetailDto>(machine);  
            
            return machineDetail;
        }

        public async Task<int> CreateMachineAsync(MachineForCreationDto machineDto)
        {
            var machine = _mapper.Map<Machine>(machineDto);

            var doesExist = await _machineRepository.DoesMachineExistAsync(machineDto.Name);
            if (doesExist)
            {
                throw new InvalidOperationException($"Machine already exist with the name: {machineDto.Name}.");
            }
            var machineId = await _machineRepository.CreateMachineAsync(machine);
            
            return machineId;
        }

        public async Task UpdateMachineAsync(int machineId, MachineForUpdateDto machineForUpdate)
        {
            var dbMachine = await _machineRepository.GetMachineByIdAsync(machineId);
            if (dbMachine == null)
                throw new InvalidOperationException($"Machine with the id: {machineId} does not exist!");

            var machine = _mapper.Map<Machine>(machineForUpdate);

            await _machineRepository.UpdateMachineAsync(machineId, machine);
        }

        public async Task DeleteMachineAsync(int machineId)
        {
            var dbMachine = await _machineRepository.GetMachineByIdAsync(machineId);
            if (dbMachine == null)
                throw new InvalidOperationException($"Machine with the id: {machineId} does not exist!");

            await _machineRepository.DeleteMachineAsync(machineId);
        }
    }
}
