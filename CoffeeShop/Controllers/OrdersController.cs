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
    public ActionResult AddDrink()
    {
      Drink drink = Drink.Find(int.Parse(Request.Form["drink-id"]));
      return RedirectToAction("Index", new);
    }
