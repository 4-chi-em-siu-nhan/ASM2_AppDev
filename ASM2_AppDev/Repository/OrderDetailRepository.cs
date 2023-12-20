using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Repository.IRepository;

namespace ASM2_AppDev.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OrderDetailRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(OrderDetails obj)
        {
            _dbContext.OrderDetails.Update(obj);
        }
    }
}
