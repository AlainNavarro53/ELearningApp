using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}