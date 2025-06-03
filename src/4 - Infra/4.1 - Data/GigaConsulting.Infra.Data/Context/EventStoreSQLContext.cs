using GigaConsulting.Infra.Data.DBSpecifications;
using GigaConsulting.Infra.Data.Mappings;
using GigaConsulting.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;

namespace GigaConsulting.Infra.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options)
        {
        }

        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MySqlDBSpecification.ConfigureProperties(modelBuilder);

            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
