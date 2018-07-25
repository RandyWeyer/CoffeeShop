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

    // [HttpPost("/order")]
    // public ActionResult SubtractAmount(int drink_id)
    // {
    //   Inventory coffee = Inventory.Find(4);
    //
    //   Inventory milk = Inventory.Find(5);
    //   coffee.SubtractFromInventory(drink_id);
    //   milk.SubtractFromInventory(drink_id);
    //
    //   return RedirectToAction("Index");
    // }
    [HttpPost("/order")]
    public ActionResult SubtractAmount(int drink_id)
    {
      Drink newDrink = Drink.Find(drink_id);
      newDrink.MakeDrink();


      return RedirectToAction("Index");
    }
  }
}
