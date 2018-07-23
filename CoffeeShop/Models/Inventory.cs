using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using CoffeeShop;

namespace CoffeeShop.Models
{
  public class Inventory
  {
    private int _id;
    private string _item;
    private int _itemAmount;

    public Inventory (string item, int itemAmount, int id = 0)
    {
      _id = id;
      _item = item;
      _itemAmount = itemAmount;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetItem()
    {
      return _item;
    }
    public int GetItemAmount()
    {
      return _itemAmount;
    }
    public override bool Equals(System.Object otherInventory)
    {
      if (!(otherInventory is Inventory))
      {
        return false;
      }
      else
      {
        Inventory newInventory = (Inventory) otherInventory;
        return this.GetId().Equals(newInventory.GetId());
      }

    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO inventories (item) VALUES (@item);";

      MySqlParameter item = new MySqlParameter();
      item.ParameterName = "@item";
      item.Value = this._item;
      cmd.Parameters.Add(item);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Inventory> GetAll()
    {
      List<Inventory> allItems = new List<Inventory> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM inventories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int inventoryId = rdr.GetInt32(0);
        string item = rdr.GetString(1);
        int itemAmount = rdr.GetInt32(2);
        Inventory newItem = new Inventory(item, itemAmount, inventoryId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    public static Inventory Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM inventories WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int inventoryId = 0;
      string item = "";
      int itemAmount = 0;

      while(rdr.Read())
      {
        inventoryId = rdr.GetInt32(0);
        item = rdr.GetString(1);
        itemAmount = rdr.GetInt32(2);
      }
      Inventory newItem = new Inventory(item, itemAmount, inventoryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newItem;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM inventories;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
