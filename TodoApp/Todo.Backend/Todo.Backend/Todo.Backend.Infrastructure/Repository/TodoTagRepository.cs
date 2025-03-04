using GenericRepository;
using Todo.Backend.Domain.Entities;
using Todo.Backend.Domain.Repository;
using Todo.Backend.Infrastructure.Context;

namespace Todo.Backend.Infrastructure.Repository
{
    internal sealed class TodoTagRepository : Repository<TodoTag, ApplicationDbContext>, ITodoTagRepository
    {
        public TodoTagRepository(ApplicationDbContext context) : base(context) 
        { 
        }
    }
}
