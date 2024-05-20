using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        //[ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }
        //[ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        public string BatchInfo { get; set; }
    }
}
