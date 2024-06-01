using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is requied")]
        public string Name { get; set; }
        [Required(ErrorMessage = "email is requied")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "phone is requied")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "address is requied")]
        public string Address { get; set; }
        [Required(ErrorMessage = "nrc is requied")]
        public string NRC { get; set; }
        [Required(ErrorMessage = "date of birth is requied")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "father name is requied")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "gender is requied")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "batch is requied")]
        public string BatchId { get; set; }
        public string AspNetUsersId { get; set; }

    }
}
