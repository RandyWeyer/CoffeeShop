using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Controllers
{
  public class InventoryController : Controller
  {
    [HttpGet("/inventory")]
    public ActionResult Index()
    {
      List<Inventory> allItems = Inventory.GetAll();
      return View(Inventory.GetAll());
    }

    [HttpPost("/inventory")]
    public ActionResult CollectInfo()
    {
      Inventory newInventory = new Inventory(Request.Form["item"], int.Parse(Request.Form["itemAmount"]));
      newInventory.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/inventory/new")]
    public ActionResult CreateForm()
    {
      return View(Inventory.GetAll());
    }
  }
}
