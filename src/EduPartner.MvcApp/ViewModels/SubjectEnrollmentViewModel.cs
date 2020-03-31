using System;

namespace EduPartner.MvcApp.ViewModels
{
    public class SubjectEnrollmentViewModel
    {
        public Guid ChildId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public DayOfWeek TimeslotDayOfWeek { get; set; }
        public DateTime TimeslotTime { get; set; }
        public bool IsHomeTutoring { get; set; }
    }
}
