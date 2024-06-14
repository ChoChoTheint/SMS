using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is requied")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is requied")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is requied")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is requied")]
        public string Address { get; set; }
        [Required(ErrorMessage = "NRC is requied")]
        public string NRC { get; set; }
        [Required(ErrorMessage = "Date of birth is requied")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Father name is requied")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Gender is requied")]
        public string Gender { get; set; }
        public string AspNetUsersId { get; set; }
        

    }
}
