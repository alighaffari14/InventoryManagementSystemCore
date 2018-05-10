using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models;

namespace netcore.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todo.ToListAsync());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .SingleOrDefaultAsync(m => m.todoId == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("todoId,todoName,description")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.todoId = Guid.NewGuid();
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.todoId == id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("todoId,todoName,description")] Todo todo)
        {
            if (id != todo.todoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.todoId))
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
            return View(todo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .SingleOrDefaultAsync(m => m.todoId == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.todoId == id);
            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todo.Any(e => e.todoId == id);
        }
    }
}
