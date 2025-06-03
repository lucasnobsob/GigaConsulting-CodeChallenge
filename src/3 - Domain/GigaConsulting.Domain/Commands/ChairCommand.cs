using GigaConsulting.Domain.Core.Commands;
using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Domain.Commands
{
    public abstract class ChairCommand : Command
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Status Status { get; set; }
        public ChairType ChairType { get; set; }
    }
}
