// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using TimeTacker.Areas.Identity.Data;

namespace WebApplication6.Areas.Identity.Data
{
    public class TimeTrackers
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
      
        public TimeTrackers()
        {
            //var dateTime = DateTime;
            CurrentDate = DateTime.Today;
            TimeIn = DateTime.Now;
            TimeOut = DateTime.Now;
            Sum = DateTime.Now.Subtract(TimeIn);
        }
        public int IdTimeStatus { get; set; }
        public TimeSpan Sum { get; set; }
        public string IdUser { get; set; }
    }
}
