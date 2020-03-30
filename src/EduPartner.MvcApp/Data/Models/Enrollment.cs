using System;

namespace EduPartner.MvcApp.Data.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public DayOfWeek TimeslotDayOfWeek { get; set; }
        public DateTime TimeslotTime { get; set; }

        public Child Child { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
}
