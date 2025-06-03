using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Domain.Events;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Domain.Models.Enums;
using MediatR;

namespace GigaConsulting.Domain.CommandHandlers
{
    public class ChairCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewChairCommand, bool>,
        IRequestHandler<UpdateChairCommand, bool>,
        IRequestHandler<RemoveChairCommand, bool>
    {
        private readonly IAllocationRepository _allocationRepository;
        private readonly IChairRepository _chairRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMediatorHandler Bus;

        public ChairCommandHandler(
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

        public async Task<bool> Handle(RegisterNewChairCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var chair = new Chair 
            { 
                Description = message.Description, 
                SerialNumber = message.SerialNumber, 
                Model = message.Model, 
                Type = message.ChairType,
                Status = Status.Disponivel 
            };
            await _chairRepository.Add(chair);

            if (Commit())
            {
                await Bus.RaiseEvent(new ChairRegisteredEvent(
                    message.SerialNumber, 
                    message.Description, 
                    message.Model, 
                    message.Status, 
                    message.ChairType));
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(UpdateChairCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var chair = await _chairRepository.GetById(message.Id);
            if (chair is null)
            {
                await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Cadeira não encontrada."));
                return await Task.FromResult(false);
            }

            _chairRepository.Update(chair);

            if (Commit())
            {
                await Bus.RaiseEvent(new ChairUpdatedEvent(message.Id, message.Description, message.Status));
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(RemoveChairCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var chair = await _chairRepository.GetById(message.Id);
            if (chair is null)
            {
                await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Cadeira não encontrada."));
                return await Task.FromResult(false);
            }

            if (chair.Status == Status.Ocupada || chair.Status == Status.Manutencao)
            {
                await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Cadeira Allocada ou em Manutenção."));
                return await Task.FromResult(false);
            }

            await _chairRepository.Remove(chair.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ChairRemovedEvent(message.Id));
            }
            return await Task.FromResult(true);
        }
    }
}
