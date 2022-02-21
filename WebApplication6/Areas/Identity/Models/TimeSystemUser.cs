
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication6.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TimeSystemUser class
public class TimeSystemUser : IdentityUser
{
    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
}

