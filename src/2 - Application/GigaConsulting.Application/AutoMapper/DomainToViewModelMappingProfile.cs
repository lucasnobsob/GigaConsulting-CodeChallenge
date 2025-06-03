using AutoMapper;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Models;

namespace GigaConsulting.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Chair, ChairViewModel>().ReverseMap();

            CreateMap<Allocation, AllocationViewModel>().ReverseMap();

            CreateMap<Room, RoomViewModel>().ReverseMap();
        }
    }
}
