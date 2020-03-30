using System;

namespace EduPartner.MvcApp.Data.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timeslot { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
}
