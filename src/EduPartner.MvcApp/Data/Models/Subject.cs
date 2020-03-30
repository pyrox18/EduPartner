using System;
using System.Collections.Generic;

namespace EduPartner.MvcApp.Data.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MonthlyFee { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}
