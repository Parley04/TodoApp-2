using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Tag.Command.Update
{
    public sealed record UpdateTagCommand
        (
            Guid Id,
            string Name,
            DateTime UpdatedDate
        ) : IRequest<Result<string>>;

    internal sealed class UpdateTagCommandHandler
        (
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : IRequestHandler<UpdateTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Tag? tag = await tagRepository
                .WhereWithTracking(t => t.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                return Result<string>.Failure("Tag not found");
            }
            mapper.Map(request, tag);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(tag.Id.ToString());
        }
    }
}
