using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart entity);
    }

}
