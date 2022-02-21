using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Areas.Identity.Entity;

namespace TimeTracker.Areas.Identity.Data.Configs
{
    public class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                 .HasMany(y => y.timetrackerEntities)
                 .WithOne(y => y.AspNetUser)
                 .HasForeignKey(y => y.IdUser)
                 .HasPrincipalKey(y => y.Id);
        }
    }
}
