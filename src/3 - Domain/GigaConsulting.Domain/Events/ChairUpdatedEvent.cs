using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Domain.Events
{
    public class ChairUpdatedEvent : Event
    {
        public ChairUpdatedEvent(Guid id, string description, Status status)
        {
            Id = id;
            Description = description;
            Status = status;
        }

        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}
