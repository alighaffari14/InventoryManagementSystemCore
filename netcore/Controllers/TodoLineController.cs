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


    public class TodoLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TodoLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TodoLine.Include(t => t.todo);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: TodoLine/Details/5
        public async Task<IActionResult>
    Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoLine = await _context.TodoLine
                        .Include(t => t.todo)
            .SingleOrDefaultAsync(m => m.todoLineId == id);
            if (todoLine == null)
            {
                return NotFound();
            }

            return View(todoLine);
        }


        // GET: TodoLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.TodoLine.SingleOrDefault(m => m.todoLineId == id);
            var selected = _context.Todo.SingleOrDefault(m => m.todoId == masterid);
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId");
            if (check == null)
            {
                TodoLine objline = new TodoLine();
                objline.todo = selected;
                objline.todoId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: TodoLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("todoLineId,todoLineName,description,todoId,createdAt")] TodoLine todoLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoLine.todoId);
            return View(todoLine);
        }

        // GET: TodoLine/Edit/5
        public async Task<IActionResult>
            Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoLine = await _context.TodoLine.SingleOrDefaultAsync(m => m.todoLineId == id);
            if (todoLine == null)
            {
                return NotFound();
            }
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoLine.todoId);
            return View(todoLine);
        }

        // POST: TodoLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Edit(string id, [Bind("todoLineId,todoLineName,description,todoId,createdAt")] TodoLine todoLine)
        {
            if (id != todoLine.todoLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoLineExists(todoLine.todoLineId))
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
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoLine.todoId);
            return View(todoLine);
        }

        // GET: TodoLine/Delete/5
        public async Task<IActionResult>
            Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoLine = await _context.TodoLine
        .Include(t => t.todo)
            .SingleOrDefaultAsync(m => m.todoLineId == id);
            if (todoLine == null)
            {
                return NotFound();
            }

            return View(todoLine);
        }




        // POST: TodoLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(string id)
        {
            var todoLine = await _context.TodoLine.SingleOrDefaultAsync(m => m.todoLineId == id);
            _context.TodoLine.Remove(todoLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoLineExists(string id)
        {
            return _context.TodoLine.Any(e => e.todoLineId == id);
        }

    }
}


