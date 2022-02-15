using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication6.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TimeSystemUser class
public class TimeSystemUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public string? FirstName { get; set; }

    [PersonalData] 
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }

    [PersonalData]
    public int IdRole { get; set; }

 

    
}

