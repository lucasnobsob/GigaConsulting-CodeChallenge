using GigaConsulting.Domain.Core.Events;

namespace GigaConsulting.Domain.Events
{
    public class ChairRemovedEvent : Event
    {
        public ChairRemovedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
