using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearningApp.Data;
using ELearningApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningApp.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ELearningContext _context;

        public LessonsController(ELearningContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var lessons = _context.Lessons.Include(l => l.Course);
            return View(await lessons.ToListAsync());
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title");
            return View();
        }

        // POST: Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,Title,CourseId,PassingThreshold")] LessonViewModel lessonviewmodel)
        {
            if (ModelState.IsValid)
            {
                var lesson = new Lesson
                {
                    Title = lessonviewmodel.Title,
                    CourseId = lessonviewmodel.CourseId,
                    PassingThreshold = lessonviewmodel.PassingThreshold

                };
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", lessonviewmodel.CourseId);
            return View(lessonviewmodel);


        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            var viewModel = new LessonViewModel
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                CourseId = lesson.CourseId,
                PassingThreshold = lesson.PassingThreshold

            };

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", lesson.CourseId);
            return View(viewModel);
        }

        // POST: Lessons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LessonId,Title,CourseId,PassingThreshold")] LessonViewModel lessonviewmodel)
        {
            if (id != lessonviewmodel.LessonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var lesson = await _context.Lessons.FindAsync(id);
                    if (lesson == null)
                    {
                        return NotFound();
                    }

                    lesson.Title = lessonviewmodel.Title;
                    lesson.CourseId = lessonviewmodel.CourseId;
                    lesson.PassingThreshold = lessonviewmodel.PassingThreshold;

                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lessonviewmodel.LessonId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", lessonviewmodel.CourseId);
            return View(lessonviewmodel);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            var viewModel = new LessonViewModel
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                CourseId = lesson.CourseId,
                Coursedescription = lesson.Course.Title,
                PassingThreshold = lesson.PassingThreshold

            };
            return View(viewModel);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
    }
}