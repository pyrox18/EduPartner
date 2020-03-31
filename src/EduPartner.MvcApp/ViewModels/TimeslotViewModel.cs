using System;

namespace EduPartner.MvcApp.ViewModels
{
    public class TimeslotViewModel
    {
        public Guid TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Time { get; set; }
    }
}
