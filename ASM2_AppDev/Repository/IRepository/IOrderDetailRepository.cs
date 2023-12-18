using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
    }
}
