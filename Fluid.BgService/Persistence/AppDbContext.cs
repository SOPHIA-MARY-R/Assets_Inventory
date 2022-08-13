using Fluid.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Fluid.BgService.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<MachineMasterModel> MachineMasters { get; set; }
}
