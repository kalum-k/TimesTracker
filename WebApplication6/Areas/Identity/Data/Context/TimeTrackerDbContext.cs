using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTacker.Areas.Identity.Data;
using WebApplication6.Areas.Identity.Data;
using TimeTracker.Areas.Identity.Models;
using TimeTracker.Areas.Identity.Entity;

namespace WebApplication6.Data;

public class TimeTrackerDbContext : IdentityDbContext<TimeSystemUser
    //,IdentityUser<int>,IdentityRole<int>
    >
{
    public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<TimeSystemUser> user { get; set; }
    public DbSet<TimeTrackers> timeTackers { get; set; }
    public DbSet<TimeStatus> TimeStatuses { get; set; }
    public DbSet<RoleModel> RoleModel { get; set; }
    public DbSet<TimetrackerEntity> entities { get; set; }
}
