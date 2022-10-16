using Asp.Net_PartialViews.Data;
using Asp.Net_PartialViews.Models;
using Asp.Net_PartialViews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_PartialViews.Controllers
{
    public class AccessoriesController : Controller
    {
        private AppDbContext _context;

        public AccessoriesController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Where(m=>!m.IsDeleted).Take(3).ToListAsync();

            AccessoriesVM accessoriesVM = new AccessoriesVM {
                Products = products,
            };

            return View(accessoriesVM);
        }
        public async Task<IActionResult> LoadMore()
        {
            IEnumerable<Product> products = await _context.Products.Where(m => !m.IsDeleted).Skip(3).Take(3).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }
    }
}
