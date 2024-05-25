using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Assignment")]
    public class AssignmentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
        [ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
    }
}
