using System;

namespace BuyNow.Models
{
    public class ViewModel
    {
        public List<Product>? products {set;get;}
       public List<Product>? CartItems {set;get;}
        
        public double CartTotal {set;get;}

        public string? CartShipping {set;get;}
        public double ShippingAmount {set;get;}
        
    }
}