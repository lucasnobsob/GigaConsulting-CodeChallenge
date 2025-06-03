using GigaConsulting.Application.ViewModels;

namespace GigaConsulting.Application.Interfaces
{
    public interface IRoomAppService
    {
        Task<IEnumerable<RoomViewModel>> GetAll();
    }
}
