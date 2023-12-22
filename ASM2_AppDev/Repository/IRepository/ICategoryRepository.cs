using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category entity);
        public IEnumerable<Category> GetAllApproval();
    }
}
