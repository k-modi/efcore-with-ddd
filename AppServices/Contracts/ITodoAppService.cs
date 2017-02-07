using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices.Contracts
{
    public interface ITodoAppService
    {   

        int Save(TodoItemDto todoItemDto);
        
        TodoItemDto FindById(int id);
     
        void AddComment(int todoItemId, TodoItemCommentDto commentDto);
    }
}