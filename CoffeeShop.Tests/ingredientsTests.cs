using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Tests
{
  [TestClass]
  public class IngredientTests : IDisposable
  {
    public void Dispose()
    {
      // Ingredient.DeleteAll();
    }
    public IngredientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=coffeeshop_test;";
    }
    [TestMethod]
    public void addAmount_Works_with_SpecifiedIDs()
    {
      Ingredient newIngredients = new Ingredient(1,1,0);
      newIngredients.Save();
      newIngredients.addAmount(300);

      Ingredient newIngredients2 = new Ingredient(1,1,300);
      newIngredients2.Save();
      //assert
      Assert.AreEqual(newIngredients.GetAmount(), newIngredients2.GetAmount());
    }
  }
}
