using System;
using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Infrastructure;
using efcore2_webapi.Repository;
using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices
{
    public class TodoAppService : ITodoAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;

        public TodoAppService(IUnitOfWork unitOfWork,
                              ITodoRepository todoRepository,
                              IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _userRepository = userRepository;
        }

        public int Save(TodoItemDto todoItemDto)
        {
            int todoItemId = 0;
            // using(_unitOfWork)
            {
                //Add a new Todo item with associated details.
                var todo = ModelToDomain(todoItemDto);

                _todoRepository.Save(todo);

                _unitOfWork.Commit();
            }

            return todoItemId;
        }

        private TodoItem ModelToDomain(TodoItemDto model)
        {
            var domain = new TodoItem();

            domain.Summary = model.Summary;
            domain.DueDate = model.DueDate;

            var owner = _userRepository.FindByUsername(model.Owner.Username);
            domain.OwnerId = owner?.Id;

            foreach (var comment in model.Comments)
            {
                var domainComment = new Comment
                {
                    Text = comment.Text,
                    TodoItem = domain,
                    CommentedOn = (comment.CommentedOn == DateTime.MinValue)
                        ? DateTime.UtcNow
                        : comment.CommentedOn,
                };

                var commenter = _userRepository.FindByUsername(comment.Commenter.Name);
                domainComment.CommenterId = commenter.Id;

                domain.AddComment(domainComment);
            }

            return domain;
        }

        public TodoItemDto FindById(int id)
        {
            var item = _todoRepository.FindById(id);

            TodoItemDto itemDto = null;

            if (item != null)
            {
                itemDto = new TodoItemDto
                {
                    Id = item.Id,
                    Summary = item.Summary,
                    DueDate = item.DueDate
                };

                User owner = null;

                if (item.OwnerId.HasValue)
                {
                    owner = _userRepository.FindById(item.OwnerId.Value);

                    itemDto.Owner = new UserDto
                    {
                        Id = owner.Id,
                        Name = owner.Name,
                    };
                }

                //Populate CommentDto
                foreach (var comment in item.Comments)
                {
                    itemDto.Comments.Add( new TodoItemCommentDto{
                        Id = comment.Id,
                        Text = comment.Text,
                        CommentedOn = comment.CommentedOn,                        
                    });
                }
            }

            return itemDto;
        }

        public void AddComment(int todoItemId, TodoItemCommentDto commentDto)
        {
            var item = _todoRepository.FindById(todoItemId);

            if (commentDto != null)
            {
                item.AddComment(new Comment{
                    Text = commentDto.Text,
                    TodoItem = item,
                    CommenterId = commentDto.Commenter.Id,
                    CommentedOn = (commentDto.CommentedOn == DateTime.MinValue)
                            ? DateTime.UtcNow
                            : commentDto.CommentedOn,
                });

                _todoRepository.Update(item);
            }
        }
    }
}