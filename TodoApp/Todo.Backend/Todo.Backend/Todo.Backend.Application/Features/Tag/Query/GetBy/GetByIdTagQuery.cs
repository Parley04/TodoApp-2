using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Tag.Query.GetBy
{
    public sealed record GetByIdTagQuery(Guid Id): IRequest<Result<Domain.Entities.Tag>>;


    internal sealed record GetByIdTagQueryHandler(
            ITagRepository tagRepository
        ) : IRequestHandler<GetByIdTagQuery, Result<Domain.Entities.Tag>>
    {
        public async Task<Result<Domain.Entities.Tag>> Handle(GetByIdTagQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Tag? tag = await tagRepository
                .Where(x => x.Id == request.Id && x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            if (tag == null)
            {
                return Result<Domain.Entities.Tag>.Failure("Tag was not found.");
            }
            return tag;
        }
    }
}

