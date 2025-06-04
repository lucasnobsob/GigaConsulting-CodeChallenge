using GigaConsulting.Domain.Core.Models;

namespace GigaConsulting.Domain.Models
{
    public class Allocation : Entity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Guid RoomId { get; set; }
        public Guid ChairId { get; set; }
        public Room Room { get; set; }
        public Chair Chair { get; set; }
    }
}
