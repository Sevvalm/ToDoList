using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.Entities;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoDbContext dbContext;
        public TodoController(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var todo = await dbContext.Todos.AsNoTracking().ToListAsync();

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddTodoViewModel viewModel)
        {
            var todo = new Todo
            {
                Name = viewModel.Name
            };
            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "todo");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == Id);
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo viewModel)
        {
            var todo = await dbContext.Todos.FindAsync(viewModel.Id);

            if (todo is not null)
            {
                todo.Name = viewModel.Name;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Todo");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Todo viewModel)
        {
            var todo = await dbContext.Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (todo is not null)
            {
                dbContext.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Todo");
        }
    }
}
