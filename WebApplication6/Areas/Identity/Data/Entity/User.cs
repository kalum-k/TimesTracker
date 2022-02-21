using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TimeTracker.Areas.Identity.Data.Entity;

namespace TimeTracker.Areas.Identity.Entity
{
    public class User : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public ICollection<TimetrackerEntity> timetrackerEntities { get; set; }
    }
}
