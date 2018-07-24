// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using CoffeeShop.Models;
// using System;
//
// namespace CoffeeShop.Controllers
// {
//   public class InventoryController : Controller
//   {
//     [HttpGet("/inventory")]
//     public ActionResult Index()
//     {
//       List<Inventory> allItems = Inventory.GetAll();
//       return View(Inventory.GetAll());
//     }
//
//     [HttpPost("/inventory")]
//     public ActionResult CollectInfo(string newInventory)
//     {
//       Inventory newInventory = new Inventory(newInventory);
//       newInventory.Save();
//       return RedirectToAction("Index");
//     }
//     [HttpGet("/inventory/new")]
//     public ActionResult CreateForm()
//     {
//       return View(Inventory.GetAll());
//     }
//   }
// }
