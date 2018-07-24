// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using CoffeeShop.Models;
// using System;
//
// namespace CoffeeShop.Controllers
// {
//   public class IngredientController : Controller
//   {
//     [HttpGet("/ingredients")]
//     public ActionResult Index()
//     {
//       List<Ingredient> allDrinks = Ingredient.GetAll();
//       return View(Ingredient.GetAll());
//     }
//
//     [HttpPost("/ingredients")]
//     public ActionResult CollectInfo(string newIngredient)
//     {
//       Ingredient newIngredient = new Ingredient(newIngredient);
//       newIngredient.Save();
//       return RedirectToAction("Index");
//     }
//     [HttpGet("/inventory/new")]
//     public ActionResult CreateForm()
//     {
//       return View(Ingredient.GetAll());
//     }
//   }
// }
