using GigaConsulting.Domain.Core.Models;
using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Domain.Models
{
    public class Chair : Entity
    {
        public string SerialNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Status Status { get; set; }
        public ChairType Type { get; set; }

    }
}
