﻿using System.Web.Mvc;

namespace HumanResources.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
    }
}