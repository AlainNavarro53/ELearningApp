
using Microsoft.EntityFrameworkCore;
using ELearningApp.Models;

namespace ELearningApp.Data
{
    public class ELearningContext : DbContext
    {
        public ELearningContext(DbContextOptions<ELearningContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
