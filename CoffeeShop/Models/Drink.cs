using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CoffeeShop;
using System;

namespace CoffeeShop.Models
{
  public class Drink
  {
    private int _id;
    private string _name;

    public Drink (string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherDrink)
    {
      if (!(otherDrink is Drink))
      {
        return false;
      }
      else
      {
        Drink newDrink = (Drink) otherDrink;
        bool idEquality = (this.GetId() == newDrink.GetId());
        bool nameEquality = (this.GetName() == newDrink.GetName());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO drinks (name) VALUES (@DrinkName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@DrinkName";
      name.Value = _name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM drinks;";

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
      cmd.CommandText  = @"DELETE FROM drinks WHERE id = @DrinkId;";
      MySqlParameter drinkIdParameter = new MySqlParameter();
      drinkIdParameter.ParameterName = "@DrinkId";
      drinkIdParameter.Value = this.GetId();
      cmd.Parameters.Add(drinkIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Drink> GetAll()
    {
      List<Drink> allDrinks = new List<Drink> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM drinks;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int drinkId = rdr.GetInt32(0);
        string drinkName = rdr.GetString(1);
        Drink newDrink = new Drink(drinkName, drinkId);
        allDrinks.Add(newDrink);
      }
      conn.Close();
      if (conn != null)
        {
          conn.Dispose();
        }
        return allDrinks;
    }
    public static Drink Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM drinks WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName  = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int drinkId = 0;
      string drinkName = "";


      while (rdr.Read())
      {
        drinkId = rdr.GetInt32(0);
        drinkName = rdr.GetString(1);
      }

      Drink foundDrink = new Drink(drinkName, drinkId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundDrink;
    }

    public List<Inventory> GetInventory()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT inventories.* FROM drinks
      JOIN ingredients ON (drinks.id = ingredients.drink_id)
      JOIN inventories ON (ingredients.inventory_id = inventories.id)
      WHERE drinks.id = @drinkId;";

      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@drinkId";
      itemIdParameter.Value = _id;
      cmd.Parameters.Add(itemIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Inventory> inventories = new List<Inventory>{};

      while(rdr.Read())
      {
        int inventoryId = rdr.GetInt32(0);
        string inventoryName = rdr.GetString(1);
        Inventory newInventory = new Inventory(inventoryName, inventoryId);
        inventories.Add(newInventory);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return inventories;
    }
    public void Edit(string newDrink)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE drinks SET name = @newDrink WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newDrink";
      name.Value = newDrink;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newDrink;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddInventory(Inventory newInventory)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO ingredients (drink_id, inventory_id) VALUES (@DrinkId, @InventoryId);";

      MySqlParameter drinkId = new MySqlParameter();
      drinkId.ParameterName = "@DrinkId";
      drinkId.Value = _id;
      cmd.Parameters.Add(drinkId);

      MySqlParameter inventoryId = new MySqlParameter();
      inventoryId.ParameterName = "@InventoryId";
      inventoryId.Value = newInventory.GetId();
      cmd.Parameters.Add(inventoryId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
