using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BuyNow.Models;
using Newtonsoft.Json;

namespace BuyNow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Product> products = new List<Product>();

        products = Product.GetProductList();
         if (HttpContext.Session.Get("cart")!=null)  
           {  
               List<Product> li = new List<Product>();  
           }  
           else  
           {
                ViewBag.cart =0;
           }
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
