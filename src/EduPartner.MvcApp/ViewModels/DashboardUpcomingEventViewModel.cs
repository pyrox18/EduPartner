using System;

namespace EduPartner.MvcApp.ViewModels
{
    public class DashboardUpcomingEventViewModel
    {
        public string ChildName { get; set; }
        public string SubjectName { get; set; }
        public DateTimeOffset StartTimestamp { get; set; }
        public bool IsHomeTutoring { get; set; }
    }
}
