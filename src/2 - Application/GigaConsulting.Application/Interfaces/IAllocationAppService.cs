using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.ViewModels;

namespace GigaConsulting.Application.Interfaces
{
    public interface IAllocationAppService
    {
        Task<IEnumerable<AllocationViewModel>> GetAll();
        Task<IList<AllocationHistoryData>> GetAllHistory(Guid id);
        Task Register(CreateAllocationViewModel allocationViewModel);
    }
}
