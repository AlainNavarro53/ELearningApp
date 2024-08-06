using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Models
{
    public class LessonViewModel
    {
        public int LessonId { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }
        public int CourseId { get; set; }
        public string Coursedescription { get; set; }

        [Required(ErrorMessage = "The PassingThreshold is required")]
        public int PassingThreshold { get; set; }
    }
}
