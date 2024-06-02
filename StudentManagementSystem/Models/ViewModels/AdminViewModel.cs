using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AdminViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "nrc is required")]
        public string NRC { get; set; }
        [Required(ErrorMessage = "date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "father name is required")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "gender is required")]
        public string Gender { get; set; }
        public string AspNetUsersId { get; set; }

    }
}
