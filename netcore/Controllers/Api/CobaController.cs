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
    [Route("api/Coba")]
    public class CobaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CobaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Coba
        [HttpGet]
        [Authorize]
        public IActionResult GetTodoLine()
        {
            return Json(new { data = _context.TodoLine });
        }

        // POST: api/Coba
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTodoLine([FromBody] TodoLine todoLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TodoLine.Add(todoLine);

            TodoLine check = _context.TodoLine.Where(x => x.todoLineId.Equals(todoLine.todoLineId)).FirstOrDefault();
            if (check == null)
            {
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

        // DELETE: api/Coba/5
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
