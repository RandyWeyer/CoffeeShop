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
      // Ingredient newIngredient = new Ingredient(drink, item, amount);
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
      // Ingredient newIngredient = new Ingredient(drink, item, amount);
      List<Inventory> allInventories = Inventory.GetAll();
      List<Drink> allDrinks = Drink.GetAll();
      model.Add("drinks", allDrinks);
      model.Add("inventories", allInventories);
      return View(model);
    }

    [HttpPost("/ingredients")]
    public ActionResult CollectInfo()
    {
      Ingredient newIngredient = new Ingredient(int.Parse(Request.Form["drink"]), int.Parse(Request.Form["item"]), int.Parse(Request.Form["amount"]));
      newIngredient.Save();
      return RedirectToAction("Index");
    }
  }
}
