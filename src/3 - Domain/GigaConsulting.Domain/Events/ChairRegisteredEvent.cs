using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Domain.Events
{
    public class ChairRegisteredEvent : Event
    {
        public ChairRegisteredEvent(string serialNumber, string description, string model, Status status, ChairType type)
        {
            SerialNumber = serialNumber;
            Description = description;
            Model = model;
            Status = status;
            Type = type;
        }

        public string SerialNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Status Status { get; set; }
        public ChairType Type { get; set; }
    }
}
