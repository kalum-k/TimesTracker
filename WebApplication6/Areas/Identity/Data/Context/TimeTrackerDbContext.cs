
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTacker.Areas.Identity.Data;
using WebApplication6.Areas.Identity.Data;
using TimeTracker.Areas.Identity.Models;
using TimeTracker.Areas.Identity.Data.Configs;
using TimeTracker.Areas.Identity.Data.Entity;
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
        builder.ApplyConfiguration(new TimeTrackerConfigs());
        builder.ApplyConfiguration(new UserConfigs());
        base.OnModelCreating(builder);
    }
    //public DbSet<User> user{ get; set; }
    //public DbSet<TimetrackerEntity> timeTackers { get; set; }
    public DbSet<TimeTrackers> timeTackers { get; set; }
    public DbSet<TimeSystemUser> user { get; set; }
    public DbSet<TimeStatus> TimeStatuses { get; set; }
    public DbSet<RoleModel> RoleModel { get; set; }

}
