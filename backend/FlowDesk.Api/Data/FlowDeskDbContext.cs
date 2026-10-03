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
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<WarehouseLocation> WarehouseLocations => Set<WarehouseLocation>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Inventory> Inventory => Set<Inventory>();

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

            modelBuilder.Entity<Warehouse>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<WarehouseLocation>()
                .HasIndex(x => new
                {
                    x.WarehouseId,
                    x.Code
                })
                .IsUnique();

            modelBuilder.Entity<WarehouseLocation>()
                .HasOne(x => x.Warehouse)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Sku)
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasIndex(x => new
                {
                    x.ProductId,
                    x.WarehouseLocationId
                })
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Inventory)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inventory>()
                .HasOne(x => x.WarehouseLocation)
                .WithMany(x => x.Inventory)
                .HasForeignKey(x => x.WarehouseLocationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }


}
