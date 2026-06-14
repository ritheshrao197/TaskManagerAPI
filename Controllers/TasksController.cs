using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
        
        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }
        
        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var userId = GetUserId();
            var tasks = IsAdmin() 
                ? await _context.Tasks.Include(t => t.User).ToListAsync()
                : await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
                
            return Ok(tasks);
        }
        
        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
                return NotFound(new { message = "Task not found" });
                
            if (!IsAdmin() && task.UserId != userId)
                return Forbid();
                
            return Ok(task);
        }
        
        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            var userId = GetUserId();
            task.UserId = userId;
            task.CreatedAt = DateTime.UtcNow;
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        
        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var userId = GetUserId();
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
                return NotFound(new { message = "Task not found" });
                
            if (!IsAdmin() && task.UserId != userId)
                return Forbid();
                
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DueDate = updatedTask.DueDate;
            
            await _context.SaveChangesAsync();
            
            return Ok(task);
        }
        
        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
                return NotFound(new { message = "Task not found" });
                
            if (!IsAdmin() && task.UserId != userId)
                return Forbid();
                
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
        // PATCH: api/tasks/{id}/complete
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
                return NotFound(new { message = "Task not found" });
                
            if (!IsAdmin() && task.UserId != userId)
                return Forbid();
                
            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            
            return Ok(task);
        }
    }
}