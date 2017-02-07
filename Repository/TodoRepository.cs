using System.Collections.Generic;
using System.Linq;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace efcore2_webapi.Repository
{
    internal class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(IDbContextManager contextMgr) : base(contextMgr)
        { }

        public override TodoItem FindById(int id)
        {
            return RootSet.Include(t => t.Comments).SingleOrDefault(t => t.Id == id);
        }

        public IList<TodoItem> GetAllTodoItems()
        {
            return RootSet.ToList();
        }

        public IList<TodoItem> GetItemsAssignedTo(int personId)
        {
            return RootSet.Where(item => item.OwnerId == personId).ToList();
        }

        public int TotalUserCount()
        {
            return RootSet.Count();
        }
    }
}
