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
    [HttpPost("/inventory/delete")]
   public ActionResult DeleteOneItem(int itemId)
   {
     Inventory.Find(itemId).Delete();
     return RedirectToAction("Index");
   }
   [HttpGet("/inventory/{id}/restock")]
   public ActionResult Restock(int id)
   {
     Inventory thisInventory = Inventory.Find(id);
     return View(thisInventory);
   }
   [HttpPost("inventory/{id}/restock")]
   public ActionResult AddStock(int id)
   {
     Inventory thisInventory = Inventory.Find(id);
     thisInventory.Restock(int.Parse(Request.Form["itemAmount"]));
     return RedirectToAction("Index");
   }

  }
}
