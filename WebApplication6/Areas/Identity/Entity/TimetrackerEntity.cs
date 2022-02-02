using System.ComponentModel.DataAnnotations;
using WebApplication6.Areas.Identity.Data;

namespace TimeTracker.Areas.Identity.Entity
{
    public class TimetrackerEntity
    {

        public int Id { get; set; }

        public string IdUser { get; set; }
        public TimeSystemUser user { get; set; }
    }
}
