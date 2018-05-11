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
    public IActionResult
    GetTodoLine()
    {
    return Json(new { data = _context.TodoLine });
    }

        // GET: api/TodoLine/5
        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetTodoLine([FromRoute] string id)
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

            return Ok(todoLine);
            }

            // PUT: api/TodoLine/5
            [HttpPut("{id}")]
            public async Task<IActionResult>
                PutTodoLine([FromRoute] string id, [FromBody] TodoLine todoLine)
                {
                if (!ModelState.IsValid)
                {
                return BadRequest(ModelState);
                }

                if (id != todoLine.todoLineId)
                {
                return BadRequest();
                }

                _context.Entry(todoLine).State = EntityState.Modified;

                try
                {
                await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                if (!TodoLineExists(id))
                {
                return NotFound();
                }
                else
                {
                throw;
                }
                }

                return NoContent();
                }

                // POST: api/TodoLine
                [HttpPost]
                public async Task<IActionResult>
                    PostTodoLine([FromBody] TodoLine todoLine)
                    {
                    if (!ModelState.IsValid)
                    {
                    return BadRequest(ModelState);
                    }

                    _context.TodoLine.Add(todoLine);
                            await _context.SaveChangesAsync();

                    return CreatedAtAction("GetTodoLine", new { id = todoLine.todoLineId }, todoLine);
                    }

                    // DELETE: api/TodoLine/5
                    [HttpDelete("{id}")]
                    public async Task<IActionResult>
                        DeleteTodoLine([FromRoute] string id)
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

                        return Ok(todoLine);
                        }

                        private bool TodoLineExists(string id)
                        {
                        return _context.TodoLine.Any(e => e.todoLineId == id);
                        }
                        }
                        }
