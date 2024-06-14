using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Course")]
    public class CourseEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        

    }
}
