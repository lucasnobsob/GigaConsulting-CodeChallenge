using GigaConsulting.Domain.Models;

namespace GigaConsulting.Domain.Interfaces
{
    public interface IAllocationRepository : IRepository<Allocation>
    {
        Task<IEnumerable<Allocation>> GetAllocationsAsync();
    }
}
