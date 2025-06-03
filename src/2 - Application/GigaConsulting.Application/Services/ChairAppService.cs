using AutoMapper;
using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Infra.Data.Repository.EventSourcing;

namespace GigaConsulting.Application.Services
{
    public class ChairAppService : IChairAppService
    {
        private readonly IMapper _mapper;
        private readonly IChairRepository _chairRepository;
        private readonly IEventStoreRepository _eventStoreSqlRepository;
        private readonly IMediatorHandler Bus;

        public ChairAppService(IMapper mapper, 
            IChairRepository transactionRepository, 
            IEventStoreRepository eventStoreSqlRepository, 
            IMediatorHandler bus)
        {
            _mapper = mapper;
            _chairRepository = transactionRepository;
            _eventStoreSqlRepository = eventStoreSqlRepository;
            Bus = bus;
        }

        public async Task<IEnumerable<ChairViewModel>> GetAll()
        {
            var chairs = await _chairRepository.GetAll();
            return _mapper.Map<List<ChairViewModel>>(chairs);
        }

        public async Task<IList<ChairHistoryData>> GetAllHistory(Guid id)
        {
            var storedEvents = await _eventStoreSqlRepository.All(id);
            return ChairHistory.ToJavaScriptCustomerHistory(storedEvents);
        }

        public async Task<ChairViewModel> GetById(Guid id)
        {
            var chair = await _chairRepository.GetById(id);
            return _mapper.Map<ChairViewModel>(chair);
        }

        public async Task Register(CreateChairViewModel chairViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewChairCommand>(chairViewModel);
            await Bus.SendCommand(registerCommand);
        }

        public async Task Update(UpdateChairViewModel chairViewModel)
        {
            var registerCommand = _mapper.Map<UpdateChairCommand>(chairViewModel);
            await Bus.SendCommand(registerCommand);
        }

        public async Task Remove(Guid id)
        {
            var removeCommand = new RemoveChairCommand(id);
            await Bus.SendCommand(removeCommand);
        }
    }
}
