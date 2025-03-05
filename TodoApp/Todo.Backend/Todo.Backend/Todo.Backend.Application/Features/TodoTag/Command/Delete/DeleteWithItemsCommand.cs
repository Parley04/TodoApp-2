using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Command.Delete
{
    public sealed record DeleteWithItemsCommand(Guid todoId, Guid tagId): IRequest<Result<string>>;

    public sealed record DeleteWithItemsCommandHandler(
                ITodoTagRepository todoTagRepository,
                ITagRepository tagRepository,
                IUnitOfWork unitOfWork,
                IMapper mapper
            ) : IRequestHandler<DeleteWithItemsCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteWithItemsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result<string>.Failure("Request is null");
            }
            var todoTag = await todoTagRepository
                .WhereWithTracking(tt => tt.TodoId == request.todoId && tt.TagId == request.tagId && tt.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);
            if (todoTag == null)
            {
                return Result<string>.Failure("TodoTag not found");
            }
            Domain.Entities.Tag? tag = await tagRepository
                .WhereWithTracking(t => t.Id == request.tagId && t.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                return Result<string>.Failure("Tag not found");
            }
            else if (tag.CountedUses > 0)
            {
                tag.CountedUses--;
                mapper.Map(request, tag);
            }
            todoTag.IsActive = false;
            todoTag.DeletedDate = DateTime.Now;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todoTag.Id.ToString());
        }
    }
}
