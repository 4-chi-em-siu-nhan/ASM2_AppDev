using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
