using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMC.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                return View();
            //}
            //return RedirectToAction("Login", "Account");
        }
    }
}
