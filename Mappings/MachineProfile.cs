using AutoMapper;
using TelemetriaMonitoramento.API.Models;
using TelemetriaMonitoramento.API.Dtos;

namespace TelemetriaMonitoramento.API.Mappings
{
    public class MachineProfile : Profile
    {
        public MachineProfile()
        {
            CreateMap<Machine, MachineDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<CreateMachineDto, Machine>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<UpdateMachineDto, Machine>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
