using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class StudentBatchViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="student is required!")]
        public string StudentId { get; set; }
        [Required(ErrorMessage = "batch is required!")]
        public string BatchId { get; set; }
    }
}
