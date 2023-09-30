using Application.Abstraction;
using AutoMapper;
using Contracts.Faults;
using Domain.Models;
using Domain.Repositories;


namespace Application.Services
{
    public class FaultsService : IFaultsService
    {
        private readonly IFaultsRepository _faultsRepository;
        private readonly IMapper _mapper;
        public FaultsService(IFaultsRepository faultsRepository, IMapper mapper)
        {
            _faultsRepository   = faultsRepository; 
            _mapper             = mapper;
        }

        public async Task<IPaginatedList<IEnumerable<FaultDto>>> GetAllFaultsAsync(int currentPageNumber, int pageSize)
        {
            currentPageNumber = Math.Max(1, currentPageNumber);

            var totalNumOfFaults = await _faultsRepository.CountAllFaults();

            int maxPageSize = 10;
            pageSize = pageSize < maxPageSize ? pageSize : maxPageSize;
            //pageSize = pageSize < 1 ? totalNumOfFaults : pageSize;

            int offset = (currentPageNumber - 1) * pageSize;

            var faults = await _faultsRepository.GetAllFaultsAsync(offset, pageSize);
            var faultsDto = _mapper.Map<IEnumerable<FaultDto>>(faults);

            IPaginatedList<IEnumerable<FaultDto>> result = new PaginatedList<IEnumerable<FaultDto>>(totalNumOfFaults, faultsDto, currentPageNumber, pageSize, maxPageSize);

            return result;   
        }
        public async Task<FaultDto> GetFaultByIdAsync(int id)
        {
            var fault = await _faultsRepository.GetFaultByIdAsync(id);
            if (fault == null) return null;

            var faultDto = _mapper.Map<FaultDto>(fault);

            return faultDto;
        }
        public async Task<int> CreateFaultAsync(FaultForCreationDto faultForCreation)
        {

            var hasActiveFault = await _faultsRepository.HasActiveFaultAsync(faultForCreation.MachineId);
            if (hasActiveFault)
            {
                throw new InvalidOperationException("An active fault already exists on this machine.");
            }

            var fault = _mapper.Map<Faults>(faultForCreation);

            var faultId = await _faultsRepository.CreateFaultAsync(fault);

            return faultId;
        }

        public async Task UpdateFaultAsync(int faultId, FaultForUpdatingDto faultForUpdatingDto)
        {
            var dbFault = await _faultsRepository.GetFaultByIdAsync(faultId);
            if (dbFault == null)
                throw new InvalidOperationException($"Fault with the id: {faultId} does not exist!");

            var fault = _mapper.Map<Faults>(faultForUpdatingDto);

            await _faultsRepository.UpdateFaultAsync(faultId, fault);
        }

        public async Task UpdateFaultStatusAsync(int faultId, FaultForUpdatingStatusDto faultForUpdatingStatus)
        {
            var dbFault = await _faultsRepository.GetFaultByIdAsync(faultId);
            if (dbFault == null)
                throw new InvalidOperationException($"Fault with the id: {faultId} does not exist!");

            var fault = _mapper.Map<Faults>(faultForUpdatingStatus);

            await _faultsRepository.UpdateFaultStatusAsync(faultId, fault);
        }

        public async Task DeleteFaultAsync(int faultId)
        {
            var fault = await _faultsRepository.GetFaultByIdAsync(faultId);
            if (fault == null)
                throw new InvalidOperationException($"Fault with the id: {faultId} does not exist!");

            await _faultsRepository.DeleteFaultAsync(faultId);
        }
    }
}
