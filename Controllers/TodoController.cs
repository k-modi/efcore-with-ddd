using Microsoft.AspNetCore.Mvc;
using efcore2_webapi.Repository;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace efcore2_webapi.Webapp.Controllers
{
    [RouteAttribute("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoAppService _todoAppService;
        private readonly ITodoRepository _todoRepository;
        private readonly ICommentAppService _commentAppService;

        public TodoController(ITodoAppService todoAppService, 
                              ITodoRepository todoRepository, 
                              ICommentAppService commentAppService)
        {
            _todoAppService = todoAppService;
            _todoRepository = todoRepository;
            _commentAppService = commentAppService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return new ObjectResult(_todoRepository.GetAllTodoItems());

            // return new List<TodoItem>{
            //     new TodoItem("Add Priority (enum property) after initial model is created in the database.", _me, DateTime.UtcNow.AddHours(4)),
            //     new TodoItem("Add ParentTask (reference property) after initial model is created in the database.", _me, DateTime.UtcNow.AddDays(3)),
            //     new TodoItem("Todo 3", _me),
            // };
        }

        [HttpGet("{id:int}", Name = "GetTodo")]
        public IActionResult GetTodoItem(int id)
        {
            var item = _todoAppService.FindById(id);

            if (item != null)
                return new ObjectResult(item);
            else
                return NotFound();
        }

        [HttpGet("{todoItemId:int}/comments")]
        public IActionResult GetComments(int todoItemId)
        {
            return new ObjectResult(_commentAppService.GetCommentsForTodoItem(todoItemId));
        }

        [HttpGet("assigned/{personId:int}")]
        public IActionResult GetAssigned(int personId)
        {
            var todos = _todoRepository.GetItemsAssignedTo(personId);

            return new ObjectResult(todos);
        }

        [HttpPost]
        public IActionResult Add([FromBodyAttribute] TodoItem todoItem)
        {
            try
            {
                var todo = _todoRepository.Save(todoItem);

                return CreatedAtRoute("GetTodo", new { id = todo.Id }, todo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPutAttribute("{id:int}")]
        public IActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            try
            {
                var item = _todoRepository.FindById(id);

                if (item == null)
                {
                    return NotFound();
                }

                item = todoItem;

                _todoRepository.Update(item);

                return Ok();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("{todoItemId:int}/comment")]
        public IActionResult AddComment(int todoItemId, [FromBody] TodoItemCommentDto commentDto)
        {
            try
            {
                var item = _todoAppService.FindById(todoItemId);

                if (item == null)
                {
                    return NotFound();
                }
                
                _todoAppService.AddComment(todoItemId, commentDto);

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
