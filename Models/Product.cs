using System;
using Newtonsoft.Json;

namespace BuyNow.Models
{
    public class Product
    {
        private int v1;
        private string? v2;
        private string? v3;
        private double v4;
        private string? v5;
        private string? v6;
        private string? v7;
        private string? v8;
        private double v9;
        private double v10;

        public Product(int v1, string? v2, string? v3, double v4, string? v5, string? v6, string? v7, string? v8, double v9, double v10)
        {
            this.id_product = v1;
            this.product_code = v2;
            this.product_name = v3;
            this.price = v4;
            this.category = v5;
            this.brand = v6;
            this.sp_category = v7;
            this.img = v8;
            this.discount = v9;
            this.old_price = v10;
            this.quantity =1;
        }

      public  int id_product {set;get;}

       public string? product_code {get;set;}

       public string? product_name {get;set;}

      public  double price {get;set;}
        
       public string? category {get;set;}
       public string? brand{get;set;}
       public string? sp_category{get;set;}
       public string? img{get;set;}
       public double discount {get;set;}
       public double old_price{get;set;}

        public int quantity{get;set;}
    public static List<Product> GetProductList(){


        List<Product> prod = new List<Product>();

        DBConnect db = new DBConnect();

        db.CreateConnection();
        string sql = "SELECT * FROM ecomm.products where specific_category in(' Tablets/Tablets/Other Tablets',' Tablets/Telephones ');";

        using(var rec = db.GetRecords(sql)){
            while(rec.Read()){
            prod.Add(
                new Product(Convert.ToInt32(rec["id_product"]),rec["product_code"].ToString(),rec["product_name"].ToString(), Convert.ToDouble(rec["price"]), rec["category"].ToString(), rec["brand"].ToString(), rec["specific_category"].ToString(), rec["img"].ToString(), Convert.ToDouble(rec["discount"]), Convert.ToDouble(rec["old_price"]))
            );
            }
        }
        return prod;

    }
     public static List<Product> GetProductsByCategory(string category){


        List<Product> prod = new List<Product>();

        DBConnect db = new DBConnect();

        db.CreateConnection();
        string sql = "SELECT * FROM ecomm.products where category LIKE '%"+category+ "%' AND (category like 'Computing%' OR category like 'Electronics%' OR category like 'Gaming%' OR category like 'Phone%' OR category like 'Sporting Goods%');";

        using(var rec = db.GetRecords(sql)){
            while(rec.Read()){
            prod.Add(
                new Product(Convert.ToInt32(rec["id_product"]),rec["product_code"].ToString(),rec["product_name"].ToString(), Convert.ToDouble(rec["price"]), rec["category"].ToString(), rec["brand"].ToString(), rec["specific_category"].ToString(), rec["img"].ToString(), Convert.ToDouble(rec["discount"]), Convert.ToDouble(rec["old_price"]))
            );
            }
        }
        return prod;

    }
  public static List<Product> GetProductsByName(string name){


        List<Product> prod = new List<Product>();

        DBConnect db = new DBConnect();

        db.CreateConnection();
        string sql = "SELECT * FROM ecomm.products where product_name LIKE '%"+name+ "%' AND (category like 'Computing%' OR category like 'Electronics%' OR category like 'Gaming%' OR category like 'Phone%' OR category like 'Sporting Goods%');";

        using(var rec = db.GetRecords(sql)){
            while(rec.Read()){
            prod.Add(
                new Product(Convert.ToInt32(rec["id_product"]),rec["product_code"].ToString(),rec["product_name"].ToString(), Convert.ToDouble(rec["price"]), rec["category"].ToString(), rec["brand"].ToString(), rec["specific_category"].ToString(), rec["img"].ToString(), Convert.ToDouble(rec["discount"]), Convert.ToDouble(rec["old_price"]))
            );
            }
        }
        return prod;

    }

    }
}