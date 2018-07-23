using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Coffeeshop.Models;
using System;

namespace Coffeeshop.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
