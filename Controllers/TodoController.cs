using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NetCoreApi.Data.Models;
using NetCoreApi.Data.Entities;
using System.Linq;

namespace NetCoreApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAll()
        {
            return await _context.Todo.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null) return NotFound();

            return todo;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
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


        [HttpPost]
        public async Task<ActionResult<Todo>> Post(Todo todo)
        {
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> Delete(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }


        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}