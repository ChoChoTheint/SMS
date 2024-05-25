using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string AspNetUsersId { get; set; }


    }
}
