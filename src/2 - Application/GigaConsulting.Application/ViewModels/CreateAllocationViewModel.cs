namespace GigaConsulting.Application.ViewModels
{
    public class CreateAllocationViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RoomId { get; set; }
        public Guid ChairId { get; set; }
    }
}
