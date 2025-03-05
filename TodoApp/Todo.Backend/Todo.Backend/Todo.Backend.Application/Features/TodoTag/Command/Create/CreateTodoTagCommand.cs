using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Command.Create
{
    public sealed record CreateTodoTagCommand
        (
            Guid TodoId,
            Guid TagId,
            DateTime CreatedDate,
            bool IsActive = true
        ) : IRequest<Result<string>>;

    internal sealed class CreateTodoTagCommandHandler
        (
            ITodoTagRepository todoTagRepository,
            ITagRepository tagRepository,
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
            Domain.Entities.Tag? tag = await tagRepository
                .WhereWithTracking(t => t.Id == request.TagId)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                return Result<string>.Failure("Tag not found");
            }
            tag.CountedUses++;
            mapper.Map(request, tag);

            Domain.Entities.TodoTag todoTag = mapper.Map<Domain.Entities.TodoTag>(request);
            await todoTagRepository.AddAsync(todoTag, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todoTag.Id.ToString());
        }
    }
}
