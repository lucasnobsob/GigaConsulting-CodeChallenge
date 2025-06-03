using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Models;

namespace GigaConsulting.Domain.Events
{
    public class AllocationRegisteredEvent : Event
    {
        public AllocationRegisteredEvent(DateTime from, DateTime to, Guid roomId, Guid chairId)
        {
            From = from;
            To = to;
            RoomId = roomId;
            ChairId = chairId;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RoomId { get; set; }
        public Guid ChairId { get; set; }
    }
}
