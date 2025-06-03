using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Domain.Events;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using MediatR;

namespace GigaConsulting.Domain.CommandHandlers
{
    public class AllocationCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewAllocationCommand, bool>
    {
        private readonly IAllocationRepository _allocationRepository;
        private readonly IChairRepository _chairRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMediatorHandler Bus;

        public AllocationCommandHandler(
            INotificationHandler<DomainNotification> notifications,
            IAllocationRepository allocationRepository,
            IChairRepository chairRepository,
            IRoomRepository roomRepository,
            IMediatorHandler bus,
            IUnitOfWork uow) : base(uow, bus, notifications)
        {
            _allocationRepository = allocationRepository;
            _chairRepository = chairRepository;
            _roomRepository = roomRepository;
            Bus = bus;
        }

        public async Task<bool> Handle(RegisterNewAllocationCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var allocations = await _allocationRepository.GetAll();
            var room = await _roomRepository.GetById(message.RoomId);
            var chair = await _chairRepository.GetById(message.ChairId);

            if (allocations.Any(x => x.From >= message.From && x.To <= message.To && x.Room.Id == message.RoomId))
            {
                await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Houve conflito de horário para esta sala."));
                return await Task.FromResult(false);
            }

            if (allocations.Any(x => x.Chair.Id == chair.Id && x.From >= message.From && x.To <= message.To))
            {
                await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Esta cadeira estará alocada nesse horário."));
                return await Task.FromResult(false);
            }

            var allocation = new Allocation
            {
                From = message.From,
                To = message.To,
                Room = room,
                Chair = chair
            };
            await _allocationRepository.Add(allocation);

            if (Commit())
            {
                await Bus.RaiseEvent(new AllocationRegisteredEvent(message.From, message.To, message.RoomId, message.ChairId));
            }
            return await Task.FromResult(true);
        }
    }
}
