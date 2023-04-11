
using Microsoft.AspNetCore.Mvc;
using BuyNow.Models;

using Newtonsoft.Json;
using static BuyNow.Models.Product;

namespace BuyNow.Controllers
{
    public class ShoppingController : Controller
    {
  
         public IActionResult productview(int id_product,string product_code ,string product_name,double price,string category,string brand,string sp_category,string img,double discount,double old_price)
        {
            Product product = new Product(id_product,product_code,product_name,price,category,brand,sp_category,img,discount,old_price);

        return View(product);
    }
    public IActionResult Index(string Category)
    {
        List<Product> products = new List<Product>();
        
        ViewBag.category= Category;

        products = Product.GetProductsByCategory(Category);
         if (HttpContext.Session.Get("cart")!=null)  
           {  
               List<Product> li = new List<Product>();  

               HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(li)); 
              TempData["Cartqyt"] = li.Count();            
               HttpContext.Session.SetInt32("count",li.Count());  
  
           }  
           else  
           {
                TempData["Cartqyt"] =0;
           }
        return View(products);
    }
     public IActionResult ViewAllProducts()
    {
          List<Product>  products= new List<Product>();//(JsonConvert.DeserializeObject<List<Product>>((string)TempData["SearchResults"]));//new List<Product>((IEnumerable<Product>)(JsonConvert.DeserializeObject((string)TempData["Cart"])));
    products = Product.GetProductsByName(Convert.ToString(TempData["SearchResults"]));
    return View("~/Views/Shopping/Index.cshtml",products);
    }
        public IActionResult Cart()
    {
          List<Product>  cart= new List<Product>(JsonConvert.DeserializeObject<List<Product>>((string)TempData["Cart"]));//new List<Product>((IEnumerable<Product>)(JsonConvert.DeserializeObject((string)TempData["Cart"])));
   TempData.Keep();
    return View(cart);
    }
   public IActionResult ViewAll(string prodsearch)
    {
         List<Product> products = new List<Product>();
        
        products = Product.GetProductsByName(prodsearch);

         if (HttpContext.Session.Get("cart")!=null)  
           {  
               List<Product> li = new List<Product>();  

               HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(li)); 
              TempData["Cartqyt"] = li.Count();            
               HttpContext.Session.SetInt32("count",li.Count());  
  
           }  
           else  
           {
                TempData["Cartqyt"] =0;
           }
    return View(products);
    
    }
    public ActionResult product()
    {
        return View();
    }
 public ActionResult RemoveFromCart(string product_code){
     var cart =TempData["Cart"]; 
        List<Product>  products= new List<Product>(JsonConvert.DeserializeObject<List<Product>>((string)cart));//new List<Product>((IEnumerable<Product>)(JsonConvert.DeserializeObject((string)TempData["Cart"])));

     products.ToList().RemoveAll(c => c.product_code==product_code);
        List<Product> prods= products.Where(c => c.product_code==product_code).ToList();
        foreach(Product prod in prods){
                products.Remove(prod);
        }

            TempData["Cart"] = JsonConvert.SerializeObject(products);
            ViewBag.cart = products.Count(); 
            TempData["carttotal"] = Convert.ToString(products.Sum(a => a.price));
            TempData["Cartqyt"] = products.Count();  

 return RedirectToAction("Index", "Home");  
 }

public ActionResult SearchProducts(string prodsearch){
     List<Product> products = new List<Product>();
        
        products = Product.GetProductsByName(prodsearch);
   TempData["SearchResults"] = prodsearch;//Newtonsoft.Json.JsonConvert.SerializeObject(products);
    
    return PartialView(products);
}

public ActionResult Checkout(){
     
     ViewModel vm = new ViewModel();
     
     
     var cart =TempData["Cart"]; 
        if (cart!=null){  
       List<Product>  products = new List<Product>(JsonConvert.DeserializeObject<List<Product>>((string)cart));//new List<Product>((IEnumerable<Product>)(JsonConvert.DeserializeObject((string)TempData["Cart"])));
        vm.CartItems = products;
        }
         vm.CartShipping =  "free";

        vm.CartTotal =  (TempData["CartTotal"]==null?0:Convert.ToDouble(TempData["CartTotal"]));
           vm.CartShipping = (TempData["Shipping"]==null?"free": Convert.ToString(TempData["Shipping"]));

           vm.ShippingAmount = (vm.CartShipping=="free"?0:(vm.CartShipping=="local"?45:100));
TempData.Keep();
    return View(vm);
}

        public ActionResult AddToCart(int id_product,string product_code ,string product_name,double price,string category,string brand,string sp_category,string img,double discount,double old_price)
        {
                Product product = new Product(id_product,product_code,product_name,price,category,brand,sp_category,img,discount,old_price);

    var cart =TempData["Cart"]; 
        if (cart==null)  
           {  
               List<Product> li = new List<Product>();  
                product.quantity=1;
               li.Add(product);  
               TempData["Cart"] = JsonConvert.SerializeObject(li);
               HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(li)); 
              TempData["Cartqyt"] = li.Count();            
               HttpContext.Session.SetInt32("count",li.Count());  
                 TempData["carttotal"]  =  Convert.ToString(product.price);
  
           }  
           else  
           {
         List<Product>  products= new List<Product>(JsonConvert.DeserializeObject<List<Product>>((string)cart));//new List<Product>((IEnumerable<Product>)(JsonConvert.DeserializeObject((string)TempData["Cart"])));


                var filteredOrders = from a in products
                     where a.product_code == product.product_code
                     select a;

                     if(filteredOrders.Count()>0){
                       products.Where(c => c.product_code==product.product_code).ToList().ForEach(c => c.quantity += 1);

                     }else{
                        products.Add(product);
                     }
       TempData["Cart"] = JsonConvert.SerializeObject(products);
            ViewBag.cart = products.Count(); 
        TempData["carttotal"] = Convert.ToString(products.Sum(a => a.price));
            TempData["Cartqyt"] = products.Count();  
            }  
            return RedirectToAction("Index", "Home");  
        }
    }
}