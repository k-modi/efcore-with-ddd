using System.Collections.Generic;
using System.Linq;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository.Infrastructure;

namespace efcore2_webapi.Repository
{
    internal class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContextManager contextMgr) : base(contextMgr)
        {}

        public IList<Comment> GetByCommenterId(int commenterId)
        {
            return RootSet.Where(c => c.CommenterId == commenterId).ToList();
        }

        public IList<Comment> GetByTodoItemId(int todoItemId)
        {
            return RootSet.Where(c => c.TodoItem.Id == todoItemId).ToList();
        }
    }
}