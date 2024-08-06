using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "The Content is required")]
        public string Content { get; set; }
        public int LessonId { get; set; }
        public string LessonDescription { get; set; }
        public QuestionType Type { get; set; }

        [Required(ErrorMessage = "The score is required")]
        public int Score { get; set; }
    }
}
