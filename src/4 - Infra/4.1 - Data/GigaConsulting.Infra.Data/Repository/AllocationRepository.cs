using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Context;

namespace GigaConsulting.Infra.Data.Repository
{
    public class AllocationRepository : Repository<Allocation>, IAllocationRepository
    {
        public AllocationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
