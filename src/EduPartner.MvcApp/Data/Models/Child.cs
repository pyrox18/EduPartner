using System;

namespace EduPartner.MvcApp.Data.Models
{
    public class Child
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Parent Parent { get; set; }
    }
}
