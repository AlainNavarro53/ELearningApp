using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Models
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }

        [Required(ErrorMessage = "The Content is required")]
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; }
    }
}
