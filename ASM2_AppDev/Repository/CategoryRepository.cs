using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Repository.IRepository;

namespace ASM2_AppDev.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
        }
        public IEnumerable<Category> GetAllApproval()
        {
            var categories = _dbContext.Categories.Where(c => c.Status == "Approval").ToList();
            return categories;
        }
    }
}
