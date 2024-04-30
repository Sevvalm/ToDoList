using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Entities;

namespace ToDoList.Data
{
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
