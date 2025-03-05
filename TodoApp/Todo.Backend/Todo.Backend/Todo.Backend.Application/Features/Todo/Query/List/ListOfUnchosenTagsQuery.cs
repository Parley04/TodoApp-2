using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Dtos;
using Todo.Backend.Domain.Repositor;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Query.List
{
    public sealed record ListOfUnchosenTagsQuery(string UserId, Guid TodoId) : IRequest<Result<List<TagDto>>>;
    public sealed record ListOfUnchosenTagsQueryHandler(
        ITodoTagRepository todoTagRepository,
        ITodoRepository todoRepository,
        ITagRepository tagRepository,
        IMapper mapper
    ) : IRequestHandler<ListOfUnchosenTagsQuery, Result<List<TagDto>>>
    {
        public async Task<Result<List<TagDto>>> Handle(ListOfUnchosenTagsQuery request, CancellationToken cancellationToken)
        {
            bool todoExists = await todoRepository
                .AnyAsync(t => t.Id == request.TodoId && t.UserId == request.UserId, cancellationToken);

            if (!todoExists)
            {
                return Result<List<TagDto>>.Failure("Todo not found for this user.");
            }

            var allTags = await tagRepository.GetAll().ToListAsync(cancellationToken);

            var relatedTodoTags = await todoTagRepository
                .Where(t => t.TodoId == request.TodoId)
                .ToListAsync(cancellationToken);

            var unchosenTags = allTags
                .Where(tag => !relatedTodoTags.Any(todoTag => todoTag.TagId == tag.Id && todoTag.IsActive))
                .OrderByDescending(tag => tag.CountedUses) 
                .ToList();

            var tagDtos = mapper.Map<List<TagDto>>(unchosenTags);

            return Result<List<TagDto>>.Succeed(tagDtos);
        }
    }

}
