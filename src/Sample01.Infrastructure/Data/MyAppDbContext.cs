using Microsoft.EntityFrameworkCore;
using Sample01.Domain.Entities;

namespace Sample01.Infrastructure.Data
{
    public class Sample01DbContext : DbContext
    {
        public Sample01DbContext(DbContextOptions<Sample01DbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
