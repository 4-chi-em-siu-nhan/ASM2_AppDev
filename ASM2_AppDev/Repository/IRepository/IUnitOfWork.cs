namespace ASM2_AppDev.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderDetailRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeader { get; }
        void Save();

    }
}
