using GigaConsulting.Domain.Events;
using MediatR;

namespace GigaConsulting.Domain.EventHandlers
{
    public class ChairEventHandler :
        INotificationHandler<ChairRegisteredEvent>,
        INotificationHandler<ChairRemovedEvent>
    {
        public Task Handle(ChairRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ChairRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
