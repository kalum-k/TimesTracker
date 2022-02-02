using System.ComponentModel.DataAnnotations;
using WebApplication6.Areas.Identity.Data;

namespace TimeTacker.Areas.Identity.Data
{
    public class TimeStatus
    {
        public int Id { get; set; }
        public string? NameStatus { get; set; }

        //public ICollection<TimeTackers> timetackers { get; set; }
    }
}
