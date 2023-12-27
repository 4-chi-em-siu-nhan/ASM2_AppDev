using ASM2_AppDev.Repository.IRepository;
using ASM2_AppDev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using ASM2_AppDev.Models.ViewModels;
using ASM2_AppDev.Models;
using ASM2_AppDev.Data;
using Microsoft.EntityFrameworkCore;

namespace ASM2_AppDev.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.OrderHeaders.OrderByDescending(p => p.Id).ToListAsync());
        }


        public async Task<IActionResult> Details(string ordercode)
        {

            var orderDetail = await _dbContext.OrderDetails.Include(o => o.Book).Where(o => o.OrderCode == ordercode).ToListAsync();
            return View(orderDetail);
        }
    }
}
