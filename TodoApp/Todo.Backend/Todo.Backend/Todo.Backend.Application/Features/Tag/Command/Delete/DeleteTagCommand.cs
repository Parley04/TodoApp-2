using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Tag.Command.Delete
{
    public sealed record DeleteTagCommand(
        Guid Id,
        DateTime DeletedDate,
        bool IsActive = false
        ) : IRequest<Result<string>>;

    internal sealed class DeleteTagCommandHandler
        (
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteTagCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Tag? tag = await tagRepository
                .WhereWithTracking(t => t.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                return Result<string>.Failure("Tag not found");
            }
            tag.IsActive = false;
            tag.DeletedDate = request.DeletedDate;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(tag.Id.ToString());
        }
    }
}
