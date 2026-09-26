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
        public DbSet<EmployeeAvailability> EmployeeAvailabilities => Set<EmployeeAvailability>();
        public DbSet<Shift> Shifts => Set<Shift>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                .HasIndex(x => new
                {
                    x.EmployeeId,
                    x.ShiftId
                })
                .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(x => x.Email)
               .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(x => x.Employee)
                .WithOne()
                .HasForeignKey<User>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EmployeeAvailability>()
                .HasIndex(a => new
                {
                    a.EmployeeId,
                    a.DayOfWeek
                })
                .IsUnique();


        }
    }


}
