using AutoMapper;
using GenericRepository;
using MediatR;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Command.Create
{
    public sealed record CreateTodoTagCommand
        (
            string TodoId,
            string TagId,
            DateTime CreatedDate,
            bool IsActive = true
        ): IRequest<Result<string>>;

    internal sealed class CreateTodoTagCommandHandler
        (
            ITodoTagRepository todoTagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : IRequestHandler<CreateTodoTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateTodoTagCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result<string>.Failure("Request is null");
            }
            Domain.Entities.TodoTag todoTag = mapper.Map<Domain.Entities.TodoTag>(request);
            await todoTagRepository.AddAsync(todoTag, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todoTag.Id.ToString());
        }
    }
}
