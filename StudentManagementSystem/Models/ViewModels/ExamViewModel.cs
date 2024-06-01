﻿using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ExamViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "exam date is required")]
        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }
    }
}
