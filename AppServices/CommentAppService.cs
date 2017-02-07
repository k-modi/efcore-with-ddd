using System.Collections.Generic;
using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Repository;
using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _personRepository;

        public CommentAppService(ICommentRepository commentRepository,
                                 ITodoRepository todoRepository,
                                 IUserRepository personRepository)
        {
            _commentRepository = commentRepository;
            _todoRepository = todoRepository;
            _personRepository = personRepository;
        }

        public IList<TodoItemCommentDto> GetCommentsForTodoItem(int todoItemId)
        {
            var comments = _commentRepository.GetByTodoItemId(todoItemId);

            var commentDtoList = new List<TodoItemCommentDto>();

            foreach (var comment in comments)
            {
                var person = _personRepository.FindById(comment.CommenterId);

                var commentDto = new TodoItemCommentDto
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    CommentedOn = comment.CommentedOn,
                    Commenter = new UserDto
                    {
                        Id = person.Id,
                        Name = person.Name
                    }
                };

                commentDtoList.Add(commentDto);
            }

            return commentDtoList;
        }
    }
}