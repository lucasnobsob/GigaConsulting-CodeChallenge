using GigaConsulting.Domain.Core.Commands;
using GigaConsulting.Domain.Models;

namespace GigaConsulting.Domain.Commands
{
    public abstract class AllocationCommand : Command
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RoomId { get; set; }
        public Guid ChairId { get; set; }
    }
}
