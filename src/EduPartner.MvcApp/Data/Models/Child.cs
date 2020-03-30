using System;
using System.Collections.Generic;

namespace EduPartner.MvcApp.Data.Models
{
    public class Child
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Parent Parent { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
