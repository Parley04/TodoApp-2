using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Command.Udate
{
    public sealed record UpdateTodoCommand
        (
            Guid Id,
            string Title,
            string Description,
            int BackgroundColor,
            bool IsCompleted,
            //List<string> Tags,
            DateTime UpdatedDate
        ) : IRequest<Result<string>>;

    internal sealed class UpdateTodoCommandHandler
        (ITodoRepository todoRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : IRequestHandler<UpdateTodoCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Todo? todo = await todoRepository
                .WhereWithTracking(t => t.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (todo == null)
            {
                return Result<string>.Failure("Todo not found");
            }
      
            mapper.Map(request, todo);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todo.Id.ToString());
        }
    }
}
