using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineEventBookingSystem.Models;
using System.Net.Http;
using OnlineEventBookingSystem;
using System.Web.Security;
using System.Net;
using OnlineEventBookingSystemUI.Filters;

namespace OnlineEventBookingSystem.Controllers
{
   
    public class UserController : Controller
    {
        private string controller = "api/UserDetails";
        private HttpResponseMessage response;

        public ActionResult Index()
        {
            List<RegistrationViewModel> listViewModel;
            response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserDetails").Result;
            if (response.IsSuccessStatusCode)
            {
                listViewModel = response.Content.ReadAsAsync<List<RegistrationViewModel>>().Result;
                ViewBag.UserList = listViewModel;
                return View();
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }

        }  
 
        public ActionResult Registration()
        {

            return View(new RegistrationViewModel { });

        }       

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>("api/Login" + "/PostUserDetail", model);
           // var displayRecord = consume.Result;
            if (ModelState.IsValid)
            {
                if (consume.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already exists.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.StatusCode + "...Server Error. Please contact administrator.");
                return View(model);
            }

        }
       
        public ActionResult Login()
        {

            return View(new LoginViewModel { });

        }      

        [HttpPost]        
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<LoginViewModel>("api/Login" + "/UserLogin", loginViewModel).Result;
            
            if (consume.IsSuccessStatusCode && ModelState.IsValid)
            {
                loginViewModel = consume.Content.ReadAsAsync<LoginViewModel>().Result;
                if (loginViewModel != null)
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.User_Name, false);
                   // return RedirectToAction("Index", "EventDetail");
                    if (loginViewModel.IsAdmin == true)
                    {
                        return RedirectToAction("Index", "EventDetail");

                    }
                    else
                    {
                        return RedirectToAction("Index", "UserEventDetail");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Name or Password");
                    return View(loginViewModel);
                }
            }
            else
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + ".Server Error. Please contact administrator.");
                return View(loginViewModel);

            }

        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id).Result;
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);
            }
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        // POST: UserDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(RegistrationViewModel userDetail)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>(controller + "/UpdateUserDetail", userDetail);

                if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, consume.Result.ReasonPhrase + "...Server Error. Please contact administrator.");
                    return View(userDetail);
                }
            
        }
        
        public ActionResult Details(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id).Result;
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);

            }
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/Delete/" + id, id);
            //var delConfirmed = consume.Result;
           

            if (ModelState.IsValid)
            {
                if (consume.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Empty);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.ReasonPhrase +"Server Error. Please contact administrator.");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id).Result;           
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);

            }
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult WelcomeAdminPage()
        {

            return View();
                        
        }

        public ActionResult WelcomePage()
        {
           
            return View();

        }


    }
}