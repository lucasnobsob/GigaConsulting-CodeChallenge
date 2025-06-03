using AutoMapper;
using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Interfaces;

namespace GigaConsulting.Application.Services
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomAppService(IMapper mapper, IRoomRepository roomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<RoomViewModel>> GetAll()
        {
            var rooms = await _roomRepository.GetAll();
            return _mapper.Map<List<RoomViewModel>>(rooms);
        }
    }
}
