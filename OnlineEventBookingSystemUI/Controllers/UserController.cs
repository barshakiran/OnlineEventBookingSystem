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
   
    public class RepoController : Controller
    {
        private string controller = "api/UserDetails";
        private HttpResponseMessage response;

        
        public ActionResult Index()
        {
            List<RegistrationViewModel> listViewModel;
            response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserDetails").Result;
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
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>("api/Login" + "/PostUserDetail", model);
            var displayRecord = consume.Result;
            if (ModelState.IsValid)
            {
                if (displayRecord.IsSuccessStatusCode)
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
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
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
            loginViewModel = consume.Content.ReadAsAsync<LoginViewModel>().Result;
            if (ModelState.IsValid)
            {
                if (loginViewModel.User_Name != null)
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.User_Name, false);
                    if (loginViewModel.IsAdmin == true)
                    {
                        return RedirectToAction("Index");

                    }
                    else 
                    {
                        return RedirectToAction("WelcomePage");
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
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
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
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            if (registrationVM == null)
            {
                return HttpNotFound();
            }
            return View(registrationVM);
        }

        // POST: UserDetails/Edit/5
        [HttpPost]

        public ActionResult Edit(RegistrationViewModel userDetail)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>(controller + "/UserDetail", userDetail);
            var displayRecord = consume.Result;
            if (ModelState.IsValid)
            {


                if (displayRecord.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is something wrong with the edit...");
                    return View(userDetail);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(userDetail);
            }
           
           // return RedirectToAction("Index");
        }

        
        public ActionResult Details(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id).Result;
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            if (registrationVM == null)
            {
                return HttpNotFound();
            }
            return View(registrationVM);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]

        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/Delete/" + id, id);
            var delConfirmed = consume.Result;
           

            if (ModelState.IsValid)
            {
                if (delConfirmed.IsSuccessStatusCode)
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
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
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
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            if (registrationVM == null)
            {
                return HttpNotFound();
            }
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