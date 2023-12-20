using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Repository;
using ASM2_AppDev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASM2_AppDev.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Customer)]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CheckoutController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IActionResult> CheckOut()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                var orderCode = Guid.NewGuid().ToString();
                var orderItem = new OrderHeader();
                orderItem.OrderCode = orderCode;
                orderItem.Email = userEmail;
                orderItem.OrderDate = DateTime.Now;
                _dbContext.Add(orderItem);
                _dbContext.SaveChanges();
                List<ShoppingCart> cartItems = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();
                foreach (var cart in cartItems)
                {
                    var orderDetails = new OrderDetails();
                    orderDetails.OrderCode = orderCode;
                    orderDetails.Email = userEmail;
                    orderDetails.BookId = cart.BookId;
                    orderDetails.Quantity = cart.Quantity;
                    orderDetails.Price = cart.Price;
                    _dbContext.Add(orderDetails);
                    _dbContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Checkout successfully";
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
