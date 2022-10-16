
using Asp.Net_PartialViews.Data;
using Asp.Net_PartialViews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_PartialViews.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            var dbProduct = await _context.Products.FindAsync(id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> basket;

            if (Request.Cookies ["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

            }
            else
            {
                basket = new List<BasketVM>();
            }

            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == dbProduct.Id);
            if (existProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

           

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction("Index");






            return Json("sadd");
        }

    }
}
