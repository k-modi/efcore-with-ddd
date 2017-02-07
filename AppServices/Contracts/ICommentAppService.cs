using System.Collections.Generic;
using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices.Contracts
{
    public interface ICommentAppService
    {
        IList<TodoItemCommentDto> GetCommentsForTodoItem(int todoItemId);
    }
}