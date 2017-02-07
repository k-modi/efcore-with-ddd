using System;
using efcore2_webapi.Infrastructure.DomainKernel;

namespace efcore2_webapi.Domain.Entities
{
    public class Comment : AggregateRoot
    {
        public virtual string Text { get; set; }

        public virtual TodoItem TodoItem { get; set;}

        public virtual int CommenterId { get; set;}

        public virtual DateTime CommentedOn { get; set; } 
    }
}