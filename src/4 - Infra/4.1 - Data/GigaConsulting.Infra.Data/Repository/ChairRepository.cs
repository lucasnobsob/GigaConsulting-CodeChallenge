using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GigaConsulting.Infra.Data.Repository
{
    public class ChairRepository : Repository<Chair>, IChairRepository
    {
        public ChairRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
