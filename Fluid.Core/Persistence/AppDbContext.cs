using Fluid.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Fluid.Core.Persistence;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MachineInfo> MachineMaster { get; set; }
    public DbSet<MotherboardInfo> MotherboardMaster { get; set; }
    public DbSet<PhysicalMemoryInfo> PhysicalMemoryMaster { get; set; }
    public DbSet<HardDiskInfo> HardDiskMaster { get; set; }
    public DbSet<KeyboardInfo> KeyboardMaster { get; set; }
    public DbSet<MouseInfo> MouseMaster { get; set; }
<<<<<<< HEAD
    public DbSet<ProcessorInfo> ProcessorMaster { get; set; }
=======
    public DbSet<FeedLog> FeedLogStorage { get; set; }
>>>>>>> 487fc68eb34cfff18050d466a9bb4a3a9f56b71b

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Entity<AppUser>().ToTable("Technicians", "dbo");
    }
}
