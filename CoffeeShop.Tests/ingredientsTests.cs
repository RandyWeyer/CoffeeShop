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
  }
}
