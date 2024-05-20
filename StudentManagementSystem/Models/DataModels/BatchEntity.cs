﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModels
{
    [Table("Batch")]
    public class BatchEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(CourseId))]
        public string CourseId { get; set; }
    }
}
