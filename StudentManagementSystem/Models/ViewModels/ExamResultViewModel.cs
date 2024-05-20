using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ExamResultViewModel
    {
        public string Id { get; set; }
        public int Mark { get; set; }
        //[ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
        public string StudentInfo { get; set; }
    }
}
