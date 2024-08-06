using System.Collections.Generic;

namespace ELearningApp.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int PassingThreshold { get; set; }
    }
}