using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class BatchViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        public string CourseInfo { get; set; }
    }
}
