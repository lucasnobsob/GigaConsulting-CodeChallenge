using AutoMapper;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateAllocationViewModel, RegisterNewAllocationCommand>()
                .ConstructUsing(c => new RegisterNewAllocationCommand(c.From, c.To, c.RoomId, c.ChairId));

            CreateMap<CreateChairViewModel, RegisterNewChairCommand>()
                .ConstructUsing(c => new RegisterNewChairCommand(c.Id, c.SerialNumber, c.Description, c.Model, c.Status, c.ChairType));

            CreateMap<UpdateChairViewModel, UpdateChairCommand>()
                .ConstructUsing(c => new UpdateChairCommand(c.Id, c.Description, c.Status));

            CreateMap<RemoveChairViewModel, RemoveChairCommand>()
                .ConstructUsing(c => new RemoveChairCommand(c.Id));
        }
    }
}
