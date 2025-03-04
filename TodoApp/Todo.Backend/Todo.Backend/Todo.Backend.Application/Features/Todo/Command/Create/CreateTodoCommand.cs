using AutoMapper;
using GenericRepository;
using MediatR;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Command.Create
{
    public sealed record CreateTodoCommand(
        string UserId,
        string Title,
        string Description,
        int BackgroundColor,
        DateTime CreatedDate,
        bool IsCompleted,
        List<string> Tags,
        bool IsActive = true
        ) : IRequest<Result<string>>;

    internal sealed class CreateTodoCommandHandler(
        ITodoRepository todoRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateTodoCommand, Result<string>>
    {  
       
        public async Task<Result<string>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Todo todo= mapper.Map<Domain.Entities.Todo>(request);
            await todoRepository.AddAsync(todo, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todo.Id.ToString());
        }
    }
}
