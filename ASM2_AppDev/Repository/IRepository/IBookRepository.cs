using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book entity);
    }
}
