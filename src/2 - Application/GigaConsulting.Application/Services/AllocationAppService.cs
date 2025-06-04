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
    public class AllocationAppService : IAllocationAppService
    {
        private readonly IEventStoreRepository _eventStoreSqlRepository;
        private readonly IAllocationRepository _allocationRepository;
        private readonly IMediatorHandler Bus;
        private readonly IMapper _mapper;

        public AllocationAppService(
            IEventStoreRepository eventStoreSqlRepository,
            IAllocationRepository allocationRepository,
            IMediatorHandler bus,
            IMapper mapper)
        {
            _eventStoreSqlRepository = eventStoreSqlRepository;
            _allocationRepository = allocationRepository;
            _mapper = mapper;
            Bus = bus;
        }

        public async Task<IEnumerable<AllocationViewModel>> GetAll()
        {
            var chairs = await _allocationRepository.GetAllocationsAsync();
            return _mapper.Map<List<AllocationViewModel>>(chairs);
        }

        public async Task<IList<AllocationHistoryData>> GetAllHistory(Guid id)
        {
            var storedEvents = await _eventStoreSqlRepository.All(id);
            return AllocationHistory.ToJavaScriptCustomerHistory(storedEvents);
        }

        public async Task Register(CreateAllocationViewModel chairViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewAllocationCommand>(chairViewModel);
            await Bus.SendCommand(registerCommand);
        }
    }
}
