using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Command.Update
{
    public sealed record UpdateTodoTagCommand
        (Guid Id,
        string TodoId,
        string TagId,
        DateTime UpdatedDate
        ) : IRequest<Result<string>>;

    internal sealed class UpdateTodoTagCommandHandler
        (
            ITodoTagRepository todoTagRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateTodoTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateTodoTagCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result<string>.Failure("Request is null");
            }

            Domain.Entities.TodoTag? todoTag = await todoTagRepository
                .WhereWithTracking(t=>t.Id== request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (todoTag == null)
            {
                return Result<string>.Failure("TodoTag not found");
            }

            todoTag.TodoId = request.TodoId;
            todoTag.TagId = request.TagId;
            todoTag.UpdatedDate = request.UpdatedDate;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todoTag.Id.ToString());
        }
    }
}
