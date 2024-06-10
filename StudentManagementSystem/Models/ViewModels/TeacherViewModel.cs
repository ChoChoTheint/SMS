using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "NRC is required")]
        public string NRC { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Father name is required")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        public string AspNetUsersId { get; set; }
        //public string CourseInfo { get; set; }
        [Required(ErrorMessage = "batch is required")]
        public string BatchId { get; set; }


    }
}
