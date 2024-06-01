﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModels
{
    public class TeacherCourseViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="teacher is required")]
        public string TeacherId { get; set; }
        [Required(ErrorMessage = "course is required")]
        public string CourseId { get; set; }

    }
}
