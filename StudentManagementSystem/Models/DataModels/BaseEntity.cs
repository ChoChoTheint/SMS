using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.DataModels
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsInActive { get; set; }
    }
}
