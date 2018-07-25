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
      Drink newDrink = Drink.Find(drink_id);
      newDrink.MakeDrink();


      return RedirectToAction("Index");
    }
  }
}
