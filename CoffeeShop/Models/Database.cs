using System;
using MySql.Data.MySqlClient;
using Coffeeshop;

namespace Coffeeshop.Models
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
