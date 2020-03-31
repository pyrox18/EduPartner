using System;

namespace EduPartner.MvcApp.ViewModels
{
    public class EnrollmentTimeslotSelectionViewModel
    {
        public Guid ChildId { get; set; }
        public Guid SubjectId { get; set; }
        public string TimeslotSelection { get; set; }
        public bool IsHomeTutoring { get; set; }
    }
}
