using GigaConsulting.Domain.Events;
using MediatR;

namespace GigaConsulting.Domain.EventHandlers
{
    public class AllocationEventHandler :
        INotificationHandler<AllocationRegisteredEvent>
    {
        public Task Handle(AllocationRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
