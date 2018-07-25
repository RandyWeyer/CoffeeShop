using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Tests
{
  [TestClass]
  public class DrinkTests : IDisposable
  {
    public void Dispose()
    {
      Drink.DeleteAll();
    }
    public DrinkTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=coffeeshop_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Drink.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Drink()
    {
      //Arrange, Action
      Drink firstDrink = new Drink("Americano");
      Drink secondDrink = new Drink("Americano");

      //Assert
      Assert.AreEqual(firstDrink, secondDrink);
    }
    [TestMethod]
    public void Save_SavesToDatabase_DrinkList()
    {
      //Arrange
      Drink testDrink = new Drink("Americano");

      //Act
      testDrink.Save();
      List<Drink> result = Drink.GetAll();
      List<Drink> testList = new List<Drink>{testDrink};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Drink testDrink = new Drink("Americano");

      //Act
      testDrink.Save();
      Drink savedDrink = Drink.GetAll()[0];

      int result = savedDrink.GetId();
      int testId = testDrink.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsDrinkInDatabase_Drink()
    {
      //Arrange
      Drink testDrink = new Drink("Americano");
      testDrink.Save();

      //Act
      Drink foundDrink = Drink.Find(testDrink.GetId());

      //Assert
      Assert.AreEqual(testDrink, foundDrink);
    }
    [TestMethod]
    public void Delete_A_Specific_Drink()
    {
      //Arrange
      Drink newDrink1 = new Drink("Americano");
      newDrink1.Save();
      Drink newDrink2 = new Drink("Latte");
      newDrink2.Save();
      Assert.IsTrue(Drink.GetAll().Count == 2);

      //Act
      newDrink1.Delete();
      List<Drink> expectedList = new List<Drink> {newDrink2};

      //Assert
      List<Drink> outputList = Drink.GetAll();
      Assert.IsTrue(outputList.Count == 1);
      CollectionAssert.AreEqual(expectedList, outputList);
    }
    [TestMethod]
    public void Edit_UpdatesDrinkInDatabase_String()
    {
      //Arrange
      string firstName = "Americano";
      Drink testDrink = new Drink(firstName);
      testDrink.Save();
      string secondName = "Latte";

      //Act
      testDrink.Edit(secondName);

      string result = Drink.Find(testDrink.GetId()).GetName();

      //Assert
      Assert.AreEqual(secondName, result);
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
      newDrink.SubtractFromInventory(newInventory.GetId());
      newInventory = Inventory.Find(newInventory.GetId());
      Console.WriteLine(newIngredients.GetAmount());

      Assert.AreEqual(newInventory.GetItemAmount(), 700);

    }
  }
}
