using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Areas.Identity.Data.Entity;
using WebApplication6.Areas.Identity.Data;

namespace TimeTracker.Areas.Identity.Data.Configs
{
    public class TimeTrackerConfigs : IEntityTypeConfiguration<TimetrackerEntity>
    {
        public void Configure(EntityTypeBuilder<TimetrackerEntity> builder)
        {
            builder
              .HasOne(x => x.AspNetUser)
              .WithMany(x => x.timetrackerEntities)
              .HasForeignKey(x => x.IdUser)
              .HasPrincipalKey(x => x.Id);
              

        }
    }
}
