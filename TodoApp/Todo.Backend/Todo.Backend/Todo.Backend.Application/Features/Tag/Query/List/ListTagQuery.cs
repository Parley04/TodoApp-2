using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Dtos;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Tag.Query.List
{
    public sealed record ListTagQuery(string UserId) : IRequest<Result<List<Domain.Entities.Tag>>>;

    public sealed record ListTagQueryHandler(
            ITagRepository tagRepository
        ) : IRequestHandler<ListTagQuery, Result<List<Domain.Entities.Tag>>>
    {
        public async Task<Result<List<Domain.Entities.Tag>>> Handle(ListTagQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Tag> tag = await tagRepository.GetAll()
                .OrderByDescending(x => x.CountedUses)
                .Where(x => x.UserId == request.UserId && x.IsActive==true)
                .ToListAsync(cancellationToken);

            if (tag == null)
            {
                return Result<List<Domain.Entities.Tag>>.Failure("Tag not found.");
            }
            return tag;
        }
    }
}
