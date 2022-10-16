using Asp.Net_PartialViews.Data;
using Asp.Net_PartialViews.Models;
using Asp.Net_PartialViews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_PartialViews.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketItem = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            List<BasketDetailVM> basketDetail = new List<BasketDetailVM>();

            foreach (var item in basketItem)
            {
                Product product = await _context.Products.Where(m => m.Id == item.Id && m.IsDeleted == false).FirstOrDefaultAsync();
                BasketDetailVM newBasket = new BasketDetailVM
                {
                    Name = product.Name,
                    Image = product.ProductImage,
                    Price = product.Price*item.Count,
                    Count = item.Count


                };

                basketDetail.Add(newBasket);

            }



            return View(basketDetail);
        }
    }
}
