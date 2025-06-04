using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GigaConsulting.Infra.Data.Repository
{
    public class AllocationRepository : Repository<Allocation>, IAllocationRepository
    {
        public AllocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Allocation>> GetAllocationsAsync()
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.Chair)
                .Include(x => x.Room)
                .ToListAsync();
        }
    }
}
