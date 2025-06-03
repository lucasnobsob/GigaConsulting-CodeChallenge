using GigaConsulting.Domain.Models;

namespace GigaConsulting.Application.ViewModels
{
    public class AllocationViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public RoomViewModel Room { get; set; } = new RoomViewModel();
        public ChairViewModel Chair { get; set; } = new ChairViewModel();
    }
}
