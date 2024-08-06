using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearningApp.Data;
using ELearningApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningApp.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ELearningContext _context;

        public AnswersController(ELearningContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var answers = _context.Answers.Include(a => a.Question);
            return View(await answers.ToListAsync());
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content");
            return View();
        }

        // POST: Answers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,Content,IsCorrect,QuestionId")] AnswerViewModel answerview)
        {
            if (ModelState.IsValid)
            {
                var answer = new Answer
                {
                    Content = answerview.Content,
                    IsCorrect = answerview.IsCorrect,
                    QuestionId = answerview.QuestionId                    
                };
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content", answerview.QuestionId);
            return View(answerview);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            var viewModel = new AnswerViewModel
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect
            };
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content", answer.QuestionId);
            return View(viewModel);
        }

        // POST: Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,Content,IsCorrect,QuestionId")] AnswerViewModel answerview)
        {
            if (id != answerview.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var answer = await _context.Answers.FindAsync(id);
                    if (answer == null)
                    {
                        return NotFound();
                    }

                    answer.Content = answerview.Content;
                    answer.IsCorrect = answerview.IsCorrect;
                    answer.QuestionId = answerview.QuestionId;

                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answerview.AnswerId))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content", answerview.QuestionId);
            return View(answerview);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }
            var viewModel = new AnswerViewModel
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                QuestionId = answer.QuestionId,
                QuestionDescription = answer.Question.Content,
                IsCorrect = answer.IsCorrect
            };
            return View(viewModel);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}