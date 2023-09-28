using Application.Abstractionn;
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

        public async Task<IEnumerable<FaultDto>> GetAllFaultsAsync(int page, int pageSize)
        {
            int offset = (Math.Max(page, 1) - 1) * pageSize;
            int limit = pageSize < 1 ? 1 : pageSize;

            var faults = await _faultsRepository.GetAllFaultsAsync(offset, limit);

            var faultsDto = _mapper.Map<IEnumerable<FaultDto>>(faults);
            return faultsDto;
            
        }

    }
}
