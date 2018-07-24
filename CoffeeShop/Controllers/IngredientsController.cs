using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Controllers
{
  public class IngredientsController : Controller
  {
    [HttpGet("/ingredients")]
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
  }
}
