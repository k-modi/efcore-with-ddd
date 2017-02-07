using System;
using System.Collections.Generic;
using efcore2_webapi.Infrastructure.DomainKernel;

namespace efcore2_webapi.Domain.Entities
{
    public class TodoItem : AggregateRoot
    {
        private ISet<Comment> _comments;

        public TodoItem()
        {
            _comments = new HashSet<Comment>();
        }

        public virtual string Summary { get; set; }

        public virtual DateTime? DueDate { get; set; }

        public virtual int? OwnerId { get; set; }

        public virtual IEnumerable<Comment> Comments
        {
            get
            {
                return _comments ?? (_comments = new HashSet<Comment>());
            }
        }

        public virtual void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public virtual void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
        }
    }
}
