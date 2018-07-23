using System;
using MySql.Data.MySqlClient;
using CoffeeShop;

namespace CoffeeShop.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
