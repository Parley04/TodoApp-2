using GenericRepository;
using Todo.Backend.Domain.Entities;
using Todo.Backend.Domain.Repository;
using Todo.Backend.Infrastructure.Context;

namespace Todo.Backend.Infrastructure.Repository
{
    internal sealed class TagRepository : Repository<Tag, ApplicationDbContext>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
