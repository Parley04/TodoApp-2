using AutoMapper;
using GenericRepository;
using MediatR;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Tag.Command.Create
{
    public sealed record CreateTagCommand
        (
            string Name,
            string UserId,
            DateTime CreatedDate,
            bool IsActive = true
        ) : IRequest<Result<string>>;

    internal sealed class CreateTagCommandHandler
        (
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : IRequestHandler<CreateTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result<string>.Failure("Request is null");
            }
            bool tagExists = await tagRepository
            .AnyAsync(t => t.IsActive && t.Name.ToLower() == request.Name.ToLower() && t.UserId == request.UserId, cancellationToken);
            if (tagExists)
            {
                return Result<string>.Failure("You already have a tag with this name.");
            }

            Domain.Entities.Tag tag = mapper.Map<Domain.Entities.Tag>(request);
            await tagRepository.AddAsync(tag, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(tag.Id.ToString());
        }
    }
}
