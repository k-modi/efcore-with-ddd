using System.Collections.Generic;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository.Infrastructure;

namespace efcore2_webapi.Repository
{
    public interface ITodoRepository : IRepository<TodoItem>
    {
        IList<TodoItem> GetAllTodoItems();
        IList<TodoItem> GetItemsAssignedTo(int personId);
        int TotalUserCount();
    }
}
