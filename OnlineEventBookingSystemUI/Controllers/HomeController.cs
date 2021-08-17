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

       private string controller = "api/UserDetails";
       private System.Net.Http.HttpResponseMessage response;

        public ActionResult Index()
        {
            List<RegistrationViewModel> listViewModel;
            response = GlobalVariables.WebApiClient.GetAsync(controller).Result;
            listViewModel = response.Content.ReadAsAsync<List<RegistrationViewModel>>().Result;
            ViewBag.UserList = listViewModel;
            return View();

        }
        public ActionResult Registration()
        {

            return View(new RegistrationViewModel { });

        }


        [HttpPost]

        public ActionResult Registration(RegistrationViewModel model)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync("Home", model);

            var displayRecord = consume.Result;
            if (displayRecord.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult WelcomePage()
        {

            return View();

        }
    }

}

   
