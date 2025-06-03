using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Application.ViewModels
{
    public class UpdateChairViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}
