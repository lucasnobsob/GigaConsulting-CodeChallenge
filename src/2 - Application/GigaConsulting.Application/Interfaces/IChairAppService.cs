using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.ViewModels;

namespace GigaConsulting.Application.Interfaces
{
    public interface IChairAppService
    {
        Task<IEnumerable<ChairViewModel>> GetAll();
        Task<IList<ChairHistoryData>> GetAllHistory(Guid id);
        Task<ChairViewModel> GetById(Guid id);
        Task Register(CreateChairViewModel ChairViewModel);
        Task Remove(Guid id);
        Task Update(UpdateChairViewModel chairViewModel);
    }
}
