using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Repository.IRepository;

namespace ASM2_AppDev.Repository
{

    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDBContext _dbContext;

        public ShoppingCartRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(ShoppingCart obj)
        {
            _dbContext.ShoppingCarts.Update(obj);
        }
    }

}
