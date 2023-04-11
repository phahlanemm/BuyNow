using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BuyNow.Models
{
    public class DBConnect
    {
        static string ConnStr ="server=localhost;port=3306;database=ecomm;user=ecomm;password=ifundi@2023";

public MySqlConnection? Conn{get;set;}
       public void CreateConnection(){
   //    MySqlConnection con = new MysqlConnection(ConnStr);
     MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection();
  
try
{
     con.ConnectionString = ConnStr;
    con.Open();

Conn = con;
   // return con;
}
catch (MySql.Data.MySqlClient.MySqlException ex)
{
    Conn=null;
  // return con;
}

        }
        public MySqlDataReader GetRecords(string sql){

             MySqlCommand sqlstatement = new MySqlCommand(sql,Conn);

            return sqlstatement.ExecuteReader();

        }
    }
}