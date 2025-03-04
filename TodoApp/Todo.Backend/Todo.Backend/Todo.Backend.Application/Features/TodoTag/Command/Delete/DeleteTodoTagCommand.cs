using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Command.Delete
{
    public sealed record DeleteTodoTagCommand
        (
            Guid Id,
            DateTime DeletedDate,
            bool IsActive = false
        ) : IRequest<Result<string>>;

    internal sealed class DeleteTodoTagCommandHandler
        (
            ITodoTagRepository todoTagRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteTodoTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTodoTagCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.TodoTag? todoTag = await todoTagRepository
                .WhereWithTracking(t => t.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (todoTag == null)
            {
                return Result<string>.Failure("TodoTag not found");
            }
            todoTag.IsActive = false;
            todoTag.DeletedDate = request.DeletedDate;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todoTag.Id.ToString());
        }
    }
}
