using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Teacher")]
    public class TeacherEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
    }
}
