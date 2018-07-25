using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Tests
{
  [TestClass]
  public class InventorytTests : IDisposable
  {
    public void Dispose()
    {
      // Inventory.DeleteAll();
    }
    public InventorytTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=coffeeshop_test;";
    }

    [TestMethod]
    public void AddInventory()
    {
      Drink newDrink = new Drink("Americano");
      newDrink.Save();
      Inventory newInventory = new Inventory("Milk", 500);
      newInventory.Save();
      Inventory newCoffee = new Inventory("Coffee", 100);
      newCoffee.Save();

      //Act
      newInventory.AddDrink(newDrink);
      newCoffee.AddDrink(newDrink);
    }
    [TestMethod]
    public void ModifyInventory()
    {
      Drink newDrink = new Drink("Latte");
      newDrink.Save();
      Inventory newInventory = new Inventory("Milk", 1000);
      newInventory.Save();
      // newDrink.AddInventory(newInventory);
      Ingredient newIngredients = new Ingredient(newDrink.GetId(),newInventory.GetId(),300);
      newIngredients.Save();
      newInventory.SubtractFromInventory(newDrink.GetId());
      newInventory = Inventory.Find(newInventory.GetId());
      Console.WriteLine(newIngredients.GetAmount());

      Assert.AreEqual(newInventory.GetItemAmount(), 700);
    }
  }
}
