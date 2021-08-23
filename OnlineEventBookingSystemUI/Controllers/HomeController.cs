using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineEventBookingSystem.Models;

using System.Net.Http;
namespace OnlineEventBookingSystem.Controllers
{
    public class HomeController : Controller
    {

      
        public ActionResult About()
        {
           
            return View();

        }


        public ActionResult NotFound()
        {
            return View();
        }
    }

}

   
