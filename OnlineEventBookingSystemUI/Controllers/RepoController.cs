using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineEventBookingSystem.Models;
using System.Net.Http;
using OnlineEventBookingSystem;




namespace OnlineEventBookingSystem.Controllers
{
    public class RepoController : Controller
    {
        private string controller = "api/UserDetails";
        private HttpResponseMessage response;
      
        public ActionResult Index()
        {
            List<RegistrationViewModel> listViewModel ;  
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

            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>(controller, model);

            var displayRecord = consume.Result;
            if (displayRecord.IsSuccessStatusCode)
            {

                Session["UserName"] = model.User_Name.ToString();
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(model);
            }

        }
    }
}