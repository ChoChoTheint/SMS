namespace StudentManagementSystem.Models.ViewModels
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OpeningDate { get; set; }
        public string DurationInHour { get; set; }
        public string DurationInMonth { get; set; }
    }
}
