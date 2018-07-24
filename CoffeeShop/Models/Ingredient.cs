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
  }
}
