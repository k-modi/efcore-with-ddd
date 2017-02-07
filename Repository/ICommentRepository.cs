using System.Collections.Generic;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository.Infrastructure;

namespace efcore2_webapi.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IList<Comment> GetByTodoItemId(int todoItemId);

        IList<Comment> GetByCommenterId(int commenterId);
    }
}