using GigaConsulting.Domain.Core.Models;

namespace GigaConsulting.Domain.Models
{
    public class Allocation : Entity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Room Room { get; set; } = new Room();
        public Chair Chair { get; set; } = new Chair();
    }
}
