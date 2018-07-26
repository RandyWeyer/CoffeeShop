using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Controllers
{
  public class IngredientsController : Controller
  {
    [HttpGet("/ingredients/new")]
    public ActionResult IngredientForm()
    {
      Dictionary<string,object> model = new Dictionary<string,object>{};
      List<Inventory> allInventories = Inventory.GetAll();
      List<Drink> allDrinks = Drink.GetAll();
      model.Add("drinks", allDrinks);
      model.Add("inventories", allInventories);

      return View(model);
    }
    [HttpGet("/ingredients")]
    public ActionResult Index()
    {
      Dictionary<string,object> model = new Dictionary<string,object>{};
      List<Inventory> allInventories = Inventory.GetAll();
      List<Drink> allDrinks = Drink.GetAll();
      List<Ingredient> allIngredients = Ingredient.GetAll();
      model.Add("drinks", allDrinks);
      model.Add("inventories", allInventories);
      model.Add("ingredients", allIngredients);
      return View(model);
    }

    [HttpPost("/ingredients")]
    public ActionResult CollectInfo()
    {
      Ingredient newIngredient = new Ingredient(int.Parse(Request.Form["drink"]), int.Parse(Request.Form["item"]), int.Parse(Request.Form["amount"]));
      newIngredient.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/ingredients/{id}/info")]
    public ActionResult Info(int id)
    {
      Dictionary<string,object> model = new Dictionary<string,object>{};
      Drink thisdrink = Drink.Find(id);
      List<Inventory> allOfInventory = thisdrink.GetInventory();
      List<Ingredient> allOfIngredients = Ingredient.GetIngredients(thisdrink.GetId());
      model.Add("thisdrink", thisdrink);
      model.Add("inventories", allOfInventory);
      model.Add("ingredients", allOfIngredients);
      return View(model);
    }

  }
}
