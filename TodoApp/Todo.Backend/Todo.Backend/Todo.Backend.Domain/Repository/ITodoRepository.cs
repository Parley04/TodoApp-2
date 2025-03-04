using GenericRepository;

namespace Todo.Backend.Domain.Repositor
{
    public interface ITodoRepository: IRepository<Entities.Todo>
    {
    }
}
