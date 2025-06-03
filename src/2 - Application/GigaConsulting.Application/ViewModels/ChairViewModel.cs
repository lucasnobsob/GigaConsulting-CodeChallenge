using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Application.ViewModels
{
    public class ChairViewModel
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Status Status { get; set; }
        public ChairType Type { get; set; }
    }
}
