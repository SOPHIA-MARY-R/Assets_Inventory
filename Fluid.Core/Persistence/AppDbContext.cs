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
    public DbSet<ProcessorInfo> ProcessorMaster { get; set; }
    public DbSet<GraphicsCardInfo> GraphicsCardMaster { get; set; }
    public DbSet<MonitorInfo> MonitorMaster { get; set; }
    public DbSet<CameraInfo> CameraMaster { get; set; }
    public DbSet<FeedLog> FeedLogStorage { get; set; }
    public DbSet<HardwareChangeLog> HardwareChangeLogs { get; set; }
    public DbSet<SpeakerInfo> SpeakerMaster { get; set; }


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
        modelBuilder.Entity<FeedLog>().Ignore(x => x.ShowDetails);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            if (entityType.ClrType.GetInterface(nameof(IHardwareComponentInfo)) != null)
                modelBuilder.Entity(entityType.ClrType).Ignore(nameof(IHardwareComponentInfo.HardwareChangeMode));
    }
}
