using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using CoffeeShop;

namespace CoffeeShop.Models
{
  public class Ingredient
  {
    private int _id;
    private int _drinkId;
    private int _inventoryId;
    private int _amount;

    public Ingredient (int drinkId, int inventoryId, int amount, int id = 0)
    {
      _id = id;
      _drinkId = drinkId;
      _inventoryId = inventoryId;
      _amount = amount;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetDrinkId()
    {
      return _drinkId;
    }
    public int GetInventoryId()
    {
      return _inventoryId;
    }
    public int GetAmount()
    {
      return _amount;
    }
    public override bool Equals(System.Object otherIngredient)
    {
      if (!(otherIngredient is Ingredient))
      {
        return false;
      }
      else
      {
        Ingredient newIngredient = (Ingredient) otherIngredient;
        bool idEquality = (this.GetId() == newIngredient.GetId());
        bool drinkEquality = (this.GetDrinkId() == newIngredient.GetDrinkId());
        bool inventoryEquality = (this.GetInventoryId() == newIngredient.GetInventoryId());
        bool amountEquality = (this.GetAmount() == newIngredient.GetAmount());
        return (idEquality && drinkEquality && inventoryEquality && amountEquality);
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
      cmd.CommandText = @"INSERT INTO ingredients (drink_id, inventory_id, amount) VALUES (@drink_id, @inventory_id, @amount);";

      MySqlParameter drink_id = new MySqlParameter();
      drink_id.ParameterName = "@drink_id";
      drink_id.Value = this._drinkId;
      cmd.Parameters.Add(drink_id);

      MySqlParameter inventory_id = new MySqlParameter();
      inventory_id.ParameterName = "@inventory_id";
      inventory_id.Value = this._inventoryId;
      cmd.Parameters.Add(inventory_id);

      MySqlParameter amount = new MySqlParameter();
      amount.ParameterName = "@amount";
      amount.Value = this._amount;
      cmd.Parameters.Add(amount);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Ingredient Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM ingredients WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ingredientId = 0;
      int drinkId = 0;
      int inventoryId = 0;
      int amount = 0;

      while(rdr.Read())
      {
        ingredientId = rdr.GetInt32(0);
        drinkId = rdr.GetInt32(1);
        inventoryId = rdr.GetInt32(2);
        amount = rdr.GetInt32(3);
      }
      Ingredient newDrink = new Ingredient(drinkId, inventoryId, amount, ingredientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newDrink;
    }
    public static List<Ingredient> GetAll()
    {
      List<Ingredient> allDrinks = new List<Ingredient> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM ingredients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int drinkId = rdr.GetInt32(1);
        int inventoryId = rdr.GetInt32(2);
        int amount = rdr.GetInt32(3);
        Ingredient newDrink = new Ingredient(drinkId, inventoryId, amount, id);
        allDrinks.Add(newDrink);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allDrinks;
    }
    public void addAmount(int newAmount)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE ingredients SET amount=@amount WHERE drink_id=@drink_id AND inventory_id=@inventory_id;";

      MySqlParameter drink_id = new MySqlParameter();
      drink_id.ParameterName = "@drink_id";
      drink_id.Value = this._drinkId;
      cmd.Parameters.Add(drink_id);

      MySqlParameter inventory_id = new MySqlParameter();
      inventory_id.ParameterName = "@inventory_id";
      inventory_id.Value = this._inventoryId;
      cmd.Parameters.Add(inventory_id);

      MySqlParameter amount = new MySqlParameter();
      amount.ParameterName = "@amount";
      amount.Value = newAmount;
      cmd.Parameters.Add(amount);

      cmd.ExecuteNonQuery();
      _amount = newAmount;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Ingredient> GetIngredients(int drink_id)
    {
      List<Ingredient> allIngredients = new List<Ingredient> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM ingredients WHERE drink_id=@drinkID;";

      cmd.Parameters.Add(new MySqlParameter("@drinkID", drink_id));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int drinkId = rdr.GetInt32(1);
        int inventoryId = rdr.GetInt32(2);
        int amount = rdr.GetInt32(3);
        Ingredient newDrink = new Ingredient(drinkId, inventoryId, amount, id);
        allIngredients.Add(newDrink);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allIngredients;

    }



  //hk
  public static List<Inventory> GetInventory(int inventory_id)
  {
    List<Inventory> allIngredients = new List<Inventory> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM inventories WHERE inventory_id=@inventoryID;";

    cmd.Parameters.Add(new MySqlParameter("@inventoryID", inventory_id));
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
  // public void Delete()
  // {
  //   MySqlConnection conn = DB.Connection();
  //   conn.Open();
  //   var cmd = conn.CreateCommand() as MySqlCommand;
  //   cmd.CommandText = @"DELETE FROM ingredients WHERE drink_id = @drinkId;";
  //
  //   MySqlParameter
  // }
    //hk

  }
}
