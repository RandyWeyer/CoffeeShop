using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Controllers
{
  public class OrdersController : Controller
  {
    [HttpGet("/order")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Drink> allDrinks = Drink.GetAll();
      model.Add("allDrinks", allDrinks);
      return View(model);
    }

    [HttpPost("/order")]
    public ActionResult SubtractAmount(int drink_id)
    {
      Inventory coffee = Inventory.Find(4);
      Console.Write("Drink Id: ");
      Console.WriteLine(drink_id);
      Console.Write("Coffee Id: ");
      Console.WriteLine(coffee.GetId());
      Inventory milk = Inventory.Find(5);
      // Drink currentDrink = Drink.Find(int.Parse(Request.Form["drink-id"]));
      coffee.SubtractFromInventory(drink_id);
      milk.SubtractFromInventory(drink_id);

      return RedirectToAction("Index");
    }
  }
}
