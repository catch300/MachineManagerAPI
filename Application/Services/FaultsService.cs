using Application.Abstractionn;
using Contracts;
using Domain.Models;
using Domain.Repositories;


namespace Application.Services
{
    public class FaultsService : IFaultsService
    {
        private readonly IFaultsRepository _faultsRepository;

        public FaultsService(IFaultsRepository faultsRepository) => _faultsRepository = faultsRepository;

        public async Task<IEnumerable<MachineDetailDto>> GetFaults()
        {
            //var faults =  await _faultsRepository.GetFaults();

            //var faultsDto = MapToFaultsDTOs(faults);


            //return faultsDto;
            return null;
        }

        
        //private static IEnumerable<FaultsDto> MapToFaultsDTOs(IEnumerable<Faults> malfunctions)
        //{
        //    var malfunctionDTOs = new List<FaultsDto>();

        //    foreach (var malfunction in malfunctions)
        //    {
        //        var malfunctionDTO = new FaultsDto
        //            {
        //                FaultId = malfunction.FaultId,
        //                Name = malfunction.Name,
        //                Priority = malfunction.Priority,
        //                StartTime = malfunction.StartTime,
        //                EndTime = malfunction.EndTime,
        //                IsResloved = malfunction.IsResloved,
        //                Description = malfunction.Description,
        //                MachineId = malfunction.MachineId,
        //            };

        //        malfunctionDTOs.Add(malfunctionDTO);
        //    }

        //    return malfunctionDTOs;
        //}
    }
}
