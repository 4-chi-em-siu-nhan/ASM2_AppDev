using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Models.ViewModels;
using ASM2_AppDev.Repository;
using ASM2_AppDev.Repository.IRepository;
using ASM2_AppDev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASM2_AppDev.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class CartController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CartController(ApplicationDBContext dBContext)
        { 
            _dbContext = dBContext;
        }

        public IActionResult Index()
        {
            List<ShoppingCart> cart = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();
            ShoppingCartVM shoppingCartVM = new()
            {
                ShoppingCartList = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)

            };
            return View(shoppingCartVM);
        }
        public async Task<IActionResult> Add(int? Id) 
        { 
            Book book = await _dbContext.Books.FindAsync(Id);
            List<ShoppingCart> cart = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();
            ShoppingCart carts = cart.Where(c => c.BookId == Id).FirstOrDefault();
            if (carts == null)
            {
                cart.Add(new ShoppingCart(book));

            }
            else
            {
                carts.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "Add to cart successfully";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Increase(int? Id)
        {
            List<ShoppingCart> cart = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart");
            ShoppingCart cartItem = cart.Where(c => c.BookId == Id).FirstOrDefault();

            if (cartItem.Quantity >= 1 ) 
            {
                ++cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p  => p.BookId == Id);
                TempData["success"] = "Removed from cart successfully";
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
                
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
               
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrease(int? Id)
        {
            List<ShoppingCart> cart = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart");
            ShoppingCart cartItem = cart.Where(c => c.BookId == Id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.BookId == Id);
                TempData["success"] = "Removed from cart successfully";
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int? Id)
        {
            List<ShoppingCart> cart = HttpContext.Session.GetJson<List<ShoppingCart>>("Cart");
            cart.RemoveAll(p => p.BookId == Id);
            TempData["success"] = "Removed from cart successfully";
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

    }
}
