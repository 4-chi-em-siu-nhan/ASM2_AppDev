using ASM2_AppDev.Models;

namespace ASM2_AppDev.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}
