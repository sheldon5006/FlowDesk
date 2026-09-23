using FlowDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Data
{
    public class FlowDeskDbContext : DbContext
    {
        public FlowDeskDbContext(DbContextOptions<FlowDeskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
    }
}
