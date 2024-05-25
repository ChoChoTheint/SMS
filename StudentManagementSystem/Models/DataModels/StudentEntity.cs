using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Student")]
    public class StudentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        [ForeignKey(nameof(BatchId))]
        public string BatchId { get; set; }
        [ForeignKey(nameof(AspNetUsersId))]
        public string AspNetUsersId { get; set; }
    }
}
