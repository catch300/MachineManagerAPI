using AutoMapper;
using Domain.Models;
using Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Faults;
using Contracts.Machines;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Machine, MachineDetailDto>()
                    .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.MachineId))
                    .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.FaultNames,
                                opt => opt.MapFrom(src => src.Faults != null
                                                    ? src.Faults.Where(f => f != null && f.Name != null).Select(f => f.Name).ToList()
                                                    : new List<string>()))
                    .ForMember(dest => dest.AverageFaultDuration,
                                opt => opt.MapFrom(src => src.Faults != null && src.Faults.Any()
                                                    ? src.Faults.Average(f => (f.EndTime - f.StartTime).TotalMinutes)
                                                    : 0));
            CreateMap <IEnumerable<MachineDetailDto>, IEnumerable<Machine >> ();
            CreateMap<MachineForCreationDto, Machine>();
            CreateMap<MachineForUpdateDto, Machine>();

            CreateMap<Faults, FaultDto>().ReverseMap();
            CreateMap<IPaginatedList<Faults>, IPaginatedList<FaultDto>>().ReverseMap();
            CreateMap<FaultForCreationDto, Faults>();
            CreateMap<FaultForUpdatingStatusDto, Faults>();

        }
    }
}
