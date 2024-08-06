using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearningApp.Data;
using ELearningApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ELearningContext _context;

        public QuestionsController(ELearningContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var questions = _context.Questions.Include(q => q.Lesson);
            return View(await questions.ToListAsync());
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "Title");
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,Content,LessonId,Type,Score")] QuestionViewModel questionview)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Content = questionview.Content,
                    LessonId = questionview.LessonId,
                    Type = questionview.Type,
                    Score = questionview.Score
                };
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "Title", questionview.LessonId);
            return View(questionview);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            var viewModel = new QuestionViewModel
            {
                QuestionId = question.QuestionId,
                Content = question.Content,
                LessonId = question.LessonId,
                Type = question.Type,
                Score = question.Score
            };

            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "Title", question.LessonId);
            return View(viewModel);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Content,LessonId,Type,Score")] QuestionViewModel questionview)
        {
            if (id != questionview.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var question = await _context.Questions.FindAsync(id);
                    if (question == null)
                    {
                        return NotFound();
                    }

                    question.Content = questionview.Content;
                    question.LessonId = questionview.LessonId;
                    question.Type = questionview.Type;
                    question.Score = questionview.Score;

                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(questionview.QuestionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "Title", questionview.LessonId);
            return View(questionview);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Lesson)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            var viewModel = new QuestionViewModel
            {
                QuestionId = question.QuestionId,
                Content = question.Content,
                LessonId = question.LessonId,
                LessonDescription = question.Lesson.Title,
                Type = question.Type,
                Score = question.Score
            };
            return View(viewModel);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}