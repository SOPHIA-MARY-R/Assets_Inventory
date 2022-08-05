using Fluid.Shared.Entities;

namespace Fluid.Core.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MachineInfo> MachineMaster { get; set; }
    public DbSet<MotherboardInfo> MotherboardMaster { get; set; }
    public DbSet<PhysicalMemoryInfo> PhysicalMemoryMaster { get; set; }
    public DbSet<HardDiskInfo> HardDiskMaster { get; set; }
    public DbSet<KeyboardInfo> KeyboardMaster { get; set; }
    public DbSet<MouseInfo> MouseMaster { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
        base.OnModelCreating(modelBuilder);
    }
}
