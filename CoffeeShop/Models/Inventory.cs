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
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO inventories (item, item_amount) VALUES (@item, @item_amount);";

      MySqlParameter item = new MySqlParameter();
      item.ParameterName = "@item";
      item.Value = this._item;
      cmd.Parameters.Add(item);

      MySqlParameter itemAmount = new MySqlParameter();
      itemAmount.ParameterName = "@item_amount";
      itemAmount.Value = this._itemAmount;
      cmd.Parameters.Add(itemAmount);

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

    public void AddDrink(Drink newDrink)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO ingredients (drink_id, inventory_id) VALUES (@DrinkId, @InventoryId);";

      MySqlParameter drinkId = new MySqlParameter();
      drinkId.ParameterName = "@DrinkId";
      drinkId.Value = newDrink.GetId();
      cmd.Parameters.Add(drinkId);

      MySqlParameter inventoryId = new MySqlParameter();
      inventoryId.ParameterName = "@InventoryId";
      inventoryId.Value = _id;
      cmd.Parameters.Add(inventoryId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText  = @"DELETE FROM inventories WHERE id = @ItemId;";
      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@ItemId";
      itemIdParameter.Value = this.GetId();
      cmd.Parameters.Add(itemIdParameter);

      cmd.ExecuteNonQuery();
    }

    public static List<Inventory> GetInventory(int drink_id)
    {
      List<Inventory> allIngredients = new List<Inventory> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM inventories WHERE drink_id=@drinkID;";

      cmd.Parameters.Add(new MySqlParameter("@drinkID", drink_id));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string item = rdr.GetString(1);
        int itemAmount = rdr.GetInt32(2);
        Inventory newInventory = new Inventory(item, itemAmount, id);
        allIngredients.Add(newInventory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allIngredients;

    }
    public void SubtractFromInventory(int drink_id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE inventories
      JOIN ingredients ON (inventories.id = ingredients.inventory_id)
      JOIN drinks ON (drinks.id = ingredients.drink_id)
      SET inventories.item_amount=(inventories.item_amount - ingredients.amount)
      WHERE inventories.id = @InventoryId AND drinks.id = @DrinkId;";

      cmd.Parameters.Add(new MySqlParameter("@InventoryId", _id));
      cmd.Parameters.Add(new MySqlParameter("@DrinkId", drink_id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //hk
    public void Restock(int newItemAmount)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE inventories SET item_amount = (@newItemAmount + inventories.item_amount) WHERE id =@InventoryId;";

      MySqlParameter inventoryIdParameter = new MySqlParameter();
      inventoryIdParameter.ParameterName = "@InventoryId";
      inventoryIdParameter.Value = _id;
      cmd.Parameters.Add(inventoryIdParameter);

      MySqlParameter restockAmount = new MySqlParameter();
      restockAmount.ParameterName = "@newItemAmount";
      restockAmount.Value = newItemAmount;
      cmd.Parameters.Add(restockAmount);

      cmd.ExecuteNonQuery();
      _itemAmount = newItemAmount;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
