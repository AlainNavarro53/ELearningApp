using System.Collections.Generic;

namespace ELearningApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public QuestionType Type { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int Score { get; set; }
    }

    public enum QuestionType
    {
        Boolean,
        SingleChoice,
        MultipleChoice,
        RequiredMultipleChoice
    }
}
