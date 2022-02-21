using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TimeTracker.Areas.Identity.Entity;

namespace TimeTracker.Areas.Identity.Data.Entity
{
    public class TimetrackerEntity
    {

        public int Id { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CurrentDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:HH/mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }

        //[DisplayFormat(DataFormatString = "{0:HH/mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime TimeOut { get; set; }

        public int IdTimeStatus { get; set; }
        public TimeSpan Sum { get; set; }
        public string IdUser { get; set; }

        public User AspNetUser { get; set; }
    }
}
