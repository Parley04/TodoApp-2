using GenericRepository;
using Todo.Backend.Domain.Repositor;
using Todo.Backend.Infrastructure.Context;

namespace Todo.Backend.Infrastructure.Repository
{
    internal sealed class TodoRepository : Repository<Domain.Entities.Todo, ApplicationDbContext>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
