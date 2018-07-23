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
      // Inventoryt.DeleteAll();
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
  }
}
