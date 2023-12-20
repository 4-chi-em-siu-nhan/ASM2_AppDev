using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASM2_AppDev.Models.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        [ValidateNever]
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
