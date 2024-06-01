using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AdminViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
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
        [Required(ErrorMessage = "FatherName is required")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        public string AspNetUsersId { get; set; }

    }
}
