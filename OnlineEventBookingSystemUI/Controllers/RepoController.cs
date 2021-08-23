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

namespace OnlineEventBookingSystem.Controllers
{
    [Authorize]
    [HandleError]
    public class RepoController : Controller
    {
        private string controller = "api/UserDetails";
        private HttpResponseMessage response;

        [Authorize(Roles = "admin", Users = "Admin")]
        public ActionResult Index()
        {
            List<RegistrationViewModel> listViewModel;
            response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserDetails").Result;
            listViewModel = response.Content.ReadAsAsync<List<RegistrationViewModel>>().Result;
            ViewBag.UserList = listViewModel;
            return View();

        }  

        [AllowAnonymous]
        public ActionResult Registration()
        {

            return View(new RegistrationViewModel { });

        }       
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationViewModel model)
        {

            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>(controller + "/PostUserDetail", model);

            var displayRecord = consume.Result;
            if (ModelState.IsValid)
            {


                if (displayRecord.IsSuccessStatusCode)
                {

                    //Session["UserName"] = model.User_Name.ToString();
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

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View(new LoginViewModel { });

        }      
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<LoginViewModel>(controller + "/UserLogin", loginViewModel).Result;
            loginViewModel = consume.Content.ReadAsAsync<LoginViewModel>().Result;
            if (ModelState.IsValid)
            {
                if (loginViewModel.User_Name != null)
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.User_Name, false);
                   // Session["UserName"] = loginViewModel.User_Name;
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


        //[HttpPost]
        //public ActionResult Login(LoginViewModel loginViewModel)
        //{

        //    var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<LoginViewModel>(controller + "/UserLogin", loginViewModel);

        //    var displayRecord = consume.Result;
        //    if (ModelState.IsValid)
        //    {


        //        if (displayRecord.IsSuccessStatusCode)
        //        {
        //            if (loginViewModel.IsAdmin == false)
        //            {
        //                Session["UserName"] = loginViewModel.User_Name.ToString();
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                Session["UserName"] = loginViewModel.User_Name.ToString();
        //                return RedirectToAction("Index");
        //            }
        //        }

        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "User don't exists.");
        //            return View(loginViewModel);
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        //        return View(loginViewModel);
        //    }

        //}

        // GET: UserDetails/Edit/5

        [Authorize(Roles = "admin", Users = "Admin")]
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin", Users = "Admin")]
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
        [Authorize(Roles = "admin", Users = "Admin")]
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

        [Authorize(Roles = "admin", Users = "Admin")]
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

        [Authorize]
        public ActionResult WelcomePage()
        {
           
            return View();

        }


    }
}