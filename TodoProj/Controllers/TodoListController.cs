using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoProj.Data;
using TodoProj.Models;

namespace TodoProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListControllers : Controller
    {
        private readonly TodoListAPIDbContext dbContext;
        public TodoListControllers(TodoListAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public async Task<IActionResult> TodoListGet()
        {
            return Ok(await dbContext.Todolists.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> TodoListsGet([FromRoute] Guid id)
        {
            var todolist = await dbContext.Todolists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }
            return Ok(todolist);
        }

        [HttpPost]

        public async Task<IActionResult> TodoListAdd(AddTodoList addTodoList)
        {
            var todolist = new Todolist()
            {
                Id = Guid.NewGuid(),
                Name = addTodoList.Name,
                Task = addTodoList.Task,
                IsCompleted = addTodoList.IsCompleted
            };

            await dbContext.Todolists.AddAsync(todolist);
            await dbContext.SaveChangesAsync();

            return Ok(todolist);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> TodoListUpdate([FromRoute] Guid id, UpdateTodoList updateTodolist)
        {
            var todolist = await dbContext.Todolists.FindAsync(id);
            if (todolist != null)
            {
                todolist.Name = updateTodolist.Name;
                todolist.Task = updateTodolist.Task;
                todolist.IsCompleted = updateTodolist.IsCompleted;

                await dbContext.SaveChangesAsync();

                return Ok(todolist);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> TodoListDelete([FromRoute] Guid id)
        {
            var todolist = await dbContext.Todolists.FindAsync(id);
            if (todolist != null)
            {
                dbContext.Remove(todolist);
                await dbContext.SaveChangesAsync();
                return Ok(todolist);
            }

            return NotFound();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Loggin(Login login)
        {
            var log = dbContext.Register.Where(x => x.Username.Equals(login.Username) &&
                      x.Password.Equals(login.Password)).FirstOrDefault();

            return Ok(log);
        }
    }
}
