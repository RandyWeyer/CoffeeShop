using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeShop.Models;
using System;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet("/menu")]
        public ActionResult Menu()
        {
            return View();
        }
    }
}
