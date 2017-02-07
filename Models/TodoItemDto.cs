using System;
using System.Collections.Generic;

namespace efcore2_webapi.Models
{
    public class TodoItemDto : EntityDto
    {
        public TodoItemDto()
        {
            Comments = new List<TodoItemCommentDto>();
        }
        public string Summary { get; set; }

        public DateTime? DueDate { get; set; }

        public UserDto Owner { get; set;}

        public List<TodoItemCommentDto> Comments { get; set;}
    }

    public class TodoItemCommentDto : EntityDto
    {
        public string Text { get; set; }

        public UserDto Commenter { get; set; }

        public DateTime CommentedOn { get; set; }
    }

    public class CommenterDto : EntityDto
    {
        public string Name { get; set; }
    }

    public class TodoItemOwnerDto : EntityDto
    {
        public string Username { get; set; }

        public string DisplayName { get; set; }
    }
}
