using Application.Abstraction;
using AutoMapper;
using Contracts;
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
            _faultsRepository = faultsRepository; 
            _mapper = mapper;
        }

        public async Task<IPaginatedList<IEnumerable<FaultDto>>> GetAllFaultsAsync(int currentPageNumber, int pageSize)
        {
            currentPageNumber = currentPageNumber < 1 ? 1 : currentPageNumber;

            var totalNumOfFaults = await _faultsRepository.CountAllFaults();

            int maxPageSize = 10;
            pageSize = pageSize < maxPageSize ? pageSize : maxPageSize;
            pageSize = pageSize < 1 ? totalNumOfFaults : pageSize;

            int offset = (currentPageNumber - 1) * pageSize;

            var faults = await _faultsRepository.GetAllFaultsAsync(offset, pageSize);
            var faultsDto = _mapper.Map<IEnumerable<FaultDto>>(faults);

            IPaginatedList<IEnumerable<FaultDto>> result = new PaginatedList<IEnumerable<FaultDto>>(totalNumOfFaults, faultsDto, currentPageNumber, pageSize);

            return result;
            
        }

    }
}
