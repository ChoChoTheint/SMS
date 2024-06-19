namespace StudentManagementSystem.Models.ViewModels
{
    public class CompositeViewModel
    {
        public IList<AdminViewModel> Admins { get; set; }
        public IList<TeacherViewModel> Teachers { get; set; }
        public IList<StudentViewModel> Students { get; set; }
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int CourseCount { get; set; }
        public int BatchCount { get; set; }
        public int AttendanceCount { get; set; }
        public int AssignmentCount { get; set; }
        public int BookCount { get; set; }
        public int ExamCount { get; set; }
        public int ExamResultCount { get; set; }
    }

}
