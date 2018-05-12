using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using netcore.Data;
using netcore.Models;

namespace netcore.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/TodoLine")]
    public class TodoLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoLine
        [HttpGet]
        [Authorize]
        public IActionResult GetTodoLine(string masterid)
        {
            return Json(new { data = _context.TodoLine.Where(x => x.todoId.Equals(masterid)).ToList() });
        }

        // POST: api/TodoLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTodoLine([FromBody] TodoLine todoLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (todoLine.todoLineId == string.Empty)
            {
                todoLine.todoLineId = Guid.NewGuid().ToString();
                _context.TodoLine.Add(todoLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(todoLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Edit data success." });
            }

        }

        // DELETE: api/TodoLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteTodoLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoLine = await _context.TodoLine.SingleOrDefaultAsync(m => m.todoLineId == id);
            if (todoLine == null)
            {
                return NotFound();
            }

            _context.TodoLine.Remove(todoLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }


        private bool TodoLineExists(string id)
        {
            return _context.TodoLine.Any(e => e.todoLineId == id);
        }


    }

}
