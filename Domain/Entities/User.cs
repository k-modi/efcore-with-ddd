using efcore2_webapi.Infrastructure.DomainKernel;

namespace efcore2_webapi.Domain.Entities
{
    public class User : AggregateRoot
    {
        // private ISet<TodoItem> _todoItems;

        protected internal User()
        { }

        public User(string name)
        {
            Name = name;
        }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Username { get; set; }

        // public virtual ISet<TodoItem> TodoItems
        // {
        //     get
        //     {
        //         return _todoItems ?? (_todoItems = new HashSet<TodoItem>());
        //     }
        // }
    }
}
