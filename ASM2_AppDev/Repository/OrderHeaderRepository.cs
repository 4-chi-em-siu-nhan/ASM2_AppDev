using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Repository.IRepository;

namespace ASM2_AppDev.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OrderHeaderRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(OrderHeader obj)
        {
            _dbContext.OrderHeaders.Update(obj);
        }
    }
}
