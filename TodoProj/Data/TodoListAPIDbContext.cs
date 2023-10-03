using Microsoft.EntityFrameworkCore;
using TodoProj.Models;

namespace TodoProj.Data
{
    public class TodoListAPIDbContext : DbContext
    {
        public TodoListAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Todolist> Todolists { get; set; }
    }
}
