using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }
    }
}
