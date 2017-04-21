using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCard.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Users users)
        {
            if (ModelState.IsValid)
            {
               return View("SentCard", users);
            }
            else
            {
               return View();
            }
        }

        public ActionResult SentCard()
        {
            return View();
        }
    }
}